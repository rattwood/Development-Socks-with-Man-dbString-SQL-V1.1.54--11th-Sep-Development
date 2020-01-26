
Public Class frmRemoveCone
    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError

    Private Sub frmRemoveCone_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnContinue.Visible = False

        If frmJobEntry.txtGrade.Text = "A" Or frmJobEntry.txtGrade.Text = "Normal" Or frmJobEntry.txtGrade.Text = "Pilot 6Ch" Or frmJobEntry.txtGrade.Text = "Pilot 15Ch" Or
            frmJobEntry.txtGrade.Text = "Pilot 20Ch" Then

            Label1.Text = "Not Grade 'A' Cheese"
            Me.lblScannedCone.Text = frmPacking.bcodeScan

        ElseIf frmJobEntry.txtGrade.Text = "ReCheckA" Then
            Label1.Text = "Not Grade 'A' Cheese"
            Me.lblScannedCone.Text = frmPackRchkA.bcodeScan

        Else
            Label1.Text = "Not Grade " & "'" & frmJobEntry.txtGrade.Text & "'" & " Cheese"
            Me.lblScannedCone.Text = frmB_AL_AD_W.bcodeScan
        End If

        Me.btnContinue.Enabled = False
        Me.KeyPreview = True  'Allows us to look for advace character from barcode

    End Sub

    Private Sub chkBarcode()

        Dim chkBCode As String
        'Routine to check Barcode is TRUE
        Try

            chkBCode = TextBox1.Text
            MsgBox(frmJobEntry.txtGrade.Text & vbCrLf & lblScannedCone.Text)

            Select Case frmJobEntry.txtGrade.Text
                Case "Normal", "A", "Pilot 6Ch", "Pilot 15Ch", "Pilot 20Ch"

                    If chkBCode = lblScannedCone.Text Then
                        btnContinue.Visible = True
                        btnContinue.Enabled = True

                    Else
                        MsgBox("This is not the cone to remove")
                        Me.TextBox1.Clear()
                        Me.btnContinue.Enabled = False
                        Me.TextBox1.Focus()
                        Me.TextBox1.Refresh()
                    End If

                Case "ReCheckA"
                    If chkBCode = lblScannedCone.Text Then
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

                Case Else 'Check for grade B,AL,AD

                    If chkBCode = lblScannedCone.Text Then
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

            End Select




        Catch ex As Exception
            writeerrorLog.writelog("Barcode Error", ex.Message, False, "System_Fault")
            writeerrorLog.writelog("Barcode Error", ex.ToString, False, "System_Fault")

            MsgBox("Remove cannot find Barcode" & vbNewLine & ex.Message)


            Me.TextBox1.Clear()
            Me.TextBox1.Focus()
            Me.TextBox1.Refresh()
            Exit Sub
        End Try

    End Sub


    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click

        If frmJobEntry.txtGrade.Text = "A" Or frmJobEntry.txtGrade.Text = "Normal" Or frmJobEntry.txtGrade.Text = "Pilot 6Ch" Or frmJobEntry.txtGrade.Text = "Pilot 15Ch" Or
                frmJobEntry.txtGrade.Text = "Pilot 20Ch" Then
            frmPacking.txtConeBcode.Clear()
            frmPacking.txtConeBcode.Focus()
            frmPacking.Show()

        ElseIf frmJobEntry.txtGrade.Text = "ReCheckA" Then
            frmPackRchkA.txtConeBcode.Clear()
            frmPackRchkA.txtConeBcode.Focus()
            frmPackRchkA.Show()
        Else
            frmB_AL_AD_W.txtConeBcode.Clear()
            frmB_AL_AD_W.txtConeBcode.Focus()
            frmB_AL_AD_W.Show()
        End If

        Me.Close()

    End Sub





    'Check for Barcode F8
    Private Sub frmRemoveCone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then


            chkBarcode()


        End If

    End Sub


End Class