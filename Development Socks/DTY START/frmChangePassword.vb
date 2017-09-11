Public Class frmChangePassword

    Private Sub frmChangePassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Visible = True
        TextBox2.Visible = False
        TextBox3.Visible = False
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        TextBox2.Visible = True

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

        TextBox3.Visible = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text = My.Settings.OpPassword Then
            If TextBox2.Text = TextBox3.Text Then
                My.Settings.OpPassword = TextBox2.Text
            Else
                MsgBox("New Passwords do not match", , "Entry Error")
            End If
            MsgBox("Password Updated")
        Else
            MsgBox("Current Password is Incorrect", , "Password Not Valid")
        End If
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If TextBox1.Text = My.Settings.MasterPassword Then
            My.Settings.Default.OpPassword = "user"
        Else
            MsgBox("Please Enter Master Password")
            TextBox1.Clear()
            TextBox1.Focus()
            Exit Sub

        End If

        MsgBox("Password Reset OK")
        Exit Sub
        Me.Close()
    End Sub
End Class