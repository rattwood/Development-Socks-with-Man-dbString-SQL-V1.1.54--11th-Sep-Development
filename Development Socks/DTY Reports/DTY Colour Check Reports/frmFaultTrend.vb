Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering
Imports Microsoft.Office
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmFaultTrend

    Private DEFSQL As New SQLConn

    'Local Database connection
    Public DEFLConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private LCmd As SqlCommand

    'SQL CONNECTORS
    Public DEFLDA As SqlDataAdapter
    Public DEFLDS As DataSet
    Public DEFLDT As DataTable
    Public DEFLCB As SqlCommandBuilder

    Public DEFLRecordCount As Integer
    Private DEFLException As String
    ' SQL QUERY PARAMETERS
    Public DEFLParams As New List(Of SqlParameter)



    Public coneNumStart As String = Nothing  'Get Macine number to then set correct spindle Number range on grid
    Public coneNum As String = Nothing  'Get Macine number to then set correct spindle Number range on grid
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

    Public Sub CreateHeaders()

        'Clear DGV
        DGVFaultTrend.Columns.Clear()


        'PROPERTIES
        ' DGVFaultTrend.SelectionMode = DataGridViewSelectionMode.FullRowSelect    'Always WORK ON FULL ROW
        DGVFaultTrend.ColumnCount = 31       'NUMBER OF COLUMNS
        DGVFaultTrend.Rows.Add(conecount)
        'Construct the Columns


        'CREATE COLUM HEADERS
        DGVFaultTrend.Columns(0).Name = "Date"               'DATE
        DGVFaultTrend.Columns(1).Name = "Day"                'DAY
        DGVFaultTrend.Columns(2).Name = "Month"              'MONTH
        DGVFaultTrend.Columns(3).Name = "Year"               'YEAR
        DGVFaultTrend.Columns(4).Name = "Product"            'PRODUCT
        DGVFaultTrend.Columns(5).Name = "Merge"              'MERGE #
        DGVFaultTrend.Columns(6).Name = "Machine"            'MACHINE NAME
        DGVFaultTrend.Columns(7).Name = "Chip Type"          'CHIP TYPE
        DGVFaultTrend.Columns(8).Name = "Weight"             'WEIGHT
        DGVFaultTrend.Columns(9).Name = "Doffing"            'DOFFING #
        DGVFaultTrend.Columns(10).Name = "Cheese No."        'CHEESE NUMBER
        DGVFaultTrend.Columns(11).Name = "  K  "             'FLT_K  
        DGVFaultTrend.Columns(12).Name = "  D  "             'FLT_D
        DGVFaultTrend.Columns(13).Name = "  F  "             'FLT_F
        DGVFaultTrend.Columns(14).Name = "  O  "             'FLT_O
        DGVFaultTrend.Columns(15).Name = "  T  "             'FLT_T
        DGVFaultTrend.Columns(16).Name = "  P  "             'FLT_P
        DGVFaultTrend.Columns(17).Name = "  N  "             'FLT_N
        DGVFaultTrend.Columns(18).Name = "  W  "             'FLT_W
        DGVFaultTrend.Columns(19).Name = "  H  "             'FLT_H
        DGVFaultTrend.Columns(20).Name = "  TR  "            'FLT_TR
        DGVFaultTrend.Columns(21).Name = "  B  "             'FLT_B
        DGVFaultTrend.Columns(22).Name = "  C  "             'FLT_C
        DGVFaultTrend.Columns(23).Name = "  DO  "            'FLT_DO
        DGVFaultTrend.Columns(24).Name = "  DH  "            'FLT_DH
        DGVFaultTrend.Columns(25).Name = "  CL  "            'FLT_CL
        DGVFaultTrend.Columns(26).Name = "  FI  "            'FLT_FI
        DGVFaultTrend.Columns(27).Name = "  YN  "            'FLT_YN
        DGVFaultTrend.Columns(28).Name = "  HT  "            'FLT_HT
        DGVFaultTrend.Columns(29).Name = "  LT  "            'FLT_LT
        DGVFaultTrend.Columns(30).Name = "  SORTENDTM  "     'SORT END TIME FROM DB




        If My.Settings.debugSet Then DGVFaultTrend.Show()

    End Sub

    Public Sub DefTrend()

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

        'Get count of Cheeses with faults on current cart

        coneNum = ((frmCart1.varConeNum - 1) - frmCart1.coneNumOffset)


        DEFSQL.ExecQuery("Select * From jobs Where MCNUM = ' " & frmJobEntry.varMachineCode & " ' And PRNUM = ' " & frmJobEntry.varProductCode & " 'CONENUM = ' " & coneNum & " 'And YY = ' " & frmJobEntry.varYear & " '  And MM = ' " & frmJobEntry.varMonth & " ' And DOFFNUM Between ' " & frmJobEntry.varDoffingNum & " ' - 3 And ' " & frmJobEntry.varDoffingNum & " ' And CONESTATE Between 8 and 9 ")


        'Load THE DATA FOR COMPLETE JOB
        'SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM BETWEEN ('" & Label5.Text & "') AND  ('" & Label6.Text & "') And DEFCONE > 0 And MISSCONE = 0 ")

        conecount = DEFSQL.RecordCount


        If conecount > 0 Then
            'CreateHeaders()

            DGVFaultTrend.DataSource = Sql.SQLDS.Tables(0)
            DGVFaultTrend.Rows(0).Selected = True

            'SORT DGV TABLE BY PRODUCT NAME
            DGVFaultTrend.Sort(DGVFaultTrend.Columns("DOFFNUM"), ListSortDirection.Ascending)

        Else
            MsgBox("No Defect Cheeses")
            MyExcel = Nothing
            workbook = Nothing
            sheet = Nothing
            DGVFaultTrend.Dispose()
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

        For count = 1 To 3 - 1


            'DATE AND DAY INFO
            If IsDBNull(DGVFaultTrend.Rows(count).Cells("SORTENDTM").Value) Then
                sortDate = "1900-00-00"
                dayDate = "00"

            Else
                dbDate = DGVFaultTrend.Rows(count).Cells("SORTENDTM").Value
                sortDate = dbDate.ToString("dd-MM-yyyy")
                dayDate = dbDate.ToString("dd")
            End If




            'Dim sortDate As String = DGVFaultTrend.Rows(count).Cells("SORTENDTM").Value.ToString("dd-MM-yyy")
            'Dim dayDate = DGVFaultTrend.Rows(count).Cells("SORTENDTM").Value.ToString("dd")
            Dim prodNum = DGVFaultTrend.Rows(count).Cells("PRNUM").Value.ToString

            MyExcel.Cells(xlcount, 1) = sortDate          'DATE
            MyExcel.Cells(xlcount, 2) = dayDate           'DAY

            MyExcel.Cells(xlcount, 3) = DGVFaultTrend.Rows(count).Cells("PRMM").Value     'MONTH

            MyExcel.Cells(xlcount, 4) = DGVFaultTrend.Rows(count).Cells("PRYY").Value  'YEAR

            MyExcel.Cells(xlcount, 5) = DGVFaultTrend.Rows(count).Cells("PRODNAME").Value   'PRODUCT

            MyExcel.Cells(xlcount, 6) = DGVFaultTrend.Rows(count).Cells("MERGENUM").Value     'MERGE #

            MyExcel.Cells(xlcount, 7) = DGVFaultTrend.Rows(count).Cells("MCNAME").Value      'MACHINE NAME

            Dim chipType As String = DGVFaultTrend.Rows(count).Cells("PRODNAME").Value
            chipType = chipType.Substring(chipType.Length - 4, 4)
            MyExcel.Cells(xlcount, 8) = chipType                                               'GET CHIP TYPE FROM PRODUCT NAME

            'WEIGHT ROUTINE FOLLOWS THESE

            MyExcel.Cells(xlcount, 10) = DGVFaultTrend.Rows(count).Cells("DOFFNUM").Value       'DOFFING #


            MyExcel.Cells(xlcount, 11) = DGVFaultTrend.Rows(count).Cells("CONENUM").Value      'CHEESE NUMBER


            If DGVFaultTrend.Rows(count).Cells("FLT_K").Value = True Then
                If DGVFaultTrend.Rows(count + 1).Cells("FLT_K").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_K").Value = True Then
                    MsgBox("Fault K twice in last 3 Doffs")
                End If

            End If


            If DGVFaultTrend.Rows(count).Cells("FLT_D").Value = True Then MyExcel.Cells(xlcount, 13) = 1 Else MyExcel.Cells(xlcount, 13) = "-"
            'MyExcel.Cells(count  , 13) = DGVFaultTrend.Rows(count).Cells("FLT_D").Value    'FLT_D

            If DGVFaultTrend.Rows(count).Cells("FLT_F").Value = True Then MyExcel.Cells(xlcount, 14) = 1 Else MyExcel.Cells(xlcount, 14) = "-"
            'MyExcel.Cells(count  , 14) = DGVFaultTrend.Rows(count).Cells("FLT_F").Value     'FLT_F

            If DGVFaultTrend.Rows(count).Cells("FLT_O").Value = True Then MyExcel.Cells(xlcount, 15) = 1 Else MyExcel.Cells(xlcount, 15) = "-"
            'MyExcel.Cells(count  , 15) = DGVFaultTrend.Rows(count).Cells("FLT_O").Value     'FLT_O

            If DGVFaultTrend.Rows(count).Cells("FLT_T").Value = True Then MyExcel.Cells(xlcount, 16) = 1 Else MyExcel.Cells(xlcount, 16) = "-"
            'MyExcel.Cells(count  , 16) = DGVFaultTrend.Rows(count).Cells("FLT_T").Value   'FLT_T

            If DGVFaultTrend.Rows(count).Cells("FLT_P").Value = True Then MyExcel.Cells(xlcount, 17) = 1 Else MyExcel.Cells(xlcount, 17) = "-"
            'MyExcel.Cells(count  , 17) = DGVFaultTrend.Rows(count).Cells("FLT_P").Value     'FLT_P

            If DGVFaultTrend.Rows(count).Cells("FLT_N").Value = True Then MyExcel.Cells(xlcount, 18) = 1 Else MyExcel.Cells(xlcount, 18) = "-"
            ' MyExcel.Cells(count  , 18) = DGVFaultTrend.Rows(count).Cells("FLT_N").Value    'FLT_N

            If DGVFaultTrend.Rows(count).Cells("FLT_W").Value = True Then MyExcel.Cells(xlcount, 19) = 1 Else MyExcel.Cells(xlcount, 19) = "-"
            'MyExcel.Cells(count  , 19) = DGVFaultTrend.Rows(count).Cells("FLT_W").Value   'FLT_W

            If DGVFaultTrend.Rows(count).Cells("FLT_H").Value = True Then MyExcel.Cells(xlcount, 20) = 1 Else MyExcel.Cells(xlcount, 20) = "-"
            'MyExcel.Cells(count  , 20) = DGVFaultTrend.Rows(count).Cells("FLT_H").Value     'FLT_H

            If DGVFaultTrend.Rows(count).Cells("FLT_TR").Value = True Then MyExcel.Cells(xlcount, 21) = 1 Else MyExcel.Cells(xlcount, 21) = "-"
            'MyExcel.Cells(count  , 21) = DGVFaultTrend.Rows(count).Cells("FLT_TR").Value    'FLT_TR

            If DGVFaultTrend.Rows(count).Cells("FLT_B").Value = True Then MyExcel.Cells(xlcount, 22) = 1 Else MyExcel.Cells(xlcount, 22) = "-"
            'MyExcel.Cells(count  , 22) = DGVFaultTrend.Rows(count).Cells("FLT_B").Value    'FLT_B

            If DGVFaultTrend.Rows(count).Cells("FLT_C").Value = True Then MyExcel.Cells(xlcount, 23) = 1 Else MyExcel.Cells(xlcount, 23) = "-"
            'MyExcel.Cells(count  , 23) = DGVFaultTrend.Rows(count).Cells("FLT_C").Value    'FLT_C

            If DGVFaultTrend.Rows(count).Cells("FLT_DO").Value = True Then MyExcel.Cells(xlcount, 24) = 1 Else MyExcel.Cells(xlcount, 24) = "-"
            'MyExcel.Cells(count  , 24) = DGVFaultTrend.Rows(count).Cells("FLT_DO").Value    'FLT_DO

            If DGVFaultTrend.Rows(count).Cells("FLT_DH").Value = True Then MyExcel.Cells(xlcount, 25) = 1 Else MyExcel.Cells(xlcount, 25) = "-"
            'MyExcel.Cells(count  , 25) = DGVFaultTrend.Rows(count).Cells("FLT_DH").Value    'FLT_DH

            If DGVFaultTrend.Rows(count).Cells("FLT_CL").Value = True Then MyExcel.Cells(xlcount, 26) = 1 Else MyExcel.Cells(xlcount, 26) = "-"
            'MyExcel.Cells(count  , 26) = DGVFaultTrend.Rows(count).Cells("FLT_CL").Value     'FLT_CL

            If DGVFaultTrend.Rows(count).Cells("FLT_FI").Value = True Then MyExcel.Cells(xlcount, 27) = 1 Else MyExcel.Cells(xlcount, 27) = "-"
            'MyExcel.Cells(count  , 27) = DGVFaultTrend.Rows(count).Cells("FLT_FI").Value   'FLT_FI

            If DGVFaultTrend.Rows(count).Cells("FLT_YN").Value = True Then MyExcel.Cells(xlcount, 28) = 1 Else MyExcel.Cells(xlcount, 28) = "-"
            'MyExcel.Cells(count  , 28) = DGVFaultTrend.Rows(count).Cells("FLT_YN").Value    'FLT_YN

            If DGVFaultTrend.Rows(count).Cells("FLT_HT").Value = True Then MyExcel.Cells(xlcount, 29) = 1 Else MyExcel.Cells(xlcount, 29) = "-"
            'MyExcel.Cells(count  , 29) = DGVFaultTrend.Rows(count).Cells("FLT_HT").Value    'FLT_HT

            If DGVFaultTrend.Rows(count).Cells("FLT_LT").Value = True Then MyExcel.Cells(xlcount, 30) = 1 Else MyExcel.Cells(xlcount, 30) = "-"
            'MyExcel.Cells(count  , 30) = DGVFaultTrend.Rows(count).Cells("FLT_LT").Value    'FLT_LT




            '    'GET WEIGHT FROM OTHER TABLE
            '    SQL.ExecQuery("SELECT * FROM product WHERE PRNUM = '" & prodNum & "' ")

            '    Dim prodcount = SQL.RecordCount


            '    If prodcount > 0 Then
            '        DGVDefProdData.DataSource = SQL.SQLDS.Tables(0)
            '        DGVDefProdData.Rows(0).Selected = True
            '    End If

            '    MyExcel.Cells(xlcount, 9) = DGVDefProdData.Rows(0).Cells("PRODWEIGHT").Value      'WEIGHT

            '    xlcount = xlcount + 1  'INC COUNT FOR ROW ON EXCEL

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
            DGVFaultTrend.Dispose()
            'DGVDefProdData.Dispose()
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

        DGVFaultTrend.Dispose()
        'DGVDefProdData.Dispose()
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

End Class