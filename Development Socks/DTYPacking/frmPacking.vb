
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Text



Public Class frmPacking
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




    Dim psorterror As String = 0
    Dim varVisConeInspect As String
    Dim coneBarley As String = 0
    Dim coneZero As String = 0
    Dim coneM10 As String = 0
    Dim coneP10 As String = 0
    Dim coneM30 As String = 0
    Dim coneP30 As String = 0
    Dim coneM50 As String = 0
    Dim coneP50 As String = 0
    Dim btnImage As Image
    Dim keepDefcodes As Integer
    Public bcodeScan As String = ""
    Dim clr As String = ""
    Public curcone As String = 0
    Public toAllocatedCount As Integer 'count of cones requierd to be scanned
    Public allocatedCount As Integer 'count of cones scanned
    Public itemCount As Integer = 0
    'ReCheck Params
    Dim reChecked, ReCheckTime As String
    Public removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
    Dim incoming As String
    Public measureOn As String
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




    'Faults


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        PExecQuery("Select * FROM jobs WHERE bcodecart = '" & frmJobEntry.dbBarcode & "' ;")

        'LOAD THE DATA FROM dB IN TO THE DATAGRID
        DGVPakingA.DataSource = PDS.Tables(0)
        DGVPakingA.Rows(0).Selected = True
        Dim PCB As SqlCommandBuilder = New SqlCommandBuilder(PDA)


        'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
        DGVPakingA.Sort(DGVPakingA.Columns("CONENUM"), ListSortDirection.Ascending)  'sorts On cone number




        'SET VRIABLE NUMBER OF BUTTONS IF PILOT MC
        If frmJobEntry.varMachineCode = 29 Then rowendcount = DGVPakingA.Rows.Count Else rowendcount = 32



        Dim btnNum As Integer
        Dim btnNums As String

        If frmJobEntry.varMachineCode = 29 Then
            btnNums = 1
        Else
            btnNums = frmJobEntry.varCartSelect
        End If
        ''btnNums = frmJobEntry.varCartSelect

        ' SELECT CONE NUMBER RANGE BASED ON CART NUMBER
        Select Case btnNums
            Case Is = 1
                btnNum = 1
                coneNumOffset = 0
            Case Is = 2
                btnNum = 33
                coneNumOffset = 32
            Case Is = 3
                btnNum = 65
                coneNumOffset = 64
            Case Is = 4
                btnNum = 97
                coneNumOffset = 96
            Case Is = 5
                btnNum = 129
                coneNumOffset = 128
            Case Is = 6
                btnNum = 161
                coneNumOffset = 160
            Case Is = 7
                btnNum = 193
                coneNumOffset = 192
            Case Is = 8
                btnNum = 225
                coneNumOffset = 224
            Case Is = 9
                btnNum = 257
                coneNumOffset = 256
            Case Is = 10
                btnNum = 289
                coneNumOffset = 288
            Case Is = 11
                btnNum = 321
                coneNumOffset = 320
            Case Is = 12
                btnNum = 353
                coneNumOffset = 352
        End Select




        For i As Integer = 1 To rowendcount

            Me.Controls("btnCone" & i.ToString).Text = btnNum
            btnNum = btnNum + 1
        Next



        ''SET CORRECT BUTTUN NUMBERS BASED ON CONE NUMBERS (SPINDEL NUMBERS)
        'For i As Integer = 1 To 32

        '    Me.Controls("btnCone" & i.ToString).Text = btnNum
        '    btnNum = btnNum + 1

        'Next


        Me.txtCartNum.Text = frmJobEntry.cartSelect
        Me.lblJobNum.Text = frmJobEntry.varJobNum

        'CHECK THAT WE HAVE CORRECT VALUES FOR CONESTATES
        Dim cstate As String
        For i = 1 To rowendcount
            cstate = DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value

            Select Case cstate

                Case Is = 0, 5
                    If DGVPakingA.Rows(i - 1).Cells("MISSCONE").Value > 0 Or
                        DGVPakingA.Rows(i - 1).Cells("DEFCONE").Value > 0 Or
                        DGVPakingA.Rows(i - 1).Cells("CONEBARLEY").Value > 0 Or
                        DGVPakingA.Rows(i - 1).Cells("M30").Value > 0 Or
                        DGVPakingA.Rows(i - 1).Cells("P30").Value > 0 Or
                        DGVPakingA.Rows(i - 1).Cells("M50").Value > 0 Or
                        DGVPakingA.Rows(i - 1).Cells("M50").Value > 0 Or
                        DGVPakingA.Rows(i - 1).Cells("DYEFLECK").Value > 0 Or
                        DGVPakingA.Rows(i - 1).Cells("COLDEF").Value > 0 Or
                        DGVPakingA.Rows(i - 1).Cells("COLWASTE").Value > 0 Then
                        ' DGVPakingA.Rows(i - 1).Cells("RECHKRESULT").Value = "AD" Or
                        '   DGVPakingA.Rows(i - 1).Cells("RECHKRESULT").Value = "AL" Then

                        DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = 8
                        DGVPakingA.Rows(i - 1).Cells("COLENDTM").Value = DGVPakingA.Rows(i - 1).Cells("SORTENDTM").Value

                        'INFORMATION FOT CSV LOG FILE
                        fltconeNum = DGVPakingA.Rows(i - 1).Cells("CONENUM").Value.ToString
                        csvRowNum = i - 1
                        CSV()
                    Else

                        DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = 9
                        DGVPakingA.Rows(i - 1).Cells("COLENDTM").Value = DGVPakingA.Rows(i - 1).Cells("SORTENDTM").Value

                        'INFORMATION FOT CSV LOG FILE
                        fltconeNum = DGVPakingA.Rows(i - 1).Cells("CONENUM").Value.ToString
                        csvRowNum = i - 1
                        CSV()
                    End If

            End Select


        Next




        'GET NUMBER OF CONES THAT NEED ALLOCATING Count agains Job Barcode

        For i = 1 To rowendcount
            If DGVPakingA.Rows(i - 1).Cells(9).Value = "9" And DGVPakingA.Rows(i - 1).Cells("FLT_S").Value = "False" Then
                toAllocatedCount = toAllocatedCount + 1
            End If
        Next



        ''For i = 1 To 32
        ''    If  DGVPakingA.Rows(i - 1).Cells(9).Value = "9" And  DGVPakingA.Rows(i - 1).Cells("FLT_S").Value = "False" Then
        ''        toAllocatedCount = toAllocatedCount + 1
        ''    End If
        ''Next

        'GET NUMBER OF CONES THAT NEED ALLOCATING Count agains Job Barcode
        If frmJobEntry.varMachineCode = 29 Then
            Dim btnCountStart As Integer = rowendcount + 1
            Dim totBtn As Integer = 31 - btnCountStart
            For i = btnCountStart To 32
                Me.Controls("btnCone" & i.ToString).Visible = False
            Next
        End If



        txtboxTotal.Text = toAllocatedCount

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        'IF THIS IS AN EXISTING JOB THEN CALL BACK VALUES FROM DATABASE
        If frmJobEntry.coneValUpdate Then UpdateConeVal()


        If My.Settings.debugSet Then DGVPakingA.Visible = True

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

            PException = "ExecQuery Error: " & vbNewLine & ex.Message
            MsgBox(PException)

        End Try

    End Sub


    'Create csv file

    Private Sub CSV()

        Dim today As String = DateAndTime.Now
        Dim csvFile As String

        'Check to see if file exists, if it does not creat the file, otherwise add data to the file
        Dim dataOut As String = String.Concat(frmJobEntry.varMachineCode, ",", frmJobEntry.varMachineName, ",", frmJobEntry.varYear, ",", frmJobEntry.varMonth, ",", frmJobEntry.varDoffingNum, ",", fltconeNum, ",", frmJobEntry.mergeNum, ",", frmJobEntry.varUserName, ",", DGVPakingA.Rows(csvRowNum).Cells("CONESTATE"), ",", DGVPakingA.Rows(csvRowNum).Cells("SHORTCONE").Value, ",", DGVPakingA.Rows(csvRowNum).Cells("MISSCONE").Value, ",", DGVPakingA.Rows(csvRowNum).Cells("DEFCONE").Value, ",", DGVPakingA.Rows(csvRowNum).Cells("BCODECART").Value, ",", DGVPakingA.Rows(csvRowNum).Cells("M30").Value, ",", DGVPakingA.Rows(csvRowNum).Cells("P30").Value, ",", DGVPakingA.Rows(csvRowNum).Cells("CARTSTARTTM").Value, ",", DGVPakingA.Rows(csvRowNum).Cells("CARTENDTM").Value, ",", today & Environment.NewLine)


        csvFile = My.Settings.dirCarts & ("\" & DGVPakingA.Rows(csvRowNum).Cells("BCODECART").Value.ToString & "PackLog.csv")


        If fileActive Then

            Dim outFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(csvFile, True)
            outFile.WriteLine("M/C Code, M/C Name, YY, MM, Doff #, Cone #, Merge #,User, Cone State, Short, NoCone, Defect, Cart Name, -30, +30,Start, End, Fault time ")
            outFile.WriteLine(dataOut)
            outFile.Close()

        Else

            Dim outFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(csvFile, False)
            outFile.WriteLine("M/C Code, M/C Name, YY, MM, Doff #, Cone #, Merge #,User, Cone State, Short, NoCone, Defect, Cart Name, -30, +30,Start, End, Fault time ")

            outFile.WriteLine(dataOut)
            outFile.Close()
            fileActive = True


        End If







    End Sub

    Public Sub UpdateConeVal()
        If My.Settings.debugSet Then frmDGV.Show()



        For rw As Integer = 1 To rowendcount

            If DGVPakingA.Rows(rw - 1).Cells(9).Value = "9" And DGVPakingA.Rows(rw - 1).Cells("FLT_S").Value = False Then
                Me.Controls("btnCone" & rw).BackColor = Color.Green       'Grade A Cone
            End If

            If DGVPakingA.Rows(rw - 1).Cells(9).Value = "15" Then
                Me.Controls("btnCone" & rw).BackColor = Color.LightGreen       'Grade A Cone
            End If

            Me.Controls("btnCone" & rw).Enabled = False
        Next




    End Sub





    Private Sub btnDefect_Click(sender As Object, e As EventArgs) Handles btnDefect.Click
        Me.Hide()
        packingActive = 1

        frmPackingFault.Show()


    End Sub






    'Private Sub txtConeBcode_TextChanged(sender As Object, e As EventArgs) Handles txtConeBcode.TextChanged

    Private Sub prgContinue()




        bcodeScan = txtConeBcode.Text
        Dim curcone As String
        Dim coneCount As Integer = 0
        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")





        For i = 1 To rowendcount



            If DGVPakingA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = "9" And DGVPakingA.Rows(i - 1).Cells("FLT_S").Value = False Then
                curcone = DGVPakingA.Rows(i - 1).Cells("CONENUM").Value
                Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.LightGreen       'Grade A Cone
                DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = "15"
                DGVPakingA.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                DGVPakingA.Rows(i - 1).Cells("OPNAME").Value = frmJobEntry.varUserName
                DGVPakingA.Rows(i - 1).Cells("CARTENDTM").Value = today

                'CHECK TO SEE IF DATE ALREADY SET FOR END TIME
                If IsDBNull(DGVPakingA.Rows(i - 1).Cells("PACKENDTM").Value) Then
                    'For rows As Integer = 1 To rowendcount
                    DGVPakingA.Rows(i - 1).Cells("PACKENDTM").Value = DateAndTime.Today  'PACKING CHECK END TIME.
                    'Next
                End If


                allocatedCount = allocatedCount + 1
                endCheck()
                curcone = 0

            ElseIf DGVPakingA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = "15" Then
                Label1.Visible = True
                Label1.Text = "Cheese already allocated"
                DelayTM()
                Label1.Visible = False
            ElseIf DGVPakingA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value < "9" Or DGVPakingA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = "9" And DGVPakingA.Rows(i - 1).Cells("FLT_S").Value = True Then
                curcone = DGVPakingA.Rows(i - 1).Cells("CONENUM").Value
                psorterror = 1
                Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.Red      'Wrong Cone scanned
                DGVPakingA.Rows(i - 1).Cells("PSORTERROR").Value = psorterror
                DGVPakingA.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = "14"
                DGVPakingA.Rows(i - 1).Cells("CARTENDTM").Value = today

                'UPDATE ALL CHEESE ON CART AS PROCESSED TODAY FOR DAILY PACKING REPORT TO WORK

                'If IsDBNull( DGVPakingA.Rows(0).Cells("PACKENDTM").Value) Then
                '    For rows As Integer = 1 To rowendcount
                '        If My.Settings.chkUseColour = True Then  DGVPakingA.Rows((rows - 1) - coneNumOffset).Cells("PACKENDTM").Value = varCartEndTime 'PACKING CHECK END TIME
                '    Next
                'End If

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


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'frmPackReport.Hide()

    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If PConn.State = ConnectionState.Open Then PConn.Close()
        DGVPakingA.ClearSelection()
        DGVPakingA.DataSource = Nothing  'used to clear DGV
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Close()
    End Sub



    Public Sub endCheck()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If toAllocatedCount = allocatedCount Then
            curcone = 0

            '**************************************************************************************************************
            'UPDATE ALL CHEESE ON CART AS PROCESSED TODAY FOR DAILY PACKING REPORT TO WORK


            If IsDBNull(DGVPakingA.Rows(0).Cells("PACKCARTTM").Value) Then
                For x As Integer = 1 To 32
                    DGVPakingA.Rows(x - 1).Cells("PACKCARTTM").Value = Today 'PACKING CHECK END TIME
                Next
            End If
            '**************************************************************************************************************

            'frmPackReport.packPrint() 'Print the packing report and go back to Job Entry for the next cart
            frmPackRepMain.PackRepMainSub()
            frmPackRepMain.Close()
            UpdateDatabase()

        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
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

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try



        If PConn.State = ConnectionState.Open Then PConn.Close()
        DGVPakingA.ClearSelection()
        DGVPakingA.DataSource = Nothing  'used to clear DGV
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        frmJobEntry.Show()
        Me.Close()



    End Sub

    Public Sub tsbtnSave()


        Dim bAddState As Boolean = DGVPakingA.AllowUserToAddRows
        'Dim iRow As Integer =  DGVPakingA.CurrentRow.Index
        DGVPakingA.AllowUserToAddRows = True
        DGVPakingA.CurrentCell = DGVPakingA.Rows(DGVPakingA.Rows.Count - 1).Cells(0) ' move to add row
        DGVPakingA.CurrentCell = DGVPakingA.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        DGVPakingA.AllowUserToAddRows = bAddState
        DGVPakingA.EndEdit()


    End Sub

    Private Sub btnCone32_Click(sender As Object, e As EventArgs) Handles btnCone32.Click

    End Sub

    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            prgContinue()


        End If

    End Sub


End Class