
Public Class frmRemoveCone

    'Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged


    '    Me.btnContinue.Enabled = True



    'End Sub

    Private Sub chkBarcode()

        Dim chkBCode As String
        'Routine to check Barcode is TRUE
        Try

            chkBCode = TextBox1.Text

            If chkBCode = frmPacking.bcodeScan Then

                btnContinue.Enabled = True


            Else
                MsgBox("This is not the cone to remove")
                Me.TextBox1.Clear()
                Me.btnContinue.Enabled = False
                Me.TextBox1.Focus()
                Me.TextBox1.Refresh()
                Exit Sub
            End If

        Catch ex As Exception
            Me.TextBox1.Clear()
            Me.TextBox1.Focus()
            Me.TextBox1.Refresh()
            Exit Sub
        End Try

    End Sub


    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click


        frmPacking.txtConeBcode.Clear()
                frmPacking.txtConeBcode.Focus()
                frmPacking.Show()

        Me.Close()

    End Sub

    Private Sub frmRemoveCone_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Label5.Text = frmPacking.bcodeScan
        Me.KeyPreview = True

        Me.btnContinue.Enabled = False
        Me.KeyPreview = True  'Allows us to look for advace character from barcode

    End Sub



    'Check for Barcode F8
    Private Sub frmRemoveCone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then


            chkBarcode()


        End If

    End Sub


End Class