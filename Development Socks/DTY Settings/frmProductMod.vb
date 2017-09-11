Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering


Public Class frmProductMod
    Private SQL As New SQLConn



    Private Sub frmProductMod_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SQL.ExecQuery("SELECT * FROM product")

        LoadGrid()



    End Sub

    Private Sub LoadGrid()
        SQL.ExecQuery("SELECT * FROM product")
        If SQL.RecordCount > 0 Then


            DGVProduct.DataSource = SQL.SQLDS.Tables(0)
            Dim dgvrowcnt = DGVProduct.Rows.Count
            DGVProduct.CurrentCell = DGVProduct.Rows(dgvrowcnt - 1).Cells(0)
            'DGVProduct.Rows(0).Selected = True
            DGVProduct.Sort(DGVProduct.Columns("PRNUM"), ListSortDirection.Ascending)  'sorts On cone number

            SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand
        End If


    End Sub


    Private Sub btnUpdate_Click_1(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Try
            SQL.SQLDA.Update(SQL.SQLDS)
            'REFRESH DATAGRID
            LoadGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try


        LoadGrid()

    End Sub
End Class