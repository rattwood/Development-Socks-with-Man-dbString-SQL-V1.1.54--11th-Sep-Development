Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering
Imports Microsoft.Office
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmProdStockWork
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



    Private jobcount As Integer = Nothing
    Private count As Integer = Nothing
    Public startDate As String
    Public endDate As String


    Dim sp_nums As String
    Dim template As String
    Dim FileInName As String
    Dim prodName As String
    Dim prodnum As String
    Dim MyWRExcel As New Excel.Application




    Public Sub processReport()
        'Excel Items
        Dim savename As String
        Dim prodWeight As String

        template = (My.Settings.dirTemplate & "\" & "Stock Work in Process Report Template.xlsx").ToString

        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Exit Sub
        End If

        Dim workbookWR As Excel.Workbook
        Dim startDate = Date.Today
        Dim endDate = Date.Today.AddDays(-3)


        savename = (My.Settings.dirPackReports & "\" & "StockWorkFullReport" & "_" & Date.Today.ToString("dd_MM_yyy") & ".xlsx").ToString


        'GET LIST OF PRODUCTS TO BE PROCESSED AS OF NOW
        SQL.ExecQuery("SELECT DISTINCT PRNUM,PRODNAME,MERGENUM FROM JOBS WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' ")

        jobcount = SQL.RecordCount



        'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
        If jobcount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVNextJobsData.DataSource = SQL.SQLDS.Tables(0)
            DGVNextJobsData.Rows(0).Selected = True

            'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
            DGVNextJobsData.Sort(DGVNextJobsData.Columns("PRNUM"), ListSortDirection.Ascending)  'sorts On cone number

        Else
            MsgBox("No Jobs Found, Please select new date range")
            DGVNextJobsData.ClearSelection()
            Exit Sub
        End If



        workbookWR = MyWRExcel.Workbooks.Open(template)

        Dim lineCount As Integer = 0
        Dim reCheckCount As Integer = 0 'COUNT OF ReCHECK CONES
        Dim fullCount As Integer = 0
        Dim shortCone As Integer = 0
        Dim tblOpen As Integer = 0


        'SERIES OF COUNTS FROM DATABASE TO GET VALUES NEEDED FOR REPORT
        For count As Integer = 1 To jobcount 'DGVSort.Rows.Count
            prodnum = DGVNextJobsData.Rows(count - 1).Cells("PRNUM").Value.ToString

            'COUNT NUMBER OF CONES THAT ARE FULL INCLUDING WASTE OR COLOUR WASTE CHEESE
            'SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And  PRNUM = '" & prodnum & "' And CONESTATE Between  5 and  14 And FLT_S = 'False' AND PACKENDTM IS NULL")
            SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And  PRNUM = '" & prodnum & "' And CONESTATE Between  5 and  14 And FLT_S = 'False' and SHORTCONE = 0 And FLT_W = 'False' And COLWASTE = 0 And  RECHK = 0 AND MISSCONE = 0 And PACKENDTM IS NULL")

            fullCount = SQL.RecordCount

            If fullCount Then
                DGVOutputData.DataSource = SQL.SQLDS.Tables(0)
                DGVOutputData.Rows(0).Selected = True
                lineCount = lineCount + 1
                tblOpen = 1
            End If



            'COUNT MISSING CONES
            'SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And PRNUM = '" & prodnum & "' And  MISSCONE > 0 ")
            'Dim missCone = SQL.RecordCount


            'COUNT NUMBER OF CONE THAT ARE SHORT
            SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And PRNUM = '" & prodnum & "' And CONESTATE Between  5 And  14 and FLT_S = 'TRUE' And FLT_W = 'False' And COLWASTE = 0  And PACKENDTM IS NULL ")
            shortCone = SQL.RecordCount

            If shortCone > 0 And Not tblOpen Then
                DGVOutputData.DataSource = SQL.SQLDS.Tables(0)
                DGVOutputData.Rows(0).Selected = True
                lineCount = lineCount + 1
                tblOpen = 1

            End If


            'COUNT WASTE CONES
            'SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And PRNUM = '" & prodnum & "' And CONESTATE Between  5 And  9 and (FLT_W = 'TRUE' Or  COLWASTE > 0) And PACKENDTM IS NULL ")
            'Dim wasteCone = SQL.RecordCount




            'COUNT ReCheck
            SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And PRNUM = '" & prodnum & "' And  RECHK Between 2 and 4 And  PACKENDTM IS NULL")
            reCheckCount = SQL.RecordCount


            Dim mergenum = DGVOutputData.Rows(0).Cells("MERGENUM").Value.ToString

            prodName = DGVOutputData.Rows(0).Cells("PRODNAME").Value.ToString



            'GET PRODUCT WEIGHT INFORMATION
            SQL.ExecQuery("SELECT * FROM Product WHERE PRNUM = '" & prodnum & "' ")

            'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
            If SQL.RecordCount > 0 Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                DGVPackWeight.DataSource = SQL.SQLDS.Tables(0)
                DGVPackWeight.Rows(0).Selected = True

                'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
                DGVPackWeight.Sort(DGVPackWeight.Columns("PRODNAME"), ListSortDirection.Ascending)  'sorts On cone number

            Else
                MsgBox("No Jobs Found, Please select new date range")
                DGVPackWeight.ClearSelection()
                Exit Sub
            End If


            prodWeight = DGVPackWeight.Rows(0).Cells("PRODWEIGHT").Value.ToString


            'MsgBox("Total =" & conecount & "  Full =" & fullCount & "   ReCheck =" & reCheckCount & "   Short =" & shortCone & "  Missount =" & missCone & "  Waste =" & wasteCone)




            MyWRExcel.Cells(count + 7, 1) = lineCount 'ROW INDEX
            MyWRExcel.Cells(count + 7, 2) = prodName 'PRODUCT NAME
            MyWRExcel.Cells(count + 7, 3) = mergenum 'MERGE NUMBER
            MyWRExcel.Cells(count + 7, 4) = prodWeight 'PRODUCT WEIGHT
            MyWRExcel.Cells(count + 7, 5) = fullCount 'FULL CONES
            MyWRExcel.Cells(count + 7, 6) = reCheckCount 'RECHECK CONES
            Dim CheeseFull = fullCount + reCheckCount
            MyWRExcel.Cells(count + 7, 7) = CheeseFull  'FULL CONES
            MyWRExcel.Cells(count + 7, 8) = CheeseFull * prodWeight  'TOTAL WEIGHT FULL CONES
            MyWRExcel.Cells(count + 7, 10) = shortCone 'SHORT CONES
            MyWRExcel.Cells(count + 7, 11) = shortCone 'SHORT CONES
            MyWRExcel.Cells(count + 7, 12) = shortCone * 2.7 'TOTAL SHORT CONE WEIGHT
            reCheckCount = 0
            fullCount = 0
            shortCone = 0
            tblOpen = 0






        Next


        'LINE NUMBER

        MyWRExcel.Cells(3, 9).value = Date.Today.ToString("dd-MM-yyyy")
        MyWRExcel.Cells(3, 12).value = TimeOfDay.ToString("hh:mm")



        Try

            'Save changes to new file in CKJobs
            MyWRExcel.DisplayAlerts = False
            workbookWR.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)
            workbookWR.Close()
            MyWRExcel.Quit()
            releaseObject(workbookWR)
            releaseObject(MyWRExcel)
            DGVOutputData.Dispose()
            DGVNextJobsData.Dispose()
            DGVPackWeight.Dispose()
            Me.Close()
            Exit Sub
        End Try

        Try
            'Close template file but do not save updates to it

            workbookWR.Close(SaveChanges:=False)
            MyWRExcel.DisplayAlerts = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        DGVOutputData.Dispose()
        DGVNextJobsData.Dispose()
        DGVPackWeight.Dispose()




        'CLEAN UP
        MyWRExcel.Quit()

        releaseObject(workbookWR)
        releaseObject(MyWRExcel)
        MsgBox("Full Stock Work in Process Report " & savename & " Created")



    End Sub


    Public Sub processShortReport()
        'Excel Items
        Dim savename As String
        Dim prodWeight As String

        template = (My.Settings.dirTemplate & "\" & "Stock Work in Process Short Report Template.xlsx").ToString

        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Exit Sub
        End If

        Dim workbookWR As Excel.Workbook
        Dim startDate = Date.Today
        Dim endDate = Date.Today.AddDays(-3)


        savename = (My.Settings.dirPackReports & "\" & "StockWorkShortReport" & "_" & Date.Today.ToString("dd_MM_yyy") & ".xlsx").ToString


        'GET LIST OF PRODUCTS TO BE PROCESSED AS OF NOW
        SQL.ExecQuery("SELECT DISTINCT PRNUM,PRODNAME,MERGENUM FROM JOBS WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And CONESTATE Between 5 And  9 And (PACKCARTTM is Null Or RECHK between 2 and 4) ")

        jobcount = SQL.RecordCount



        'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
        If jobcount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVNextJobsData.DataSource = SQL.SQLDS.Tables(0)
            DGVNextJobsData.Rows(0).Selected = True

            'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
            DGVNextJobsData.Sort(DGVNextJobsData.Columns("PRNUM"), ListSortDirection.Ascending)  'sorts On Product Number

        Else
            MsgBox("No Jobs Found, Please select new date range")
            DGVNextJobsData.ClearSelection()
            Exit Sub
        End If



        workbookWR = MyWRExcel.Workbooks.Open(template)

        Dim lineCount As Integer = 0
        Dim fullCount As Integer = 0
        Dim reCheckCount As Integer 'COUNT OF ReCHECK CONES
        Dim shortCone = 0
        Dim tblOpen As Integer = 0

        'SERIES OF COUNTS FROM DATABASE TO GET VALUES NEEDED FOR REPORT
        For count As Integer = 1 To jobcount 'DGVSort.Rows.Count
            prodnum = DGVNextJobsData.Rows(count - 1).Cells("PRNUM").Value.ToString

            'COUNT NUMBER OF CONES THAT ARE FULL
            'SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And  PRNUM = '" & prodnum & "' And CONESTATE Between  5 and  9 And FLT_S = 'False' And PACKCARTTM IS NULL ")
            SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And  PRNUM = '" & prodnum & "' And CONESTATE Between  5 and  9 And FLT_S = 'False' and SHORTCONE = 0 And FLT_W = 'False' And COLWASTE = 0 And  RECHK = 0 AND MISSCONE = 0 And PACKCARTTM IS NULL")




            fullCount = SQL.RecordCount


            If fullCount Then
                DGVOutputData.DataSource = SQL.SQLDS.Tables(0)
                DGVOutputData.Rows(0).Selected = True
                lineCount = lineCount + 1
                tblOpen = 1

            End If




            ''COUNT MISSING CONES
            'SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And PRNUM = '" & prodnum & "' And  MISSCONE > 0 And  PACKCARTTM IS NULL")
            'Dim missCone = SQL.RecordCount


            'COUNT NUMBER OF CONE THAT ARE SHORT
            SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And PRNUM = '" & prodnum & "' And CONESTATE Between  5 And  9 and (FLT_S = 'TRUE' Or SHORTCONE > 0) And FLT_W = 'False' And COLWASTE = 0  And  PACKCARTTM IS NULL ")
            shortCone = SQL.RecordCount

            If shortCone > 0 And Not tblOpen Then
                DGVOutputData.DataSource = SQL.SQLDS.Tables(0)
                DGVOutputData.Rows(0).Selected = True
                lineCount = lineCount + 1
                tblOpen = 1
            End If


            ''COUNT WASTE CONES
            'SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And PRNUM = '" & prodnum & "' And CONESTATE Between  5 And  9 and (FLT_W = 'TRUE' Or COLWASTE > 0) And  PACKCARTTM IS NULL")
            'Dim wasteCone = SQL.RecordCount



            'COUNT ReCheck
            SQL.ExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & endDate & "' And '" & startDate & "' And PRNUM = '" & prodnum & "' And  RECHK Between 2 and 4 And  PACKENDTM IS NULL")

            reCheckCount = SQL.RecordCount

            If reCheckCount > 0 And Not tblOpen Then
                DGVOutputData.DataSource = SQL.SQLDS.Tables(0)
                DGVOutputData.Rows(0).Selected = True
                lineCount = lineCount + 1
                tblOpen = 1
            Else
                Continue For
            End If



            Dim mergenum = DGVOutputData.Rows(0).Cells("MERGENUM").Value.ToString

            prodName = DGVOutputData.Rows(0).Cells("PRODNAME").Value.ToString


            'GET PRODUCT WEIGHT INFORMATION
            SQL.ExecQuery("SELECT * FROM Product WHERE PRNUM = '" & prodnum & "' ")

            'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
            If SQL.RecordCount > 0 Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                DGVPackWeight.DataSource = SQL.SQLDS.Tables(0)
                DGVPackWeight.Rows(0).Selected = True

                'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
                DGVPackWeight.Sort(DGVPackWeight.Columns("PRODNAME"), ListSortDirection.Ascending)  'sorts On cone number

            Else
                MsgBox("No Jobs Found, Please select new date range")
                DGVPackWeight.ClearSelection()
                Exit Sub
            End If


            prodWeight = DGVPackWeight.Rows(0).Cells("PRODWEIGHT").Value.ToString




            MyWRExcel.Cells(count + 7, 1) = lineCount 'ROW INDEX
            MyWRExcel.Cells(count + 7, 2) = prodName 'PRODUCT NAME
            MyWRExcel.Cells(count + 7, 3) = mergenum 'MERGE NUMBER
            MyWRExcel.Cells(count + 7, 4) = prodWeight 'PRODUCT WEIGHT
            MyWRExcel.Cells(count + 7, 5) = fullCount 'FULL CONES
            MyWRExcel.Cells(count + 7, 6) = reCheckCount 'RECHECK CONES
            Dim CheeseFull = fullCount + reCheckCount
            MyWRExcel.Cells(count + 7, 7) = CheeseFull  'FULL CONES
            MyWRExcel.Cells(count + 7, 8) = CheeseFull * prodWeight  'TOTAL WEIGHT FULL CONES
            MyWRExcel.Cells(count + 7, 10) = shortCone 'SHORT CONES
            MyWRExcel.Cells(count + 7, 11) = shortCone 'SHORT CONES
            MyWRExcel.Cells(count + 7, 12) = shortCone * 2.7 'TOTAL SHORT CONE WEIGHT


            reCheckCount = 0
            fullCount = 0
            shortCone = 0
            tblOpen = 0



        Next


        'LINE NUMBER

        MyWRExcel.Cells(3, 9).value = Date.Today.ToString("dd-MM-yyyy")
        MyWRExcel.Cells(3, 12).value = TimeOfDay.ToString("hh:mm")



        Try

            'Save changes to new file in CKJobs
            MyWRExcel.DisplayAlerts = False
            workbookWR.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception

            MsgBox(ex.Message)
            workbookWR.Close()
            MyWRExcel.Quit()
            releaseObject(workbookWR)
            releaseObject(MyWRExcel)
            DGVOutputData.Dispose()
            DGVNextJobsData.Dispose()
            DGVPackWeight.Dispose()


            Me.Close()
            Exit Sub
        End Try

        Try
            'Close template file but do not save updates to it

            workbookWR.Close(SaveChanges:=False)
            MyWRExcel.DisplayAlerts = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        DGVOutputData.Dispose()
        DGVNextJobsData.Dispose()
        DGVPackWeight.Dispose()




        'CLEAN UP
        MyWRExcel.Quit()

        releaseObject(workbookWR)
        releaseObject(MyWRExcel)
        MsgBox("Short Stock Work in Process Report " & savename & " Created")



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

    Private Sub frmProdStockWork_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnFullReport.Visible = True
    End Sub

    Private Sub btnShortReport_Click(sender As Object, e As EventArgs) Handles btnShortReport.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        lblmessage.Visible = True
        lblMessage.Text = "Please Wait Creating Sort and Colour" & vbCrLf & "    Work in Progress Report"
        processShortReport()
        lblmessage.Visible = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        frmJobEntry.Show()
        Me.Close()

    End Sub



    Private Sub btnFullReport_Click(sender As Object, e As EventArgs) Handles btnFullReport.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        lblmessage.Visible = True
        lblMessage.Text = "Please Wait Creating Full Work" & vbCrLf & "      in Progress Report"
        processReport()
        lblmessage.Visible = False
        Me.Cursor = System.Windows.Forms.Cursors.Default
        frmJobEntry.Show()
        Me.Close()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmJobEntry.Show()
        Me.Close()
    End Sub


End Class