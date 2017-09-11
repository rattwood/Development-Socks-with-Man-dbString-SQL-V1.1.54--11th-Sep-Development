Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering
Imports Microsoft.Office
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class DGVDefReport

    Private SQL As New SQLConn

    'Local Database connection
    Public LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private LCmd As SqlCommand

    'SQL CONNECTORS
    Public LDA As SqlDataAdapter
    Public LDS As DataSet
    Public LDT As DataTable
    Public LCB As SqlCommandBuilder

    Public LRecordCount As Integer
    Private LException As String
    ' SQL QUERY PARAMETERS
    Public LParams As New List(Of SqlParameter)

    Public coneNumStart As String = Nothing  'Get Macine number to then set correct spindle Number range on grid
    Public coneNumEnd As String = Nothing  'Get Macine number to then set correct spindle Number range on grid
    Private mcname As String
    Private Sortcount As Integer = Nothing
    Private count As Integer = Nothing
    Public startDate As String
    Public endDate As String

    Dim conecount As Integer
    Dim sp_nums As String
    Dim template As String
    Dim FileInName As String
    Dim MyExcel As New Excel.Application




    Private Sub DGVDefReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub

    Private Sub CreateHeaders()

        'Clear DGV
        DGVDefData.Columns.Clear()


        'PROPERTIES
        ' DGVDefData.SelectionMode = DataGridViewSelectionMode.FullRowSelect    'Always WORK ON FULL ROW
        DGVDefData.ColumnCount = 31       'NUMBER OF COLUMNS
        DGVDefData.Rows.Add(conecount)
        'Construct the Columns


        'CREATE COLUM HEADERS
        DGVDefData.Columns(0).Name = "Date"               'DATE
        DGVDefData.Columns(1).Name = "Day"                'DAY
        DGVDefData.Columns(2).Name = "Month"              'MONTH
        DGVDefData.Columns(3).Name = "Year"               'YEAR
        DGVDefData.Columns(4).Name = "Product"            'PRODUCT
        DGVDefData.Columns(5).Name = "Merge"              'MERGE #
        DGVDefData.Columns(6).Name = "Machine"            'MACHINE NAME
        DGVDefData.Columns(7).Name = "Chip Type"          'CHIP TYPE
        DGVDefData.Columns(8).Name = "Weight"             'WEIGHT
        DGVDefData.Columns(9).Name = "Doffing"            'DOFFING #
        DGVDefData.Columns(10).Name = "Cheese No."        'CHEESE NUMBER
        DGVDefData.Columns(11).Name = "  K  "             'FLT_K  
        DGVDefData.Columns(12).Name = "  D  "             'FLT_D
        DGVDefData.Columns(13).Name = "  F  "             'FLT_F
        DGVDefData.Columns(14).Name = "  O  "             'FLT_O
        DGVDefData.Columns(15).Name = "  T  "             'FLT_T
        DGVDefData.Columns(16).Name = "  P  "             'FLT_P
        DGVDefData.Columns(17).Name = "  N  "             'FLT_N
        DGVDefData.Columns(18).Name = "  W  "             'FLT_W
        DGVDefData.Columns(19).Name = "  H  "             'FLT_H
        DGVDefData.Columns(20).Name = "  TR  "            'FLT_TR
        DGVDefData.Columns(21).Name = "  B  "             'FLT_B
        DGVDefData.Columns(22).Name = "  C  "             'FLT_C
        DGVDefData.Columns(23).Name = "  DO  "            'FLT_DO
        DGVDefData.Columns(24).Name = "  DH  "            'FLT_DH
        DGVDefData.Columns(25).Name = "  CL  "            'FLT_CL
        DGVDefData.Columns(26).Name = "  FI  "            'FLT_FI
        DGVDefData.Columns(27).Name = "  YN  "            'FLT_YN
        DGVDefData.Columns(28).Name = "  HT  "            'FLT_HT
        DGVDefData.Columns(29).Name = "  LT  "            'FLT_LT
        DGVDefData.Columns(30).Name = "  SORTENDTM  "     'SORT END TIME FROM DB




        If My.Settings.debugSet Then DGVDefData.Show()

    End Sub



    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged

        'Routine to get date range
        Label5.Text = MonthCalendar1.SelectionRange.Start.ToString("dd/MMM/yyyy")
        Label6.Text = MonthCalendar1.SelectionRange.End.ToString("dd/MMM/yyyy")

        'STRIPOUT / Characters from date so that they are not used in the file name

        startDate = Label5.Text.Replace("/", "")
        endDate = Label6.Text.Replace("/", "")
        btnLoadData.Enabled = True

    End Sub



    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click

        'Excel Items
        Dim savename As String

        template = (My.Settings.dirTemplate & "\" & "DefectSortingTemplate.xlsx").ToString

        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Exit Sub
        End If



        Dim workbook As Excel.Workbook
        Dim sheet As Excel.Worksheets


        savename = (My.Settings.dirJobs & "\" & "Defect Sorting_" & startDate & "_" & endDate & ".xlsx").ToString




        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor




        'Load THE DATA FOR COMPLETE JOB
        SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM BETWEEN ('" & Label5.Text & "') AND  ('" & Label6.Text & "') And DEFCONE > 0 And MISSCONE = 0 ")

        conecount = SQL.RecordCount


        If conecount > 0 Then
            'CreateHeaders()

            DGVDefData.DataSource = SQL.SQLDS.Tables(0)
            DGVDefData.Rows(0).Selected = True

            'SORT DGV TABLE BY PRODUCT NAME
            DGVDefData.Sort(DGVDefData.Columns("PRODNAME"), ListSortDirection.Ascending)

        Else
            MsgBox("No Defect Cheeses")
            MyExcel = Nothing
            workbook = Nothing
            sheet = Nothing
            DGVDefData.Dispose()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        'CreateHeaders()

        workbook = MyExcel.Workbooks.Open(template)
        'MyExcel.Visible = True



        Dim dbDate As Date
        Dim sortDate As String
        Dim dayDate As String
        Dim xlcount As Integer = 2   'START ROW ON EXCEL SHEET

        For count = 1 To conecount - 1


            'DATE AND DAY INFO
            If IsDBNull(DGVDefData.Rows(count).Cells("SORTENDTM").Value) Then
                sortDate = "1900-00-00"
                dayDate = "00"

            Else
                dbDate = DGVDefData.Rows(count).Cells("SORTENDTM").Value
                sortDate = dbDate.ToString("dd-MM-yyyy")
                dayDate = dbDate.ToString("dd")
            End If




            'Dim sortDate As String = DGVDefData.Rows(count).Cells("SORTENDTM").Value.ToString("dd-MM-yyy")
            'Dim dayDate = DGVDefData.Rows(count).Cells("SORTENDTM").Value.ToString("dd")
            Dim prodNum = DGVDefData.Rows(count).Cells("PRNUM").Value.ToString

            MyExcel.Cells(xlcount, 1) = sortDate          'DATE
            MyExcel.Cells(xlcount, 2) = dayDate           'DAY

            MyExcel.Cells(xlcount, 3) = DGVDefData.Rows(count).Cells("PRMM").Value     'MONTH

            MyExcel.Cells(xlcount, 4) = DGVDefData.Rows(count).Cells("PRYY").Value  'YEAR

            MyExcel.Cells(xlcount, 5) = DGVDefData.Rows(count).Cells("PRODNAME").Value   'PRODUCT

            MyExcel.Cells(xlcount, 6) = DGVDefData.Rows(count).Cells("MERGENUM").Value     'MERGE #

            MyExcel.Cells(xlcount, 7) = DGVDefData.Rows(count).Cells("MCNAME").Value      'MACHINE NAME

            Dim chipType As String = DGVDefData.Rows(count).Cells("PRODNAME").Value
            chipType = chipType.Substring(chipType.Length - 4, 4)
            MyExcel.Cells(xlcount, 8) = chipType                                               'GET CHIP TYPE FROM PRODUCT NAME

            'WEIGHT ROUTINE FOLLOWS THESE

            MyExcel.Cells(xlcount, 10) = DGVDefData.Rows(count).Cells("DOFFNUM").Value       'DOFFING #


            MyExcel.Cells(xlcount, 11) = DGVDefData.Rows(count).Cells("CONENUM").Value      'CHEESE NUMBER

            If DGVDefData.Rows(count).Cells("FLT_K").Value = True Then MyExcel.Cells(xlcount, 12) = "1" Else MyExcel.Cells(xlcount, 12) = "-"
            'MyExcel.Cells(count  , 12) = DGVDefData.Rows(count).Cells("FLT_K").Value       'FLT_K 

            If DGVDefData.Rows(count).Cells("FLT_D").Value = True Then MyExcel.Cells(xlcount, 13) = 1 Else MyExcel.Cells(xlcount, 13) = "-"
            'MyExcel.Cells(count  , 13) = DGVDefData.Rows(count).Cells("FLT_D").Value    'FLT_D

            If DGVDefData.Rows(count).Cells("FLT_F").Value = True Then MyExcel.Cells(xlcount, 14) = 1 Else MyExcel.Cells(xlcount, 14) = "-"
            'MyExcel.Cells(count  , 14) = DGVDefData.Rows(count).Cells("FLT_F").Value     'FLT_F

            If DGVDefData.Rows(count).Cells("FLT_O").Value = True Then MyExcel.Cells(xlcount, 15) = 1 Else MyExcel.Cells(xlcount, 15) = "-"
            'MyExcel.Cells(count  , 15) = DGVDefData.Rows(count).Cells("FLT_O").Value     'FLT_O

            If DGVDefData.Rows(count).Cells("FLT_T").Value = True Then MyExcel.Cells(xlcount, 16) = 1 Else MyExcel.Cells(xlcount, 16) = "-"
            'MyExcel.Cells(count  , 16) = DGVDefData.Rows(count).Cells("FLT_T").Value   'FLT_T

            If DGVDefData.Rows(count).Cells("FLT_P").Value = True Then MyExcel.Cells(xlcount, 17) = 1 Else MyExcel.Cells(xlcount, 17) = "-"
            'MyExcel.Cells(count  , 17) = DGVDefData.Rows(count).Cells("FLT_P").Value     'FLT_P

            If DGVDefData.Rows(count).Cells("FLT_N").Value = True Then MyExcel.Cells(xlcount, 18) = 1 Else MyExcel.Cells(xlcount, 18) = "-"
            ' MyExcel.Cells(count  , 18) = DGVDefData.Rows(count).Cells("FLT_N").Value    'FLT_N

            If DGVDefData.Rows(count).Cells("FLT_W").Value = True Then MyExcel.Cells(xlcount, 19) = 1 Else MyExcel.Cells(xlcount, 19) = "-"
            'MyExcel.Cells(count  , 19) = DGVDefData.Rows(count).Cells("FLT_W").Value   'FLT_W

            If DGVDefData.Rows(count).Cells("FLT_H").Value = True Then MyExcel.Cells(xlcount, 20) = 1 Else MyExcel.Cells(xlcount, 20) = "-"
            'MyExcel.Cells(count  , 20) = DGVDefData.Rows(count).Cells("FLT_H").Value     'FLT_H

            If DGVDefData.Rows(count).Cells("FLT_TR").Value = True Then MyExcel.Cells(xlcount, 21) = 1 Else MyExcel.Cells(xlcount, 21) = "-"
            'MyExcel.Cells(count  , 21) = DGVDefData.Rows(count).Cells("FLT_TR").Value    'FLT_TR

            If DGVDefData.Rows(count).Cells("FLT_B").Value = True Then MyExcel.Cells(xlcount, 22) = 1 Else MyExcel.Cells(xlcount, 22) = "-"
            'MyExcel.Cells(count  , 22) = DGVDefData.Rows(count).Cells("FLT_B").Value    'FLT_B

            If DGVDefData.Rows(count).Cells("FLT_C").Value = True Then MyExcel.Cells(xlcount, 23) = 1 Else MyExcel.Cells(xlcount, 23) = "-"
            'MyExcel.Cells(count  , 23) = DGVDefData.Rows(count).Cells("FLT_C").Value    'FLT_C

            If DGVDefData.Rows(count).Cells("FLT_DO").Value = True Then MyExcel.Cells(xlcount, 24) = 1 Else MyExcel.Cells(xlcount, 24) = "-"
            'MyExcel.Cells(count  , 24) = DGVDefData.Rows(count).Cells("FLT_DO").Value    'FLT_DO

            If DGVDefData.Rows(count).Cells("FLT_DH").Value = True Then MyExcel.Cells(xlcount, 25) = 1 Else MyExcel.Cells(xlcount, 25) = "-"
            'MyExcel.Cells(count  , 25) = DGVDefData.Rows(count).Cells("FLT_DH").Value    'FLT_DH

            If DGVDefData.Rows(count).Cells("FLT_CL").Value = True Then MyExcel.Cells(xlcount, 26) = 1 Else MyExcel.Cells(xlcount, 26) = "-"
            'MyExcel.Cells(count  , 26) = DGVDefData.Rows(count).Cells("FLT_CL").Value     'FLT_CL

            If DGVDefData.Rows(count).Cells("FLT_FI").Value = True Then MyExcel.Cells(xlcount, 27) = 1 Else MyExcel.Cells(xlcount, 27) = "-"
            'MyExcel.Cells(count  , 27) = DGVDefData.Rows(count).Cells("FLT_FI").Value   'FLT_FI

            If DGVDefData.Rows(count).Cells("FLT_YN").Value = True Then MyExcel.Cells(xlcount, 28) = 1 Else MyExcel.Cells(xlcount, 28) = "-"
            'MyExcel.Cells(count  , 28) = DGVDefData.Rows(count).Cells("FLT_YN").Value    'FLT_YN

            If DGVDefData.Rows(count).Cells("FLT_HT").Value = True Then MyExcel.Cells(xlcount, 29) = 1 Else MyExcel.Cells(xlcount, 29) = "-"
            'MyExcel.Cells(count  , 29) = DGVDefData.Rows(count).Cells("FLT_HT").Value    'FLT_HT

            If DGVDefData.Rows(count).Cells("FLT_LT").Value = True Then MyExcel.Cells(xlcount, 30) = 1 Else MyExcel.Cells(xlcount, 30) = "-"
            'MyExcel.Cells(count  , 30) = DGVDefData.Rows(count).Cells("FLT_LT").Value    'FLT_LT




            'GET WEIGHT FROM OTHER TABLE
            SQL.ExecQuery("SELECT * FROM product WHERE PRNUM = '" & prodNum & "' ")

            Dim prodcount = SQL.RecordCount


            If prodcount > 0 Then
                DGVDefProdData.DataSource = SQL.SQLDS.Tables(0)
                DGVDefProdData.Rows(0).Selected = True
            End If

            MyExcel.Cells(xlcount, 9) = DGVDefProdData.Rows(0).Cells("PRODWEIGHT").Value      'WEIGHT

            xlcount = xlcount + 1  'INC COUNT FOR ROW ON EXCEL

        Next








        'clear variables







        Try

            'Save changes to new file in Jobs Directory
            MyExcel.DisplayAlerts = False
            workbook.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)
            workbook.Close()
            MyExcel.Quit()
            releaseObject(workbook)
            DGVDefData.Dispose()
            DGVDefProdData.Dispose()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Me.Close()
            Exit Sub
        End Try

        Try
            'Close template file but do not save updates to it

            workbook.Close(SaveChanges:=False)
            MyExcel.DisplayAlerts = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try




        MyExcel.Quit()
        releaseObject(workbook)

        DGVDefData.Dispose()
        DGVDefProdData.Dispose()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        MsgBox("Job Report " & savename & " Created")
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