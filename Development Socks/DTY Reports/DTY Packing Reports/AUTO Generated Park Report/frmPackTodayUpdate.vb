Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmPackTodayUpdate

    Dim MyTodyExcel As New Excel.Application
    Dim xlRowCount As Integer
    Dim mycount As Integer = 0
    Dim boxCount As Integer = 0
    Dim nfree As Integer = 13

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




End Class