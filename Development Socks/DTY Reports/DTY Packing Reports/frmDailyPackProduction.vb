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
    Dim mcName As String
    Dim doofNum As String
    Dim mergeNum As String
    Dim doffNum As String
    Dim prodWeight As String
    Dim lineCount As Integer = 0
    Dim reCheckCount As Integer = 0 'COUNT OF ReCHECK CONES
    Dim bcodejob As String = Nothing
    'Dim startDate As Date


    'TOTAL Column results

    Dim tot_carts, A_Master, ReA_Master, AD_Master, AL_MAster, B_Master, AS_Master, BS_Master, DEF_MAster, ReC_Master, NoCone_Master, GT_Master As Integer

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
        frmJobEntry.Show()
    End Sub

    Private Sub frmDailyPackProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.debugSet Then
            DGVJobData.Show()
            DGVJobsData.Show()
            DGVProdData.Show()
        End If
    End Sub

    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError



    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        'Routine to get date range
        Label5.Text = MonthCalendar1.SelectionRange.Start.ToString("dd/MMM/yyyy")
        packDate = MonthCalendar1.SelectionRange.Start.ToString("dd-MM-yyyy")



        btnCreate.Enabled = True
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Label2.Visible = True
        Label2.Text = "Please wait Creating Packing Report for " & Label5.Text
        processReport()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        frmJobEntry.Show()
        Me.Close()

    End Sub

    Public Sub processReport()
        'Excel Items
        Dim savename As String


        template = (My.Settings.dirTemplate & "\" & "Daily Production Report Packing Template.xlsx").ToString

        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Exit Sub
        End If

        Dim workbookPR As Excel.Workbook
        Dim worksheetPR As Excel.Worksheet
        Dim chartRange As Excel.Range

        savename = (My.Settings.dirPackReports & "\" & "DayPackingReport" & "_" & MonthCalendar1.SelectionRange.Start.ToString("dd_MMM_yyyy") & ".xlsx").ToString


        Dim searchdate As Date = MonthCalendar1.SelectionRange.Start.ToString("yyyy-MM-dd ")
        Dim startTm As String = searchdate.AddDays(-1) & " 15:00:00.000"
        Dim endTm As String = searchdate & " 14:59:59.999"


        'GET LIST OF PRODUCTS TO BE PROCESSED AS OF NOW
        SQL.ExecQuery("SELECT DISTINCT a.PRNUM, a.PRODNAME, a.MERGENUM, a.DOFFNUM, a.MCNUM, a.bcodejob, a.mcname, b.PRODWEIGHT FROM JOBS a INNER JOIN product b on a.prnum = b.prnum WHERE PACKENDTM Between '" & startTm & "' and '" & endTm & "' Order by a.PRODNAME ")


        jobcount = SQL.RecordCount


        'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS
        If jobcount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVJobsData.DataSource = SQL.SQLDS.Tables(0)

        Else
            MsgBox("No Jobs Found, Please select new date range")
            DGVJobsData.ClearSelection()
            Exit Sub
        End If



        workbookPR = MyPRExcel.Workbooks.Open(template)
        worksheetPR = workbookPR.Sheets("DAILY REPORT 401")

        'SERIES OF COUNTS FROM DATABASE TO GET VALUES NEEDED FOR REPORT
        For count As Integer = 0 To jobcount - 1 'DGVSort.Rows.Count


            Dim tmpprodnum As Integer = DGVJobsData.Rows(count).Cells("PRNUM").Value
            prodNum = tmpprodnum.ToString("000")

            prodName = DGVJobsData.Rows(count).Cells("PRODNAME").Value.ToString
            mcNum = DGVJobsData.Rows(count).Cells("MCNUM").Value.ToString
            mcName = DGVJobsData.Rows(count).Cells("MCNAME").Value.ToString
            mergeNum = DGVJobsData.Rows(count).Cells("MERGENUM").Value.ToString
            doffNum = DGVJobsData.Rows(count).Cells("DOFFNUM").Value.ToString
            bcodejob = DGVJobsData.Rows(count).Cells("BCODEJOB").Value.ToString


            If Not IsDBNull(DGVJobsData.Rows(count).Cells("BCODEJOB").Value.ToString) Then
                If DGVJobsData.Rows(count).Cells("PRODWEIGHT").Value > "0" Then
                    prodWeight = DGVJobsData.Rows(count).Cells("PRODWEIGHT").Value.ToString
                Else
                    MsgBox("Cannont complete report " & vbCrLf & "no weight information for Product Number " & prodNum & vbCrLf & "Product Name " & prodName)

                    DGVProdData.ClearSelection()
                    Exit Sub
                End If
            Else
                MsgBox("Cannont complete report " & vbCrLf & "no weight information for Product Number " & prodNum & vbCrLf & "Product Name " & prodName)
                DGVProdData.ClearSelection()
                Exit Sub

            End If

            'COUNT NUMBER OF CARTS
            SQL.AddParam("@bcodejob", bcodejob)
            SQL.ExecQuery("SELECT  DISTINCT PRNUM,PRODNAME,MERGENUM,DOFFNUM,CARTNUM  FROM jobs WHERE bcodejob = @bcodejob And  PACKENDTM Between '" & startTm & "' and '" & endTm & "' ")
            Dim totalcarts = SQL.RecordCount





            'COUNT NUMBER OF A CONES
            SQL.AddParam("@bcodejob", bcodejob)
            SQL.ExecQuery("SELECT * FROM JOBS WHERE bcodejob = @bcodejob  " _
                          & " And PACKENDTM Between '" & startTm & "' and '" & endTm & "' And CONESTATE = 15 And FLT_S = 'False' and (RECHK = 0 or RECHK is Null) " _
                          & "And DEFCONE = 0 And CONEBARLEY = 0  And M30 = 0 And P30 = 0 ")


            Dim totalA = SQL.RecordCount



            'COUNT NUMBER OF ReCheck A CONES
            SQL.AddParam("@bcodejob", bcodejob)
            SQL.ExecQuery("SELECT * FROM JOBS WHERE bcodejob = @bcodejob  " _
                          & " And PACKENDTM Between '" & startTm & "' and '" & endTm & "' And CONESTATE = 15 And FLT_S = 'False' And RECHKRESULT = 'A' And DEFCONE = 0 And CONEBARLEY = 0 ")


            Dim totalReA = SQL.RecordCount  'add recheck A to the A count



            'COUNT NUMBER OF  AS Cones
            SQL.AddParam("@bcodejob", bcodejob)
            SQL.ExecQuery("SELECT * FROM JOBS WHERE bcodejob = @bcodejob  " _
                          & " And PACKENDTM Between '" & startTm & "' and '" & endTm & "' And CONESTATE = 9 And FLT_S = 'True'  " _
                          & "And DEFCONE = 0 And CONEBARLEY = 0  And M30 = 0 And P30 = 0 ")

            Dim totalAS = SQL.RecordCount



            'COUNT NUMBER OF B CONES
            SQL.AddParam("@bcodejob", bcodejob)
            ' SQL.AddParam("@bcodejob2", bcodejob)
            SQL.ExecQuery("SELECT * FROM JOBS WHERE bcodejob = @bcodejob " _
                          & " And PACKENDTM Between '" & startTm & "' and '" & endTm & "' And CONESTATE = 8 And FLT_S = 'False' " _
                          & " And (Defcone > 0 Or CONEBARLEY > 0 OR M30 > 0 OR P30 > 0) ")

            Dim totalB = SQL.RecordCount



            'COUNT NUMBER OF BS CONES
            SQL.AddParam("@bcodejob", bcodejob)
            ' SQL.AddParam("@bcodejob2", bcodejob)
            SQL.ExecQuery("SELECT * FROM JOBS WHERE bcodejob = @bcodejob " _
                          & " And PACKENDTM Between '" & startTm & "' and '" & endTm & "' And CONESTATE = '8' And FLT_S = 'True' " _
                          & " And (Defcone > 0 Or CONEBARLEY > 0 Or M30 = 0 Or P30 = 0) ")

            Dim totalBS = SQL.RecordCount





            'COUNT NUMBER OR AL CONES
            SQL.AddParam("@bcodejob", bcodejob)
            SQL.ExecQuery("SELECT * FROM JOBS WHERE bcodejob = @bcodejob " _
                          & " And PACKENDTM  Between '" & startTm & "' and '" & endTm & "' And (CONESTATE = 8 or conestate = 15) And FLT_S = 'False'  and RECHKRESULT = 'AL' " _
                          & "And Defcone = 0 And CONEBARLEY = 0   ")


            Dim totalAL = SQL.RecordCount.ToString



            'COUNT NUMBER OF AD CONES
            SQL.AddParam("@bcodejob", bcodejob)
            SQL.ExecQuery("SELECT * FROM JOBS WHERE bcodejob = @bcodejob " _
                          & " And PACKENDTM  Between '" & startTm & "' and '" & endTm & "' And (CONESTATE = 8 or CONESTATE = 15) And FLT_S = 'False'  And RECHKRESULT = 'AD' " _
                          & "And Defcone = 0 And CONEBARLEY = 0 ")
            Dim totalAD = SQL.RecordCount.ToString





            Dim totalMD = 0 'GRADE MD CONES
            Dim totalML = 0 'GRADE ML CONES


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
            MyPRExcel.Cells(count + 7, 9) = totalReA  'GRADE ReCheck A's CONES
            MyPRExcel.Cells(count + 7, 10) = totalMD  'GRADE MD CONES
            MyPRExcel.Cells(count + 7, 11) = totalML 'GRADE ML CONES
            MyPRExcel.Cells(count + 7, 12) = totalAD 'GRADE AD CONES
            MyPRExcel.Cells(count + 7, 13) = totalAL 'GRADE AL CONES
            MyPRExcel.Cells(count + 7, 14) = totalB 'GRADE B CONES
            MyPRExcel.Cells(count + 7, 15) = totalAS 'GRADE AS CONES
            MyPRExcel.Cells(count + 7, 16) = totalBS    'GRADE BS CONES
            'MyPRExcel.Cells(count + 7, 16) = totalDF  'GRADE DEFECT CONES
            ' MyPRExcel.Cells(count + 7, 17) = totalRC 'ReCHECK CONES
            'MyPRExcel.Cells(count + 7, 18) = totalNC 'NOCONE 

            tot_carts = tot_carts + totalcarts
            A_Master = A_Master + totalA
            ReA_Master = ReA_Master + totalReA
            AD_Master = AD_Master + totalAD
            AL_MAster = AL_MAster + totalAL
            B_Master = B_Master + totalB
            AS_Master = AS_Master + totalAS
            BS_Master = BS_Master + totalBS
            ' DEF_MAster = DEF_MAster + totalDF
            ' ReC_Master = ReC_Master + totalRC
            ' NoCone_Master = NoCone_Master + totalNC




        Next




        GT_Master = A_Master + ReA_Master + AD_Master + AL_MAster + B_Master + AS_Master + BS_Master

        'fILL IN cOLUMN TOTALS

        'GET LINE NUMBER FOR TOTALS
        Dim total_line = jobcount + 10

        'Merge Cells for Total
        chartRange = worksheetPR.Range("A" & total_line, "F" & total_line)
        chartRange.Merge()

        'clear borders around unwanted cells
        chartRange = worksheetPR.Range("A" & total_line + 1, "S200")
        chartRange.Borders.LineStyle = Excel.XlLineStyle.xlLineStyleNone

        'Clear all unused Sum cells
        chartRange = worksheetPR.Range("S" & total_line + 1, "S200")
        chartRange.Value = " "

        'Set Border around Total cells
        chartRange = worksheetPR.Range("A" & total_line, "S" & total_line)
        chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic)
        'Set same range to bold
        chartRange.Font.Bold = True


        'Clear Zeros from 3 cells above totals

        For i = 0 To 2
            MyPRExcel.Cells((jobcount + 7) + i, 19).value = " "
        Next

        MyPRExcel.Cells(total_line, 1).value = "Totals"


        MyPRExcel.Cells(total_line, 7).value = tot_carts
        MyPRExcel.Cells(total_line, 8).value = A_Master
        MyPRExcel.Cells(total_line, 9).value = ReA_Master
        MyPRExcel.Cells(total_line, 10).value = "0"
        MyPRExcel.Cells(total_line, 11).value = "0"
        MyPRExcel.Cells(total_line, 12).value = AD_Master
        MyPRExcel.Cells(total_line, 13).value = AL_MAster
        MyPRExcel.Cells(total_line, 14).value = B_Master
        MyPRExcel.Cells(total_line, 15).value = AS_Master
        MyPRExcel.Cells(total_line, 16).value = BS_Master
        '  MyPRExcel.Cells(total_line, 17).value = DEF_MAster
        ' MyPRExcel.Cells(total_line, 18).value = ReC_Master
        '  MyPRExcel.Cells(total_line, 19).value = NoCone_Master



        MyPRExcel.Cells(3, 17).value = packDate




        Try

            'Save changes to new file in CKJobs
            MyPRExcel.DisplayAlerts = False
            workbookPR.SaveAs(Filename:=savename, FileFormat:=51)

        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Save Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Save Error", ex.ToString, False, "System Fault")

            MsgBox(ex.Message)
            workbookPR.Close()
            DGVJobsData.Dispose()
            DGVJobData.Dispose()
            DGVProdData.Dispose()
            MyPRExcel.Quit()
            releaseObject(chartRange)
            releaseObject(worksheetPR)
            releaseObject(workbookPR)
            releaseObject(MyPRExcel)
            frmJobEntry.Show()
            Me.Close()
            Exit Sub
        End Try

        Try
            'Close template file but do not save updates to it

            workbookPR.Close(SaveChanges:=False)
            MyPRExcel.DisplayAlerts = True
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Close Error", ex.Message, False, "System Fault")
            MsgBox(ex.Message)

        End Try




        DGVJobsData.Dispose()
        DGVJobData.Dispose()
        DGVProdData.Dispose()

        'Write total time


        'CLEAN UP
        MyPRExcel.Quit()
        releaseObject(chartRange)
        releaseObject(worksheetPR)
        releaseObject(workbookPR)
        releaseObject(MyPRExcel)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Label2.Visible = False
        MsgBox("Daily Packing Report " & savename & " Created")
        frmJobEntry.Show()
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
