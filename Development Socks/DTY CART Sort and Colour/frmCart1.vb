Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class frmCart1

    Dim MyHLGradeUpdate As New Excel.Application

    'Colour related variables
    Dim dC, Blue, BlueGreen, Green, GreenYellow, Yellow, YellowRed, Red, RedBlue As String
    Dim varLedR As String
    Dim varLedG As String
    Dim varLedB As String
    Dim varCIE_L As String
    Dim varCIE_a As String
    Dim varCIE_b As String
    Dim varCIE_dL As String
    Dim varCIE_dE As String


    'Manual assesment variables
    Dim varVisConeInspect As String
    Dim coneBarley As String = 0
    Dim coneWaste As String = 0
    Dim coneZero As String = 0
    Dim coneM10 As String = 0
    Dim coneP10 As String = 0
    Dim coneM30 As String = 0
    Dim coneP30 As String = 0
    Dim coneM50 As String = 0
    Dim coneP50 As String = 0
    Dim valHH As String
    Dim valLL As String
    Dim valHStd As String

    Dim dbHHLL As String = Nothing

    Dim btnImage As Image
    Dim keepDefcodes As Integer

    'Faults
    Dim Fault_S As String = "False"
    Dim Fault_X As String = "False"
    Dim shortC(32) As String
    'Dim short1, short2, short3, short4, short5, short6, short7, short8, short9, short10, short11, short12, short13, short14, short15, short16 As String
    'Dim short17, short18, short19, short20, short21, short22, short23, short24, short25, short26, short27, short28, short29, short30, short31, short32 As String

    'ReCheck Params
    Dim reChecked, ReCheckTime As String

    '
    Public removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
    Dim incoming As String
    Public measureOn As String
    Public NoCone As Integer
    Public StdCone As Integer = Nothing
    Public stdStateVar As Integer = Nothing
    Public defect As Integer
    Public shortCone As Integer
    Public varCartStartTime As String   'Record time that we started measuring
    Public varCartEndTime As String
    Public coneNumOffset As Integer
    Dim varConeBCode As String
    Dim fileActive As Integer
    Public varConeNum As Integer
    'Public batchNum As String
    Public coneCount As Integer
    Public coneState As String
    Dim fltconeNum As String
    Dim csvRowNum As Integer
    Dim rptTime As String
    Dim lclHHLL, lclMDML As String
    Dim remaincount As Integer
    Dim tmpCartType As String = Nothing
    Dim HLColCheck As Integer
    Dim HorL As String = Nothing

    'Printing
    Dim prodNameMod As String = Nothing
    Dim sheetName As String = Nothing
    Dim SaveString As String = Nothing

    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError






    'Private SQL As New SQLConn


    Private Sub frmCart1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        rptTime = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")  'log time we opened cart


        Dim localMCCode = frmJobEntry.varMachineCode
        Dim btnNum As Integer
        Dim btnNums As String

        If Not frmJobEntry.HLColChk = Nothing Then
            HorL = frmJobEntry.HLColChk
            HLColCheck = 1
            btnNums = 1  'set to 1 as mixed cheese numbers so we will always use positions 1-32 on display
        Else
            btnNums = frmJobEntry.varCartSelect
        End If
        ' btnNums = frmJobEntry.varCartSelect

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

        'SET CORRECT BUTTUN NUMBERS BASED ON CONE NUMBERS (SPINDEL NUMBERS)
        For i As Integer = 1 To frmDGV.DGVdata.Rows.Count

            Me.Controls("btnCone" & i.ToString).Text = btnNum
            btnNum = btnNum + 1

        Next


        'New section to hide unused buttons
        Dim btnEraseStart As Integer = frmDGV.DGVdata.Rows.Count + 1
        Dim TotalBtn As Integer = 31 - btnEraseStart

        For i = btnEraseStart To 32
            Me.Controls("btnCone" & i.ToString).Visible = False
        Next

        If Not HLColCheck = 1 Then
            Me.txtCartNum.Text = frmJobEntry.cartSelect
            Me.lblMcNum.Text = frmDGV.DGVdata.Rows(0).Cells("MCNAME").Value.ToString
        Else
            Me.txtCartNum.Text = HorL
            Me.lblMcNum.Text = "MIX"
        End If


        lblProdName.Text = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value.ToString
        lblTFNum.Text = frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value.ToString
        lblDoffNum.Text = frmDGV.DGVdata.Rows(0).Cells("DOFFNUM").Value.ToString


        If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("CARTSTARTTM").Value) Then
            varCartStartTime = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")    'Set new time if cell is Null
        Else
            varCartStartTime = frmDGV.DGVdata.Rows(0).Cells("CARTSTARTTM").Value  'set time to cell value if not null
        End If





        'HIDE SAVE BUTTON WHEN FORM OPENS
        Me.btnSave.Visible = False

        'HIDE CLEAR BUTTON WHEN FORM OPENS
        Me.btnClear.Visible = False

        'SET TEXT FOR VISUAL GRADING BUTTON WHEN FORM OPENS
        btnVisGrade.Text = "Visual Grade OFF"

        'SET TEXT ON OVERIDE BUTTON TO OFF
        btnUnlock.Text = "UNLOCK OFF"




        If Not HLColCheck Then 'If we are not doinf hl colour check then check for HL seperation
            lclHHLL = frmJobEntry.HHLL
            lclMDML = frmJobEntry.MDML

            'Check to see if cart has alrady had Pattern Seperation
            If Not IsDBNull(frmDGV.DGVdata.Rows(0).Cells("HHLL").Value) Then
                dbHHLL = "YES"
            End If

            frmJobEntry.HHLL = Nothing
            frmJobEntry.MDML = Nothing
        End If

        tmpCartType = frmJobEntry.txtLotNumber.Text.Substring(12, 1)

        'VISUAL CHECK BUTTONS VISABLE OR NOT
        If My.Settings.chkUseColour And (String.IsNullOrWhiteSpace(lclHHLL) Or lclHHLL = "NO") Then  'do normal colour grading

            Me.btnVisGrade.Visible = False  'Hide visgrade button
            btnBarley.Visible = True
            btnWaste.Visible = True
            btnZero.Visible = True
            btnM10.Visible = True
            btnM30.Visible = True
            btnM50.Visible = True
            btnP10.Visible = True
            btnP30.Visible = True
            btnP50.Visible = True
            btnBarley.Enabled = True
            btnWaste.Enabled = True
            btnZero.Enabled = True
            btnM10.Enabled = True
            btnM30.Enabled = True
            btnM50.Enabled = True
            btnP10.Enabled = True
            btnP30.Enabled = True
            btnP50.Enabled = True
            btnWaste.Visible = True
            btnStdCheese.Visible = False
            lblBarley.Visible = True
            lblWaste.Visible = True
            lblZero.Visible = True
            lblM10.Visible = True
            lblP10.Visible = True
            lblM30.Visible = True
            lblP30.Visible = True
            lblM50.Visible = True
            lblP50.Visible = True
            txtBarley.Visible = True
            txtWaste.Visible = True
            txtZero.Visible = True
            txtM10.Visible = True
            txtP10.Visible = True
            txtM30.Visible = True
            txtP30.Visible = True
            txtM50.Visible = True
            txtP50.Visible = True

            If Not HorL = Nothing Then
                Select Case HorL
                    Case "H"
                        lblHLSeperation.Text = "H Colour Grading"
                        lblHLSeperation.Visible = True
                    Case "L"
                        lblHLSeperation.Text = "L Colour Grading"
                        lblHLSeperation.Visible = True
                End Select
            End If


            varVisConeInspect = 1
            coneBarley = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0


            Me.btnDefect.Enabled = False
            Me.btnNoCone.Enabled = False
            Me.btnShort.Enabled = False





        ElseIf My.Settings.chkUseColour And lclHHLL = "YES" And tmpCartType = "B" Then '(lclHHLL = "YES" And Not dbHHLL = "YES") Then  'Do H and L seperation grading
            GroupBoxHHLL.Show()
            Me.btnVisGrade.Visible = False  'Hide visgrade button
            btnBarley.Visible = False
            btnZero.Visible = False
            btnM10.Visible = False
            btnM30.Visible = False
            btnM50.Visible = False
            btnP10.Visible = False
            btnP30.Visible = False
            btnP50.Visible = False
            btnStdCheese.Visible = False
            lblBarley.Visible = False
            lblWaste.Visible = False
            lblZero.Visible = False
            lblM10.Visible = False
            lblP10.Visible = False
            lblM30.Visible = False
            lblP30.Visible = False
            lblM50.Visible = False
            lblP50.Visible = False
            txtBarley.Visible = False
            txtWaste.Visible = False
            txtZero.Visible = False
            txtM10.Visible = False
            txtP10.Visible = False
            txtM30.Visible = False
            txtP30.Visible = False
            txtM50.Visible = False
            txtP50.Visible = False


            txtHH.Location = New Point(160, 603)
            txtHH.Size = New Size(922, 24)
            txtHH.Show()
            txtLL.Location = New Point(160, 630)
            txtLL.Size = New Size(922, 24)
            txtLL.Show()
            txtHSTD.Location = New Point(160, 657)
            txtHSTD.Size = New Size(922, 24)
            txtHSTD.Show()

            lblZero.Location = New Point(126, 608)
            lblZero.Text = "H"
            lblM10.Location = New Point(126, 635)
            lblM10.Text = "L"
            lblP10.Location = New Point(126, 664)
            lblP10.Text = "H Std"

            lblZero.Visible = True
            lblM10.Visible = True
            lblP10.Visible = True


            lblHLSeperation.Visible = True
            lblRemain.Visible = True
            txtBoxRemain.Visible = True


            varVisConeInspect = 1
            coneBarley = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0
            valHH = 0
            valLL = 0
            valHStd = 0


            Me.btnDefect.Enabled = False
            Me.btnNoCone.Enabled = False
            Me.btnShort.Enabled = False
            btnFinishedJob.Hide()



        Else  ' Do not show any colour grading
            'COLOUR CHECK BUTTONS NOT VISIBLE
            Me.btnVisGrade.Visible = False
            btnBarley.Visible = False
            btnZero.Visible = False
            btnM10.Visible = False
            btnM30.Visible = False
            btnM50.Visible = False
            btnP10.Visible = False
            btnP30.Visible = False
            btnP50.Visible = False
            btnStdCheese.Visible = False
            lblBarley.Visible = False
            lblWaste.Visible = False
            lblZero.Visible = False
            lblM10.Visible = False
            lblP10.Visible = False
            lblM30.Visible = False
            lblP30.Visible = False
            lblM50.Visible = False
            lblP50.Visible = False
            txtBarley.Visible = False
            txtWaste.Visible = False
            txtZero.Visible = False
            txtM10.Visible = False
            txtP10.Visible = False
            txtM30.Visible = False
            txtP30.Visible = False
            txtM50.Visible = False
            txtP50.Visible = False
        End If

        ' SHOW SORT BUTTONS VISIBLE OR NOT 
        If My.Settings.chkUseSort Then
            Me.btnNoCone.Visible = True
            Me.btnDefect.Visible = True
            Me.btnShort.Visible = True
            btnFinishedJob.Show()
            Me.btnStdCheese.Visible = True

            If lclHHLL = "YES" Then
                txtHH.Show()
                txtLL.Show()
                txtHSTD.Show()

                lblZero.Text = "H"
                lblM10.Text = "L"
                lblP10.Text = "H Std"

                lblZero.Visible = True
                lblM10.Visible = True
                lblP10.Visible = True
                lblHLSeperation.Visible = True
            End If



        Else
            Me.btnNoCone.Visible = False
            Me.btnDefect.Visible = False
            Me.btnShort.Visible = False

        End If


        If My.Settings.chkUsePack And dbHHLL = "YES" Then
            btnBarley.Visible = False
            btnZero.Visible = False
            btnM10.Visible = False
            btnM30.Visible = False
            btnM50.Visible = False
            btnP10.Visible = False
            btnP30.Visible = False
            btnP50.Visible = False
            btnStdCheese.Visible = False

            lblBarley.Visible = False
            lblWaste.Visible = False
            lblZero.Visible = False
            lblM10.Visible = False
            lblP10.Visible = False
            lblM30.Visible = False
            lblP30.Visible = False
            lblM50.Visible = False
            lblP50.Visible = False

            txtBarley.Visible = False
            txtWaste.Visible = False
            txtZero.Visible = False
            txtM10.Visible = False
            txtP10.Visible = False
            txtM30.Visible = False
            txtP30.Visible = False
            txtM50.Visible = False
            txtP50.Visible = False

            txtHH.Show()
            txtLL.Show()
            ' txtHSTD.Show()
            ' txtLSTD.Show()

            lblZero.Text = "H"
            lblM10.Text = "L"
            ' lblP10.Text = "H Std"
            ' lblM30.Text = "L Std"
            lblZero.Visible = True
            lblM10.Visible = True
            ' lblP10.Visible = True
            ' lblM30.Visible = True

            lblHLSeperation.Visible = True
            'lblRemain.Visible = True
            'txtBoxRemain.Visible = True


            varVisConeInspect = 1
            coneBarley = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0
            valHH = 0
            valLL = 0
            ' valHStd = 0
            ' valLStd = 0

            Me.btnDefect.Enabled = False
            Me.btnNoCone.Enabled = False
            Me.btnShort.Enabled = False
            btnFinishedJob.Hide()



        End If





        'IF THIS IS AND EXISTING JOB THEN CALL BACK VALUES FROM DATABASE
        If frmJobEntry.coneValUpdate Then UpdateConeVal()


    End Sub


    Private Sub UpdateConeVal()

        Dim cellVal As String
        remaincount = 32

        'H Images
        Dim im_HDD As Image = My.Resources.H_DD
        Dim im_HD As Image = My.Resources.H_D
        Dim im_HMM As Image = My.Resources.H_MM
        Dim im_HL As Image = My.Resources.H_L
        Dim im_HLL As Image = My.Resources.H_LL
        Dim im_HB As Image = My.Resources.H_B

        'H Short images
        Dim im_HSD As Image = My.Resources.HS_D
        Dim im_HSM As Image = My.Resources.HS_M
        Dim im_HSL As Image = My.Resources.HS_L
        Dim im_HSB As Image = My.Resources.HS_B

        'L Images
        Dim im_LDD As Image = My.Resources.L_DD
        Dim im_LD As Image = My.Resources.L_D
        Dim im_LMM As Image = My.Resources.L_MM
        Dim im_LL As Image = My.Resources.L_L
        Dim im_LLL As Image = My.Resources.L_LL
        Dim im_LB As Image = My.Resources.L_B

        'H Short images
        Dim im_LSD As Image = My.Resources.LS_D
        Dim im_LSM As Image = My.Resources.LS_M
        Dim im_LSL As Image = My.Resources.LS_L
        Dim im_LSB As Image = My.Resources.LS_B

        'Waste
        Dim im_LW As Image = My.Resources.L_Waste
        Dim im_HW As Image = My.Resources.H_Waste

        For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count 'cheese count

            For cl As Integer = 10 To 22


                'jump rows if not requierd
                If cl > 12 And cl < 15 Then Continue For


                cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells(cl).Value.ToString

                If cl = 10 And cellVal > 0 Then
                    Me.Controls("btnCone" & rw).BackColor = Color.Red       'SHORT
                    Me.Controls("btnCone" & rw).Enabled = True
                    shortC(rw) = 1

                End If

                If cl = 11 And cellVal > 0 Then
                    Me.Controls("btnCone" & rw).BackColor = Color.Pink      'NOCONE
                    Me.Controls("btnCone" & rw).Enabled = False
                    remaincount = remaincount - 1
                End If

                If cl = 12 And cellVal > 0 Then Me.Controls("btnCone" & rw).BackColor = Color.Yellow    'DEFECT



                If cl = 15 And cellVal > 0 Then
                    If HLColCheck Then
                        Select Case HorL
                            Case "H"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HSM
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HMM   'My.Resources.H_MM
                                End If

                            Case "L"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LSM
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LMM
                                End If
                        End Select
                    Else
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.Zero       'ZERO CONE
                    End If
                End If


                If cl = 16 And cellVal > 0 Then
                    If HLColCheck Then
                        Select Case HorL
                            Case "H"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HSB
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HB
                                End If

                            Case "L"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LSB
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LB
                                End If
                        End Select
                    Else
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.BARRE        'BARRE
                    End If
                End If


                If cl = 17 And cellVal > 0 Then
                    If HLColCheck Then
                        Select Case HorL
                            Case "H"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HSL       'M10
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HL       'M10
                                End If

                            Case "L"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LSL  'M10
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LL  'M10
                                End If
                        End Select
                    Else
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.M10  'M10
                    End If
                End If


                If cl = 18 And cellVal > 0 Then
                    If HLColCheck Then
                        Select Case HorL
                            Case "H"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HSD       'P10
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HD        'P10
                                End If

                            Case "L"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LSD  'P10
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LD  'P10
                                End If
                        End Select
                    Else
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.P10     'P10
                    End If
                End If


                If cl = 19 And cellVal > 0 Then
                    If HLColCheck Then
                        Select Case HorL
                            Case "H"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HSB       'M30
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HLL        'M30
                                End If

                            Case "L"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LSB   'M30
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LLL    'M30
                                End If
                        End Select
                    Else
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.M30    'M30
                    End If
                End If



                If cl = 20 And cellVal > 0 Then
                    If HLColCheck Then
                        Select Case HorL
                            Case "H"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HSB      'P30
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HDD       'P30
                                End If

                            Case "L"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LSB 'P30
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LDD   'P30
                                End If
                        End Select
                    Else
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.P30   'P30
                    End If
                End If



                If cl = 21 And cellVal > 0 Then
                    If HLColCheck Then
                        Select Case HorL
                            Case "H"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HSB        'M50
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HB        'M50
                                End If

                            Case "L"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LSB 'M50
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LB  'M50
                                End If
                        End Select
                    Else
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.M50      'M50
                    End If
                End If


                If cl = 22 And cellVal > 0 Then
                    If HLColCheck Then
                        Select Case HorL
                            Case "H"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HSB       'P50
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_HB       'P50
                                End If

                            Case "L"
                                If shortC(rw) = 1 Then
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LSB  'P50
                                Else
                                    Me.Controls("btnCone" & rw).BackgroundImage = im_LSB   'P50
                                End If
                        End Select
                    Else
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.P50     'P50
                    End If
                End If


            Next

            If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("STDSTATE").Value) Then
                If frmDGV.DGVdata.Rows(rw - 1).Cells("STDSTATE").Value > "0" Then

                    Me.Controls("btnCone" & rw).BackColor = Color.Orange      'NOCONE As allocated as STD
                    Me.Controls("btnCone" & rw).Enabled = False
                End If
            End If


            'CHECK FLT_S FLA
            If frmDGV.DGVdata.Rows(rw - 1).Cells("FLT_S").Value = True And frmDGV.DGVdata.Rows(rw - 1).Cells("DEFCONE").Value = 0 Then
                Me.Controls("btnCone" & rw).BackColor = Color.Red
                Me.Controls("btnCone" & rw).Enabled = True
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("FLT_S").Value = True And frmDGV.DGVdata.Rows(rw - 1).Cells("DEFCONE").Value > 0 Then
                Me.Controls("btnCone" & rw).BackColor = Color.Red
                Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.DefectStrip
                Me.Controls("btnCone" & rw).Enabled = True
            End If


            'WASTE CELLS in db

            If frmDGV.DGVdata.Rows(rw - 1).Cells("COLWASTE").Value > 0 Or
                    frmDGV.DGVdata.Rows(rw - 1).Cells("FLT_W").Value = "True" Then

                If HorL = Nothing Then
                    Me.Controls("btnCone" & rw).BackColor = Color.Purple  'WASTE
                    'If frmDGV.DGVdata.Rows(rw - 1).Cells("FLT_W").Value = True Then Me.Controls("btnCone" & rw).BackColor = Color.Purple  'WASTE
                Else

                    Select Case HorL
                        Case "H"
                            Me.Controls("btnCone" & rw).BackColor = Color.Purple  'WASTE
                            Me.Controls("btnCone" & rw).BackgroundImage = im_HW
                        Case "L"
                            Me.Controls("btnCone" & rw).BackColor = Color.Purple  'WASTE
                            Me.Controls("btnCone" & rw).BackgroundImage = im_LW
                    End Select

                End If

            End If



            If Not HorL = Nothing Then

                If Me.Controls("btnCone" & (rw)).BackgroundImage IsNot Nothing Then

                    'H Full values
                    If Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HDD Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "H_DD"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HD Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "H_D"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HMM Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "H_MM"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HL Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "H_L"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HLL Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "H_LL"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HB Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "H_B"

                        'L Full values
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LDD Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "L_DD"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LD Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "L_D"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LMM Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "L_MM"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LL Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "L_L"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LLL Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "L_LL"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LB Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "L_B"

                        'SMALL H
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HSD Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "HS_D"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HSM Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "HS_M"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HSL Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "HS_L"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HSB Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "HS_B"

                        'Small L
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LSD Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "LS_D"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage.Tag Is im_LMM Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "LS_M"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LSL Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "LS_L"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LSB Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "LS_B"

                        'H and L waste
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_HW Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "H_W"
                    ElseIf Me.Controls("btnCone" & (rw)).BackgroundImage Is im_LW Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL_Res").Value = "L_W"
                    End If
                Else
                    Me.Controls("btnCone" & (rw)).BackgroundImage = Nothing
                End If
            End If












            'new section to Display H and L values if a standard Cart and HHLL


            If lclHHLL = "YES" And tmpCartType = "B" Then

                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value) Then
                    If frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value = "H" Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.H
                        Me.Controls("btnCone" & rw).Enabled = False
                        remaincount = remaincount - 1


                    ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value = "L" Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.L
                        Me.Controls("btnCone" & rw).Enabled = False
                        remaincount = remaincount - 1

                    ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value = "H Std" Then

                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.H_Std

                        Me.Controls("btnCone" & rw).Enabled = False
                        remaincount = remaincount - 1

                    End If

                    'check to see if all cheeses are graded and show finish button if they are
                    If My.Settings.chkUseColour Then
                        If remaincount = 0 Then 'All have values
                            btnFinishedJob.Show()
                        Else
                            btnFinishedJob.Hide()
                        End If
                    End If

                End If
            End If


        Next

        txtBoxRemain.Text = remaincount

        txtBoxUpdates()




    End Sub




    Private Sub btnUnlock_Click(sender As Object, e As EventArgs) Handles btnUnlock.Click

        'SET ALL CONE KEYS TO EDIT
        Unlock()

    End Sub

    Private Sub Unlock()

        If btnUnlock.Text = "UNLOCK OFF" Then
            btnUnlock.Text = "UNLOCK ON"
            btnUnlock.ForeColor = Color.Green

            For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count

                Me.Controls("btnCone" & rw).Enabled = True
                btnDelete.Visible = True

                If My.Settings.chkUseColour Then
                    btnDefect.Visible = True
                    btnShort.Visible = True
                    btnNoCone.Visible = True
                    btnDefect.Enabled = True
                    btnShort.Enabled = True
                    btnNoCone.Enabled = True
                End If
            Next

        ElseIf btnUnlock.Text = "UNLOCK ON" Then
            btnUnlock.Text = "UNLOCK OFF"
            btnUnlock.ForeColor = Color.Red
            btnDelete.Visible = False
            If My.Settings.chkUseColour Then btnDefect.Visible = False
            If My.Settings.chkUseColour Then btnShort.Visible = False
            If My.Settings.chkUseColour Then btnNoCone.Visible = False
        End If


    End Sub


    Private Sub btnMeasure_Click(sender As Object, e As EventArgs)
        If varConeNum > 0 Then

            measureOn = 1

            If VeriColorCom.IsOpen = False Then
                VeriColorCom.Open()
            End If

            VeriColorCom.WriteLine("ma")
            VeriColorCom.WriteLine("01gr")

            frmDelay.Show()




            MeaDisplay()



            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnSave.Enabled = True
            'If frmSettings.chkUseSpectro.Checked Then Me.btnReMeasure.Visible = True  'Show Cancel button when form opens
            Me.btnVisGrade.Enabled = False



            incoming = ""  'Clear out existing data in incoming String


        Else

            MsgBox("You must select a Cheese number first")
        End If



    End Sub


    Private Sub MeaDisplay()                                    'Display measure results

        MsgBox("In Measure Section")

        incoming = incoming.TrimStart(removeChar)
        incoming = incoming.TrimEnd(removeChar)

        Dim dC As String = ""
        Dim Blue As String = ""
        Dim BlueGreen As String
        Dim Green As String = ""
        Dim GreenYellow As String = ""
        Dim Yellow As String = ""
        Dim YellowRed As String = ""
        Dim Red As String = ""
        Dim RedBlue As String = ""

        Dim strArray() As String
        Dim intCount As Integer


        strArray = Split(incoming, ",")

        For intCount = 0 To UBound(strArray)
            dC = strArray(0)
            Blue = strArray(1)
            BlueGreen = strArray(2)
            Green = strArray(3)
            GreenYellow = strArray(4)
            Yellow = strArray(5)
            YellowRed = strArray(6)
            Red = strArray(7)
            strArray(8) = strArray(8).TrimEnd(removeChar)
            RedBlue = strArray(8) / 100
        Next



        ' txtResult.Text = dC / 100  'Display dC value with rescale
        'Blue = Blue * 2.55 
        'Green = Green * 2.55 
        'Red = Red * 2.55 

        ' Color Maths
        Dim var_R, var_G, var_B As String
        Dim var_X, var_Y, var_Z As String
        Dim X, Y, Z As String
        Dim CIEbat_L As String = ""
        Dim CIEbat_a As String = ""
        Dim CIEbat_b As String = ""
        Dim CIEdelta_L As String = ""
        Dim CIEdelta_E As String = ""
        Dim CIEstd_L As String = ""
        Dim CIEstd_a As String = ""
        Dim CIEstd_b As String = ""

        CIEstd_L = 38.87
        CIEstd_a = -7.11
        CIEstd_b = -37.26

        var_R = (Red / 255) ' R from 0 To 255
        var_G = (Green / 255) ' G from 0 To 255
        var_B = (Blue / 255) ' B from 0 To 255



        If (var_R > 0.04045) Then var_R = ((var_R + 0.055) / 1.055) ^ 2.4 Else var_R = var_R / 12.92
        If (var_G > 0.04045) Then var_G = ((var_G + 0.055) / 1.055) ^ 2.4 Else var_G = var_G / 12.92
        If (var_B > 0.04045) Then var_B = ((var_B + 0.055) / 1.055) ^ 2.4 Else var_B = var_B / 12.92

        var_R = var_R * 100
        var_G = var_G * 100
        var_B = var_B * 100

        '//Observer. = 2°, Illuminant = D65
        X = var_R * 0.4124 + var_G * 0.3576 + var_B * 0.1805
        Y = var_R * 0.2126 + var_G * 0.7152 + var_B * 0.0722
        Z = var_R * 0.0193 + var_G * 0.1192 + var_B * 0.9505


        'xyzToCIELab()

        var_X = X / 94.811   'ref_X = 95.047   Observer= 2°, Illuminant= D65    10° 94.811
        var_Y = Y / 100.0    'ref_Y = 100.0                                         100
        var_Z = Z / 107.304 'ref_Z = 108.883                                       107.304

        If (var_X > 0.008856) Then var_X = var_X ^ (1 / 3) Else var_X = (7.787 * var_X) + (16 / 116)
        If (var_Y > 0.008856) Then var_Y = var_Y ^ (1 / 3) Else var_Y = (7.787 * var_Y) + (16 / 116)
        If (var_Z > 0.008856) Then var_Z = var_Z ^ (1 / 3) Else var_Z = (7.787 * var_Z) + (16 / 116)

        CIEbat_L = (116 * var_Y) - 16
        CIEbat_a = 500 * (var_X - var_Y)
        CIEbat_b = 200 * (var_Y - var_Z)


        ' Delta CIE L
        CIEdelta_L = CIEbat_L - CIEstd_L                   'reversed as Toray take std away from batch so if batch is lighter result is negative

        'CIE Delta E
        CIEdelta_E = Math.Sqrt(((CIEstd_L - CIEbat_L) ^ 2) + ((CIEstd_a - CIEbat_a) ^ 2) + ((CIEstd_b - CIEbat_b) ^ 2))

        Blue = Blue / 100
        Green = Green / 100
        Red = Red / 100

        'ReScale outputs
        'CIEbat_L = CIEbat_L
        'CIEbat_a = CIEbat_a
        'CIEbat_b = CIEbat_b
        'CIEdelta_L = CIEdelta_L
        'CIEdelta_E = CIEdelta_E


        'to Display sample colour conver strings to Integers
        Dim RedI As Integer = CInt(Red)
        Dim GreenI As Integer = CInt(Green)
        Dim BlueI As Integer = CInt(Blue)





        'btnSampleColour.BackColor = Color.FromArgb(RedI, GreenI, BlueI)  'takes measuerd value in RGB and displays color sample

    End Sub

    Private Sub btnNoCone_Click(sender As Object, e As EventArgs) Handles btnNoCone.Click
        If varConeNum > 0 Then

            NoCone = 1


            Me.btnVisGrade.Enabled = False
            Me.btnShort.Enabled = False
            Me.btnNoCone.Enabled = False
            Me.btnDefect.Enabled = False
            Me.btnStdCheese.Enabled = False
            Me.btnDefectSave.Visible = True
            Me.btnClear.Visible = True
            shortC(varConeNum - coneNumOffset) = 0
            defect = 0
            StdCone = Nothing
            stdStateVar = Nothing
            shortCone = 0
            coneBarley = 0
            coneWaste = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0

            Me.chk_K.Visible = False
            Me.chk_D.Visible = False
            Me.chk_F.Visible = False
            Me.chk_O.Visible = False
            Me.chk_T.Visible = False
            Me.chk_P.Visible = False
            Me.chk_S.Visible = False
            Me.chk_X.Visible = False
            Me.chk_N.Visible = False
            Me.chk_W.Visible = False
            Me.chk_H.Visible = False
            Me.chk_TR.Visible = False
            Me.chk_B.Visible = False
            Me.chk_C.Visible = False
            'SORT Dept FAULTS
            Me.chk_DO.Visible = False
            Me.chk_DH.Visible = False
            Me.chk_CL.Visible = False
            Me.chk_FI.Visible = False
            Me.chk_YN.Visible = False
            Me.chk_HT.Visible = False
            Me.chk_LT.Visible = False
            StdCone = Nothing
            stdStateVar = Nothing
        Else
            MsgBox("You must select a Cheese number first")
        End If

    End Sub

    Private Sub btnDefect_Click(sender As Object, e As EventArgs) Handles btnDefect.Click
        If varConeNum > 0 Then
            defect = 1
            Me.btnDefect.BackColor = Color.Yellow
            Me.btnDefectSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens
            Me.btnStdCheese.Enabled = False
            Me.btnDefect.Enabled = False

            Me.btnVisGrade.Enabled = False
            Me.btnNoCone.Visible = False
            Me.btnShort.Visible = False
            Me.btnNoCone.Enabled = False
            Me.btnShort.Enabled = False
            'shortC(varConeNum - coneNumOffset) = 0
            'FAULTS FROM POY-DTY Dept
            Me.chk_K.Visible = True
            Me.chk_D.Visible = True
            Me.chk_F.Visible = True
            Me.chk_O.Visible = True
            Me.chk_T.Visible = True
            Me.chk_P.Visible = True
            Me.chk_S.Visible = False
            Me.chk_X.Visible = False
            Me.chk_N.Visible = True
            Me.chk_W.Visible = True
            Me.chk_H.Visible = True
            Me.chk_TR.Visible = True
            Me.chk_B.Visible = True
            Me.chk_C.Visible = True

            'ONLY SHOW IF COLOUR SORT CHECK ACTIVE
            'If My.Settings.chkUseColour Then
            Me.chk_DO.Visible = True
            Me.chk_DH.Visible = True
            Me.chk_CL.Visible = True
            Me.chk_FI.Visible = True
            Me.chk_YN.Visible = True
            Me.chk_HT.Visible = True
            Me.chk_LT.Visible = True
            'End If

            Dim fltDrow = (varConeNum - coneNumOffset) - 1

            'THIS WILL CALL BACK THE FAULT DATA FROM THE DATAGRID

            chk_K.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_K").Value.ToString
            chk_D.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_D").Value.ToString
            chk_F.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_F").Value.ToString
            chk_O.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_O").Value.ToString
            chk_T.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_T").Value.ToString
            chk_P.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_P").Value.ToString
            'chk_S.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_S").Value.ToString
            'chk_X.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_X").Value.ToString
            chk_N.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_N").Value.ToString
            chk_W.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_W").Value.ToString
            chk_H.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_H").Value.ToString
            chk_TR.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_TR").Value.ToString
            chk_B.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_B").Value.ToString
            chk_C.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_C").Value.ToString

            chk_DO.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_DO").Value.ToString
            chk_DH.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_DH").Value.ToString
            chk_CL.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_CL").Value.ToString
            chk_FI.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_FI").Value.ToString
            chk_YN.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_YN").Value.ToString
            chk_HT.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_HT").Value.ToString
            chk_LT.Checked = frmDGV.DGVdata.Rows(fltDrow).Cells("FLT_LT").Value.ToString
            StdCone = Nothing
            stdStateVar = Nothing
            'End If

            Me.btnDefectSave.Visible = True 'Show Save button when form opens


        Else
            MsgBox("You must select a Cheese number first")
        End If


    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If varConeNum > 0 Then



            Dim result = MessageBox.Show("ERASE Information for Cheese #" & varConeNum, "ERASE Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

            If result = DialogResult.Yes Then
                Dim result2 = MessageBox.Show("ARE YOU SURE YOU WANT TO ERASE Cheese #" & varConeNum & "  INFORMATION", "CONFIRM ERASE", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If result2 = DialogResult.Yes Then
                    'ERASE CONE VALUES

                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("SHORTCONE").Value = 0  'shortCone
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("MISSCONE").Value = 0 'missingCone
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("DEFCONE").Value = 0 'defectCone
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("CONEZERO").Value = 0 'passCone  Zero Colour Difference    
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("CONEBARLEY").Value = 0 'Cone with large colour defect
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("M10").Value = 0   'coneM10
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("P10").Value = 0   'coneP10
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("M30").Value = 0   'coneM30
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("P30").Value = 0  'coneP30
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("M50").Value = 0   'coneM50
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("P50").Value = 0  'coneP50


                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_K").Value = "False"   'KEBA Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_D").Value = "False"  'DIRTY Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_F").Value = "False"     'FORM AB Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_O").Value = "False"     'OVERTHROWN Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_T").Value = "False"    'TENSION AB. Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_P").Value = "False"    'PAPER TUBE AB. Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_S").Value = "False"          'SHORT CHEESE Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_X").Value = "False"          'No HAVE CHEESE Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_N").Value = "False"     'NO TAIL & ABNORMAL Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_W").Value = "False"   'WASTE Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_H").Value = "False"    'HITTING Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_TR").Value = "False"    'TARUMI Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_B").Value = "False"    'B- GRADE BY M/C  Fault   TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_C").Value = "False"    'C- GRADE BY M/C  Fault  TODO
                    'SORT Dept FAULTS
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_DO").Value = "False"    'DO- GRADE BY M/C  Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_DH").Value = "False"   'DH- GRADE BY M/C  Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_CL").Value = "False"     'CL- GRADE BY M/C  Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_FI").Value = "False"     'FI- GRADE BY M/C  Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_YN").Value = "False"     'YN- GRADE BY M/C  Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_HT").Value = "False"    'HT- GRADE BY M/C  Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_LT").Value = "False"     'LT- GRADE BY M/C  Fault  TODO
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("DYEFLECK").Value = 0
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("COLDEF").Value = 0
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("COLWASTE").Value = 0
                    StdCone = Nothing
                    stdStateVar = Nothing
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("STDCHEESE").Value = Nothing 'stdcheese
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("STDSTATE").Value = Nothing
                    shortC(varConeNum - coneNumOffset) = 0
                    Me.Controls("btnCone" & varConeNum - coneNumOffset).BackColor = SystemColors.ControlDark
                    Me.Controls("btnCone" & varConeNum - coneNumOffset).BackgroundImage = Nothing

                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("HHLL").Value = Nothing
                    frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("HHLL_Res").Value = Nothing

                    UpdateConeVal()

                    Dim department As String

                    If My.Settings.chkUseSort Then department = "SORTING"
                    If My.Settings.chkUseColour Then department = "COLOUR"
                    If My.Settings.chkUsePack Then department = "PACKING"


                    Dim message As String = "Operator " & frmJobEntry.varUserName & " Department " & department & vbCrLf & "Deleted Cheese Number " & (varConeNum) & " From Job " & frmJobEntry.txtLotNumber.Text
                    MsgBox(message)

                    'Write error to Log File
                    writeerrorLog.writelog("CHESSE DELETED", message, False, "Operator Overide")


                    varConeNum = 0
                    txtConeNum.Text = ""



                    btnUnlock.Text = "UNLOCK OFF"
                    btnUnlock.ForeColor = Color.Red
                    btnDelete.Visible = False

                    Exit Sub
                End If

                If result2 = DialogResult.No Then
                    varConeNum = 0
                    txtConeNum.Text = ""
                    Exit Sub
                End If
            End If

            If result = DialogResult.No Then
                varConeNum = 0
                txtConeNum.Text = ""
                Exit Sub
            End If
        Else
            MsgBox("You must select a Cheese number first")
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        If varConeNum > 0 Then


            If varVisConeInspect = 1 Then
                NoCone = 0
                defect = 0
                StdCone = Nothing
                stdStateVar = Nothing
                shortCone = 0
                varConeNum = 0
                txtConeNum.Text = ""
                coneBarley = 0
                coneWaste = 0
                coneZero = 0
                coneM10 = 0
                coneP10 = 0
                coneM30 = 0
                coneP30 = 0
                coneM50 = 0
                coneP50 = 0

                valHH = 0
                valLL = 0
                valHStd = 0


                Me.btnVisGrade.Enabled = True
                Me.btnShort.Enabled = False
                Me.btnNoCone.Enabled = False
                Me.btnDefect.Enabled = False
                Me.btnStdCheese.Enabled = False
                Me.btnSave.Visible = False
                Me.btnDefectSave.Visible = False
                Me.btnClear.Visible = False
                Me.chk_K.Visible = False
                Me.chk_D.Visible = False
                Me.chk_F.Visible = False
                Me.chk_O.Visible = False
                Me.chk_T.Visible = False
                Me.chk_P.Visible = False
                Me.chk_S.Visible = False
                Me.chk_X.Visible = False
                Me.chk_N.Visible = False
                Me.chk_W.Visible = False
                Me.chk_H.Visible = False
                Me.chk_TR.Visible = False
                Me.chk_B.Visible = False
                Me.chk_C.Visible = False
                'SORT Dept FAULTS
                Me.chk_DO.Visible = False
                Me.chk_DH.Visible = False
                Me.chk_CL.Visible = False
                Me.chk_FI.Visible = False
                Me.chk_YN.Visible = False
                Me.chk_HT.Visible = False
                Me.chk_LT.Visible = False
            Else
                NoCone = 0
                defect = 0
                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("STDCHEESE").Value = Nothing 'stdcheese
                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("STDSTATE").Value = Nothing
                StdCone = Nothing
                stdStateVar = Nothing
                shortCone = 0
                varConeNum = 0
                txtConeNum.Text = ""
                coneBarley = 0
                coneWaste = 0
                coneZero = 0
                coneM10 = 0
                coneP10 = 0
                coneM30 = 0
                coneP30 = 0
                coneM50 = 0
                coneP50 = 0
                Me.btnVisGrade.Enabled = True
                Me.btnShort.Visible = True
                Me.btnShort.Enabled = True
                Me.btnStdCheese.Enabled = True
                Me.btnNoCone.Visible = True
                Me.btnNoCone.Enabled = True
                Me.btnDefect.Enabled = True
                Me.btnSave.Visible = False
                Me.btnDefectSave.Visible = False
                Me.btnClear.Visible = False
                Me.chk_K.Visible = False
                Me.chk_D.Visible = False
                Me.chk_F.Visible = False
                Me.chk_O.Visible = False
                Me.chk_T.Visible = False
                Me.chk_P.Visible = False
                Me.chk_S.Visible = False
                Me.chk_X.Visible = False
                Me.chk_N.Visible = False
                Me.chk_W.Visible = False
                Me.chk_H.Visible = False
                Me.chk_TR.Visible = False
                Me.chk_B.Visible = False
                Me.chk_C.Visible = False
                'SORT Dept FAULTS
                Me.chk_DO.Visible = False
                Me.chk_DH.Visible = False
                Me.chk_CL.Visible = False
                Me.chk_FI.Visible = False
                Me.chk_YN.Visible = False
                Me.chk_HT.Visible = False
                Me.chk_LT.Visible = False

            End If
        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub

    Private Sub btnShort_Click(sender As Object, e As EventArgs) Handles btnShort.Click
        If varConeNum > 0 Then
            'If My.Settings.chkUseColour Then
            '    shortCone = 2
            'Else
            shortCone = 1
            'End If


            Me.btnDefectSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens
            Me.btnDefect.Enabled = False

            Me.btnVisGrade.Enabled = False
            Me.btnStdCheese.Enabled = False
            Me.btnNoCone.Enabled = False
            Me.btnShort.Enabled = False
            StdCone = Nothing
            stdStateVar = Nothing
        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub

    Private Sub btnVisGrade_Click(sender As Object, e As EventArgs) Handles btnVisGrade.Click

        If btnVisGrade.Text = "Visual Grade OFF" Then
            btnVisGrade.Text = "Visual Grade ON"
            btnBarley.Enabled = True
            btnZero.Enabled = True
            btnM10.Enabled = True
            btnM30.Enabled = True
            btnM50.Enabled = True
            btnP10.Enabled = True
            btnP30.Enabled = True
            btnP50.Enabled = True

            varVisConeInspect = 1
            coneBarley = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0


            Me.btnDefect.Enabled = False
            Me.btnNoCone.Enabled = False
            Me.btnShort.Enabled = False

        ElseIf btnVisGrade.Text = "Visual Grade ON" Then
            btnVisGrade.Text = "Visual Grade OFF"
            btnBarley.Enabled = False
            btnZero.Enabled = False
            btnM10.Enabled = False
            btnM30.Enabled = False
            btnM50.Enabled = False
            btnP10.Enabled = False
            btnP30.Enabled = False
            btnP50.Enabled = False

            varVisConeInspect = 0
            coneBarley = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0


            Me.btnDefect.Enabled = True
            Me.btnNoCone.Enabled = True
            Me.btnShort.Enabled = True

        End If




    End Sub

    Private Sub btnM10_Click(sender As Object, e As EventArgs) Handles btnM10.Click

        If varConeNum > 0 Then
            coneBarley = 0
            coneZero = 0
            coneM10 = 1
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0
            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens
            'btnCone10.BackColor = Color.
        Else
            MsgBox("You must select a  Cheese number first")
        End If

    End Sub

    Private Sub btnBarley_Click(sender As Object, e As EventArgs) Handles btnBarley.Click
        If varConeNum > 0 Then
            coneBarley = 1
            coneWaste = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0
            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub

    Private Sub btnZero_Click(sender As Object, e As EventArgs) Handles btnZero.Click
        If varConeNum > 0 Then
            coneBarley = 0
            coneWaste = 0
            coneZero = 1
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0
            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub



    Private Sub btnP10_Click(sender As Object, e As EventArgs) Handles btnP10.Click
        If varConeNum > 0 Then
            coneBarley = 0
            coneWaste = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 1
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0
            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub

    Private Sub btnM30_Click(sender As Object, e As EventArgs) Handles btnM30.Click
        If varConeNum > 0 Then
            coneBarley = 0
            coneWaste = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 1
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0
            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub

    Private Sub btnP30_Click(sender As Object, e As EventArgs) Handles btnP30.Click
        If varConeNum > 0 Then
            coneBarley = 0
            coneWaste = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 1
            coneM50 = 0
            coneP50 = 0
            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub

    Private Sub btnM50_Click(sender As Object, e As EventArgs) Handles btnM50.Click
        If varConeNum > 0 Then
            coneBarley = 0
            coneWaste = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 1
            coneP50 = 0
            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub

    Private Sub btnP50_Click(sender As Object, e As EventArgs) Handles btnP50.Click
        If varConeNum > 0 Then
            coneBarley = 0
            coneWaste = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 1
            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub


    Private Sub btnHH_Click(sender As Object, e As EventArgs) Handles btnHH.Click
        If varConeNum > 0 Then

            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

            valHH = 1
            valLL = 0
            valHStd = 0

        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub

    Private Sub btnLL_Click(sender As Object, e As EventArgs) Handles btnLL.Click
        If varConeNum > 0 Then


            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

            valHH = 0
            valLL = 1
            valHStd = 0


        Else
            MsgBox("You must select a  Cheese number first")
        End If
    End Sub

    Private Sub btnALL_H_Click(sender As Object, e As EventArgs) Handles btnALL_H.Click


        If Not remaincount = "0" Then
            valHH = 1
            valLL = 0
            valHStd = 0
            setAllCheese()
        Else
            MsgBox("All cheeses alreay have a value so this option cannot be used")
        End If

    End Sub

    Private Sub btnAll_L_Click(sender As Object, e As EventArgs) Handles btnAll_L.Click

        If Not remaincount = "0" Then
            valHH = 0
            valLL = 1
            valHStd = 0
            setAllCheese()
        Else
            MsgBox("All cheeses alreay have a value so this option cannot be used")
        End If


    End Sub

    Private Sub setAllCheese()

        Dim HHFound As Integer
        Dim llFound As Integer


        'Process set H values
        If valHH = 1 Then
            For rw = 1 To frmDGV.DGVdata.Rows.Count
                'Chek to see if an H values already set

                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value) Then
                    If frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value.ToString = "H" Then
                        HHFound = 1
                    End If
                End If
            Next

            If HHFound = 1 Then
                Dim result = MessageBox.Show("H values already on cart are you sure you wish to remaining Cheeses to H ", "YES or NO", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                If result = DialogResult.Yes Then
                    'just contine to next part
                End If

                If result = DialogResult.No Then
                    valHH = 0   '280372102001B3
                    Exit Sub
                End If
            End If

            For rw = 1 To frmDGV.DGVdata.Rows.Count
                'Chek to see if an H values already set
                If IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value) Then
                    frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value = "H"
                End If
            Next

            UpdateConeVal()
            ' Me.btnSave.Visible = True 'Show Save button when form opens

        End If


        'Process set HLvalues
        If valLL = 1 Then
            For rw = 1 To frmDGV.DGVdata.Rows.Count
                'Chek to see if an H values already set

                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value) Then
                    If frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value.ToString = "L" Then
                        HHFound = 1
                    End If
                End If
            Next

            If HHFound = 1 Then
                Dim result = MessageBox.Show("L values already on cart are you sure you wish to remaining Cheeses to L ", "YES or NO", MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                If result = DialogResult.Yes Then
                    'just contine to next part
                End If

                If result = DialogResult.No Then
                    valHH = 0   '280372102001B3
                    Exit Sub
                End If
            End If

            For rw = 1 To frmDGV.DGVdata.Rows.Count
                'Chek to see if an H values already set
                If IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value) Then
                    frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value = "L"
                End If
            Next

            UpdateConeVal()
            ' Me.btnSave.Visible = True 'Show Save button when form opens

        End If

    End Sub



    Private Sub btnCone1_Click(sender As Object, e As EventArgs) Handles btnCone1.Click
        varConeNum = btnCone1.Text
        txtConeNum.Text = varConeNum

    End Sub

    Private Sub btnCone2_Click(sender As Object, e As EventArgs) Handles btnCone2.Click
        varConeNum = btnCone2.Text                 'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone3_Click(sender As Object, e As EventArgs) Handles btnCone3.Click
        varConeNum = btnCone3.Text                  'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone4_Click(sender As Object, e As EventArgs) Handles btnCone4.Click
        varConeNum = btnCone4.Text                   'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone5_Click(sender As Object, e As EventArgs) Handles btnCone5.Click
        varConeNum = btnCone5.Text                  'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone6_Click(sender As Object, e As EventArgs) Handles btnCone6.Click
        varConeNum = btnCone6.Text                   'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone7_Click(sender As Object, e As EventArgs) Handles btnCone7.Click
        varConeNum = btnCone7.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone8_Click(sender As Object, e As EventArgs) Handles btnCone8.Click
        varConeNum = btnCone8.Text                   'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone9_Click(sender As Object, e As EventArgs) Handles btnCone9.Click
        varConeNum = btnCone9.Text                   'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone10_Click(sender As Object, e As EventArgs) Handles btnCone10.Click
        varConeNum = btnCone10.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnWaste_Click(sender As Object, e As EventArgs) Handles btnWaste.Click
        If varConeNum > 0 Then
            coneWaste = 1
            coneBarley = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0
            Me.btnSave.Visible = True 'Show Save button when form opens
            Me.btnClear.Visible = True  'Show Cancel button when form opens

        Else
            MsgBox("You must select a Cheese number first")
        End If
    End Sub

    Private Sub btnCone11_Click(sender As Object, e As EventArgs) Handles btnCone11.Click
        varConeNum = btnCone11.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone12_Click(sender As Object, e As EventArgs) Handles btnCone12.Click
        varConeNum = btnCone12.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone13_Click(sender As Object, e As EventArgs) Handles btnCone13.Click
        varConeNum = btnCone13.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone14_Click(sender As Object, e As EventArgs) Handles btnCone14.Click
        varConeNum = btnCone14.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone15_Click(sender As Object, e As EventArgs) Handles btnCone15.Click
        varConeNum = btnCone15.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone16_Click(sender As Object, e As EventArgs) Handles btnCone16.Click
        varConeNum = btnCone16.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone17_Click(sender As Object, e As EventArgs) Handles btnCone17.Click
        varConeNum = btnCone17.Text                  'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone18_Click(sender As Object, e As EventArgs) Handles btnCone18.Click
        varConeNum = btnCone18.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone19_Click(sender As Object, e As EventArgs) Handles btnCone19.Click
        varConeNum = btnCone19.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone20_Click(sender As Object, e As EventArgs) Handles btnCone20.Click
        varConeNum = btnCone20.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone21_Click(sender As Object, e As EventArgs) Handles btnCone21.Click
        varConeNum = btnCone21.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone22_Click(sender As Object, e As EventArgs) Handles btnCone22.Click
        varConeNum = btnCone22.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone23_Click(sender As Object, e As EventArgs) Handles btnCone23.Click
        varConeNum = btnCone23.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone24_Click(sender As Object, e As EventArgs) Handles btnCone24.Click
        varConeNum = btnCone24.Text                   'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone25_Click(sender As Object, e As EventArgs) Handles btnCone25.Click
        varConeNum = btnCone25.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone26_Click(sender As Object, e As EventArgs) Handles btnCone26.Click
        varConeNum = btnCone26.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone27_Click(sender As Object, e As EventArgs) Handles btnCone27.Click
        varConeNum = btnCone27.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone28_Click(sender As Object, e As EventArgs) Handles btnCone28.Click
        varConeNum = btnCone28.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone29_Click(sender As Object, e As EventArgs) Handles btnCone29.Click
        varConeNum = btnCone29.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone30_Click(sender As Object, e As EventArgs) Handles btnCone30.Click
        varConeNum = btnCone30.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone31_Click(sender As Object, e As EventArgs) Handles btnCone31.Click
        varConeNum = btnCone31.Text                    'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub

    Private Sub btnCone32_Click(sender As Object, e As EventArgs) Handles btnCone32.Click
        varConeNum = btnCone32.Text                     'Sets the cone Number
        txtConeNum.Text = varConeNum

        Me.txtConeNum.Refresh()
    End Sub





    Private Sub btnNoConeSave_Click(sender As Object, e As EventArgs)


        readsave()
    End Sub

    Private Sub btnDefectSave_Click(sender As Object, e As EventArgs) Handles btnDefectSave.Click

        Me.chk_K.Visible = False
        Me.chk_D.Visible = False
        Me.chk_F.Visible = False
        Me.chk_O.Visible = False
        Me.chk_T.Visible = False
        Me.chk_P.Visible = False
        Me.chk_S.Visible = False
        Me.chk_X.Visible = False
        Me.chk_N.Visible = False
        Me.chk_W.Visible = False
        Me.chk_H.Visible = False
        Me.chk_TR.Visible = False
        Me.chk_B.Visible = False
        Me.chk_C.Visible = False
        'SORT Dept FAULTS
        Me.chk_DO.Visible = False
        Me.chk_DH.Visible = False
        Me.chk_CL.Visible = False
        Me.chk_FI.Visible = False
        Me.chk_YN.Visible = False
        Me.chk_HT.Visible = False
        Me.chk_LT.Visible = False


        If btnUnlock.Text = "UNLOCK ON" Then
            btnUnlock.Text = "UNLOCK OFF"
            btnUnlock.ForeColor = Color.Red
            btnDelete.Visible = False

        End If

        readsave()
    End Sub

    Private Sub btnShortSave_Click(sender As Object, e As EventArgs)


        readsave()

    End Sub



    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click


        readsave()

    End Sub

    Private Sub btnFinishedJob_Click(sender As Object, e As EventArgs) Handles btnFinishedJob.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        endJob()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub btnNextJob_Click(sender As Object, e As EventArgs) Handles btnNextJob.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        endJob()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub endJob()

        'UPDATE DATABASE WITH CHANGES



        'ONLY PRINT IF COLOUR SELECTED
        Dim today As String = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")
        ' today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")


        Dim cellVal As String
        Dim oldval As String
        Dim newvalue As String

        For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count

            If My.Settings.chkUseColour Then

                For colcount As Integer = 10 To 22

                    cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells(colcount).Value




                    If IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("STDCHEESE").Value) Then

                        Select Case colcount
                            Case 10, 15
                                If cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value < 14 Then
                                    oldval = frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value
                                    frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value = "9"
                                    newvalue = frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value
                                    'MsgBox("Cone Num = " & cellVal & "  Old State = " & oldval & "  New State = " & newvalue)
                                End If
                            Case 11, 12, 16, 17, 18, 19, 20, 21, 22
                                If cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value < 14 Then
                                    oldval = frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value
                                    frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value = "8"
                                    newvalue = frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value
                                    'MsgBox("Cone Num = " & cellVal & "  Old State = " & oldval & "  New State = " & newvalue & "Col Num = " & colcount)
                                End If

                        End Select
                    End If

                    If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("STDCHEESE").Value) Then
                        If frmDGV.DGVdata.Rows(rw - 1).Cells("STDCHEESE").Value = "0" Or frmDGV.DGVdata.Rows(rw - 1).Cells("STDCHEESE").Value = Nothing _
                            Or frmDGV.DGVdata.Rows(rw - 1).Cells("STDSTATE").Value = "10" Then
                            Select Case colcount
                                Case 10, 15
                                    If cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value < 14 Then
                                        oldval = frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value
                                        frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value = "9"
                                        newvalue = frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value
                                    End If
                                Case 11, 12, 16, 17, 18, 19, 20, 21, 22
                                    If cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value < 14 Then
                                        oldval = frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value
                                        frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value = "8"
                                        newvalue = frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value
                                    End If
                            End Select
                        End If
                    End If
                Next

                cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells("COLWASTE").Value.ToString          'SET CONE STATE IF WASTE CONE TO 8
                If cellVal > 0 Then frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value = "8"
                cellVal = 0

            End If

        Next

        For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count

            If My.Settings.chkUseSort Then
                If frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value = "0" Then
                    frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value = "5"
                    frmDGV.DGVdata.Rows(rw - 1).Cells("CARTSTARTTM").Value = today
                    frmDGV.DGVdata.Rows(rw - 1).Cells("CARTENDTM").Value = today
                End If
            End If

            If My.Settings.chkUseColour And frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value.ToString IsNot "8" Then
                If frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value < 14 Then frmDGV.DGVdata.Rows(rw - 1).Cells("CONESTATE").Value = "9"  'No Faults recorded so set to 9 Unless already Packed then do not change state
            End If

            If My.Settings.chkUseColour Then
                frmDGV.DGVdata.Rows(rw - 1).Cells("OPCOLOUR").Value = frmJobEntry.ColorOP
                frmDGV.DGVdata.Rows(rw - 1).Cells("CARTENDTM").Value = today
                If IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("COLENDTM").Value) Then
                    frmDGV.DGVdata.Rows(rw - 1).Cells("COLENDTM").Value = today 'COLOUR CHECK END TIME
                End If

                If Not HorL = Nothing Then
                    frmDGV.DGVdata.Rows(rw - 1).Cells("HHLLState").Value = 3
                End If


            ElseIf My.Settings.chkUseSort Then
                frmDGV.DGVdata.Rows(rw - 1).Cells("OPSORT").Value = frmJobEntry.SortOP
                frmDGV.DGVdata.Rows(rw - 1).Cells("CARTENDTM").Value = today
                If IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("SORTENDTM").Value) Then
                    frmDGV.DGVdata.Rows(rw - 1).Cells("SORTENDTM").Value = today 'SORT END TIME
                End If
            End If

        Next



        'CART LOG File entry
        Dim cartMessage As String


        Dim tmpPrdRpt As String = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value.ToString
        Dim tmpMerRpt As String = frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value.ToString

        cartMessage = rptTime & "  To  " & today &
            " Operator = " & frmJobEntry.varUserName &
            " Cart Barcode = " & frmJobEntry.dbBarcode &
            " Product = " & tmpPrdRpt & " " & tmpMerRpt &
            "  Doff # " & frmJobEntry.varDoffingNum

        writeerrorLog.writelog("Cart Opened for edit ", cartMessage, False, "Cart Edit Opened")


        If Not frmJobEntry.HLColChk = Nothing Then
            printSheet()
        End If

        UpdateDatabase()



        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        'frmDGV.DGVdata.DataSource = Nothing
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Close()

    End Sub



    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        'frmDGV.DGVdata.ClearSelection()
        frmDGV.DGVdata.DataSource = Nothing
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Close()
    End Sub

    Private Sub readsave()

        btnFinishedJob.Visible = True  'Turn on the finish button if any changes

        'MEASUERD CONE Set the color of Measuerd button if Spectro used
        If measureOn = 1 Then
            If varConeNum - coneNumOffset = 1 Then
                btnCone1.Enabled = False
                btnCone1.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 2 Then
                btnCone2.Enabled = False
                btnCone2.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 3 Then
                btnCone3.Enabled = False
                btnCone3.BackColor = Color.Green
                measureOn = varConeNum

            ElseIf varConeNum - coneNumOffset = 4 Then
                btnCone4.Enabled = False
                btnCone4.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 5 Then
                btnCone5.Enabled = False
                btnCone5.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 6 Then
                btnCone6.Enabled = False
                btnCone6.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 7 Then
                btnCone7.Enabled = False
                btnCone7.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 8 Then
                btnCone8.Enabled = False
                btnCone8.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 9 Then
                btnCone9.Enabled = False
                btnCone9.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 10 Then
                btnCone10.Enabled = False
                btnCone10.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 11 Then
                btnCone11.Enabled = False
                btnCone11.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 12 Then
                btnCone12.Enabled = False
                btnCone12.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 13 Then
                btnCone13.Enabled = False
                btnCone13.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 14 Then
                btnCone14.Enabled = False
                btnCone14.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 15 Then
                btnCone15.Enabled = False
                btnCone15.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 16 Then
                btnCone16.Enabled = False
                btnCone16.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 17 Then
                btnCone17.Enabled = False
                btnCone17.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 18 Then
                btnCone18.Enabled = False
                btnCone18.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 19 Then
                btnCone19.Enabled = False
                btnCone19.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 20 Then
                btnCone20.Enabled = False
                btnCone20.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 21 Then
                btnCone21.Enabled = False
                btnCone21.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 22 Then
                btnCone22.Enabled = False
                btnCone22.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 23 Then
                btnCone23.Enabled = False
                btnCone23.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 24 Then
                btnCone24.Enabled = False
                btnCone24.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 25 Then
                btnCone25.Enabled = False
                btnCone25.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 26 Then
                btnCone26.Enabled = False
                btnCone26.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 27 Then
                btnCone27.Enabled = False
                btnCone27.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 28 Then
                btnCone28.Enabled = False
                btnCone28.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 29 Then
                btnCone29.Enabled = False
                btnCone29.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 30 Then
                btnCone30.Enabled = False
                btnCone30.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 31 Then
                btnCone31.Enabled = False
                btnCone31.BackColor = Color.Green
                measureOn = varConeNum
            ElseIf varConeNum - coneNumOffset = 32 Then
                btnCone32.Enabled = False
                btnCone32.BackColor = Color.Green
                measureOn = varConeNum
            End If
        End If





        'NO CONE Update Cone button to colour of NoCone And add the cone number to the coneMissingID string so we have a full list of missing cones
        If NoCone Then



            Fault_X = True  'Sets the nocone fault flag



            If varConeNum - coneNumOffset = 1 Then
                btnCone1.Enabled = False
                btnCone1.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 2 Then
                btnCone2.Enabled = False
                btnCone2.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 3 Then
                btnCone3.Enabled = False
                btnCone3.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 4 Then
                btnCone4.Enabled = False
                btnCone4.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 5 Then
                btnCone5.Enabled = False
                btnCone5.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 6 Then
                btnCone6.Enabled = False
                btnCone6.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 7 Then
                btnCone7.Enabled = False
                btnCone7.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 8 Then
                btnCone8.Enabled = False
                btnCone8.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 9 Then
                btnCone9.Enabled = False
                btnCone9.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 10 Then
                btnCone10.Enabled = False
                btnCone10.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 11 Then
                btnCone11.Enabled = False
                btnCone11.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 12 Then
                btnCone12.Enabled = False
                btnCone12.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 13 Then
                btnCone13.Enabled = False
                btnCone13.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 14 Then
                btnCone14.Enabled = False
                btnCone14.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 15 Then
                btnCone15.Enabled = False
                btnCone15.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 16 Then
                btnCone16.Enabled = False
                btnCone16.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 17 Then
                btnCone17.Enabled = False
                btnCone17.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 18 Then
                btnCone18.Enabled = False
                btnCone18.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 19 Then
                btnCone19.Enabled = False
                btnCone19.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 20 Then
                btnCone20.Enabled = False
                btnCone20.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 21 Then
                btnCone21.Enabled = False
                btnCone21.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 22 Then
                btnCone22.Enabled = False
                btnCone22.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 23 Then
                btnCone23.Enabled = False
                btnCone23.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 24 Then
                btnCone24.Enabled = False
                btnCone24.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 25 Then
                btnCone25.Enabled = False
                btnCone25.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 26 Then
                btnCone26.Enabled = False
                btnCone26.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 27 Then
                btnCone27.Enabled = False
                btnCone27.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 28 Then
                btnCone28.Enabled = False
                btnCone28.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 29 Then
                btnCone29.Enabled = False
                btnCone29.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 30 Then
                btnCone30.Enabled = False
                btnCone30.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 31 Then
                btnCone31.Enabled = False
                btnCone31.BackColor = Color.LightPink
                NoCone = varConeNum
            ElseIf varConeNum - coneNumOffset = 32 Then
                btnCone32.Enabled = False
                btnCone32.BackColor = Color.LightPink
                NoCone = varConeNum
            End If

        End If

        'NO CONE Update Cone button to colour of NoCone And add the cone number to the coneMissingID string so we have a full list of missing cones
        If StdCone Then



            stdStateVar = 1  'Sets the stdsate  flag



            If varConeNum - coneNumOffset = 1 Then
                btnCone1.Enabled = False
                btnCone1.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 2 Then
                btnCone2.Enabled = False
                btnCone2.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 3 Then
                btnCone3.Enabled = False
                btnCone3.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 4 Then
                btnCone4.Enabled = False
                btnCone4.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 5 Then
                btnCone5.Enabled = False
                btnCone5.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 6 Then
                btnCone6.Enabled = False
                btnCone6.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 7 Then
                btnCone7.Enabled = False
                btnCone7.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 8 Then
                btnCone8.Enabled = False
                btnCone8.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 9 Then
                btnCone9.Enabled = False
                btnCone9.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 10 Then
                btnCone10.Enabled = False
                btnCone10.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 11 Then
                btnCone11.Enabled = False
                btnCone11.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 12 Then
                btnCone12.Enabled = False
                btnCone12.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 13 Then
                btnCone13.Enabled = False
                btnCone13.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 14 Then
                btnCone14.Enabled = False
                btnCone14.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 15 Then
                btnCone15.Enabled = False
                btnCone15.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 16 Then
                btnCone16.Enabled = False
                btnCone16.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 17 Then
                btnCone17.Enabled = False
                btnCone17.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 18 Then
                btnCone18.Enabled = False
                btnCone18.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 19 Then
                btnCone19.Enabled = False
                btnCone19.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 20 Then
                btnCone20.Enabled = False
                btnCone20.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 21 Then
                btnCone21.Enabled = False
                btnCone21.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 22 Then
                btnCone22.Enabled = False
                btnCone22.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 23 Then
                btnCone23.Enabled = False
                btnCone23.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 24 Then
                btnCone24.Enabled = False
                btnCone24.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 25 Then
                btnCone25.Enabled = False
                btnCone25.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 26 Then
                btnCone26.Enabled = False
                btnCone26.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 27 Then
                btnCone27.Enabled = False
                btnCone27.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 28 Then
                btnCone28.Enabled = False
                btnCone28.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 29 Then
                btnCone29.Enabled = False
                btnCone29.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 30 Then
                btnCone30.Enabled = False
                btnCone30.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 31 Then
                btnCone31.Enabled = False
                btnCone31.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            ElseIf varConeNum - coneNumOffset = 32 Then
                btnCone32.Enabled = False
                btnCone32.BackColor = Color.Orange
                StdCone = varConeNum
                If lclHHLL = "YES" Then
                    valHStd = 1
                End If
            End If

        End If



        If defect Then
            'Routine to Set Cone color to defect and update cone numbers with defects



            If varConeNum - coneNumOffset = 1 Then
                btnCone1.Enabled = False
                btnCone1.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 2 Then
                btnCone2.Enabled = False
                btnCone2.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 3 Then
                btnCone3.Enabled = False
                btnCone3.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 4 Then
                btnCone4.Enabled = False
                btnCone4.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 5 Then
                btnCone5.Enabled = False
                btnCone5.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 6 Then
                btnCone6.Enabled = False
                btnCone6.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 7 Then
                btnCone7.Enabled = False
                btnCone7.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 8 Then
                btnCone8.Enabled = False
                btnCone8.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 9 Then
                btnCone9.Enabled = False
                btnCone9.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 10 Then
                btnCone10.Enabled = False
                btnCone10.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 11 Then
                btnCone11.Enabled = False
                btnCone11.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 12 Then
                btnCone12.Enabled = False
                btnCone12.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 13 Then
                btnCone13.Enabled = False
                btnCone13.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 14 Then
                btnCone14.Enabled = False
                btnCone14.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 15 Then
                btnCone15.Enabled = False
                btnCone15.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 16 Then
                btnCone16.Enabled = False
                btnCone16.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 17 Then
                btnCone17.Enabled = False
                btnCone17.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 18 Then
                btnCone18.Enabled = False
                btnCone18.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 19 Then
                btnCone19.Enabled = False
                btnCone19.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 20 Then
                btnCone20.Enabled = False
                btnCone20.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 21 Then
                btnCone21.Enabled = False
                btnCone21.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 22 Then
                btnCone22.Enabled = False
                btnCone22.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 23 Then
                btnCone23.Enabled = False
                btnCone23.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 24 Then
                btnCone24.Enabled = False
                btnCone24.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 25 Then
                btnCone25.Enabled = False
                btnCone25.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 26 Then
                btnCone26.Enabled = False
                btnCone26.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 27 Then
                btnCone27.Enabled = False
                btnCone27.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 28 Then
                btnCone28.Enabled = False
                btnCone28.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 29 Then
                btnCone29.Enabled = False
                btnCone29.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 30 Then
                btnCone30.Enabled = False
                btnCone30.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 31 Then
                btnCone31.Enabled = False
                btnCone31.BackColor = Color.Yellow
                defect = varConeNum
            ElseIf varConeNum - coneNumOffset = 32 Then
                btnCone32.Enabled = False
                btnCone32.BackColor = Color.Yellow
                defect = varConeNum
            End If

        End If

        If shortCone Then

            'THIS IS THE SHORT CONE TEMP UPDATE ALL OTHER CONES ARE FINISHED WHEN SAVED BUT SHORT CONE NEEDS A TEMP UPDATE TO WORK FOR SORTING DEPT

            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("SHORTCONE").Value = shortCone 'shortCone
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_S").Value = "True" 'Sets the SHORT fault flag

            txtBoxUpdates()



            'UPDATE DATABASE FROM DATAGRID AS SETTING SHORT DOES NOT DO A FULL ROW UPDATE
            'UNTIL CONE VALUE Is ENTERED, THIS Is NEEDED AS SORT WILL BE DONE SEPERATE TO VISUAL COLOUR CHECK
            ' UpdateDatabase()

            If varConeNum - coneNumOffset = 1 Then
                btnCone1.Enabled = True
                btnCone1.BackColor = Color.Red
                shortC(1) = 1
            ElseIf varConeNum - coneNumOffset = 2 Then
                btnCone2.Enabled = True
                btnCone2.BackColor = Color.Red
                shortC(2) = 1
            ElseIf varConeNum - coneNumOffset = 3 Then
                btnCone3.Enabled = True
                btnCone3.BackColor = Color.Red
                shortC(3) = 1
            ElseIf varConeNum - coneNumOffset = 4 Then
                btnCone4.Enabled = True
                btnCone4.BackColor = Color.Red
                shortC(4) = 1
            ElseIf varConeNum - coneNumOffset = 5 Then
                btnCone5.Enabled = True
                btnCone5.BackColor = Color.Red
                shortC(5) = 1
            ElseIf varConeNum - coneNumOffset = 6 Then
                btnCone6.Enabled = True
                btnCone6.BackColor = Color.Red
                shortC(6) = 1
            ElseIf varConeNum - coneNumOffset = 7 Then
                btnCone7.Enabled = True
                btnCone7.BackColor = Color.Red
                shortC(7) = 1
            ElseIf varConeNum - coneNumOffset = 8 Then
                btnCone8.Enabled = True
                btnCone8.BackColor = Color.Red
                shortC(8) = 1
            ElseIf varConeNum - coneNumOffset = 9 Then
                btnCone9.Enabled = True
                btnCone9.BackColor = Color.Red
                shortC(9) = 1
            ElseIf varConeNum - coneNumOffset = 10 Then
                btnCone10.Enabled = True
                btnCone10.BackColor = Color.Red
                shortC(10) = 1
            ElseIf varConeNum - coneNumOffset = 11 Then
                btnCone11.Enabled = True
                btnCone11.BackColor = Color.Red
                shortC(11) = 1
            ElseIf varConeNum - coneNumOffset = 12 Then
                btnCone12.Enabled = True
                btnCone12.BackColor = Color.Red
                shortC(12) = 1
            ElseIf varConeNum - coneNumOffset = 13 Then
                btnCone13.Enabled = True
                btnCone13.BackColor = Color.Red
                shortC(13) = 1
            ElseIf varConeNum - coneNumOffset = 14 Then
                btnCone14.Enabled = True
                btnCone14.BackColor = Color.Red
                shortC(14) = 1
            ElseIf varConeNum - coneNumOffset = 15 Then
                btnCone15.Enabled = True
                btnCone15.BackColor = Color.Red
                shortC(15) = 1
            ElseIf varConeNum - coneNumOffset = 16 Then
                btnCone16.Enabled = True
                btnCone16.BackColor = Color.Red
                shortC(16) = 1
            ElseIf varConeNum - coneNumOffset = 17 Then
                btnCone17.Enabled = True
                btnCone17.BackColor = Color.Red
                shortC(17) = 1
            ElseIf varConeNum - coneNumOffset = 18 Then
                btnCone18.Enabled = True
                btnCone18.BackColor = Color.Red
                shortC(18) = 1
            ElseIf varConeNum - coneNumOffset = 19 Then
                btnCone19.Enabled = True
                btnCone19.BackColor = Color.Red
                shortC(19) = 1
            ElseIf varConeNum - coneNumOffset = 20 Then
                btnCone20.Enabled = True
                btnCone20.BackColor = Color.Red
                shortC(20) = 1
            ElseIf varConeNum - coneNumOffset = 21 Then
                btnCone21.Enabled = True
                btnCone21.BackColor = Color.Red
                shortC(21) = 1
            ElseIf varConeNum - coneNumOffset = 22 Then
                btnCone22.Enabled = True
                btnCone22.BackColor = Color.Red
                shortC(22) = 1
            ElseIf varConeNum - coneNumOffset = 23 Then
                btnCone23.Enabled = True
                btnCone23.BackColor = Color.Red
                shortC(23) = 1
            ElseIf varConeNum - coneNumOffset = 24 Then
                btnCone24.Enabled = True
                btnCone24.BackColor = Color.Red
                shortC(24) = 1
            ElseIf varConeNum - coneNumOffset = 25 Then
                btnCone25.Enabled = True
                btnCone25.BackColor = Color.Red
                shortC(25) = 1
            ElseIf varConeNum - coneNumOffset = 26 Then
                btnCone26.Enabled = True
                btnCone26.BackColor = Color.Red
                shortC(26) = 1
            ElseIf varConeNum - coneNumOffset = 27 Then
                btnCone27.Enabled = True
                btnCone27.BackColor = Color.Red
                shortC(27) = 1
            ElseIf varConeNum - coneNumOffset = 28 Then
                btnCone28.Enabled = True
                btnCone28.BackColor = Color.Red
                shortC(28) = 1
            ElseIf varConeNum - coneNumOffset = 29 Then
                btnCone29.Enabled = True
                btnCone29.BackColor = Color.Red
                shortC(29) = 1
            ElseIf varConeNum - coneNumOffset = 30 Then
                btnCone30.Enabled = True
                btnCone30.BackColor = Color.Red
                shortC(30) = 1
            ElseIf varConeNum - coneNumOffset = 31 Then
                btnCone31.Enabled = True
                btnCone31.BackColor = Color.Red
                shortC(31) = 1
            ElseIf varConeNum - coneNumOffset = 32 Then
                btnCone32.Enabled = True
                btnCone32.BackColor = Color.Red
                shortC(32) = 1
            End If

        End If

        If coneBarley = 1 Then
            'Routine to Set Cone color to barley and update cone numbers with defects



            If varConeNum - coneNumOffset = 1 Then
                btnCone1.Enabled = False
                btnCone1.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 2 Then
                btnCone2.Enabled = False
                btnCone2.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 3 Then
                btnCone3.Enabled = False
                btnCone3.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 4 Then
                btnCone4.Enabled = False
                btnCone4.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 5 Then
                btnCone5.Enabled = False
                btnCone5.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 6 Then
                btnCone6.Enabled = False
                btnCone6.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 7 Then
                btnCone7.Enabled = False
                btnCone7.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 8 Then
                btnCone8.Enabled = False
                btnCone8.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 9 Then
                btnCone9.Enabled = False
                btnCone9.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 10 Then
                btnCone10.Enabled = False
                btnCone10.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 11 Then
                btnCone11.Enabled = False
                btnCone11.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 12 Then
                btnCone12.Enabled = False
                btnCone12.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 13 Then
                btnCone13.Enabled = False
                btnCone13.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 14 Then
                btnCone14.Enabled = False
                btnCone14.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 15 Then
                btnCone15.Enabled = False
                btnCone15.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 16 Then
                btnCone16.Enabled = False
                btnCone16.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 17 Then
                btnCone17.Enabled = False
                btnCone17.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 18 Then
                btnCone18.Enabled = False
                btnCone18.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 19 Then
                btnCone19.Enabled = False
                btnCone19.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 20 Then
                btnCone20.Enabled = False
                btnCone20.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 21 Then
                btnCone21.Enabled = False
                btnCone21.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 22 Then
                btnCone22.Enabled = False
                btnCone22.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 23 Then
                btnCone23.Enabled = False
                btnCone23.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 24 Then
                btnCone24.Enabled = False
                btnCone24.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 25 Then
                btnCone25.Enabled = False
                btnCone25.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 26 Then
                btnCone26.Enabled = False
                btnCone26.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 27 Then
                btnCone27.Enabled = False
                btnCone27.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 28 Then
                btnCone28.Enabled = False
                btnCone28.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 29 Then
                btnCone29.Enabled = False
                btnCone29.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 30 Then
                btnCone30.Enabled = False
                btnCone30.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 31 Then
                btnCone31.Enabled = False
                btnCone31.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            ElseIf varConeNum - coneNumOffset = 32 Then
                btnCone32.Enabled = False
                btnCone32.BackgroundImage = My.Resources.BARRE
                coneBarley = varConeNum
            End If

        End If

        If coneWaste = 1 Then
            'Routine to Set Cone color to defect and update cone numbers with defects



            If varConeNum - coneNumOffset = 1 Then
                btnCone1.Enabled = False
                btnCone1.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 2 Then
                btnCone2.Enabled = False
                btnCone2.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 3 Then
                btnCone3.Enabled = False
                btnCone3.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 4 Then
                btnCone4.Enabled = False
                btnCone4.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 5 Then
                btnCone5.Enabled = False
                btnCone5.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 6 Then
                btnCone6.Enabled = False
                btnCone6.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 7 Then
                btnCone7.Enabled = False
                btnCone7.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 8 Then
                btnCone8.Enabled = False
                btnCone8.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 9 Then
                btnCone9.Enabled = False
                btnCone9.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 10 Then
                btnCone10.Enabled = False
                btnCone10.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 11 Then
                btnCone11.Enabled = False
                btnCone11.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 12 Then
                btnCone12.Enabled = False
                btnCone12.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 13 Then
                btnCone13.Enabled = False
                btnCone13.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 14 Then
                btnCone14.Enabled = False
                btnCone14.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 15 Then
                btnCone15.Enabled = False
                btnCone15.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 16 Then
                btnCone16.Enabled = False
                btnCone16.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 17 Then
                btnCone17.Enabled = False
                btnCone17.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 18 Then
                btnCone18.Enabled = False
                btnCone18.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 19 Then
                btnCone19.Enabled = False
                btnCone19.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 20 Then
                btnCone20.Enabled = False
                btnCone20.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 21 Then
                btnCone21.Enabled = False
                btnCone21.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 22 Then
                btnCone22.Enabled = False
                btnCone22.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 23 Then
                btnCone23.Enabled = False
                btnCone23.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 24 Then
                btnCone24.Enabled = False
                btnCone24.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 25 Then
                btnCone25.Enabled = False
                btnCone25.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 26 Then
                btnCone26.Enabled = False
                btnCone26.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 27 Then
                btnCone27.Enabled = False
                btnCone27.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 28 Then
                btnCone28.Enabled = False
                btnCone28.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 29 Then
                btnCone29.Enabled = False
                btnCone29.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 30 Then
                btnCone30.Enabled = False
                btnCone30.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 31 Then
                btnCone31.Enabled = False
                btnCone31.BackColor = Color.Purple
                coneWaste = varConeNum
            ElseIf varConeNum - coneNumOffset = 32 Then
                btnCone32.Enabled = False
                btnCone32.BackColor = Color.Purple
                coneWaste = varConeNum
            End If

        End If






        If (varVisConeInspect = 1) Then


            If coneM10 Then

                btnImage = My.Resources.M10
                coneM10 = varConeNum
            ElseIf coneP10 Then

                btnImage = My.Resources.P10
                coneP10 = varConeNum
            ElseIf coneM30 Then

                btnImage = My.Resources.M30
                coneM30 = varConeNum
            ElseIf coneP30 Then

                btnImage = My.Resources.P30
                coneP30 = varConeNum
            ElseIf coneM50 Then

                btnImage = My.Resources.M50
                coneM50 = varConeNum
            ElseIf coneP50 Then

                btnImage = My.Resources.P50
                coneP50 = varConeNum
            ElseIf coneZero Then

                btnImage = My.Resources.Zero
                coneZero = varConeNum
            ElseIf valHH > 0 Then
                btnImage = My.Resources.PatH
                valHH = varConeNum

            ElseIf valLL > 0 Then
                btnImage = My.Resources.PatL
                valLL = varConeNum

            ElseIf valHStd > 0 Then
                btnImage = My.Resources.PatHSTD
                valHStd = varConeNum

            End If


            If varConeNum - coneNumOffset = 1 Then
                btnCone1.Enabled = False

                btnCone1.BackgroundImage = btnImage
                If shortC(1) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 2 Then
                btnCone2.Enabled = False

                btnCone2.BackgroundImage = btnImage
                If shortC(2) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 3 Then
                btnCone3.Enabled = False

                btnCone3.BackgroundImage = btnImage
                If shortC(3) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 4 Then
                btnCone4.Enabled = False

                btnCone4.BackgroundImage = btnImage
                If shortC(4) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 5 Then
                btnCone5.Enabled = False

                btnCone5.BackgroundImage = btnImage
                If shortC(5) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 6 Then
                btnCone6.Enabled = False

                btnCone6.BackgroundImage = btnImage
                If shortC(6) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 7 Then
                btnCone7.Enabled = False

                btnCone7.BackgroundImage = btnImage
                If shortC(7) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 8 Then
                btnCone8.Enabled = False

                btnCone8.BackgroundImage = btnImage
                If shortC(8) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 9 Then
                btnCone9.Enabled = False

                btnCone9.BackgroundImage = btnImage
                If shortC(9) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 10 Then
                btnCone10.Enabled = False

                btnCone10.BackgroundImage = btnImage
                If shortC(10) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 11 Then
                btnCone11.Enabled = False

                btnCone11.BackgroundImage = btnImage
                If shortC(11) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 12 Then
                btnCone12.Enabled = False

                btnCone12.BackgroundImage = btnImage
                If shortC(12) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 13 Then
                btnCone13.Enabled = False

                btnCone13.BackgroundImage = btnImage
                If shortC(13) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 14 Then
                btnCone14.Enabled = False

                btnCone14.BackgroundImage = btnImage
                If shortC(14) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 15 Then
                btnCone15.Enabled = False

                btnCone15.BackgroundImage = btnImage
                If shortC(15) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 16 Then
                btnCone16.Enabled = False

                btnCone16.BackgroundImage = btnImage
                If shortC(16) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 17 Then
                btnCone17.Enabled = False

                btnCone17.BackgroundImage = btnImage
                If shortC(17) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 18 Then
                btnCone18.Enabled = False

                btnCone18.BackgroundImage = btnImage
                If shortC(18) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 19 Then
                btnCone19.Enabled = False

                btnCone19.BackgroundImage = btnImage
                If shortC(19) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 20 Then
                btnCone20.Enabled = False

                btnCone20.BackgroundImage = btnImage
                If shortC(20) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 21 Then
                btnCone21.Enabled = False

                btnCone21.BackgroundImage = btnImage
                If shortC(21) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 22 Then
                btnCone22.Enabled = False

                btnCone22.BackgroundImage = btnImage
                If shortC(22) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 23 Then
                btnCone23.Enabled = False

                btnCone23.BackgroundImage = btnImage
                If shortC(23) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 24 Then
                btnCone24.Enabled = False

                btnCone24.BackgroundImage = btnImage
                If shortC(24) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 25 Then
                btnCone25.Enabled = False

                btnCone25.BackgroundImage = btnImage
                If shortC(25) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 26 Then
                btnCone26.Enabled = False

                btnCone26.BackgroundImage = btnImage
                If shortC(26) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 27 Then
                btnCone27.Enabled = False

                btnCone27.BackgroundImage = btnImage
                If shortC(27) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 28 Then
                btnCone28.Enabled = False

                btnCone28.BackgroundImage = btnImage
                If shortC(28) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 29 Then
                btnCone29.Enabled = False

                btnCone29.BackgroundImage = btnImage
                If shortC(29) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 30 Then
                btnCone30.Enabled = False

                btnCone30.BackgroundImage = btnImage
                If shortC(30) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 31 Then
                btnCone31.Enabled = False

                btnCone31.BackgroundImage = btnImage
                If shortC(31) = 1 Then shortCone = 2
            ElseIf varConeNum - coneNumOffset = 32 Then
                btnCone32.Enabled = False

                btnCone32.BackgroundImage = btnImage
                If shortC(32) = 1 Then shortCone = 2
            End If

            btnImage = Nothing
        End If




        If varVisConeInspect = 1 Then

            Me.btnVisGrade.Enabled = True
            Me.btnBarley.Enabled = True

            If lclHHLL = "YES" And btnLL.Visible Then
                Me.btnWaste.Visible = False
            Else
                Me.btnWaste.Visible = True
                Me.btnWaste.Enabled = True
            End If


            Me.btnShort.Enabled = False
            Me.btnNoCone.Enabled = False
            Me.btnDefect.Enabled = False
            Me.btnShort.Visible = False
            Me.btnNoCone.Visible = False
            Me.btnDefect.Visible = False
            Me.btnSave.Visible = False
            'Me.btnNoConeSave.Visible = False
            Me.btnDefectSave.Visible = False
            Me.btnDefect.Visible = False
            btnUnlock.Text = "UNLOCK OFF"
            btnUnlock.ForeColor = Color.Red
            btnDelete.Visible = False
            'Me.btnShortSave.Visible = False
            Me.btnClear.Visible = False

            If My.Settings.chkUseSort And lclHHLL = "YES" Then
                Me.btnStdCheese.Visible = True
            Else
                Me.btnStdCheese.Visible = False
            End If

            If shortCone = 1 Then coneCount = coneCount Else coneCount = coneCount + 1  'if Short being set do not add to cone count
            'lblConeCount.Text = coneCount

        Else

            Me.btnVisGrade.Enabled = True
            Me.btnBarley.Enabled = False
            Me.btnShort.Visible = True
            Me.btnShort.Enabled = True
            Me.btnNoCone.Visible = True
            Me.btnNoCone.Enabled = True
            Me.btnDefect.Enabled = True
            Me.btnStdCheese.Visible = True
            Me.btnStdCheese.Enabled = True
            Me.btnSave.Visible = False
            'Me.btnNoConeSave.Visible = False
            Me.btnDefectSave.Visible = False
            'Me.btnShortSave.Visible = False
            Me.btnClear.Visible = False
            If shortCone = 1 Then coneCount = coneCount Else coneCount = coneCount + 1  'if Short being set do not add to cone count
            'lblConeCount.Text = coneCount

        End If




        If shortCone = 1 Then
            NoCone = 0
            defect = 0
            shortCone = 0
            StdCone = Nothing
            stdStateVar = Nothing
            varConeNum = 0
            txtConeNum.Text = ""


        Else
            varCartEndTime = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")
            If shortCone = 2 Then shortCone = varConeNum
            If shortCone > 0 Then Fault_S = "True" 'Else Fault_S = "False"
            jobArrayUpdate()


            NoCone = 0
            StdCone = Nothing
            stdStateVar = Nothing
            defect = 0
            shortCone = 0
            varConeNum = 0
            BackgroundImage = Nothing
            coneBarley = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0
            coneWaste = 0
            valHH = 0
            valLL = 0
            valHStd = 0


            Me.chk_K.Checked = False
            Me.chk_D.Checked = False
            Me.chk_F.Checked = False
            Me.chk_O.Checked = False
            Me.chk_T.Checked = False
            Me.chk_P.Checked = False
            Me.chk_S.Checked = False
            Me.chk_X.Checked = False
            Me.chk_N.Checked = False
            Me.chk_W.Checked = False
            Me.chk_H.Checked = False
            Me.chk_TR.Checked = False
            Me.chk_B.Checked = False
            Me.chk_C.Checked = False
            Fault_S = "False"
            Fault_X = "False"
            'SORT Dept FAULTS
            Me.chk_DO.Visible = False
            Me.chk_DH.Visible = False
            Me.chk_CL.Visible = False
            Me.chk_FI.Visible = False
            Me.chk_YN.Visible = False
            Me.chk_HT.Visible = False
            Me.chk_LT.Visible = False


            txtConeNum.Text = ""

        End If



    End Sub



    Private Sub btnStdCheese_Click(sender As Object, e As EventArgs) Handles btnStdCheese.Click
        If varConeNum > 0 Then

            StdCone = 1



            Me.btnVisGrade.Enabled = False
            Me.btnShort.Enabled = False
            Me.btnNoCone.Enabled = False
            Me.btnDefect.Enabled = False
            Me.btnDefectSave.Visible = True
            Me.btnClear.Visible = True
            shortC(varConeNum - coneNumOffset) = 0
            defect = 0
            NoCone = 0
            shortCone = 0
            coneBarley = 0
            coneWaste = 0
            coneZero = 0
            coneM10 = 0
            coneP10 = 0
            coneM30 = 0
            coneP30 = 0
            coneM50 = 0
            coneP50 = 0

            Me.chk_K.Visible = False
            Me.chk_D.Visible = False
            Me.chk_F.Visible = False
            Me.chk_O.Visible = False
            Me.chk_T.Visible = False
            Me.chk_P.Visible = False
            Me.chk_S.Visible = False
            Me.chk_X.Visible = False
            Me.chk_N.Visible = False
            Me.chk_W.Visible = False
            Me.chk_H.Visible = False
            Me.chk_TR.Visible = False
            Me.chk_B.Visible = False
            Me.chk_C.Visible = False
            'SORT Dept FAULTS
            Me.chk_DO.Visible = False
            Me.chk_DH.Visible = False
            Me.chk_CL.Visible = False
            Me.chk_FI.Visible = False
            Me.chk_YN.Visible = False
            Me.chk_HT.Visible = False
            Me.chk_LT.Visible = False
        Else
            MsgBox("You must select a Cheese number first")
        End If

    End Sub


    'Routines to control Vericolor Solo

    Delegate Sub DataDelegate(ByVal sdata As String)


    Private Sub StoreReceivedData(ByVal sdata As String)



        incoming &= sdata
        MsgBox(incoming)
        MsgBox(sdata)

    End Sub

    Private Sub VeriColorCom_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs)
        Dim ReceivedData As String = ""
        Try
            ReceivedData = VeriColorCom.ReadLine
        Catch ex As Exception
            'ReceivedData = ex.Message
            MsgBox(ex.Message)
        End Try

        Dim adre As New DataDelegate(AddressOf StoreReceivedData)

        Me.Invoke(adre, ReceivedData)

    End Sub



    Private Sub jobArrayUpdate()



        If My.Settings.chkUseColour = True Then
            If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("COLENDTM").Value) Then
                For i As Integer = 1 To frmDGV.DGVdata.Rows.Count
                    frmDGV.DGVdata.Rows(i - 1).Cells("COLENDTM").Value = varCartEndTime 'COLOUR CHECK END TIME
                Next
            End If
        End If

        If My.Settings.chkUseSort = True Then
            If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("SORTENDTM").Value) Then
                For i As Integer = 1 To frmDGV.DGVdata.Rows.Count
                    frmDGV.DGVdata.Rows(i - 1).Cells("SORTENDTM").Value = varCartEndTime 'SORT END TIME
                Next
            End If
        End If

        'list of Array Feilds to Update

        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("OPNAME").Value = frmJobEntry.varUserName  'operatorName   fron entry screen

        If My.Settings.chkUseSort Or My.Settings.chkUseColour Then

            If frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("SHORTCONE").Value = 0 Then
                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("SHORTCONE").Value = shortCone   'shortCone
            End If


            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("MISSCONE").Value = NoCone  'missingCone


            If lclHHLL = "YES" And Val(NoCone) > 0 Then
                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("HHLL").Value = "MISS"
            End If


            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("DEFCONE").Value = defect  'defectCone
        End If


        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("CONEZERO").Value = coneZero  'passCone  Zero Colour Difference   

        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("CONEBARLEY").Value = coneBarley 'Cone with large colour defect
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("M10").Value = coneM10   'coneM10
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("P10").Value = coneP10   'coneP10
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("M30").Value = coneM30   'coneM30
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("P30").Value = coneP30  'coneP30
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("M50").Value = coneM50   'coneM50
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("P50").Value = coneP50  'coneP50

        If Val(valHH) > 0 Then
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("HHLL").Value = "H"
        End If

        If Val(valLL) > 0 Then
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("HHLL").Value = "L"
        End If

        If Val(valHStd) > 0 Then
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("HHLL").Value = "H Std"
        End If

        If lclHHLL = "YES" And Val(NoCone) > 0 Then
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("HHLL").Value = "MISS"
        End If




        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("CARTENDTM").Value = varCartEndTime 'cartEndTime
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("RECHK").Value = reChecked    'Cone has been reChecked    
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("RECHKTM").Value = ReCheckTime    'Cone has been reChecked  

        If My.Settings.chkUseSort Or My.Settings.chkUseColour Then
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_K").Value = chk_K.Checked    'KEBA Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_D").Value = chk_D.Checked     'DIRTY Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_F").Value = chk_F.Checked     'FORM AB Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_O").Value = chk_O.Checked     'OVERTHROWN Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_T").Value = chk_T.Checked     'TENSION AB. Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_P").Value = chk_P.Checked     'PAPER TUBE AB. Fault

            If frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_S").Value = "False" Then
                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_S").Value = Fault_S       'SHORT CHEESE Fault
            End If 'SHORT CHEESE Fault


            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_X").Value = Fault_X           'No HAVE CHEESE Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_N").Value = chk_N.Checked     'NO TAIL & ABNORMAL Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_W").Value = chk_W.Checked     'WASTE Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_H").Value = chk_H.Checked     'HITTING Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_TR").Value = chk_TR.Checked    'TARUMI Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_B").Value = chk_B.Checked     'B- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_C").Value = chk_C.Checked     'C- GRADE BY M/C  Fault  
            'SORT Dept FAULTS
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_DO").Value = chk_DO.Checked     'DO- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_DH").Value = chk_DH.Checked     'DH- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_CL").Value = chk_CL.Checked     'CL- GRADE BY M/C  Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_FI").Value = chk_FI.Checked     'FI- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_YN").Value = chk_YN.Checked     'YN- GRADE BY M/C  Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_HT").Value = chk_HT.Checked     'HT- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_LT").Value = chk_LT.Checked     'LT- GRADE BY M/C  Fault 

            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("COLWASTE").Value = coneWaste     'COLOUR WASTE BY COLOUR DEPT


            'IF STDSTATE IS ZERO THEN WRITE NULL TO db OTHERWISE WRITE VALUE
            If StdCone > 0 Then
                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("STDCHEESE").Value = StdCone      'sets cheese number 

                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("STDSTATE").Value = stdStateVar
            Else
                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("STDCHEESE").Value = DBNull.Value    'sets cheese number 

                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("STDSTATE").Value = DBNull.Value
            End If


        End If



        UpdateConeVal()


    End Sub



    Public Sub txtBoxUpdates()

        Dim coneMissingID As String = Nothing
        Dim coneDefectID As String = Nothing
        Dim shortConeID As String = Nothing

        Dim visConeBarleyID As String = Nothing
        Dim visConeWasteID As String = Nothing
        Dim visConeZeroID As String = Nothing
        Dim visConeM10ID As String = Nothing
        Dim visConeP10ID As String = Nothing
        Dim visConeM30ID As String = Nothing
        Dim visConeP30ID As String = Nothing
        Dim visConeM50ID As String = Nothing
        Dim visConeP50ID As String = Nothing

        Dim visConeHH As String = Nothing
        Dim visConeLL As String = Nothing
        Dim visConeHSTD As String = Nothing
        Dim visConeLSTD As String = Nothing

        Dim fmt As String = "000"    'FORMAT STRING FOR NUMBER 
        Dim tmpConeNum = ""


        txtShort.Text = ""
        txtMissing.Text = ""
        txtDefect.Text = ""
        txtBarley.Text = ""
        txtWaste.Text = ""
        txtM10.Text = ""
        txtP10.Text = ""
        txtM30.Text = ""
        txtP30.Text = ""
        txtM50.Text = ""
        txtP50.Text = ""

        txtHH.Text = ""
        txtLL.Text = ""
        ' txtHSTD.Text = ""

        For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count


            If frmDGV.DGVdata.Rows(rw - 1).Cells("SHORTCONE").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                shortConeID = shortConeID & tmpConeNum & ","
                txtShort.Text = shortConeID
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("MISSCONE").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                coneMissingID = coneMissingID & tmpConeNum & ","
                txtMissing.Text = coneMissingID



            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("DEFCONE").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                coneDefectID = coneDefectID & tmpConeNum & ","
                txtDefect.Text = coneDefectID
            End If

            If frmDGV.DGVdata.Rows(rw - 1).Cells("CONEZERO").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                visConeZeroID = visConeZeroID & tmpConeNum & ","
                txtZero.Text = visConeZeroID
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("CONEBARLEY").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                visConeBarleyID = visConeBarleyID & tmpConeNum & ","
                txtBarley.Text = visConeBarleyID
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("M10").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                visConeM10ID = visConeM10ID & tmpConeNum & ","
                txtM10.Text = visConeM10ID
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("P10").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                visConeP10ID = visConeP10ID & tmpConeNum & ","
                txtP10.Text = visConeP10ID
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("M30").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                visConeM30ID = visConeM30ID & tmpConeNum & ","
                txtM30.Text = visConeM30ID
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("P30").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                visConeP30ID = visConeP30ID & tmpConeNum & ","
                txtP30.Text = visConeP30ID
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("M50").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                visConeM50ID = visConeM50ID & tmpConeNum & ","
                txtM50.Text = visConeM50ID
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("P50").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                visConeP50ID = visConeP50ID & tmpConeNum & ","
                txtP50.Text = visConeP50ID
            ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("COLWASTE").Value > 0 Then
                tmpConeNum = rw + coneNumOffset.ToString(fmt)
                visConeWasteID = visConeWasteID & tmpConeNum & ","
                txtWaste.Text = visConeWasteID
            End If

            If lclHHLL = "YES" And tmpCartType = "B" Then
                If Not IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value) Then
                    If frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value = "H" Then
                        tmpConeNum = rw + coneNumOffset.ToString(fmt)
                        visConeHH = visConeHH & tmpConeNum & ","
                        txtHH.Text = visConeHH
                    ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value = "H Std" Then
                        tmpConeNum = rw + coneNumOffset.ToString(fmt)
                        visConeHSTD = visConeHSTD & tmpConeNum & ","
                        txtHSTD.Text = visConeHSTD

                    ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells("HHLL").Value = "L" Then
                        tmpConeNum = rw + coneNumOffset.ToString(fmt)
                        visConeLL = visConeLL & tmpConeNum & ","
                        txtLL.Text = visConeLL

                    End If
                End If
            End If
            tmpConeNum = 0
        Next

    End Sub

    Public Sub tsbtnSave()




        Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows
        'Dim iRow As Integer = frmDGV.DGVdata.CurrentRow.Index
        frmDGV.DGVdata.AllowUserToAddRows = True
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmDGV.DGVdata.AllowUserToAddRows = bAddState
        'frmDGV.DGVdata.EndEdit()


    End Sub


    Private Sub UpdateDatabase()

        tsbtnSave()





        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            If frmJobEntry.LDS.HasChanges Then

                frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

            End If

        Catch dbcx As DBConcurrencyException
            Dim Response As String

            MessageBox.Show("Data for this job is already being used in packing, " & vbCrLf _
                       & "Please retry after some time, all changes made will be lost" & vbCrLf _
                       & "ข้อมูลสำหรับงานนี้กำลังถูกประมวลผลโดยแผนกแพ็คกิ้ง," _
                       & "กรุณาป้อนใหม่อีกครั้งในภายหลัง, ข้อมูลที่ท่านป้อนจะไม่ได้ทำการบันทึก.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            Response = dbcx.Row.ToString
            writeerrorLog.writelog("db A_Pk Con Error", Response, False, "A_Pk Con Fault")
            Response = dbcx.RowCount.ToString
            writeerrorLog.writelog("db A_Pk Con Error", Response, False, "A_Pk Con Fault")

            If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
            frmDGV.DGVdata.ClearSelection()
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.Close()


        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("db Update Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("db Update Error", ex.ToString, False, "System Fault")


            MsgBox("Update Error:  " & vbNewLine & ex.Message)
        End Try

        Dim coneref
        For rows = 1 To frmDGV.DGVdata.Rows.Count

            coneref = frmDGV.DGVdata.Rows(rows - 1).Cells("BCODECONE").Value


            If Not IsDBNull(frmDGV.DGVdata.Rows(rows - 1).Cells("STDCHEESE").Value) Then
                If frmDGV.DGVdata.Rows(rows - 1).Cells("STDCHEESE").Value > "0" Then

                    frmJobEntry.AddParam("@bcodecart", frmJobEntry.txtLotNumber.Text)
                    frmJobEntry.AddParam("@bcodecone", coneref)

                    frmJobEntry.LExecQuery("UPDATE Jobs SET STDCHEESE = '" & frmDGV.DGVdata.Rows(rows - 1).Cells("STDCHEESE").Value & "' ," _
                                           & "  STDSTATE = '" & frmDGV.DGVdata.Rows(rows - 1).Cells("STDSTATE").Value & "'  Where bcodecart = @bcodecart and bcodecone = @bcodecone ")

                    ' frmJobEntry.LExecQuery("UPDATE Jobs SET STDSTATE = '" & frmDGV.DGVdata.Rows(rows - 1).Cells("STDSTATE").Value & "' Where bcodecone = '" & coneref & "' ")



                    If lclHHLL = "YES" Then
                        frmJobEntry.AddParam("@bcodecart", frmJobEntry.txtLotNumber.Text)
                        frmJobEntry.AddParam("@bcodecone", coneref)


                        If Not IsDBNull(frmDGV.DGVdata.Rows(rows - 1).Cells("HHLL").Value) Then
                            frmJobEntry.LExecQuery("UPDATE jobs SET HHLL = '" & frmDGV.DGVdata.Rows(rows - 1).Cells("HHLL").Value & "' Where bcodecart =  @bcodecart and bcodecone = @bcodecone ")
                            '        ' frmJobEntry.LExecQuery("UPDATE jobs SET HHLL_Res = '" & frmDGV.DGVdata.Rows(rows - 1).Cells("HHLL_Res").Value & "' Where bcodecone = '" & coneref & "' ")
                        End If
                    End If

                Else
                    frmJobEntry.AddParam("@bcodecart", frmJobEntry.txtLotNumber.Text)
                    frmJobEntry.AddParam("@bcodecone", coneref)

                    frmJobEntry.LExecQuery("UPDATE jobs SET stdcheese = NULL,stdstate = NULL Where bcodecart = @bcodecart and  bcodecone = @bcodecone ")
                    'frmJobEntry.LExecQuery("UPDATE jobs SET stdstate = NULL Where bcodecone = '" & coneref & "' ")

                    'If lclHHLL = "YES" Then
                    '    frmJobEntry.LExecQuery("UPDATE jobs SET HHLL = NULL Where bcodecone = '" & coneref & "' ")
                    '    '    ' frmJobEntry.LExecQuery("UPDATE jobs SET HHLL_Res = NULL Where bcodecone = '" & coneref & "' ")
                    'End If

                End If
            End If
        Next

    End Sub
    Private Sub printSheet()

        'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
        prodNameMod = frmDGV.DGVdata.Rows(0).Cells(52).Value.ToString
        prodNameMod = prodNameMod.Replace("/", "_")

        'CREATE THE SHEET NAME WHICH IS THE 4 LETTER REFRENCE AT THE END OF PRODUCT NAME
        sheetName = prodNameMod.Substring(prodNameMod.Length - 4) & "_" & frmJobEntry.txtGrade.Text

        Dim endsheetname As String = Nothing



        Select Case HorL
            Case "H"
                endsheetname = "Create H Cart"
            Case "L"
                endsheetname = "Create L Cart"
        End Select


        'CREATE THE FULL NAME FOR SAVING THE FILE
        SaveString = (prodNameMod & " " _
            & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value.ToString & "_" _
            & frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value.ToString) & " " & endsheetname

        'Use save same name as we need to update existing file
        'SaveString = frmJobEntry.txtLotNumber.Text

        'CREATE Date STRING
        Dim finddate As String



        Dim YY, MM, DD As String
        Dim todaypath As String
        Dim savename As String
        Dim SheetNum As Integer


        finddate = frmJobEntry.txtLotNumber.Text 'gets the date the file was created
        YY = finddate.Substring(3, 2)
        MM = finddate.Substring(5, 2)
        DD = finddate.Substring(7, 2)


        finddate = (DD & "_" & MM & "_20" & YY)

        todaypath = (My.Settings.dirPacking & "\" & finddate) 'path of date the file shuld be stored in

        'create the save name of the file
        savename = (todaypath & "\" & SaveString & ".xlsx").ToString


        Dim sheetNumber As Integer = 0
        If frmJobEntry.txtLotNumber.Text.Length > 18 Then
            sheetNumber = frmJobEntry.txtLotNumber.Text.Substring(17, 2)
        Else
            sheetNumber = frmJobEntry.txtLotNumber.Text.Substring(17, 1)
        End If


        'CHECK TO SEE IF THERE IS ALREADY A FILE STARTED FOR PRODUCT NUMBER
        'IN TODATY DIRECTORY
        ' todayDir()

        'UPDATE THE EXCEL SHEET FOR THIS JOB


        Dim ReCheckworkbook As Excel.Workbook
        Dim ReChecksheets As Excel.Worksheet


        ReCheckworkbook = MyHLGradeUpdate.Workbooks.Open(savename)
        ReChecksheets = ReCheckworkbook.Worksheets(sheetNumber)
        ReChecksheets.Activate()




        Try
            If File.Exists(savename) Then

                For i = 1 To frmDGV.DGVdata.Rows.Count

                    If IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value) Then Continue For

                    Select Case frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                        'H Grades

                        Case "H_DD"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.DarkBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "+30"
                        Case "H_D"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.DarkBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "+10"
                        Case "H_MM"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Blue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "0"
                        Case "H_L"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.LightBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "-10"
                        Case "H_LL"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.LightBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "-30"
                        Case "H_B"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Black
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "B"
                        Case "H_W"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Purple
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "Waste"
                        'L grades
                        Case "L_DD"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.DarkBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "+30"
                        Case "L_D"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.DarkBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "+10"
                        Case "L_MM"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Blue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "0"
                        Case "L_L"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.LightBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "-10"
                        Case "L_LL"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.LightBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "-30"
                        Case "L_B"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Black
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "B"
                        Case "L_W"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Purple
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "Waste"
                            'Short Grades H
                        Case "HS_D"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.DarkBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "+10 Short"
                        Case "HS_M"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Blue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "0 Short"
                        Case "HS_L"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.LightBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "-10 Short"
                        Case "HS_B"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Black
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "B Short"
                               'Short Grades H
                        Case "LS_D"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.DarkBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "+10 Short"
                        Case "LS_M"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Blue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "0 Short"
                        Case "LS_L"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.LightBlue
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "-10 Short"
                        Case "LS_B"
                            MyHLGradeUpdate.Cells(8 + i, 4).Font.Color = Color.Black
                            MyHLGradeUpdate.Cells(8 + i, 4) = frmDGV.DGVdata.Rows(i - 1).Cells("HHLL_Res").Value
                            MyHLGradeUpdate.Cells(8 + i, 6) = "B Short"
                    End Select
                Next

                MyHLGradeUpdate.Cells(45, 3) = frmJobEntry.varUserName  'Puts user name on the form


            Else
                MsgBox("No previous sheet found, please check and copy in to a today directory to continue")
                Exit Sub
            End If



        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Error", ex.ToString, False, "System Fault")
            MsgBox(ex.Message)
        End Try



        Try

            'Save changes to new file in Paking Dir
            MyHLGradeUpdate.DisplayAlerts = False
            ReCheckworkbook.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Save Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Save Error", ex.ToString, False, "System Fault")

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            ReCheckworkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Close Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Close Error", ex.ToString, False, "System Fault")
            MsgBox(ex.Message)
        End Try


        MyHLGradeUpdate.Quit()
        releaseObject(ReCheckworkbook)
        releaseObject(MyHLGradeUpdate)
        Me.Close()

    End Sub

    'Private Sub todayDir()

    '    ' routine to check if a today directory exists otherwise creat a new one
    '    PrevPath1 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-1).ToString("dd_MM_yyyy"))
    '    PrevPath2 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-2).ToString("dd_MM_yyyy"))
    '    PrevPath3 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-3).ToString("dd_MM_yyyy"))


    '    todayPath = (My.Settings.dirPacking & "\" & Date.Now.ToString("dd_MM_yyyy"))




    'End Sub


    Private Sub releaseObject(ByVal obj As Object)

        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub



End Class