




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





        Me.KeyPreview = True  'Allows us to look for advace character from barcode


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
        Dim curcone As String

        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")


        packedFlag = 0

        For i = 1 To dgvRows - 1


            If frmDGV.DGVdata.Rows(i - 1).Cells(36).Value = bcodeScan And IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then

                'write to the local DGV grid
                DataGridView1.Rows(gridRow).Cells(gridCol).Value = frmDGV.DGVdata.Rows(i - 1).Cells(36).Value  'Write to Grid Cone Bcode
                DataGridView1.Rows(gridRow).Cells(gridCol).Style.BackColor = Color.LightGreen

                'Update DGV that Cheese has been alocated, update Packendtm
                frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value = DateAndTime.Today
                gridRow = gridRow + 1
                coneCount = coneCount + 1
                DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow)
                txtConeBcode.Clear()
                txtConeBcode.Focus()

            ElseIf frmDGV.DGVdata.Rows(i - 1).Cells(36).Value = bcodeScan And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then
                Label8.Visible = True
                Label8.Text = "Cheese already allocated"
                DelayTM()
                Label8.Visible = False
                packedFlag = 0
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Exit Sub
            Else
                Label8.Visible = True
                Label8.Text = ("This is not a Grade " & frmJobEntry.txtGrade.Text & " Cheese")
                DelayTM()
                Label8.Visible = False
                txtConeBcode.Clear()
                txtConeBcode.Focus()
            End If

            If coneCount = 9 Then
                jobEnd()
            End If

            If gridRow = 3 Then
                gridRow = 0
                gridCol = gridCol + 2
            End If


            Label12.Text = gridRow
            Label13.Text = gridCol
            Label14.Text = coneCount
            'MsgBox("idxRow =" & idxRow & "  idxCol =" & idxCol & "  Conecount =" & coneCount)




            ' If frmDGV.DGVdata.Rows(i - 1).Cells(36).Value = bcodeScan And Not (frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value)
            ' Me.Hide()
            'frmRemoveCone.Show()

        Next


    End Sub

    Private Sub jobEnd()

        Me.Close()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()


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


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'frmPackReport.Hide()

    End Sub


    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Close()
    End Sub



    Public Sub endCheck()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If toAllocatedCount = allocatedCount Then

            'frmPackReport.packPrint() 'Print the packing report and go back to Job Entry for the next cart
            frmPackRepMain.PackRepMainSub()
            frmPackRepMain.Close()
            UpdateDatabase()

        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
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


End Class