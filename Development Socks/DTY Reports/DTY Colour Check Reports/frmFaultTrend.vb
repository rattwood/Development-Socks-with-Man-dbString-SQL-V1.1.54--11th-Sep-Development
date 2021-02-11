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

    'Public Sub CreateHeaders()

    '    'Clear DGV
    '    DGVFaultTrend.Columns.Clear()


    '    'PROPERTIES
    '    ' DGVFaultTrend.SelectionMode = DataGridViewSelectionMode.FullRowSelect    'Always WORK ON FULL ROW
    '    DGVFaultTrend.ColumnCount = 34       'NUMBER OF COLUMNS
    '    DGVFaultTrend.Rows.Add(conecount)
    '    'Construct the Columns


    '    'CREATE COLUM HEADERS
    '    DGVFaultTrend.Columns(0).Name = "Date"               'DATE
    '    DGVFaultTrend.Columns(1).Name = "Day"                'DAY
    '    DGVFaultTrend.Columns(2).Name = "Month"              'MONTH
    '    DGVFaultTrend.Columns(3).Name = "Year"               'YEAR
    '    DGVFaultTrend.Columns(4).Name = "Product"            'PRODUCT
    '    DGVFaultTrend.Columns(5).Name = "Merge"              'MERGE #
    '    DGVFaultTrend.Columns(6).Name = "Machine"            'MACHINE NAME
    '    DGVFaultTrend.Columns(7).Name = "Chip Type"          'CHIP TYPE
    '    DGVFaultTrend.Columns(8).Name = "Weight"             'WEIGHT
    '    DGVFaultTrend.Columns(9).Name = "Doffing"            'DOFFING #
    '    DGVFaultTrend.Columns(10).Name = "Cheese No."        'CHEESE NUMBER
    '    DGVFaultTrend.Columns(11).Name = "  K  "             'FLT_K  
    '    DGVFaultTrend.Columns(12).Name = "  D  "             'FLT_D
    '    DGVFaultTrend.Columns(13).Name = "  F  "             'FLT_F
    '    DGVFaultTrend.Columns(14).Name = "  O  "             'FLT_O
    '    DGVFaultTrend.Columns(15).Name = "  T  "             'FLT_T
    '    DGVFaultTrend.Columns(16).Name = "  P  "             'FLT_P
    '    DGVFaultTrend.Columns(17).Name = "  S  "             'FLT_S
    '    DGVFaultTrend.Columns(18).Name = "  X  "             'FLT_X
    '    DGVFaultTrend.Columns(19).Name = "  N  "             'FLT_N
    '    DGVFaultTrend.Columns(20).Name = "  W  "             'FLT_W
    '    DGVFaultTrend.Columns(21).Name = "  H  "             'FLT_H
    '    DGVFaultTrend.Columns(22).Name = "  TR  "            'FLT_TR
    '    DGVFaultTrend.Columns(23).Name = "  B  "             'FLT_B
    '    DGVFaultTrend.Columns(24).Name = "  C  "             'FLT_C
    '    DGVFaultTrend.Columns(25).Name = "Colour Waste "     'COLWASTE
    '    DGVFaultTrend.Columns(26).Name = "  DO  "            'FLT_DO
    '    DGVFaultTrend.Columns(27).Name = "  DH  "            'FLT_DH
    '    DGVFaultTrend.Columns(28).Name = "  CL  "            'FLT_CL
    '    DGVFaultTrend.Columns(29).Name = "  FI  "            'FLT_FI
    '    DGVFaultTrend.Columns(30).Name = "  YN  "            'FLT_YN
    '    DGVFaultTrend.Columns(31).Name = "  HT  "            'FLT_HT
    '    DGVFaultTrend.Columns(32).Name = "  LT  "            'FLT_LT
    '    DGVFaultTrend.Columns(33).Name = "  SORTENDTM  "     'SORT END TIME FROM DB




    '    If My.Settings.debugSet Then DGVFaultTrend.Show()

    'End Sub

    Public Sub DefTrend()

        'Excel Items
        'Dim savename As String

        'template = (My.Settings.dirTemplate & "\" & "DefectSortingTemplate.xlsx").ToString

        'If template = "" Then
        '    MsgBox("Please set template file location in Settings")
        '    Exit Sub
        'End If



        'Dim workbook As Excel.Workbook
        'Dim sheet As Excel.Worksheets


        'savename = (My.Settings.dirJobs & "\" & "Defect Sorting_" & startDate & "_" & endDate & ".xlsx").ToString



        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        'Get count of Cheeses with faults on current cart



        coneNum = lblL.coneNumOffset + 1
        Dim cheeseNum As Integer

        For cheeseNum = coneNum To (coneNum + 32) - 1

            DEFSQL.ExecQuery("Select * From jobs Where MCNUM = '" & frmJobEntry.varMachineCode & "' And PRNUM = '" & frmJobEntry.varProductCode & "' And CONENUM = '" & cheeseNum & "'And PRYY = '" & frmJobEntry.varYear & "'  And PRMM = '" & frmJobEntry.varMonth & "' And DOFFNUM Between '" & frmJobEntry.varDoffingNum - 3 & "' And '" & frmJobEntry.varDoffingNum & "' And CONESTATE BETWEEN 8 And 9 ")


            'Load THE DATA FOR COMPLETE JOB
            'SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM BETWEEN ('" & Label5.Text & "') AND  ('" & Label6.Text & "') And DEFCONE > 0 And MISSCONE = 0 ")

            conecount = DEFSQL.RecordCount


            If conecount > 0 Then
                'CreateHeaders()

                DGVFaultTrend.DataSource = DEFSQL.SQLDS.Tables(0)
                DGVFaultTrend.Rows(0).Selected = True

                'SORT DGV TABLE BY PRODUCT NAME
                DGVFaultTrend.Sort(DGVFaultTrend.Columns("DOFFNUM"), ListSortDirection.Ascending)

            Else
                'MsgBox("No Defect Cheeses")
                'MyExcel = Nothing
                'workbook = Nothing
                'sheet = Nothing
                DGVFaultTrend.Dispose()
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If

            'CreateHeaders()

            'workbook = MyExcel.Workbooks.Open(template)
            'MyExcel.Visible = True



            'Dim dbDate As Date
            'Dim sortDate As String
            'Dim dayDate As String
            'Dim xlcount As Integer = 2   'START ROW ON EXCEL SHEET

            For count = 1 To 3 - 1


                'DATE AND DAY INFO
                'If IsDBNull(DGVFaultTrend.Rows(count).Cells("SORTENDTM").Value) Then
                '    sortDate = "1900-00-00"
                '    dayDate = "00"

                'Else
                '    dbDate = DGVFaultTrend.Rows(count).Cells("SORTENDTM").Value
                '    sortDate = dbDate.ToString("dd-MM-yyyy")
                '    dayDate = dbDate.ToString("dd")
                'End If




                'Dim sortDate As String = DGVFaultTrend.Rows(count).Cells("SORTENDTM").Value.ToString("dd-MM-yyy")
                'Dim dayDate = DGVFaultTrend.Rows(count).Cells("SORTENDTM").Value.ToString("dd")
                Dim prodNum = DGVFaultTrend.Rows(count).Cells("PRNUM").Value.ToString

                'MyExcel.Cells(xlcount, 1) = sortDate          'DATE
                'MyExcel.Cells(xlcount, 2) = dayDate           'DAY

                'MyExcel.Cells(xlcount, 3) = DGVFaultTrend.Rows(count).Cells("PRMM").Value     'MONTH

                'MyExcel.Cells(xlcount, 4) = DGVFaultTrend.Rows(count).Cells("PRYY").Value  'YEAR

                'MyExcel.Cells(xlcount, 5) = DGVFaultTrend.Rows(count).Cells("PRODNAME").Value   'PRODUCT

                'MyExcel.Cells(xlcount, 6) = DGVFaultTrend.Rows(count).Cells("MERGENUM").Value     'MERGE #

                'MyExcel.Cells(xlcount, 7) = DGVFaultTrend.Rows(count).Cells("MCNAME").Value      'MACHINE NAME

                'Dim chipType As String = DGVFaultTrend.Rows(count).Cells("PRODNAME").Value
                'chipType = chipType.Substring(chipType.Length - 4, 4)
                'MyExcel.Cells(xlcount, 8) = chipType                                               'GET CHIP TYPE FROM PRODUCT NAME

                'WEIGHT ROUTINE FOLLOWS THESE

                'MyExcel.Cells(xlcount, 10) = DGVFaultTrend.Rows(count).Cells("DOFFNUM").Value       'DOFFING #


                'MyExcel.Cells(xlcount, 11) = DGVFaultTrend.Rows(count).Cells("CONENUM").Value      'CHEESE NUMBER


                If DGVFaultTrend.Rows(count).Cells("M10").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("M10").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("M10").Value = True Then
                        MsgBox(" -10 twice in last 3 Doffs")
                    End If

                End If


                If DGVFaultTrend.Rows(count).Cells("P10").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("P10").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("P10").Value = True Then
                        MsgBox("Fault +10 twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("M30").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("M30").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("M30").Value = True Then
                        MsgBox("Fault -30 twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("P30").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("P30").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("P30").Value = True Then
                        MsgBox("Fault P30 twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("M50").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("M50").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("M50").Value = True Then
                        MsgBox("Fault -50 twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("P50").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("P50").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("P50").Value = True Then
                        MsgBox("Fault +50 twice in last 3 Doffs")
                    End If
                End If



                If DGVFaultTrend.Rows(count).Cells("FLT_K").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_K").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_K").Value = True Then
                        MsgBox("Fault K twice in last 3 Doffs")
                    End If

                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_D").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_D").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_D").Value = True Then
                        MsgBox("Fault D twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_F").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_F").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_F").Value = True Then
                        MsgBox("Fault F twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_O").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_O").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_O").Value = True Then
                        MsgBox("Fault O twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_T").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_T").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_T").Value = True Then
                        MsgBox("Fault T twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_P").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_P").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_P").Value = True Then
                        MsgBox("Fault P twice in last 3 Doffs")
                    End If
                End If

                If DGVFaultTrend.Rows(count).Cells("FLT_S").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_S").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_S").Value = True Then
                        MsgBox("Fault N twice in last 3 Doffs")
                    End If
                End If

                If DGVFaultTrend.Rows(count).Cells("FLT_X").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_X").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_X").Value = True Then
                        MsgBox("Fault X twice in last 3 Doffs")
                    End If
                End If

                If DGVFaultTrend.Rows(count).Cells("FLT_N").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_N").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_N").Value = True Then
                        MsgBox("Fault N twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_W").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_W").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_W").Value = True Then
                        MsgBox("Fault W twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_H").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_H").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_H").Value = True Then
                        MsgBox("Fault H twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_TR").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_TR").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_TR").Value = True Then
                        MsgBox("Fault TR twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_B").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_B").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_B").Value = True Then
                        MsgBox("Fault B twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_C").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_C").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_C").Value = True Then
                        MsgBox("Fault C twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_DO").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_DO").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_DO").Value = True Then
                        MsgBox("Fault DO twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_DH").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_DH").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_DH").Value = True Then
                        MsgBox("Fault DH twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_CL").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_CL").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_CL").Value = True Then
                        MsgBox("Fault CL twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_FI").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_FI").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_FI").Value = True Then
                        MsgBox("Fault FI twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_YN").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_YN").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_YN").Value = True Then
                        MsgBox("Fault YN twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_HT").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_HT").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_HT").Value = True Then
                        MsgBox("Fault HT twice in last 3 Doffs")
                    End If
                End If


                If DGVFaultTrend.Rows(count).Cells("FLT_LT").Value = True Then
                    If DGVFaultTrend.Rows(count + 1).Cells("FLT_LT").Value = True Or DGVFaultTrend.Rows(count + 2).Cells("FLT_LT").Value = True Then
                        MsgBox("Fault LT twice in last 3 Doffs")
                    End If
                End If





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

        Next






        'clear variables







        'Try

        '    'Save changes to new file in Jobs Directory
        '    'MyExcel.DisplayAlerts = False
        '    'workbook.SaveAs(Filename:=savename, FileFormat:=51)

        'Catch ex As Exception

        '    'MsgBox(ex.Message)
        '    'workbook.Close()
        '    'MyExcel.Quit()
        '    'releaseObject(workbook)
        '    'DGVFaultTrend.Dispose()
        '    'DGVDefProdData.Dispose()
        '    Me.Cursor = System.Windows.Forms.Cursors.Default
        '    Me.Close()
        '    Exit Sub
        'End Try

        'Try
        '    'Close template file but do not save updates to it

        '    'workbook.Close(SaveChanges:=False)
        '    'MyExcel.DisplayAlerts = True
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try




        'MyExcel.Quit()
        'releaseObject(workbook)

        DGVFaultTrend.Dispose()
        'DGVDefProdData.Dispose()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        'MsgBox("Job Report " & savename & " Created")
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
