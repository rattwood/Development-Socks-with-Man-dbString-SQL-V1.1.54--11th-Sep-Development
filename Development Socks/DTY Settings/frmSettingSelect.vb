Public Class frmSettingSelect
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmSettings.Show()
        frmPassword.Close()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmProductMod.Show()
        frmPassword.Close()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If frmPassword.TextBox1.Text = My.Settings.MasterPassword Then
            frmdbString.Show()
            frmPassword.Close()
            Me.Close()
        Else
            MsgBox("The Password Is Incorrect  Password Error")
        End If

    End Sub

    Private Sub frmSettingSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class