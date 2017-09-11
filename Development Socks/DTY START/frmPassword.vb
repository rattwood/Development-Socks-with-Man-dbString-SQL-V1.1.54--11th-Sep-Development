Public Class frmPassword
    Private Sub password_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnContinue.Click

        If TextBox1.Text = My.Settings.OpPassword Or TextBox1.Text = My.Settings.MasterPassword Then
            frmSettingSelect.Show()
        Else
            MsgBox("The Password Is Incorrect")
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        frmChangePassword.Show()
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class