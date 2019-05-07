
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Text



Public Class frmPackRchkA
    'Private SQL As New SQLConn

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







    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Try
            PExecQuery("Select * FROM Jobs Where RECHECKBARCODE = '" & frmJobEntry.txtLotNumber.Text & "' ")

            If PRecordCount > 0 Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                DGVPakingRecA.DataSource = PDS.Tables(0)
                DGVPakingRecA.Rows(0).Selected = True
                Dim PCB As SqlCommandBuilder = New SqlCommandBuilder(PDA)


                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
                'DGVPakingA.Sort(DGVPakingA.Columns("CONENUM"), ListSortDirection.Ascending)  'sorts On cone number
                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE by our own index
                DGVPakingRecA.Sort(DGVPakingRecA.Columns("RECHKIDX"), ListSortDirection.Ascending)  'sorts On cone number

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
            writeerrorLog.writelog("PAckRchk Load error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Drum Scan Error", ex.ToString, False, "System Fault")

            MsgBox("DRUM BarCode Is Not Valid " & vbNewLine & ex.Message)

        End Try

        txtboxTotal.Text = toAllocatedCount

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        'IF THIS IS AN EXISTING JOB THEN CALL BACK VALUES FROM DATABASE
        If frmJobEntry.coneValUpdate Then UpdateConeVal()


        If My.Settings.debugSet Then DGVPakingRecA.Visible = True

        Me.txtConeBcode.Clear()
        Me.txtConeBcode.Focus()


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
                    Me.Controls("btnCone" & rw).BackColor = Color.Green       'Grade A Cone
                End If

                If DGVPakingRecA.Rows(rw - 1).Cells(9).Value = "15" Then
                    Me.Controls("btnCone" & rw).BackColor = Color.LightGreen       'Grade A Cone
                End If

                Me.Controls("btnCone" & rw).Enabled = False
            Next

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

        bcodeScan = txtConeBcode.Text
        Dim curcone As String


        Today = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")



        Try



            For i = 1 To PRecordCount


                If DGVPakingRecA.Rows(i - 1).Cells(9).Value = "8" And IsDBNull(DGVPakingRecA.Rows(i - 1).Cells("RECHKRESULT").Value) Then Continue For

                If DGVPakingRecA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value = "8" And DGVPakingRecA.Rows(i - 1).Cells("RECHKRESULT").Value = "A" Then
                    curcone = DGVPakingRecA.Rows(i - 1).Cells("RECHKIDX").Value
                    Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.LightGreen       'Grade A Cone
                    DGVPakingRecA.Rows(i - 1).Cells("RECHK").Value = 5
                    DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value = "15"
                    DGVPakingRecA.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                    DGVPakingRecA.Rows(i - 1).Cells("OPNAME").Value = frmJobEntry.varUserName
                    DGVPakingRecA.Rows(i - 1).Cells("CARTENDTM").Value = today

                    'CHECK TO SEE IF DATE ALREADY SET FOR END TIME
                    If IsDBNull(DGVPakingRecA.Rows(i - 1).Cells("PACKENDTM").Value) Then
                        'For rows As Integer = 1 To rowendcount
                        DGVPakingRecA.Rows(i - 1).Cells("PACKENDTM").Value = Today  'PACKING CHECK END TIME.
                        'Next
                    End If


                    allocatedCount = allocatedCount + 1
                    curcone = 0

                ElseIf DGVPakingRecA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value = "15" Then
                    Label1.Visible = True
                    Label1.Text = "Cheese already allocated"
                    DelayTM()
                    Label1.Visible = False
                ElseIf DGVPakingRecA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value < "8" Then
                    curcone = DGVPakingRecA.Rows(i - 1).Cells("CONENUM").Value
                    psorterror = 1
                    Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.Red      'Wrong Cone scanned
                    DGVPakingRecA.Rows(i - 1).Cells("PSORTERROR").Value = psorterror
                    DGVPakingRecA.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                    DGVPakingRecA.Rows(i - 1).Cells("CONESTATE").Value = "14"
                    DGVPakingRecA.Rows(i - 1).Cells("CARTENDTM").Value = today


                    Me.Hide()
                    frmRemoveCone.Show()
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

        If toAllocatedCount = allocatedCount Then
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
            Me.Close()
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            frmJobEntry.Show()
        End If

    End Sub



    Private Sub UpdateDatabase()

        tsbtnSave()





        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            If PDS.HasChanges Then


                'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

                PDA.Update(PDS.Tables(0))

            End If
        Catch ex As Exception

            'Write error to Log File
            writeerrorLog.writelog("db update Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("db update Error", ex.ToString, False, "System Fault")

            MsgBox("Update Error: " & vbNewLine & ex.Message)




        End Try



        If PConn.State = ConnectionState.Open Then PConn.Close()
        DGVPakingRecA.ClearSelection()
        DGVPakingRecA.DataSource = Nothing  'used to clear DGV
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        frmJobEntry.Show()
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



    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            prgContinue()


        End If

    End Sub


End Class