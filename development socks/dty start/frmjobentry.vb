'Imports System.Data.DataTable
Imports System.Data.SqlClient
Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering



Public Class frmJobEntry
    'THIS CREATS LOCAL INSTANCE TO REFRENCE THE SQL CLASS FORM, NOT USED WHEN WORKING WITH DATAGRIDVIEW
    'Private SQL As New SQLConn

    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError

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
    Public year As String
    Public month As String
    Dim doffingNum As String
    Dim cartNum As String
    Dim quit As Integer
    Dim pilotentry As Integer = 0
    Dim pilotCount As Integer = 0
    Public stdcheck As Integer = 0
    Public reCheck As Integer = 0
    Public cartReport As Integer

    Public rechkA As Integer
    Public stdReChk As Integer = 0
    Public SortOP As String
    Public PackOp As String
    Public ColorOP As String
    Public PackSortOP As String
    Public changeCone As Integer
    Public time As DateTime = DateTime.Now
    Public Format As String = "dd mm yyyy  HH:mm"

    Dim fltconeNum As String
    Dim csvRowNum As String
    Dim fileActive As Integer
    Public varCartStartTime As String   'Record time that we started measuring
    Public varCartEndTime As String
    Dim coneBarley As String = 0
    Dim coneZero As String = 0
    Dim coneM10 As String = 0
    Dim coneP10 As String = 0
    Dim coneM30 As String = 0
    Dim coneP30 As String = 0
    Dim coneM50 As String = 0
    Dim coneP50 As String = 0



    Private Sub frmJobEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        Me.txtLotNumber.Visible = False

        If My.Settings.chkUseColour Then btnCartReport.Visible = True Else btnCartReport.Visible = False


        'NEW PACKING MENU ITEMS

        If My.Settings.chkUsePack Then ToolsToolStripMenuItem.Visible = True Else ToolsToolStripMenuItem.Visible = False
        If My.Settings.chkUsePack Then PackingGradeToolStripMenuItem.Visible = True Else PackingGradeToolStripMenuItem.Visible = False
        If My.Settings.chkUsePack Then ReportsToolStripMenuItem.Visible = True Else ReportsToolStripMenuItem.Visible = False
        If My.Settings.chkUsePack Then btnSearchCone.Visible = False Else btnSearchCone.Visible = True


        If My.Settings.chkUsePack Or My.Settings.chkUseSort Then lblSelectGrade.Visible = True Else lblSelectGrade.Visible = False
        If My.Settings.chkUsePack Or My.Settings.chkUseSort Then lblGrade.Visible = True Else lblGrade.Visible = False
        If My.Settings.chkUsePack Or My.Settings.chkUseSort Then txtGrade.Visible = True Else txtGrade.Visible = False

        If My.Settings.chkUseSort Or My.Settings.chkUsePack Then PrintFormsToolStripMenuItem.Visible = True Else PrintFormsToolStripMenuItem.Visible = False

        If My.Settings.chkUseSort Or My.Settings.chkUsePack Then ReCheckToolStripMenuItem1.Visible = True



        If My.Settings.chkUseSort = False And My.Settings.chkUseColour = False And My.Settings.chkUsePack = False Then
            MsgBox("Please edit SETTINGS for type of User" & vbCrLf & " กรุณาแก้ไข setting เพื่อกำหนด User")
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

        ' audioAlarms.Start()


    End Sub

    Public Sub txtOperator_TextChanged(sender As Object, e As EventArgs) Handles txtOperator.TextChanged



        If My.Settings.chkUseSort Then
            SortOP = txtOperator.Text
        ElseIf My.Settings.chkUseColour Then
            ColorOP = txtOperator.Text
        ElseIf My.Settings.chkUsePack Then
            PackOp = txtOperator.Text
        End If



        'If stdcheck Or txtGrade.Text = "ReCheck" Then lblScanType.Text = "Scan First Cheese on Cart"
        If My.Settings.chkUseColour Then txtGrade.Text = "Normal"  'Fix grade value for colour check

        'New section to display correct text for scan type
        Select Case txtGrade.Text
            Case "A", "Normal", "Pilot 6Ch", "Pilot 15Ch", "Pilot 20Ch", "ReCheckA"
                lblScanType.Text = "Scan Job Sheet"
                txtLotNumber.Visible = True

            Case "P15 AS", "P25 AS", "P35 AS"
                lblScanType.Text = "Scan First  Cheese on Cart"
                txtLotNumber.Visible = True
            Case "P20 BS", "P30 BS", "P35 BS"
                lblScanType.Text = "Scan First  Cheese on Cart"
                txtLotNumber.Visible = True
            Case "B", "AL", "AD"
                lblScanType.Text = "Scan First Cheese On Cart"
                txtLotNumber.Visible = True
            Case "ReCheck"
                lblScanType.Text = "Scan First Cheese On Cart"
                txtLotNumber.Visible = True
            Case "Round1", "Round2", "Round3", "STD"
                lblScanType.Text = "Scan First Cheese On Cart"
                txtLotNumber.Visible = True
        End Select





        varUserName = txtOperator.Text

    End Sub



    Private Sub prgContinue()



        Dim chkBCode As String
        Dim chkBCode2 As String


        'Routine to check Barcode is TRUE
        'Check to see if PILOT Cheese, if it is force operatoer to select correct packing grade.

        If txtLotNumber.Text = "" Then
            MsgBox("Please scan Barcode")
            txtLotNumber.Clear()
            txtLotNumber.Focus()
            Exit Sub

        End If

        If My.Settings.chkUsePack And txtLotNumber.Text.Substring(0, 2) = "29" Then
            Select Case txtGrade.Text
                Case "Pilot 6Ch", "Pilot 15Ch", "Pilot 20Ch"

                Case Else
                    MsgBox("This Is a PILOT Machine job Please Select correct" & vbCrLf & "Packing grade from Menu And Try Again" & vbCrLf & " หมายเลขนี้เป็นงานไพล็อต กรุณาเลือกการแพ็คจากเมนูให้ถูกต้อง ")
                    txtLotNumber.Clear()
                    txtLotNumber.Focus()
                    Exit Sub
            End Select
        End If



        Try

            chkBCode = txtLotNumber.Text.Substring(9, 1)
            chkBCode2 = txtLotNumber.Text.Substring(9, 3)


            'CHECK TO SEE IT STANDARD RECHECK OR RECHECK CART
            If chkBCode2 = "R11" Or chkBCode2 = "R12" Or chkBCode2 = "R21" Or chkBCode2 = "R31" Or chkBCode2 = "STD" Then  ' we must check this way first otherwise we will always get R and use recheck
                reCheck = 0
                stdcheck = 1
                dbBarcode = txtLotNumber.Text
            ElseIf chkBCode = "R" Then
                stdcheck = 0
                reCheck = 1
                dbBarcode = txtLotNumber.Text

            ElseIf txtLotNumber.Text.Substring(12, 1) = "B" Then
                chkBCode = txtLotNumber.Text.Substring(12, 1)


                stdcheck = 0
                reCheck = 0
                machineCode = txtLotNumber.Text.Substring(0, 2)

                Select Case txtLotNumber.TextLength
                    Case 14
                        If txtLotNumber.Text.Substring(13, 1) >= 1 And txtLotNumber.Text.Substring(13, 1) <= 9 Then
                            cartNum = txtLotNumber.Text.Substring(12, 2)
                        Else
                            MsgBox("This Is Not a CART Barcode Please RE Scan" & vbCrLf & " หมายเลขนี้ไม่ใช่ บาร์โค็ดของรถ กรุณาสแกนใหม่อีกครั้ง")
                            Me.txtLotNumber.Clear()
                            Me.txtLotNumber.Focus()
                            Me.txtLotNumber.Refresh()
                            Exit Sub
                        End If
                    Case 15
                        If txtLotNumber.Text.Substring(13, 2) = "10" Or txtLotNumber.Text.Substring(13, 2) = "11" Or txtLotNumber.Text.Substring(13, 2) = "12" Then
                            If machineCode >= 30 Then  'check that carts B10, B11 and B12 are not used on machines 30,31,32,33
                                MsgBox("This CART No. " + txtLotNumber.Text.Substring(13, 2) + " Is Not valid for this machine Please check Barcode" & vbCrLf & " หมายเลขนี้ไม่ใช่ บาร์โค็ดของรถ กรุณาสแกนใหม่อีกครั้ง")
                                Me.txtLotNumber.Clear()
                                Me.txtLotNumber.Focus()
                                Me.txtLotNumber.Refresh()
                                Exit Sub
                            End If
                            cartNum = txtLotNumber.Text.Substring(12, 3)
                        Else
                            MsgBox("This Is Not a CART Barcode Please RE Scan" & vbCrLf & " หมายเลขนี้ไม่ใช่ บาร์โค็ดของรถ กรุณาสแกนใหม่อีกครั้ง")
                            Me.txtLotNumber.Clear()
                            Me.txtLotNumber.Focus()
                            Me.txtLotNumber.Refresh()
                            Exit Sub
                        End If
                    Case > 15
                        MsgBox("This Is Not a CART Barcode Please RE Scan" & vbCrLf & " หมายเลขนี้ไม่ใช่ บาร์โค็ดของรถ กรุณาสแกนใหม่อีกครั้ง")
                        Me.txtLotNumber.Clear()
                        Me.txtLotNumber.Focus()
                        Me.txtLotNumber.Refresh()
                        Exit Sub
                End Select
            Else
                MsgBox("This Is Not a CART Barcode Please RE Scan" & vbCrLf & " หมายเลขนี้ไม่ใช่ บาร์โค็ดของรถ กรุณาสแกนใหม่อีกครั้ง")
                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Focus()
                Me.txtLotNumber.Refresh()
                Exit Sub
            End If

        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("Barcode Error", ex.Message, False, "User Fault")
            writeerrorLog.writelog("Barcode Error", ex.ToString, False, "User Fault")


            MsgBox("BarCcode Is Not Valid" & vbCrLf & " บาร์โค็ดไม่ถูกต้อง")
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            Exit Sub
        End Try

        CreateJob()


    End Sub

    Private Sub CreateJob()

        'For A packing on normal cart
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

            Select Case machineCode
                Case 21
                    machineName = "11D1"        'Left Side
                Case 22
                    machineName = "11D2"        'Right Side
                Case 23
                    machineName = "12D1"        'Left Side
                Case 24
                    machineName = "12D2"        'Right Side
                Case 25
                    machineName = "21D1"        'Left Side
                Case 26
                    machineName = "21D2"        'Right Side
                Case 27
                    machineName = "22D1"        'Left Side
                Case 28
                    machineName = "22D2"        'Right Side
                Case 29
                    machineName = "Pilot"
                Case 30
                    machineName = "31D1"        'Left Side
                Case 31
                    machineName = "31D2"        'Left Side
                Case 32
                    machineName = "32D1"        'Left Side
                Case 33
                    machineName = "32D2"        'Left Side
            End Select




            Select Case machineCode
                'Machine Left side
                Case 21, 23, 25, 27

                    Select Case cartNum
                        Case "B1", "B2"
                            varCartNameA = "B1"
                            varCartNameB = "B2"
                            cartSelect = 1
                            varSpNums = "001 - 032"


                        Case "B3", "B4"
                            varCartNameA = "B3"
                            varCartNameB = "B4"
                            cartSelect = 2
                            varSpNums = "033 - 064"


                        Case "B5", "B6"
                            varCartNameA = "B5"
                            varCartNameB = "B6"
                            cartSelect = 3
                            varSpNums = "065 - 096"


                        Case "B7", "B8"
                            varCartNameA = "B7"
                            varCartNameB = "B8"
                            cartSelect = 4
                            varSpNums = "097 - 128"


                        Case "B9", "B10"
                            varCartNameA = "B9"
                            varCartNameB = "B10"
                            cartSelect = 5
                            varSpNums = "129 - 160"


                        Case "B11", "B12"
                            varCartNameA = "B11"
                            varCartNameB = "B12"
                            cartSelect = 6
                            varSpNums = "161 - 192"


                    End Select

                Case 29
                    cartSelect = 1
                    varSpNums = "001 - 032"
                    varCartNameA = "B1"
                    varCartNameB = "B2"



                'Machine Right side
                Case 22, 24, 26, 28
                    Select Case cartNum

                        Case "B1", "B2"
                            varCartNameA = "B1"
                            varCartNameB = "B2"
                            cartSelect = 7
                            varSpNums = "193 - 224"


                        Case "B3", "B4"
                            varCartNameA = "B3"
                            varCartNameB = "B4"
                            cartSelect = 8
                            varSpNums = "225 - 256"


                        Case "B5", "B6"
                            varCartNameA = "B5"
                            varCartNameB = "B6"
                            cartSelect = 9
                            varSpNums = "257 - 288"


                        Case "B7", "B8"
                            varCartNameA = "B7"
                            varCartNameB = "B8"
                            cartSelect = 10
                            varSpNums = "289 - 320"


                        Case "B9", "B10"
                            varCartNameA = "B9"
                            varCartNameB = "B10"
                            cartSelect = 11
                            varSpNums = "321 - 352"


                        Case "B11", "B12"
                            varCartNameA = "B11"
                            varCartNameB = "B12"
                            cartSelect = 12
                            varSpNums = "353 - 384"


                    End Select

                    'Changes for new machines
                Case 30, 32

                    Select Case cartNum
                        Case "B1", "B2"
                            varCartNameA = "B1"
                            varCartNameB = "B2"
                            cartSelect = 1
                            varSpNums = "001 - 032"

                        Case "B3", "B4"
                            varCartNameA = "B3"
                            varCartNameB = "B4"
                            cartSelect = 2
                            varSpNums = "033 - 064"

                        Case "B5", "B6"
                            varCartNameA = "B5"
                            varCartNameB = "B6"
                            cartSelect = 3

                            varSpNums = "065 - 096"

                        Case "B7", "B8"
                            varCartNameA = "B7"
                            varCartNameB = "B8"
                            cartSelect = 4
                            varSpNums = "097 - 128"

                        Case "B9", "B10"
                            varCartNameA = "B9"
                            varCartNameB = "B10"
                            cartSelect = 5
                            varSpNums = "129 - 144"

                    End Select



                'Machine Right side
                Case 31, 33
                    Select Case cartNum

                        Case "B1", "B2"
                            varCartNameA = "B1"
                            varCartNameB = "B2"
                            cartSelect = 6
                            varSpNums = "145 - 176"


                        Case "B3", "B4"
                            varCartNameA = "B3"
                            varCartNameB = "B4"
                            cartSelect = 7
                            varSpNums = "177 - 208"


                        Case "B5", "B6"
                            varCartNameA = "B5"
                            varCartNameB = "B6"
                            cartSelect = 8
                            varSpNums = "209 - 240"

                        Case "B7", "B8"
                            varCartNameA = "B7"
                            varCartNameB = "B8"
                            cartSelect = 9
                            varSpNums = "241 - 272"

                        Case "B9", "B10"
                            varCartNameA = "B9"
                            varCartNameB = "B10"
                            cartSelect = 10
                            varSpNums = "273 - 288"

                    End Select





            End Select







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
            Exit Sub
        Else
            If My.Settings.chkUseColour Or My.Settings.chkUseSort Then CheckJob()
        End If

        'THIS Selects "A" Packing Routine
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
            'Write error to Log File
            writeerrorLog.writelog("ExecQuery Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("ExecQuery Error", ex.ToString, False, "System Fault")

            LException = "ExecQuery Error:  " & vbNewLine & ex.Message
            MsgBox(LException)

        End Try

    End Sub



    Public Sub CheckJob()


        LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & dbBarcode & "' ORDER BY CONENUM")

        If LRecordCount > 0 Then



            Dim result = MessageBox.Show("Edit Job Yes Or No", "JOB ALREADY EXISTS", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If result = DialogResult.Yes Then

                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)
                LDA.UpdateCommand = New SqlCommandBuilder(LDA).GetUpdateCommand






                coneValUpdate = 1

                frmCart1.Show()


                Me.Hide()
                Exit Sub
            End If

            If result = DialogResult.No Then
                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Focus()

            End If
        Else
            If My.Settings.chkUseColour Or My.Settings.chkUsePack Then
                MsgBox("Job does not Exist, you must create new Job " & vbCrLf & " ไม่พบงานที่ทำ กรุณาสร้างงานใหม่")
                txtLotNumber.Clear()
                txtLotNumber.Focus()
                Exit Sub
            End If

            If My.Settings.chkUseSort And machineCode = 29 Then
                PilCount()
                Exit Sub
            End If

            If My.Settings.chkDisableCreate Then
                MsgBox("Job does not Exist, It must be created on 2nd Floor " & vbCrLf & " ไม่พบงานที่ทำ งานต้องสร้างมาจากชั้นที่ 2 ")
                txtLotNumber.Clear()
                txtLotNumber.Focus()
                Exit Sub
            Else
                CreatNewJob()
            End If


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

            'If My.Settings.debugSet Then frmDGV.Show()

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
                    LExecQuery("SELECT * FROM jobs WHERE RECHECKBARCODE = '" & dbBarcode & "' And STDSTATE = 2  ORDER BY RECHKIDX   ")
                Case "R21"
                    LExecQuery("SELECT * FROM jobs WHERE RECHECKBARCODE = '" & dbBarcode & "' And STDSTATE = 4  ORDER BY RECHKIDX")
                Case "R31"
                    LExecQuery("SELECT * FROM jobs WHERE RECHECKBARCODE = '" & dbBarcode & "' And STDSTATE = 6  ORDER BY RECHKIDX")
            End Select
        ElseIf My.Settings.chkUseSort And txtGrade.Text = "ReCheck" Then
            LExecQuery("SELECT * FROM jobs WHERE RECHECKBARCODE = '" & dbBarcode & "' And STDSTATE = 10 ORDER BY RECHKIDX")
        Else
            LExecQuery("SELECT * FROM jobs WHERE RECHECKBARCODE = '" & dbBarcode & "' ORDER BY RECHKIDX ")
        End If




        If LRecordCount > 0 Then

            If reCheck = 1 And txtGrade.Text = "A" And My.Settings.chkUsePack Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)


                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE

                If My.Settings.debugSet Then frmDGV.Show()
                varProductName = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value.ToString
                coneValUpdate = 1
                nonAPacking()
                Exit Sub
            End If

            Dim result = MessageBox.Show("Edit Job Yes Or No", "JOB ALREADY EXISTS", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If result = DialogResult.Yes Then

                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)


                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE

                If My.Settings.debugSet Then frmDGV.Show()
                varProductName = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value.ToString
                coneValUpdate = 1
                If My.Settings.chkUseSort Then
                    frmSortReCheck.Show()
                ElseIf My.Settings.chkUseColour Then

                    If stdcheck Then frmSTDColChk.Show() Else frmColReCheck.Show()
                    'ElseIf My.Settings.chkUsePack Then
                    '    nonAPacking()
                End If




                Me.Hide()
                Exit Sub
            End If

            If result = DialogResult.No Then
                Me.txtLotNumber.Clear()
                Me.txtLotNumber.Focus()

            End If
        Else

            MsgBox("Job does not Exist" & vbCrLf & " ไม่พบงานที่ทำ")
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

            Case 1
                If machineCode = 29 Then
                    coneNumStart = 1
                    coneNumStop = pilotCount

                ElseIf machineCode = 30 Or machineCode = 32 Then
                    coneNumStart = 1
                    coneNumStop = 32
                Else
                    coneNumStart = 1
                    coneNumStop = 32
                End If
            Case 2
                If machineCode = 30 Or machineCode = 32 Then
                    coneNumStart = 33
                    coneNumStop = 64
                Else
                    coneNumStart = 33
                    coneNumStop = 64
                End If

            Case 3
                If machineCode = 30 Or machineCode = 32 Then
                    coneNumStart = 65
                    coneNumStop = 96
                Else
                    coneNumStart = 65
                    coneNumStop = 96
                End If

            Case 4
                If machineCode = 30 Or machineCode = 32 Then
                    coneNumStart = 97
                    coneNumStop = 128
                Else
                    coneNumStart = 97
                    coneNumStop = 128
                End If
            Case 5
                If machineCode = 30 Or machineCode = 32 Then
                    coneNumStart = 129
                    coneNumStop = 144
                Else
                    coneNumStart = 129
                    coneNumStop = 160
                End If

            Case 6
                If machineCode = 31 Or machineCode = 33 Then
                    coneNumStart = 145
                    coneNumStop = 176
                Else
                    coneNumStart = 161
                    coneNumStop = 192
                End If
            Case 7
                If machineCode = 31 Or machineCode = 33 Then
                    coneNumStart = 177
                    coneNumStop = 208
                Else
                    coneNumStart = 193
                    coneNumStop = 224
                End If
            Case 8
                If machineCode = 31 Or machineCode = 33 Then
                    coneNumStart = 209
                    coneNumStop = 240
                Else
                    coneNumStart = 225
                    coneNumStop = 256
                End If
            Case 9
                If machineCode = 31 Or machineCode = 33 Then
                    coneNumStart = 241
                    coneNumStop = 272
                Else
                    coneNumStart = 257
                    coneNumStop = 288
                End If
            Case 10
                If machineCode = 31 Or machineCode = 33 Then
                    coneNumStart = 273
                    coneNumStop = 288
                Else
                    coneNumStart = 289
                    coneNumStop = 320
                End If
            Case 11

                coneNumStart = 321
                coneNumStop = 352

            Case 12

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
        Dim today As String = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")


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
            MsgBox("PRODUCT NUMBER " & varProductCode & " VALUE DOES NOT EXIST" & vbCrLf & " หมายเลขโปรดักส์นี้ไม่มมีในรายการโปรดักส์")
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            quit = 1
            Exit Sub

        End If

        Try

            For i As Integer = coneNumStart To coneNumStop

                If x <= 16 Then cartName = varCartNameA Else cartName = varCartNameB  'SETS CORRECT CART NUMBER

                x = x + 1
                modConeNum = i.ToString(fmt)   ' FORMATS THE CONE NUMBER TO 3 DIGITS
                coneBarcode = modLotStr & modConeNum   'CREATE THE CONE BARCODE NUMBER
                JobBarcode = modLotStr

                JobBarcode = modLotStr

                'Parameters List for full db

                'ADD ORA PARAMETERS & RUN THE COMMAND
                LAddParam("@mcnum", varMachineCode)
                LAddParam("@prodnum", varProductCode)
                LAddParam("@yy", varYear)
                LAddParam("@mm", varMonth)
                LAddParam("@doff", varDoffingNum)
                LAddParam("@cone", modConeNum)
                LAddParam("@merge", mergeNum)
                LAddParam("@user", "")
                LAddParam("@conestate", "0")
                LAddParam("@shortcone", "0")
                LAddParam("@nocone", "0")
                LAddParam("@defectcone", "0")
                LAddParam("@cartnum", varCartSelect)
                LAddParam("@cartname", cartName)
                LAddParam("@passzero", "0")
                LAddParam("@barley", "0")
                LAddParam("@m10", "0")
                LAddParam("@p10", "0")
                LAddParam("@m30", "0")
                LAddParam("@p30", "0")
                LAddParam("@m50", "0")
                LAddParam("@p50", "0")
                LAddParam("@cartstart", today)
                LAddParam("@barcart", dbBarcode)
                LAddParam("@barcone", coneBarcode)
                LAddParam("@fk", "False")
                LAddParam("@fd", "False")
                LAddParam("@ff", "False")
                LAddParam("@fo", "False")
                LAddParam("@ft", "False")
                LAddParam("@fp", "False")
                LAddParam("@fs", "False")
                LAddParam("@fx", "False")
                LAddParam("@fn", "False")
                LAddParam("@fw", "False")
                LAddParam("@fh", "False")
                LAddParam("@ftr", "False")
                LAddParam("@fb", "False")
                LAddParam("@fc", "False")
                LAddParam("@mcname", varMachineName)
                LAddParam("@prodname", varProductName)
                LAddParam("@barjob", JobBarcode)
                LAddParam("@packsortop", "0")
                LAddParam("@packop", "0")
                LAddParam("@sortop", "0")
                LAddParam("@colourop", "0")
                LAddParam("@errpsort", "0")
                LAddParam("@errweigh", "0")
                LAddParam("@weight", "0")
                LAddParam("@boxnum", "0")
                LAddParam("@errsort", "0")
                LAddParam("@errcol", "0")
                LAddParam("@errdyefleck", "0")
                LAddParam("@coldef", "0")
                LAddParam("@colwaste", "0")
                LAddParam("@fdo", "False")
                LAddParam("@fdh", "False")
                LAddParam("@fcl", "False")
                LAddParam("@ffi", "False")
                LAddParam("@fyn", "False")
                LAddParam("@fht", "False")
                LAddParam("@flt", "False")
                LAddParam("@conead", "0")
                LAddParam("@coneal", "0")
                'NEWPARAMS ADDED FOR @ND FLOOR DATA ENTRY
                LAddParam("@opcreate", txtOperator.Text)


                LExecQuery("INSERT INTO Jobs (MCNUM, PRNUM, PRYY, PRMM, DOFFNUM, CONENUM, MERGENUM, OPNAME,CONESTATE," _
                       & "SHORTCONE, MISSCONE, DEFCONE, CARTNUM, CARTNAME, CONEZERO, CONEBARLEY, M10, P10, M30, P30, M50, P50, CARTSTARTTM," _
                      & "BCODECART, BCODECONE,FLT_K, FLT_D, FLT_F, FLT_O, FLT_T, FLT_P, FLT_S, FLT_X, FLT_N, FLT_W, FLT_H, FLT_TR, FLT_B, FLT_C," _
                       & "MCNAME, PRODNAME, BCODEJOB,OPPACKSORT,OPPACK,OPSORT,PSORTERROR,WEIGHTERROR,WEIGHT,CARTONNUM,SORTERROR,COLOURERROR,DYEFLECK," _
                       & "COLDEF, COLWASTE, FLT_DO, FLT_DH, FLT_CL, FLT_FI, FLT_YN, FLT_HT, FLT_LT, CONEAD, CONEAL, OPCREATECART) " _
                        & "VALUES (@mcnum, @prodnum,@yy,@mm,@doff,@cone,@merge,@user,@conestate,@shortcone,@nocone,@defectcone,@cartnum,@cartname,@passzero," _
                        & "@barley,@m10,@p10,@m30,@p30,@m50,@p50,@cartstart," _
                        & "@barcart,@barcone,@fk,@fd,@ff,@fo,@ft,@fp,@fs,@fx,@fn,@fw,@fh,@ftr,@fb,@fc,@mcname,@prodname," _
                        & "@barjob,@packsortop,@packop,@sortop,@colourop,@errpsort,@errweigh,@boxnum,@errsort,@errcol,@errdyefleck,@coldef,@colwaste,@fdo,@fdh," _
                        & "@fcl,@ffi,@fyn,@fht,@flt,@conead,@coneal,@opcreate) ")



            Next

        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("Job Create Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Job Create Error", ex.ToString, False, "System Fault")
        End Try



        LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & dbBarcode & "' ORDER BY CONENUM")

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
            MsgBox("PRODUCT NUMBER " & varProductCode & " THIS PRODUCT IS NOT IN THE PRODUCT LIST" & vbCrLf & " หมายเลขโปรดักส์นี้ไม่มมีในรายการโปรดักส์")
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            quit = 1
            Exit Sub

        End If

        Try
            LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & dbBarcode & "' AND CONESTATE = '9' and FLT_S = 'False' ")

            If LRecordCount > 0 Then
                LExecQuery("Select * FROM jobs WHERE bcodecart = '" & dbBarcode & "' ORDER BY CONENUM")

                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)



                If LConn.State = ConnectionState.Open Then LConn.Close()
                frmDGV.DGVdata.ClearSelection()
                frmDGV.DGVdata.DataSource = Nothing  'used to clear DGV
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
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("Scan Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Scan Error", ex.ToString, False, "System Fault")
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
        End Try

    End Sub







    Private Sub STDCreate()
        'Check Barcode is a valid Chees number, it must be 15 characters and no "B" in it
        Dim chkBCode As String

        Try

            chkBCode = txtLotNumber.Text.Substring(12, 1)

            If chkBCode = "B" Then

                Label3.Visible = True
                Label3.Text = "This is not a Valid Cheese Number" & vbCrLf & " ไม่พบหมายเลข cheese นี้ "
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

            'Write error to Log File
            writeerrorLog.writelog("Barcode Error", ex.Message, False, "User Fault")
            writeerrorLog.writelog("Barcode Error", ex.ToString, False, "User Fault")

            Label3.Visible = True
            Label3.Text = "BarCcode Is Not Valid" * vbCrLf & " ไม่พบหมายเลข บาร์โค็ด นี้"
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
                            Label3.Text = "THIS CHEESE CANNOT BE USED" & vbCrLf & " ใม่สามารถใช้ cheese นี้ได้"
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
                            Label3.Text = "THIS CHEESE CANNOT BE USED" & vbCrLf & " ใม่สามารถใช้ cheese นี้ได้"
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
                            Label3.Text = "THIS CHEESE CANNOT BE USED" & vbCrLf & " ใม่สามารถใช้ cheese นี้ได้"
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
                            Label3.Text = "THIS CHEESE CANNOT BE USED" & vbCrLf & " ใม่สามารถใช้ cheese นี้ได้"
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
            Case 30
                varMachineName = "31D1"        'Left Side 1 - 144
            Case 31
                varMachineName = "31D2"        'Right Side  145 - 288
            Case 32
                varMachineName = "32D1"        'Left Side  1 - 144
            Case 33
                varMachineName = "32D2"        'Right Side  145 - 288
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
            Label3.Text = "PRODUCT NUMBER " & varProductCode & " THIS PRODUCT IS NOT IN THE PRODUCT LIST" & vbCrLf &
                "หมายเลขโปรดักส์ “ & varProductCode & ” นี้ ไม่พบอยู่ในรายการสินค้า"
            DelayTM()
            Label3.Visible = False
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            quit = 1
            Exit Sub

        End If



        Select Case txtGrade.Text
            Case "Round1"
                LExecQuery("Select * FROM Jobs Where Stdstate  = 1 And  PRNUM = '" & varProductCode & "' And PRYY = '" & year & "' And PRMM = '" & month & "' ORDER BY CONENUM ")
            Case "Round2"
                LExecQuery("Select * FROM Jobs Where Stdstate  = 3 And  PRNUM = '" & varProductCode & "' And PRYY = '" & year & "' And PRMM = '" & month & "' ORDER BY CONENUM")
            Case "Round3"
                LExecQuery("Select * FROM Jobs Where Stdstate  = 5 And  PRNUM = '" & varProductCode & "' And PRYY = '" & year & "' And PRMM = '" & month & "'ORDER BY CONENUM ")
            Case "STD"
                LExecQuery("Select * FROM Jobs Where Stdstate  = 7 And  PRNUM = '" & varProductCode & "' And PRYY = '" & year & "' And PRMM = '" & month & "'ORDER BY CONENUM ")


        End Select







        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True
            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)


        Else
            Label3.Visible = True
            Label3.Text = "NO GRADE " & "'" & txtGrade.Text & "'" & " CHEESES CAN BE FOUND" & vbCrLf &
                "ไม่มีค่าสี “ & txtGrade.Text & ” พบ cheese ลูกนี้แล้ว"
            DelayTM()
            Label3.Visible = False
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Visible = True
            quit = 1
            Exit Sub

        End If



        Me.Hide()
        If My.Settings.debugSet Then frmDGV.Show()

        varCartNum = 1

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

            'Write error to Log File
            writeerrorLog.writelog("Barcode Error", ex.Message, False, "User Fault")
            writeerrorLog.writelog("Barcode Error", ex.ToString, False, "User Fault")

            Label3.Visible = True
            Label3.Text = "Barcode Is Not Valid" & vbCrLf & "ไม่พบหมายเลข cheese นี้"
            DelayTM()
            Label3.Visible = False

            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            Exit Sub
        End Try

        'CHECK SCANNED CHEESE IS CORREECT GRADE OTHERWISE RESCAN

        Select Case txtGrade.Text
            Case "A"  'Case "ReCheckA"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where RECHECKBARCODE = '" & txtLotNumber.Text & "'  And RECHK = 4 And  PACKENDTM is Null And RECHKRESULT = 'A' ")
            Case "B"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE BETWEEN 8 And 14  And (DEFCONE > 0 OR CONEBARLEY > 0 Or RECHKRESULT = 'B') And FLT_W = 'False' And PACKENDTM is Null ")
            Case "AL"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE BETWEEN 8 And 9 And DEFCONE = 0 And CONEBARLEY = 0 And RECHKRESULT = 'AL' And RECHK = 4 And PACKENDTM is Null")
            Case "AD"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE BETWEEN 8 And 9 And DEFCONE = 0 And CONEBARLEY = 0 And RECHKRESULT = 'AD' And RECHK = 4 And PACKENDTM is Null")
            Case "P15 AS", "P25 AS", "P35 AS"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'True' And CONESTATE = 9 And DEFCONE = 0 And CONEBARLEY = 0  And FLT_W = 'False' And PACKENDTM is Null")

            Case "P20 BS", "P30 BS", "P35 BS"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'True' And CONESTATE = 8 And (DEFCONE > 0 Or CONEBARLEY > 0 Or m30 > 0 Or P30 >0) And PACKENDTM is Null  ")
            Case "ReCheck"

                packGrade = txtGrade.Text
                If stdReChk = 0 Then

                    LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE BETWEEN 8 And 14  And DEFCONE = 0 And CONEBARLEY = 0 And (M30 > 0 Or P30 > 0) And PACKENDTM is Null And RECHKSTARTTM Is Null  ")

                Else

                    LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 9 And DEFCONE = 0 And CONEBARLEY = 0 And (M30 > 0 Or P30 > 0) And PACKENDTM is Null And STDSTATE = 10 ")
                End If


                If LRecordCount > 0 Then
                    'LOAD THE DATA FROM dB IN TO THE DATAGRID
                    frmDGV.DGVdata.DataSource = LDS.Tables(0)
                    frmDGV.DGVdata.Rows(0).Selected = True



                    If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("RECHK").Value) Then  'check to see if cheese scanned has already been allocated
                        If frmDGV.DGVdata.Rows(0).Cells("RECHK").Value > "0" Then
                            Label3.Visible = True
                            Label3.Text = "THIS CHEESE HAS ALREADY BEEN ALLOCATED " & vbCrLf & " Cheese ลูกนี้ได้ถูกนำไปใช้แล้ว"
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
                End If


            Case "Waste"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 8 And FLT_W = 'True' And PACKENDTM is Null Or BCODECONE = '" & txtLotNumber.Text & "' And FLT_S = 'False' And CONESTATE = 8  And COLWASTE > 0 And PACKENDTM is Null ")
        End Select

        If LRecordCount = 0 Then
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Visible = True
            Me.txtLotNumber.Focus()
            Label3.Visible = True
            Label3.Text = "NO Grade " & "'" & txtGrade.Text & "'" & " CHEESES PLEASE RE-SCAN" & vbCrLf & "ไม่มีค่าสี “ & txtGrade.Text & ” กรุณาสแกน cheese ลูกนี้อีกครั้ง"
            DelayTM()
            Label3.Visible = False

            quit = 1
            Exit Sub

        End If


        If txtGrade.Text = "A" Then  'txtGrade.Text = "ReCheckA" Then
            'Extract requierd Informatiom
            varProductCode = txtLotNumber.Text.Substring(0, 3)
            year = txtLotNumber.Text.Substring(3, 2)
            month = txtLotNumber.Text.Substring(5, 2)
        Else
            'Extract requierd Informatiom
            varProductCode = txtLotNumber.Text.Substring(2, 3)
            year = txtLotNumber.Text.Substring(5, 2)
            month = txtLotNumber.Text.Substring(7, 2)

        End If




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
            Label3.Text = "PRODUCT NUMBER " & varProductCode & " THIS " & vbCrLf & "PRODUCT Is Not In THE PRODUCT LIST" & vbCrLf &
                "หมายเลขโปรดักส์ “ & varProductCode & ” นี้  ไม่พบอยู่ในรายการสินค้า"
            DelayTM()
            Label3.Visible = False
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Visible = True
            Me.txtLotNumber.Focus()
            quit = 1
            Exit Sub

        End If



        'Check for correct cheese selection
        Select Case txtGrade.Text

            Case "A"   '"ReCheckA"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where RECHECKBARCODE = '" & txtLotNumber.Text & "' And  RECHK = 4 And RECHKRESULT = 'A' ORDER BY RECHKIDX ")
            Case "B"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And CONESTATE BETWEEN 8 And 9  And (DEFCONE > 0 OR CONEBARLEY > 0 Or RECHKRESULT = 'B') And FLT_W = 'False' And PACKENDTM is Null  ")
            Case "AL"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And CONESTATE BETWEEN 8 And 9 And DEFCONE = 0 And CONEBARLEY = 0 And RECHKRESULT = 'AL' And RECHK = 4 And PACKENDTM is Null")
            Case "AD"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And CONESTATE BETWEEN 8 And 9 And DEFCONE = 0 And CONEBARLEY = 0 And RECHKRESULT = 'AD' And RECHK = 4 And PACKENDTM is Null")
            Case "P15 AS", "P25 AS", "P35 AS"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'True' And CONESTATE = 9 And DEFCONE = 0 And CONEBARLEY = 0 And  PACKENDTM is Null")
            Case "P20 BS", "P30 BS", "P35 BS"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'True'  And CONESTATE = 8 And (DEFCONE > 0 Or CONEBARLEY > 0 Or M30 > 0 Or P30 > 0) And PACKENDTM is Null  ")
            Case "ReCheck"  'CREATE RECHECK SHEET
                packGrade = txtGrade.Text
                If stdReChk = 0 Then
                    LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And (CONESTATE = 8 Or  CONESTATE = 14) And DEFCONE = 0 And CONEBARLEY = 0 And (M30 > 0 Or P30 > 0) And PACKENDTM is Null And RECHKSTARTTM is Null And RECHK is Null ")
                Else
                    'ReCheck creation for std cheese  state 10
                    LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "' And FLT_S = 'False' And CONESTATE = 9 And DEFCONE = 0 And CONEBARLEY = 0 And (M30 > 0 Or P30 > 0) And PACKENDTM is Null And STDSTATE = 10 and RECHK is Null")

                End If


            Case "Waste"
                packGrade = txtGrade.Text
                LExecQuery("Select * FROM Jobs Where PRNUM = '" & varProductCode & "'  (CONESTATE = 8 Or CONESTATE = 14) And (FLT_W = 'True' Or COLWASTE > 0) And PACKENDTM is Null ")
        End Select



        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True
            Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(LDA)

            If txtGrade.Text = "A" Then  '  If txtGrade.Text = "ReCheckA" Then
                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE by our own index
                ' frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns("RECHKIDX"), ListSortDirection.Ascending)  'sorts On cone number

            Else
                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(0), ListSortDirection.Ascending)  'sorts On cone number

            End If

            If txtGrade.Text = "A" Then  ' If txtGrade.Text = "ReCheckA" Then
                rechkA = 1
                coneValUpdate = 1
                varCartSelect = 1
                Me.Hide()
                frmPackRchkA.Show()

            Else
                frmB_AL_AD_W.txtConeBcode.Clear()
                frmB_AL_AD_W.txtConeBcode.Focus()
                frmB_AL_AD_W.Show()
            End If


        Else
            Label3.Visible = True
            Label3.Text = "NO GRADE " & "'" & txtGrade.Text & "'" & " CHEESES CAN BE FOUND" & vbCrLf &
                "ไม่มีค่าสี “ & txtGrade.Text & ” กรุณาสแกน cheese ลูกนี้อีกครั้ง"
            DelayTM()
            Label3.Visible = False
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Visible = False
            quit = 1
            Exit Sub

        End If






    End Sub



    Private Sub DelayTM()
        Dim interval As Integer = "5000"  '5sec Delay time
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
        cartReport = 1
        Me.KeyPreview = True

    End Sub

    Private Sub cartReportSub()


        If txtBoxCartReport.Text = "" Then
            MsgBox("Please enter Barcode first" & vbCrLf & " กรุณาป้อนรหัสบาร์โค็ด")
            Me.txtBoxCartReport.Focus()
            Exit Sub
        End If




        Dim modLotStr = txtBoxCartReport.Text.Substring(0, 12)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        LExecQuery("SELECT * FROM JOBS WHERE BCODEJOB = '" & modLotStr & "' ")

        If LRecordCount > 0 Then


            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmPrintCartReport.DGVcartReport.DataSource = LDS.Tables(0)
            frmPrintCartReport.DGVcartReport.Rows(0).Selected = True

            'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
            frmPrintCartReport.DGVcartReport.Sort(frmPrintCartReport.DGVcartReport.Columns("CONENUM"), ListSortDirection.Ascending)  'sorts On cone number
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.Hide()
            frmPrintCartReport.Show()



        Else
            Me.Cursor = System.Windows.Forms.Cursors.Default
            MsgBox("No Job Found, Please check if this Job has been checked" & vbCrLf & " ไม่พบงาน กรุณาตรวจสอบว่างานนี้ได้รับการตรวจเช็คหรือไม่")

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
            MsgBox("Please Enter Operator Name First" & vbCrLf & " กรุณาใส่ชื่อผู้ปฏิบัติงาน")
        Else
            changeCone = 1
            Me.Hide()
            frmExChangeCone.Show()
        End If

    End Sub

    Private Sub btnSearchCone_Click(sender As Object, e As EventArgs) Handles btnSearchCone.Click
        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First" & vbCrLf & " กรุณาใส่ชื่อผู้ปฏิบัติงาน")
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
                ElseIf My.Settings.chkUseSort And (stdcheck = 0 And txtGrade.Text <> "ReCheck") Or My.Settings.chkUseColour Then
                    prgContinue()
                ElseIf (My.Settings.chkUsePack And txtGrade.Text = "A") Or (My.Settings.chkUsePack And (txtGrade.Text = "Pilot 6Ch" Or txtGrade.Text = "Pilot 15Ch" Or txtGrade.Text = "Pilot 20Ch")) Then
                    prgContinue()
                ElseIf My.Settings.chkUseSort And stdcheck Or My.Settings.chkUsePack And stdcheck Then
                    STDCreate()
                Else

                    nonAPacking()

                End If
            End If
        Else
            If e.KeyCode = Keys.Return And pilotentry = 1 Then

                Dim result = MessageBox.Show("IS NUMBER CORRECT CONTINUE ", "YES or NO", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                If result = DialogResult.Yes Then

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

    ' ADD PARAMS
    Public Sub LAddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        LParams.Add(NewParam)
    End Sub




    Private Sub btnDefRep_Click(sender As Object, e As EventArgs) Handles btnDefRep.Click
        DGVDefReport.Show()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        frmPassword.Show()
    End Sub





    Private Sub EndOfDayReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EndOfDayReportToolStripMenuItem.Click
        frmEODReport.Show()
    End Sub


    Private Sub DailyPackingReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DailyPackingReportToolStripMenuItem.Click
        frmDailyPackProduction.Show()
    End Sub

    Private Sub ExChangeCheeseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExChangeCheeseToolStripMenuItem.Click

        If txtOperator.Text = "" Then
            MsgBox("Please Enter Operator Name First" & vbCrLf & " กรุณาใส่ชื่อผู้ปฏิบัติงาน")
        Else
            changeCone = 1
            Me.Hide()
            frmExChangeCone.Show()
        End If

    End Sub

    Private Sub FindCheeseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindCheeseToolStripMenuItem.Click

        Me.Hide()
            frmConeSearch.Show()
        'End If
    End Sub

    'Private Sub AGradeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AGradeToolStripMenuItem.Click
    '    stdReChk = 0
    '    txtGrade.Text = AGradeToolStripMenuItem.Text
    '    lblSelectGrade.Visible = False
    '    txtOperator.Visible = True
    '    txtOperator.Focus()
    '    lblScanType.Text = "Scan Job Sheet"
    'End Sub

    Private Sub P15ASToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P15ASToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = P15ASToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P25ASToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P25ASToolStripMenuItem.Click
        txtGrade.Text = P25ASToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P35ASToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P35ASToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = P35ASToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub



    Private Sub WasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WasteToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = WasteToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub BToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = BToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub ALToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ALToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = ALToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub ADToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ADToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = ADToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P20BSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P20BSToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = P20BSToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P30BSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P30BSToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = P30BSToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    Private Sub P35BSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles P35BSToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = P35BSToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub

    'THIS IS THE PACKING RECHECK CREATE RECHECK FORM OPTION
    Private Sub ReCheckToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReCheckToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = ReCheckToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
    End Sub



    Private Sub Round1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Round1ToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = Round1ToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
        stdcheck = 1
    End Sub

    Private Sub Round2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Round2ToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = Round2ToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
        stdcheck = 1
    End Sub

    Private Sub Round3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Round3ToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = Round3ToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
        stdcheck = 1
    End Sub

    Private Sub StdSheetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StdSheetToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = StdSheetToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"
        stdcheck = 1
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        stdReChk = 0
        txtGrade.Text = ToolStripMenuItem1.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan Job Sheet"
    End Sub

    Private Sub Pilot6ChToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Pilot6ChToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = Pilot6ChToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan Job Sheet"
    End Sub

    Private Sub Pilot15ChToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Pilot15ChToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = Pilot15ChToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan Job Sheet"
    End Sub

    Private Sub Pilot20ChToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Pilot20ChToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = Pilot20ChToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan Job Sheet"
    End Sub

    Private Sub ReCheckToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ReCheckToolStripMenuItem1.Click
        stdReChk = 0  ' changed from 1 for STD recheck to 0 for normal Recheck
        txtGrade.Text = ReCheckToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan First Cheese on Cart"

    End Sub

    'Private Sub ReCheckAToolStripMenuItem4_Click(sender As Object, e As EventArgs)
    '    stdReChk = 0
    '    txtGrade.Text = ReCheckAToolStripMenuItem4.Text
    '    lblSelectGrade.Visible = False
    '    txtOperator.Visible = True
    '    txtOperator.Focus()
    '    lblScanType.Text = "Scan Job Sheet"
    'End Sub

    Private Sub StockToProcessReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockToProcessReportToolStripMenuItem.Click
        frmProdStockWork.Show()

    End Sub

    Private Sub GradeAReCheckAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GradeAReCheckAToolStripMenuItem.Click
        stdReChk = 0
        txtGrade.Text = AGradeToolStripMenuItem.Text
        lblSelectGrade.Visible = False
        txtOperator.Visible = True
        txtOperator.Focus()
        lblScanType.Text = "Scan Job Sheet"
    End Sub
End Class