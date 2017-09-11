Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class frmExChangeCone
    Dim changeConeNum As Integer
    Dim defectCone As Integer
    Dim shortCone As Integer
    Dim removeCone As Integer
    Dim replacementCone As Integer
    Dim chkBcode
    Dim dcState, dcCarton, dcProdNum, dcDoffNum As String
    Dim rcState, rcCarton, rcProdNum, rcDoffNum As String



    Private Sub frmExChangeCone_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        defectCone = 0
        shortCone = 0
        removeCone = 0
        replacementCone = 0

        Me.btnContinue.Visible = True 'Show Save button when form opens
        Me.btnContinue.Enabled = False
        Me.btnClear.Visible = False 'Show Cancel button when form opens
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

        Me.KeyPreview = True  'Allows us to look for advace character from barcode
        If My.Settings.debugSet Then frmDGV.DGVdata.Visible = True

    End Sub

    Private Sub txtRemoveCone_TextChanged(sender As Object, e As EventArgs) Handles txtRemoveCone.TextChanged

    End Sub

    Private Sub txtReplaceCone_TextChanged(sender As Object, e As EventArgs) Handles txtReplaceCone.TextChanged

    End Sub


    Private Sub checkBcode()


        frmJobEntry.LExecQuery("SELECT * FROM jobs WHERE BCODECONE = '" & chkBcode & "' ")


        If removeCone = 1 Then
            If frmJobEntry.LRecordCount > 0 Then
                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = frmJobEntry.LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(frmJobEntry.LDA)

                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(6), ListSortDirection.Ascending)  'sorts On cone number

                dcState = frmDGV.DGVdata.Rows(0).Cells("CONESTATE").Value
                dcCarton = frmDGV.DGVdata.Rows(0).Cells("CARTONNUM").Value
                dcProdNum = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value
                dcDoffNum = frmDGV.DGVdata.Rows(0).Cells("DOFFNUM").Value

                Label3.Visible = True
                txtReplaceCone.Visible = True
                removeCone = 2
            Else
                MsgBox("Defect Cone does Not Exist")
                removeCone = 0
                txtRemoveCone.Clear()
                txtRemoveCone.Focus()
            End If
        End If

        If replacementCone = 1 Then

            If frmJobEntry.LRecordCount > 0 Then

                'LOAD THE DATA FROM dB IN TO THE DATAGRID
                frmDGV.DGVdata.DataSource = frmJobEntry.LDS.Tables(0)
                frmDGV.DGVdata.Rows(0).Selected = True
                Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(frmJobEntry.LDA)


                'SORT GRIDVIEW IN TO CORRECT CONE SEQUENCE
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(6), ListSortDirection.Ascending)  'sorts On cone number


                rcState = frmDGV.DGVdata.Rows(0).Cells("CONESTATE").Value
                rcProdNum = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value
                rcDoffNum = frmDGV.DGVdata.Rows(0).Cells("DOFFNUM").Value


            Else
                MsgBox("Replacment Cone does Not Exist")
                replacementCone = 0
                txtReplaceCone.Clear()
                txtReplaceCone.Focus()
            End If


        End If

        If replacementCone = 1 Then
            If dcState = 15 Then    'CHECK IF DEFECT CONE HAS BEEN ALLOCATED TO PACKING
                If dcState = rcState Then    'MAKE SURE THAT BOTH CONES ARE IN STATE 15 READY TO PACK
                    If dcProdNum = rcProdNum Then

                        btnShort.Enabled = True
                            btnDefect.Enabled = True
                            btnClear.Visible = True
                            btnClear.Enabled = True
                            replacementCone = 2

                        Else
                            replacementCone = 0
                            txtReplaceCone.Clear()
                            txtReplaceCone.Focus()
                            replacementCone = 0
                        MsgBox("2 CHEESES CANNOT BE EXCHANGED" & vbCr & "Defective Product:     " & dcProdNum & "  Doff #:  " & dcDoffNum & vbCr _
                           & "Replacment Product:  " & rcProdNum & "Doff #:  " & rcDoffNum)

                    End If
                Else
                    MsgBox("Both Cones not Grade A ")
                    replacementCone = 0
                    txtReplaceCone.Clear()
                    txtReplaceCone.Focus()
                End If
            End If
        End If



    End Sub


    Private Sub btnShort_Click(sender As Object, e As EventArgs) Handles btnShort.Click

        shortCone = 1
        defectCone = 0

        Me.btnContinue.Visible = True
        Me.btnClear.Enabled = True
        Me.btnDefect.Enabled = False
        Me.Label5.Visible = True
        Me.Label6.Visible = True
        Me.txtWeight.Visible = True
        Me.txtWeight.Focus()

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




        ' Me.btnContinue.Visible = True 'Show continue button when form opens




    End Sub

    Private Sub btnDefect_Click(sender As Object, e As EventArgs) Handles btnDefect.Click

        defectCone = 1
        shortCone = 0

        Me.btnContinue.Visible = True 'Show Save button when form opens
        Me.btnContinue.Enabled = True
        Me.btnClear.Visible = True  'Show Cancel button when form opens
        Me.btnDefect.Enabled = False


        Me.btnShort.Enabled = False
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




        Me.btnContinue.Visible = True 'Show continue button when form opens



    End Sub

    Private Sub btnGoBack_Click(sender As Object, e As EventArgs) Handles btnGoBack.Click


        Me.Close()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()




    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        defectCone = 0
        shortCone = 0
        removeCone = 0
        replacementCone = 0

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

        Me.txtRemoveCone.Clear()
        Me.txtRemoveCone.Focus()

        Me.txtReplaceCone.Visible = False
        Me.txtReplaceCone.Clear()
        Me.Label3.Visible = False
        Me.btnDefect.Enabled = False
        Me.btnShort.Enabled = False
        Me.btnClear.Enabled = False
        Me.Label5.Visible = False
        Me.Label6.Visible = False
        Me.txtWeight.Visible = False

        Me.KeyPreview = True 'Allows us to look for advace character from barcode
    End Sub





    'DATABASE UPDATE ROUTINES


    Private Sub UpdateDatabase()


        Dim conenum As String
        conenum = txtRemoveCone.Text
        conenum = conenum.Substring(conenum.Length - 3)


        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")
        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            'UPDATE DEFECTIVE CONE INFORMATION
            If defectCone = 1 Then


                frmJobEntry.LExecQuery("UPDATE jobs SET CONESTATE = '14' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET CARTONNUM = '0-0' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET DEFCONE = '" & conenum & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET CARTENDTM = '" & today & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_K = '" & chk_K.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_D = '" & chk_D.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_F = '" & chk_F.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_O = '" & chk_O.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_T = '" & chk_T.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_P = '" & chk_P.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_N = '" & chk_N.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_W = '" & chk_W.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_H = '" & chk_H.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_TR = '" & chk_TR.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_B = '" & chk_B.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_C = '" & chk_C.Checked & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET OPNAME = '" & frmJobEntry.txtOperator.Text & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET OPPACK = '" & frmJobEntry.txtOperator.Text & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")



            End If

            If shortCone = 1 Then


                frmJobEntry.LExecQuery("UPDATE jobs SET CONESTATE = '14' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET CARTONNUM = '0-0' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET SHORTCONE = '" & conenum & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET CARTENDTM = '" & today & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET FLT_S = 'True' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET OPNAME = '" & frmJobEntry.txtOperator.Text & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET OPPACK = '" & frmJobEntry.txtOperator.Text & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET CARTONNUM = '" & dcCarton & "' WHERE BCODECONE = '" & txtReplaceCone.Text & "' ")
                frmJobEntry.LExecQuery("UPDATE jobs SET WEIGHTERROR = '" & txtWeight.Text & "' WHERE BCODECONE = '" & txtRemoveCone.Text & "' ")


            End If

            'UPDATE REPLACEMENT CONE INFORMATION

            frmJobEntry.LExecQuery("UPDATE jobs SET CONESTATE = '16' WHERE BCODECONE = '" & txtReplaceCone.Text & "' ")
            frmJobEntry.LExecQuery("UPDATE jobs SET CARTENDTM = '" & today & "' WHERE BCODECONE = '" & txtReplaceCone.Text & "' ")
            frmJobEntry.LExecQuery("UPDATE jobs SET OPNAME = '" & frmJobEntry.txtOperator.Text & "' WHERE BCODECONE = '" & txtReplaceCone.Text & "' ")
            frmJobEntry.LExecQuery("UPDATE jobs SET OPPACK = '" & frmJobEntry.txtOperator.Text & "' WHERE BCODECONE = '" & txtReplaceCone.Text & "' ")
            frmJobEntry.LExecQuery("UPDATE jobs SET CARTONNUM = '" & dcCarton & "' WHERE BCODECONE = '" & txtReplaceCone.Text & "' ")




        Catch ex As Exception
            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try

        'Clean Up and return to jobentry


        defectCone = 0
        shortCone = 0
        Me.Close()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
    End Sub


    Private Sub frmExChangeCone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            If removeCone = 0 Then
                chkBcode = txtRemoveCone.Text
                removeCone = 1
                checkBcode()
            ElseIf replacementCone = 0 Then
                chkBcode = txtReplaceCone.Text
                replacementCone = 1
                checkBcode()
            End If

        End If

    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        UpdateDatabase()
    End Sub

    Private Sub txtWeight_TextChanged(sender As Object, e As EventArgs) Handles txtWeight.TextChanged
        btnContinue.Enabled = True
    End Sub
End Class