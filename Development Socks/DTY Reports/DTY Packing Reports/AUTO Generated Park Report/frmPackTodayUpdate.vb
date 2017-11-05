Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmPackTodayUpdate

    Dim MyTodyExcel As New Excel.Application
    Dim xlRowCount As Integer
    Dim mycount As Integer = 0
    Dim boxCount As Integer = 0
    Dim nfree As Integer = 13
    Dim toAlocate As Integer
    Dim nCol As Integer
    Dim ncfree As Integer
    Dim SheetCodeString As String




    Public Sub TodayUpdate()

        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet


        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        boxCount = mycount

        Dim totCount As Integer = 1
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET
        For rcount = 13 To 102
            If MyTodyExcel.Cells(rcount, 4).Value > 0 Then
                totCount = totCount = 1
                Continue For
            Else
                nfree = rcount
                Exit For
            End If
        Next


        'CHECK TO SEE IF THE NEW CURRENT SHEET IS FULL IF SO ADD A NEW SHEET
        If totCount = 90 Then
            xlTodyWorkbook.Sheets(1).Copy(After:=xlTodyWorkbook.Sheets(mycount))
            'ReName the work sheet 
            CType(MyTodyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

            nfree = 13


            'Product Name
            MyTodyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
            'Product Code
            MyTodyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
            'Packer Name
            MyTodyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value





            For i = 13 To 102
                MyTodyExcel.Cells(nfree, 4) = "" 'Clear the contents of cone cells
            Next
            boxCount = boxCount + 1
        End If


        'Routine to go through the rows and extract Grade A cones plus keep count
        Dim cartonNum As String = ""
        Dim cellNum As Integer


        Try

            For i = 1 To 32

                If frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "15" Then


                    'USED TO ALLOCATE BOX NUMBER USED WHEN PACKED
                    Select Case nfree
                        Case 13, 14, 15, 16, 17, 18
                            cartonNum = 1
                            cellNum = 13
                        Case 19, 20, 21, 22, 23, 24
                            cartonNum = 2
                            cellNum = 19
                        Case 25, 26, 27, 28, 29, 30
                            cartonNum = 3
                            cellNum = 25
                        Case 31, 32, 33, 34, 35, 36
                            cartonNum = 4
                            cellNum = 31
                        Case 37, 38, 39, 40, 41, 42
                            cartonNum = 5
                            cellNum = 37
                        Case 43, 44, 45, 46, 47, 48
                            cartonNum = 6
                            cellNum = 43
                        Case 49, 50, 51, 52, 53, 54
                            cartonNum = 7
                            cellNum = 49
                        Case 55, 56, 57, 58, 59, 60
                            cartonNum = 8
                            cellNum = 55
                        Case 61, 62, 63, 64, 65, 66
                            cartonNum = 9
                            cellNum = 61
                        Case 67, 68, 69, 70, 71, 72
                            cartonNum = 10
                            cellNum = 67
                        Case 73, 74, 75, 76, 77, 78
                            cartonNum = 11
                            cellNum = 73
                        Case 79, 80, 81, 82, 83, 84
                            cartonNum = 12
                            cellNum = 79
                        Case 85, 86, 87, 88, 89, 90
                            cartonNum = 13
                            cellNum = 85
                        Case 91, 92, 93, 94, 95, 96
                            cartonNum = 14
                            cellNum = 91
                        Case 97, 98, 99, 100, 101, 102
                            cartonNum = 15
                            cellNum = 97
                    End Select


                    cartonNum = (cartonNum & "-" & boxCount).ToString

                    'WRITE CONE NUMBER TO SHEET
                    MyTodyExcel.Cells(nfree, 4) = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value

                    'WRITE CARTON NUMBER TO SHEET AND PUT IN DGV
                    MyTodyExcel.Cells(cellNum, 2) = cartonNum
                    frmDGV.DGVdata.Rows(i - 1).Cells(61).Value = cartonNum
                    nfree = nfree + 1

                    'ROUTINE IF SHEET IS FULL TO COPY SHEET AND CREATE A NEW SHEET IN THE WORKBOOK
                    If nfree = 103 Then
                        Dim tmpsaveName As String

                        tmpsaveName = (frmPackRepMain.finPath & "\" & frmPackRepMain.sheetName & "_" & mycount & ".xlsx")
                        MyTodyExcel.DisplayAlerts = False
                        xlTodyWorkbook.Sheets(mycount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)

                        MyTodyExcel.DisplayAlerts = True

                        xlTodyWorkbook.Sheets(frmPackRepMain.sheetName).Copy(After:=xlTodyWorkbook.Sheets(mycount))
                        CType(MyTodyExcel.Workbooks(1).Worksheets(frmPackRepMain.sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

                        MyTodyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                        'Product Code
                        MyTodyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
                        'Packer Name
                        MyTodyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value

                        For x = 13 To 102
                            MyTodyExcel.Cells(x, 4) = "" 'Clear the contents of cone cells
                        Next

                        nfree = 13
                        boxCount = boxCount + 1
                    End If
                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
        Me.Close()

    End Sub


    Public Sub TodayUpdateB_AL_AD()

        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet


        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        boxCount = mycount

        Dim totCount As Integer = 1
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET
        For rcount = 13 To 102
            If MyTodyExcel.Cells(rcount, 4).Value > 0 Then
                totCount = totCount = 1
                Continue For
            Else
                nfree = rcount
                Exit For
            End If
        Next


        'CHECK TO SEE IF THE NEW CURRENT SHEET IS FULL IF SO ADD A NEW SHEET
        If totCount = 90 Then
            xlTodyWorkbook.Sheets(1).Copy(After:=xlTodyWorkbook.Sheets(mycount))
            'ReName the work sheet 
            CType(MyTodyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

            nfree = 13


            'Product Name
            MyTodyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
            'Product Code
            MyTodyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
            'Packer Name
            MyTodyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value





            For i = 13 To 102
                MyTodyExcel.Cells(nfree, 4) = "" 'Clear the contents of cone cells
            Next
            boxCount = boxCount + 1
        End If


        'Routine to go through the rows and extract Grade A cones plus keep count
        Dim cartonNum As String = ""
        Dim cellNum As Integer


        Try

            For i = 1 To frmDGV.DGVdata.Rows.Count

                If frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "8" And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then



                    'USED TO ALLOCATE BOX NUMBER USED WHEN PACKED
                    Select Case nfree
                        Case 13, 14, 15, 16, 17, 18
                            cartonNum = 1
                            cellNum = 13
                        Case 19, 20, 21, 22, 23, 24
                            cartonNum = 2
                            cellNum = 19
                        Case 25, 26, 27, 28, 29, 30
                            cartonNum = 3
                            cellNum = 25
                        Case 31, 32, 33, 34, 35, 36
                            cartonNum = 4
                            cellNum = 31
                        Case 37, 38, 39, 40, 41, 42
                            cartonNum = 5
                            cellNum = 37
                        Case 43, 44, 45, 46, 47, 48
                            cartonNum = 6
                            cellNum = 43
                        Case 49, 50, 51, 52, 53, 54
                            cartonNum = 7
                            cellNum = 49
                        Case 55, 56, 57, 58, 59, 60
                            cartonNum = 8
                            cellNum = 55
                        Case 61, 62, 63, 64, 65, 66
                            cartonNum = 9
                            cellNum = 61
                        Case 67, 68, 69, 70, 71, 72
                            cartonNum = 10
                            cellNum = 67
                        Case 73, 74, 75, 76, 77, 78
                            cartonNum = 11
                            cellNum = 73
                        Case 79, 80, 81, 82, 83, 84
                            cartonNum = 12
                            cellNum = 79
                        Case 85, 86, 87, 88, 89, 90
                            cartonNum = 13
                            cellNum = 85
                        Case 91, 92, 93, 94, 95, 96
                            cartonNum = 14
                            cellNum = 91
                        Case 97, 98, 99, 100, 101, 102
                            cartonNum = 15
                            cellNum = 97
                    End Select


                    cartonNum = (cartonNum & "-" & boxCount).ToString

                    'WRITE CONE NUMBER TO SHEET
                    ' MsgBox("I value = " & i & " Cone Number = " & frmDGV.DGVdata.Rows(i - 1).Cells(36).Value & " nfree Value = " & nfree)
                    MyTodyExcel.Cells(nfree, 4) = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value





                    'WRITE CARTON NUMBER (TraceNumber) TO SHEET AND PUT IN DGV
                    MyTodyExcel.Cells(cellNum, 2) = cartonNum
                    frmDGV.DGVdata.Rows(i - 1).Cells(61).Value = cartonNum
                    nfree = nfree + 1

                    'ROUTINE IF SHEET IS FULL TO COPY SHEET AND CREATE A NEW SHEET IN THE WORKBOOK
                    If nfree = 103 Then
                        Dim tmpsaveName As String

                        tmpsaveName = (frmPackRepMain.finPath & "\" & frmPackRepMain.sheetName & "_" & mycount & ".xlsx")
                        MyTodyExcel.DisplayAlerts = False
                        xlTodyWorkbook.Sheets(mycount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)

                        MyTodyExcel.DisplayAlerts = True

                        xlTodyWorkbook.Sheets(frmPackRepMain.sheetName).Copy(After:=xlTodyWorkbook.Sheets(mycount))
                        CType(MyTodyExcel.Workbooks(1).Worksheets(frmPackRepMain.sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

                        MyTodyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                        'Product Code
                        MyTodyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
                        'Packer Name
                        MyTodyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value

                        For x = 13 To 102
                            MyTodyExcel.Cells(x, 4) = "" 'Clear the contents of cone cells
                        Next

                        nfree = 13
                        boxCount = boxCount + 1
                    End If
                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
        Me.Close()

    End Sub

    Public Sub TodatUpdateBS_AS_35()
        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet


        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        boxCount = mycount

        Dim totCount As Integer
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET

        Dim colCount As Integer = 4
        Dim endloop As Integer

        For ccount = 1 To 3  'Three sets of columns
            For rcount = 12 To 41
                If MyTodyExcel.Cells(rcount, colCount).Value > 0 Then  'C9-C40
                    totCount = totCount + 1
                    Continue For
                Else
                    nfree = rcount
                    ncfree = colCount
                    endloop = 1
                    Exit For
                End If
            Next
            If endloop Then

                Exit For
            Else
                colCount = colCount + 4
            End If
        Next






        'CHECK TO SEE IF THE NEW CURRENT SHEET IS FULL IF SO ADD A NEW SHEET
        If totCount = 90 Then

            xlTodyWorkbook.Sheets(1).Copy(After:=xlTodyWorkbook.Sheets(mycount))
            'ReName the work sheet 
            'CType(MyTodyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

            nfree = 12


            'Product Name
            MyTodyExcel.Cells(6, 8) = frmDGV.DGVdata.Rows(0).Cells(52).Value
            'Product Code
            MyTodyExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells(2).Value
            'Packer Name
            MyTodyExcel.Cells(43, 4) = frmDGV.DGVdata.Rows(0).Cells(55).Value
            'Add Barcode to Sheet
            createBarcode()
            MyTodyExcel.Cells(1, 4) = SheetCodeString

            colCount = 4
            For ccount = 1 To 3
                For i = 12 To 41
                    MyTodyExcel.Cells(i, colCount) = "" 'Clear the contents of cone cells
                    MyTodyExcel.Cells(i, colCount - 2) = "" 'Clear the contents of Carton cells
                Next
                If colCount < 12 Then colCount = colCount + 4
            Next
            boxCount = boxCount + 1
            nfree = 12
            ncfree = 4
        End If


        'Routine to go through the rows and extract Grade A cones plus keep count
        Dim cartonNum As String = ""
        Dim cellNum As Integer


        Try

            For i = 1 To frmDGV.DGVdata.Rows.Count

                If frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "8" And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then



                    'USED TO ALLOCATE BOX NUMBER USED WHEN PACKED
                    Select Case nfree
                        Case 12, 13, 14, 15, 16, 17
                            If ncfree = 4 Then
                                cartonNum = 1
                                cellNum = 12
                            ElseIf ncfree = 8 Then
                                cartonNum = 6
                                cellNum = 12
                            ElseIf ncfree = 12 Then
                                cartonNum = 11
                                cellNum = 12
                            End If
                        Case 18, 19, 20, 21, 22, 23
                            If ncfree = 4 Then
                                cartonNum = 2
                                cellNum = 18
                            ElseIf ncfree = 8 Then
                                cartonNum = 7
                                cellNum = 18
                            ElseIf ncfree = 12 Then
                                cartonNum = 12
                                cellNum = 18
                            End If
                        Case 24, 25, 26, 27, 28, 29
                            If ncfree = 4 Then
                                cartonNum = 3
                                cellNum = 24
                            ElseIf ncfree = 8 Then
                                cartonNum = 8
                                cellNum = 24
                            ElseIf ncfree = 12 Then
                                cartonNum = 13
                                cellNum = 24
                            End If
                        Case 30, 31, 32, 33, 34, 35
                            If ncfree = 4 Then
                                cartonNum = 4
                                cellNum = 30
                            ElseIf ncfree = 8 Then
                                cartonNum = 9
                                cellNum = 30
                            ElseIf ncfree = 12 Then
                                cartonNum = 14
                                cellNum = 30
                            End If
                        Case 36, 37, 38, 39, 40, 41
                            If ncfree = 4 Then
                                cartonNum = 5
                                cellNum = 36
                            ElseIf ncfree = 8 Then
                                cartonNum = 10
                                cellNum = 36
                            ElseIf ncfree = 12 Then
                                cartonNum = 15
                                cellNum = 36
                            End If
                    End Select

                    'cartonNum = (cartonNum & "-" & boxCount).ToString  'Box then sheet number
                    cartonNum = (boxCount & "-" & cartonNum).ToString  'Sheet then Box number

                    'WRITE CONE NUMBER TO SHEET

                    MyTodyExcel.Cells(nfree, ncfree) = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value





                    'WRITE CARTON NUMBER TO SHEET AND PUT IN DGV
                    MyTodyExcel.Cells(cellNum, ncfree - 2) = cartonNum
                    frmDGV.DGVdata.Rows(i - 1).Cells(61).Value = cartonNum
                    nfree = nfree + 1
                    'Increment the Col Number
                    If nfree = 42 And ncfree < 12 Then
                        ncfree = ncfree + 4
                        nfree = 12
                    End If

                    'ROUTINE IF SHEET IS FULL TO COPY SHEET AND CREATE A NEW SHEET IN THE WORKBOOK
                    If nfree = 42 And ncfree = 12 Then
                        Dim tmpsaveName As String

                        tmpsaveName = (frmPackRepMain.finPath & "\" & frmPackRepMain.sheetName & "_" & mycount & ".xlsx")
                        MyTodyExcel.DisplayAlerts = False
                        xlTodyWorkbook.Sheets(mycount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)

                        MyTodyExcel.DisplayAlerts = True

                        xlTodyWorkbook.Sheets(frmPackRepMain.sheetName).Copy(After:=xlTodyWorkbook.Sheets(mycount))
                        CType(MyTodyExcel.Workbooks(1).Worksheets(frmPackRepMain.sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

                        MyTodyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                        'Product Code
                        MyTodyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
                        'Packer Name
                        MyTodyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value

                        ncfree = 4
                        For nCol = 1 To 3
                            For x = 12 To 41
                                MyTodyExcel.Cells(x, ncfree) = "" 'Clear the contents of cone cells
                                MyTodyExcel.Cells(x, ncfree - 2) = "" 'Clear the contents of Carton cells
                            Next
                            ncfree = ncfree + 4
                        Next
                        'REST ROW AND COLUMN TO DEFAULT VALUES
                        nfree = 13
                        ncfree = 4
                        boxCount = boxCount + 1
                    End If
                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
        Me.Close()
    End Sub

    Public Sub TodayUpdateBS_AS_30()
        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet


        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        boxCount = mycount

        Dim totCount As Integer = 1
        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET
        For rcount = 13 To 102
            If MyTodyExcel.Cells(rcount, 4).Value > 0 Then
                totCount = totCount = 1
                Continue For
            Else
                nfree = rcount
                Exit For
            End If
        Next


        'CHECK TO SEE IF THE NEW CURRENT SHEET IS FULL IF SO ADD A NEW SHEET
        If totCount = 90 Then
            xlTodyWorkbook.Sheets(1).Copy(After:=xlTodyWorkbook.Sheets(mycount))
            'ReName the work sheet 
            CType(MyTodyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

            nfree = 13


            'Product Name
            MyTodyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
            'Product Code
            MyTodyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
            'Packer Name
            MyTodyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value





            For i = 13 To 102
                MyTodyExcel.Cells(nfree, 4) = "" 'Clear the contents of cone cells
            Next
            boxCount = boxCount + 1
        End If


        'Routine to go through the rows and extract Grade A cones plus keep count
        Dim cartonNum As String = ""
        Dim cellNum As Integer


        Try

            For i = 1 To frmDGV.DGVdata.Rows.Count

                If frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "8" And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then


                    'CREATE CORRECT HEADER FOR SHEET
                    Select Case frmJobEntry.txtGrade.Text
                        Case "A", "B", "AL", "AD"

                        Case "P35 AS", "P25 AS", "P35 BS", "P30 BS"

                        Case "P15 AS", "P20 BS"

                    End Select



                    'USED TO ALLOCATE BOX NUMBER USED WHEN PACKED
                    Select Case nfree
                        Case 13, 14, 15, 16, 17, 18
                            cartonNum = 1
                            cellNum = 13
                        Case 19, 20, 21, 22, 23, 24
                            cartonNum = 2
                            cellNum = 19
                        Case 25, 26, 27, 28, 29, 30
                            cartonNum = 3
                            cellNum = 25
                        Case 31, 32, 33, 34, 35, 36
                            cartonNum = 4
                            cellNum = 31
                        Case 37, 38, 39, 40, 41, 42
                            cartonNum = 5
                            cellNum = 37
                        Case 43, 44, 45, 46, 47, 48
                            cartonNum = 6
                            cellNum = 43
                        Case 49, 50, 51, 52, 53, 54
                            cartonNum = 7
                            cellNum = 49
                        Case 55, 56, 57, 58, 59, 60
                            cartonNum = 8
                            cellNum = 55
                        Case 61, 62, 63, 64, 65, 66
                            cartonNum = 9
                            cellNum = 61
                        Case 67, 68, 69, 70, 71, 72
                            cartonNum = 10
                            cellNum = 67
                        Case 73, 74, 75, 76, 77, 78
                            cartonNum = 11
                            cellNum = 73
                        Case 79, 80, 81, 82, 83, 84
                            cartonNum = 12
                            cellNum = 79
                        Case 85, 86, 87, 88, 89, 90
                            cartonNum = 13
                            cellNum = 85
                        Case 91, 92, 93, 94, 95, 96
                            cartonNum = 14
                            cellNum = 91
                        Case 97, 98, 99, 100, 101, 102
                            cartonNum = 15
                            cellNum = 97
                    End Select


                    cartonNum = (cartonNum & "-" & boxCount).ToString

                    'WRITE CONE NUMBER TO SHEET
                    ' MsgBox("I value = " & i & " Cone Number = " & frmDGV.DGVdata.Rows(i - 1).Cells(36).Value & " nfree Value = " & nfree)
                    MyTodyExcel.Cells(nfree, 4) = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value





                    'WRITE CARTON NUMBER (TraceNumber) TO SHEET AND PUT IN DGV
                    MyTodyExcel.Cells(cellNum, 2) = cartonNum
                    frmDGV.DGVdata.Rows(i - 1).Cells(61).Value = cartonNum
                    nfree = nfree + 1

                    'ROUTINE IF SHEET IS FULL TO COPY SHEET AND CREATE A NEW SHEET IN THE WORKBOOK
                    If nfree = 103 Then
                        Dim tmpsaveName As String

                        tmpsaveName = (frmPackRepMain.finPath & "\" & frmPackRepMain.sheetName & "_" & mycount & ".xlsx")
                        MyTodyExcel.DisplayAlerts = False
                        xlTodyWorkbook.Sheets(mycount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)

                        MyTodyExcel.DisplayAlerts = True

                        xlTodyWorkbook.Sheets(frmPackRepMain.sheetName).Copy(After:=xlTodyWorkbook.Sheets(mycount))
                        CType(MyTodyExcel.Workbooks(1).Worksheets(frmPackRepMain.sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

                        MyTodyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                        'Product Code
                        MyTodyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
                        'Packer Name
                        MyTodyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value

                        For x = 13 To 102
                            MyTodyExcel.Cells(x, 4) = "" 'Clear the contents of cone cells
                        Next

                        nfree = 13
                        boxCount = boxCount + 1
                    End If
                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
        Me.Close()
    End Sub

    Public Sub TodayUpdateBS_AS_20()

    End Sub

    'ROUTINE TO CREATE RECHECK SHEET

    Public Sub todayUpdate_ReCheck()
        Dim xlTodyWorkbook As Excel.Workbook
        Dim xlTodysheets As Excel.Worksheet
        createBarcode()

        xlTodyWorkbook = MyTodyExcel.Workbooks.Open(frmPackRepMain.savename)
        mycount = xlTodyWorkbook.Worksheets.Count
        'boxCount = mycount

        Dim totCount As Integer



        'FIND NEXT BLANK ROW FOR ON EXCEL SHEET
        For rcount = 9 To 40

            ' Only single Column to look at
            If MyTodyExcel.Cells(rcount, 3).Value > 0 Then
                totCount = totCount + 1
                Continue For
            Else
                nfree = rcount
                Exit For
            End If
        Next



        'CHECK TO SEE IF THE NEW CURRENT SHEET IS FULL IF SO ADD A NEW SHEET
        If totCount = 32 Then
            xlTodyWorkbook.Sheets(1).Copy(After:=xlTodyWorkbook.Sheets(mycount))
            'ReName the work sheet 
            CType(MyTodyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

            nfree = 9


            'Product Name
            MyTodyExcel.Cells(5, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
            'Product Code
            MyTodyExcel.Cells(5, 7) = frmDGV.DGVdata.Rows(0).Cells(2).Value
            'Packer Name
            'MyTodyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value

            'Barcode test
            'MyTodyExcel.Cells(0, 6) = SheetCodeString



            For i = 9 To 40
                MyTodyExcel.Cells(i, 3) = "" 'Clear the contents of cone cells
            Next

        End If



        Try

            For i = 1 To frmDGV.DGVdata.Rows.Count

                If frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "8" And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then



                    'WRITE CONE NUMBER TO SHEET
                    ' MsgBox("I value = " & i & " Cone Number = " & frmDGV.DGVdata.Rows(i - 1).Cells(36).Value & " nfree Value = " & nfree)
                    MyTodyExcel.Cells(nfree, 3) = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value



                    nfree = nfree + 1

                    'ROUTINE IF SHEET IS FULL TO COPY SHEET AND CREATE A NEW SHEET IN THE WORKBOOK
                    If nfree = 41 Then
                        Dim tmpsaveName As String

                        tmpsaveName = (frmPackRepMain.finPath & "\" & frmPackRepMain.sheetName & "_" & mycount & ".xlsx")
                        MyTodyExcel.DisplayAlerts = False
                        xlTodyWorkbook.Sheets(mycount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)

                        MyTodyExcel.DisplayAlerts = True

                        xlTodyWorkbook.Sheets(frmPackRepMain.sheetName).Copy(After:=xlTodyWorkbook.Sheets(mycount))
                        CType(MyTodyExcel.Workbooks(1).Worksheets(frmPackRepMain.sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

                        'PRODUCT NAME
                        MyTodyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                        'Product Code
                        MyTodyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
                        'Packer Name
                        MyTodyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value

                        'CREATE AND WRITE BARCODE TO NEW SHEET
                        MyTodyExcel.Cells(1, 6) = SheetCodeString



                        For x = 9 To 40
                            MyTodyExcel.Cells(x, 3) = "" 'Clear the contents of cone cells
                        Next

                        nfree = 9

                    End If
                End If
            Next

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyTodyExcel.DisplayAlerts = False
            xlTodyWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        MyTodyExcel.Quit()
        releaseObject(xlTodysheets)
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)
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


    Private Sub DelayTM()
        Dim interval As Integer = "2000"
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        sw.Stop()

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


        SheetCodeString = ("*" & frmJobEntry.varProductCode & year & month & day & frmPackRepMain.sheetName & gradeTxt & (boxCount + 1) & "*")

    End Sub

End Class