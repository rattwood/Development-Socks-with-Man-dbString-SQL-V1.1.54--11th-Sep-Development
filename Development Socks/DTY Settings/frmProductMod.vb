Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering
Imports System.Data.SqlClient

Public Class frmProductMod
    Private SQL As New SQLConn
    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError


    Private Sub frmProductMod_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SQL.ExecQuery("SELECT * FROM product")

        LoadGrid()



    End Sub

    Private Sub LoadGrid()
        SQL.ExecQuery("SELECT PRNUM,PRODNAME,MERGENUM,PRODWEIGHT,WEIGHTCODE FROM product order by prnum")
        If SQL.RecordCount > 0 Then


            DGVProduct.DataSource = SQL.SQLDS.Tables(0)
            Dim dgvrowcnt = DGVProduct.Rows.Count
            DGVProduct.CurrentCell = DGVProduct.Rows(dgvrowcnt - 1).Cells(0)
            ' DGVProduct.Sort(DGVProduct.Columns("PRNUM"), ListSortDirection.Ascending)  'sorts On cone number

            SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand

        End If


    End Sub


    Private Sub btnUpdate_Click_1(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Try
            SQL.SQLDA.Update(SQL.SQLDS)
            'REFRESH DATAGRID
            LoadGrid()
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("Product Update Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Product Update Error", ex.ToString, False, "System Fault")
            MessageBox.Show(ex.Message.ToString)
        End Try


        LoadGrid()

    End Sub
End Class