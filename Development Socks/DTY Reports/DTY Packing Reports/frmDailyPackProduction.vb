Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering
Imports Microsoft.Office
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmDailyPackProduction

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

    Dim MyPRExcel As New Excel.Application
    Dim packDate As String

    Dim sp_nums As String
    Dim template As String
    Dim FileInName As String
    Dim prodName As String
    Dim prodNum As String
    Dim mcNum As String
    Dim doofNum As String
    Dim mergeNum As String
    Dim doffNum As String
    Dim prodWeight As String
    Dim lineCount As Integer = 0
    Dim reCheckCount As Integer = 0 'COUNT OF ReCHECK CONES



    Public Sub processReport()
            'Excel Items
            Dim savename As String


        template = (My.Settings.dirTemplate & "\" & "Daily Production Report Packing Template.xlsx").ToString

        If template = "" Then
                MsgBox("Please set template file location in Settings")
                Exit Sub
            End If

        Dim workbookPR As Excel.Workbook



        savename = (My.Settings.dirPackReports & "\" & "DayPackingReport" & "_" & MonthCalendar1.SelectionRange.Start.ToString("dd_MMM_yyyy") & ".xlsx").ToString

        ' Dim searchdate As String
        ' searchdate = "2017-07-21"
        'Dim searchdate As Date = Date.Today                  ' xxxxxxxxxxxxxxxxxxxxxx  needed for final version
        Dim searchdate As Date = MonthCalendar1.SelectionRange.Start.ToString("yyy-MM-dd")


        'GET LIST OF PRODUCTS TO BE PROCESSED AS OF NOW
        SQL.ExecQuery("SELECT DISTINCT PRNUM,PRODNAME,MERGENUM,DOFFNUM,MCNUM FROM JOBS WHERE PACKENDTM = '" & searchdate & "' and CONESTATE >= 8") 'OR  PACKENDTM = '" & searchdate & "' and CONESTATE = 8 ")

        jobcount = SQL.RecordCount


        'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
        If jobcount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVJobsData.DataSource = SQL.SQLDS.Tables(0)
            DGVJobsData.Rows(0).Selected = True

            'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
            DGVJobsData.Sort(DGVJobsData.Columns("PRODNAME"), ListSortDirection.Ascending)  'sorts On cone number

        Else
                MsgBox("No Jobs Found, Please select new date range")
            DGVJobsData.ClearSelection()
            Exit Sub
            End If




        workbookPR = MyPRExcel.Workbooks.Open(template)


        'SERIES OF COUNTS FROM DATABASE TO GET VALUES NEEDED FOR REPORT
        For count As Integer = 0 To jobcount - 1 'DGVSort.Rows.Count



            prodNum = DGVJobsData.Rows(count).Cells("PRNUM").Value.ToString
            prodName = DGVJobsData.Rows(count).Cells("PRODNAME").Value.ToString
            mcNum = DGVJobsData.Rows(count).Cells("MCNUM").Value.ToString
            mergeNum = DGVJobsData.Rows(count).Cells("MERGENUM").Value.ToString
            doffNum = DGVJobsData.Rows(count).Cells("DOFFNUM").Value.ToString


            'COUNT NUMBER OF CARTS
            SQL.ExecQuery("SELECT  DISTINCT PRNUM,PRODNAME,MERGENUM,DOFFNUM,CARTNUM  FROM jobs WHERE PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And  PACKENDTM = '" & searchdate & "' ")
            Dim totalcarts = SQL.RecordCount

            'COUNT NUMBER OF MISSING CONES
            SQL.ExecQuery("SELECT * FROM JOBS WHERE PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And MISSCONE > 0 ")
            Dim totalNC = SQL.RecordCount

            'COUNT NUMBER OF A CONES
            SQL.ExecQuery("SELECT * FROM JOBS WHERE PRNUM = '" & prodNum & "'  And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE >= 15 And FLT_S = 'False' ")
            Dim totalA = SQL.RecordCount

            'COUNT NUMBER OF  AS Cones
            SQL.ExecQuery("SELECT * FROM JOBS WHERE PRNUM = '" & prodNum & "'  And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 9 And FLT_S = 'True' And DEFCONE = 0 ")
            Dim totalAS = SQL.RecordCount

            'COUNT NUMBER OF BS CONES
            SQL.ExecQuery("SELECT * FROM JOBS WHERE PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 8 And FLT_S = 'True' Or  PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 14 And FLT_S = 'True'  ") ' OR PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 8 And FLT_S = 'True' And M50 > 0 OR PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 8 And FLT_S = 'True' And P50 > 0")
            Dim totalBS = SQL.RecordCount

            'COUNT NUMBER OF B CONES
            SQL.ExecQuery("SELECT * FROM JOBS WHERE PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 8 And FLT_S = 'False' And Defcone = 0 And Misscone = 0 Or PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 14 And FLT_S = 'False' And Defcone = 0 And Misscone = 0 ") 'And CONEBARLEY > 0 OR PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 8 And FLT_S = 'False' And M50 > 0 OR PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 8 And FLT_S = 'False' And P50 > 0")
            Dim totalB = SQL.RecordCount

            'COUNT NUMBER OF DEFECT CONES
            SQL.ExecQuery("SELECT * FROM JOBS WHERE PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 8 And FLT_S = 'False' And Defcone > 0 OR PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 14 And FLT_S = 'False' And DEFCONE > 0 ")
            Dim totalDF = SQL.RecordCount

            'COUNT NUMBER OF ReCHECK CONES
            SQL.ExecQuery("SELECT * FROM JOBS WHERE PRNUM = '" & prodNum & "' And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' And CONESTATE = 8 And FLT_S = 'True' ")
            Dim totalRC = 0 'SQL.RecordCount   xxxxxxxxxxxxxxxxxxxx ADD BACK IN WHEN WE KNOW WHAT TO DO






            'GET PRODUCT WEIGHT INFORMATION
            SQL.ExecQuery("SELECT * FROM Product WHERE PRNUM = '" & prodNum & "' ")

            'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
            If SQL.RecordCount > 0 Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                DGVProdData.DataSource = SQL.SQLDS.Tables(0)
                DGVProdData.Rows(0).Selected = True

                'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
                DGVProdData.Sort(DGVProdData.Columns("PRODNAME"), ListSortDirection.Ascending)  'sorts On cone number

            Else
                MsgBox("No Jobs Found, Please select new date range")
                DGVProdData.ClearSelection()
                Exit Sub
            End If


            prodWeight = DGVProdData.Rows(0).Cells("PRODWEIGHT").Value.ToString



            'GET MACHINE NAME
            SQL.ExecQuery("SELECT * FROM Jobs WHERE PRNUM = '" & prodNum & "'  And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' and CONESTATE = 15 OR PRNUM = '" & prodNum & "'  And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' and CONESTATE = 8 OR PRNUM = '" & prodNum & "'  And MCNUM = '" & mcNum & "' And MERGENUM = '" & mergeNum & "' and DOFFNUM = '" & doffNum & "' And PACKENDTM = '" & searchdate & "' and CONESTATE = 14 ")

            'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
            If SQL.RecordCount > 0 Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                DGVJobData.DataSource = SQL.SQLDS.Tables(0)
                DGVJobData.Rows(0).Selected = True

                'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
                DGVJobData.Sort(DGVJobData.Columns("PRODNAME"), ListSortDirection.Ascending)  'sorts On cone number

            Else
                MsgBox("No Jobs Found, Please select new date range")
                DGVJobData.ClearSelection()
                Exit Sub
            End If
            Dim mcName As String = DGVJobData.Rows(0).Cells("MCNAME").Value.ToString



            Dim totalMD = 0 'GRADE MD CONES
            Dim totalML = 0 'GRADE ML CONES
            Dim totalAD = 0 'GRADE AD CONES
            Dim totalAL = 0 'GRADE AL CONES

            lineCount = lineCount + 1

            MyPRExcel.Cells(count + 7, 1) = lineCount 'ROW INDEX
            MyPRExcel.Cells(count + 7, 2) = prodName 'PRODUCT NAME
            MyPRExcel.Cells(count + 7, 3) = mergeNum 'MERGE NUMBER
            MyPRExcel.Cells(count + 7, 4) = prodWeight 'PRODUCT WEIGHT
            MyPRExcel.Cells(count + 7, 5) = mcName 'MACHINE NAME
            MyPRExcel.Cells(count + 7, 6) = doffNum
            MyPRExcel.Cells(count + 7, 7) = totalcarts 'NUMBER OF CARTS
            ' Dim CheeseFull = fullCount + reCheckCount
            MyPRExcel.Cells(count + 7, 8) = totalA  'GRADE A CONES
            MyPRExcel.Cells(count + 7, 9) = totalMD  'GRADE MD CONES
            MyPRExcel.Cells(count + 7, 10) = totalML 'GRADE ML CONES
            MyPRExcel.Cells(count + 7, 11) = totalAD 'GRADE AD CONES
            MyPRExcel.Cells(count + 7, 12) = totalAL 'GRADE AL CONES
            MyPRExcel.Cells(count + 7, 13) = totalB 'GRADE B CONES
            MyPRExcel.Cells(count + 7, 14) = totalAS 'GRADE AS CONES
            MyPRExcel.Cells(count + 7, 15) = totalBS    'GRADE BS CONES
            MyPRExcel.Cells(count + 7, 16) = totalDF  'GRADE DEFECT CONES
            MyPRExcel.Cells(count + 7, 17) = totalRC 'ReCHECK CONES
            MyPRExcel.Cells(count + 7, 18) = totalNC 'NOCONE 





        Next


        'LINE NUMBER

        MyPRExcel.Cells(3, 17).value = Date.Today.ToString("dd-MM-yyy")
        'MyPRExcel.Cells(3, 12).value = TimeOfDay.ToString("hh:mm")



        Try

            'Save changes to new file in CKJobs
            MyPRExcel.DisplayAlerts = False
            workbookPR.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception
            MsgBox(ex.Message)
            workbookPR.Close()
            DGVJobsData.Dispose()
            DGVJobData.Dispose()
            DGVProdData.Dispose()
            MyPRExcel.Quit()
            frmPackReports.lblMessage.Text = Nothing
            releaseObject(workbookPR)
            releaseObject(MyPRExcel)
            frmPackReports.Show()
            Me.Close()
            Exit Sub
        End Try

            Try
            'Close template file but do not save updates to it

            workbookPR.Close(SaveChanges:=False)
            MyPRExcel.DisplayAlerts = True
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


        DGVJobsData.Dispose()
        DGVJobData.Dispose()
        DGVProdData.Dispose()




        'CLEAN UP
        MyPRExcel.Quit()

        releaseObject(workbookPR)
        releaseObject(MyPRExcel)
        frmPackReports.lblMessage.Text = Nothing
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Label2.Visible = False
        MsgBox("Daily Packing Report " & savename & " Created")
        frmPackReports.Show()
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

    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        'Routine to get date range
        Label5.Text = MonthCalendar1.SelectionRange.Start.ToString("dd/MMM/yyyy")


        'STRIPOUT / Characters from date so that they are not used in the file name

        packDate = Label5.Text.Replace("/", "")

        btnCreate.Enabled = True
    End Sub

    Private Sub frmDailyPackProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        If packDate = "" Then
            MsgBox("Please select valid Date")

        Else
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            Label2.Visible = True
            Label2.Text = "Please wait Creating Daily Production Report"
            processReport()
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End If
    End Sub

End Class
