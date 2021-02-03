Imports System.ComponentModel
Imports System.Data.SqlClient




Public Class frmHLCreate

    Public packingActive = 0
    Public bcodeScan As String = ""
    Public toAllocatedCount As Integer 'count of cones requierd to be scanned
    Public allocatedCount As Integer 'count of cones scanned

    Public packedFlag As Integer
    Dim stdChkNum As Integer = 0
    Dim reqstate As Integer = 0
    'Index for DGV
    Dim gridRow As Integer = 0
    Dim gridCol As Integer = 1
    'INDEX FOR DGV1
    Dim dgv1gridRow As Integer = 0
    Dim dgv1gridCol As Integer = 0
    'Index for changing input location in DGV

    Dim cartType As String = Nothing
    Dim coneCount As Integer = 0

    Dim dgvRows As Integer

    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError

    Private Sub frmHLCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtConeBcode.Focus()


        Select Case frmJobEntry.txtGrade.Text
            Case "Create H Cart"
                Label2.Text = "H"
                cartType = "H"
            Case "Create L Cart"
                Label2.Text = "L"
                cartType = "L"
        End Select

        'Header Information
        Label5.Text = frmJobEntry.varProductName
        Label6.Text = frmJobEntry.varProductCode




        'DEFINE FORM LAYOUT

        'create rows 
        DataGridView1.Rows.Add(32)
        DataGridView1.RowHeadersVisible = False

        'NUMBER THE 32 CELLS
        For nums = 1 To 32

            DataGridView1.Rows(dgv1gridRow).Cells(dgv1gridCol).Value = nums
            dgv1gridRow = dgv1gridRow + 1

        Next

        toAllocatedCount = frmDGV.DGVdata.Rows.Count

        lbltotCount.Text = toAllocatedCount

        Me.KeyPreview = True 'Allows us to look for advace character from barcode

        'THESE TWO LINES CONE SCANNED CHEESE FROM JOBENTRY IN TO THE FIRST ROW OF THE FORM AS WE KNOW IT IS THE CORRECT GRADE
        txtConeBcode.Text = frmJobEntry.txtLotNumber.Text
        prgContinue()

        If My.Settings.debugSet Then frmDGVTemp.Show()


    End Sub



    Public Sub prgContinue()



        dgvRows = toAllocatedCount


        bcodeScan = txtConeBcode.Text
        Dim cheeseOK As Integer = 0
        Dim fmt As String = "00"
        Dim modIdxNum As String
        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")




        For i = 1 To dgvRows

            'CHECK FOR UNPACKED CHEESE AND ALLOCATE


            If frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("RECHKIDX").Value) Then
                'write to the local DGV grid
                DataGridView1.Rows(gridRow).Cells(gridCol).Value = frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value  'Write to Grid Cone Bcode
                DataGridView1.Rows(gridRow).Cells(gridCol).Style.BackColor = Color.LightGreen

                Dim tmpNum As Integer = DataGridView1.Rows(gridRow).Cells(0).Value
                modIdxNum = tmpNum.ToString(fmt)
                frmDGV.DGVdata.Rows(i - 1).Cells("RECHKIDX").Value = modIdxNum
                frmDGV.DGVdata.Rows(i - 1).Cells("HHLLState").Value = 1



                If My.Settings.debugSet Then
                    Label9.Text = ("Row " & gridRow)
                    Label10.Text = ("Col " & gridCol)
                    Label11.Text = ("Grid count i =" & i)
                End If

                cheeseOK = 1

                Exit For

                'CHECK FOR ALREADY PACKED CHEESE
            ElseIf frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("RECHKIDX").Value) Then
                Label8.Visible = True
                Label8.Text = "This cheese has already been alocated"
                Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
                DelayTM()
                Label8.Visible = False
                cheeseOK = 0
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Me.KeyPreview = True 'Allows us to look for advace character from barcode
                Exit Sub

            ElseIf i = dgvRows Then 'This is not in the cheese list if we have gone through the whole list

                Label8.Visible = True
                Label8.Text = "This is not an '" & cartType & "' Grade cheese"
                Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
                DelayTM()
                Label8.Visible = False
                cheeseOK = 0
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Me.KeyPreview = True 'Allows us to look for advace character from barcode
                Exit Sub
            End If

        Next


        'UPDATE TOTAL COUNTED
        lbltotScan.Text = coneCount + 1


        'Increment count and Check if sheet is full and go to end routine
        If cheeseOK Then
            If coneCount < 31 Then DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow + 1)
            gridRow = gridRow + 1
            coneCount = coneCount + 1
            cheeseOK = 0
        End If



        If coneCount = 32 Or coneCount = toAllocatedCount Then saveScanYN()

        txtConeBcode.Clear()
        txtConeBcode.Focus()


    End Sub

    Private Sub saveScanYN()

        Dim result = MessageBox.Show("Do you wish to save this Job Yes or No", "Save Job Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            jobEnd()
        End If

        If result = DialogResult.No Then
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            frmJobEntry.Show()
            Me.Close()
            Exit Sub
        End If

    End Sub


    Private Sub jobEnd()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Label8.Visible = True
        Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
        Label8.Text = ("Please wait creating Cart " & cartType & " Excel sheet")


        '****************************** Routine to save database and then recall it from SQL and index on Recheck idx
        UpdateDatabase()


        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.DataSource = Nothing

        frmJobEntry.LAddParam("@gradetype", cartType)
        Select Case frmJobEntry.txtGrade.Text
            Case "Create H Cart"
                frmJobEntry.LExecQuery("Select * FROM Jobs Where HHLLState  = 1 And  PRNUM = '" & frmJobEntry.varProductCode & "' And HHLL = @gradetype  ORDER BY RECHKIDX ")
            Case "Create L Cart"
                frmJobEntry.LExecQuery("Select * FROM Jobs Where HHLLState  = 1 And  PRNUM = '" & frmJobEntry.varProductCode & "' And HHLL = @gradetype ORDER BY RECHKIDX ")

        End Select

        Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(frmJobEntry.LDA)

        If frmJobEntry.LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = frmJobEntry.LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True



            frmPackRepMain.PackRepMainSub()

        ElseIf frmPackTodayUpdate.prtError Then
            frmPackRepMain.Close()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Label8.Visible = False
            Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
            frmPackTodayUpdate.Close()
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            frmJobEntry.Show()
            Me.Close()
            Exit Sub
        End If

        'If all Ok update the database

        UpdateDatabase()
            frmPackRepMain.Close()
            Label8.Visible = False
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
            frmDGV.DGVdata.DataSource = Nothing
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            frmJobEntry.Show()
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

                frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

            End If
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("db Update Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("db Update Error", ex.ToString, False, "System Fault")

            MsgBox("Update Error: " & vbNewLine & ex.Message)
        End Try






    End Sub

    Public Sub tsbtnSave()


        Dim bAddState As Boolean = frmDGV.DGVdata.AllowUserToAddRows
        'Dim iRow As Integer = frmDGV.DGVdata.CurrentRow.Index
        frmDGV.DGVdata.AllowUserToAddRows = True
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(frmDGV.DGVdata.Rows.Count - 1).Cells(0) ' move to add row
        frmDGV.DGVdata.CurrentCell = frmDGV.DGVdata.Rows(0).Cells(0) ' move back to current row  Changed Rows(iRow) to (0)
        frmDGV.DGVdata.AllowUserToAddRows = bAddState
        'frmDGV.DGVdata.EndEdit()


    End Sub

    'THIS LOOKS FOR ENTER key to be pressed or received via barcode
    Private Sub frmHLCreate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            prgContinue()


        End If

    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click

        jobEnd()

    End Sub


End Class