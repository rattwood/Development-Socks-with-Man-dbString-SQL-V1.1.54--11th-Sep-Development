
Public Class frmRemoveCone



    Private Sub chkBarcode()

        Dim chkBCode As String
        'Routine to check Barcode is TRUE
        Try

            chkBCode = TextBox1.Text

            If frmJobEntry.txtGrade.Text = "Normal" Then
                If chkBCode = frmPacking.bcodeScan Then

                    btnContinue.Enabled = True
                    btnContinue.Enabled = True
                Else
                    MsgBox("This is not the cone to remove")
                    Me.TextBox1.Clear()
                    Me.btnContinue.Enabled = False
                    Me.TextBox1.Focus()
                    Me.TextBox1.Refresh()
                    Exit Sub
                End If
            End If



            If frmJobEntry.txtGrade.Text <> "Normal" Then
                'Routine for non Grade A cheese
                If chkBCode = frmB_AL_AD_W.bcodeScan Then

                    btnContinue.Visible = True
                    btnContinue.Enabled = True
                Else
                    MsgBox("This is not the cone to remove")
                    Me.TextBox1.Clear()
                    Me.btnContinue.Enabled = False
                    Me.TextBox1.Focus()
                    Me.TextBox1.Refresh()
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            Me.TextBox1.Clear()
            Me.TextBox1.Focus()
            Me.TextBox1.Refresh()
            Exit Sub
        End Try

    End Sub


    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click

        If frmJobEntry.txtGrade.Text = "A" Then
            frmPacking.txtConeBcode.Clear()
            frmPacking.txtConeBcode.Focus()
            frmPacking.Show()
        Else
            frmB_AL_AD_W.txtConeBcode.Clear()
            frmB_AL_AD_W.txtConeBcode.Focus()
            frmB_AL_AD_W.Show()
        End If

        Me.Close()

    End Sub

    Private Sub frmRemoveCone_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnContinue.Visible = False

        If frmJobEntry.txtGrade.Text = "A" Then
            Label1.Text = "Not Grade 'A' Cheese"
            Me.Label5.Text = frmPacking.bcodeScan
        Else
            Label1.Text = "Not Grade " & "'" & frmJobEntry.txtGrade.Text & "'" & " Cheese"
            Me.Label5.Text = frmB_AL_AD_W.bcodeScan
        End If

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