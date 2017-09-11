Public Class frmPackingFault

    Dim changeConeNum As Integer
    Dim defectCone As Integer
    Dim shortCone As Integer
    Dim chkBcode
    Dim coneNum As String


    Private Sub frmPackingFault_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        defectCone = 0
        shortCone = 0

        Me.btnContinue.Visible = True 'Show Save button when form opens
        Me.btnClear.Visible = False  'Show Cancel button when form opens
        Me.btnDefect.Enabled = False
        Me.btnShort.Enabled = False


        Me.chk_K.Visible = False
        Me.chk_D.Visible = False
        Me.chk_F.Visible = False
        Me.chk_O.Visible = False
        Me.chk_T.Visible = False
        Me.chk_P.Visible = False
        Me.chk_N.Visible = False
        Me.chk_W.Visible = False
        Me.chk_H.Visible = False
        Me.chk_TR.Visible = False
        Me.chk_B.Visible = False
        Me.chk_C.Visible = False
        Me.Label3.Visible = False
        Me.KeyPreview = True  'Allows us to look for advace character from barcode

    End Sub



    Private Sub checkBcode()

        chkBcode = TextBox1.Text
        changeConeNum = 0

        'THIS CHECKS CONE ROW NUMBER IN DGV

        For i = 1 To 32
                If frmDGV.DGVdata.Rows(i - 1).Cells(36).Value = chkBcode Then
                changeConeNum = i
                coneNum = frmDGV.DGVdata.Rows(i - 1).Cells(6).Value   'GET THE ACTUAL CONE NUMBER
            End If
            Next

            If changeConeNum = 0 Then
                Label3.Visible = True
            Label3.Text = "This is not a Cheese from this Cart. Please Re Scan"
            DelayTM()
                Label3.Visible = False




                'MsgBox("This is not a Cone from this Cart. Please Re Scan")

                defectCone = 0
                shortCone = 0

                Me.btnContinue.Visible = False 'Show Save button when form opens
                Me.btnClear.Visible = False  'Show Cancel button when form opens
                Me.btnDefect.Enabled = False
                Me.btnShort.Enabled = False

                Me.chk_K.Visible = False
                Me.chk_D.Visible = False
                Me.chk_F.Visible = False
                Me.chk_O.Visible = False
                Me.chk_T.Visible = False
                Me.chk_P.Visible = False
                Me.chk_N.Visible = False
                Me.chk_W.Visible = False
                Me.chk_H.Visible = False
                Me.chk_TR.Visible = False
                Me.chk_B.Visible = False
                Me.chk_C.Visible = False
                Me.TextBox1.Clear()
                Me.TextBox1.Focus()
                Me.TextBox1.Refresh()
                Exit Sub
            Else
                'ENABLE KEYS IF CONE NUMBER VALID
                Me.btnDefect.Enabled = True
                Me.btnShort.Enabled = True
            Me.btnReSetShort.Enabled = True

        End If




    End Sub

    Private Sub DelayTM()
        Dim interval As Integer = "2000"
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            Application.DoEvents()
        Loop
        sw.Stop()

    End Sub


    Private Sub btnShort_Click(sender As Object, e As EventArgs) Handles btnShort.Click

        shortCone = 1
        defectCone = 0

        Me.btnContinue.Visible = True 'Show Save button when form opens
        Me.btnContinue.Enabled = True
        Me.btnClear.Visible = True  'Show Cancel button when form opens
        Me.btnDefect.Enabled = False
        Me.btnShort.Enabled = False
        Me.btnReSetShort.Enabled = False

        Me.chk_K.Visible = False
        Me.chk_D.Visible = False
        Me.chk_F.Visible = False
        Me.chk_O.Visible = False
        Me.chk_T.Visible = False
        Me.chk_P.Visible = False
        Me.chk_N.Visible = False
        Me.chk_W.Visible = False
        Me.chk_H.Visible = False
        Me.chk_TR.Visible = False
        Me.chk_B.Visible = False
        Me.chk_C.Visible = False

        If changeConeNum Then


            Me.btnContinue.Visible = True 'Show continue button when form opens

        End If


    End Sub

    Private Sub btnDefect_Click(sender As Object, e As EventArgs) Handles btnDefect.Click

        defectCone = 1
        shortCone = 0

        Me.btnContinue.Visible = True 'Show Save button when form opens
        Me.btnContinue.Enabled = True
        Me.btnClear.Visible = True  'Show Cancel button when form opens
        Me.btnDefect.Enabled = False
        Me.btnShort.Enabled = False
        Me.btnReSetShort.Enabled = False


        Me.chk_K.Visible = True
            Me.chk_D.Visible = True
            Me.chk_F.Visible = True
            Me.chk_O.Visible = True
            Me.chk_T.Visible = True
            Me.chk_P.Visible = True
            Me.chk_N.Visible = True
            Me.chk_W.Visible = True
            Me.chk_H.Visible = True
            Me.chk_TR.Visible = True
            Me.chk_B.Visible = True
            Me.chk_C.Visible = True

        If changeConeNum Then


            Me.btnContinue.Visible = True 'Show continue button when form opens

        End If

    End Sub

    Private Sub btnReSetShort_Click(sender As Object, e As EventArgs) Handles btnReSetShort.Click

        defectCone = 0
        shortCone = 2



        Me.btnContinue.Visible = True 'Show Save button when form opens
        Me.btnContinue.Enabled = True
        Me.btnClear.Visible = True  'Show Cancel button when form opens
        Me.btnDefect.Enabled = False
        Me.btnShort.Enabled = False
        Me.btnReSetShort.Enabled = False

        Me.chk_K.Visible = False
        Me.chk_D.Visible = False
        Me.chk_F.Visible = False
        Me.chk_O.Visible = False
        Me.chk_T.Visible = False
        Me.chk_P.Visible = False
        Me.chk_N.Visible = False
        Me.chk_W.Visible = False
        Me.chk_H.Visible = False
        Me.chk_TR.Visible = False
        Me.chk_B.Visible = False
        Me.chk_C.Visible = False

        If changeConeNum Then


            Me.btnContinue.Visible = True 'Show continue button when form opens

        End If

    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click

        Dim hasdefect As Integer = 0
        'Routine to check Barcode is TRUE
        Try



            If frmPacking.packingActive Then

                frmPacking.Controls("btnCone" & changeConeNum.ToString).BackColor = Color.Yellow

                If defectCone = 1 Then
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(9).Value = "14"  'change cone state back to DEFECT FROM PACKING
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_K").Value = chk_K.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_D").Value = chk_D.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_F").Value = chk_F.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_O").Value = chk_O.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_T").Value = chk_T.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_P").Value = chk_P.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_N").Value = chk_N.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_W").Value = chk_W.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_H").Value = chk_H.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_TR").Value = chk_TR.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_B").Value = chk_B.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_C").Value = chk_C.Checked
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("OPNAME").Value = frmJobEntry.PackOp
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("DEFCONE").Value = frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(6).Value
                End If

                If shortCone = 1 Then

                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(9).Value = "14"  'change cone state back to defect cone
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(10).Value = frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(6).Value 'shortCone
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(43).Value = "True" 'Sets the SHORT fault flag
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("OPNAME").Value = frmJobEntry.PackOp
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("OPPACK").Value = frmJobEntry.PackOp

                End If


                If shortCone = 2 Then  'NOT SHORT CONE
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(9).Value = "9"  'change cone state back to defect cone
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(10).Value = 0 'ReSet shortCone
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(43).Value = "False" 'Re Sets the SHORT fault flag



                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_K").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_D").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_F").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_O").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_T").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_P").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_N").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_W").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_H").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_TR").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_B").Value = True Then hasdefect = 1
                    If frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("FLT_C").Value = True Then hasdefect = 1
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("OPNAME").Value = frmJobEntry.PackOp
                    frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                    If hasdefect = 1 Then
                        frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(9).Value = "14"
                        frmDGV.DGVdata.Rows(changeConeNum - 1).Cells("DEFCONE").Value = frmDGV.DGVdata.Rows(changeConeNum - 1).Cells(6).Value
                    End If

                End If

                UpdateDatabase()
            End If

        Catch ex As Exception

            MsgBox(ex.ToString)
            defectCone = 0
            shortCone = 0
            hasdefect = 0

            Me.btnContinue.Visible = False 'Show Save button when form opens
            Me.btnClear.Visible = False  'Show Cancel button when form opens
            Me.btnDefect.Enabled = False
            Me.btnShort.Enabled = False
            Me.btnReSetShort.Enabled = False

            Me.chk_K.Visible = False
            Me.chk_D.Visible = False
            Me.chk_F.Visible = False
            Me.chk_O.Visible = False
            Me.chk_T.Visible = False
            Me.chk_P.Visible = False
            Me.chk_N.Visible = False
            Me.chk_W.Visible = False
            Me.chk_H.Visible = False
            Me.chk_TR.Visible = False
            Me.chk_B.Visible = False
            Me.chk_C.Visible = False

            Me.TextBox1.Clear()
            Me.TextBox1.Focus()
            Me.TextBox1.Refresh()
            Exit Sub
        End Try



        'UpdateDatabase()

        If shortCone = 2 And hasdefect = 0 Then

            frmPacking.toAllocatedCount = frmPacking.toAllocatedCount + 1  'reduce number of cones to allocate

        Else

            frmPacking.toAllocatedCount = frmPacking.toAllocatedCount - 1  'reduce number of cones to allocate

        End If


        defectCone = 0
        shortCone = 0


        Me.btnContinue.Visible = False 'Show Save button when form opens
        Me.btnClear.Visible = False  'Show Cancel button when form opens
        Me.btnDefect.Enabled = False
        Me.btnShort.Enabled = False
        Me.btnReSetShort.Enabled = False

        Me.chk_K.Visible = False
        Me.chk_D.Visible = False
        Me.chk_F.Visible = False
        Me.chk_O.Visible = False
        Me.chk_T.Visible = False
        Me.chk_P.Visible = False
        Me.chk_N.Visible = False
        Me.chk_W.Visible = False
        Me.chk_H.Visible = False
        Me.chk_TR.Visible = False
        Me.chk_B.Visible = False
        Me.chk_C.Visible = False
        frmPacking.UpdateConeVal()
        frmPacking.Show()
        frmPacking.txtConeBcode.Clear()
        frmPacking.txtConeBcode.Focus()
        frmPacking.endCheck()   'CHECK TO SEE IF THIS WAS THE LAST CHEESE 
        Me.Close()

    End Sub


    Private Sub btnGoBack_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click

        If frmPacking.packingActive Then
            Me.Close()
            frmPacking.Show()
            frmPacking.txtConeBcode.Clear()
            frmPacking.txtConeBcode.Focus()
        Else 'we have come from job entry screen
            Me.Close()
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
        End If


    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        defectCone = 0
        shortCone = 0

        Me.chk_K.Visible = False
        Me.chk_D.Visible = False
        Me.chk_F.Visible = False
        Me.chk_O.Visible = False
        Me.chk_T.Visible = False
        Me.chk_P.Visible = False
        Me.chk_N.Visible = False
        Me.chk_W.Visible = False
        Me.chk_H.Visible = False
        Me.chk_TR.Visible = False
        Me.chk_B.Visible = False
        Me.chk_C.Visible = False

        Me.chk_K.Checked = False
        Me.chk_D.Checked = False
        Me.chk_F.Checked = False
        Me.chk_O.Checked = False
        Me.chk_T.Checked = False
        Me.chk_P.Checked = False
        Me.chk_N.Checked = False
        Me.chk_W.Checked = False
        Me.chk_H.Checked = False
        Me.chk_TR.Checked = False
        Me.chk_B.Checked = False
        Me.chk_C.Checked = False

        Me.TextBox1.Clear()
        Me.TextBox1.Focus()

        Me.btnDefect.Enabled = False
        Me.btnShort.Enabled = False
        Me.btnReSetShort.Enabled = False

    End Sub





    'DATABASE UPDATE ROUTINES


    Private Sub UpdateDatabase()

        tsbtnSave()


        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            If frmJobEntry.LDS.HasChanges Then


                'frmJobEntry.LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

                frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

            End If
        Catch ex As Exception

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try





    End Sub

    Public Sub tsbtnSave()




        Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows

        frmDGV.DGVdata.AllowUserToAddRows = True
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmDGV.DGVdata.AllowUserToAddRows = bAddState


    End Sub

    Private Sub frmPackingFault_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            checkBcode()
        End If
    End Sub


End Class