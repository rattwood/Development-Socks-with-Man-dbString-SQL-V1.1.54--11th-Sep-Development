Imports System.ComponentModel  'Allows function of Datagridview sorting and filtering
Imports System.Data.SqlClient

Public Class frmProductMod
    Dim opname_dept As String


    Private SQL As New SQLConn
    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError


    Private Sub frmProductMod_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtBoxOperator.Visible = True
        Label1.Visible = True
        txtBoxOperator.Clear()
        txtBoxOperator.Focus()

        ' LoadGrid()

    End Sub

    Private Sub txtBoxOperator_TextChanged(sender As Object, e As EventArgs) Handles txtBoxOperator.TextChanged

        BbtnEnter.Visible = True

    End Sub

    Private Sub BbtnEnter_Click(sender As Object, e As EventArgs) Handles BbtnEnter.Click


        If My.Settings.chkUseColour Then
            opname_dept = txtBoxOperator.Text & "_Colour"


        ElseIf My.Settings.chkUsePack Then
            opname_dept = txtBoxOperator.Text & "_Pack"


        ElseIf My.Settings.chkUseSort Then
            opname_dept = txtBoxOperator.Text & "_Sort"

        End If

        BbtnEnter.Visible = False

        txtBoxOperator.Visible = False
        Label1.Visible = False


        LoadGrid()

    End Sub




    Private Sub LoadGrid()
        SQL.ExecQuery("SELECT PRNUM,PRODNAME,MERGENUM,PRODWEIGHT,WEIGHTCODE,OPNAME,UPDATETIME FROM product order by prnum")
        If SQL.RecordCount > 0 Then


            DGVProduct.DataSource = SQL.SQLDS.Tables(0)
            Dim dgvrowcnt = DGVProduct.Rows.Count
            DGVProduct.CurrentCell = DGVProduct.Rows(dgvrowcnt - 1).Cells(0)

            SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand

        End If


    End Sub



    Private Sub DGVProduct_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGVProduct.CellValueChanged
        ' Pass the row and cell indexes to the method so we can change the color of the correct row
        ' CompareDgvToDataSource(e.RowIndex, e.ColumnIndex)

        RowsToUpdate.Items.Add(e.RowIndex)

        btnUpdate.BackColor = Color.Green
        btnUpdate.Visible = True
    End Sub



    Private Sub btnUpdate_Click_1(sender As Object, e As EventArgs) Handles btnUpdate.Click


        '*************************************  NEW CODE TO FIND REFRENCE OF ROWS CHANGED ******************************
        Dim tmpRowvalue As String

        If RowsToUpdate.Items.Count() > 0 Then
            For I = 1 To RowsToUpdate.Items.Count()
                tmpRowvalue = RowsToUpdate.Items(I - 1).ToString
                DGVProduct.Rows(tmpRowvalue).Cells("OPNAME").Value = opname_dept
                DGVProduct.Rows(tmpRowvalue).Cells("UPDATETIME").Value = DateAndTime.Now
            Next
            RowsToUpdate.Items.Clear()
            RowsToUpdate.ResetText()
        End If
        '****************************************************************************************************************

        Try
            SQL.SQLDA.Update(SQL.SQLDS)
            'REFRESH DATAGRID
            LoadGrid()
        Catch ex As Exception
            btnUpdate.BackColor = DefaultBackColor
            btnUpdate.Visible = False
            'Write error to Log File
            writeerrorLog.writelog("Product Update Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Product Update Error", ex.ToString, False, "System Fault")
            MessageBox.Show(ex.Message.ToString)
        End Try

        btnUpdate.BackColor = DefaultBackColor
        btnUpdate.Visible = False

        LoadGrid()

    End Sub


End Class