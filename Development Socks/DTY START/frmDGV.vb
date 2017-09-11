
Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering



Public Class frmDGV

    Private SQL As New SQLConn



    Private Sub datview()


        'Dim dt As New DataTable()
        'dt.Columns.Add("M/C #", Type.GetType("System.String"))
        ' dt.Columns.Add("M/C Name", Type.GetType("System.String"))
        ' dt.Columns.Add("Prod Code", Type.GetType("System.String"))
        '  dt.Columns.Add("Prod Name", Type.GetType("System.String"))


        ' For i As Integer = 0 To 33
        ' dt.Rows.Add()
        ' dt.Rows(dt.Rows.Count - 1)("M/C #") = frmCart1.jobArray(i, 1)
        ' dt.Rows(dt.Rows.Count - 1)("M/C Name") = frmCart1.jobArray(i, 1)
        ' dt.Rows(dt.Rows.Count - 1)("Prod Code") = frmCart1.jobArray(i, 1)
        '  dt.Rows(dt.Rows.Count - 1)("Prod Name") = frmCart1.jobArray(i, 1)
        '  Next

        'GridMultiD.DataSource = dt
        'GridMultiiD.DataBind()




    End Sub



    Private Sub LoadGrid(Optional Query As String = "")   'USED TO CALL IN DATA FROM DATABASE



        frmJobEntry.LExecQuery("SELECT * FROM jobs WHERE bcodecart = '" & frmJobEntry.dbBarcode & "'")



        If frmJobEntry.LRecordCount > 0 Then

            DGVdata.DataSource = frmJobEntry.LDS.Tables(0)
        End If


        'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
        DGVdata.Sort(DGVdata.Columns(5), ListSortDirection.Ascending)  'sorts On cone number

    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        frmJobEntry.Show()
        Me.Hide()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        frmJobEntry.LDA.Update(frmJobEntry.LDS)
        'REFRESH DATAGRID
        LoadGrid()
    End Sub

    Private Sub FindJob()
        Sql.AddParam("@Job", "%" & txtSearch.Text & "%")
        LoadGrid("Select BCODECART FROM JOBS WHERE BCODECART LIKE @Job;")
    End Sub


    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        FindJob()
    End Sub

    Private Sub LoadCBX()
        ' Refresh Combobox
        cbxItems.Items.Clear()

        'Run Query
        Sql.ExecQuery("SELECT CONEBCODE FROM JOBS ORDER BY CONEBARDODE ASC;")

        If Sql.HasException(True) Then Exit Sub
        'LOOP ROW & ADD TO COMBOBOX
        For Each r As DataRow In Sql.SQLDT.Rows
            cbxItems.Items.Add(r("CONEBARCODE").ToString)
        Next

    End Sub

    Public Sub tsbtnSave()

        Dim bAddState As Boolean = DGVdata.AllowUserToAddRows
            Dim iRow As Integer = DGVdata.CurrentRow.Index
            DGVdata.AllowUserToAddRows = True
            DGVdata.CurrentCell = DGVdata.Rows(DGVdata.Rows.Count - 1).Cells(0) ' move to add row
            DGVdata.CurrentCell = DGVdata.Rows(iRow).Cells(0) ' move back to current row
            DGVdata.AllowUserToAddRows = bAddState


    End Sub

    Private Sub frmDGV_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


End Class