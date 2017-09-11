Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering
Imports Microsoft.Office
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmDGVJobReport

    Private SQL As New SQLConn

    'Local Database connection
    Public LConn As New SQLConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private LCmd As SQLCommand

    'SQL CONNECTORS
    Public LDA As SQLDataAdapter
    Public LDS As DataSet
    Public LDT As DataTable
    Public LCB As SQLCommandBuilder

    Public LRecordCount As Integer
    Private LException As String
    ' SQL QUERY PARAMETERS
    Public LParams As New List(Of SQLParameter)

    Public coneNumStart As String = Nothing  'Get Macine number to then set correct spindle Number range on grid
    Public coneNumEnd As String = Nothing  'Get Macine number to then set correct spindle Number range on grid
    Private mcname As String
    Private Sortcount As Integer = Nothing
    Private count As Integer = Nothing
    Public startDate As String
    Public endDate As String


    Dim sp_nums As String
    Dim template As String
    Dim FileInName As String
    Dim MyExcel As New Excel.Application



    Private Sub CreateHeaders()
        'Clear DGV
        DGVReportData.Columns.Clear()


        'PROPERTIES
        ' DGVReportData.SelectionMode = DataGridViewSelectionMode.FullRowSelect    'Always WORK ON FULL ROW
        DGVReportData.ColumnCount = 208        'NUMBER OF COLUMNS
        DGVReportData.Rows.Add(Sortcount)
        'Construct the Columns


        'CREATE COLUM HEADERS
        DGVReportData.Columns(0).Name = "M/C Name"              'machineName
        DGVReportData.Columns(1).Name = "DD"                    'Day
        DGVReportData.Columns(2).Name = "MM"                    'Month
        DGVReportData.Columns(3).Name = "YY"                    'Yesr
        DGVReportData.Columns(4).Name = "Prod Name"             'prodYY  from BarCode
        DGVReportData.Columns(5).Name = "Merge #"               'Merge # from BarCode
        DGVReportData.Columns(6).Name = "Doff #"                'doffNum  from BarCode
        DGVReportData.Columns(7).Name = "Checker"               'Colour Cheker Name
        DGVReportData.Columns(8).Name = "Dark Count"            'DARK Count (P values)
        DGVReportData.Columns(9).Name = "Light Count"           'LIGHT Count (M values)
        DGVReportData.Columns(10).Name = "AB Count"             'AB Count
        DGVReportData.Columns(11).Name = "Colour Waste Count"   'Color Waste Count
        DGVReportData.Columns(12).Name = "Total Judged"         'Total Judged
        DGVReportData.Columns(13).Name = "+S Count"             '+S Count
        DGVReportData.Columns(14).Name = "-S Count"              '-S Count
        DGVReportData.Columns(15).Name = "Total Short"          'SHORT Count
        DGVReportData.Columns(16).Name = "Total Full"           'FULL Count
        DGVReportData.Columns(17).Name = "Dark Ratio"           'Dark Ratio (P values)
        DGVReportData.Columns(18).Name = "Light Ratio"          'Light Ratio (M values)
        DGVReportData.Columns(19).Name = "AB Ratio"             'AB Ratio
        'DGVReportData.Columns(20).Name = "Colour Waste Ratio"    'Colour Waste Ratio
        DGVReportData.Columns(20).Name = "Medium Colour Ratio"  'Med Colour Ratio


        If My.Settings.debugSet Then DGVReportData.Show()

    End Sub



    Private Sub lstMCName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMCName.SelectedIndexChanged

        'Dim cartSelect As String
        If lstMCName.Text = "11D1" Or lstMCName.Text = "12D1" Or lstMCName.Text = "21D1" Then
            coneNumStart = 1

        ElseIf lstMCName.Text = "11D2" Or lstMCName.Text = "12D2" Or lstMCName.Text = "21D2" Then
            coneNumStart = 193
        End If

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

        template = (My.Settings.dirTemplate & "\" & "JobReportTemplate.xlsx").ToString

        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Exit Sub
        End If



        Dim workbook As Excel.Workbook
        Dim sheet As Excel.Worksheets


        savename = (My.Settings.dirJobs & "\" & startDate & "_" & endDate & ".xlsx").ToString





        'GET THE JOB NUMBERS FROM THE DATABASE IN THE DATE RANGE GIVEN
        SQL.ExecQuery("SELECT DISTINCT BCODEJOB FROM JOBS WHERE COLENDTM BETWEEN ('" & Label5.Text & "') AND  ('" & Label6.Text & "')  ")

        Sortcount = SQL.RecordCount


        'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
        If Sortcount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVSort.DataSource = SQL.SQLDS.Tables(0)
            DGVSort.Rows(0).Selected = True

            'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
            DGVSort.Sort(DGVSort.Columns("BCODEJOB"), ListSortDirection.Ascending)  'sorts On cone number

        Else
            MsgBox("No Jobs Found, Please select new date range")
            DGVSort.ClearSelection()
            Exit Sub
        End If

        Dim dbBarcode As String

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        CreateHeaders()

        workbook = MyExcel.Workbooks.Open(template)
        'MyExcel.Visible = True

        'LOAD FULL DATA FOR EACH JOB ONE JOB AT A TIME
        For count As Integer = 0 To Sortcount - 1 'DGVSort.Rows.Count
            dbBarcode = DGVSort.Rows(count).Cells("BCODEJOB").Value.ToString



            'Load THE DATA FOR COMPLETE JOB
            SQL.ExecQuery("SELECT * FROM jobs WHERE BCODEJOB = '" & dbBarcode & "'")

            Dim conecount = SQL.RecordCount
            Dim NotFinCount As Integer

            If conecount < 192 Then
                If NotFinCount = Sortcount - 1 Then
                    MsgBox("Cones for all jobs Not finished")
                    MyExcel = Nothing
                    workbook = Nothing
                    sheet = Nothing
                    MyExcel.Quit()
                    DGVReportData.Dispose()
                    Exit Sub
                Else
                    NotFinCount = NotFinCount + 1
                    'MsgBox(NotFinCount & "  " & Sortcount)
                    Continue For

                End If
            End If


            DGVJob.DataSource = SQL.SQLDS.Tables(0)
            DGVJob.Rows(0).Selected = True

            'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE

            DGVJob.Sort(DGVJob.Columns("CONENUM"), ListSortDirection.Ascending)  'sorts On cone number
            'CreateHeaders()

            coneNumStart = DGVJob.Rows(0).Cells("CONENUM").Value.ToString


            Dim countMissing As Integer     'MISSING CONE COUNT
            Dim countDefect As Integer      'PHYSICAL DEFECT COUNT
            Dim countBarley As Integer      'BARRE COUNT
            Dim countShortM10, countShortP10, countShortM30, countShortP30, countShortM50, countShortP50, countShortA, countShortML, countShortMD As Integer  'COUNTS FOR DIFFERENT SHORT TYPES
            Dim countShort0 As Integer       '
            Dim count0 As Integer           'ZERO CONE COUNT
            Dim countM10 As Integer         'M10 CONE COUNT
            Dim countP10 As Integer         'P10 CONE COUNT
            Dim countM30 As Integer         'M30 CONE COUNT
            Dim countP30 As Integer         'P30 CONE COUNT
            Dim countM50 As Integer         'M50 CONE COUNT
            Dim countP50 As Integer         'P50 CONE COUNT
            Dim countColWaste As Integer    'COLOUR WASTE CONE COUNT
            Dim coneMS As Integer = 0        '- Cones Count Light Cones
            Dim conePS As Integer = 0        '+ Cones Count Dark Cones
            For x As Integer = 1 To 192


                If DGVJob.Rows(x).Cells("CONEZERO").Value > 0 Then
                    count0 = count0 + 1
                    If DGVJob.Rows(x).Cells("SHORTCONE").Value > 0 Then
                        DGVReportData.Rows(count).Cells(16 + x).Value = "0S  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Green
                        countShort0 = countShort0 + 1
                        MyExcel.Cells(count + 2, (23 + x)) = "0S  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Green
                    Else
                        DGVReportData.Rows(count).Cells(16 + x).Value = "0  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Green
                        MyExcel.Cells(count + 2, (23 + x)) = "0  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Green
                    End If
                End If

                If DGVJob.Rows(x).Cells("M10").Value > 0 Then
                    countM10 = countM10 + 1
                    If DGVJob.Rows(x).Cells("SHORTCONE").Value > 0 Then
                        DGVReportData.Rows(count).Cells(16 + x).Value = "-S  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Green
                        countShortM10 = countShortM10 + 1
                        MyExcel.Cells(count + 2, (23 + x)) = "-S  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Green
                    Else
                        DGVReportData.Rows(count).Cells(16 + x).Value = "-10  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Green
                        MyExcel.Cells(count + 2, (23 + x)) = "-10  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Green
                    End If
                End If

                If DGVJob.Rows(x).Cells("P10").Value > 0 Then
                    countP10 = countP10 + 1
                    If DGVJob.Rows(x).Cells("SHORTCONE").Value > 0 Then
                        DGVReportData.Rows(count).Cells(16 + x).Value = "+S  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Green
                        countShortP10 = countShortP10 + 1
                        MyExcel.Cells(count + 2, (23 + x)) = "+S  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Green
                    Else
                        DGVReportData.Rows(count).Cells(16 + x).Value = "+10  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Green
                        MyExcel.Cells(count + 2, (23 + x)) = "+10  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Green
                    End If
                End If

                If DGVJob.Rows(x).Cells("M30").Value > 0 Then
                    countM30 = countM30 + 1
                    If DGVJob.Rows(x).Cells("SHORTCONE").Value > 0 Then
                        DGVReportData.Rows(count).Cells(16 + x).Value = "-S  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.LightBlue
                        countShortM30 = countShortM30 + 1
                        MyExcel.Cells(count + 2, (23 + x)) = "-S  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.LightBlue

                    Else
                        DGVReportData.Rows(count).Cells(16 + x).Value = "-30  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.LightBlue
                        MyExcel.Cells(count + 2, (23 + x)) = "-30  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.LightBlue
                    End If
                End If

                If DGVJob.Rows(x).Cells("P30").Value > 0 Then
                    countP30 = countP30 + 1
                    If DGVJob.Rows(x).Cells("SHORTCONE").Value > 0 Then
                        DGVReportData.Rows(count).Cells(16 + x).Value = "+S  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.LightSalmon
                        countShortP30 = countShortP30 + 1
                        MyExcel.Cells(count + 2, (23 + x)) = "+S  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.LightSalmon
                    Else
                        DGVReportData.Rows(count).Cells(16 + x).Value = "+30  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.LightSalmon
                        MyExcel.Cells(count + 2, (23 + x)) = "+30  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.LightSalmon
                    End If
                End If


                If DGVJob.Rows(x).Cells("M50").Value > 0 Then
                    countM50 = countM50 + 1
                    If DGVJob.Rows(x).Cells("SHORTCONE").Value > 0 Then
                        DGVReportData.Rows(count).Cells(16 + x).Value = "-50S  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.LightSalmon
                        countShortM50 = countShortM50 + 1
                        MyExcel.Cells(count + 2, (23 + x)) = "ABS  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.LightSalmon
                    Else
                        DGVReportData.Rows(count).Cells(16 + x).Value = "-50  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Yellow
                        MyExcel.Cells(count + 2, (23 + x)) = "AB  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Yellow
                    End If
                End If

                If DGVJob.Rows(x).Cells("P50").Value > 0 Then
                    countP50 = countP50 + 1
                    If DGVJob.Rows(x).Cells("SHORTCONE").Value > 0 Then
                        DGVReportData.Rows(count).Cells(16 + x).Value = "+50S  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.LightSalmon
                        countShortP50 = countShortP50 + 1
                        MyExcel.Cells(count + 2, (23 + x)) = "ABS  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.LightSalmon
                    Else
                        DGVReportData.Rows(count).Cells(16 + x).Value = "+50  " '& coneNumStart + x
                        DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Yellow
                        MyExcel.Cells(count + 2, (23 + x)) = "AB  " '& coneNumStart + x
                        MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Yellow
                    End If
                End If


                If DGVJob.Rows(x).Cells("MISSCONE").Value > 0 Then
                    countMissing = countMissing + 1

                    DGVReportData.Rows(count).Cells(16 + x).Value = "MISS    " '& coneNumStart + x
                    DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Black
                    MyExcel.Cells(count + 2, (23 + x)) = "MISS  " '& coneNumStart + x

                End If

                'If DGVJob.Rows(x).Cells("DEFCONE").Value > 0 Then
                '    countDefect = countDefect + 1

                '    DGVReportData.Rows(count).Cells(16 + x).Value = "AB " '& coneNumStart + x
                '    DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Yellow
                '    MyExcel.Cells(count + 2, (23 + x)) = "AB  " '& coneNumStart + x
                '    MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Yellow

                'End If

                If DGVJob.Rows(x).Cells("CONEBARLEY").Value > 0 Then
                    countBarley = countBarley + 1
                    DGVReportData.Rows(count).Cells(16 + x).Value = "AB " '& coneNumStart + x
                    DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Yellow
                    MyExcel.Cells(count + 2, (23 + x)) = "AB  " '& coneNumStart + x
                    MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Yellow

                End If

                If DGVJob.Rows(x).Cells("COLWASTE").Value > 0 Then
                    countColWaste = countColWaste + 1
                    DGVReportData.Rows(count).Cells(16 + x).Value = "CW " '& coneNumStart + x
                    DGVReportData.Rows(count).Cells(16 + x).Style.BackColor = Color.Purple
                    MyExcel.Cells(count + 2, (23 + x)) = "CW  " '& coneNumStart + x
                    MyExcel.Cells(count + 2, (23 + x)).interior.color = Color.Purple

                End If

            Next
            Dim dbdate As Date
            Dim day As String

            'TOTALS
            Dim totalA, totalMD, totalML, totalM, totalP, totalAB, totalCW, totalJud, totalSP, totalSM, totalSmall, totalCart, totalfull As Integer

            totalCart = 192 - countMissing
            totalMD = count0 + countShort0 + countP10 + countShortP10
            totalML = countM30 + countShortM30
            totalM = countM30 + countShortM30
            totalP = countP30 + countShortP30
            totalAB = countBarley + countM50 + countShortM50 + countP50 + countShortP50
            totalCW = countColWaste
            totalJud = (countM30 + countShortM30) + (countP30 + countShortP30) + (countBarley + countM50 + countShortM50 + countP50 + countShortP50)
            totalSP = countShortP10
            totalSM = countShortM30
            totalSmall = totalSP + totalSM
            totalfull = (192 - (countMissing + totalCW + totalSmall))
            totalA = (192 - (totalML + totalM + totalP + totalAB + totalCW))



            'NOW TRANSFER DATA TO Job REPORT DATAGRID

            'MACHINE NUMBER
            DGVReportData.Rows(count).Cells("M/C Name").Value = DGVJob.Rows(count).Cells("MCNAME").Value
            MyExcel.Cells(count + 2, 1).value = DGVJob.Rows(count).Cells("MCNAME").Value

            'CHECK TO SEE IF DATE ALREADY SET FOR END TIME

            If IsDBNull(DGVJob.Rows(count).Cells("COLENDTM").Value) Then
                day = "00"
            Else
                DGVReportData.Rows(count).Cells("DD").Value = DGVJob.Rows(count).Cells("COLENDTM").Value
                dbdate = DGVReportData.Rows(count).Cells("DD").Value
                day = dbdate.ToString("dd")
                MyExcel.Cells(count + 2, 2).value = day
            End If



            'MONTH
            DGVReportData.Rows(count).Cells("MM").Value = DGVJob.Rows(count).Cells("PRMM").Value
            MyExcel.Cells(count + 2, 3).value = DGVJob.Rows(count).Cells("PRMM").Value

            'YEAR
            DGVReportData.Rows(count).Cells("YY").Value = DGVJob.Rows(count).Cells("PRYY").Value
            MyExcel.Cells(count + 2, 4) = DGVJob.Rows(count).Cells("PRYY").Value

            'PRODUCT NAME
            DGVReportData.Rows(count).Cells("Prod Name").Value = DGVJob.Rows(count).Cells("PRODNAME").Value
            MyExcel.Cells(count + 2, 5) = DGVJob.Rows(count).Cells("PRODNAME").Value

            'MERGE NUMBER
            DGVReportData.Rows(count).Cells("Merge #").Value = DGVJob.Rows(count).Cells("MERGENUM").Value
            MyExcel.Cells(count + 2, 6) = DGVJob.Rows(count).Cells("MERGENUM").Value

            'DOFFING NUMBER
            DGVReportData.Rows(count).Cells("Doff #").Value = DGVJob.Rows(count).Cells("DOFFNUM").Value
            MyExcel.Cells(count + 2, 7) = DGVJob.Rows(count).Cells("DOFFNUM").Value

            'COLOUR CHECKER
            DGVReportData.Rows(count).Cells("Checker").Value = DGVJob.Rows(count).Cells("OPCOLOUR").Value
            MyExcel.Cells(count + 2, 8) = DGVJob.Rows(count).Cells("OPCOLOUR").Value

            'TOTAL DARK COUNT
            DGVReportData.Rows(count).Cells("Dark Count").Value = totalP
            MyExcel.Cells(count + 2, 9) = totalP

            'TOTAL LIGHT COUNT
            DGVReportData.Rows(count).Cells("Light Count").Value = totalM
            MyExcel.Cells(count + 2, 10) = totalM

            'TOTAL AB COUNT
            DGVReportData.Rows(count).Cells("AB Count").Value = totalAB
            MyExcel.Cells(count + 2, 11) = totalAB

            'TOTAL COLOUR WASTE
            DGVReportData.Rows(count).Cells("Colour Waste Count").Value = totalCW
            MyExcel.Cells(count + 2, 12) = totalCW

            'TOTAL JUDGED
            DGVReportData.Rows(count).Cells("Total Judged").Value = totalJud
            MyExcel.Cells(count + 2, 13) = totalJud

            '+ Short Count
            DGVReportData.Rows(count).Cells("+S Count").Value = totalSP
            MyExcel.Cells(count + 2, 14) = totalSP


            '- Short Count
            DGVReportData.Rows(count).Cells("-S Count").Value = countShortP30
            MyExcel.Cells(count + 2, 15) = totalSM

            'TOTAL SHORT
            DGVReportData.Rows(count).Cells("Total Short").Value = totalSmall
            MyExcel.Cells(count + 2, 16) = totalSmall

            'TOTAL FULL
            DGVReportData.Rows(count).Cells("Total Full").Value = totalFull
            MyExcel.Cells(count + 2, 17) = totalFull

            'DARK RATIO
            DGVReportData.Rows(count).Cells("Dark Ratio").Value = ((totalP / totalCart) * 100)
            MyExcel.Cells(count + 2, 18) = ((totalP / totalCart) * 100)

            'LIGHT RATIO
            DGVReportData.Rows(count).Cells("Light Ratio").Value = ((totalM / totalCart) * 100)
            MyExcel.Cells(count + 2, 19) = ((totalM / totalCart) * 100)

            'AB RATIO
            DGVReportData.Rows(count).Cells("AB Ratio").Value = ((totalAB / totalCart) * 100)
            MyExcel.Cells(count + 2, 20) = (((totalAB + totalCW) / totalCart) * 100)

            'COLOUR WASTE RATIO
            'DGVReportData.Rows(count).Cells("Colour Waste Ratio").Value = ((totalCW / totalCart) * 100)
            'MyExcel.Cells(count + 2, 21) = ((totalCW / (totalCart) * 100))

            'MEDIUM RATIO
            DGVReportData.Rows(count).Cells("Medium Colour Ratio").Value = (((totalCart - totalJud) / totalCart) * 100)
            MyExcel.Cells(count + 2, 21) = (((totalCart - totalJud) / totalCart) * 100)


            'clear variables
            countMissing = 0
            countDefect = 0
            countBarley = 0
            countShortM10 = 0
            countShortP10 = 0
            countShortM30 = 0
            countShortP30 = 0
            countShortM50 = 0
            countShortP50 = 0
            countShortA = 0
            countShortML = 0
            countShortMD = 0
            countShort0 = 0
            count0 = 0
            countM10 = 0
            countP10 = 0
            countM30 = 0
            countP30 = 0
            countM50 = 0
            countP50 = 0
            countColWaste = 0
            coneMS = 0
            conePS = 0

        Next





        Try

            'Save changes to new file in CKJobs
            MyExcel.DisplayAlerts = False
            workbook.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)
            workbook.Close()
            MyExcel.Quit()
            releaseObject(workbook)
            DGVReportData.Dispose()
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


        DGVReportData.Dispose()

        Me.Cursor = System.Windows.Forms.Cursors.Default
        MsgBox("Job Report " & savename & " Created")

        Me.Close()


    End Sub





    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Title = "Select Job File "
        OpenFileDialog1.InitialDirectory = My.Settings.dirJobs
        OpenFileDialog1.ShowDialog()

    End Sub



    Private Sub frmDGVReportData_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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