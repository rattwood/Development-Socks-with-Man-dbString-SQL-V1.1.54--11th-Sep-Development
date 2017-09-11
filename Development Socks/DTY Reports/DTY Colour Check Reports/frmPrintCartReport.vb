Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel





Public Class frmPrintCartReport
    Dim sp_nums As String
    Dim template As String
    Dim FileInName As String
    Dim MyExcel As New Excel.Application
    Dim abortPrint As Integer = 0



    Public Sub prtCartSheet()


        'Left Side Rows and Columns on Excel Sheet
        Dim exNcRw As Integer = 9
        Dim exNcCl As Integer = 2
        Dim exM30Rw As Integer = 19  '14 was original on old form
        Dim exM30Cl As Integer = 2
        Dim exP30Rw As Integer = 14   '19 was original on old form
        Dim exP30Cl As Integer = 2
        Dim exABRw As Integer = 24
        Dim exABCl As Integer = 2
        Dim exDfRw As Integer = 29
        Dim exDfCl As Integer = 2

        'Right Side Rows and Columns on Excel Sheet
        Dim ex0Rw As Integer = 9
        Dim ex0Cl As Integer = 12
        Dim exM10Rw As Integer = 14
        Dim exM10Cl As Integer = 12
        Dim exP10Rw As Integer = 19
        Dim exP10Cl As Integer = 12
        Dim exM50Rw As Integer = 24
        Dim exM50Cl As Integer = 12
        Dim exP50Rw As Integer = 29
        Dim exP50Cl As Integer = 12

        Dim exNcVal = 0
        Dim exWasVal As String = 0
        Dim exM30Val = 0
        Dim exP30Val = 0
        Dim exABVal = 0
        Dim exDfVal = 0
        Dim ex0Val = 0
        Dim exM10Val = 0
        Dim exP10Val = 0
        Dim exM50Val = 0
        Dim exP50Val = 0



        Dim savename As String

        template = (My.Settings.dirTemplate & "\" & "CartReportTemplate.xlsx").ToString

        Dim ExcelApp As New Excel.Application
        Dim workbook As Excel.Workbook
        Dim sheet As Excel.Worksheet

        Dim saveString As String
        Dim sp_nums As String

        saveString = DGVcartReport.Rows(0).Cells(53).Value.ToString  'gets the BCODEJOB Value

        savename = (My.Settings.dirCarts & "\" & saveString & ".xlsx").ToString


        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Me.Close()
            frmJobEntry.Show()
        End If

        'Call IsFileOpen(New FileInfo(savename))


        If abortprint Then
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Visible = True
            frmJobEntry.txtLotNumber.Focus()
            frmJobEntry.cartReport = 0
            frmJobEntry.txtBoxCartReport.Text = ""
            frmJobEntry.Show()
            Me.Close()
            Exit Sub
        End If


        'Make worksheet visible
        ' MyExcel.Visible = True


        If frmJobEntry.varMachineCode = 21 Or frmJobEntry.varMachineCode = 23 Or frmJobEntry.varMachineCode = 25 Then sp_nums = "1 - 192"
        If frmJobEntry.varMachineCode = 22 Or frmJobEntry.varMachineCode = 24 Or frmJobEntry.varMachineCode = 26 Then sp_nums = "193 - 384"



        workbook = MyExcel.Workbooks.Open(template)

        'MyExcel.Visible = True

        'Date and Time
        MyExcel.Cells(3, 18) = DateAndTime.Now.ToString("dd MM yyy")
        'MachineName
        MyExcel.Cells(4, 2) = DGVcartReport.Rows(0).Cells(51).Value
        'ProductName
        MyExcel.Cells(4, 5) = DGVcartReport.Rows(0).Cells(52).Value
        'MERGE #
        MyExcel.Cells(4, 9) = DGVcartReport.Rows(0).Cells(7).Value

        'DoffingNum
        MyExcel.Cells(4, 12) = DGVcartReport.Rows(0).Cells(5).Value
        'sp_nums RANGE
        'sp_nums = ((DGVcartReport.Rows(0).Cells(6).Value) & "-" & DGVcartReport.Rows(191).Cells(6).Value)
        'MyExcel.Cells(4, 15) = sp_nums
        'Machine number from Barcode
        MyExcel.Cells(6, 6) = DGVcartReport.Rows(0).Cells(1).Value

        If frmJobEntry.varProdWeight = Nothing Then frmJobEntry.varProdWeight = 0
        'PRODUCT WEIGHT
        MyExcel.Cells(4, 18) = frmJobEntry.varProdWeight.ToString



        Dim missCount As Integer = 0   'VAR TO COUNT MISSING CONES
        Dim JudCount As Integer = 0
        Dim gradeACount, gradeASCount, gradeDefCount As Integer

        For dgvRW As Integer = 1 To frmJobEntry.LRecordCount

            'Routine for Spindles

            'line to get row/colum dat from DatGridView for NoCone and write to Excel Sheet 
            exNcVal = DGVcartReport.Rows(dgvRW - 1).Cells(11).Value  'Missing Cheese

            If exNcVal > 0 Then
                MyExcel.Cells(exNcRw, exNcCl) = exNcVal
                If exNcRw = 12 Then
                    exNcCl = exNcCl + 1
                    missCount = missCount + 1
                    exNcRw = 9
                Else
                    exNcRw = exNcRw + 1
                    missCount = missCount + 1
                End If
            End If

            'LINE TO GET WASTE CONES FOUND IN SORTING AND REPORT IN MISSING CONE SECTION

            If DGVcartReport.Rows(dgvRW - 1).Cells(46).Value = True Then  'CHECK FOR WATE CONE CHECKED
                exWasVal = DGVcartReport.Rows(dgvRW - 1).Cells(6).Value.ToString  'GET CONE NUMBER

                If exWasVal > 0 Then
                    MyExcel.Cells(exNcRw, exNcCl) = (exWasVal + "W")
                    If exNcRw = 12 Then
                        exNcCl = exNcCl + 1
                        missCount = missCount + 1
                        exNcRw = 9
                    Else
                        exNcRw = exNcRw + 1
                        missCount = missCount + 1
                    End If
                End If
            End If


            'line to get row/colum dat from DatGridView for M30 and write to Excel Sheet 
            exM30Val = DGVcartReport.Rows(dgvRW - 1).Cells(19).Value
            If exM30Val > 0 Then
                MyExcel.Cells(exM30Rw, exM30Cl) = exM30Val
                If DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then MyExcel.Cells(exM30Rw, exM30Cl).interior.color = Color.LightSalmon
                If exM30Rw = 22 Then   'Original value 17
                    exM30Cl = exM30Cl + 1
                    JudCount = JudCount + 1
                    exM30Rw = 19    'Original value 14
                Else
                    exM30Rw = exM30Rw + 1
                    JudCount = JudCount + 1
                End If
            End If

            'line to get row/colum dat from DatGridView for P30 and write to Excel Sheet 
            exP30Val = DGVcartReport.Rows(dgvRW - 1).Cells(20).Value
            If exP30Val > 0 Then
                MyExcel.Cells(exP30Rw, exP30Cl) = exP30Val
                If DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then MyExcel.Cells(exP30Rw, exP30Cl).interior.color = Color.LightSalmon
                If exP30Rw = 17 Then    'Original 22
                    exP30Cl = exP30Cl + 1
                    JudCount = JudCount + 1
                    exP30Rw = 14    'Original 19
                Else
                    exP30Rw = exP30Rw + 1
                    JudCount = JudCount + 1
                End If
            End If

            'line to get row/colum dat from DatGridView for AB (Barley) Or Defect Cones and write to Excel Sheet 
            If DGVcartReport.Rows(dgvRW - 1).Cells(16).Value > 0 Then   'Barley
                exABVal = DGVcartReport.Rows(dgvRW - 1).Cells(6).Value
                'ElseIf DGVcartReport.Rows(dgvRW - 1).Cells(49).Value = True Then 'GRADE B
                'exABVal = DGVcartReport.Rows(dgvRW - 1).Cells(6).Value
            ElseIf DGVcartReport.Rows(dgvRW - 1).Cells(21).Value > 0 Then  'M50
                exABVal = DGVcartReport.Rows(dgvRW - 1).Cells(6).Value
            ElseIf DGVcartReport.Rows(dgvRW - 1).Cells(22).Value > 0 Then  'P50
                exABVal = DGVcartReport.Rows(dgvRW - 1).Cells(6).Value
            End If


            If exABVal > 0 Then
                MyExcel.Cells(exABRw, exABCl) = exABVal
                If DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then MyExcel.Cells(exABRw, exABCl).interior.color = Color.LightSalmon
                If exABRw = 27 Then
                    exABCl = exABCl + 1
                    JudCount = JudCount + 1
                    exABRw = 24
                Else
                    exABRw = exABRw + 1
                    JudCount = JudCount + 1
                End If
            End If



            'line to get row/colum dat from DatGridView for Waste (Dyefect) > 50 and write to Excel Sheet 


            If DGVcartReport.Rows(dgvRW - 1).Cells(66).Value > 0 Then
                exDfVal = DGVcartReport.Rows(dgvRW - 1).Cells(6).Value ' Colour Waste

            End If




            If exDfVal > 0 Then
                MyExcel.Cells(exDfRw, exDfCl) = exDfVal
                If DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then MyExcel.Cells(exDfRw, exDfCl).interior.color = Color.LightSalmon
                If exDfRw = 32 Then
                    exDfCl = exDfCl + 1
                    JudCount = JudCount + 1
                    exDfRw = 29
                Else
                    exDfRw = exDfRw + 1
                    JudCount = JudCount + 1
                End If
            End If



            'line to get row/colum dat from DatGridView for Zero and write to Excel Sheet 
            'ex0Val = DGVcartReport.Rows(dgvRW - 1).Cells(15).Value
            'If ex0Val > 0 Then
            '    MyExcel.Cells(ex0Rw, ex0Cl) = ex0Val
            '    If DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then MyExcel.Cells(ex0Rw, ex0Cl).interior.color = Color.LightSalmon
            '    If ex0Rw = 12 Then
            '        ex0Cl = ex0Cl + 1
            '        JudCount = JudCount + 1
            '        ex0Rw = 9
            '    Else
            '        ex0Rw = ex0Rw + 1
            '        JudCount = JudCount + 1
            '    End If
            'End If

            ''line to get row/colum dat from DatGridView for M10 and write to Excel Sheet 
            'exM10Val = DGVcartReport.Rows(dgvRW - 1).Cells(17).Value
            'If exM10Val > 0 Then
            '    MyExcel.Cells(exM10Rw, exM10Cl) = exM10Val
            '    If DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then MyExcel.Cells(exM10Rw, exM10Cl).interior.color = Color.LightSalmon
            '    If exM10Rw = 17 Then
            '        exM10Cl = exM10Cl + 1
            '        JudCount = JudCount + 1
            '        exM10Rw = 14
            '    Else
            '        exM10Rw = exM10Rw + 1
            '        JudCount = JudCount + 1
            '    End If
            'End If

            ''line to get row/colum dat from DatGridView for P10 and write to Excel Sheet 
            'exP10Val = DGVcartReport.Rows(dgvRW - 1).Cells(18).Value
            'If exP10Val > 0 Then
            '    MyExcel.Cells(exP10Rw, exP10Cl) = exP10Val
            '    If DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then MyExcel.Cells(exP10Rw, exP10Cl).interior.color = Color.LightSalmon
            '    If exP10Rw = 22 Then
            '        exP10Cl = exP10Cl + 1
            '        JudCount = JudCount + 1
            '        exP10Rw = 19
            '    Else
            '        exP10Rw = exP10Rw + 1
            '        JudCount = JudCount + 1
            '    End If
            'End If

            ''line to get row/colum dat from DatGridView for M50 and write to Excel Sheet 
            'exM50Val = DGVcartReport.Rows(dgvRW - 1).Cells(21).Value
            'If exM50Val > 0 Then
            '    MyExcel.Cells(exM50Rw, exM50Cl) = exM50Val
            '    If DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then MyExcel.Cells(exM50Rw, exM50Cl).interior.color = Color.LightSalmon
            '    If exM50Rw = 27 Then
            '        exM50Cl = exM50Cl + 1
            '        JudCount = JudCount + 1
            '        exM50Rw = 24
            '    Else
            '        exM50Rw = exM50Rw + 1
            '        JudCount = JudCount + 1
            '    End If
            'End If

            ''line to get row/colum dat from DatGridView for P50 and write to Excel Sheet 
            'exP50Val = DGVcartReport.Rows(dgvRW - 1).Cells(22).Value
            'If exP50Val > 0 Then
            '    MyExcel.Cells(exP50Rw, exP50Cl) = exP50Val
            '    If DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then MyExcel.Cells(exP50Rw, exP50Cl).interior.color = Color.LightSalmon
            '    If exP50Rw = 32 Then
            '        exP50Cl = exP50Cl + 1
            '        JudCount = JudCount + 1
            '        exP50Rw = 29
            '    Else
            '        exP50Rw = exP50Rw + 1
            '        JudCount = JudCount + 1
            '    End If
            'End If

            'COUNT GRADE A CONES
            If DGVcartReport.Rows(dgvRW - 1).Cells(9).Value = 9 And DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = False Or
                    DGVcartReport.Rows(dgvRW - 1).Cells(9).Value = 15 And DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = False Then gradeACount = gradeACount + 1
            'COUNT GRADE AS CONES
            If DGVcartReport.Rows(dgvRW - 1).Cells(9).Value = 9 And DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Or
                DGVcartReport.Rows(dgvRW - 1).Cells(9).Value = 15 And DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Then gradeASCount = gradeASCount + 1



            'COUNT SORT DEFECT CONES
            If DGVcartReport.Rows(dgvRW - 1).Cells(12).Value > 0 And DGVcartReport.Rows(dgvRW - 1).Cells(16).Value = 0 Then
                If DGVcartReport.Rows(dgvRW - 1).Cells(37).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(38).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(39).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(40).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(41).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(42).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(43).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(44).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(45).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(46).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(47).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(48).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(49).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(50).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(67).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(68).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(69).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(70).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(71).Value = True Or DGVcartReport.Rows(dgvRW - 1).Cells(72).Value = True Or
                        DGVcartReport.Rows(dgvRW - 1).Cells(73).Value = True Then

                    gradeDefCount = gradeDefCount + 1
                End If
            End If


            exNcVal = 0
            ex0Val = 0
            exM10Val = 0
            exP10Val = 0
            exM30Val = 0
            exP30Val = 0
            exM50Val = 0
            exP50Val = 0
            exABVal = 0
            exDfVal = 0




        Next

        'TOTALMISSING CONES
        MyExcel.Cells(6, 18) = JudCount
        'TOTAL OF CONES ON CART  192 LESS MISSING CONES
        MyExcel.Cells(8, 19) = (frmJobEntry.LRecordCount - missCount)
        'TOTAL OF GRADE A FULL CONES
        MyExcel.Cells(35, 9) = gradeACount
        'TOTAL SHORT GRADE A CONES
        MyExcel.Cells(36, 9) = gradeASCount
        'TOTAL SORT DEFECT CONES
        MyExcel.Cells(37, 9) = gradeDefCount

        'Routine to get the product weight
        Dim prNum As String = DGVcartReport.Rows(0).Cells(2).Value.ToString
        frmJobEntry.LExecQuery("SELECT * FROM product WHERE PRNUM = '" & prNum & "' ")
        'LOAD THE DATA FROM dB IN TO THE DATAGRID
        frmDGV.DGVdata.DataSource = frmJobEntry.LDS.Tables(0)
        frmDGV.DGVdata.Rows(0).Selected = True
        'CHEESE WEIGHT
        MyExcel.Cells(4, 18) = frmDGV.DGVdata.Rows(0).Cells(11).Value


        Try

            'Save changes to new file in CKCarts
            MyExcel.DisplayAlerts = False
            workbook.SaveAs(Filename:=savename, FileFormat:=51)
            MyExcel.DisplayAlerts = True
        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it

            workbook.Close(SaveChanges:=False)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyExcel.Quit()

        releaseObject(sheet)
        releaseObject(workbook)
        releaseObject(MyExcel)


        MsgBox("Job Report " & savename & " Created")

        frmJobEntry.txtLotNumber.Visible = True
        frmJobEntry.txtLotNumber.Focus()
        frmJobEntry.cartReport = 0
        frmJobEntry.txtBoxCartReport.Text = ""

        Me.Close()
        Exit Sub
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

    Private Sub IsFileOpen(ByVal file As FileInfo)
        Dim stream As FileStream = Nothing
        Try
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            stream.Close()
        Catch ex As Exception


            ' do something here, either close the file if you have a handle, show a msgbox, retry  or as a last resort terminate the process - which could cause corruption and lose data
            MsgBox("Excel file is Open, Please close and retry")

            MyExcel.DisplayAlerts = False
            MyExcel.Quit()
            abortPrint = 1

        End Try

    End Sub


End Class