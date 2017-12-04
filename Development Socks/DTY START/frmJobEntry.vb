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
    Public cheeseBcode As String
    Public packGrade As String
    Dim machineName As String = ""
    Dim machineCode As String
    Dim productCode As String
    Dim year As String
    Dim month As String
    Dim doffingNum As String
    Dim cartNum As String
    Dim quit As Integer
    Dim pilotentry As Integer = 0
    Dim pilotCount As Integer = 0
    Public stdcheck As Integer = 0
    Public reCheck As Integer = 0
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


        'NEW PACKING MENU ITEMS

        If My.Settings.chkUsePack Then ToolsToolStripMenuItem.Visible = True Else ToolsToolStripMenuItem.Visible = False
        If My.Settings.chkUsePack Then PackingGradeToolStripMenuItem.Visible = True Else PackingGradeToolStripMenuItem.Visible = False
        If My.Settings.chkUsePack Then ReportsToolStripMenuItem.Visible = True Else ReportsToolStripMenuItem.Visible = False
        If My.Settings.chkUsePack Then btnSearchCone.Visible = False Else btnSearchCone.Visible = True


        If My.Settings.chkUsePack Or My.Settings.chkUseSort Then lblSelectGrade.Visible = True Else lblSelectGrade.Visible = False
        If My.Settings.chkUsePack Or My.Settings.chkUseSort Then lblGrade.Visible = True Else lblGrade.Visible = False
        If My.Settings.chkUsePack Or My.Settings.chkUseSort Then txtGrade.Visible = True Else txtGrade.Visible = False

        If My.Settings.chkUseSort Or My.Settings.chkUsePack Then PrintFormsToolStripMenuItem.Visible = True Else PrintFormsToolStripMenuItem.Visible = False

        If My.Settings.chkUseSort Then ReCheckToolStripMenuItem1.Visible = True

        'If My.Settings.chkUsePack Then btnExChangeCone.Visible = True Else btnExChangeCone.Visible = False
        'If My.Settings.chkUsePack Then btnSearchCone.Visible = True Else btnSearchCone.Visible = False
        'If My.Settings.chkUsePack Then btnReports.Visible = True Else btnReports.Visible = False

        If My.Settings.chkUseSort = False And My.Settings.chkUseColour = False And My.Settings.chkUsePack = False Then
            MsgBox("Please edit SETTINGS for type of User")
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
        End If

        'Me.KeyPreview = True  'Allows us to look for advance character from barcode
        Me.KeyPreview = True
        'Set Form Header text
        If My.Settings.chkUseSort Then
            Me.Text = "Job Entry Sorting"
            txtOperator.Visible = False
        End If
        If My.Settings.chkUseColour Then
            Me.Text = "Job Entry Colour"
            txtOperator.Visible = True
        End If

        If My.Settings.chkUsePack Then
            Me.Text = "Job Entry Packing"
            lblSelectGrade.Visible = True
            txtOperator.Visible = False
        End If


        If My.Settings.debugSet Then frmDGV.Show()

        Me.btnCancelReport.Visible = False



    End Sub

    Public Sub txtOperator_TextChanged(sender As Object, e As EventArgs) Handles txtOperator.TextChanged



        If My.Settings.chkUseSort Then
            SortOP = txtOperator.Text
        ElseIf My.Settings.chkUseColour Then
            ColorOP = txtOperator.Text
        ElseIf My.Settings.chkUsePack Then
            PackOp = txtOperator.Text
        End If


        If My.Settings.chkUsePack = False Or stdcheck = 0 Then
            lblScanType.Text = "Scan Job Sheet"
            txtLotNumber.Visible = True
        Else
            lblScanType.Text = "Scan First Cheese on Cart"
            txtLotNumber.Visible = True
        End If

        If stdcheck Then lblScanType.Text = "Scan First Cheese on Cart"



        varUserName = txtOperator.Text

    End Sub

    'Private Sub txtTraceNum_TextChanged(sender As Object, e As EventArgs) Handles txtTraceNum.TextChanged
    '    txtLotNumber.Visible = True
    '    'varUserName = txtOperator.Text

    '    'Me.KeyPreview = True  'Allows us to look for advance character from barcode
    '    'txtLotNumber.Focus()
    '    If txtTraceNum.TextLength = 10 Then
    '        txtLotNumber.Visible = True
    '        Me.txtLotNumber.Focus()
    '        Me.KeyPreview = True
    '    End If

    'End Sub



    'Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
    Private Sub prgContinue()


        Dim chkBCode As String
        Dim chkBCode2 As String
        'Routine to check Barcode is TRUE

        'Check to see if PILOT Cheese, if it is force operatoer to select correct packing grade.
        If My.Settings.chkUsePack = True And txtLotNumber.Text.Substring(0, 2) = "29" Then
            If txtGrade.Text.Substring(0, 1) = "" Or txtGrade.Text.Substring(0, 5) <> "Pilot" Then
                MsgBox("This is a PILOT Machine job Please select correct" & vbCrLf & "Packing grade from Menu and Try Again")
                txtLotNumber.Clear()
                txtLotNumber.Focus()
                Exit Sub
            End If
        End If


            Try

            chkBCode = txtLotNumber.Text.Substring(9, 1)
                chkBCode2 = txtLotNumber.Text.Substring(9, 3)



            If chkBCode2 = "R11" Or chkBCode2 = "R12" Or chkBCode2 = "R21" Or chkBCode2 = "R31" Or chkBCode2 = "STD" Then  ' we must check this way first otherwise we will always get R and use recheck
                reCheck = 0
                stdcheck = 1
                dbBarcode = txtLotNumber.Text
            ElseIf chkBCode = "R" Then
                stdcheck = 0
                reCheck = 1
                dbBarcode = txtLotNumber.Text
                MsgBox(txtLotNumber.Text.Substring(12, 1))
            ElseIf txtLotNumber.Text.Substring(12, 1) = "B" Then
                chkBCode = txtLotNumber.Text.Substring(12, 1)
                '
                ' If chkBCode = "B" Then
                stdcheck = 0
                    reCheck = 0
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

        If reCheck = 0 And stdcheck = 0 Then
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
            ElseIf machineCode = 29 Then
                machineName = "Pilot"
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

            If machineCode = 29 Then   'CHECK FOR PILOT MACHINE
                cartSelect = 1
                varSpNums = "001 - 032"
                varCartNameA = "B1"
                varCartNameB = "B2"
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
        End If





        If reCheck Or stdcheck Then
            dbBarcode = txtLotNumber.Text
            productCode = txtLotNumber.Text.Substring(0, 3)
            year = txtLotNumber.Text.Substring(3, 2)
            month = txtLotNumber.Text.Substring(5, 2)
            varJobNum = txtLotNumber.Text
            reCheckJob()
        Else
            If My.Settings.chkUseColour Or My.Settings.chkUseSort Then CheckJob()
        End If

        'Select Packing Routine
        If My.Settings.chkUsePack Then APacking()



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

            If My.Settings.chkUseSort And machineCode = 29 Then
                PilCount()
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


    Private Sub PilCount()


        If machineCode = 29 And My.Settings.chkUseSort Then   'CHECK FOR PILOT MACHINE
            cartSelect = 1
            varSpNums = "001 - 032"
            varCartNameA = "B1"
            varCartNameB = "B2"
            Label2.Visible = True
            txtPilotCount.Visible = True
            txtPilotCount.Focus()
            pilotentry = 1  'Flag to keep in loop for count entry
        End If


    End Sub

    Public Sub reCheckJob()

        If stdcheck Then
            Select Case txtLotNumber.Text.Substring(9, 3)
                Case "R11", "R12"
                    LExecQuery("SELECT * FROM jobs WHERE RECHECKBARCODE = '" & dbBarcode & "' And STDSTATE = 2")
                Case "R21"
                    LExecQuery("SELECT * FROM jobs WHERE RECHECKBARCODE = '" & dbBarcode & "' And STDSTATE = 4")
                Case "R31"
                    LExecQuery("SELECT * FROM jobs WHERE RECHECKBARCODE = '" & dbBarcode & "' And STDSTATE = 6")
            End Select
        ElseIf My.Settings.chkUseSort And txtGrade.Text = "ReCheck" Then
            LExecQuery("SELECT * FROM jobs WHERE RECHECKBARCODE = '" & dbBarcode & "' And STDSTATE = 10")

        Else
            LExecQuery("SELECT * FROM jobs WHERE BCODECONE = '" & dbBarcode & "' and (M30 > 0 Or P30 > 0) ")
        End If



        If LRecordCount > 0 Then


            Dim result = MessageBox.Show("Edit Job Yes Or No", "JOB ALREADY EXISTS", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If result = DialogResult.Yes Then

                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)


                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns("RECHKIDX"), ListSortDirection.Ascending)  'sorts On ReCheck index Number

                If My.Settings.debugSet Then frmDGV.Show()
                varProductName = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value.ToString
                coneValUpdate = 1
                If My.Settings.chkUseSort Then
                    frmSortReCheck.Show()
                ElseIf My.Settings.chkUseColour Then

                    If stdcheck Then frmSTDColChk.Show() Else frmColReCheck.Show()
                ElseIf My.Settings.chkUsePack Then
                    nonAPacking()
                End If


                'If My.Settings.debugSet Then frmDGV.Show()

                Me.Hide()
                Exit Sub
            End If

            If result = DialogResult.No Then
                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Focus()

            End If
        Else

            MsgBox("Job does not Exist")
            txtLotNumber.Clear()
            txtLotNumber.Focus()
            Exit Sub
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
            Case Is = 1  'Little routine to only create the correct number of cheese entries for Pilot
                If machineCode = 29 Then
                    coneNumStart = 1
                    coneNumStop = pilotCount
                Else
                    coneNumStart = 1
                    coneNumStop = 32
                End If
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

            If x <= 16 Then cartName= varCartNameA Else cartName= varCartNameB  'SETS CORRECT CART NUMBER

            x = x + 1
            modConeNum = i.ToString(fmt)   ' FORMATS THE CONE NUMBER TO 3 DIGITS
            coneBarcode= modLotStr & modConeNum   'CREATE THE CONE BARCODE NUMBER
            JobBarcode = modLotStr


            LExecQuery("INSERT INTO jobs (MCNUM, PRNUM, PRYY, PRMM, DOFFNUM, CONENUM, MERGENUM, OPNAME,CONESTATE," _
               & "SHORTCONE, MISSCONE, DEFCONE, CARTNUM, CARTNAME, CONEZERO, CONEBARLEY, M10, P10, M30, P30, M50, P50, CARTSTARTTM," _
              & "BCODECART, BCODECONE,FLT_K, FLT_D, FLT_F, FLT_O, FLT_T, FLT_P, FLT_S, FLT_X, FLT_N, FLT_W, FLT_H, FLT_TR, FLT_B, FLT_C," _
               & "MCNAME, PRODNAME, BCODEJOB,OPPACKSORT,OPPACK,OPSORT,PSORTERROR,WEIGHTERROR,WEIGHT,CARTONNUM,SORTERROR,COLOURERROR,DYEFLECK," _
               & "COLDEF, COLWASTE, FLT_DO, FLT_DH, FLT_CL, FLT_FI, FLT_YN, FLT_HT, FLT_LT, CONEAD, CONEAL) " _
              & "VALUES ('" & varMachineCode & "', '" & varProductCode & "','" & varYear & "','" & varMonth & "','" & varDoffingNum & "','" & modConeNum & "'," _
              & "'" & mergeNum & "',  ' ', ' 0', ' 0', ' 0', ' 0', '" & varCartSelect & "','" & cartName & "', ' 0', ' 0', ' 0', ' 0', ' 0', ' 0', ' 0', ' 0','" & today & "','" & dbBarcode & "','" & coneBarcode & "'," _
             & "' False', ' False', ' False', ' False', ' False', ' False', ' False', ' False', ' False', ' False', ' False', ' False', ' False', ' False', '" & varMachineName & "','" & varProductName & "', '" & JobBarcode & "'," _
             & "' 0',' 0',' 0',' 0',' 0',' 0',' 0',' 0',' 0',' 0',' 0',' 0',' False',' False',' False',' False',' False',' False',' False',' 0',' 0')")


        Next

        LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & dbBarcode & "'")

        Me.Cursor = System.Windows.Forms.Cursors.Default
        If LRecordCount > 1 Then
            If machineCode = 29 Then
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)
                LDA.UpdateCommand = New SqlCommandBuilder(LDA).GetUpdateCommand
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(6), ListSortDirection.Ascending)  ' sorts On cone number
                frmCart1.Show()

                If My.Settings.debugSet Then frmDGV.Show()
                Me.Hide()
                Exit Sub
            Else
                Exit Sub
            End If
        Else
            MsgBox("Records Not created")
        End If



    End Sub

    Private Sub APacking()


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

    Private Sub STDCreate()
        'Check Barcode is a valid Chees number, it must be 15 characters and no "B" in it
        Dim chkBCode As String

        Try

            chkBCode = txtLotNumber.Text.Substring(12, 1)

            If chkBCode = "B" Then

                Label3.Visible = True
                Label3.Text = "This is not a Valid Cheese Number"
                DelayTM()
                Label3.Visible = False
                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Focus()
                Me.txtLotNumber.Refresh()
                Exit Sub
            Else


                cheeseBcode = txtLotNumber.Text
            End If

        Catch ex As Exception

            Label3.Visible = True
            Label3.Text = "BarCcode Is Not Valid"
            DelayTM()
            Label3.Visible = False

            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            Exit Sub
        End Try





        LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' ")
        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True



            If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("STDCHEESE").Value) Then  'check to see if cheese scanned has already been allocated
                Label3.Visible = True
                Label3.Text = "THIS CHEESE IS NOT A 'STD' CHEESE "
                DelayTM()
                Label3.Visible = False
                quit = 1
                frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV
                quit = 1

                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Visible = True
                Me.txtLotNumber.Focus()
                Exit Sub
            ElseIf frmDGV.DGVdata.Rows(0).Cells("STDSTATE").Value > 7 Then
                Label3.Visible = True
                Label3.Text = "THIS CHEESE IS ALREADY SET FOR RECHECK"
                DelayTM()
                Label3.Visible = False
                quit = 1
                frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV
                quit = 1

                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Visible = True
                Me.txtLotNumber.Focus()
                Exit Sub
            Else
                Select Case txtGrade.Text
                    Case "Round1"
                        If frmDGV.DGVdata.Rows(0).Cells("STDSTATE").Value <> 1 Then
                            Label3.Visible = True
                            Label3.Text = "THIS CHEESE IS CANNOT BE USED"
                            DelayTM()
                            Label3.Visible = False
                            quit = 1
                            frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV
                            quit = 1

                            Me.txtLotNumber.Clear()
                            Me.txtLotNumber.Visible = True
                            Me.txtLotNumber.Focus()
                            Exit Sub
                        End If
                    Case "Round2"
                        If frmDGV.DGVdata.Rows(0).Cells("STDSTATE").Value <> 3 Then
                            Label3.Visible = True
                            Label3.Text = "THIS CHEESE IS CANNOT BE USED"
                            DelayTM()
                            Label3.Visible = False
                            quit = 1
                            frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV
                            quit = 1

                            Me.txtLotNumber.Clear()
                            Me.txtLotNumber.Visible = True
                            Me.txtLotNumber.Focus()
                            Exit Sub
                        End If
                    Case "Round3"
                        If frmDGV.DGVdata.Rows(0).Cells("STDSTATE").Value <> 5 Then
                            Label3.Visible = True
                            Label3.Text = "THIS CHEESE IS CANNOT BE USED"
                            DelayTM()
                            Label3.Visible = False
                            quit = 1
                            frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV
                            quit = 1

                            Me.txtLotNumber.Clear()
                            Me.txtLotNumber.Visible = True
                            Me.txtLotNumber.Focus()
                            Exit Sub
                        End If
                    Case "STD"
                        If frmDGV.DGVdata.Rows(0).Cells("STDSTATE").Value <> 7 Then
                            Label3.Visible = True
                            Label3.Text = "THIS CHEESE IS CANNOT BE USED"
                            DelayTM()
                            Label3.Visible = False
                            quit = 1
                            frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV
                            quit = 1

                            Me.txtLotNumber.Clear()
                            Me.txtLotNumber.Visible = True
                            Me.txtLotNumber.Focus()
                            Exit Sub
                        End If
                End Select
            End If
        End If

        'Extract requierd Informatiom
        varProductCode = txtLotNumber.Text.Substring(2, 3)
        year = txtLotNumber.Text.Substring(5, 2)
        month = txtLotNumber.Text.Substring(7, 2)
        doffingNum = txtLotNumber.Text.Substring(9, 3)
        machineCode = txtLotNumber.Text.Substring(0, 2)



        Select Case machineCode
            Case 21
                varMachineName = "11D1"        'Left Side
            Case 22
                varMachineName = "11D2"        'Right Side
            Case 23
                varMachineName = "12D1"        'Left Side
            Case 24
                varMachineName = "12D2"        'Right Side
            Case 25
                varMachineName = "21D1"        'Left Side
            Case 26
                varMachineName = "21D2"        'Right Side
            Case 27
                varMachineName = "22D1"        'Left Side
            Case 28
                varMachineName = "22D2"        'Right Side
            Case 29
                varMachineName = "Pilot"
        End Select




        'GET PRODUCT WEIGHT INFORMATION
        LExecQuery("SELECT PRODNAME,PRODWEIGHT,WEIGHTCODE FROM PRODUCT WHERE PRNUM = '" & varProductCode & "'")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True

            varProductName = frmDGV.DGVdata.Rows(0).Cells(0).Value.ToString
            varProdWeight = frmDGV.DGVdata.Rows(0).Cells(1).Value.ToString
            varweightcode = frmDGV.DGVdata.Rows(0).Cells(2).Value.ToString


            frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV

        Else
            Label3.Visible = True
            Label3.Text = "PRODUCT NUMBER " & varProductCode & " THIS PRODUCT IS NOT IN THE PRODUCT LIST"
            DelayTM()
            Label3.Visible = False
            quit = 1
            Exit Sub

        End If



        Select Case txtGrade.Text
            Case "Round1"
                LExecQuery("Select * FROM Jobs Where Stdstate  = 1 And  PRNUM = '" & varProductCode & "' And PRYY = '" & year & "' And PRMM = '" & month & "' AND DOFFNUM = '" & doffingNum & "'")
            Case "Round2"
                LExecQuery("Select * FROM Jobs Where Stdstate  = 3 And  PRNUM = '" & varProductCode & "' And PRYY = '" & year & "' And PRMM = '" & month & "' AND DOFFNUM = '" & doffingNum & "'")
            Case "Round3"
                LExecQuery("Select * FROM Jobs Where Stdstate  = 5 And  PRNUM = '" & varProductCode & "' And PRYY = '" & year & "' And PRMM = '" & month & "' AND DOFFNUM = '" & doffingNum & "'")
            Case "STD"
                LExecQuery("Select * FROM Jobs Where Stdstate  = 7 And  PRNUM = '" & varProductCode & "' And PRYY = '" & year & "' And PRMM = '" & month & "' AND DOFFNUM = '" & doffingNum & "'")


        End Select







        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True
            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)

            IsDBNull(frmDGV.DGVdata.Rows(0).Cells("BCODECONE").Value)

            'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
            frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns("BCODECONE"), ListSortDirection.Ascending)  'sorts On cone number





            Else
                Label3.Visible = True
            Label3.Text = "NO GRADE " & "'" & txtGrade.Text & "'" & " CHEESES CAN BE FOUND"
            DelayTM()
            Label3.Visible = False
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Visible = True
            quit = 1
            Exit Sub

        End If



        Me.Hide()
        If My.Settings.debugSet Then frmDGV.Show()


        frmStdCreate.txtConeBcode.Clear()
        frmStdCreate.txtConeBcode.Focus()
        frmStdCreate.Show()

        'frmB_AL_AD_W.txtConeBcode.Clear()
        '    frmB_AL_AD_W.txtConeBcode.Focus()
        '    frmB_AL_AD_W.Show()


    End Sub

    Private Sub nonAPacking()

        'Check Barcode is a valid Chees number, it must be 15 characters and no "B" in it
        Dim chkBCode As String

        Try

            chkBCode = txtLotNumber.Text.Substring(12, 1)

            If chkBCode = "B" Then

                Label3.Visible = True
                Label3.Text = "This is not a Valid Cheese Number"
                DelayTM()
                Label3.Visible = False
                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Focus()
                Me.txtLotNumber.Refresh()
                Exit Sub
            Else


                cheeseBcode = txtLotNumber.Text
            End If
        Catch ex As Exception

            Label3.Visible = True
            Label3.Text = "BarCcode Is Not Valid"
            DelayTM()
            Label3.Visible = False

            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            Exit Sub
        End Try

        'CHECK SCANNED CHEESE IS CORREECT GRADE OTHERWISE RESCAN

        Select Case txtGrade.Text
            Case "A"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where RECHECKBARCODE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And RECHK = 4 And DEFCONE = 0 And CONEBARLEY = 0 And (CONEAL = 'AL' OR RECHKRESULT = 'A') And PACKENDTM is Null")
            Case "B"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 8  And (DEFCONE > 0 OR CONEBARLEY > 0 ) And FLT_W = 'False' And PACKENDTM is Null ")
            Case "AL"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 8 And DEFCONE = 0 And CONEBARLEY = 0 And CONEAL = 'AL' And RECHK = 4 And PACKENDTM is Null")
            Case "AD"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 8 And DEFCONE = 0 And CONEBARLEY = 0 And CONEAD = 'AD' And RECHK = 4 And PACKENDTM is Null")
            Case "P15 AS", "P25 AS", "P35 AS"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'True' And CONESTATE = 9 And DEFCONE = 0 And CONEBARLEY = 0  And FLT_W = 'False' And PACKENDTM is Null")

            Case "P20 BS", "P30 BS", "P35 BS"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'True' And CONESTATE = 8 And (DEFCONE > 0 Or CONEBARLEY > 0) And PACKENDTM is Null  ")
            Case "ReCheck"

                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 8 And DEFCONE = 0 And CONEBARLEY = 0 And M30 > 0 And PACKENDTM is Null Or BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 8 And DEFCONE = 0 And CONEBARLEY = 0 And P30 > 0 And PACKENDTM is Null")
                If LRecordCount > 0 Then
                    'LOAD THE DATA FROM dB IN TO THE DATAGRID
                    frmDGV.DGVdata.DataSource = LDS.Tables(0)
                    frmDGV.DGVdata.Rows(0).Selected = True



                    If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("RECHK").Value) Then  'check to see if cheese scanned has already been allocated
                        Label3.Visible = True
                        Label3.Text = "THIS CHEESE HAS ALREADY BEEN ALLOCATED "
                        DelayTM()
                        Label3.Visible = False
                        quit = 1
                        frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV
                        quit = 1

                        Me.txtLotNumber.Clear()
                        Me.txtLotNumber.Visible = True
                        Me.txtLotNumber.Focus()
                        Exit Sub

                    End If
                End If


            Case "Waste"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 8 And FLT_W = 'True' And PACKENDTM is Null Or BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 8  And COLWASTE > 0 And PACKENDTM is Null ")
        End Select

        If LRecordCount = 0 Then

            Label3.Visible = True
            Label3.Text = "This is NOT Grade " & "'" & txtGrade.Text & "'" & " CHEESES PLEASE RE-SCAN"
            DelayTM()
            Label3.Visible = False
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Visible = True
            Me.txtLotNumber.Focus()
            quit = 1
            Exit Sub

        End If



        'Extract requierd Informatiom
        varProductCode = txtLotNumber.Text.Substring(2, 3)
        year = txtLotNumber.Text.Substring(5, 2)
        month = txtLotNumber.Text.Substring(7, 2)

        'GET PRODUCT WEIGHT INFORMATION
        LExecQuery("SELECT PRODNAME,PRODWEIGHT,WEIGHTCODE FROM PRODUCT WHERE PRNUM = '" & varProductCode & "'")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True

            varProductName = frmDGV.DGVdata.Rows(0).Cells(0).Value.ToString
            varProdWeight = frmDGV.DGVdata.Rows(0).Cells(1).Value.ToString
            varweightcode = frmDGV.DGVdata.Rows(0).Cells(2).Value.ToString


            frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV

        Else
            Label3.Visible = True
            Label3.Text = "PRODUCT NUMBER " & varProductCode & " THIS PRODUCT IS NOT IN THE PRODUCT LIST"
            DelayTM()
            Label3.Visible = False
            quit = 1
            Exit Sub

        End If



        'Check for correct cheese selection
        Select Case txtGrade.Text

            Case "A"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where RECHECKBARCODE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And RECHK = 4 And DEFCONE = 0 And CONEBARLEY = 0 And (CONEAL = 'AL' OR RECHKRESULT = 'A') And PACKENDTM is Null")
            Case "B"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And SHORTCONE = 0 And CONESTATE = 8  And DEFCONE > 0 And FLT_W = 'False' And PACKENDTM is Null Or PRNUM = '" & varProductCode & "' And FLT_S = 'False' And SHORTCONE = 0 And CONESTATE = 8 And CONEBARLEY > 0 And PACKENDTM is Null")
            Case "AL"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And SHORTCONE = 0 And CONESTATE = 8 And DEFCONE = 0 And CONEBARLEY = 0 And (CONEAL = 'AL' OR RECHKRESULT = 'A')  And RECHK = 4 And PACKENDTM is Null")
            Case "AD"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And SHORTCONE = 0 And CONESTATE = 8 And DEFCONE = 0 And CONEBARLEY = 0 And CONEAD = 'AD' And RECHK = 4 And PACKENDTM is Null")
            Case "P15 AS", "P25 AS", "P35 AS"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'True' And CONESTATE = 9 And DEFCONE = 0 And CONEBARLEY = 0 And  PACKENDTM is Null")
            Case "P20 BS", "P30 BS", "P35 BS"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'True'  And CONESTATE = 8 And DEFCONE > 0  And PACKENDTM is Null Or PRNUM = '" & varProductCode & "' And FLT_S = 'True' And CONESTATE = 8 And CONEBARLEY > 0 And PACKENDTM is Null  Or PRNUM = '" & varProductCode & "' And FLT_S = 'True' And CONESTATE = 8 And M30 > 0 And PACKENDTM is Null Or PRNUM = '" & varProductCode & "' And FLT_S = 'True' And CONESTATE = 8 And P30 > 0 And PACKENDTM is Null ")
            Case "ReCheck"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And SHORTCONE = 0 And CONESTATE = 8 And DEFCONE = 0 And CONEBARLEY = 0 And M30 > 0 And PACKENDTM is Null Or PRNUM = '" & varProductCode & "' And FLT_S = 'False' And SHORTCONE = 0 And CONESTATE = 8 And DEFCONE = 0 And CONEBARLEY = 0 And P30 > 0 And PACKENDTM is Null")
            Case "Waste"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And SHORTCONE = 0 And CONESTATE = 8 And FLT_W = 'True' And PACKENDTM is Null Or PRNUM = '" & varProductCode & "' And FLT_S = 'False' And SHORTCONE = 0 And CONESTATE = 8  And COLWASTE > 0 And PACKENDTM is Null ")
        End Select


        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True
            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)

            If txtGrade.Text = "A" Then
                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE by our own index
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns("RECHKIDX"), ListSortDirection.Ascending)  'sorts On cone number
            Else
                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(0), ListSortDirection.Ascending)  'sorts On cone number

            End If




        Else
            Label3.Visible = True
            Label3.Text = "NO GRADE " & "'" & txtGrade.Text & "'" & " CHEESES CAN BE FOUND"
            DelayTM()
            Label3.Visible = False
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Visible = False
            quit = 1
            Exit Sub

        End If



        Me.Hide()
        If My.Settings.debugSet Then frmDGV.Show()

        If txtGrade.Text = "A" Then
            frmPackRchkA.txtConeBcode.Clear()
            frmPackRchkA.txtConeBcode.Focus()
            frmPackRchkA.Show()
        Else
            frmB_AL_AD_W.txtConeBcode.Clear()
            frmB_AL_AD_W.txtConeBcode.Focus()
            frmB_AL_AD_W.Show()
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


    Private Sub btnSettings_Click_1(sender As Object, e As EventArgs)
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
        'Me.KeyPreview = True  'Allows us to look for advance character from barcode
        cartReport = 1
        Me.KeyPreview = True

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


            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmPrintCartReport.DGVcartReport.DataSource = LDS.Tables(0)
                frmPrintCartReport.DGVcartReport.Rows(0).Selected = True

                'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
                frmPrintCartReport.DGVcartReport.Sort(frmPrintCartReport.DGVcartReport.Columns(6), ListSortDirection.Ascending)  'sorts On cone number
                'frmPrintCartReport.Show()
                Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
                frmPrintCartReport.prtCartSheet()
                Me.Cursor = System.Windows.Forms.Cursors.Default


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

    Private Sub btnExChangeCone_Click(sender As Object, e As EventArgs)

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

        If pilotentry = 0 Then
            If e.KeyCode = Keys.Return And txtLotNumber.Visible = True Or e.KeyCode = Keys.Return And txtBoxCartReport.Visible Then

                If cartReport = 1 Then
                    cartReportSub()
                ElseIf My.Settings.chkUseSort And stdcheck = 0 Or My.Settings.chkUseColour Then
                    prgContinue()
                ElseIf My.Settings.chkUsePack And txtGrade.Text.Substring(0, 1) = "A" Or My.Settings.chkUsePack And txtGrade.Text.Substring(0, 1) = "P" Then
                    prgContinue()
                ElseIf My.Settings.chkUseSort And stdcheck Then
                    STDCreate()
                ElseIf My.Settings.chkUsePack And Not txtGrade.Text = "A" And Not txtLotNumber.Text = "" Then
                        nonAPacking()

                End If
            End If
        Else
            If e.KeyCode = Keys.Return And pilotentry = 1 Then

                Dim result = MessageBox.Show("IS NUMBER CORRECT CONTINUE ", "YES or NO", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                If result = DialogResult.Yes Then
                    'cone count = number entered and clear info from screen and continue
                    varSpNums = "1 - " & txtPilotCount.Text
                    pilotCount = txtPilotCount.Text
                    pilotentry = 0
                    Label2.Visible = False
                    txtPilotCount.Visible = False
                    CreatNewJob()
                End If

                If result = DialogResult.No Then
                    Me.txtPilotCount.Clear()
                    Me.txtPilotCount.Focus()
                End If
            End If
        End If

    End Sub



    Private Sub btnReports_Click(sender As Object, e As EventArgs)
        frmPackReports.Show()
    End Sub

    Private Sub btnDefRep_Click(sender As Object, e As EventArgs) Handles btnDefRep.Click
        DGVDefReport.Show()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        frmPassword.Show()
    End Sub



    Private Sub ReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem.Click

    End Sub

    Private Sub EndOfDayReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EndOfDayReportToolStripMenuItem.Click
        frmEODReport.Show()
    End Sub

    Private Sub StockToProcessReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockToProcessReportToolStripMenuItem.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        lblMessage.Text = "Please wait Creating Work in Process Report"
        frmProdStockWork.processReport()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        lblMessage.Text = ""

    End Sub

    Private Sub DailyPackingReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DailyPackingReportToolStripMenuItem.Click
        frmDailyPackProduction.Show()
    End Sub

    Private Sub ExChangeCheeseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExChangeCheeseToolStripMenuItem.Click

        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First")
        Else
            changeCone = 1
            Me.Hide()
            frmExChangeCone.Show()
        End If

    End Sub

    Private Sub FindCheeseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindCheeseToolStripMenuItem.Click
        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First")
        Else
            Me.Hide()
            frmConeSearch.Show()
        End If
    End Sub

    Private Sub AGradeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AGradeToolStripMenuItem.Click
        txtGrade.Text = AGradeToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan Job Sheet"
    End Sub

    Private Sub P15ASToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P15ASToolStripMenuItem.Click
        txtGrade.Text = P15ASToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P25ASToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P25ASToolStripMenuItem.Click
        txtGrade.Text = P25ASToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P35ASToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P35ASToolStripMenuItem.Click
        txtGrade.Text = P35ASToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub



    Private Sub WasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WasteToolStripMenuItem.Click
        txtGrade.Text = WasteToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub BToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BToolStripMenuItem.Click
        txtGrade.Text = BToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub ALToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ALToolStripMenuItem.Click
        txtGrade.Text = ALToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub ADToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ADToolStripMenuItem.Click
        txtGrade.Text = ADToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P20BSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P20BSToolStripMenuItem.Click
        txtGrade.Text = P20BSToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P30BSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P30BSToolStripMenuItem.Click
        txtGrade.Text = P30BSToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P35BSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P35BSToolStripMenuItem.Click
        txtGrade.Text = P35BSToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub ReCheckToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReCheckToolStripMenuItem.Click
        txtGrade.Text = ReCheckToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub



    Private Sub Round1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Round1ToolStripMenuItem.Click
        txtGrade.Text = Round1ToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
        stdcheck = 1
    End Sub

    Private Sub Round2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Round2ToolStripMenuItem.Click
        txtGrade.Text = Round2ToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
        stdcheck = 1
    End Sub

    Private Sub Round3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Round3ToolStripMenuItem.Click
        txtGrade.Text = Round3ToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
        stdcheck = 1
    End Sub

    Private Sub StdSheetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StdSheetToolStripMenuItem.Click
        txtGrade.Text = StdSheetToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan First Cheese on Cart"
        stdcheck = 1
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        txtGrade.Text = ToolStripMenuItem1.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan Job Sheet"
    End Sub

    Private Sub Pilot6ChToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Pilot6ChToolStripMenuItem.Click
        txtGrade.Text = Pilot6ChToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan Job Sheet"
    End Sub

    Private Sub Pilot15ChToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Pilot15ChToolStripMenuItem.Click
        txtGrade.Text = Pilot15ChToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan Job Sheet"
    End Sub

    Private Sub Pilot20ChToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Pilot20ChToolStripMenuItem.Click
        txtGrade.Text = Pilot20ChToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        lblScanType.Text = "Scan Job Sheet"
    End Sub


End Class