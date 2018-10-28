Public Class frmdbString

    Private Sub frmdbString_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = My.Settings.SQLConn

        'Shows current settings
        txtBoxTemplates.Text = My.Settings.dirTemplate
        txtBoxCarts.Text = My.Settings.dirCarts
        txtBoxJobs.Text = My.Settings.dirJobs
        txtBoxPack.Text = My.Settings.dirPacking
        txtBoxPackReports.Text = My.Settings.dirPackReports



        btnSave.Enabled = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        FolderBrowserDialog1.ShowDialog()
        txtBoxTemplates.Text = FolderBrowserDialog1.SelectedPath
        btnSave.Enabled = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        FolderBrowserDialog1.ShowDialog()
        txtBoxCarts.Text = FolderBrowserDialog1.SelectedPath
        btnSave.Enabled = True
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        FolderBrowserDialog1.ShowDialog()
        txtBoxJobs.Text = FolderBrowserDialog1.SelectedPath
        btnSave.Enabled = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        FolderBrowserDialog1.ShowDialog()
        txtBoxPack.Text = FolderBrowserDialog1.SelectedPath
        btnSave.Enabled = True
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        FolderBrowserDialog1.ShowDialog()
        txtBoxPackReports.Text = FolderBrowserDialog1.SelectedPath
        btnSave.Enabled = True
    End Sub



    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        My.Settings.SQLConn = TextBox1.Text
        My.Settings.dirTemplate = txtBoxTemplates.Text
        My.Settings.dirCarts = txtBoxCarts.Text
        My.Settings.dirJobs = txtBoxJobs.Text
        My.Settings.dirPacking = txtBoxPack.Text
        My.Settings.dirPackReports = txtBoxPackReports.Text


        btnSave.Enabled = False
        TextBox1.Refresh()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        btnSave.Enabled = True
    End Sub
End Class