

Public Class frmPackRchkA



    Public packingActive = 0
    Public bcodeScan As String = ""
    Public toAllocatedCount As Integer 'count of cones requierd to be scanned
    Public allocatedCount As Integer 'count of cones scanned
    Public varCartEndTime As String
    Public packedFlag As Integer

    Public gradePackActive As Integer = 1
    'Index for DGV
    Dim gridRow As Integer = 0
    Dim gridCol As Integer = 1
    'INDEX FOR DGV1
    Dim dgv1gridRow As Integer = 0
    Dim dgv1gridCol As Integer = 0
    'Index for changing input location in DGV


    Dim coneCount As Integer = 0

    Dim dgvRows As Integer




    Private Sub frmPackRchkA_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        txtConeBcode.Focus()

        'Header Information
        Label2.Text = frmJobEntry.txtGrade.Text
        Label5.Text = frmJobEntry.varProductName
        Label6.Text = frmJobEntry.varProductCode


        For i = 2 To 9  'Columns to Hide
            DataGridView1.Columns(i).Visible = False
        Next

        'create rows 
        DataGridView1.Rows.Add(32)

        DataGridView1.RowHeadersVisible = False

        'NUMBER THE 90 CELLS
        For nums = 1 To 32


            DataGridView1.Rows(dgv1gridRow).Cells(dgv1gridCol).Value = nums
            dgv1gridRow = dgv1gridRow + 1

            'If dgv1gridRow = 31 And dgv1gridCol < 4 Then
            '    dgv1gridRow = 0
            '    dgv1gridCol = dgv1gridCol + 2
            'End If

        Next

        'SET FOCUS ON TO INPUT COLUMN IN DGV

        DataGridView1.CurrentCell = DataGridView1(1, 0)

        toAllocatedCount = frmDGV.DGVdata.Rows.Count

        lbltotCount.Text = toAllocatedCount

        Me.KeyPreview = True 'Allows us to look for advace character from barcode

        'THESE TWO LINES CONE SCANNED CHEESE FROM JOBENTRY IN TO THE FIRST ROW OF THE FORM AS WE KNOW IT IS THE CORRECT GRADE
        'txtConeBcode.Text = frmJobEntry.txtLotNumber.Text
        'prgContinue()



    End Sub





    Private Sub btnDefect_Click(sender As Object, e As EventArgs)
        Me.Hide()
        packingActive = 1

        frmPackingFault.Show()


    End Sub

    Public Sub prgContinue()



        dgvRows = toAllocatedCount


        bcodeScan = txtConeBcode.Text

        Dim fmt As String = "00"
        Dim modIdxNum As String
        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")




        For i = 1 To dgvRows


            If frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then

                'write to the local DGV grid
                DataGridView1.Rows(gridRow).Cells(gridCol).Value = frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value  'Write to Grid Cone Bcode
                DataGridView1.Rows(gridRow).Cells(gridCol).Style.BackColor = Color.LightGreen


                'Update DGV that Cheese has been alocated, update Packendtm
                frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value = DateAndTime.Today
                frmDGV.DGVdata.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.txtOperator.Text
                frmDGV.DGVdata.Rows(i - 1).Cells("OPNAME").Value = frmJobEntry.txtOperator.Text
                frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = "5"
                frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "15"
                packedFlag = 1


                Exit For
                'CHECK FOR ALREADY PACKED CHEESE
            ElseIf frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then
                Label8.Visible = True
                Label8.Text = "Cheese already allocated"
                Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
                DelayTM()
                Label8.Visible = False
                packedFlag = 0
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Me.KeyPreview = True 'Allows us to look for advace character from barcode
                Exit Sub

            ElseIf i - 1 = dgvRows - 1 And packedFlag = 0 Then    'CHECK FOR WRONG CHEESE ON CART
                'MsgBox("i = " & i - 1 & "Rows = " & dgvRows - 1)
                Label8.Visible = True
                Label8.Text = ("This is not a Grade " & frmJobEntry.txtGrade.Text & " Cheese")
                Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
                DelayTM()
                Me.Hide()
                Me.KeyPreview = True 'Allows us to look for advace character from barcode
                frmRemoveCone.Show()


                frmDGV.DGVdata.Rows(i - 1).Cells(58).Value = 1
                frmDGV.DGVdata.Rows(i - 1).Cells(55).Value = frmJobEntry.txtOperator.Text
                frmDGV.DGVdata.Rows(i - 1).Cells(32).Value = today

                Label8.Visible = False
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Exit Sub

            End If

        Next
        'UPDATE TOTAL COUNTED
        lbltotScan.Text = coneCount + 1


        'ROUTINE TO MOVE TO NEW COLUMN WHEN COLUMN IS FULL

        'If gridRow < 29 Then DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow + 1)

        '' gridRow = gridRow + 1
        'coneCount = coneCount + 1

        'If gridRow = 30 And gridCol < 5 Then
        '    gridRow = 0
        '    gridCol = gridCol + 2
        '    DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow)
        'End If


        'CHECK FOR END OF COUNT
        If coneCount <= 31 Then DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow + 1)

        gridRow = gridRow + 1
        coneCount = coneCount + 1


        If coneCount = 32 Or coneCount = toAllocatedCount Then jobEnd()



        packedFlag = 0




        txtConeBcode.Clear()
        txtConeBcode.Focus()







    End Sub




    Private Sub jobEnd()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Label8.Visible = True
        Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
        Label8.Text = ("Please wait creating packing Excel sheet")



        frmPackRepMain.PackRepMainSub()

        If frmPackTodayUpdate.prtError Then
            frmPackRepMain.Close()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Label8.Visible = False
            Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
            frmPackTodayUpdate.Close()
            gradePackActive = 0
            Me.Close()
            Exit Sub
        Else

            frmPackRepMain.Close()
            UpdateDatabase()
            Label8.Visible = False
            Me.Cursor = System.Windows.Forms.Cursors.Default
            gradePackActive = 0
            Me.Close()
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Clear()

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





    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ' TODO WRITE CURRENT SCANNED CONES TO THE PRINT FORM AND ALLOCATE PACKED VALUES TO THE DATABASE
        coneCount = 0
        toAllocatedCount = 0
        DataGridView1.ClearSelection()
        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        gradePackActive = 0
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
        gradePackActive = 0
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

    Private Sub btnDefect_Click_1(sender As Object, e As EventArgs) Handles btnDefect.Click
        Me.Hide()
        packingActive = 1
        frmPackingFault.Show()
    End Sub
End Class



