Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmPackCreateNew
    'Dim MyCrExcel As New Excel.Application




    Public Sub CreateNew()
        Dim MyPakExcel As New Excel.Application
        Dim boxCount As Integer = 0
        Dim nfree As Integer = 13

        Dim xlWorkbook As Excel.Workbook
        Dim xlSheets As Excel.Worksheet

        'OPEN A NEW WORKSHEET
        xlWorkbook = MyPakExcel.Workbooks.Open(frmPackRepMain.template)
        'ReName the work sheet 
        CType(MyPakExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName

        'Product Name
        MyPakExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value
        'Product Code
        MyPakExcel.Cells(7, 5) = frmDGV.DGVdata.Rows(0).Cells(2).Value
        'DATE
        MyPakExcel.Cells(5, 3) = Date.Now.ToString("dd_MM_yyyy")
        'CHEESE WEIGHT
        MyPakExcel.Cells(13, 5) = frmJobEntry.varProdWeight
        'PACKER NAME
        MyPakExcel.Cells(13, 8) = frmDGV.DGVdata.Rows(0).Cells(55).Value


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

        frmPackTodayUpdate.TodayUpdate()
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

End Class