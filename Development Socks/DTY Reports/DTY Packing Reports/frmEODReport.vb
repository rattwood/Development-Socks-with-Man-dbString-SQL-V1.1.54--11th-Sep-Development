Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports System.ComponentModel

Public Class frmEODReport

    Private SQL As New SQLConn

    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Public LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private LCmd As SqlCommand

    'SQL CONNECTORS
    Public LDA As SqlDataAdapter
    Public LDS As DataSet
    Public LDT As DataTable
    Public LCB As SqlCommandBuilder

    Public LRecordCount As Integer
    Private LException As String




    Dim chkBCode As String
    Dim cartNum As String

    Dim todaypath As String
    Dim finPath As String
    Dim yesterdayPath As String
    Dim myCount As Integer = 0

    Dim MyExcel As New Excel.Application

    Private Sub EODReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True  'Allows us to look for advace character from barcode
        btnEOD.Enabled = False
    End Sub


    Private Sub chkBarcode()


        'Routine to check Barcode is TRUE
        Try

            chkBCode = txtLotNumber.Text.Substring(12, 1)

            If chkBCode = "B" Then
                If txtLotNumber.TextLength > 14 Then  ' For carts B10,11 & 12
                    cartNum = txtLotNumber.Text.Substring(12, 3)
                Else
                    cartNum = txtLotNumber.Text.Substring(12, 2)
                End If

            Else
                MsgBox("This is not a CART Barcode Please RE Scan")
                Me.txtLotNumber.Clear()

                Me.txtLotNumber.Focus()
                Me.txtLotNumber.Refresh()
                Exit Sub
            End If

        Catch ex As Exception
            MsgBox("BarCcode Is Not Valid")
            Me.txtLotNumber.Clear()
            Me.txtLotNumber.Focus()
            Me.txtLotNumber.Refresh()
            Exit Sub
        End Try

        btnEOD.Enabled = True


    End Sub


    Private Sub btnCancelReport_Click(sender As Object, e As EventArgs) Handles btnCancelReport.Click
        canceljob()
    End Sub

    Private Sub canceljob()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Close()
    End Sub

    Private Sub btnEOD_Click(sender As Object, e As EventArgs) Handles btnEOD.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        moveReport()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub moveReport()


        Dim workbook As Excel.Workbook
        Dim sheets As Excel.Worksheet
        Dim prodNameMod As String

        todaypath = (My.Settings.dirPacking & "\" & Date.Now.ToString("dd_MM_yyyy"))
        finPath = (My.Settings.dirPackReports & "\" & Date.Now.ToString("dd_MM_yyyy"))



        frmJobEntry.LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & txtLotNumber.Text & "' ")

        If frmJobEntry.LRecordCount > 0 Then

            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = frmJobEntry.LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True



            'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
            frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(6), ListSortDirection.Ascending)  'sorts On cone number

        End If

        prodNameMod = frmDGV.DGVdata.Rows(1).Cells("PRODNAME").Value
        prodNameMod = prodNameMod.Replace("/", "_")

        'Create the Report name

        Dim savestring As String
        savestring = (prodNameMod & " " _
            & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value.ToString & "_" _
            & frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value.ToString)


        Dim sheetName = prodNameMod.Substring(prodNameMod.Length - 4)

        Dim fileName = (todaypath & "\" & savestring & ".xlsx").ToString
        Dim tmpsaveName As String



        ' routine to check if a today directory exists otherwise creat a new one



        'MsgBox("Today = " & Date.Now.ToString("dd_MM_yyyy") & "  Yesterday = " & Date.Now.AddDays(-1).ToString("dd_MM_yyyy"))

        'Check to make sure template exists



        'Call IsFileOpen(New FileInfo(fileName))






        Try

            If File.Exists(fileName) Then


                workbook = MyExcel.Workbooks.Open(fileName)
                myCount = workbook.Worksheets.Count


                tmpsaveName = (finPath & "\" & sheetName & "_" & myCount & "_EOD.xlsx").ToString

                MyExcel.DisplayAlerts = False
                workbook.Sheets(myCount).SaveAs(Filename:=tmpsaveName, FileFormat:=51)

                MyExcel.DisplayAlerts = True

            Else
                MsgBox("No jobs for Today")
                txtLotNumber.Clear()
                txtLotNumber.Focus()
                btnEOD.Enabled = False
                Exit Sub
            End If
        Catch ex As Exception

            MsgBox(ex.ToString)


        End Try

        Try
            'Close template file but do not save updates to it

            workbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        workbook = Nothing
        sheets = Nothing
        MyExcel.Quit()

        If File.Exists(tmpsaveName) Then
            MsgBox("File " & sheetName & "_" & myCount & "_EOD.xlsx" & "  Has been created")
        Else
            MsgBox("Failed to create file")
        End If

        txtLotNumber.Clear()
        txtLotNumber.Focus()
        btnEOD.Enabled = False


    End Sub

    Private Sub todayDir()
        ' routine to check if a today directory exists otherwise creat a new one

        todaypath = (My.Settings.dirPacking & "\" & Date.Now.ToString("dd_MM_yyyy"))
        yesterdayPath = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-1).ToString("dd_MM_yyyy"))
        finPath = (My.Settings.dirPackReports & "\" & Date.Now.ToString("dd_MM_yyyy"))



        If Not Directory.Exists(todaypath) Then
            MsgBox("Directory for Today does not exist")
            canceljob()

        End If

        If Not Directory.Exists(finPath) Then
            MsgBox("Directory for Todays Finished Jobs does not exist")
            canceljob()
        End If





    End Sub


    Private Sub EODReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            chkBarcode()


        End If

    End Sub


    Private Sub IsFileOpen(ByVal file As FileInfo)
        Dim stream As FileStream = Nothing
        Try
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            stream.Close()

        Catch ex As Exception

            ' do something here, either close the file if you have a handle, show a msgbox, retry  or as a last resort terminate the process - which could cause corruption and lose data

            MsgBox("Excel File is open please close and then Retry")
            MyExcel.DisplayAlerts = False
            MyExcel.Quit()
            MsgBox("Excel file is open please close and retry")
            canceljob()

        End Try
    End Sub


End Class