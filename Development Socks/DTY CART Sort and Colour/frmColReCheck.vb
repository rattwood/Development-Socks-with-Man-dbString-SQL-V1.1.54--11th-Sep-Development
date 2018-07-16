Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.ComponentModel

Public Class frmColReCheck

    Dim MyReCheckExcel As New Excel.Application

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
    Dim prodNameMod, sheetName, saveString As String
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


        For i = 1 To frmDGV.DGVdata.Rows.Count
            DataGridView1.Rows(i - 1).Cells(0).Value = frmDGV.DGVdata.Rows(i - 1).Cells(88).Value
            DataGridView1.Rows(i - 1).Cells(1).Value = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value

            If frmDGV.DGVdata.Rows(i - 1).Cells(16).Value > 0 Then DataGridView1.Rows(i - 1).Cells(5).Value = "BARRE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(37).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "KEBA"
            If frmDGV.DGVdata.Rows(i - 1).Cells(38).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY"
            If frmDGV.DGVdata.Rows(i - 1).Cells(39).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "FORM AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(40).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "OVERTHROWN"
            If frmDGV.DGVdata.Rows(i - 1).Cells(41).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "TENSION AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(42).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "PAPERTUBE AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(43).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "SHORT CHEESE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(44).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "X MISSING CHEESE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(45).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "NO TAIL & ABNORMAL"
            If frmDGV.DGVdata.Rows(i - 1).Cells(46).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "WASTE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(47).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "HITTING"
            If frmDGV.DGVdata.Rows(i - 1).Cells(48).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "TARUMI"
            If frmDGV.DGVdata.Rows(i - 1).Cells(49).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "B GRADE BY M/C"
            If frmDGV.DGVdata.Rows(i - 1).Cells(50).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "C GRADE BY MACHINE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(67).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY OIL"
            If frmDGV.DGVdata.Rows(i - 1).Cells(68).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY NY HAND"
            If frmDGV.DGVdata.Rows(i - 1).Cells(69).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "COLOUR AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(70).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "FLY IN"
            If frmDGV.DGVdata.Rows(i - 1).Cells(71).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "YARN AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(72).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "HIGH TENSION"
            If frmDGV.DGVdata.Rows(i - 1).Cells(73).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "LOW TENSION"

            'Debug values remove before release

            ' DataGridView1.Rows(i - 1).Cells(2).Value = "d"
            'DataGridView1.Rows(i - 1).Cells(3).Value = "d"

        Next

        DataGridView1.AllowUserToAddRows = False
        Label20.Text = frmDGV.DGVdata.Rows(0).Cells(52).Value
        Label21.Text = frmJobEntry.txtLotNumber.Text

    End Sub



    Private Sub btnResults_Click(sender As Object, e As EventArgs) Handles btnResults.Click


        'CHECK DATA IN CORRECTLY
        Dim colname As String
        For x = 2 To 3
            For i = 1 To frmDGV.DGVdata.Rows.Count

                If DataGridView1.Rows(i - 1).Cells(x).Value = "" Then
                    If x > 2 Then colname = "ReCheck2" Else colname = "ReCheck1"
                    MsgBox(colname & ", Row " & i & " has no value. Please correct and try again")
                    Exit Sub

                End If

            Next
        Next

        Dim CharRead As String
        For x = 2 To 3
            For i = 1 To frmDGV.DGVdata.Rows.Count
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

        Dim tmpReChk1, tmpRechk2, tmpDef As String
        Dim ACount, ALCount, ADCount, BCount, WCount


        For i = 1 To frmDGV.DGVdata.Rows.Count
            tmpReChk1 = DataGridView1.Rows(i - 1).Cells(2).Value
            tmpRechk2 = DataGridView1.Rows(i - 1).Cells(3).Value
            tmpDef = DataGridView1.Rows(i - 1).Cells(5).Value


            If tmpReChk1 = "*" Or tmpRechk2 = "*" Then
                'Waste Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Black   'Grade Waste
                DataGridView1.Rows(i - 1).Cells(4).Value = "W"
                WCount = WCount + 1
            ElseIf (tmpReChk1 = "@" Or tmpRechk2 = "@") Or (tmpReChk1 = "-" And tmpRechk2 = "+") Or (tmpReChk1 = "+" And tmpRechk2 = "-") Then
                'AB (B) Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Red    'Grade Abnormal (B)
                DataGridView1.Rows(i - 1).Cells(4).Value = "B"
                BCount = BCount + 1
            ElseIf (tmpReChk1 = "OK" And tmpRechk2 = "OK") And tmpDef = "" Then
                'A Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.DarkBlue  'Grade A
                DataGridView1.Rows(i - 1).Cells(4).Value = "A"
                ACount = ACount + 1
            ElseIf (tmpReChk1 = "OK" And tmpRechk2 = "+" Or tmpReChk1 = "+" And tmpRechk2 = "OK" Or tmpReChk1 = "+" And tmpRechk2 = "+") And tmpDef = "" Then
                'AD Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Green    'Grade AD
                DataGridView1.Rows(i - 1).Cells(4).Value = "AD"
                ADCount = ADCount + 1
            ElseIf (tmpReChk1 = "OK" And tmpRechk2 = "-" Or tmpReChk1 = "-" And tmpRechk2 = "OK" Or tmpReChk1 = "-" And tmpRechk2 = "-") And tmpDef = "" Then
                'AL Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Blue   'Grade AL
                DataGridView1.Rows(i - 1).Cells(4).Value = "AL"
                ALCount = ALCount + 1
            Else
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Red    'Grade Abnormal (B)
                DataGridView1.Rows(i - 1).Cells(4).Value = "B"
                BCount = BCount + 1
            End If




        Next

        Label24.Text = ACount
        Label26.Text = ALCount
        Label28.Text = ADCount
        Label30.Text = BCount
        Label32.Text = WCount

        btnReEnter.Visible = True
        btnFinish.Visible = True

    End Sub

    Private Sub btnReEnter_Click(sender As Object, e As EventArgs) Handles btnReEnter.Click

        btnReEnter.Visible = False
        btnFinish.Visible = False

        For i = 1 To frmDGV.DGVdata.Rows.Count



            'Reset reCheck1 values for re entry or modification
            Select Case DataGridView1.Rows(i - 1).Cells(2).Value

                Case "OK"
                    DataGridView1.Rows(i - 1).Cells(2).Value = "A"
                Case "-"
                    DataGridView1.Rows(i - 1).Cells(2).Value = "L"
                Case "+"
                    DataGridView1.Rows(i - 1).Cells(2).Value = "D"
                Case "@"
                    DataGridView1.Rows(i - 1).Cells(2).Value = "B"
                Case "*"
                    DataGridView1.Rows(i - 1).Cells(2).Value = "W"
            End Select
            'Reset reCheck2 values for re entry or modification
            Select Case DataGridView1.Rows(i - 1).Cells(3).Value
                Case "OK"
                    DataGridView1.Rows(i - 1).Cells(3).Value = "A"
                Case "-"
                    DataGridView1.Rows(i - 1).Cells(3).Value = "L"
                Case "+"
                    DataGridView1.Rows(i - 1).Cells(3).Value = "D"
                Case "@"
                    DataGridView1.Rows(i - 1).Cells(3).Value = "B"
                Case "*"
                    DataGridView1.Rows(i - 1).Cells(3).Value = "W"

            End Select
            DataGridView1.Rows(i - 1).Cells(4).Value = ""

        Next




        Label24.Text = 0
        Label26.Text = 0
        Label28.Text = 0
        Label30.Text = 0
        Label32.Text = 0




    End Sub




    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click



        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")

        'CHECK TO SEE IF DATE ALREADY SET FOR END TIME

        If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("RECHKENDTM").Value) Then
            For i As Integer = 1 To frmDGV.DGVdata.Rows.Count
                frmDGV.DGVdata.Rows(i - 1).Cells("RECHKENDTM").Value = today 'COLOUR CHECK END TIME
            Next
        End If



        For i = 1 To frmDGV.DGVdata.Rows.Count



            If DataGridView1.Rows(i - 1).Cells(5).Value = "KEBA" Then frmDGV.DGVdata.Rows(i - 1).Cells(37).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY" Then frmDGV.DGVdata.Rows(i - 1).Cells(38).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "FORM AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(39).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "OVERTHROWN" Then frmDGV.DGVdata.Rows(i - 1).Cells(40).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "TENSION AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(41).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "PAPERTUBE AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(42).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "SHORT CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(43).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "X MISSING CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(44).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "NO TAIL & ABNORMAL" Then frmDGV.DGVdata.Rows(i - 1).Cells(45).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "WASTE" Then frmDGV.DGVdata.Rows(i - 1).Cells(46).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "HITTING" Then frmDGV.DGVdata.Rows(i - 1).Cells(47).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "TARUMI" Then frmDGV.DGVdata.Rows(i - 1).Cells(48).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "B GRADE BY M/C" Then frmDGV.DGVdata.Rows(i - 1).Cells(49).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "C GRADE BY MACHINE" Then frmDGV.DGVdata.Rows(i - 1).Cells(50).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY OIL" Then frmDGV.DGVdata.Rows(i - 1).Cells(67).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY NY HAND" Then frmDGV.DGVdata.Rows(i - 1).Cells(68).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "COLOUR AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(69).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "FLY IN" Then frmDGV.DGVdata.Rows(i - 1).Cells(70).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "YARN AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(71).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "HIGH TENSION" Then frmDGV.DGVdata.Rows(i - 1).Cells(72).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "LOW TENSION" Then frmDGV.DGVdata.Rows(i - 1).Cells(73).Value = True

            frmDGV.DGVdata.Rows(i - 1).Cells(89).Value = frmJobEntry.varUserName  'operatorName   fron entry screen


            If DataGridView1.Rows(i - 1).Cells(5).Value = "SHORT CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(10).Value = 1 'shortCone
            If DataGridView1.Rows(i - 1).Cells(5).Value = "X MISSING CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(11).Value = 1  'missingCone
            If DataGridView1.Rows(i - 1).Cells(5).Value = "BARRE" Then frmDGV.DGVdata.Rows(i - 1).Cells(16).Value = 1 'Cone with large colour defect

            frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = 4  'Cone has been reChecked  so can be packed

        Next



        For i = 1 To frmDGV.DGVdata.Rows.Count
            'CHECK reCheck1
            Select Case DataGridView1.Rows(i - 1).Cells(2).Value

                Case "OK"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "A"
                Case "-"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "AL"
                Case "+"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "AD"
                Case "@"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "B"
                Case "*"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "W"
            End Select
            'CHECK reCheck2
            Select Case DataGridView1.Rows(i - 1).Cells(3).Value

                Case "OK"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = "A"
                Case "-"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = "AL"
                Case "+"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = "AD"
                Case "@"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = "B"
                Case "*"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = "W"
            End Select

            frmDGV.DGVdata.Rows(i - 1).Cells("RECHKCOLOP").Value = frmJobEntry.varUserName
            frmDGV.DGVdata.Rows(i - 1).Cells("RECHKRESULT").Value = DataGridView1.Rows(i - 1).Cells(4).Value 'WRITE RECHECK RESULT

            If DataGridView1.Rows(i - 1).Cells(4).Value = "AL" Then frmDGV.DGVdata.Rows(i - 1).Cells("CONEAL").Value = DataGridView1.Rows(i - 1).Cells(4).Value  'WRITE RECHEECK RESULT TO DGV
            If DataGridView1.Rows(i - 1).Cells(4).Value = "AD" Then frmDGV.DGVdata.Rows(i - 1).Cells("CONEAD").Value = DataGridView1.Rows(i - 1).Cells(4).Value 'WRITE RECHEECK RESULT TO DGV

        Next


        printSheet()




        UpdateDatabase()


        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Close()


    End Sub


    Private Sub printSheet()

        'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
        prodNameMod = frmDGV.DGVdata.Rows(0).Cells(52).Value.ToString
        prodNameMod = prodNameMod.Replace("/", "_")

        'CREATE THE SHEET NAME WHICH IS THE 4 LETTER REFRENCE AT THE END OF PRODUCT NAME
        sheetName = prodNameMod.Substring(prodNameMod.Length - 4) & "_" & frmJobEntry.txtGrade.Text

        'CREATE THE FULL NAME FOR SAVING THE FILE
        saveString = (prodNameMod & " " _
            & frmDGV.DGVdata.Rows(0).Cells(7).Value.ToString & "_" _
            & frmDGV.DGVdata.Rows(0).Cells(2).Value.ToString) & " " & "ReCheck"


        'CREATE DATE STRING
        Dim finddate As String

        finddate = frmJobEntry.txtLotNumber.Text

        Dim YY, MM, DD As String
        Dim todaypath As String
        Dim savename As String
        Dim SheetNum As Integer

        YY = finddate.Substring(3, 2)
        MM = finddate.Substring(5, 2)
        DD = finddate.Substring(7, 2)
        SheetNum = finddate.Substring(16, 1)

        finddate = (DD & "_" & MM & "_20" & YY)

        todaypath = (My.Settings.dirPacking & "\" & finddate)

        'create the save name of the file
        savename = (todaypath & "\" & saveString & ".xlsx").ToString

        Dim sheetNumber As Integer = 0

        sheetNumber = frmJobEntry.txtLotNumber.Text.Substring(16, 1)




        'UPDATE THE EXCEL SHEET FOR THIS JOB


        Dim ReCheckworkbook As Excel.Workbook
        Dim ReChecksheets As Excel.Worksheet


        ReCheckworkbook = MyReCheckExcel.Workbooks.Open(savename) '.Sheets(SheetNum)
        ReChecksheets = ReCheckworkbook.Worksheets(sheetNumber)
        ReChecksheets.Activate()


        'CHECK TO SEE IF THERE IS ALREADY A FILE STARTED FOR PRODUCT NUMBER
        'IN TODATY DIRECTORY
        Try
            If File.Exists(savename) Then






                For i = 1 To 32

                    Select Case DataGridView1.Rows(i - 1).Cells(2).Value
                        Case "OK"

                            MyReCheckExcel.Cells(8 + i, 4).Font.Color = Color.DarkBlue  'Grade A
                            MyReCheckExcel.Cells(8 + i, 4) = DataGridView1.Rows(i - 1).Cells(2).Value
                        Case "+"
                            MyReCheckExcel.Cells(8 + i, 4).Font.Color = Color.Green    'Grade AD
                            MyReCheckExcel.Cells(8 + i, 4) = DataGridView1.Rows(i - 1).Cells(2).Value
                        Case "-"
                            MyReCheckExcel.Cells(8 + i, 4).Font.Color = Color.Blue   'Grade AL
                            MyReCheckExcel.Cells(8 + i, 4) = DataGridView1.Rows(i - 1).Cells(2).Value
                        Case "@"
                            MyReCheckExcel.Cells(8 + i, 4).Font.Color = Color.Red    'Grade Abnormal (B)
                            MyReCheckExcel.Cells(8 + i, 4) = DataGridView1.Rows(i - 1).Cells(2).Value
                        Case "*"
                            MyReCheckExcel.Cells(8 + i, 4).Font.Color = Color.Black   'Grade Waste
                            MyReCheckExcel.Cells(8 + i, 4) = DataGridView1.Rows(i - 1).Cells(2).Value

                    End Select

                    Select Case DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "OK"
                            MyReCheckExcel.Cells(8 + i, 5).Font.Color = Color.DarkBlue  'Grade A
                            MyReCheckExcel.Cells(8 + i, 5) = DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "+"
                            MyReCheckExcel.Cells(8 + i, 5).Font.Color = Color.Green    'Grade AD
                            MyReCheckExcel.Cells(8 + i, 5) = DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "-"
                            MyReCheckExcel.Cells(8 + i, 5).Font.Color = Color.Blue   'Grade AL
                            MyReCheckExcel.Cells(8 + i, 5) = DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "@"
                            MyReCheckExcel.Cells(8 + i, 5).Font.Color = Color.Red    'Grade Abnormal (B)
                            MyReCheckExcel.Cells(8 + i, 5) = DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "*"
                            MyReCheckExcel.Cells(8 + i, 5).Font.Color = Color.Black   'Grade Waste
                            MyReCheckExcel.Cells(8 + i, 5) = DataGridView1.Rows(i - 1).Cells(3).Value
                    End Select


                    Select Case DataGridView1.Rows(i - 1).Cells(4).Value
                        Case "A"
                            MyReCheckExcel.Cells(8 + i, 6).Font.Color = Color.DarkBlue  'Grade A
                            MyReCheckExcel.Cells(8 + i, 6) = DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "AD"
                            MyReCheckExcel.Cells(8 + i, 6).Font.Color = Color.Green    'Grade AD
                            MyReCheckExcel.Cells(8 + i, 6) = DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "AL"
                            MyReCheckExcel.Cells(8 + i, 6).Font.Color = Color.Blue   'Grade AL
                            MyReCheckExcel.Cells(8 + i, 6) = DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "B"
                            MyReCheckExcel.Cells(8 + i, 6).Font.Color = Color.Red    'Grade Abnormal (B)
                            MyReCheckExcel.Cells(8 + i, 6) = DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "W"
                            MyReCheckExcel.Cells(8 + i, 6).Font.Color = Color.Black   'Grade Waste
                            MyReCheckExcel.Cells(8 + i, 6) = DataGridView1.Rows(i - 1).Cells(3).Value
                    End Select






                    MyReCheckExcel.Cells(8 + i, 6) = DataGridView1.Rows(i - 1).Cells(4).Value 'GRADE RESULT
                    MyReCheckExcel.Cells(8 + i, 7) = DataGridView1.Rows(i - 1).Cells(5).Value 'DEFECT NAME

                Next

                MyReCheckExcel.Cells(43, 6) = frmJobEntry.varUserName  'Puts user name on the form


            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Try

            'Save changes to new file in Paking Dir
            MyReCheckExcel.DisplayAlerts = False
            ReCheckworkbook.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            ReCheckworkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyReCheckExcel.Quit()
        releaseObject(ReCheckworkbook)
        releaseObject(MyReCheckExcel)
        Me.Close()

    End Sub




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

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click

    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click

    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click

    End Sub

    Private Sub Label26_Click(sender As Object, e As EventArgs) Handles Label26.Click

    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click

    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click

    End Sub

    Private Sub Label32_Click(sender As Object, e As EventArgs) Handles Label32.Click

    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click

    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click

    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Close()

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


End Class