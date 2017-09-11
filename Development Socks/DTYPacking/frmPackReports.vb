Public Class frmPackReports

    Private Sub btnEODRep_Click(sender As Object, e As EventArgs) Handles btnEODRep.Click
        Me.Hide()
        frmEODReport.Show()
    End Sub

    Private Sub btnDailyProdRep_Click(sender As Object, e As EventArgs) Handles btnDailyProdRep.Click
        'Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'lblMessage.Text = "Please wait Creating Daily Production Report"
        frmDailyPackProduction.Show()
        'frmDailyPackProduction.processReport()
        'Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub btnStockWorkRep_Click(sender As Object, e As EventArgs) Handles btnStockWorkRep.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        lblMessage.Text = "Please wait Creating Work in Process Report"
        frmProdStockWork.processReport()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Me.Close()
    End Sub


End Class