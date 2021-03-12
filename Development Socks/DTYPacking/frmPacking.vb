
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Text
Imports System.IO

Public Class frmPacking
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

    'THIS CREATS LOCAL INSTANCE OD
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

    'DIRECTORY PATHS ALL PUBLIC
    Public finPath As String
    Dim todayPath As String
    Dim PrevPath1 As String
    Dim PrevPath2 As String
    Dim PrevPath3 As String

    Dim sheetSearch As String
    Dim sheetDate As String
    Dim tmp_sheetdate As Date
    Dim prodNum As String



    'Faults


    Private Sub frmPacking_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If frmJobEntry.HLColSep = Nothing Then
            PExecQuery("Select * FROM jobs WHERE bcodecart = '" & frmJobEntry.dbBarcode & "' Order By CONENUM ;")
        Else
            PExecQuery("Select * FROM jobs WHERE recheckbarcode = '" & frmJobEntry.dbBarcode & "' Order By rechkidx ;")
        End If
        'PExecQuery("Select * FROM jobs WHERE bcodecart = '" & frmJobEntry.dbBarcode & "' Order By CONENUM ;")



        'LOAD THE DATA FROM dB IN TO THE DATAGRID
        DGVPakingA.DataSource = PDS.Tables(0)
        DGVPakingA.Rows(0).Selected = True


        Dim PCB As SqlCommandBuilder = New SqlCommandBuilder(PDA)
        Dim localMCCode As Integer = Convert.ToInt32(frmJobEntry.varMachineCode)



        Console.WriteLine(DGVPakingA.Rows.Count.ToString)

        'SET number of buttons based on machine number
        If localMCCode = 29 Then
            rowendcount = DGVPakingA.Rows.Count
        ElseIf localMCCode > 29 Then  'Sets buttons for new 12P position machines
            rowendcount = DGVPakingA.Rows.Count
        ElseIf localMCCode < 29 Then
            rowendcount = 32
        End If



        'Dim localMCCode = frmJobEntry.varMachineCode
        Dim btnNum As Integer
        Dim btnNums As String

        If frmJobEntry.varMachineCode = "29" Or Not frmJobEntry.HLColSep = Nothing Then
            btnNums = 1
        Else
            btnNums = frmJobEntry.varCartSelect
        End If
        ''btnNums = frmJobEntry.varCartSelect

        ' SELECT CONE NUMBER RANGE BASED ON CART NUMBER
        Select Case btnNums
            Case Is = 1
                If localMCCode = 30 Or localMCCode = 32 Then
                    btnNum = 1
                    coneNumOffset = 0
                Else
                    btnNum = 1
                    coneNumOffset = 0
                End If

            Case Is = 2
                If localMCCode = 30 Or localMCCode = 32 Then
                    btnNum = 33
                    coneNumOffset = 32
                Else
                    btnNum = 33
                    coneNumOffset = 32
                End If

            Case Is = 3
                If localMCCode = 30 Or localMCCode = 32 Then
                    btnNum = 65
                    coneNumOffset = 64
                Else
                    btnNum = 65
                    coneNumOffset = 64
                End If


            Case Is = 4
                If localMCCode = 30 Or localMCCode = 32 Then
                    btnNum = 97
                    coneNumOffset = 96
                Else
                    btnNum = 97
                    coneNumOffset = 96
                End If


            Case Is = 5
                If localMCCode = 30 Or localMCCode = 32 Then
                    btnNum = 129
                    coneNumOffset = 128
                Else
                    btnNum = 129
                    coneNumOffset = 128
                End If


            Case Is = 6
                If localMCCode = 31 Or localMCCode = 33 Then
                    btnNum = 145
                    coneNumOffset = 144
                Else
                    btnNum = 161
                    coneNumOffset = 160
                End If


            Case Is = 7
                If localMCCode = 31 Or localMCCode = 33 Then
                    btnNum = 177
                    coneNumOffset = 176
                Else
                    btnNum = 193
                    coneNumOffset = 192
                End If


            Case Is = 8
                If localMCCode = 31 Or localMCCode = 33 Then
                    btnNum = 209
                    coneNumOffset = 208
                Else
                    btnNum = 225
                    coneNumOffset = 224
                End If


            Case Is = 9
                If localMCCode = 31 Or localMCCode = 33 Then
                    btnNum = 241
                    coneNumOffset = 240
                Else
                    btnNum = 257
                    coneNumOffset = 256
                End If


            Case Is = 10
                If localMCCode = 31 Or localMCCode = 33 Then
                    btnNum = 273
                    coneNumOffset = 272
                Else
                    btnNum = 289
                    coneNumOffset = 288
                End If


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



        'New section to hide unused buttons
        Dim btnEraseStart As Integer = DGVPakingA.Rows.Count + 1
        Dim TotalBtn As Integer = 31 - btnEraseStart

        For i = btnEraseStart To 32
            Me.Controls("btnCone" & i.ToString).Visible = False
        Next




        Me.txtCartNum.Text = frmJobEntry.cartSelect
        Me.lblJobNum.Text = frmJobEntry.varJobNum






        'GET NUMBER OF CONES THAT NEED ALLOCATING Count agains Job Barcode
        If Not frmJobEntry.HHLL = "YES" AndAlso frmJobEntry.HLColSep = Nothing Then  ' only display unload information on screen
            For i = 1 To rowendcount
                If DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = "9" And DGVPakingA.Rows(i - 1).Cells("FLT_S").Value = False And (IsDBNull(DGVPakingA.Rows(i - 1).Cells("STDSTATE").Value)) Then
                    toAllocatedCount = toAllocatedCount + 1
                End If
            Next

        ElseIf frmJobEntry.HHLL = "YES" And frmJobEntry.HLColSep = Nothing Then

            Dim hCount As Integer
            Dim lCount As Integer

            For i = 1 To rowendcount
                If DGVPakingA.Rows(i - 1).Cells("HHLL").Value = "H" Or DGVPakingA.Rows(i - 1).Cells("HHLL").Value = "L" Then

                    If DGVPakingA.Rows(i - 1).Cells("HHLL").Value = "H" Then hCount = hCount + 1
                    If DGVPakingA.Rows(i - 1).Cells("HHLL").Value = "L" Then lCount = lCount + 1


                    toAllocatedCount = hCount + lCount
                End If
            Next
            lblCheese.Text = "Total to Remove"

            lblHGrade.Show()
            txtBoxHCount.Show()
            txtBoxHCount.Text = hCount

            lblLGrade.Show()
            txtBoxLCount.Show()
            txtBoxLCount.Text = lCount

        ElseIf Not frmJobEntry.HLColSep = Nothing Then
            If frmJobEntry.HLColSep = "H" Then

                toAllocatedCount = DGVPakingA.Rows.Count
                lblCheese.Text = "Total to Remove"

            ElseIf frmJobEntry.HLColSep = "L" Then

                toAllocatedCount = DGVPakingA.Rows.Count
                lblCheese.Text = "Total to Remove"

            End If
        End If
        'For i = 1 To rowendcount
        '    If DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = "9" And DGVPakingA.Rows(i - 1).Cells("FLT_S").Value = False And (IsDBNull(DGVPakingA.Rows(i - 1).Cells("STDSTATE").Value)) Then
        '        toAllocatedCount = toAllocatedCount + 1
        '    End If
        'Next



        'GET NUMBER OF CONES THAT NEED ALLOCATING Count agains Job Barcode
        If frmJobEntry.varMachineCode = 29 Then
            Dim btnCountStart As Integer = rowendcount + 1
            Dim totBtn As Integer = 31 - btnCountStart
            For i = btnCountStart To 32
                Me.Controls("btnCone" & i.ToString).Visible = False
            Next
        End If

        'THIS SECTION GETS THE COUNT OF CHEESE ON THE LAST EXCEL SHEET TO DISPLAY NUMBER LEFT TO COMPLETE THE PACK SHEET 
        If Not frmJobEntry.HHLL = "YES" AndAlso frmJobEntry.HLColSep = Nothing Then
            sheetconecount()


            txtBoxToFinish.Text = remainingCheese
            txtBoxOnSheet.Text = packedCheese
        Else  ' change screen buttons
            btnDefect.Hide()
            btnSaveJob.Hide()
            btnFinJob.Hide()
            Label3.Hide()
            Label6.Hide()
            txtConeBcode.Hide()
            txtBoxToFinish.Hide()

            If Not frmJobEntry.HLColSep = Nothing Then
                lblHLSeperation.Text = frmJobEntry.HLColSep & " Colour Seperation Removal"
                txtCartNum.Text = frmJobEntry.HLColSep
            End If

            lblHLSeperation.Show()
            Label4.Hide()
            txtBoxOnSheet.Hide()


        End If


        txtboxTotal.Text = toAllocatedCount

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        'IF THIS IS AN EXISTING JOB THEN CALL BACK VALUES FROM DATABASE
        If frmJobEntry.coneValUpdate = 1 Then UpdateConeVal()


        If My.Settings.debugSet Then DGVPakingA.Visible = True

        Me.txtConeBcode.Clear()
        Me.txtConeBcode.Focus()


    End Sub


    Private Sub sheetconecount()

        'Go off to Class and get the cone count on any excel sheet for this grade from last 3 days
        getConeCount.xlCheck()

        'Dim searchstring = getConeCount.searchBarcode

        'SQL.ExecQuery("Select * from jobs where packsheetbcode = '" & searchstring & "'  ")
        'xlcheesecount = SQL.RecordCount

        xlcheesecount = getConeCount.nfree

        If xlcheesecount > 0 Then
            'MsgBox("sheeet name = " & searchstring & vbCrLf & "Cheese on sheet count = " & xlcheesecount)
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
            writeerrorLog.writelog("ExecQuery Error", ex.Message, False, "SQL Fault")
            writeerrorLog.writelog("ExecQuery Error", ex.ToString, False, "SQL Fault")
            PException = "ExecQuery Error: " & vbNewLine & ex.Message
            MsgBox(PException)

        End Try

    End Sub




    Public Sub UpdateConeVal()
        If My.Settings.debugSet Then frmDGV.Show()


        If Not frmJobEntry.HHLL = "YES" AndAlso frmJobEntry.HLColSep = Nothing Then
            For rw As Integer = 1 To rowendcount

                If DGVPakingA.Rows(rw - 1).Cells("CONESTATE").Value = "9" And DGVPakingA.Rows(rw - 1).Cells("FLT_S").Value = False And (IsDBNull(DGVPakingA.Rows(rw - 1).Cells("STDSTATE").Value)) Then

                    Me.Controls("btnCone" & rw).BackColor = Color.Green       'Grade A Cone
                End If

                If DGVPakingA.Rows(rw - 1).Cells("CONESTATE").Value = "15" Then
                    Me.Controls("btnCone" & rw).BackColor = Color.LightGreen       'Grade A Cone
                End If

                Me.Controls("btnCone" & rw).Enabled = False
            Next

        ElseIf frmJobEntry.HHLL = "YES" AndAlso frmJobEntry.HLColSep = Nothing Then

            For rw As Integer = 1 To rowendcount

                If Not IsDBNull(DGVPakingA.Rows(rw - 1).Cells("HHLL").Value) Then
                    If DGVPakingA.Rows(rw - 1).Cells("HHLL").Value = "H" Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.HPK
                        Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                        Me.Controls("btnCone" & rw).Enabled = False
                    ElseIf DGVPakingA.Rows(rw - 1).Cells("HHLL").Value = "L" Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.LPK
                        Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(126, 125, 250)
                        Me.Controls("btnCone" & rw).Enabled = False
                        'ElseIf DGVPakingA.Rows(rw - 1).Cells("HHLL").Value = "H Std" Then
                        '    Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.H_Std
                        '    Me.Controls("btnCone" & rw).BackColor = Color.Orange
                        '    Me.Controls("btnCone" & rw).Enabled = False
                        'ElseIf DGVPakingA.Rows(rw - 1).Cells("HHLL").Value = "MISS" Then
                        '    ' Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.H_Std
                        '    Me.Controls("btnCone" & rw).BackColor = Color.Red
                        '    Me.Controls("btnCone" & rw).Enabled = False

                    End If
                End If
            Next
        ElseIf Not frmJobEntry.HLColSep = Nothing Then



            'H Images
            Dim im_HDD As Image = My.Resources.HPK_DD
            Dim im_HD As Image = My.Resources.HPK_D
            Dim im_HMM As Image = My.Resources.HPK_MM
            Dim im_HL As Image = My.Resources.HPK_L
            Dim im_HLL As Image = My.Resources.HPK_LL
            Dim im_HB As Image = My.Resources.HPK_B

            'H Short images
            Dim im_HSD As Image = My.Resources.HSPK_D
            Dim im_HSM As Image = My.Resources.HSPK_M
            Dim im_HSL As Image = My.Resources.HSPK_L
            Dim im_HSB As Image = My.Resources.HSPK_B

            'L Images
            Dim im_LDD As Image = My.Resources.LPK_DD
            Dim im_LD As Image = My.Resources.LPK_D
            Dim im_LMM As Image = My.Resources.LPK_MM
            Dim im_LL As Image = My.Resources.LPK_L
            Dim im_LLL As Image = My.Resources.LPK_LL
            Dim im_LB As Image = My.Resources.LPK_B

            'H Short images
            Dim im_LSD As Image = My.Resources.LSPK_D
            Dim im_LSM As Image = My.Resources.LSPK_M
            Dim im_LSL As Image = My.Resources.LSPK_L
            Dim im_LSB As Image = My.Resources.LSPK_B

            'Waste
            Dim im_LW As Image = My.Resources.LPK_Waste
            Dim im_HW As Image = My.Resources.HPK_Waste

            For rw As Integer = 1 To rowendcount

                If Not IsDBNull(DGVPakingA.Rows(rw - 1).Cells("HHLL_Res").Value) Then
                    If frmJobEntry.HLColSep = "H" Then
                        Select Case DGVPakingA.Rows(rw - 1).Cells("HHLL_Res").Value.ToString
                            Case "H_DD"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HDD
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "H_D"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HD
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "H_MM"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HMM
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "H_L"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HL
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "H_LL"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HLL
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "H_B"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HB
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False

                            Case "HS_D"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HSD
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "HS_M"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HSM
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "HS_L"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HSL
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "HS_B"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HSB
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "H_W"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_HW
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                        End Select
                    End If


                    If frmJobEntry.HLColSep = "L" Then
                        Select Case DGVPakingA.Rows(rw - 1).Cells("HHLL_Res").Value.ToString
                            Case "L_DD"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LDD
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(126, 125, 250)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "L_D"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LD
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(126, 125, 250)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "L_MM"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LMM
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(126, 125, 250)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "L_L"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LL
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(126, 125, 250)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "L_LL"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LLL
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(126, 125, 250)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "L_B"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LB
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(126, 125, 250)
                                Me.Controls("btnCone" & rw).Enabled = False



                            Case "LS_D"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LSD
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "LS_M"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LSM
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "LS_L"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LSL
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False
                            Case "LS_B"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LSB
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(26, 169, 238)
                                Me.Controls("btnCone" & rw).Enabled = False



                            Case "L_W"
                                Me.Controls("btnCone" & rw).BackgroundImage = im_LW
                                Me.Controls("btnCone" & rw).BackColor = Color.FromArgb(126, 125, 250)
                                Me.Controls("btnCone" & rw).Enabled = False


                        End Select
                    End If
                End If
            Next

        End If

        txtBoxCancel.Focus()

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
        Dim coneCount As Integer = 0
        Dim today As String
        today = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")




        Try

            For i = 1 To rowendcount



                If DGVPakingA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = 9 And DGVPakingA.Rows(i - 1).Cells("FLT_S").Value = False Then
                    curcone = DGVPakingA.Rows(i - 1).Cells("CONENUM").Value
                    Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.LightGreen       'Grade A Cone
                    DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = 14
                    DGVPakingA.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                    DGVPakingA.Rows(i - 1).Cells("OPNAME").Value = frmJobEntry.varUserName
                    'DGVPakingA.Rows(i - 1).Cells("CARTENDTM").Value = today

                    'CHECK TO SEE IF DATE ALREADY SET FOR END TIME
                    If IsDBNull(DGVPakingA.Rows(i - 1).Cells("PACKENDTM").Value) Then
                        'For rows As Integer = 1 To rowendcount
                        DGVPakingA.Rows(i - 1).Cells("PACKENDTM").Value = today 'PACKING CHECK END TIME.
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
                    txtConeBcode.Clear()
                    txtConeBcode.Refresh()
                    txtConeBcode.Focus()

                ElseIf DGVPakingA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value >= 14 Then


                    Label1.Text = "Cheese already allocated"
                    Label1.Visible = True
                    DelayTM()
                    Label1.Visible = False

                    txtConeBcode.Clear()
                    txtConeBcode.Refresh()
                    txtConeBcode.Focus()

                ElseIf DGVPakingA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value < 9 Or DGVPakingA.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = 9 And DGVPakingA.Rows(i - 1).Cells("FLT_S").Value = True Then
                    curcone = DGVPakingA.Rows(i - 1).Cells("CONENUM").Value
                    psorterror = 1
                    Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.Red      'Wrong Cone scanned
                    DGVPakingA.Rows(i - 1).Cells("PSORTERROR").Value = psorterror
                    DGVPakingA.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                    ' DGVPakingA.Rows(i - 1).Cells("CONESTATE").Value = "14"
                    DGVPakingA.Rows(i - 1).Cells("CARTENDTM").Value = today

                    'Label1.Visible = True
                    'Label1.Text = "You Have scanned a Cheese that is not 'GRADE A'"
                    'DelayTM()
                    'Label1.Visible = False

                    Me.Hide()
                    frmRemoveCone.Show()

                    bcodeScan = ""
                    psorterror = 0
                    curcone = 0
                    txtConeBcode.Clear()
                    txtConeBcode.Refresh()
                    txtConeBcode.Focus()


                End If
            Next

        Catch ex As Exception

            'Write error to Log File
            writeerrorLog.writelog("Scan Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Scan Error", ex.ToString, False, "System Fault")

            MsgBox("Barcode Sacn Error " & vbNewLine & ex.Message)
            txtConeBcode.Clear()
            txtConeBcode.Refresh()
            txtConeBcode.Focus()
            Exit Sub
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
        DGVPakingA.ClearSelection()
        DGVPakingA.DataSource = Nothing  'used to clear DGV
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Close()
    End Sub



    Public Sub endCheck()

        If toAllocatedCount = allocatedCount Or saveJob = 1 Or finJob = 1 Then

            Dim prodNameMod As String
            Dim PathFileName As String
            Dim sheetname As String
            Dim FileName As String

            todayDir()


            prodNameMod = DGVPakingA.Rows(0).Cells("PRODNAME").Value.ToString
            prodNameMod = prodNameMod.Replace("/", "_")

            'CREATE THE SHEET NAME WHICH IS THE 4 LETTER REFRENCE AT THE END OF PRODUCT NAME
            sheetname = prodNameMod.Substring(prodNameMod.Length - 5) & "_A"

            'Search for last date for file

            FileName = prodNameMod & " " _
                    & DGVPakingA.Rows(0).Cells("MERGENUM").Value.ToString & "_" _
                    & DGVPakingA.Rows(0).Cells("PRNUM").Value.ToString & " A" & ".xlsx"



            If Not PrevPath1 = Nothing Then  'If we have a previous date then do a file open check, otherwise do not check
                PathFileName = PrevPath1 & "\" & FileName


                Try

                    ' Dim tmpFileName As String = ""
                    Dim fOpen As IO.FileStream = IO.File.Open(PathFileName, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.None)
                    fOpen.Close()
                    fOpen.Dispose()
                    fOpen = Nothing
                Catch e1 As IO.IOException



                    writeerrorLog.writelog("Excel File Open", "File " & PathFileName & "Cannot Save, file is Open", False, "Packing sheet")


                    If saveJob = 1 Then
                        Dim result = MessageBox.Show("The file " & PathFileName & " is open on this computer or another computer." & vbCrLf &
                   "Please find out who has the file open and close it." & vbCrLf & vbCrLf &
                   vbCrLf &
                   "When file has been closed Press OK and then press SAVE on the Cart screen which will Retry the save",
                   "Excel File Open Cannot Save", MessageBoxButtons.OK, MessageBoxIcon.Warning)


                        If result = DialogResult.OK Then
                            saveJob = 0
                            finJob = 0
                            Exit Sub
                        End If
                    ElseIf finJob = 1 Then
                        Dim result = MessageBox.Show("The file " & PathFileName & " is open on this computer or another computer." & vbCrLf &
                "Please find out who has the file open and close it." & vbCrLf & vbCrLf &
                vbCrLf &
                "When file has been closed Press OK and then press FINISH on the Cart screen which will Retry the finish operation",
                "Excel File Open Cannot Save", MessageBoxButtons.OK, MessageBoxIcon.Warning)


                        If result = DialogResult.OK Then
                            saveJob = 0
                            finJob = 0
                            Exit Sub
                        End If
                    Else
                        Dim result = MessageBox.Show("The file " & PathFileName & " is open on this computer or another computer." & vbCrLf &
                                    "Please find out who has the file open and close it." & vbCrLf & vbCrLf &
                                    vbCrLf &
                                    "When file has been closed Press OK and then press SAVE on the Cart screen which will Retry the save",
                                    "Excel File Open Cannot Save", MessageBoxButtons.OK, MessageBoxIcon.Warning)


                        If result = DialogResult.OK Then
                            saveJob = 0
                            finJob = 0
                            Exit Sub
                        End If
                    End If

                End Try
            End If


            curcone = 0
                Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
                Label1.Visible = True

                'pauseScan = 1 'Stop Barcode entry
                Label1.Text = ("Please wait creating packing Excel sheet")


                '**************************************************************************************************************
                'UPDATE ALL CHEESE ON CART AS PROCESSED TODAY FOR DAILY PACKING REPORT TO WORK


                If IsDBNull(DGVPakingA.Rows(0).Cells("PACKCARTTM").Value) Then
                    For x As Integer = 1 To rowendcount
                        DGVPakingA.Rows(x - 1).Cells("PACKCARTTM").Value = Today 'PACKING CHECK END TIME
                    Next
                End If
            '**************************************************************************************************************




            frmPackRepMain.PackRepMainSub()
            frmPackRepMain.Close()
            UpdateDatabase()
            Label1.Visible = False
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

        tsbtnSave()


        'New Update to avoid concurrency errors

        Try


            For i = 1 To rowendcount


                SQL.AddParam("@id", DGVPakingA.Rows(i - 1).Cells("id_Product").Value)
                SQL.AddParam("@opname", DGVPakingA.Rows(i - 1).Cells("OpName").Value)
                SQL.AddParam("@conestate", DGVPakingA.Rows(i - 1).Cells("conestate").Value)
                SQL.AddParam("@shortcone", DGVPakingA.Rows(i - 1).Cells("SHORTCONE").Value)
                SQL.AddParam("@defcone", DGVPakingA.Rows(i - 1).Cells("DEFCONE").Value)
                SQL.AddParam("@cartendtm", DGVPakingA.Rows(i - 1).Cells("CartEndTm").Value)
                SQL.AddParam("@flt_k", DGVPakingA.Rows(i - 1).Cells("FLT_K").Value)
                SQL.AddParam("@flt_d", DGVPakingA.Rows(i - 1).Cells("FLT_D").Value)
                SQL.AddParam("@flt_f", DGVPakingA.Rows(i - 1).Cells("FLT_F").Value)
                SQL.AddParam("@flt_o", DGVPakingA.Rows(i - 1).Cells("FLT_O").Value)
                SQL.AddParam("@flt_t", DGVPakingA.Rows(i - 1).Cells("FLT_T").Value)
                SQL.AddParam("@flt_p", DGVPakingA.Rows(i - 1).Cells("FLT_P").Value)
                SQL.AddParam("@flt_s", DGVPakingA.Rows(i - 1).Cells("FLT_S").Value)
                SQL.AddParam("@flt_n", DGVPakingA.Rows(i - 1).Cells("FLT_N").Value)
                SQL.AddParam("@flt_w", DGVPakingA.Rows(i - 1).Cells("FLT_W").Value)
                SQL.AddParam("@flt_h", DGVPakingA.Rows(i - 1).Cells("FLT_H").Value)
                SQL.AddParam("@flt_tr", DGVPakingA.Rows(i - 1).Cells("FLT_TR").Value)
                SQL.AddParam("@flt_b", DGVPakingA.Rows(i - 1).Cells("FLT_B").Value)
                SQL.AddParam("@flt_c", DGVPakingA.Rows(i - 1).Cells("FLT_C").Value)
                SQL.AddParam("@oppack", DGVPakingA.Rows(i - 1).Cells("OpPack").Value)
                SQL.AddParam("@psorterror", DGVPakingA.Rows(i - 1).Cells("PSORTERROR").Value)
                SQL.AddParam("@packendtm", DGVPakingA.Rows(i - 1).Cells("Packendtm").Value)
                SQL.AddParam("@packsheet", DGVPakingA.Rows(i - 1).Cells("PACKSHEETBCODE").Value)
                SQL.AddParam("@carton", DGVPakingA.Rows(i - 1).Cells("CARTONNUM").Value)
                SQL.AddParam("@packcarttm", DGVPakingA.Rows(i - 1).Cells("PACKCARTTM").Value)


                SQL.ExecQuery(" Update jobs set CONESTATE = @conestate, OPPACK = @oppack, OPNAME = @opname, PACKENDTM = @packendtm, " _
                            & "SHORTCONE = @shortcone, DEFCONE = @defcone," _
                            & "FLT_K =  @flt_k, FLT_D = @flt_d, FLT_F = @flt_f, FLT_O = @flt_o, FLT_T = @flt_t, FLT_P = @flt_p, " _
                            & "FLT_S = @flt_s, FLT_N = @flt_n, FLT_W = @flt_w, FLT_H = @flt_h, FLT_TR = @flt_tr, FLT_B = @flt_b,FLT_C = @flt_c, " _
                            & "PSORTERROR = @psorterror, CARTENDTM = @cartendtm,PACKSHEETBCODE = @packsheet, CARTONNUM = @carton, PACKCARTTM = @packcarttm " _
                            & "Where id_product = @id")

            Next

        Catch dbcx As DBConcurrencyException
            Dim Response As String

            Response = dbcx.Row.ToString
            writeerrorLog.writelog("db A_Pk Con Error", Response, False, "reChkA_Pk Con Fault")
            Response = dbcx.RowCount.ToString
            writeerrorLog.writelog("db A_Pk Con Error", Response, False, "ReChkA_Pk Con Fault")



        Catch ex As Exception
            Dim sheetNo As String = frmJobEntry.txtLotNumber.Text
            'Write error to Log File
            writeerrorLog.writelog("Sheet No.", sheetNo, False, "Packing sheet")
            writeerrorLog.writelog("db A_Pk Error", ex.Message, False, "db ReChkA_Pk Fault")
            writeerrorLog.writelog("db A_Pk Error", ex.ToString, False, "db ReChkA_Pk Fault")

            MsgBox("Update Error: " & vbNewLine & ex.Message)

        End Try

        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        'Try

        '    If PDS.HasChanges Then


        '        'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

        '        PDA.Update(PDS.Tables(0))

        '    End If
        'Catch dbcx As DBConcurrencyException
        '    Dim Response As String

        '    Response = dbcx.Row.ToString
        '    writeerrorLog.writelog("db A_Pk Con Error", Response, False, "A_Pk Con Fault")
        '    Response = dbcx.RowCount.ToString
        '    writeerrorLog.writelog("db A_Pk Con Error", Response, False, "A_Pk Con Fault")



        'Catch ex As Exception
        '    Dim sheetNo As String = frmJobEntry.txtLotNumber.Text
        '    'Write error to Log File
        '    writeerrorLog.writelog("Sheet No.", sheetNo, False, "Packing sheet")
        '    writeerrorLog.writelog("db A_Pk Error", ex.Message, False, "db A_Pk Fault")
        '    writeerrorLog.writelog("db A_Pk Error", ex.ToString, False, "db A_Pk Fault")

        '    MsgBox("Update Error: " & vbNewLine & ex.Message)
        'End Try



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
        'DGVPakingA.EndEdit()


    End Sub



    Private Sub btnSaveJob_Click(sender As Object, e As EventArgs) Handles btnSaveJob.Click

        Dim btnpress As String = "Operator " & frmJobEntry.varUserName & "  Pressed STOP on Grade A Packing sheet"

        writeerrorLog.writelog("Packing Stop/Finish pressed", btnpress, False, "Packing sheet")

        saveJob = 1
        endCheck()

    End Sub

    Private Sub btnFinJob_Click(sender As Object, e As EventArgs) Handles btnFinJob.Click

        Dim btnpress As String = "Operator " & frmJobEntry.varUserName & "  Pressed FINISH on Grade A Packing sheet"

        writeerrorLog.writelog("Packing Stop/Finish pressed", btnpress, False, "Packing sheet")


        finJob = 1
        endCheck()


    End Sub


    'SUBROUTINE TO CHECK IF DAY DIRECTORIES EXIST IF NOT THEY ARE CREATED
    Private Sub todayDir()

        prodNum = DGVPakingA.Rows(0).Cells("PRNUM").Value.ToString
        sheetSearch = prodNum & "______A[0-9]"


        If frmJobEntry.txtGrade.Text <> "Round1" And frmJobEntry.txtGrade.Text <> "Round2" And
            frmJobEntry.txtGrade.Text <> "Round3" And frmJobEntry.txtGrade.Text <> "STD" And
            frmJobEntry.txtGrade.Text <> "HLRound1" And frmJobEntry.txtGrade.Text <> "HLRound2" And
            frmJobEntry.txtGrade.Text <> "HLRound3" And frmJobEntry.txtGrade.Text <> "HLSTD" And
            frmJobEntry.txtGrade.Text <> "ReCheck" And
            frmJobEntry.txtGrade.Text <> "Create H Cart" And
            frmJobEntry.txtGrade.Text <> "Create L Cart" Then  'IF RECHECK DO NOT GET SHEETS FROM PREVIOUS DAY

            ' routine to check if a today directory exists otherwise creat a new one
            'Check to see if we have any sheets for this product and Grade in previous days
            SQL.AddParam("@searchsheet", sheetSearch)
            Dim daysstring As Integer = "-" & My.Settings.searchDays
            SQL.AddParam("@days", daysstring)




            Try


                SQL.ExecQuery("Select MAX(PACKENDTM) PACKENDTM from jobs where packendtm between DateAdd(DD, @days, GETDATE()) and GetDATE() and (packsheetbcode like  '%' +  @searchsheet + '%'  )")

                If SQL.RecordCount > 0 Then


                    'LOAD THE DATA FROM dB IN TO THE DATAGRID
                    frmDGV.DGVdata.DataSource = SQL.SQLDS.Tables(0)
                    frmDGV.DGVdata.Rows(0).Selected = True


                    If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("PACKENDTM").Value) Then


                        tmp_sheetdate = frmDGV.DGVdata.Rows(0).Cells("PACKENDTM").Value
                        sheetDate = tmp_sheetdate.ToString("dd_MM_yyyy")
                    End If





                    If sheetDate = Nothing Then
                        PrevPath1 = Nothing
                    Else

                        PrevPath1 = (My.Settings.dirPacking & "\" & sheetDate)

                    End If


                Else

                    PrevPath1 = Nothing

                End If


            Catch ex As Exception
                writeerrorLog.writelog("xl Find Old Sheet", ex.Message, True, "System_Fault")
                MsgBox(ex.ToString)

            End Try
        End If






    End Sub









    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then
            If txtBoxCancel.Text = "CANCEL" Then
                btnBack.PerformClick()
            Else
                prgContinue()

            End If



        End If

    End Sub


End Class