'Imports System.Data.DataTable

Imports System.Data.SqlClient

Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering





Public Class liveGraph







    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------

    Public GConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings

    Private GCmd As SqlCommand



    'SQL CONNECTORS

    Public GDA As SqlDataAdapter

    Public GDS As DataSet

    Public GDT As DataTable

    Public GCB As SqlCommandBuilder

    Public GDR As SqlDataReader



    Public GRecordCount As Integer

    Private GException As String

    ' SQL QUERY PARAMETERS

    Public LParams As New List(Of SqlParameter)





    'GRADES IN SORTING

    Private GradeAsort As Integer

    Private GradeBsort As Integer

    Private GradeASsort As Integer

    Private GradeBSsort As Integer

    Private GradePilotsort As Integer

    Private GradeStdsort As Integer

    Private ReChecksort As Integer







    'GRADES in COLOUR VARIABLES FOR GRAPH

    Private GradeAcol As Integer

    Private GradeBcol As Integer

    Private GradeAScol As Integer

    Private GradeBScol As Integer

    Private GradePilotcol As Integer

    Private GradeStdcol As Integer

    Private ReCheckcol As Integer





    'GRADES in Packing VARIABLES FOR GRAPH

    Private GradeApak As Integer

    Private GradeBpak As Integer

    Private GradeASpak As Integer

    Private GradeBSpak As Integer

    Private GradePilotpak As Integer

    Private GradeStdpak As Integer

    Private ReCheckpak As Integer







    Private startDate

    Private endDate

    Dim fullCount As Integer = 0

    Dim reCheckCount As Integer 'COUNT OF ReCHECK CONES

    Dim shortCone As Integer = 0

    Dim prname

    Dim prodnum

    Dim jobcount

    Private sortCount

    Private colCount

    '







    Private Sub liveGraph_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        colLive.Series.Add("Full")

        colLive.Series.Add("Short")

        colLive.Series.Add("ReCheck")





        'colLive.Series.Add("Grade BS")

        'colLive.Series.Add("Grade ReCheck")

        'colLive.Series.Add("Grade Pilot")

        'colLive.Series.Add("Grade Std")

        'colLive.Series.Add("Missing")



        'create rows 

        DGVReportOutput.Rows.Add(100)  'Creat same number of rows as there are jobs found

        MonthCalendar1.Visible = True

        'For i = 1 To jobcount

        '    DGVReportOutput.Rows(i - 1).Cells(0).Value = x

        '    x = x + 1

        'Next







    End Sub



    Private Sub MonthCalendar1_DateChanged(sender As Object, e As DateRangeEventArgs) Handles MonthCalendar1.DateChanged

        'Routine to get date range

        lblStartDate.Text = MonthCalendar1.SelectionRange.Start.ToString("dd/MMM/yyyy")

        lblEndDate.Text = MonthCalendar1.SelectionRange.End.ToString("dd/MMM/yyyy")



        'STRIPOUT / Characters from date so that they are not used in the file name



        startDate = lblStartDate.Text.Replace("/", "")

        endDate = lblEndDate.Text.Replace("/", "")



        btnUpdateGraph.Enabled = True



    End Sub







    Private Sub GExecQuery(Query As String)

        ' RESET QUERY STATISTCIS

        GRecordCount = 0

        GException = ""





        If GConn.State = ConnectionState.Open Then GConn.Close()

        Try



            'OPEN SQL DATABSE CONNECTION

            GConn.Open()



            'CREATE SQL COMMAND

            GCmd = New SqlCommand(Query, GConn)



            'LOAD PARAMETER INTO SQL COMMAND

            LParams.ForEach(Sub(p) GCmd.Parameters.Add(p))



            'CLEAR PARAMETER LIST

            LParams.Clear()



            'EXECUTE COMMAND AND FILL DATASET

            GDS = New DataSet

            GDT = New DataTable

            GDA = New SqlDataAdapter(GCmd)

            GRecordCount = GDA.Fill(GDS)



        Catch ex As Exception



            GException = "ExecQuery Error: " & vbNewLine & ex.Message

            MsgBox(GException)



        End Try



    End Sub



    Public Sub getData()





        Dim tblOpen As Integer = 0

        Dim prodname As String

        Dim x = 1

        'GET LIST OF PRODUCTS TO BE PROCESSED AS OF NOW

        GExecQuery("SELECT DISTINCT PRNUM,PRODNAME,MERGENUM FROM JOBS WHERE SORTENDTM Between '" & startDate & "' And '" & endDate & "'  ")



        jobcount = GRecordCount







        'IF JOBS HAVE BEEN FOUND THEN CREATE A SORTED LIST OF THESE JOBS

        If jobcount > 0 Then

            'LOAD THE DATA FROM dB IN TO THE DATAGRID

            DGVReportJobs.DataSource = GDS.Tables(0)

            DGVReportJobs.Rows(0).Selected = True



            'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE

            DGVReportJobs.Sort(DGVReportJobs.Columns("PRNUM"), ListSortDirection.Ascending)  'sorts On cone number

            'create rows 

            'DGVReportOutput.Rows.Add(jobcount)  'Creat same number of rows as there are jobs found



            'For i = 1 To jobcount

            '    DGVReportOutput.Rows(i - 1).Cells(0).Value = x

            '    x = x + 1

            'Next

        Else

            MsgBox("No Jobs Found, Please select new date range")

            DGVReportJobs.ClearSelection()

            Exit Sub

        End If











        'SERIES OF COUNTS FROM DATABASE TO GET VALUES NEEDED FOR REPORT

        For count As Integer = 1 To jobcount 'DGVSort.Rows.Count

            prodnum = DGVReportJobs.Rows(count - 1).Cells("PRNUM").Value









            'COUNT NUMBER OF CONES THAT ARE FULL INCLUDING WASTE OR COLOUR WASTE CHEESE

            GExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & startDate & "' And '" & endDate & "' And  PRNUM = '" & prodnum & "' And FLT_S = 'False' and SHORTCONE = 0 And FLT_W = 'False' And COLWASTE = 0 And  (RECHK = 0 OR RECHK Is Null) AND MISSCONE = 0 ")

            fullCount = GRecordCount





            If fullCount > 0 Then

                DGVReportInput.DataSource = GDS.Tables(0)

                DGVReportInput.Rows(0).Selected = True

                tblOpen = 1

            End If





            '******************************  SEARCHES FOR CHEESE IN COLOUR SECTION **************************************************

            'COUNT NUMBER OF CONE THAT ARE SHORT

            GExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & startDate & "' And '" & endDate & "' And PRNUM = '" & prodnum & "' And FLT_S = 'TRUE' And FLT_W = 'False' And COLWASTE = 0 ")

            Dim shortCone = GRecordCount



            If shortCone > 0 And tblOpen = 0 Then

                DGVReportInput.DataSource = GDS.Tables(0)

                DGVReportInput.Rows(0).Selected = True

                tblOpen = 1

            End If







            'COUNT ReCheck

            GExecQuery("SELECT * FROM jobs WHERE SORTENDTM Between '" & startDate & "' And '" & endDate & "' And PRNUM = '" & prodnum & "' And  RECHK Between 2 and 4 ")

            reCheckCount = GRecordCount



            If reCheckCount > 0 And tblOpen = 0 Then

                DGVReportInput.DataSource = GDS.Tables(0)

                DGVReportInput.Rows(0).Selected = True

                tblOpen = 1

            End If









            'Dim mergenum = DGVReportInput.Rows(0).Cells("MERGENUM").Value.ToString



            prname = DGVReportInput.Rows(count - 1).Cells("PRODNAME").Value









            DGVReportOutput.Rows(count - 1).Cells(0).Value = PRNUM

            DGVReportOutput.Rows(count - 1).Cells(1).Value = prname

            DGVReportOutput.Rows(count - 1).Cells(2).Value = fullCount

            DGVReportOutput.Rows(count - 1).Cells(3).Value = reCheckCount

            DGVReportOutput.Rows(count - 1).Cells(4).Value = shortCone







        Next



        Dim fullGraph

        Dim ReCheckGraph

        Dim ShortGraph





        For rw = 1 To jobcount

            prodnum = DGVReportOutput.Rows(rw - 1).Cells(0).Value

            PRODNAME = DGVReportOutput.Rows(rw - 1).Cells(1).Value

            fullGraph = DGVReportOutput.Rows(rw - 1).Cells(2).Value

            ReCheckGraph = DGVReportOutput.Rows(rw - 1).Cells(3).Value

            shortgraph = DGVReportOutput.Rows(rw - 1).Cells(4).Value

            colLive.Series("Full").IsValueShownAsLabel = True

            colLive.Series("Full").Label = "#VALY"

            colLive.Series("ReCheck").IsValueShownAsLabel = True

            colLive.Series("ReCheck").Label = "#VALY"

            colLive.Series("Short").IsValueShownAsLabel = True

            colLive.Series("Short").Label = "#VALY"





            colLive.Series("Full").Points.AddXY(prodname.ToString, fullGraph.ToString)

            colLive.Series("ReCheck").Points.AddXY(prodname.ToString, ReCheckGraph.ToString)

            colLive.Series("Short").Points.AddXY(prodname.ToString, ShortGraph.ToString)

            'colLive.Series("Grade BS").Points.AddXY(prodname.ToString, GradeB.ToString)

            'colLive.Series("Grade ReCheck").Points.AddXY(prodname.ToString, GradeA.ToString)

            'colLive.Series("Missing").Points.AddXY(prodname.ToString, GradeB.ToString)

        Next



        'COUNT PRODUCTIVITY

        'COUNT ALL CHEESE IN DATE RAGE WHERE CONESTATE IS BETWEEN 8 and 9, AND NO PACKENDTM

        'COUNT ALL CHEESE IN DATE RAGE WHERE CONESTATE IS 5, AND NO PACKENDTM



        'COUNT SORT CHEESE RECIEVED FOR DAY

        If My.Settings.debugSet Then
            GExecQuery("SELECT * FROM jobs WHERE CARTSTARTTM Between '" & startDate & "' And '" & endDate & "' AND SORTENDTM Between '" & startDate & "' And '" & endDate & "' and MISSCONE = 0 ")

            sortCount = GRecordCount



            'COUNT COLOUR CHECKED CHEESE FOR DAY

            GExecQuery("SELECT * FROM jobs WHERE CARTSTARTTM Between '" & startDate & "' And '" & endDate & "' AND COLENDTM Between '" & startDate & "' And '" & endDate & "' and MISSCONE = 0 ")

            colCount = GRecordCount

        Else
            GExecQuery("SELECT * FROM jobs WHERE CARTSTARTTM Between '" & startDate & "' And '" & endDate & "' and MISSCONE = 0 ")

            sortCount = GRecordCount



            'COUNT COLOUR CHECKED CHEESE FOR DAY

            GExecQuery("SELECT * FROM jobs WHERE COLENDTM Between '" & startDate & "' And '" & endDate & "' and MISSCONE = 0 ")

            colCount = GRecordCount

        End If


        lblTotSort.Text = sortCount

        lblTotChecked.Text = colCount





        lblEffVal.Text = (colCount / sortCount) * 100



        DGVReportJobs.ClearSelection()

        DGVReportInput.ClearSelection()

        DGVReportOutput.ClearSelection()

        DGVReportInput.Dispose()

        DGVReportJobs.Dispose()

        DGVReportOutput.Dispose()

        btnUpdateGraph.Enabled = False



    End Sub



    Private Sub updateGraph()

        Dim rw As Integer

        Dim prodnum

        Dim prodname







        For rw = 1 To jobcount

            prodnum = DGVReportOutput.Rows(rw - 1).Cells(0).Value

            prodname = DGVReportOutput.Rows(rw).Cells(1).Value

            Full = DGVReportOutput.Rows(rw - 1).Cells(2).Value

            reCheckCount = DGVReportOutput.Rows(rw - 1).Cells(3).Value

            shortCone = DGVReportOutput.Rows(rw - 1).Cells(4).Value





            colLive.Series("Full").Points.AddXY(prodname.ToString, Full.ToString)

            colLive.Series("ReCheck").Points.AddXY(prodname.ToString, reCheckCount.ToString)

            colLive.Series("Short").Points.AddXY(prodname.ToString, shortCone.ToString)

            'colLive.Series("Grade BS").Points.AddXY(prodname.ToString, GradeB.ToString)

            'colLive.Series("Grade ReCheck").Points.AddXY(prodname.ToString, GradeA.ToString)

            'colLive.Series("Missing").Points.AddXY(prodname.ToString, GradeB.ToString)

        Next

        MonthCalendar1.Visible = False

    End Sub





    Private Sub btnUpdateGraph_Click(sender As Object, e As EventArgs) Handles btnUpdateGraph.Click

        getData()

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Close()

        frmJobEntry.Show()

    End Sub

End Class