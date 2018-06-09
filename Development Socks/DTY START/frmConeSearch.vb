Imports System.ComponentModel

Public Class frmConeSearch
    Dim dbDate As Date
    Dim datestring As String
    Dim jobSearchFlag As Integer
    Dim coneSearchFlag As Integer

    Private Sub frmConeSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtBoxJob.Text = ""
        txtBoxConeBC.Text = ""
        'txtBoxSpindle.Text = ""
        'txtBoxSpindle.Enabled = False
        btnJobSearch.Enabled = False
        btnConeSearch.Enabled = False

        Me.KeyPreview = True  'Allows us to look for advace character from barcode

        txtBoxJob.Focus()


    End Sub


    Private Sub btnJobSearch_Click(sender As Object, e As EventArgs) Handles btnJobSearch.Click

        jobSearchFlag = 1

        If txtBoxJob.TextLength < 14 Then
            MsgBox("Job number is not the correct length")
            Me.txtBoxJob.Clear()
            Me.txtBoxSpindle.Clear()
            Me.btnJobSearch.Enabled = False
            Me.txtBoxJob.Focus()
            Me.txtBoxJob.Refresh()
            Exit Sub
        End If



        'frmJobEntry.LExecQuery("SELECT MCNUM,PRODNAME, DOFFNUM, CARTENDTM, OPPACK, OPCOLOUR, DEFCONE, CONESTATE, SHORTCONE FROM jobs Where BCODEJOB = '" & txtBoxJob.Text & "' AND CONENUM = '" & txtBoxSpindle.Text & "' ")

        ' frmJobEntry.LExecQuery("SELECT MCNUM,PRODNAME, DOFFNUM, MERGENUM, CONENUM, CONESTATE, SHORTCONE, MISSCONE, DEFCONE, M30, P30, CONEBARLEY, SORTENDTM, COLENDTM, PACKENDTM FROM jobs Where BCODEJOB = '" & txtBoxJob.Text & "' ")






        Try

            ' frmJobEntry.LExecQuery("SELECT MCNUM, PRODNAME, DOFFNUM, CARTENDTM, OPPACK, OPCOLOUR, DEFCONE, CONESTATE, SHORTCONE, CARTONNUM FROM jobs Where BCODEJOB = '" & txtBoxJob.Text & "' AND CONENUM = '" & txtBoxSpindle.Text & "' ")
            frmJobEntry.LExecQuery("SELECT MCNUM,PRODNAME, MERGENUM, DOFFNUM,CONENUM, CONESTATE, SHORTCONE, MISSCONE, DEFCONE, M30, P30, CONEBARLEY, SORTENDTM, COLENDTM, PACKENDTM FROM jobs Where BCODECART = '" & txtBoxJob.Text & "' ")



            If frmJobEntry.LRecordCount > 0 Then

                jobSearch()

            Else
                MsgBox("Job: " & txtBoxJob.Text & "  Cannot be found")
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

        jobSearchFlag = 0

    End Sub

    Private Sub btnConeSearch_Click(sender As Object, e As EventArgs) Handles btnConeSearch.Click

        frmJobEntry.LExecQuery("SELECT * FROM jobs Where BCODECONE = '" & txtBoxConeBC.Text & "' ")
        coneSearchFlag = 1

        Try
            If frmJobEntry.LRecordCount > 0 Then
                jobSearch()
                Exit Sub
            Else
                MsgBox("Cheese #: " & txtBoxSpindle.Text & "  Cannot be found")
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
        coneSearchFlag = 0
    End Sub

    Private Sub jobSearch()



        If coneSearchFlag Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DataGridView1.DataSource = frmJobEntry.LDS.Tables(0)
            DataGridView1.Rows(0).Selected = True

            'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
            DataGridView1.Sort(DataGridView1.Columns("CONENUM"), ListSortDirection.Ascending)  'sorts On cone number
            'frmPrintCartReport.Show()

            'PRODUCT NAME
            txtBoxProdName.Text = DataGridView1.Rows(0).Cells("PRODNAME").Value
            'DOFFING NUMBER
            txtBoxDoff.Text = DataGridView1.Rows(0).Cells("DOFFNUM").Value
            'MACHINE NUMBER
            txtBoxMCNum.Text = DataGridView1.Rows(0).Cells("MCNUM").Value

            'PACKING INFO 
            If Not IsDBNull(DataGridView1.Rows(0).Cells("PACKENDTM").Value) Then
                dbDate = DataGridView1.Rows(0).Cells("PACKENDTM").Value.ToString
                dateConv()
                txtBoxPackDate.Text = datestring
                txtBoxPacker.Text = DataGridView1.Rows(0).Cells("OPPACK").Value
                txtBoxPacker.Text = DataGridView1.Rows(0).Cells("OPPACK").Value
            Else
                txtBoxCartonNum.Text = ""
                txtBoxPacker.Text = ""
            End If


            'SORT CHECKER INFORMATION
            If DataGridView1.Rows(0).Cells("OPSORT").Value > "0" Then TextBox1.Text = DataGridView1.Rows(0).Cells("OPSORT").Value Else TextBox1.Text = ""

            'COLOR CHECKER INFO
            If DataGridView1.Rows(0).Cells("OPCOLOUR").Value > "0" Then txtBoxColour.Text = DataGridView1.Rows(0).Cells("OPCOLOUR").Value Else txtBoxColour.Text = ""

            'DEFECTS
            If DataGridView1.Rows(0).Cells("DEFCONE").Value > "0" Then txtBoxDef.Text = "Yes" Else txtBoxDef.Text = "No"
            'ReCheck DEFECTS

            If Not IsDBNull(DataGridView1.Rows(0).Cells("RECHK").Value) And Not IsDBNull(DataGridView1.Rows(0).Cells("RECHKDEFCODE").Value) Then txtReChkDef.Text = "Yes" Else txtBoxDef.Text = "No"

            'GRADE
            If DataGridView1.Rows(0).Cells("CONESTATE").Value > "0" Then
                Select Case DataGridView1.Rows(0).Cells("CONESTATE").Value
                    Case 8, 14, 16
                        If DataGridView1.Rows(0).Cells("DEFCONE").Value > "0" And Not (DataGridView1.Rows(0).Cells("M30").Value > "0" Or DataGridView1.Rows(0).Cells("P30").Value > "0") Or DataGridView1.Rows(0).Cells("CONEBARLEY").Value > "0" Or DataGridView1.Rows(0).Cells("COLWASTE").Value > "0" Then

                            If DataGridView1.Rows(0).Cells("FLT_W").Value = "True" Or DataGridView1.Rows(0).Cells("COLWASTE").Value > "0" Then
                                txtBoxGrad.Text = "Waste"
                            Else
                                txtBoxGrad.Text = "B"
                            End If
                        ElseIf DataGridView1.Rows(0).Cells("M30").Value > "0" Then
                            txtBoxGrad.Text = "-30"
                        ElseIf DataGridView1.Rows(0).Cells("P30").Value > "0" Then
                            txtBoxGrad.Text = "+30"
                        End If

                        If DataGridView1.Rows(0).Cells("FLT_X").Value = "True" Then txtBoxGrad.Text = "Missing"
                    Case 9, 15
                        If Not IsDBNull(DataGridView1.Rows(0).Cells("STDSTATE").Value) Then

                            If DataGridView1.Rows(0).Cells("STDSTATE").Value > 0 And DataGridView1.Rows(0).Cells("STDSTATE").Value < 10 Then txtBoxGrad.Text = "Standard"
                        Else
                            txtBoxGrad.Text = "Grade A"
                        End If

                End Select
            End If
            'SHORT
            If DataGridView1.Rows(0).Cells("SHORTCONE").Value > "0" Or DataGridView1.Rows(0).Cells("FLT_S").Value = True Then txtBoxShort.Text = "Yes" Else txtBoxShort.Text = "No"



            'ReCHECK DISPLAYS
            If Not IsDBNull(DataGridView1.Rows(0).Cells("RECHK").Value) Then
                If DataGridView1.Rows(0).Cells("RECHK").Value > "0" Then
                    'HIDE RECHECK INFO ON OPEN
                    Label16.Visible = True
                    Label15.Visible = True
                    Label17.Visible = True
                    Label18.Visible = True
                    Label19.Visible = True
                    Label20.Visible = True
                    Label21.Visible = True
                    Label22.Visible = True
                    Label23.Visible = True

                    txtReChkPackDate.Visible = True
                    txtReChkSort.Visible = True
                    txtReChkCol.Visible = True
                    txtReChkPacker.Visible = True
                    txtReChkGrade.Visible = True
                    txtReChkDef.Visible = True
                    txtBoxCartonNum2.Visible = True
                    txtTraceNum2.Visible = True
                Else
                    Label16.Visible = False
                    Label15.Visible = False
                    Label17.Visible = False
                    Label18.Visible = False
                    Label19.Visible = False
                    Label20.Visible = False
                    Label21.Visible = False
                    Label22.Visible = False
                    Label23.Visible = False

                    txtReChkPackDate.Visible = False
                    txtReChkSort.Visible = False
                    txtReChkCol.Visible = False
                    txtReChkPacker.Visible = False
                    txtReChkGrade.Visible = False
                    txtReChkDef.Visible = False
                    txtBoxCartonNum2.Visible = False
                    txtTraceNum2.Visible = False
                End If
            End If

            If Not IsDBNull(DataGridView1.Rows(0).Cells("PACKENDTM").Value) Then
                dbDate = DataGridView1.Rows(0).Cells("PACKENDTM").Value.ToString
                dateConv()
                txtReChkPackDate.Text = datestring
            End If

            'ReCheck SORT
            txtReChkSort.Text = DataGridView1.Rows(0).Cells("OPSORT").Value

            'ReCheck COLOUR
            If Not IsDBNull(DataGridView1.Rows(0).Cells("RECHKCOLOP").Value) Then txtReChkSort.Text = DataGridView1.Rows(0).Cells("RECHKCOLOP").Value Else txtReChkSort.Text = "-"


            'ReCheck PACK
            txtReChkSort.Text = DataGridView1.Rows(0).Cells("OPPACK").Value

            'RECHECK GRADES
            If Not IsDBNull(DataGridView1.Rows(0).Cells("RECHKRESULT").Value) Then txtReChkGrade.Text = DataGridView1.Rows(0).Cells("RECHKRESULT").Value Else txtReChkGrade.Text = "-"




            'ReCheck DEFECTS
            If Not IsDBNull(DataGridView1.Rows(0).Cells("RECHKDEFCODE").Value) Then txtReChkDef.Text = "Yes" Else txtBoxDef.Text = "No"

            'ReCHECK CARTON
            txtBoxCartonNum2.Text = DataGridView1.Rows(0).Cells("CARTONNUM").Value

            'ReCHECK TRACE #
            'txtTraceNum2.Text = DataGridView1.Rows(0).Cells("").Value

        End If

        If jobSearchFlag Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DataGridView1.DataSource = frmJobEntry.LDS.Tables(0)
            DataGridView1.Rows(0).Selected = True

            'SORT GRIDVIEW IN TO CORRECT JOB SEQUENCE
            DataGridView1.Sort(DataGridView1.Columns("CONENUM"), ListSortDirection.Ascending)  'sorts On cone number
            DataGridView1.Visible = True

        End If









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
        'txtBoxSpindle.Enabled = True
        btnJobSearch.Enabled = True

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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class