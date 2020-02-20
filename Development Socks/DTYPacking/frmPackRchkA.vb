
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Text



Public Class frmPackRchkA
    'GIVES ACCESS TO GLOBAL SQL CLASS
    Private SQL As New SQLConn


    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Private PConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private PCmd As SqlCommand

    'SQL CONNECTORS
    Private PDA As SqlDataAdapter
    Private PDS As DataSet
    Private PDT As DataTable
    Private PCB As SqlCommandBuilder

    Private PRecordCount As Integer
    Private PException As String
    ' SQL QUERY PARAMETERS
    Private PParams As New List(Of SqlParameter)
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError

    'THIS CREATS LOCAL INSTANCE xlConeCount Class
    Private getConeCount As New xlConeCount

    Dim psorterror As String = 0
    Public bcodeScan As String = ""
    Public curcone As String = 0
    Public toAllocatedCount As Integer 'count of cones requierd to be scanned
    Public allocatedCount As Integer 'count of cones scanned
    Dim reChecked, ReCheckTime As String
    Public removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
    Public NoCone As Integer
    Public defect As Integer

    Public varCartStartTime As String   'Record time that we started measuring
    Public varCartEndTime As String
    Public coneNumOffset As Integer
    Dim varConeBCode As String
    Dim fileActive As Integer
    Public varConeNum As Integer
    Private coneCount As Integer
    Public coneState As String
    Public packingActive = 0
    Private rowendcount As Integer
    Dim fltconeNum As String
    Dim csvRowNum As String

    Public saveJob As Integer = 0
    Public finJob As Integer

    'Variables used to display remaining on sheet and number left to finish sheet
    Dim xlcheesecount As Integer
    Dim packedCheese As Integer
    Dim remainingCheese As Integer
    Dim pauseScan As Integer = 0  'Stop barcode entry when 1





    Private Sub frmPackRchkA_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Try
            PExecQuery("Select * FROM Jobs Where RECHECKBARCODE = '" & frmJobEntry.txtLotNumber.Text & "' Order by RECHKIDX  ")

            If PRecordCount > 0 Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                DGVPakingRecA.DataSource = PDS.Tables(0)
                DGVPakingRecA.Rows(0).Selected = True
                Dim PCB As SqlCommandBuilder = New SqlCommandBuilder(PDA)


            Else

                MsgBox("There are no Grade A Cheese on the cart")
                frmJobEntry.Show()
                frmJobEntry.txtLotNumber.Clear()
                frmJobEntry.txtLotNumber.Focus()
                Me.Close()
            End If


            Dim btnNum As Integer = 1
            Dim btnNums As String = 1
            coneNumOffset = 0




            For i = 1 To 32

                Me.Controls("btnCone" & i.ToString).Text = btnNum
                btnNum = btnNum + 1
            Next


            Me.txtCartNum.Text = 1
            Me.lblJobNum.Text = (frmJobEntry.varProductName & "  " & frmJobEntry.varProductCode)




            'GET NUMBER OF CONES THAT NEED ALLOCATING Count agains Job Barcode

            For i As Integer = 1 To PRecordCount
                If DGVPakingRecA.Rows(i - 1).Cells(9).Value = "8" And IsDBNull(DGVPakingRecA.Rows(i - 1).Cells("RECHKRESULT").Value) Then Continue For
                If DGVPakingRecA.Rows(i - 1).Cells(9).Value = "8" And DGVPakingRecA.Rows(i - 1).Cells("RECHKRESULT").Value = "A" Then
                    toAllocatedCount = toAllocatedCount + 1
                End If
            Next

        Catch ex As Exception

            'Write error to Log File
            writeerrorLog.writelog("PackRchk Load error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("PackRchk Load Error", ex.ToString, False, "System Fault")

            MsgBox("PackRchk Load " & vbNewLine & ex.Message)

        End Try

        'THIS SECTION GETS THE COUNT OF CHEESE ON THE LAST EXCEL SHEET TO DISPLAY NUMBER LEFT TO COMPLETE THE PACK SHEET 
        sheetconecount()

        txtBoxToFinish.Text = remainingCheese
        txtBoxOnSheet.Text = packedCheese

        txtboxTotal.Text = toAllocatedCount

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        'IF THIS IS AN EXISTING JOB THEN CALL BACK VALUES FROM DATABASE
        If frmJobEntry.coneValUpdate Then UpdateConeVal()


        If My.Settings.debugSet Then DGVPakingRecA.Visible = True

        Me.txtConeBcode.Clear()
        Me.txtConeBcode.Focus()


    End Sub

    Private Sub sheetconecount()

        'Go off to Class and get the cone count on any excel sheet for this grade from last 3 days
        getConeCount.xlCheck()

        Dim searchstring = getConeCount.searchBarcode

        SQL.ExecQuery("Select * from jobs where packsheetbcode = '" & searchstring & "'  ")
        xlcheesecount = SQL.RecordCount
        If xlcheesecount > 0 Then
            packedCheese = xlcheesecount   'this is the number of cheese already included on the excel sheet
            remainingCheese = 90 - packedCheese
        Else
            packedCheese = 0
            remainingCheese = 90
        End If

    End Sub




    Public Sub PExecQuery(Query As String)
        ' RESET QUERY STATISTCIS
        PRecordCount = 0
        PException = ""


        If PConn.State = ConnectionState.Open Then PConn.Close()
        Try

            'OPEN SQL DATABSE CONNECTION
            PConn.Open()

            'CREATE SQL COMMAND
            PCmd = New SqlCommand(Query, PConn)

            'LOAD PARAMETER INTO SQL COMMAND
            PParams.ForEach(Sub(p) PCmd.Parameters.Add(p))

            'CLEAR PARAMETER LIST
            PParams.Clear()

            'EXECUTE COMMAND AND FILL DATASET
            PDS = New DataSet
            PDT = New DataTable
            PDA = New SqlDataAdapter(PCmd)

            PRecordCount = PDA.Fill(PDS)

        Catch ex As Exception

            'Write error to Log File
            writeerrorLog.writelog("SQL Access Error", ex.Message, False, "SQL Client Fault")
            writeerrorLog.writelog("SQL Access Error", ex.ToString, False, "SQL Client Fault")

            PException = "ExecQuery Error: " & vbNewLine & ex.Message
            MsgBox(PException)

        End Try

    End Sub



    Public Sub UpdateConeVal()
        If My.Settings.debugSet Then DGVPakingRecA.Show()


        Try


            For rw As Integer = 1 To PRecordCount
                If DGVPakingRecA.Rows(rw - 1).Cells(9).Value = "8" And IsDBNull(DGVPakingRecA.Rows(rw - 1).Cells("RECHKRESULT").Value) Then Continue For

                If DGVPakingRecA.Rows(rw - 1).Cells(9).Value = "8" And DGVPakingRecA.Rows(rw - 1).Cells("RECHKRESULT").Value = "A" Then
                    Me.Controls("btnCone" & rw).BackColor = Color.Green       'Grade A Cone not packed
                End If

                If DGVPakingRecA.Rows(rw - 1).Cells(9).Value = "15" Then
                    Me.Controls("btnCone" & rw).BackColor = Color.LightGreen       'Grade A Cone and Packed
                End If

                Me.Controls("btnCone" & rw).Enabled = False
            Next

            txtboxTotal.Text = toAllocatedCount
        Catch ex As Exception

            'Write error to Log File
            writeerrorLog.writelog("Update Cone error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Update Cone error", ex.ToString, False, "System Fault")
        End Try



    End Sub





    Private Sub btnDefect_Click(sender As Object, e As EventArgs) Handles btnDefect.Click
        Me.Hide()
        packingActive = 1

        frmPackingFault.Show()


    End Sub


    Private Sub prgContinue()


        If txtConeBcode.TextLength <> 15 Then
            Label1.Visible = True
            Label1.Text = "BARCODE ERROR not a cheese BARCODE"
            DelayTM()
            Label1.Visible = False
            Exit Sub
        End If





        bcodeScan = txtConeBcode.Text
        Dim curcone As String


        Dim Today = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")



        Try



            For i = 1 To PRecordCount


                If DGVPakingRecA.Rows(i - 1).Cells(9).Value = 8 And IsDBNull(DGVPakingRecA.Rows(i - 1).Cells("RECHKRESULT").Value) Then Continue For

                If DGVPakingRecA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value = 8 And DGVPakingRecA.Rows(i - 1).Cells("RECHKRESULT").Value = "A" Then
                    curcone = DGVPakingRecA.Rows(i - 1).Cells("RECHKIDX").Value
                    Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.LightGreen       'Grade A Cone
                    DGVPakingRecA.Rows(i - 1).Cells("RECHK").Value = 5
                    DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value = 14
                    DGVPakingRecA.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                    DGVPakingRecA.Rows(i - 1).Cells("OPNAME").Value = frmJobEntry.varUserName
                    DGVPakingRecA.Rows(i - 1).Cells("CARTENDTM").Value = Today

                    'CHECK TO SEE IF DATE ALREADY SET FOR END TIME
                    If IsDBNull(DGVPakingRecA.Rows(i - 1).Cells("PACKENDTM").Value) Then
                        'For rows As Integer = 1 To rowendcount
                        DGVPakingRecA.Rows(i - 1).Cells("PACKENDTM").Value = Today  'PACKING CHECK END TIME.
                        'Next
                    End If


                    'Section to adjust Counts on screen
                    allocatedCount = allocatedCount + 1

                    packedCheese = packedCheese + 1
                    remainingCheese = remainingCheese - 1

                    If packedCheese = 90 Then
                        packedCheese = 0
                        remainingCheese = 90
                    End If

                    txtBoxOnSheet.Text = packedCheese
                    txtBoxToFinish.Text = remainingCheese

                    curcone = 0

                ElseIf DGVPakingRecA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value >= 14 Then
                    Label1.Visible = True
                    Label1.Text = "Cheese already allocated"
                    DelayTM()
                    Label1.Visible = False
                    txtConeBcode.Clear()
                    txtConeBcode.Refresh()
                    txtConeBcode.Focus()
                ElseIf DGVPakingRecA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value < 8 Then
                    curcone = DGVPakingRecA.Rows(i - 1).Cells("RECHKIDX").Value
                    psorterror = 1
                    Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.Red      'Wrong Cone scanned
                    DGVPakingRecA.Rows(i - 1).Cells("PSORTERROR").Value = psorterror
                    DGVPakingRecA.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                    DGVPakingRecA.Rows(i - 1).Cells("CARTENDTM").Value = Today

                    Me.Hide()
                    frmRemoveCone.Show()


                    'Label1.Visible = True
                    'Label1.Text = "You Have scanned a Cheese that is not 'GRADE A'"
                    'DelayTM()
                    'Label1.Visible = False
                    txtConeBcode.Clear()
                    txtConeBcode.Refresh()
                    txtConeBcode.Focus()
                    psorterror = 0
                    curcone = 0
                    Continue For

                ElseIf DGVPakingRecA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value = 8 And DGVPakingRecA.Rows(i - 1).Cells("RECHKRESULT").Value = "AL" Or
                        DGVPakingRecA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value = 8 And DGVPakingRecA.Rows(i - 1).Cells("RECHKRESULT").Value = "AD" Then
                    curcone = DGVPakingRecA.Rows(i - 1).Cells("RECHKIDX").Value
                    psorterror = 1
                    Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.Red      'Wrong Cone scanned
                    DGVPakingRecA.Rows(i - 1).Cells("PSORTERROR").Value = psorterror
                    DGVPakingRecA.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                    DGVPakingRecA.Rows(i - 1).Cells("CARTENDTM").Value = Today




                    Me.Hide()
                    frmRemoveCone.Show()




                    txtConeBcode.Clear()
                    txtConeBcode.Refresh()
                    txtConeBcode.Focus()
                    psorterror = 0
                    curcone = 0
                    Continue For



                Else
                    txtConeBcode.Clear()
                    txtConeBcode.Refresh()
                    txtConeBcode.Focus()

                End If
            Next

        Catch ex As Exception

            'Write error to Log File
            writeerrorLog.writelog("barcode Scan Error", ex.Message, False, "User Fault")
            writeerrorLog.writelog("barcode Scan Error", ex.ToString, False, "User Fault")
        End Try

        endCheck()

    End Sub

    Private Sub DelayTM()
        Dim interval As Integer = "2000"
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            If My.Settings.audioAlarm Then
                My.Computer.Audio.Play(My.Resources.toray_warning, AudioPlayMode.WaitToComplete)
            End If
            Application.DoEvents()
        Loop
        sw.Stop()

    End Sub



    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If PConn.State = ConnectionState.Open Then PConn.Close()
        DGVPakingRecA.ClearSelection()
        DGVPakingRecA.DataSource = Nothing  'used to clear DGV
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Close()
    End Sub



    Public Sub endCheck()

        If toAllocatedCount = allocatedCount Or saveJob = 1 Or finJob = 1 Then
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            curcone = 0

            '**************************************************************************************************************
            'UPDATE ALL CHEESE ON CART AS PROCESSED TODAY FOR DAILY PACKING REPORT TO WORK


            If IsDBNull(DGVPakingRecA.Rows(0).Cells("PACKCARTTM").Value) Then
                For x As Integer = 1 To PRecordCount
                    DGVPakingRecA.Rows(x - 1).Cells("PACKCARTTM").Value = Today 'PACKING CHECK END TIME
                Next
            End If
            '**************************************************************************************************************

            'frmPackReport.packPrint() 'Print the packing report and go back to Job Entry for the next cart
            frmPackRepMain.PackRepMainSub()
            frmPackRepMain.Close()
            UpdateDatabase()
            Me.Cursor = System.Windows.Forms.Cursors.Default

            saveJob = 0
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            Me.Close()

        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub



    Private Sub UpdateDatabase()

        pauseScan = 1 'Stop Barcode entry

        'tsbtnSave()

        'NEW db UPDATE Routine not using CommandBuilder
        Try

            For i = 1 To PRecordCount   'This is all cheese on the DGV



                'load parameters for cheese to write
                Dim id As String = DGVPakingRecA.Rows(i - 1).Cells("id_Product").Value
                    Dim conestate As String = DGVPakingRecA.Rows(i - 1).Cells("conestate").Value
                    Dim oppack As String = DGVPakingRecA.Rows(i - 1).Cells("OpPack").Value
                    Dim opname = DGVPakingRecA.Rows(i - 1).Cells("OpName").Value
                    Dim packendtm = DGVPakingRecA.Rows(i - 1).Cells("Packendtm").Value
                    Dim psorterror = DGVPakingRecA.Rows(i - 1).Cells("PSORTERROR").Value
                    Dim cartendtm = DGVPakingRecA.Rows(i - 1).Cells("CartEndTm").Value
                    Dim recheck = DGVPakingRecA.Rows(i - 1).Cells("RECHK").Value
                    Dim cartsheet = DGVPakingRecA.Rows(i - 1).Cells("PACKSHEETBCODE").Value
                    Dim cartonno = DGVPakingRecA.Rows(i - 1).Cells("CARTONNUM").Value

                    SQL.AddParam("@id", DGVPakingRecA.Rows(i - 1).Cells("id_Product").Value)
                    SQL.AddParam("@conestate", DGVPakingRecA.Rows(i - 1).Cells("conestate").Value)
                    SQL.AddParam("@oppack", DGVPakingRecA.Rows(i - 1).Cells("OpPack").Value)
                    SQL.AddParam("@opname", DGVPakingRecA.Rows(i - 1).Cells("OpName").Value)
                    SQL.AddParam("@packendtm", DGVPakingRecA.Rows(i - 1).Cells("Packendtm").Value)
                    SQL.AddParam("@psorterror", DGVPakingRecA.Rows(i - 1).Cells("PSORTERROR").Value)
                    SQL.AddParam("@cartendtm", DGVPakingRecA.Rows(i - 1).Cells("CartEndTm").Value)
                    SQL.AddParam("@rechk", DGVPakingRecA.Rows(i - 1).Cells("RECHK").Value)
                    SQL.AddParam("@carton", DGVPakingRecA.Rows(i - 1).Cells("CARTONNUM").Value)
                    SQL.AddParam("@packsheet", DGVPakingRecA.Rows(i - 1).Cells("PACKSHEETBCODE").Value)



                'MsgBox("ID = " & id.ToString & vbCrLf _
                '       & "coneState = " & conestate.ToString & vbCrLf _
                '       & "rechk =" & recheck.ToString & vbCrLf _
                '       & "oppack = " & oppack.ToString & vbCrLf _
                '       & "opname = " & opname.ToString & vbCrLf _
                '       & "packendtm = " & packendtm.ToString & vbCrLf _
                '       & "psorterror = " & psorterror.ToString & vbCrLf _
                '       & "cartendtm = " & cartendtm.ToString & vbCrLf)



                SQL.ExecQuery(" Update jobs set CONESTATE = @conestate, OPPACK = @oppack, OPNAME = @opname, PACKENDTM = @packendtm, " _
                          & "PSORTERROR = @psorterror, CARTENDTM = @cartendtm, RECHK = @rechk,PACKSHEETBCODE = @packsheet, CARTONNUM = @carton  Where id_product = @id")




            Next

        Catch dbcx As DBConcurrencyException
            Dim Response As String

            Response = dbcx.Row.ToString
            writeerrorLog.writelog("db ReChk Con Error", Response, False, "ReChk Con Fault")
            Response = dbcx.RowCount.ToString
            writeerrorLog.writelog("db ReChk Con Error", Response, False, "ReChk Fault")


        Catch ex As Exception

            Dim sheetNo As String = frmJobEntry.txtLotNumber.Text
        'Write error to Log File
        writeerrorLog.writelog("Sheet No.", sheetNo, False, "Packing sheet")

        'Write error to Log File
        writeerrorLog.writelog("db ReCheckPack Error", ex.Message, False, "db ReCheckPack Fault")
        writeerrorLog.writelog("db ReCheckPack Error", ex.ToString, False, "db ReCheckPack Fault")

        MsgBox("Update Error: " & vbNewLine & ex.Message)

        pauseScan = 0 'Allow barcode entry


        End Try











        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        'Try

        '    If PDS.HasChanges Then


        '        PDA.Update(PDS.Tables(0))

        '    End If

        'Catch dbcx As DBConcurrencyException
        '    Dim Response As String

        '    Response = dbcx.Row.ToString
        '    writeerrorLog.writelog("db ReChk Con Error", Response, False, "ReChk Con Fault")
        '    Response = dbcx.RowCount.ToString
        '    writeerrorLog.writelog("db ReChk Con Error", Response, False, "ReChk Fault")


        'Catch ex As Exception

        '    Dim sheetNo As String = frmJobEntry.txtLotNumber.Text
        '    'Write error to Log File
        '    writeerrorLog.writelog("Sheet No.", sheetNo, False, "Packing sheet")

        '    'Write error to Log File
        '    writeerrorLog.writelog("db ReCheckPack Error", ex.Message, False, "db ReCheckPack Fault")
        '    writeerrorLog.writelog("db ReCheckPack Error", ex.ToString, False, "db ReCheckPack Fault")

        '    MsgBox("Update Error: " & vbNewLine & ex.Message)

        '    pauseScan = 0 'Allow barcode entry


        'End Try



        If PConn.State = ConnectionState.Open Then PConn.Close()
        DGVPakingRecA.ClearSelection()
        DGVPakingRecA.DataSource = Nothing  'used to clear DGV
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        frmJobEntry.Show()
        pauseScan = 0 'Allow barcode entry
        Me.Close()



    End Sub

    Public Sub tsbtnSave()


        Dim bAddState As Boolean = DGVPakingRecA.AllowUserToAddRows
        'Dim iRow As Integer =  DGVPakingA.CurrentRow.Index
        DGVPakingRecA.AllowUserToAddRows = True
        DGVPakingRecA.CurrentCell = DGVPakingRecA.Rows(DGVPakingRecA.Rows.Count - 1).Cells(0) ' move to add row
        DGVPakingRecA.CurrentCell = DGVPakingRecA.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        DGVPakingRecA.AllowUserToAddRows = bAddState
        'DGVPakingRecA.EndEdit()


    End Sub

    Private Sub btnSaveJob_Click(sender As Object, e As EventArgs) Handles btnSaveJob.Click

        saveJob = 1
        endCheck()

    End Sub

    Private Sub btnFinJob_Click(sender As Object, e As EventArgs) Handles btnFinJob.Click

        finJob = 1
        endCheck()


    End Sub

    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If pauseScan = 0 Then
            If e.KeyCode = Keys.Return Then

                prgContinue()


            End If
        End If

    End Sub


End Class