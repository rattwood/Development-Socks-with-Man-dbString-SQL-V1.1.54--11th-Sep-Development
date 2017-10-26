




Public Class frmB_AL_AD_W

    Public packingActive = 0
    Public bcodeScan As String = ""
    Public toAllocatedCount As Integer 'count of cones requierd to be scanned
    Public allocatedCount As Integer 'count of cones scanned
    Public varCartEndTime As String
    Public packedFlag As Integer
    'Index for DGV
    Dim gridRow As Integer = 0
    Dim gridCol As Integer = 0
    'INDEX FOR DGV1
    Dim dgv1gridRow As Integer = 0
    Dim dgv1gridCol As Integer = 1
    'Index for changing input location in DGV


    Dim coneCount As Integer = 0

    Dim dgvRows As Integer

    Private Sub frmB_AL_AD_W_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtConeBcode.Focus()

        'Header Information
        Label2.Text = frmJobEntry.txtGrade.Text
        Label5.Text = frmJobEntry.varProductName
        Label6.Text = frmJobEntry.varProductCode




        'create rows 
        DataGridView1.Rows.Add(30)

        For nums = 1 To 90


            DataGridView1.Rows(dgv1gridRow).Cells(dgv1gridCol).Value = nums
            dgv1gridRow = dgv1gridRow + 1

            If dgv1gridRow = 30 And dgv1gridCol < 5 Then
                dgv1gridRow = 0
                dgv1gridCol = dgv1gridCol + 2
            End If

        Next

        ' For i = 1 To dgvRows - 1
        'If frmDGV.DGVdata.Rows(i - 1).Cells(78).Value = "8" And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then
        'toAllocatedCount = toAllocatedCount + 1
        'End If
        ' Next

        toAllocatedCount = frmDGV.DGVdata.Rows.Count

        lbltotCount.Text = toAllocatedCount

        Me.KeyPreview = True 'Allows us to look for advace character from barcode


    End Sub

    Private Sub btnDefect_Click(sender As Object, e As EventArgs) Handles btnDefect.Click
        Me.Hide()
        packingActive = 1

        frmPackingFault.Show()


    End Sub






    'Private Sub txtConeBcode_TextChanged(sender As Object, e As EventArgs) Handles txtConeBcode.TextChanged

    Public Sub prgContinue()



        dgvRows = frmDGV.DGVdata.Rows.Count - 1


        bcodeScan = txtConeBcode.Text


        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")




        For i = 1 To dgvRows - 1

            'CHECK FOR UNPACKED CHEESE AND ALLOCATE
            If frmDGV.DGVdata.Rows(i - 1).Cells(36).Value = bcodeScan And IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then

                'write to the local DGV grid
                DataGridView1.Rows(gridRow).Cells(gridCol).Value = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value  'Write to Grid Cone Bcode
                DataGridView1.Rows(gridRow).Cells(gridCol).Style.BackColor = Color.LightGreen

                'Update DGV that Cheese has been alocated, update Packendtm
                frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value = DateAndTime.Today
                gridRow = gridRow + 1
                coneCount = coneCount + 1
                DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow)
                packedFlag = 1
                Exit For
                'CHECK FOR ALREADY PACKED CHEESE
            ElseIf frmDGV.DGVdata.Rows(i - 1).Cells(36).Value = bcodeScan And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then
                Label8.Visible = True
                Label8.Text = "Cheese already allocated"
                DelayTM()
                Label8.Visible = False
                packedFlag = 0
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Exit Sub
            ElseIf i = dgvRows - 1 And packedFlag = 0 Then    'CHECK FOR WRONG CHEESE ON CART
                Label8.Visible = True
                Label8.Text = ("This is not a Grade " & frmJobEntry.txtGrade.Text & " Cheese")
                DelayTM()
                Me.Hide()
                frmRemoveCone.Show()


                frmDGV.DGVdata.Rows(i - 1).Cells(58).Value = 1
                frmDGV.DGVdata.Rows(i - 1).Cells(55).Value = frmJobEntry.PackOp
                'frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "14"
                frmDGV.DGVdata.Rows(i - 1).Cells(32).Value = today

                Label8.Visible = False
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Exit Sub

            End If







        Next
        'UPDATE TOTAL COUNTED
        lbltotScan.Text = coneCount

        'Check if all cheeses or 90 have been scanned
        endCheck()

        If gridRow = 3 Then
            gridRow = 0
            gridCol = gridCol + 2
        End If

        packedFlag = 0


        'TURN DEBUG ON
        If My.Settings.debugSet Then
            Label12.Text = gridRow
            Label13.Text = gridCol
            Label14.Text = coneCount
        End If

        txtConeBcode.Clear()
        txtConeBcode.Focus()







    End Sub

    Public Sub endCheck()

        If coneCount = 9 Or coneCount = toAllocatedCount Then

            jobEnd()

        End If

    End Sub


    Private Sub jobEnd()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor



        frmPackRepMain.PackRepMainSub()
        frmPackRepMain.Close()
        'UpdateDatabase()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtTraceNum.Clear()
        frmJobEntry.txtTraceNum.Focus()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Close()

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





    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' TODO WRITE CURRENT SCANNED CONES TO THE PRINT FORM AND ALLOCATE PACKED VALUES TO THE DATABASE
        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Close()
    End Sub






    Private Sub UpdateDatabase()

        tsbtnSave()





        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        Try

            If frmJobEntry.LDS.HasChanges Then


                'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

                frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

            End If
        Catch ex As Exception

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try



        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        frmJobEntry.Show()
        Me.Close()



    End Sub

    Public Sub tsbtnSave()


        Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows
        'Dim iRow As Integer = frmDGV.DGVdata.CurrentRow.Index
        frmDGV.DGVdata.AllowUserToAddRows = True
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmDGV.DGVdata.AllowUserToAddRows = bAddState



    End Sub

    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            prgContinue()


        End If

    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click

        jobEnd()

    End Sub
End Class