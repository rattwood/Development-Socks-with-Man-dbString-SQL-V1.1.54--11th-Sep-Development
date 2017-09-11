Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmPackReport


    Dim sp_nums As String
    Dim template As String
    Dim FileInName As String
    Dim todayPath As String
    Dim finPath As String
    Dim yesterdayPath As String
    Dim MyExcel As New Excel.Application
    Dim MyExcelYest As New Excel.Application
    Dim xlRowCount As Integer
    Dim abortPrint As Integer = 0
    Dim cheeseweight As String
    Dim yesterdayCont As Integer = 0
    Dim xlWorkbook As Excel.Workbook
    Dim xlWBYesterday As Excel.Workbook
    Dim xlsheet As Excel.Worksheet
    Dim xlWSYesterday As Excel.Worksheet
    Dim yestname As String
    Dim nfree As Integer = 13

    Public Sub packPrint()
        Dim sheetCount As Integer = 0
        Dim myCount As Integer = 0
        Dim boxCount As Integer = 0
        Dim savename As String
        Dim saveString As String
        Dim prodNameMod As String
        Dim sheetName As String



        prodNameMod = frmDGV.DGVdata.Rows(0).Cells(52).Value.ToString
        prodNameMod = prodNameMod.Replace("/", "_")

        sheetName = prodNameMod.Substring(prodNameMod.Length - 4)
        'Check if a directory for Todays date is created, if not create one


        todayDir()



        'Create the Report name
        saveString = (prodNameMod & " " _
            & frmDGV.DGVdata.Rows(0).Cells(7).Value.ToString & "_" _
            & frmDGV.DGVdata.Rows(0).Cells(2).Value.ToString)


        'create the save name of the file
        savename = (todayPath & "\" & saveString & ".xlsx").ToString
        template = (My.Settings.dirTemplate & "\" & "PackingTemplate.xlsx").ToString

        'Create Yesterday Check refrence
        yestname = (yesterdayPath & "\" & saveString & ".xlsx").ToString





        'Check to make sure template exists
        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Me.Close()
            frmJobEntry.Show()
        End If



        ''Call IsFileOpen(New FileInfo(savename))

        If abortPrint Then
            Exit Sub

        End If






        Try


            If File.Exists(savename) Then
                xlWorkbook = MyExcel.Workbooks.Open(savename)
                sheetCount = xlWorkbook.Worksheets.Count
                myCount = sheetCount
                xlsheet = MyExcel.Sheets(myCount)
                boxCount = myCount


                'FIND NEXT BLANK ROW FOR CONES
                For rcount = 13 To 102
                    If MyExcel.Cells(rcount, 4).Value <> 0 Then
                        Continue For
                    Else
                        nfree = rcount
                        Exit For
                    End If
                Next


                'CREATE A NEW WORK SHEET AND CLEAR ALL VALUES COPIED FROM ORIGINAL SHEET USED AS TEMPLATE
                If nfree = 0 Then
                    xlWorkbook.Sheets(1).Copy(After:=xlWorkbook.Sheets(myCount))
                    'ReName the work sheet 
                    CType(MyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = sheetName

                    nfree = 13
                    'PRODUCT NAME
                    MyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                    'Product Code
                    MyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
                    'Packer Name
                    MyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value

                    For i = 13 To 102
                        MyExcel.Cells(nfree, 4) = "" 'Clear the contents of cone cells
                    Next
                    boxCount = boxCount + 1

                End If

            Else
                'IF xlWorkbook DOES Not EXIST FOR TODAY THEN CHECK TO SEE IF SAME PRODUCT PROCESSED YESTERDAY 
                'And IF LAST SHEET OF YESTERDAY HAS SPACE CONTINE FROM THERE


                If File.Exists(yestname) Then

                    'xlWorkbook = MyExcel.Workbooks.Open(yestname)      **************************
                    xlWBYesterday = MyExcelYest.Workbooks.Open(yestname)

                    ' FIND NEXT BLANK ROW FOR CONES
                    For rcount = 13 To 103
                        If MyExcelYest.Cells(rcount, 4).Value <> 0 Then
                            Continue For
                        Else
                            nfree = rcount
                            If nfree > 102 Then nfree = 0  'Incase last days sheet was a full sheet
                            Exit For
                        End If
                    Next

                    'USED AS A FLAG TO NEXT SECTION TO ENTER DATES
                    yesterdayCont = 1


                    Try
                        'Close YESTERDAYS FILE but do not save updates to it
                        'xlWorkbook.Close(SaveChanges:=False)
                        'MyExcel.Quit()
                        'releaseObject(sheetCount)
                        'releaseObject(myCount)
                        'releaseObject(xlWorkbook)

                        'Close YESTERDAYS FILE but do Not save updates to it
                        xlWBYesterday.Close(SaveChanges:=False)
                        MyExcelYest.Quit()
                        releaseObject(xlWBYesterday)

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


                    DelayTM()  'Delay to allow Excel to close down


                End If


                'OPENS A BLANK WORKSHEET FROM TEMPLATE

                xlWorkbook = MyExcel.Workbooks.Open(template)
                'xlsheet = CType(MyExcel.Sheets(1), Excel._Worksheet)
                'xlsheet.a

                MyExcel.Visible = True

                'ReName the work sheet 
                CType(MyExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = sheetName

                'Product Name
                MyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                'Product Code
                MyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
                'Packer Name

                MyExcel.Cells(5, 3) = Date.Now.ToString("dd_MM_yyyy")

                MyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value
                If boxCount = 0 Then boxCount = 1
                'nfree = 13  'SETS DEFAULT IF NEW SHEET WITH NO CARRY ON FROM PREVIOUS DAYS

                If yesterdayCont Then
                    If nfree > 0 Then
                        For usedrow = 13 To nfree - 1
                            MyExcel.Cells(usedrow, 4) = Date.Now.AddDays(-1).ToString("ddMMyyyy")
                        Next
                    End If
                    nfree = 13  'SETS DEFAULT IF NEW SHEET WITH NO CARRY ON FROM PREVIOUS DAYS
                End If
            End If

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try




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
                    MyExcel.Cells(nfree, 4) = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value

                    'WRITE CARTON NUMBER TO SHEET AND PUT IN DGV
                    MyExcel.Cells(cellNum, 2) = cartonNum
                    frmDGV.DGVdata.Rows(i - 1).Cells(61).Value = cartonNum
                    nfree = nfree + 1


                    If nfree = 103 Then 'checks to see if we have written to the last cell of sheet and starts a new sheet

                        Dim tmpsaveName As String
                        'Dim tmpCount As Integer


                        'If yesterdayCont Then
                        '    Dim Bill As Integer
                        '    tmpCount = MyExcel.ActiveSheet.value

                        '    Dim Dorris As String
                        '    Dorris = tmpCount.ToString
                        '    Label1.Text = Dorris
                        '    DelayTM()


                        '    tmpsaveName = (finPath & "\" & sheetName & "_" & Dorris & ".xlsx")
                        '    MyExcel.DisplayAlerts = False


                        '    'make a copy of sheet in Finished day sheets  Directory Only After first save
                        '    'xlWorkbook.Sheets(tmpCount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)
                        '    xlWorkbook.ActiveSheet.SaveAS(Filename:=tmpsaveName, FileFormat:=51)


                        '    MyExcel.DisplayAlerts = True
                        '    'CREAT NEW SHEET FOR NEXT PALLETE

                        '    xlWorkbook.Sheets(sheetName).Copy(After:=xlWorkbook.Sheets(tmpCount))
                        '    CType(MyExcel.Workbooks(1).Worksheets(sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = sheetName
                        'End If
                        'Dim tmpsaveName As String
                        'Dim tmpCount As String = 1
                        'myCount = MyExcel.ActiveSheet.value


                        tmpsaveName = (finPath & "\" & sheetName & "_" & myCount & ".xlsx")
                            MyExcel.DisplayAlerts = False


                            'make a copy of sheet in Finished day sheets  Directory Only After first save       **************
                            xlWorkbook.Sheets(myCount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)



                            MyExcel.DisplayAlerts = True
                            'CREAT NEW SHEET FOR NEXT PALLETE

                            xlWorkbook.Sheets(sheetName).Copy(After:=xlWorkbook.Sheets(myCount))
                            CType(MyExcel.Workbooks(1).Worksheets(sheetName), Microsoft.Office.Interop.Excel.Worksheet).Name = sheetName
                        End If




                        MyExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                            'Product Code
                            MyExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
                            'Packer Name
                            MyExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value

                            For x = 13 To 102
                                MyExcel.Cells(x, 4) = "" 'Clear the contents of cone cells
                            Next

                            nfree = 13
                            boxCount = boxCount + 1
                            xlsheet = Nothing
                        End If

            Next

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            'Save changes to new file in Paking Dir
            MyExcel.DisplayAlerts = False
            xlWorkbook.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message & "save workbook")

        End Try

        Try
            'Close template file but do not save updates to it
            xlWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message & "Close Workbook")
        End Try


        MyExcel.Quit()

        releaseObject(xlsheet)
        releaseObject(xlWorkbook)
        releaseObject(MyExcel)






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

    Private Sub todayDir()
        ' routine to check if a today directory exists otherwise creat a new one

        todayPath = (My.Settings.dirPacking & "\" & Date.Now.ToString("dd_MM_yyyy"))
        finPath = (My.Settings.dirPackReports & "\" & Date.Now.ToString("dd_MM_yyyy"))

        If Not Directory.Exists(todayPath) Then
            Directory.CreateDirectory(todayPath)
        End If

        If Not Directory.Exists(finPath) Then
            Directory.CreateDirectory(finPath)
        End If

        yesterdayPath = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-1).ToString("dd_MM_yyyy"))


    End Sub

    Private Sub IsFileOpen(ByVal file As FileInfo)

        Dim stream As FileStream = Nothing
        Try
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            stream.Close()

        Catch ex As Exception


            ' do something here, either close the file if you have a handle, show a msgbox, retry  or as a last resort terminate the process - which could cause corruption and lose data

            MsgBox("Excel File is open please close and then Retry")
            MyExcel.DisplayAlerts = False
            MyExcel.Quit()
            abortPrint = 1
        End Try







    End Sub

    Private Sub frmPackReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class