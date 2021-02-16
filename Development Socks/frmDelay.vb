Public Class frmDelay
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim interval As Integer = "2000"
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        sw.Stop()

        frmCart1.Show()
        Me.Close()
    End Sub
End Class