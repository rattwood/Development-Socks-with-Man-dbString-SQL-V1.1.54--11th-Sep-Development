'Imports System.Data.DataTable
Imports System.Data.SqlClient
Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering



Public Class frmJobEntry
    'THIS CREATS LOCAL INSTANCE TO REFRENCE THE SQL CLASS FORM, NOT USED WHEN WORKING WITH DATAGRIDVIEW
    Private SQL As New SQLConn


    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Public LConn As New SQLConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private LCmd As SQLCommand

    'SQL CONNECTORS
    Public LDA As SQLDataAdapter
    Public LDS As DataSet
    Public LDT As DataTable
    Public LCB As SQLCommandBuilder

    Public LRecordCount As Integer
    Private LException As String
    ' SQL QUERY PARAMETERS
    Public LParams As New List(Of SQLParameter)
    '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    Public cartSelect
    Public varCartSelect
    Public varUserName
    Public varJobNum
    Public varMachineCode
    Public varMachineName
    Public varProductCode
    Public varYear
    Public varMonth
    Public varDoffingNum
    Public varCartNum
    Public varProductName
    Public varSpNums
    Public varCartBCode
    Public varCartNameA As String
    Public varCartNameB As String
    Public mergeNum As String
    Public dbBarcode As String
    Public coneValUpdate As Integer
    Public JobBarcode As String
    Public varProdWeight As String
    Public varweightcode As String


    Dim machineName As String = ""
    Dim machineCode As String
    Dim productCode As String
    Dim year As String
    Dim month As String
    Dim doffingNum As String
    Dim cartNum As String
    Dim quit As Integer
    Public cartReport As Integer

    Public SortOP As String
    Public PackOp As String
    Public ColorOP As String
    Public PackSortOP As String
    Public changeCone As Integer
    Public time As DateTime = DateTime.Now
    Public Format As String = "dd mm yyyy  HH:mm"



    Private Sub frmJobEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Me.txtLotNumber.Visible = False

        If My.Settings.chkUseColour Then btnCartReport.Visible = True Else btnCartReport.Visible = False
        If My.Settings.chkUseColour Then btnJobReport.Visible = True Else btnJobReport.Visible = False
        If My.Settings.chkUseColour Then btnDefRep.Visible = True Else btnDefRep.Visible = False
        If My.Settings.chkUsePack Then btnExChangeCone.Visible = True Else btnExChangeCone.Visible = False
        If My.Settings.chkUsePack Then btnSearchCone.Visible = True Else btnSearchCone.Visible = False
        If My.Settings.chkUsePack Then btnReports.Visible = True Else btnReports.Visible = False

        If My.Settings.chkUseSort = False And My.Settings.chkUseColour = False And My.Settings.chkUsePack = False Then
            MsgBox("Please edit SETTINGS for type of User")
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
        End If

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        'Set Form Header text
        If My.Settings.chkUseSort Then Me.Text = "Job Entry Sorting"
        If My.Settings.chkUseColour Then Me.Text = "Job Entry Colour"
        If My.Settings.chkUsePack Then Me.Text = "Job Entry Packing"


        Me.btnCancelReport.Visible = False


    End Sub

    Public Sub txtOperator_TextChanged(sender As Object, e As EventArgs) Handles txtOperator.TextChanged

        txtLotNumber.Visible = True

        If My.Settings.chkUseSort Then
            SortOP = txtOperator.Text
        ElseIf My.Settings.chkUseColour Then
            ColorOP = txtOperator.Text
        ElseIf My.Settings.chkUsePack Then
            PackOp = txtOperator.Text
        End If



        varUserName = txtOperator.Text

    End Sub




    'Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
    Private Sub prgContinue()
        Dim chkBCode As String
        'Routine to check Barcode is TRUE
        Try

            chkBCode = txtLotNumber.Text.Substring(12, 1)

            If chkBCode = "B" Then
                If txtLotNumber.TextLength > 14 Then  ' For carts B10,11 & 12
                    cartNum = txtLotNumber.Text.Substring(12, 3)
                Else
                    cartNum = txtLotNumber.Text.Substring(12, 2)
                End If




            Else
                MsgBox("This is not a CART Barcode Please RE Scan")
                Me.txtLotNumber.Clear()

                Me.txtLotNumber.Focus()
                Me.txtLotNumber.Refresh()
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox("BarCcode Is Not Valid")
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            Exit Sub
        End Try

        CreateJob()

    End Sub

    Private Sub CreateJob()

        If txtLotNumber.TextLength > 14 Then  ' For carts B10,11 & 12
            machineName = ""
            machineCode = txtLotNumber.Text.Substring(0, 2)
            productCode = txtLotNumber.Text.Substring(2, 3)
            year = txtLotNumber.Text.Substring(5, 2)
            month = txtLotNumber.Text.Substring(7, 2)
            doffingNum = txtLotNumber.Text.Substring(9, 3)
            cartNum = txtLotNumber.Text.Substring(12, 3)
        Else
            machineName = ""                                    ' For carts B1 - 9
            machineCode = txtLotNumber.Text.Substring(0, 2)
            productCode = txtLotNumber.Text.Substring(2, 3)
            year = txtLotNumber.Text.Substring(5, 2)
            month = txtLotNumber.Text.Substring(7, 2)
            doffingNum = txtLotNumber.Text.Substring(9, 3)
            cartNum = txtLotNumber.Text.Substring(12, 2)

        End If

        varCartBCode = txtLotNumber.Text

        If machineCode = 21 Then
            machineName = "11D1"        'Left Side
        ElseIf machineCode = 22 Then
            machineName = "11D2"        'Right Side
        ElseIf machineCode = 23 Then
            machineName = "12D1"        'Left Side
        ElseIf machineCode = 24 Then
            machineName = "12D2"        'Right Side
        ElseIf machineCode = 25 Then
            machineName = "21D1"        'Left Side
        ElseIf machineCode = 26 Then
            machineName = "21D2"        'Right Side
        ElseIf machineCode = 27 Then
            machineName = "22D1"        'Left Side
        ElseIf machineCode = 28 Then
            machineName = "22D2"        'Right Side
        End If

        'Dim cartSelect As String
        If machineCode = 21 Or machineCode = 23 Or machineCode = 25 Or machineCode = 27 Then    ' Set Left Side of Machine

            If cartNum = "B1" Or cartNum = "B2" Then
                varCartNameA = "B1"
                varCartNameB = "B2"
                cartSelect = 1
                varSpNums = "001 - 032"
            ElseIf cartNum = "B3" Or cartNum = "B4" Then
                varCartNameA = "B3"
                varCartNameB = "B4"
                cartSelect = 2
                varSpNums = "033 - 064"
            ElseIf cartNum = "B5" Or cartNum = "B6" Then
                varCartNameA = "B5"
                varCartNameB = "B6"
                cartSelect = 3
                varSpNums = "065 - 096"
            ElseIf cartNum = "B7" Or cartNum = "B8" Then
                varCartNameA = "B7"
                varCartNameB = "B8"
                cartSelect = 4
                varSpNums = "097 - 128"
            ElseIf cartNum = "B9" Or cartNum = "B10" Then
                varCartNameA = "B9"
                varCartNameB = "B10"
                cartSelect = 5
                varSpNums = "129 - 160"
            ElseIf cartNum = "B11" Or cartNum = "B12" Then
                varCartNameA = "B11"
                varCartNameB = "B12"
                cartSelect = 6
                varSpNums = "161 - 192"

            End If
        End If


        If machineCode = 22 Or machineCode = 24 Or machineCode = 26 Or machineCode = 28 Then  ' Set Right Side of Machine
            If cartNum = "B1" Or cartNum = "B2" Then
                varCartNameA = "B1"
                varCartNameB = "B2"
                cartSelect = 7
                varSpNums = "193 - 224"
            ElseIf cartNum = "B3" Or cartNum = "B4" Then
                varCartNameA = "B3"
                varCartNameB = "B4"
                cartSelect = 8
                varSpNums = "225 - 256"
            ElseIf cartNum = "B5" Or cartNum = "B6" Then
                varCartNameA = "B5"
                varCartNameB = "B6"
                cartSelect = 9
                varSpNums = "257 - 288"
            ElseIf cartNum = "B7" Or cartNum = "B8" Then
                varCartNameA = "B7"
                varCartNameB = "B8"
                cartSelect = 10
                varSpNums = "289 - 320"
            ElseIf cartNum = "B9" Or cartNum = "B10" Then
                varCartNameA = "B9"
                varCartNameB = "B10"
                cartSelect = 11
                varSpNums = "321 - 352"
            ElseIf cartNum = "B11" Or cartNum = "B12" Then
                varCartNameA = "B11"
                varCartNameB = "B12"
                cartSelect = 12
                varSpNums = "353 - 384"

            End If
        End If

        varMachineCode = machineCode
        varMachineName = machineName
        varProductCode = productCode
        varYear = year
        varMonth = month
        varDoffingNum = doffingNum
        varCartNum = cartNum
        varCartSelect = cartSelect


        varJobNum = (machineName & " " & month & " " & doffingNum & " " & varCartNameA)

        'Routine to change the scanned BARCODE to be the First CART not the secone cart and this is what will be stored in the DATABASE

        dbBarcode = txtLotNumber.Text.Replace(varCartNum, varCartNameA)



        If My.Settings.chkUseColour Or My.Settings.chkUseSort Then CheckJob()
        If My.Settings.chkUsePack Then PackScree1()



    End Sub

    Public Sub LExecQuery(Query As String)
        ' RESET QUERY STATISTCIS
        LRecordCount = 0
        LException = ""


        If LConn.State = ConnectionState.Open Then LConn.Close()
        Try

            'OPEN SQL DATABSE CONNECTION
            LConn.Open()

            'CREATE SQL COMMAND
            LCmd = New SqlCommand(Query, LConn)

            'LOAD PARAMETER INTO SQL COMMAND
            LParams.ForEach(Sub(p) LCmd.Parameters.Add(p))

            'CLEAR PARAMETER LIST
            LParams.Clear()

            'EXECUTE COMMAND AND FILL DATASET
            LDS = New DataSet
            LDT = New DataTable
            LDA = New SqlDataAdapter(LCmd)

            LRecordCount = LDA.Fill(LDS)

        Catch ex As Exception

            LException = "ExecQuery Error: " & vbNewLine & ex.Message
            MsgBox(LException)

        End Try

    End Sub


    Public Sub CheckJob()

        LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & dbBarcode & "'")

        If LRecordCount > 0 Then

            Dim result = MessageBox.Show("Edit Job Yes Or No", "JOB ALREADY EXISTS", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If result = DialogResult.Yes Then

                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)


                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(6), ListSortDirection.Ascending)  'sorts On cone number

                'Dim LCB As SQLCommandBuilder = New SQLCommandBuilder(LDA)

                coneValUpdate = 1

                frmCart1.Show()
                If My.Settings.debugSet Then frmDGV.Show()

                Me.Hide()
                Exit Sub
            End If

            If result = DialogResult.No Then
                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Focus()

            End If
        Else
            If My.Settings.chkUseColour Or My.Settings.chkUsePack Then
                MsgBox("Job does not Exist, you must creat new Job from Sort Computer")
                txtLotNumber.Clear()
                txtLotNumber.Focus()
                Exit Sub
            End If

            CreatNewJob()

            If quit Then
                quit = 0
                txtLotNumber.Clear()
                txtLotNumber.Focus()
                Exit Sub
            End If
            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)
            LDA.UpdateCommand = New SqlCommandBuilder(LDA).GetUpdateCommand
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True
            frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(6), ListSortDirection.Ascending)  'sorts On cone number
            frmCart1.Show()
            If My.Settings.debugSet Then frmDGV.Show()

            Me.Hide()
        End If




    End Sub

    Private Sub CreatNewJob()

        ' RESET QUERY STATISTCIS
        LRecordCount = 0
        LException = ""
        If LConn.State = ConnectionState.Open Then LConn.Close()


        Dim coneNumStart As Integer
        Dim coneNumStop As Integer
        Dim cartSelNumber As String

        cartSelNumber = varCartSelect

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        ' Auto buton numbering based on Cart being measuerd
        Select Case cartSelNumber
            Case Is = 1
                coneNumStart = 1
                coneNumStop = 32
            Case Is = 2
                coneNumStart = 33
                coneNumStop = 64
            Case Is = 3
                coneNumStart = 65
                coneNumStop = 96
            Case Is = 4
                coneNumStart = 97
                coneNumStop = 128
            Case Is = 5
                coneNumStart = 129
                coneNumStop = 160
            Case Is = 6
                coneNumStart = 161
                coneNumStop = 192
            Case Is = 7
                coneNumStart = 193
                coneNumStop = 224
            Case Is = 8
                coneNumStart = 225
                coneNumStop = 256
            Case Is = 9
                coneNumStart = 257
                coneNumStop = 288
            Case Is = 10
                coneNumStart = 289
                coneNumStop = 320
            Case Is = 11
                coneNumStart = 321
                coneNumStop = 352
            Case Is = 12
                coneNumStart = 353
                coneNumStop = 384
        End Select

        'CONSTRUCT ROWS

        'Dim rowData As String()
        Dim x = 1
        Dim fmt As String = "000"    'FORMAT STRING FOR NUMBER
        Dim modConeNum As String
        Dim modLotStr = txtLotNumber.Text.Substring(0, 12)
        Dim coneBarcode As String
        Dim cartName As String
        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")

        'If My.Settings.chkUseSort And My.Settings.chkUseColour = False Then today = "04-Feb-1960"


        'Routine to check get product name and merge number and load in to variables then clear grid
        'LExecQuery("SELECT PRODNAME,MERGENUM,PRODWEIGHT,WEIGHTCODE FROM PRODUCT WHERE PRNUM = '" & varProductCode & "'")
        LExecQuery("SELECT PRODNAME,MERGENUM,PRODWEIGHT,WEIGHTCODE FROM PRODUCT WHERE PRNUM = '" & varProductCode & "'")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True

            varProductName = frmDGV.DGVdata.Rows(0).Cells(0).Value.ToString
            mergeNum = frmDGV.DGVdata.Rows(0).Cells(1).Value.ToString
            varProdWeight = frmDGV.DGVdata.Rows(0).Cells(2).Value.ToString
            varweightcode = frmDGV.DGVdata.Rows(0).Cells(3).Value.ToString
            If My.Settings.debugSet Then frmDGV.Show()

            frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV

        Else
            MsgBox("PRODUCT NUMBER " & varProductCode & " VALUE DOES NOT EXIST")
            quit = 1
            Exit Sub

        End If


        For i As Integer = coneNumStart To coneNumStop

            If x <= 16 Then cartName = varCartNameA Else cartName = varCartNameB  'SETS CORRECT CART NUMBER

            x = x + 1
            modConeNum = i.ToString(fmt)   'FORMATS THE CONE NUMBER TO 3 DIGITS
            coneBarcode = modLotStr & modConeNum   'CREATE THE CONE BARCODE NUMBER
            JobBarcode = modLotStr


            LExecQuery("INSERT INTO jobs (MCNUM, PRNUM, PRYY, PRMM, DOFFNUM, CONENUM, MERGENUM, OPNAME,CONESTATE," _
               & "SHORTCONE, MISSCONE, DEFCONE, CARTNUM, CARTNAME, CONEZERO, CONEBARLEY, M10, P10, M30, P30, M50, P50, CARTSTARTTM," _
              & "BCODECART, BCODECONE,FLT_K, FLT_D, FLT_F, FLT_O, FLT_T, FLT_P, FLT_S, FLT_X, FLT_N, FLT_W, FLT_H, FLT_TR, FLT_B, FLT_C," _
               & "MCNAME, PRODNAME, BCODEJOB,OPPACKSORT,OPPACK,OPSORT,PSORTERROR,WEIGHTERROR,WEIGHT,CARTONNUM,SORTERROR,COLOURERROR,DYEFLECK," _
               & "COLDEF, COLWASTE, FLT_DO, FLT_DH, FLT_CL, FLT_FI, FLT_YN, FLT_HT, FLT_LT, CONEMD, CONEML) " _
              & "VALUES ('" & varMachineCode & "', '" & varProductCode & "','" & varYear & "','" & varMonth & "','" & varDoffingNum & "','" & modConeNum & "'," _
              & "'" & mergeNum & "',  ' ', '0', '0', '0', '0', '" & varCartSelect & "','" & cartName & "', '0', '0', '0', '0', '0', '0', '0', '0','" & today & "','" & dbBarcode & "','" & coneBarcode & "'," _
             & "'False', 'False', 'False', 'False', 'False', 'False', 'False', 'False', 'False', 'False', 'False', 'False', 'False', 'False', '" & varMachineName & "','" & varProductName & "', '" & JobBarcode & "'," _
             & "'0','0','0','0','0','0','0','0','0','0','0','0','False','False','False','False','False','False','False','0','0')")


        Next

        LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & dbBarcode & "'")

        Me.Cursor = System.Windows.Forms.Cursors.Default
        If LRecordCount > 1 Then
            Exit Sub
        Else
            MsgBox("Records Not created")
        End If


    End Sub

    Private Sub PackScree1()


        'GET PRODUCT WEIGHT INFORMATION
        LExecQuery("SELECT PRODWEIGHT,WEIGHTCODE FROM PRODUCT WHERE PRNUM = '" & varProductCode & "'")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True

            varProdWeight = frmDGV.DGVdata.Rows(0).Cells(0).Value.ToString
            varweightcode = frmDGV.DGVdata.Rows(0).Cells(1).Value.ToString


            frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV

        Else
            MsgBox("PRODUCT NUMBER " & varProductCode & " THIS PRODUCT IS NOT IN THE PRODUCT LIST")
            quit = 1
            Exit Sub

        End If

        LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & dbBarcode & "' AND CONESTATE = '9' and FLT_S = 'False'")

        If LRecordCount > 0 Then
            LExecQuery("Select * FROM jobs WHERE bcodecart = '" & dbBarcode & "' ;")

            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True
            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)


            'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
            frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(5), ListSortDirection.Ascending)  'sorts On cone number

            coneValUpdate = 1
            Me.Hide()
            frmPacking.Show()

        Else

            LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & dbBarcode & "' AND CONESTATE = '15'")

            If LRecordCount > 0 Then
                Label3.Visible = True

                Label3.Text = "Cart has already been allocated"

                DelayTM()
                Label3.Visible = False

            Else
                LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & dbBarcode & "' AND CONESTATE = '5'")
                If LRecordCount > 0 Then

                    Label3.Visible = True

                    Label3.Text = "Cart Has not been COLOUR CHECKED"

                    DelayTM()
                    Label3.Visible = False
                Else
                    Label3.Visible = True

                    Label3.Text = "Cart Has No Grade 'A' Cheese"


                    DelayTM()
                    Label3.Visible = False
                End If
            End If


                Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()

        End If

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





    Private Sub btnSettings_Click_1(sender As Object, e As EventArgs) Handles btnSettings.Click
        frmPassword.Show()
    End Sub

    Private Sub btnJobReport_Click(sender As Object, e As EventArgs) Handles btnJobReport.Click

        frmDGVJobReport.Show()

    End Sub

    Private Sub btnCartReport_Click(sender As Object, e As EventArgs) Handles btnCartReport.Click

        Me.txtLotNumber.Visible = False
        Me.txtBoxCartReport.Visible = True
        Me.btnJobReport.Visible = False
        btnCancelReport.Visible = True
        Me.txtBoxCartReport.Focus()
        cartReport = 1



    End Sub

    Private Sub cartReportSub()


        If txtBoxCartReport.Text = "" Then
            MsgBox("Please enter Barcode first")
            Me.txtBoxCartReport.Focus()
            Exit Sub
        End If




        Dim modLotStr = txtBoxCartReport.Text.Substring(0, 12)


        LExecQuery("SELECT * FROM JOBS WHERE BCODEJOB = '" & modLotStr & "' ")

        If LRecordCount > 0 Then
            'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
            'If LRecordCount = 192 Then

            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmPrintCartReport.DGVcartReport.DataSource = LDS.Tables(0)
                frmPrintCartReport.DGVcartReport.Rows(0).Selected = True

                'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
                frmPrintCartReport.DGVcartReport.Sort(frmPrintCartReport.DGVcartReport.Columns(6), ListSortDirection.Ascending)  'sorts On cone number
                'frmPrintCartReport.Show()
                Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
                frmPrintCartReport.prtCartSheet()
                Me.Cursor = System.Windows.Forms.Cursors.Default

            'Else
            '    MsgBox("Not all cones present Please check all carts have been checked")

            '    Me.txtBoxCartReport.Visible = False
            '    Me.btnCancelReport.Visible = False
            '    Me.btnJobReport.Visible = True
            '    Me.txtLotNumber.Visible = True
            '    Me.txtLotNumber.Clear()
            '    Me.txtLotNumber.Focus()
            '    Me.txtLotNumber.Refresh()

            '    Exit Sub
            'End If
        Else

            MsgBox("No Job Found, Please check if this Job has been checked")

            Me.txtBoxCartReport.Visible = False
            Me.btnCancelReport.Visible = False
            Me.btnJobReport.Visible = True
            Me.txtLotNumber.Visible = True
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()

        End If


        Me.txtBoxCartReport.Visible = False
        Me.btnCancelReport.Visible = False
        Me.btnJobReport.Visible = True
        Me.txtLotNumber.Visible = True
        Me.txtLotNumber.Clear()
        Me.txtLotNumber.Focus()
        Me.txtLotNumber.Refresh()

        LRecordCount = 0



    End Sub

    Private Sub btnCancelReport_Click(sender As Object, e As EventArgs) Handles btnCancelReport.Click

        cartReport = 0
        Me.txtBoxCartReport.Visible = False
        Me.btnCancelReport.Visible = False
        Me.btnJobReport.Visible = True
        Me.txtBoxCartReport.Clear()
        Me.txtLotNumber.Visible = True
        Me.txtLotNumber.Clear()
        Me.txtLotNumber.Focus()




    End Sub

    Private Sub btnExChangeCone_Click(sender As Object, e As EventArgs) Handles btnExChangeCone.Click

        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First")
        Else
            changeCone = 1
            Me.Hide()
            frmExChangeCone.Show()
        End If

    End Sub

    Private Sub btnSearchCone_Click(sender As Object, e As EventArgs) Handles btnSearchCone.Click
        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First")
        Else
            Me.Hide()
            frmConeSearch.Show()
        End If
    End Sub


    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then
            If cartReport = 1 Then
                cartReportSub()

            Else

                prgContinue()
            End If
        End If
    End Sub



    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        frmPackReports.Show()
    End Sub

    Private Sub btnDefRep_Click(sender As Object, e As EventArgs) Handles btnDefRep.Click
        DGVDefReport.Show()
    End Sub


End Class