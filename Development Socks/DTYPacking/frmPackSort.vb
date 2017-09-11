Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmPackSort


    'Manual assesment variables
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

        'Faults
        Dim Fault_S, Fault_X As String
        Dim short1, short2, short3, short4, short5, short6, short7, short8, short9, short10, short11, short12, short13, short14, short15, short16 As String
        Dim short17, short18, short19, short20, short21, short22, short23, short24, short25, short26, short27, short28, short29, short30, short31, short32 As String

        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
            frmDGV.DGVdata.ClearSelection()
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            Me.Close()
        End Sub

        'ReCheck Params
        Dim reChecked, ReCheckTime As String

        '
        Public removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
        Dim incoming As String
        Public measureOn As String
        Public NoCone As Integer
        Public defect As Integer
        Public shortCone As Integer
        Public varCartStartTime As String   'Record time that we started measuring
        Public varCartEndTime As String
        Dim coneNumOffset As Integer
        Dim varConeBCode As String
        Dim fileActive As Integer
        Public varConeNum As Integer
        'Public batchNum As String
        Public coneCount As Integer
        Public coneState As String


        Public bill As String



        Private SQL As New SQLConn




        Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'SQL.ExecQuery("UPDATE jobs SET OPPACKSORT=frmJobEntry.varUserName")
            'SQL.ExecQuery("UPDATE jobs SET OPPACK=frmJobEntry.varUserName")



            Dim btnNum As Integer
            Dim btnNums As String

            btnNums = frmJobEntry.varCartSelect

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

            'SET CORRECT BUTTUN NUMBERS BASED ON CONE NUMBERS (SPINDEL NUMBERS)
            For i As Integer = 1 To 32

                Me.Controls("btnCone" & i.ToString).Text = btnNum
                btnNum = btnNum + 1

            Next


            Me.txtCartNum.Text = frmJobEntry.cartSelect
            Me.lblJobNum.Text = frmJobEntry.varJobNum




            'IF THIS IS AND EXISTING JOB THEN CALL BACK VALUES FROM DATABASE
            If frmJobEntry.coneValUpdate Then UpdateConeVal()






            'frmDGV.Show()            'Open Datgrid in background

        End Sub


        Private Sub UpdateConeVal()

            Dim cellVal As String


            For rw As Integer = 1 To 32

                For cl As Integer = 10 To 12

                    cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells(cl).Value.ToString

                    If cl = 10 And cellVal > 0 Then
                        Me.Controls("btnCone" & rw).BackColor = Color.Red       'SHORT
                        Me.Controls("btnCone" & rw).Enabled = False  'Turns off button
                    End If

                    If cl = 11 And cellVal > 0 Then
                        Me.Controls("btnCone" & rw).BackColor = Color.White     'NOCONE
                        Me.Controls("btnCone" & rw).Enabled = False
                    End If

                    If cl = 12 And cellVal > 0 Then
                        If cl = 12 And cellVal > 0 Then Me.Controls("btnCone" & rw).BackColor = Color.Yellow    'DEFECT
                        Me.Controls("btnCone" & rw).Enabled = False
                    End If



                Next

                For cl2 As Integer = 15 To 22

                    cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells(cl2).Value.ToString

                    If cl2 = 15 And cellVal > 0 Then Me.Controls("btnCone" & rw).BackColor = Color.Green      'ZERO CONE
                    If cl2 = 16 And cellVal > 0 Then Me.Controls("btnCone" & rw).BackColor = Color.Yellow     'BARLEY

                    If cl2 = 17 And cellVal > 0 Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.M10  'M10
                        Me.Controls("btnCone" & rw).BackColor = Color.Yellow
                    End If
                    If cl2 = 18 And cellVal > 0 Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.P10     'P10
                        Me.Controls("btnCone" & rw).BackColor = Color.Yellow
                    End If
                    If cl2 = 19 And cellVal > 0 Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.M30    'M30
                        Me.Controls("btnCone" & rw).BackColor = Color.Yellow
                    End If
                    If cl2 = 20 And cellVal > 0 Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.P30   'P30
                        Me.Controls("btnCone" & rw).BackColor = Color.Yellow
                    End If
                    If cl2 = 21 And cellVal > 0 Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.M50      'M50
                        Me.Controls("btnCone" & rw).BackColor = Color.Yellow
                    End If
                    If cl2 = 22 And cellVal > 0 Then
                        Me.Controls("btnCone" & rw).BackgroundImage = My.Resources.P50     'P50
                        Me.Controls("btnCone" & rw).BackColor = Color.Yellow
                    End If


                Next




            Next

            txtBoxUpdates()


        End Sub


        Private Sub endJob()

            'UPDATE DATABASE WITH CHANGES



            'ONLY PRINT IF COLOUR SELECTED
            Dim today As String = DateAndTime.Today

            If My.Settings.chkUseSort And My.Settings.chkUseColour Or My.Settings.chkUseColour Then
                today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")
                For i As Integer = 1 To 32
                    frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = 9  'coneState
                    frmDGV.DGVdata.Rows(i - 1).Cells(32).Value = today  'Update job date
                Next

                If My.Settings.chkUseSort And My.Settings.chkUseColour = False Then
                    For i As Integer = 1 To 32
                        frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = 5 'coneState
                    Next
                End If




                UpdateDatabase()
                ' frmPrintCartReport.prtCartSheet()



            End If


            If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
            frmDGV.DGVdata.ClearSelection()
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            Me.Close()

        End Sub


        Private Sub readsave()


            'NO CONE Update Cone button to colour of NoCone And add the cone number to the coneMissingID string so we have a full list of missing cones
            If NoCone Then


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
                'Routine to Set Cone color to defect and update cone numbers with defects


                'THIS IS THE SHORT CONE TEMP UPDATE ALL OTHER CONES ARE FINISHED WHEN SAVED BUT SHORT CONE NEEDS A TEMP UPDATE TO WORK FOR SORTING DEPT

                frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(10).Value = shortCone   'shortCone
                txtBoxUpdates()

                Fault_S = True   'Sets the SHORT fault flag

                'UPDATE DATABASE FROM DATAGRID AS SETTING SHORT DOES NOT DO A FULL ROW UPDATE
                'UNTIL CONE VALUE Is ENTERED, THIS Is NEEDED AS SORT WILL BE DONE SEPERATE TO VISUAL COLOUR CHECK
                UpdateDatabase()

                If varConeNum - coneNumOffset = 1 Then
                    btnCone1.Enabled = True
                    btnCone1.BackColor = Color.Red
                    short1 = 1
                ElseIf varConeNum = 2 Then
                    btnCone2.Enabled = True
                    btnCone2.BackColor = Color.Red
                    short2 = 1
                ElseIf varConeNum - coneNumOffset = 3 Then
                    btnCone3.Enabled = True
                    btnCone3.BackColor = Color.Red
                    short3 = 1
                ElseIf varConeNum - coneNumOffset = 4 Then
                    btnCone4.Enabled = True
                    btnCone4.BackColor = Color.Red
                    short4 = 1
                ElseIf varConeNum - coneNumOffset = 5 Then
                    btnCone5.Enabled = True
                    btnCone5.BackColor = Color.Red
                    short5 = 1
                ElseIf varConeNum - coneNumOffset = 6 Then
                    btnCone6.Enabled = True
                    btnCone6.BackColor = Color.Red
                    short6 = 1
                ElseIf varConeNum - coneNumOffset = 7 Then
                    btnCone7.Enabled = True
                    btnCone7.BackColor = Color.Red
                    short7 = 1
                ElseIf varConeNum - coneNumOffset = 8 Then
                    btnCone8.Enabled = True
                    btnCone8.BackColor = Color.Red
                    short8 = 1
                ElseIf varConeNum - coneNumOffset = 9 Then
                    btnCone9.Enabled = True
                    btnCone9.BackColor = Color.Red
                    short9 = 1
                ElseIf varConeNum - coneNumOffset = 10 Then
                    btnCone10.Enabled = True
                    btnCone10.BackColor = Color.Red
                    short10 = 1
                ElseIf varConeNum - coneNumOffset = 11 Then
                    btnCone11.Enabled = True
                    btnCone11.BackColor = Color.Red
                    short11 = 1
                ElseIf varConeNum - coneNumOffset = 12 Then
                    btnCone12.Enabled = True
                    btnCone12.BackColor = Color.Red
                    short12 = 1
                ElseIf varConeNum - coneNumOffset = 13 Then
                    btnCone13.Enabled = True
                    btnCone13.BackColor = Color.Red
                    short13 = 1
                ElseIf varConeNum - coneNumOffset = 14 Then
                    btnCone14.Enabled = True
                    btnCone14.BackColor = Color.Red
                    short14 = 1
                ElseIf varConeNum - coneNumOffset = 15 Then
                    btnCone15.Enabled = True
                    btnCone15.BackColor = Color.Red
                    short15 = 1
                ElseIf varConeNum - coneNumOffset = 16 Then
                    btnCone16.Enabled = True
                    btnCone16.BackColor = Color.Red
                    short16 = 1
                ElseIf varConeNum - coneNumOffset = 17 Then
                    btnCone17.Enabled = True
                    btnCone17.BackColor = Color.Red
                    short17 = 1
                ElseIf varConeNum - coneNumOffset = 18 Then
                    btnCone18.Enabled = True
                    btnCone18.BackColor = Color.Red
                    short18 = 1
                ElseIf varConeNum - coneNumOffset = 19 Then
                    btnCone19.Enabled = True
                    btnCone19.BackColor = Color.Red
                    short19 = 1
                ElseIf varConeNum - coneNumOffset = 20 Then
                    btnCone20.Enabled = True
                    btnCone20.BackColor = Color.Red
                    short20 = 1
                ElseIf varConeNum - coneNumOffset = 21 Then
                    btnCone21.Enabled = True
                    btnCone21.BackColor = Color.Red
                    short21 = 1
                ElseIf varConeNum - coneNumOffset = 22 Then
                    btnCone22.Enabled = True
                    btnCone22.BackColor = Color.Red
                    short22 = 1
                ElseIf varConeNum - coneNumOffset = 23 Then
                    btnCone23.Enabled = True
                    btnCone23.BackColor = Color.Red
                    short23 = 1
                ElseIf varConeNum - coneNumOffset = 24 Then
                    btnCone24.Enabled = True
                    btnCone24.BackColor = Color.Red
                    short24 = 1
                ElseIf varConeNum - coneNumOffset = 25 Then
                    btnCone25.Enabled = True
                    btnCone25.BackColor = Color.Red
                    short25 = 1
                ElseIf varConeNum - coneNumOffset = 26 Then
                    btnCone26.Enabled = True
                    btnCone26.BackColor = Color.Red
                    short26 = 1
                ElseIf varConeNum - coneNumOffset = 27 Then
                    btnCone27.Enabled = True
                    btnCone27.BackColor = Color.Red
                    short27 = 1
                ElseIf varConeNum - coneNumOffset = 28 Then
                    btnCone28.Enabled = True
                    btnCone28.BackColor = Color.Red
                    short28 = 1
                ElseIf varConeNum - coneNumOffset = 29 Then
                    btnCone29.Enabled = True
                    btnCone29.BackColor = Color.Red
                    short29 = 1
                ElseIf varConeNum - coneNumOffset = 30 Then
                    btnCone30.Enabled = True
                    btnCone30.BackColor = Color.Red
                    short30 = 1
                ElseIf varConeNum - coneNumOffset = 31 Then
                    btnCone31.Enabled = True
                    btnCone31.BackColor = Color.Red
                    short31 = 1
                ElseIf varConeNum - coneNumOffset = 32 Then
                    btnCone32.Enabled = True
                    btnCone32.BackColor = Color.Red
                    short32 = 1
                End If

            End If


            If (coneBarley = 1) Then
                'Routine to Set Cone color to defect and update cone numbers with defects



                If varConeNum - coneNumOffset = 1 Then
                    btnCone1.Enabled = False
                    btnCone1.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 2 Then
                    btnCone2.Enabled = False
                    btnCone2.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 3 Then
                    btnCone3.Enabled = False
                    btnCone3.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 4 Then
                    btnCone4.Enabled = False
                    btnCone4.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 5 Then
                    btnCone5.Enabled = False
                    btnCone5.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 6 Then
                    btnCone6.Enabled = False
                    btnCone6.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 7 Then
                    btnCone7.Enabled = False
                    btnCone7.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 8 Then
                    btnCone8.Enabled = False
                    btnCone8.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 9 Then
                    btnCone9.Enabled = False
                    btnCone9.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 10 Then
                    btnCone10.Enabled = False
                    btnCone10.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 11 Then
                    btnCone11.Enabled = False
                    btnCone11.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 12 Then
                    btnCone12.Enabled = False
                    btnCone12.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 13 Then
                    btnCone13.Enabled = False
                    btnCone13.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 14 Then
                    btnCone14.Enabled = False
                    btnCone14.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 15 Then
                    btnCone15.Enabled = False
                    btnCone15.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 16 Then
                    btnCone16.Enabled = False
                    btnCone16.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 17 Then
                    btnCone17.Enabled = False
                    btnCone17.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 18 Then
                    btnCone18.Enabled = False
                    btnCone18.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 19 Then
                    btnCone19.Enabled = False
                    btnCone19.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 20 Then
                    btnCone20.Enabled = False
                    btnCone20.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 21 Then
                    btnCone21.Enabled = False
                    btnCone21.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 22 Then
                    btnCone22.Enabled = False
                    btnCone22.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 23 Then
                    btnCone23.Enabled = False
                    btnCone23.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 24 Then
                    btnCone24.Enabled = False
                    btnCone24.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 25 Then
                    btnCone25.Enabled = False
                    btnCone25.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 26 Then
                    btnCone26.Enabled = False
                    btnCone26.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 27 Then
                    btnCone27.Enabled = False
                    btnCone27.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 28 Then
                    btnCone28.Enabled = False
                    btnCone28.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 29 Then
                    btnCone29.Enabled = False
                    btnCone29.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 30 Then
                    btnCone30.Enabled = False
                    btnCone30.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 31 Then
                    btnCone31.Enabled = False
                    btnCone31.BackColor = Color.Wheat
                    coneBarley = varConeNum
                ElseIf varConeNum - coneNumOffset = 32 Then
                    btnCone32.Enabled = False
                    btnCone32.BackColor = Color.Wheat
                    coneBarley = varConeNum
                End If

            End If






            If (varVisConeInspect = 1) And (coneBarley = 0) Then


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

                End If


                If varConeNum - coneNumOffset = 1 Then
                    btnCone1.Enabled = False

                    btnCone1.BackgroundImage = btnImage
                    If short1 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 2 Then
                    btnCone2.Enabled = False

                    btnCone2.BackgroundImage = btnImage
                    If short2 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 3 Then
                    btnCone3.Enabled = False

                    btnCone3.BackgroundImage = btnImage
                    If short3 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 4 Then
                    btnCone4.Enabled = False

                    btnCone4.BackgroundImage = btnImage
                    If short4 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 5 Then
                    btnCone5.Enabled = False

                    btnCone5.BackgroundImage = btnImage
                    If short5 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 6 Then
                    btnCone6.Enabled = False

                    btnCone6.BackgroundImage = btnImage
                    If short6 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 7 Then
                    btnCone7.Enabled = False

                    btnCone7.BackgroundImage = btnImage
                    If short7 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 8 Then
                    btnCone8.Enabled = False

                    btnCone8.BackgroundImage = btnImage
                    If short8 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 9 Then
                    btnCone9.Enabled = False

                    btnCone9.BackgroundImage = btnImage
                    If short9 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 10 Then
                    btnCone10.Enabled = False

                    btnCone10.BackgroundImage = btnImage
                    If short10 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 11 Then
                    btnCone11.Enabled = False

                    btnCone11.BackgroundImage = btnImage
                    If short11 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 12 Then
                    btnCone12.Enabled = False

                    btnCone12.BackgroundImage = btnImage
                    If short12 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 13 Then
                    btnCone13.Enabled = False

                    btnCone13.BackgroundImage = btnImage
                    If short13 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 14 Then
                    btnCone14.Enabled = False

                    btnCone14.BackgroundImage = btnImage
                    If short14 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 15 Then
                    btnCone15.Enabled = False

                    btnCone15.BackgroundImage = btnImage
                    If short15 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 16 Then
                    btnCone16.Enabled = False

                    btnCone16.BackgroundImage = btnImage
                    If short16 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 17 Then
                    btnCone17.Enabled = False

                    btnCone17.BackgroundImage = btnImage
                    If short17 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 18 Then
                    btnCone18.Enabled = False

                    btnCone18.BackgroundImage = btnImage
                    If short18 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 19 Then
                    btnCone19.Enabled = False

                    btnCone19.BackgroundImage = btnImage
                    If short19 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 20 Then
                    btnCone20.Enabled = False

                    btnCone20.BackgroundImage = btnImage
                    If short20 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 21 Then
                    btnCone21.Enabled = False

                    btnCone21.BackgroundImage = btnImage
                    If short21 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 22 Then
                    btnCone22.Enabled = False

                    btnCone22.BackgroundImage = btnImage
                    If short22 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 23 Then
                    btnCone23.Enabled = False

                    btnCone23.BackgroundImage = btnImage
                    If short23 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 24 Then
                    btnCone24.Enabled = False

                    btnCone24.BackgroundImage = btnImage
                    If short24 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 25 Then
                    btnCone25.Enabled = False

                    btnCone25.BackgroundImage = btnImage
                    If short25 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 26 Then
                    btnCone26.Enabled = False

                    btnCone26.BackgroundImage = btnImage
                    If short26 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 27 Then
                    btnCone27.Enabled = False

                    btnCone27.BackgroundImage = btnImage
                    If short27 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 28 Then
                    btnCone28.Enabled = False

                    btnCone28.BackgroundImage = btnImage
                    If short28 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 29 Then
                    btnCone29.Enabled = False

                    btnCone29.BackgroundImage = btnImage
                    If short29 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 30 Then
                    btnCone30.Enabled = False

                    btnCone30.BackgroundImage = btnImage
                    If short30 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 31 Then
                    btnCone31.Enabled = False

                    btnCone31.BackgroundImage = btnImage
                    If short31 = 1 Then shortCone = 2
                ElseIf varConeNum - coneNumOffset = 32 Then
                    btnCone32.Enabled = False

                    btnCone32.BackgroundImage = btnImage
                    If short32 = 1 Then shortCone = 2
                End If

                btnImage = Nothing
            End If

        End Sub

        'Create csv file

        Private Sub CSV()








        End Sub

        Private Sub jobArrayUpdate()

            If coneZero Or coneM10 Or coneP10 Or coneM30 Or coneP30 Or coneM50 Or coneP50 > 0 Then
                defect = 0    'FrmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(11).Value = 0
                keepDefcodes = 1
            End If




            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(8).Value = frmJobEntry.varUserName  'operatorName   fron entry screen
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(9).Value = 0   'coneState
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(10).Value = shortCone   'shortCone
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(11).Value = NoCone  'missingCone
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(12).Value = defect  'defectCone
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(13).Value = cartName  'cartNum  from Job Screen
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(14).Value = frmJobEntry.varCartSelect  'cartName  from BarCode
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(15).Value = coneZero  'passCone  Zero Colour Difference    
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(16).Value = coneBarley 'Cone with large colour defect
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(17).Value = coneM10   'coneM10
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(18).Value = coneP10   'coneP10
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(19).Value = coneM30   'coneM30
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(20).Value = coneP30  'coneP30
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(21).Value = coneM50   'coneM50
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(22).Value = coneP50  'coneP50
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(23).Value = varLedR   'ledR
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(24).Value = varLedG  'ledG
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(25).Value = varLedB   'ledB
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(26).Value = varCIE_L   'CIE_L
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(27).Value = varCIE_a  'CIE_a
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(28).Value = varCIE_b  'CIE_b
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(29).Value = varCIE_dL  'CIE_dL
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(30).Value = varCIE_dE   'CIE_dE
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(31).Value = varCartStartTime  'cartStratTime
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(32).Value = varCartEndTime 'cartEndTime
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(33).Value = reChecked    'Cone has been reChecked    TODO
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(34).Value = ReCheckTime    'Cone has been reChecked   TODO
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(35).Value = frmJobEntry.varCartBCode  'Cart actual Barcode
            'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(36).Value = varConeBCode  'Cone actual Barcode 



            UpdateDatabase()
            txtBoxUpdates()

        End Sub

        Public Sub txtBoxUpdates()

            Dim coneMissingID As String = Nothing
            Dim coneDefectID As String = Nothing
            Dim shortConeID As String = Nothing

            Dim visConeBarleyID As String = Nothing
            Dim visConeZeroID As String = Nothing
            Dim visConeM10ID As String = Nothing
            Dim visConeP10ID As String = Nothing
            Dim visConeM30ID As String = Nothing
            Dim visConeP30ID As String = Nothing
            Dim visConeM50ID As String = Nothing
            Dim visConeP50ID As String = Nothing


            Dim fmt As String = "000"    'FORMAT STRING FOR NUMBER 
            Dim tmpConeNum = ""



            For rw As Integer = 1 To 32


                If frmDGV.DGVdata.Rows(rw - 1).Cells(10).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    shortConeID = shortConeID & tmpConeNum & ","
                    txtShort.Text = shortConeID
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells(11).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    coneMissingID = coneMissingID & tmpConeNum & ","
                    txtMissing.Text = coneMissingID
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells(12).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    coneDefectID = coneDefectID & tmpConeNum & ","
                    txtDefect.Text = coneDefectID
                End If

                If frmDGV.DGVdata.Rows(rw - 1).Cells(15).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    visConeZeroID = visConeZeroID & tmpConeNum & ","
                    txtZero.Text = visConeZeroID
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells(16).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    visConeBarleyID = visConeBarleyID & tmpConeNum & ","
                    txtBarley.Text = visConeBarleyID
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells(17).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    visConeM10ID = visConeM10ID & tmpConeNum & ","
                    txtM10.Text = visConeM10ID
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells(18).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    visConeP10ID = visConeP10ID & tmpConeNum & ","
                    txtP10.Text = visConeP10ID
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells(19).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    visConeM30ID = visConeM30ID & tmpConeNum & ","
                    txtM30.Text = visConeM30ID
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells(20).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    visConeP30ID = visConeP30ID & tmpConeNum & ","
                    txtP30.Text = visConeP30ID
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells(21).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    visConeM50ID = visConeM50ID & tmpConeNum & ","
                    txtM50.Text = visConeM50ID
                ElseIf frmDGV.DGVdata.Rows(rw - 1).Cells(22).Value > 0 Then
                    tmpConeNum = rw + coneNumOffset.ToString(fmt)
                    visConeP50ID = visConeP50ID & tmpConeNum & ","
                    txtP50.Text = visConeP50ID
                End If
                tmpConeNum = 0
            Next

        End Sub

        Public Sub tsbtnSave()

            Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows
            Dim iRow As Integer = frmDGV.DGVdata.CurrentRow.Index
            frmDGV.DGVdata.AllowUserToAddRows = True
            frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
            frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(iRow).Cells(0) ' move back to current row
            frmDGV.DGVdata.AllowUserToAddRows = bAddState

        End Sub


        Private Sub UpdateDatabase()

            tsbtnSave()

        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

                If frmJobEntry.LDS.HasChanges Then


                    'frmJobEntry.LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

                    frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

                End If
            Catch ex As Exception

                bill = "Update Error: " & vbNewLine & ex.Message

                MsgBox(bill)
            End Try





        End Sub

End Class

