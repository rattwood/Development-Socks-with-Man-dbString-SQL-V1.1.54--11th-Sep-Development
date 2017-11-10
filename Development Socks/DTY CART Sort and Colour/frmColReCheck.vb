﻿Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel


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


        For i = 1 To 32
            DataGridView1.Rows(i - 1).Cells(0).Value = frmDGV.DGVdata.Rows(i - 1).Cells(88).Value
            DataGridView1.Rows(i - 1).Cells(1).Value = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value

            If frmDGV.DGVdata.Rows(i - 1).Cells(37).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "KEBA"
            If frmDGV.DGVdata.Rows(i - 1).Cells(38).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY"
            If frmDGV.DGVdata.Rows(i - 1).Cells(39).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "FORM AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(40).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "OVERTHROWN"
            If frmDGV.DGVdata.Rows(i - 1).Cells(41).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "TENSION AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(42).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "PAPERTUBE AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(43).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "SHORT CHEESE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(44).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "X MISSING CHEESE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(45).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "NO TAIL & ABNORMAL"
            If frmDGV.DGVdata.Rows(i - 1).Cells(46).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "HITTING"
            If frmDGV.DGVdata.Rows(i - 1).Cells(47).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "TARUMI"
            If frmDGV.DGVdata.Rows(i - 1).Cells(48).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "B GRADE BY M/C"
            If frmDGV.DGVdata.Rows(i - 1).Cells(49).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "C GRADE BY MACHINE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(50).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY OIL"
            If frmDGV.DGVdata.Rows(i - 1).Cells(66).Value > 0 Then DataGridView1.Rows(i - 1).Cells(5).Value = "BARRE"
            If frmDGV.DGVdata.Rows(i - 1).Cells(67).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY NY HAND"
            If frmDGV.DGVdata.Rows(i - 1).Cells(68).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "COLOUR AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(69).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "FLY IN"
            If frmDGV.DGVdata.Rows(i - 1).Cells(70).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "YARN AB"
            If frmDGV.DGVdata.Rows(i - 1).Cells(71).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "HIGH TENSION"
            If frmDGV.DGVdata.Rows(i - 1).Cells(72).Value = True Then DataGridView1.Rows(i - 1).Cells(5).Value = "LOW TENSION"





        Next

        DataGridView1.AllowUserToAddRows = False
        Label20.Text = frmDGV.DGVdata.Rows(0).Cells(52).Value
        Label21.Text = frmJobEntry.txtLotNumber.Text

    End Sub



    Private Sub btnResults_Click(sender As Object, e As EventArgs) Handles btnResults.Click


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

        Dim tmpReChk1, tmpRechk2, tmpDef As String



        For i = 1 To 32
            tmpReChk1 = DataGridView1.Rows(i - 1).Cells(2).Value
            tmpRechk2 = DataGridView1.Rows(i - 1).Cells(3).Value
            tmpDef = DataGridView1.Rows(i - 1).Cells(5).Value


            If tmpReChk1 = "*" Or tmpRechk2 = "*" Then 'tmpReChk1 = "Ok" And tmpRechk2 = "*" Or tmpReChk1 = "*" And tmpRechk2 = "OK" Or tmpReChk1 = "*" And tmpRechk2 = "*" Or tmpReChk1 = "*" And tmpRechk2 = "-" Or tmpReChk1 = "-" And tmpRechk2 = "*" Or tmpReChk1 = "*" And tmpRechk2 = "+" Or tmpReChk1 = "+" And tmpRechk2 = "*" Or tmpReChk1 = "+" And tmpRechk2 = "@" Or tmpReChk1 = "*" And tmpRechk2 = "@" Or tmpReChk1 = "@" And tmpRechk2 = "*" Then
                'Waste Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Black   'Grade Waste
                DataGridView1.Rows(i - 1).Cells(4).Value = "W"
            ElseIf tmpReChk1 = "@" Or tmpRechk2 = "@" Then 'tmpReChk1 = "OK" And tmpRechk2 = "@" Or tmpReChk1 = "@" And tmpRechk2 = "OK" Or tmpReChk1 = "@" And tmpRechk2 = "@" Or tmpReChk1 = "@" And tmpRechk2 = "-" Or tmpReChk1 = "-" And tmpRechk2 = "@" Or tmpReChk1 = "@" And tmpRechk2 = "+" Or tmpReChk1 = "+" And tmpRechk2 = "@" Then
                'AB (B) Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Red    'Grade Abnormal (B)
                DataGridView1.Rows(i - 1).Cells(4).Value = "B"

            ElseIf (tmpReChk1 = "OK" And tmpRechk2 = "OK") And tmpDef = "" Then
                'A Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.DarkBlue  'Grade A
                DataGridView1.Rows(i - 1).Cells(4).Value = "A"

            ElseIf (tmpReChk1 = "OK" And tmpRechk2 = "+" Or tmpReChk1 = "+" And tmpRechk2 = "OK" Or tmpReChk1 = "+" And tmpRechk2 = "+") And tmpDef = "" Then
                'AD Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Green    'Grade AD
                DataGridView1.Rows(i - 1).Cells(4).Value = "AD"
            ElseIf (tmpReChk1 = "OK" And tmpRechk2 = "-" Or tmpReChk1 = "-" And tmpRechk2 = "OK" Or tmpReChk1 = "-" And tmpRechk2 = "-") And tmpDef = "" Then
                'AL Grade
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Blue   'Grade AL
                DataGridView1.Rows(i - 1).Cells(4).Value = "AL"
            ElseIf tmpReChk1 = "-" And tmpRechk2 = "+" Or tmpReChk1 = "+" And tmpRechk2 = "-" Then
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Red    'If any Cheese has a defect set to Grade Abnormal (B)
                DataGridView1.Rows(i - 1).Cells(4).Value = "ERROR"
            Else
                DataGridView1.Rows(i - 1).Cells(4).Style.ForeColor = Color.Red    'Grade Abnormal (B)
                DataGridView1.Rows(i - 1).Cells(4).Value = "B"

            End If




        Next


        btnFinish.Visible = True

    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click



        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")

        'CHECK TO SEE IF DATE ALREADY SET FOR END TIME

        If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("RECHKENDTM").Value) Then
            For i As Integer = 1 To 32
                frmDGV.DGVdata.Rows(i - 1).Cells("RECHKENDTM").Value = today 'COLOUR CHECK END TIME
            Next
        End If



        For i = 1 To 32
            If DataGridView1.Rows(i - 1).Cells(5).Value = "KEBA" Then frmDGV.DGVdata.Rows(i - 1).Cells(37).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY" Then frmDGV.DGVdata.Rows(i - 1).Cells(38).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "FORM AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(39).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "OVERTHROWN" Then frmDGV.DGVdata.Rows(i - 1).Cells(40).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "TENSION AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(41).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "PAPERTUBE AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(42).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "SHORT CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(43).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "X MISSING CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(44).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "NO TAIL & ABNORMAL" Then frmDGV.DGVdata.Rows(i - 1).Cells(45).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "HITTING" Then frmDGV.DGVdata.Rows(i - 1).Cells(46).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "TARUMI" Then frmDGV.DGVdata.Rows(i - 1).Cells(47).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "B GRADE BY M/C" Then frmDGV.DGVdata.Rows(i - 1).Cells(48).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "C GRADE BY MACHINE" Then frmDGV.DGVdata.Rows(i - 1).Cells(49).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "BARRE" Then frmDGV.DGVdata.Rows(i - 1).Cells(66).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY OIL" Then frmDGV.DGVdata.Rows(i - 1).Cells(67).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "DIRTY NY HAND" Then frmDGV.DGVdata.Rows(i - 1).Cells(68).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "COLOUR AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(69).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "FLY IN" Then frmDGV.DGVdata.Rows(i - 1).Cells(70).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "YARN AB" Then frmDGV.DGVdata.Rows(i - 1).Cells(71).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "HIGH TENSION" Then frmDGV.DGVdata.Rows(i - 1).Cells(72).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "LOW TENSION" Then frmDGV.DGVdata.Rows(i - 1).Cells(73).Value = True
            If DataGridView1.Rows(i - 1).Cells(5).Value = "LOW TENSION" Then frmDGV.DGVdata.Rows(i - 1).Cells(74).Value = True


            'list of Array Fields to Update

            frmDGV.DGVdata.Rows(i - 1).Cells(57).Value = frmJobEntry.varUserName  'operatorName   fron entry screen


            If DataGridView1.Rows(i - 1).Cells(5).Value = "SHORT CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(10).Value = 1 'shortCone
            If DataGridView1.Rows(i - 1).Cells(5).Value = "X MISSING CHEESE" Then frmDGV.DGVdata.Rows(i - 1).Cells(11).Value = 1  'missingCone
            If DataGridView1.Rows(i - 1).Cells(5).Value = "BARRE" Then frmDGV.DGVdata.Rows(i - 1).Cells(16).Value = 1 'Cone with large colour defect

            If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("RECHKENDTM").Value) Then
                frmDGV.DGVdata.Rows(i - 1).Cells("RECHKENDTM").Value = today 'COLOUR CHECK END TIME
            End If


            frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = 2   'Cone has been reChecked  so can be packed

            'CHECK reCheck1
            Select Case DataGridView1.Rows(i - 1).Cells(2).Value

                Case "OK"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "A"
                Case "L"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "AL"
                Case "D"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "AD"

                Case "B"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "B"
                Case "W"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = "W"
            End Select
            'CHECK reCheck2
            Select Case DataGridView1.Rows(i - 1).Cells(2).Value

                Case "OK"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = "A"
                Case "L"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK12").Value = "AL"
                Case "D"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = "AD"
                Case "B"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = "B"
                Case "W"
                    frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = "W"
            End Select


            ' frmDGV.DGVdata.Rows(i - 1).Cells("RECHK1").Value = DataGridView1.Rows(i - 1).Cells(2).Value 'WRITE RECHEECK RESULT TO DGV
            'frmDGV.DGVdata.Rows(i - 1).Cells("RECHK2").Value = DataGridView1.Rows(i - 1).Cells(3).Value 'WRITE RECHEECK RESULT TO DGV
            frmDGV.DGVdata.Rows(i - 1).Cells("RECHKRESULT").Value = DataGridView1.Rows(i - 1).Cells(4).Value 'WRITE RECHEECK RESULT TO DGV
            frmDGV.DGVdata.Rows(i - 1).Cells("RECHKDEFCODE").Value = DataGridView1.Rows(i - 1).Cells(5).Value 'WRITE RECHEECK RESULT TO DGV


        Next


        'printSheet()




        ' UpdateDatabase()


        'If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        'frmDGV.DGVdata.ClearSelection()
        'frmJobEntry.Show()
        'frmJobEntry.txtLotNumber.Clear()
        'frmJobEntry.txtLotNumber.Focus()
        'Me.Cursor = System.Windows.Forms.Cursors.Default
        'Me.Close()


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








        'UPDATE THE EXCEL SHEET FOR THIS JOB

        Dim MyReCheckExcel As New Excel.Application
        Dim ReCheckworkbook As Excel.Workbook

        MsgBox("about to open Excel")
        ReCheckworkbook = MyReCheckExcel.Workbooks.Open(savename) '.Sheets(SheetNum)



        'CHECK TO SEE IF THERE IS ALREADY A FILE STARTED FOR PRODUCT NUMBER
        'IN TODATY DIRECTORY
        Try
            If File.Exists(savename) Then


                For i = 1 To 32
                    MyReCheckExcel.Cells(i - 1, 4) = DataGridView1.Rows(i - 1).Cells(2).Value
                    MyReCheckExcel.Cells(i - 1, 5) = DataGridView1.Rows(i - 1).Cells(3).Value
                    MyReCheckExcel.Cells(i - 1, 6) = DataGridView1.Rows(i - 1).Cells(4).Value
                    MyReCheckExcel.Cells(i - 1, 7) = DataGridView1.Rows(i - 1).Cells(5).Value
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Try

            'Save changes to new file in Paking Dir
            MyReCheckExcel.DisplayAlerts = False
            ReCheckworkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

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



    End Sub


End Class