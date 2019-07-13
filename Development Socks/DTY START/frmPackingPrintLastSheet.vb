Imports System.IO
Imports Microsoft.Office
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmPackingPrintLastSheet
    Dim startDate As String
    Dim fileStartDate As String
    Dim curItem As String
    Dim fileToOpen As String
    Dim MyExcel As New Excel.Application


    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged

        'Routine to get date range
        lblDate.Text = MonthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy")
        lblSelectedDate.Text = MonthCalendar1.SelectionRange.Start.ToString("dd/MM/yyyy")

        fileStartDate = MonthCalendar1.SelectionRange.Start.ToString("dd_MM_yyyy")

        btnSelect.Enabled = True



    End Sub

    Private Sub frmPackingPrintLastSheet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnSelect.Enabled = False
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        'go to packing Directory and find all jobs with todays date

        For Each file As String In System.IO.Directory.GetFiles(My.Settings.dirPacking & "\" & fileStartDate)
            lstBoxFiles.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file))
        Next

    End Sub



    Private Sub lstBoxFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstBoxFiles.SelectedIndexChanged
        Dim index As Integer = lstBoxFiles.FindString(curItem)  'Gets Index number of current item



        curItem = lstBoxFiles.SelectedItem.ToString()


        'Dim index As Integer = lstBoxFiles.FindString(curItem)  'Gets Index number of current item

    End Sub

    Private Sub lstBoxFiles_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lstBoxFiles.MouseDoubleClick
        'on double click get the index number of the selected row and use name to open file
        Dim xlWorkbook As Excel.Workbook
        Dim sheetCount As Integer
        Dim xlWorksheet As Excel.Worksheet




        fileToOpen = My.Settings.dirPacking & "\" & fileStartDate & "\" & curItem & ".xlsx"


        xlWorkbook = MyExcel.Workbooks.Open(fileToOpen)
        sheetCount = xlWorkbook.Worksheets.Count


        xlWorkbook = MyExcel.Workbooks.Open(fileToOpen)
        xlWorksheet = CType(xlWorkbook.Worksheets(sheetCount), Excel.Worksheet)

        MyExcel.Visible = True

        DelayTM()

        'xlWorksheet.close()
        'xlWorkbook.Close()

        'MyExcel.UserControl = True
        'MyExcel.Quit()
        'ReleaseComObject(xlWorkbook)
        'ReleaseComObject(xlWorksheet)
        'ReleaseComObject(MyExcel)


        lstBoxFiles.Items.Clear()



    End Sub


    Public Sub ReleaseComObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        frmJobEntry.Show()
    End Sub
End Class