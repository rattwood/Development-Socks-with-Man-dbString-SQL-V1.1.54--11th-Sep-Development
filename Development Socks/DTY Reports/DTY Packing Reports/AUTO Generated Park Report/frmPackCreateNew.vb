Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmPackCreateNew
    'Dim MyCrExcel As New Excel.Application
    Dim SheetCodeString As String



    Public Sub CreateNew()
        Dim MyPakExcel As New Excel.Application
        Dim boxCount As Integer = 0
        Dim nfree As Integer  'This will be container for the next row free  
        Dim ncfree As Integer 'This will be container for the next column free  
        Dim colcount As Integer
        Dim xlWorkbook As Excel.Workbook
        Dim xlSheets As Excel.Worksheet


        'OPEN A NEW WORKSHEET
        xlWorkbook = MyPakExcel.Workbooks.Open(frmPackRepMain.template)
        'ReName the work sheet 
        CType(MyPakExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName


        'CREATE CORRECT HEADER FOR SHEET
        Select Case frmJobEntry.txtGrade.Text
            Case "A", "B", "AL", "AD"
                nfree = 13
                'Product Name
                MyPakExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value       'D7
                'Product Code
                MyPakExcel.Cells(7, 6) = frmDGV.DGVdata.Rows(0).Cells(2).Value        'F7
                'DATE
                MyPakExcel.Cells(5, 3) = Date.Now.ToString("dd_MM_yyyy")              'C5
                'CHEESE WEIGHT
                MyPakExcel.Cells(13, 5) = frmJobEntry.varProdWeight                   'E13
                'PACKER NAME
                MyPakExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value      'H13

                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                If frmPackPrvGet.nfree > 0 Then
                    nfree = frmPackPrvGet.nfree
                    For usedrow = 13 To nfree - 1
                        MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                    Next

                End If



            Case "P35 AS", "P35 BS"
                nfree = 12
                'Product Name
                MyPakExcel.Cells(6, 8) = frmDGV.DGVdata.Rows(0).Cells(52).Value       'H6
                'Product Code
                MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells(2).Value       'L6
                'DATE
                MyPakExcel.Cells(5, 4) = Date.Now.ToString("dd_MM_yyyy")              'D5
                'CHEESE WEIGHT
                MyPakExcel.Cells(12, 5) = frmJobEntry.varProdWeight                   'E12
                'PACKER NAME
                MyPakExcel.Cells(43, 4) = frmDGV.DGVdata.Rows(0).Cells(55).Value      'D43

                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                Select Case frmPackPrvGet.ncfree
                    Case 12
                        'This will write date to the first two cone columns
                        colcount = 4
                        For ccount = 1 To 2
                            For rcount = 11 To 41
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 11 To nfree - 1
                                MyPakExcel.Cells(usedrow, 12) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 8
                        'This will write date to the first One cone columns
                        For rcount = 11 To 41
                            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                        Next


                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 11 To nfree - 1
                                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 4

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 11 To nfree - 1
                                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                            Next
                        End If
                End Select

                createBarcode()
                MyPakExcel.Cells(1, 4) = SheetCodeString



            Case "P25 AS", "P30 BS"
                nfree = 13
                'Product Name
                MyPakExcel.Cells(6, 8) = frmDGV.DGVdata.Rows(0).Cells(52).Value       'H6
                'Product Code
                MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells(2).Value       'L6
                'DATE
                MyPakExcel.Cells(5, 4) = Date.Now.ToString("dd_MM_yyyy")              'D5
                'CHEESE WEIGHT
                MyPakExcel.Cells(12, 5) = frmJobEntry.varProdWeight                   'E12
                'PACKER NAME
                MyPakExcel.Cells(53, 4) = frmDGV.DGVdata.Rows(0).Cells(55).Value      'D53


                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                Select Case frmPackPrvGet.ncfree
                    Case 12
                        'This will write date to the first two cone columns
                        colcount = 4
                        For ccount = 1 To 2
                            For rcount = 11 To 51
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 11 To nfree - 1
                                MyPakExcel.Cells(usedrow, 12) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 8
                        'This will write date to the first One cone columns
                        For rcount = 11 To 51
                            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                        Next


                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 11 To nfree - 1
                                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 4

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 11 To nfree - 1
                                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                            Next
                        End If
                End Select


                createBarcode()
                MyPakExcel.Cells(1, 4) = SheetCodeString

            Case "P15 AS", "P20 BS"
                nfree = 14
                'Product Name
                MyPakExcel.Cells(7, 9) = frmDGV.DGVdata.Rows(0).Cells(52).Value       'E8
                'Product Code
                MyPakExcel.Cells(7, 14) = frmDGV.DGVdata.Rows(0).Cells(2).Value       'N8
                'DATE
                MyPakExcel.Cells(5, 4) = Date.Now.ToString("dd_MM_yyyy")              'D6
                'CHEESE WEIGHT
                MyPakExcel.Cells(14, 5) = frmJobEntry.varProdWeight                   'E13
                'BARCODE IN
                MyPakExcel.Cells(54, 17) = frmDGV.DGVdata.Rows(0).Cells(55).Value      'P55


                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                Select Case frmPackPrvGet.ncfree
                    Case 16
                        'This will write date to the first three cone columns
                        colcount = 4
                        For ccount = 1 To 3
                            For rcount = 13 To 66
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 13 To nfree - 1
                                MyPakExcel.Cells(usedrow, 16) = frmPackRepMain.prevDays
                            Next

                        End If

                    Case 12
                        colcount = 4
                        For ccount = 1 To 2
                            For rcount = 13 To 66
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 13 To nfree - 1
                                MyPakExcel.Cells(usedrow, 16) = frmPackRepMain.prevDays
                            Next

                        End If


                    Case 8
                        'This will write date to the first One cone columns
                        For rcount = 13 To 66
                            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                        Next


                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 13 To nfree - 1
                                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 4

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 13 To nfree - 1
                                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                            Next
                        End If
                End Select


                createBarcode()
                MyPakExcel.Cells(1, 4) = SheetCodeString

            Case "ReCheck"
                nfree = 9
                        'Product Name
                        MyPakExcel.Cells(5, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value       'D5
                        'Product Code
                        MyPakExcel.Cells(5, 7) = frmDGV.DGVdata.Rows(0).Cells(2).Value       'G5
                        'DATE
                        MyPakExcel.Cells(4, 7) = Date.Now.ToString("dd_MM_yyyy")              'G4
                        'CHEESE WEIGHT
                        MyPakExcel.Cells(4, 5) = frmJobEntry.varProdWeight                   'E4
                'PACKER NAME
                MyPakExcel.Cells(42, 3) = frmDGV.DGVdata.Rows(0).Cells(55).Value      'D53

                createBarcode()
                MyPakExcel.Cells(1, 3) = SheetCodeString

        End Select


                If boxCount = 0 Then boxCount = 1


        'THIS IS USED TO WRITE DATE IN TO USED ROWS
        If frmPackPrvGet.nfree > 0 Then
            nfree = frmPackPrvGet.nfree
            For usedrow = 13 To nfree - 1
                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
            Next

        End If

        'SAVE THE FILE (THIS FILE WILL NOT HAVE ANY CONES ADDED TO IT)
        Try

            'Save changes to new file in Paking Dir
            MyPakExcel.DisplayAlerts = False
            xlWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'CLOSE THE TEMPLATE FILE 
        Try
            'Save changes to new file in Paking Dir
            MyPakExcel.DisplayAlerts = False
            xlWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'CLEAN UP
        MyPakExcel.Quit()
        releaseObject(xlSheets)
        releaseObject(xlWorkbook)
        releaseObject(MyPakExcel)


        Select Case frmJobEntry.txtGrade.Text
                Case "A"
                    frmPackTodayUpdate.TodayUpdate()
                Case "B", "AL", "AD"
                    frmPackTodayUpdate.TodayUpdateB_AL_AD()
                Case "P35 AS", "P35 BS"
                    frmPackTodayUpdate.TodatUpdateBS_AS_35()
                Case "P25 AS", "P30 BS"
                    frmPackTodayUpdate.TodayUpdateBS_AS_30()
                Case "P15 AS", "P20 BS"
                    frmPackTodayUpdate.TodayUpdateBS_AS_20()
                Case "ReCheck"
                    frmPackTodayUpdate.todayUpdate_ReCheck()
            End Select





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

    Public Sub createBarcode()

        Dim today As String = Date.Now
        Dim day As String
        Dim month As String
        Dim year As String
        Dim gradeTxt As String

        'Routine to get date brocken down
        today = Convert.ToDateTime(today).ToString("dd-MM-yyyy")
        day = today.Substring(0, 2)
        month = today.Substring(3, 2)
        year = today.Substring(6, 4)

        gradeTxt = UCase(frmJobEntry.txtGrade.Text)


        SheetCodeString = ("*" & frmJobEntry.varProductCode & year & month & day & frmPackRepMain.sheetName & gradeTxt & "0" & "*")

    End Sub

End Class