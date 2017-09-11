Imports System.ComponentModel

Public Class frmConeSearch
    Dim dbDate As Date
    Dim datestring As String

    Private Sub frmConeSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtBoxJob.Text = ""
        txtBoxConeBC.Text = ""
        txtBoxSpindle.Text = ""
        txtBoxSpindle.Enabled = False
        btnJobSearch.Enabled = False
        btnConeSearch.Enabled = False

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        txtBoxJob.Focus()



    End Sub


    Private Sub btnJobSearch_Click(sender As Object, e As EventArgs) Handles btnJobSearch.Click

        If txtBoxJob.TextLength > 12 Then
            MsgBox("Job number is not the correct length")
            Me.txtBoxJob.Clear()
            Me.txtBoxSpindle.Clear()
            Me.btnJobSearch.Enabled = False
            Me.txtBoxJob.Focus()
            Me.txtBoxJob.Refresh()
            Exit Sub
        End If



        frmJobEntry.LExecQuery("SELECT MCNUM,PRODNAME, DOFFNUM, CARTENDTM, OPPACK, OPCOLOUR, DEFCONE, CONESTATE, SHORTCONE FROM jobs Where BCODEJOB = '" & txtBoxJob.Text & "' AND CONENUM = '" & txtBoxSpindle.Text & "' ")

        If frmJobEntry.LRecordCount > 0 Then

        Else
            MsgBox("Job: " & txtBoxJob.Text & "   Spindle #: " & txtBoxSpindle.Text & "  Cannot be found")
            Me.txtBoxJob.Clear()
            Me.txtBoxSpindle.Clear()
            Me.btnJobSearch.Enabled = False
            Me.txtBoxJob.Focus()
            Me.txtBoxJob.Refresh()
            Exit Sub
        End If




        Try

            frmJobEntry.LExecQuery("SELECT MCNUM, PRODNAME, DOFFNUM, CARTENDTM, OPPACK, OPCOLOUR, DEFCONE, CONESTATE, SHORTCONE, CARTONNUM FROM jobs Where BCODEJOB = '" & txtBoxJob.Text & "' AND CONENUM = '" & txtBoxSpindle.Text & "' ")



            If frmJobEntry.LRecordCount > 0 Then

                jobSearch()

            Else
                MsgBox("Job: " & txtBoxJob.Text & "   Spindle #: " & txtBoxSpindle.Text & "  Cannot be found")
                Me.txtBoxJob.Clear()
                Me.txtBoxSpindle.Clear()
                Me.btnJobSearch.Enabled = False
                Me.txtBoxJob.Focus()
                Me.txtBoxJob.Refresh()
                Exit Sub

            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
            Me.txtBoxJob.Clear()
            Me.btnJobSearch.Enabled = False
            Me.txtBoxJob.Focus()
            Me.txtBoxJob.Refresh()
            Exit Sub
        End Try
    End Sub

    Private Sub btnConeSearch_Click(sender As Object, e As EventArgs) Handles btnConeSearch.Click

        frmJobEntry.LExecQuery("SELECT MCNUM, PRODNAME, DOFFNUM, CARTENDTM, OPPACK, OPCOLOUR, DEFCONE, CONESTATE, SHORTCONE, CARTONNUM FROM jobs Where BCODECONE = '" & txtBoxConeBC.Text & "' ")

        If frmJobEntry.LRecordCount > 0 Then
            jobSearch()
            Exit Sub
        Else
            MsgBox("Cone #: " & txtBoxSpindle.Text & "  Cannot be found")
            Me.txtBoxJob.Clear()
            Me.txtBoxSpindle.Clear()
            Me.btnJobSearch.Enabled = False
            Me.txtBoxJob.Focus()
            Me.txtBoxJob.Refresh()
            Exit Sub
        End If

    End Sub

    Private Sub jobSearch()


        'LOAD THE DATA FROM dB IN TO THE DATAGRID
        DataGridView1.DataSource = frmJobEntry.LDS.Tables(0)
        DataGridView1.Rows(0).Selected = True

        'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
        DataGridView1.Sort(DataGridView1.Columns(7), ListSortDirection.Ascending)  'sorts On cone number
        'frmPrintCartReport.Show()

        'PRODUCT NAME
        txtBoxProdName.Text = DataGridView1.Rows(0).Cells(1).Value
        'DOFFING NUMBER
        txtBoxDoff.Text = DataGridView1.Rows(0).Cells(2).Value
        'DATE
        dbDate = DataGridView1.Rows(0).Cells(3).Value.ToString
        dateConv()
        txtBoxPackDate.Text = datestring


        'PACKER INFORMATION
        'If DataGridView1.Rows(0).Cells(4).Value > 0 Then txtBoxPacker.Text = DataGridView1.Rows(0).Cells(4).Value.ToString Else txtBoxPacker.Text = ""
        'COLOR CHECKER INFO
        txtBoxColour.Text = DataGridView1.Rows(0).Cells(5).Value
        'DEFECTS
        If DataGridView1.Rows(0).Cells(6).Value > 0 Then txtBoxDef.Text = "Yes" Else txtBoxDef.Text = "No"
        'GRADE
        If DataGridView1.Rows(0).Cells(7).Value > 0 Then
            Select Case DataGridView1.Rows(0).Cells(7).Value
                Case 8, 14, 16
                    txtBoxGrad.Text = "Grade B"
                    txtBoxPacker.Text = "N/A"
                    txtBoxCartonNum.Text = "N/A"
                Case 9, 15
                    txtBoxGrad.Text = "Grade A"
                    txtBoxPacker.Text = DataGridView1.Rows(0).Cells(4).Value
                    txtBoxCartonNum.Text = DataGridView1.Rows(0).Cells(9).Value.ToString
            End Select
        End If
        'SHORT
        If DataGridView1.Rows(0).Cells(8).Value > 0 Then txtBoxShort.Text = "Yes" Else txtBoxShort.Text = "No"
        'MACHINE NUMBER
        txtBoxMCNum.Text = DataGridView1.Rows(0).Cells(0).Value
        'If DataGridView1.Rows(0).Cells(61).Value > 0 Then txtBoxCartonNum.Text = DataGridView1.Rows(0).Cells(61).Value Else txtBoxCartonNum.Text = 0
        'txtBoxCartonNum.Text = DataGridView1.Rows(0).Cells(8).Value.ToString

    End Sub

    Public Sub dateConv()

        Try
            datestring = dbDate.ToString("dd/MM/yyyy")
        Catch ex As Exception
            MsgBox("Date Missing in Database")
        End Try




    End Sub

    Private Sub txtBoxJob_TextChanged(sender As Object, e As EventArgs) Handles txtBoxJob.TextChanged
        txtBoxConeBC.Clear()
        txtBoxSpindle.Enabled = True


    End Sub

    Private Sub txtBoxConeBC_TextChanged(sender As Object, e As EventArgs) Handles txtBoxConeBC.TextChanged
        txtBoxJob.Clear()
        btnConeSearch.Enabled = True

    End Sub

    Private Sub txtBoxSpindle_TextChanged(sender As Object, e As EventArgs) Handles txtBoxSpindle.TextChanged

        btnJobSearch.Enabled = True

    End Sub

    Private Sub frmConeSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

        End If
    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click

        Me.Close()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
    End Sub
End Class