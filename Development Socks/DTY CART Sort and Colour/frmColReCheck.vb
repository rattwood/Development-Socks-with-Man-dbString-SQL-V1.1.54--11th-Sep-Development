Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmColReCheck



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
    Dim btnImage As Image
    Dim keepDefcodes As Integer

    'Faults
    Dim Fault_S As String = "False"
        Dim Fault_X As String = "False"
        Dim shortC(32) As String

        'ReCheck Params
        Dim reChecked, ReCheckTime As String

        '        Public removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
        Dim incoming As String
        Public measureOn As String
        Public NoCone As Integer
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





        Private SQL As New SQLConn




        Private Sub frmColReCheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'CREATE ROWS IN DGV

        'create rows 
        DataGridView1.Rows.Add(32)
        DataGridView1.RowHeadersVisible = False


        For i = 1 To 32
            DataGridView1.Rows(i - 1).Cells(0).Value = frmDGV.DGVdata.Rows(i - 1).Cells(88).Value
            DataGridView1.Rows(i - 1).Cells(1).Value = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value
        Next

        DataGridView1.AllowUserToAddRows = False
        Label20.Text = frmJobEntry.varProductName


    End Sub


    Private Sub finish()



    End Sub




    Private Sub endJob()

            'UPDATE DATABASE WITH CHANGES



            'ONLY PRINT IF COLOUR SELECTED
            Dim today As String = DateAndTime.Today
            today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")


            Dim cellVal As String


            For rw As Integer = 1 To 32

                If My.Settings.chkUseColour Then

                    For cl As Integer = 10 To 22

                        cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells(cl).Value.ToString

                        If cl = 14 Then
                            Continue For
                        End If



                        If cl = 10 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "9"
                            Continue For
                        ElseIf cl = 11 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                            Continue For
                        ElseIf cl = 12 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                            Continue For
                        ElseIf cl = 15 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "9"
                            Continue For
                        ElseIf cl = 16 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                            Continue For
                        ElseIf cl = 17 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                            Continue For
                        ElseIf cl = 18 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                            Continue For
                        ElseIf cl = 19 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                            Continue For
                        ElseIf cl = 20 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                            Continue For
                        ElseIf cl = 21 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                            Continue For
                        ElseIf cl = 22 And cellVal > 0 And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then
                            frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                            Continue For
                        End If
                    Next

                    cellVal = frmDGV.DGVdata.Rows(rw - 1).Cells(66).Value.ToString          'SET CONE STATE IF WASTE CONE TO 8
                    If cellVal > 0 Then frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "8"
                    cellVal = 0

                End If

            Next

            For rw As Integer = 1 To 32

                If My.Settings.chkUseSort Then
                    If frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 5 Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "5"
                        frmDGV.DGVdata.Rows(rw - 1).Cells(31).Value = today
                        frmDGV.DGVdata.Rows(rw - 1).Cells(32).Value = today
                    End If
                End If

                If My.Settings.chkUseColour And frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value.ToString IsNot "8" Then
                    If frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value < 14 Then frmDGV.DGVdata.Rows(rw - 1).Cells(9).Value = "9"  'No Faults recorded so set to 9 Unless already Packed then do not change state
                End If

                If My.Settings.chkUseColour Then
                    frmDGV.DGVdata.Rows(rw - 1).Cells(57).Value = frmJobEntry.ColorOP
                    frmDGV.DGVdata.Rows(rw - 1).Cells(32).Value = today
                    If IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("COLENDTM").Value) Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("COLENDTM").Value = today 'COLOUR CHECK END TIME
                    End If
                ElseIf My.Settings.chkUseSort Then
                    frmDGV.DGVdata.Rows(rw - 1).Cells(56).Value = frmJobEntry.SortOP
                    frmDGV.DGVdata.Rows(rw - 1).Cells(32).Value = today
                    If IsDBNull(frmDGV.DGVdata.Rows(rw - 1).Cells("SORTENDTM").Value) Then
                        frmDGV.DGVdata.Rows(rw - 1).Cells("SORTENDTM").Value = today 'SORT END TIME
                    End If

                End If

            Next

            UpdateDatabase()


            If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
            frmDGV.DGVdata.ClearSelection()
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.Close()

        End Sub





    Private Sub jobArrayUpdate()


            'If coneZero Or coneM10 Or coneP10 Or coneM30 Or coneP30 Or coneM50 Or coneP50 > 0 Then
            '    defect = 0    'FrmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(11).Value = 0

            'End If

            'CHECK TO SEE IF DATE ALREADY SET FOR END TIME

            If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("COLENDTM").Value) Then
                For i As Integer = 1 To 32
                    If My.Settings.chkUseColour = True Then frmDGV.DGVdata.Rows(i - 1).Cells("COLENDTM").Value = varCartEndTime 'COLOUR CHECK END TIME
                Next
            End If

            If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("SORTENDTM").Value) Then
                For i As Integer = 1 To 32
                    If My.Settings.chkUseSort = True Then frmDGV.DGVdata.Rows(i - 1).Cells("SORTENDTM").Value = varCartEndTime 'SORT END TIME
                Next
            End If


            'list of Array Feilds to Update

            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(8).Value = frmJobEntry.varUserName  'operatorName   fron entry screen


        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(10).Value = shortCone   'shortCone
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(11).Value = NoCone  'missingCone
        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(12).Value = defect  'defectCone

        frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(15).Value = coneZero  'passCone  Zero Colour Difference    
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(16).Value = coneBarley 'Cone with large colour defect
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(17).Value = coneM10   'coneM10
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(18).Value = coneP10   'coneP10
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(19).Value = coneM30   'coneM30
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(20).Value = coneP30  'coneP30
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(21).Value = coneM50   'coneM50
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(22).Value = coneP50  'coneP50

        'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(31).Value = varCartStartTime  'cartStratTime
        'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(32).Value = varCartEndTime 'cartEndTime
        'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(33).Value = reChecked    'Cone has been reChecked    
        'frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells(34).Value = ReCheckTime    'Cone has been reChecked  

        If My.Settings.chkUseSort Or My.Settings.chkUseColour Then
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_K").Value = "K"   'KEBA Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_D").Value = "D"     'DIRTY Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_F").Value = "F"     'FORM AB Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_O").Value = "O"    'OVERTHROWN Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_T").Value = "T"     'TENSION AB. Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_P").Value = "P"    'PAPER TUBE AB. Fault
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_S").Value = "S"          'SHORT CHEESE Fault
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_X").Value = "X"        'No HAVE CHEESE Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_N").Value = "N"  'NO TAIL & ABNORMAL Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_W").Value = "W"   'WASTE Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_H").Value = "H"    'HITTING Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_TR").Value = "TR"   'TARUMI Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_B").Value = "B"    'B- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_C").Value = "C"    'C- GRADE BY M/C  Fault  
            'SORT Dept FAULTS
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_DO").Value = "DO"   'DO- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_DH").Value = "DH"    'DH- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_CL").Value = "CL"    'CL- GRADE BY M/C  Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_FI").Value = "FI"   'FI- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_YN").Value = "YN"    'YN- GRADE BY M/C  Fault 
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_HT").Value = "HT"    'HT- GRADE BY M/C  Fault  
            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("FLT_LT").Value = "LT"     'LT- GRADE BY M/C  Fault 

            frmDGV.DGVdata.Rows((varConeNum - 1) - coneNumOffset).Cells("COLWASTE").Value = coneWaste     'COLOUR WASTE BY COLOUR DEPT

            End If






            UpdateDatabase()


    End Sub

    'Private Sub CellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
    '    MsgBox("I am here")
    '    Dim allletters As String = "adlbw"
    '    'If Not allletters.Contains(e.KeyChar.ToString.ToLower) Then

    '    '    e.KeyChar = ChrW(0)
    '    '    e.Handled = True

    '    'End If
    'End Sub

    'Private Sub DataGridView1_CellFormmatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellValueChanged
    '    If e.Value IsNot Nothing Then
    '        e.Value = e.Value.ToString().ToUpper()
    '        e.FormattingApplied = True
    '    End If

    'End Sub


    Public Sub tsbtnSave()




            Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows
            'Dim iRow As Integer = frmDGV.DGVdata.CurrentRow.Index
            frmDGV.DGVdata.AllowUserToAddRows = True
            frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
            frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
            frmDGV.DGVdata.AllowUserToAddRows = bAddState



        End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click


        'CHECK DATA IN CORRECTLY
        Dim colname As String
        For x = 2 To 3
            For i = 1 To 32

                If DataGridView1.Rows(i - 1).Cells(x).Value = "" Then
                    If x > 2 Then colname = "ReCheck2" Else colname = "ReCheck1"
                    MsgBox(colname & ", Row " & i & " has no value. Please correct and try again")
                    Exit Sub

                End If

            Next
        Next

        Dim CharRead As String
        For x = 2 To 3
            For i = 1 To 32
                CharRead = DataGridView1.Rows(i - 1).Cells(x).Value

                Select Case CharRead

                    Case "a", "A"

                        DataGridView1.Rows(i - 1).Cells(x).Style.ForeColor = Color.DarkBlue  'Grade A
                        DataGridView1.Rows(i - 1).Cells(x).Value = "OK"

                    Case "d", "D"
                        DataGridView1.Rows(i - 1).Cells(x).Style.ForeColor = Color.Green    'Grade AD
                        DataGridView1.Rows(i - 1).Cells(x).Value = "+"
                    Case "l", "L"
                        DataGridView1.Rows(i - 1).Cells(x).Style.ForeColor = Color.Blue   'Grade AL
                        DataGridView1.Rows(i - 1).Cells(x).Value = "-"
                    Case "b", "B"
                        DataGridView1.Rows(i - 1).Cells(x).Style.ForeColor = Color.Red    'Grade Abnormal (B)
                        DataGridView1.Rows(i - 1).Cells(x).Value = "@"
                    Case "w", "W"
                        DataGridView1.Rows(i - 1).Cells(x).Style.ForeColor = Color.Black   'Grade Waste
                        DataGridView1.Rows(i - 1).Cells(x).Value = "*"

                End Select
            Next
        Next

        Dim tmpReChk1, tmpRechk2 As String



        For i = 1 To 32
            tmpReChk1 = DataGridView1.Rows(i - 1).Cells(2).Value
            tmpRechk2 = DataGridView1.Rows(i - 1).Cells(3).Value

            'A Grade
            If tmpReChk1 = "OK" And tmpRechk2 = "OK" Then
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.DarkBlue  'Grade A
                DataGridView1.Rows(i - 1).Cells(4).Value = "A"
            ElseIf tmpReChk1 = "OK" And tmpRechk2 = "+" Or tmpReChk1 = "+" And tmpRechk2 = "OK" Or tmpReChk1 = "+" And tmpRechk2 = "+" Then
                'AD Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Green    'Grade AD
                DataGridView1.Rows(i - 1).Cells(4).Value = "AD"
            ElseIf tmpReChk1 = "OK" And tmpRechk2 = "-" Or tmpReChk1 = "-" And tmpRechk2 = "OK" Or tmpReChk1 = "-" And tmpRechk2 = "-" Then
                'AL Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Blue   'Grade AL
                DataGridView1.Rows(i - 1).Cells(4).Value = "AL"
            ElseIf tmpReChk1 = "OK" And tmpRechk2 = "@" Or tmpReChk1 = "@" And tmpRechk2 = "OK" Or tmpReChk1 = "@" And tmpRechk2 = "@" Or tmpReChk1 = "@" And tmpRechk2 = "-" Or tmpReChk1 = "-" And tmpRechk2 = "@" Or tmpReChk1 = "@" And tmpRechk2 = "+" Or tmpReChk1 = "+" And tmpRechk2 = "@" Then
                'AB (B) Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Red    'Grade Abnormal (B)
                DataGridView1.Rows(i - 1).Cells(4).Value = "B"
            ElseIf tmpReChk1 = "Ok" And tmpRechk2 = "*" Or tmpReChk1 = "*" And tmpRechk2 = "OK" Or tmpReChk1 = "*" And tmpRechk2 = "*" Or tmpReChk1 = "*" And tmpRechk2 = "-" Or tmpReChk1 = "-" And tmpRechk2 = "*" Or tmpReChk1 = "*" And tmpRechk2 = "+" Or tmpReChk1 = "+" And tmpRechk2 = "*" Then
                'Waste Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Black   'Grade Waste
                DataGridView1.Rows(i - 1).Cells(4).Value = "W"
            End If

        Next






        finish()

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try


        'If My.Settings.chkUseColour Then frmFaultTrend.DefTrend()



    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting

    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged


    End Sub
End Class



