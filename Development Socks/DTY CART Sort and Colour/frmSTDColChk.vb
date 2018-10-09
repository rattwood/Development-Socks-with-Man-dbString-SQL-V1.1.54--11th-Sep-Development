Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.ComponentModel


Public Class frmSTDColChk

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
    Dim PrevPath1 As String
    Dim PrevPath2 As String
    Dim PrevPath3 As String
    Dim todayPath As String
    Dim yestname1 As String
    Dim yestname2 As String
    Dim yestname3 As String

    Public prevDays As String
    Public savename As String
    Public template As String
    Public prevDaysName As String





    'ReCheck Params
    Dim reChecked, ReCheckTime As String

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
    Public coneCount As Integer
    Public coneState As String





        Private SQL As New SQLConn




        Private Sub frmColReCheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'CREATE ROWS IN DGV
        Dim rowcount = frmDGV.DGVdata.Rows.Count
        'create rows 
        DataGridView1.Rows.Add(rowcount)
        DataGridView1.RowHeadersVisible = False


        For i = 1 To rowcount
            DataGridView1.Rows(i - 1).Cells(0).Value = i
            DataGridView1.Rows(i - 1).Cells(1).Value = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value

            If frmDGV.DGVdata.Rows(i - 1).Cells(16).Value > 0 Then DataGridView1.Rows(i - 1).Cells(4).Value = "BARRE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(37).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "KEBA"
            If frmDGV.DGVdata.Rows(i - 1).Cells(38).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "DIRTY"
            If frmDGV.DGVdata.Rows(i - 1).Cells(39).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "FORM AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(40).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "OVERTHROWN"
            If frmDGV.DGVdata.Rows(i - 1).Cells(41).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "TENSION AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(42).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "PAPERTUBE AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(43).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "SHORT CHEESE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(44).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "X MISSING CHEESE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(45).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "NO TAIL & ABNORMAL"
            If frmDGV.DGVdata.Rows(i - 1).Cells(46).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "WASTE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(47).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "HITTING"
            If frmDGV.DGVdata.Rows(i - 1).Cells(48).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "TARUMI"
            If frmDGV.DGVdata.Rows(i - 1).Cells(49).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "B GRADE BY M/C"
            If frmDGV.DGVdata.Rows(i - 1).Cells(50).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "C GRADE BY MACHINE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(67).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "DIRTY OIL"
            If frmDGV.DGVdata.Rows(i - 1).Cells(68).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "DIRTY NY HAND"
            If frmDGV.DGVdata.Rows(i - 1).Cells(69).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "COLOUR AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(70).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "FLY IN"
            If frmDGV.DGVdata.Rows(i - 1).Cells(71).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "YARN AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(72).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "HIGH TENSION"
            If frmDGV.DGVdata.Rows(i - 1).Cells(73).Value = True Then DataGridView1.Rows(i - 1).Cells(4).Value = "LOW TENSION"



        Next
        DataGridView1.CurrentCell = DataGridView1(2, 0)
        DataGridView1.AllowUserToAddRows = False
            Label20.Text = frmDGV.DGVdata.Rows(0).Cells(52).Value
            Label21.Text = frmJobEntry.txtLotNumber.Text

        End Sub



        Private Sub btnResults_Click(sender As Object, e As EventArgs) Handles btnResults.Click


            'CHECK DATA IN CORRECTLY
            Dim colname As String

        'For i = 1 To frmDGV.DGVdata.Rows.Count


        '    If DataGridView1.Rows(i - 1).Cells(2).Value = "" Then
        '        colname = "ReCheck"
        '        MsgBox(colname & ", Row " & i & " has no value. Please correct and try again")
        '        Exit Sub

        '    End If

        'Next


        Dim CharRead As String

        For i = 1 To frmDGV.DGVdata.Rows.Count
            CharRead = DataGridView1.Rows(i - 1).Cells(2).Value

            Select Case CharRead

                Case "a", "A"
                    DataGridView1.Rows(i - 1).Cells(2).Style.ForeColor = Color.DarkBlue  'Grade A
                    DataGridView1.Rows(i - 1).Cells(2).Value = "OK"
                Case "d", "D"
                    DataGridView1.Rows(i - 1).Cells(2).Style.ForeColor = Color.Green    'Grade AD
                    DataGridView1.Rows(i - 1).Cells(2).Value = "+"
                Case "l", "L"
                    DataGridView1.Rows(i - 1).Cells(2).Style.ForeColor = Color.Blue   'Grade AL
                    DataGridView1.Rows(i - 1).Cells(2).Value = "-"
                Case "b", "B"
                    DataGridView1.Rows(i - 1).Cells(2).Style.ForeColor = Color.Red   'Grade BARRE
                    DataGridView1.Rows(i - 1).Cells(2).Value = "@"
                    DataGridView1.Rows(i - 1).Cells(4).Value = "BARRE"
                Case Else
                    'DataGridView1.Rows(i - 1).Cells(2).Style.ForeColor = Color.Red   'Grade AL
                    'DataGridView1.Rows(i - 1).Cells(2).Value = "ERROR ReEnter"
                    colname = "ReCheck"
                    MsgBox(colname & ", Row " & i & " has no value. Please correct and try again")
                    btnReEnter.Visible = True
                    Exit Sub
            End Select
        Next


        Dim tmpReChk1 As String
        Dim ACount, ALCount, ADCount, ABCount


        For i = 1 To frmDGV.DGVdata.Rows.Count
            tmpReChk1 = DataGridView1.Rows(i - 1).Cells(2).Value

            If tmpReChk1 = "OK" Then
                'A Grade
                'DataGridView1.Rows(i - 1).Cells(3).Style.ForeColor = Color.DarkBlue  'Grade A
                'DataGridView1.Rows(i - 1).Cells(4).Value = "OK"
                ACount = ACount + 1
            ElseIf tmpReChk1 = "+" Then
                'AD Grade
                DataGridView1.Rows(i - 1).Cells(3).Style.ForeColor = Color.Black    'Grade AD
                DataGridView1.Rows(i - 1).Cells(3).Value = "RECHECK"
                ADCount = ADCount + 1
            ElseIf tmpReChk1 = "-" Then
                'AL Grade
                DataGridView1.Rows(i - 1).Cells(3).Style.ForeColor = Color.Black   'Grade AL
                DataGridView1.Rows(i - 1).Cells(3).Value = "RECHECK"
                ALCount = ALCount + 1
            ElseIf tmpReChk1 = "@" Then
                'BARRE Grade
                DataGridView1.Rows(i - 1).Cells(3).Style.ForeColor = Color.Red   'Grade AL
                DataGridView1.Rows(i - 1).Cells(3).Value = "AB Grade"
                ABCount = ABCount + 1
            End If

        Next

        Label24.Text = ACount
        Label26.Text = ALCount + ADCount
        Label5.Text = ABCount


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

            End Select

            DataGridView1.Rows(i - 1).Cells(3).Value = ""

        Next




        Label24.Text = 0
        Label26.Text = 0





    End Sub




    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click



        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")





        For i = 1 To frmDGV.DGVdata.Rows.Count



            If DataGridView1.Rows(i - 1).Cells(4).Value = "KEBA" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_K").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "DIRTY" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_D").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "FORM AB" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_F").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "OVERTHROWN" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_O").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "TENSION AB" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_T").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "PAPERTUBE AB" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_P").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "SHORT CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_S").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "X MISSING CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_X").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "NO TAIL & ABNORMAL" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_N").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "WASTE" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_W").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "HITTING" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_H").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "TARUMI" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_TR").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "B GRADE BY M/C" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_B").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "C GRADE BY MACHINE" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_C").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "DIRTY OIL" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_DO").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "DIRTY NY HAND" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_DH").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "COLOUR AB" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_CL").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "FLY IN" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_FI").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "YARN AB" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_YN").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "HIGH TENSION" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_HT").Value = True
            If DataGridView1.Rows(i - 1).Cells(4).Value = "LOW TENSION" Then frmDGV.DGVdata.Rows(i - 1).Cells("FLT_LT").Value = True

            frmDGV.DGVdata.Rows(i - 1).Cells(57).Value = frmJobEntry.varUserName  'operatorName   fron entry screen


            If DataGridView1.Rows(i - 1).Cells(4).Value = "SHORT CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(10).Value = 1 'shortCone
            If DataGridView1.Rows(i - 1).Cells(4).Value = "X MISSING CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(11).Value = 1  'missingCone
            ' If DataGridView1.Rows(i - 1).Cells(4).Value = "BARRE" Then frmDGV.DGVdata.Rows(i - 1).Cells(16).Value = 1 'Cone with large colour defect






        Next

        'ROUTINE TO UPDAE STDSTATE IN db
        Dim tmpstate As Integer
        For i = 1 To frmDGV.DGVdata.Rows.Count
            tmpstate = frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value

            Select Case DataGridView1.Rows(i - 1).Cells(2).Value
                Case "OK"
                    frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = tmpstate + 1
                Case "-"
                    frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 10
                    frmDGV.DGVdata.Rows(i - 1).Cells("CONESTATE").Value = 8  'RESETS CHEESE TO STATE SO CREATE RECHECK CAN FIND IT
                    frmDGV.DGVdata.Rows(i - 1).Cells("M30").Value = frmDGV.DGVdata.Rows(i - 1).Cells("CONENUM").Value
                Case "+"
                    frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 10
                    frmDGV.DGVdata.Rows(i - 1).Cells("CONESTATE").Value = 8  'RESETS CHEESE TO STATE SO CREATE RECHECK CAN FIND IT
                    frmDGV.DGVdata.Rows(i - 1).Cells("P30").Value = frmDGV.DGVdata.Rows(i - 1).Cells("CONENUM").Value
                Case "@"
                    frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = Nothing
                    frmDGV.DGVdata.Rows(i - 1).Cells("STDCHEESE").Value = Nothing
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHECKBARCODE").Value = Nothing
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHKIDX").Value = Nothing
                    frmDGV.DGVdata.Rows(i - 1).Cells("CONESTATE").Value = 8  'RESETS CHEESE TO STATE SO CREATE RECHECK CAN FIND IT
                    frmDGV.DGVdata.Rows(i - 1).Cells("CONEBARLEY").Value = frmDGV.DGVdata.Rows(i - 1).Cells("CONENUM").Value

            End Select


            frmDGV.DGVdata.Rows(i - 1).Cells("RECHKCOLOP").Value = frmJobEntry.varUserName

        Next

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
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

        Dim endsheetname As String

        Select Case frmJobEntry.txtLotNumber.Text.Substring(9, 3)
            Case "R11", "R12"
                endsheetname = "Round1"
            Case "R21"
                endsheetname = "Round2"
            Case "R31"
                endsheetname = "Round3"
            Case "STD"
                endsheetname = "STD"
        End Select


        'CREATE THE FULL NAME FOR SAVING THE FILE
        saveString = (prodNameMod & " " _
            & frmDGV.DGVdata.Rows(0).Cells(7).Value.ToString & "_" _
            & frmDGV.DGVdata.Rows(0).Cells(2).Value.ToString) & " " & endsheetname


        'CREATE Date STRING
        Dim finddate As String

        finddate = frmJobEntry.txtLotNumber.Text

        Dim YY, MM, DD As String
        Dim todaypath As String
        Dim savename As String
        Dim SheetNum As Integer

        YY = finddate.Substring(3, 2)
        MM = finddate.Substring(5, 2)
        DD = finddate.Substring(7, 2)
        SheetNum = finddate.Substring(11, 1)

        finddate = (DD & "_" & MM & "_20" & YY)

        todaypath = (My.Settings.dirPacking & "\" & finddate)

        'create the save name of the file
        savename = (todaypath & "\" & saveString & ".xlsx").ToString

        Dim sheetNumber As Integer = 0

        sheetNumber = frmJobEntry.txtLotNumber.Text.Substring(11, 1)





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

                For i = 1 To frmDGV.DGVdata.Rows.Count

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
                            MyReCheckExcel.Cells(8 + i, 4).Font.Color = Color.Red   'Grade BARRE
                            MyReCheckExcel.Cells(8 + i, 4) = DataGridView1.Rows(i - 1).Cells(2).Value
                    End Select




                    Select Case DataGridView1.Rows(i - 1).Cells(3).Value

                        Case "RECHECK"
                            MyReCheckExcel.Cells(8 + i, 6).Font.Color = Color.Black    'Grade AD
                            MyReCheckExcel.Cells(8 + i, 6) = DataGridView1.Rows(i - 1).Cells(3).Value
                        Case "@"
                            MyReCheckExcel.Cells(8 + i, 6).Font.Color = Color.Red    'Grade AD
                            MyReCheckExcel.Cells(8 + i, 6) = DataGridView1.Rows(i - 1).Cells(3).Value

                    End Select







                    MyReCheckExcel.Cells(8 + i, 7) = DataGridView1.Rows(i - 1).Cells(4).Value 'DEFECT NAME

                Next

                MyReCheckExcel.Cells(45, 3) = frmJobEntry.varUserName  'Puts user name on the form

            Else


                If File.Exists(yestname1) Then      'ONE DAY AGO
                    prevDaysName = yestname1
                    prevDays = Date.Now.AddDays(-1).ToString("ddMMyyyy")
                    'MsgBox("I am ready to continue with sheet from Yesterday  " & prevDays)
                    frmPackPrvGet.PrvGet()
                    Me.Close()
                ElseIf File.Exists(yestname2) Then  'TWO DAYS AGO
                    prevDaysName = yestname2
                    prevDays = Date.Now.AddDays(-2).ToString("ddMMyyyy")
                    'MsgBox("I am ready to continue with sheet from Two days ago  " & prevDays)
                    frmPackPrvGet.PrvGet()
                    Me.Close()
                ElseIf File.Exists(yestname3) Then  'THREE DAYS AGO
                    prevDaysName = yestname3
                    prevDays = Date.Now.AddDays(-3).ToString("ddMMyyyy")
                    'MsgBox("I am ready to continue with sheet from three days ago  " & prevDays)
                    frmPackPrvGet.PrvGet()
                    Me.Close()
                Else
                    MsgBox("No previous sheet in last 4 days please check and copy in to a today directory to continue")
                    Exit Sub
                End If

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

    Private Sub todayDir()

        ' routine to check if a today directory exists otherwise creat a new one
        PrevPath1 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-1).ToString("dd_MM_yyyy"))
        PrevPath2 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-2).ToString("dd_MM_yyyy"))
        PrevPath3 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-3).ToString("dd_MM_yyyy"))


        todayPath = (My.Settings.dirPacking & "\" & Date.Now.ToString("dd_MM_yyyy"))
        ' finPath = (My.Settings.dirPackReports & "\" & Date.Now.ToString("dd_MM_yyyy"))



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

