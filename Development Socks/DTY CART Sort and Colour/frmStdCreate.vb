




Imports System.ComponentModel
Imports System.Data.SqlClient

Public Class frmStdCreate

    Public packingActive = 0
    Public bcodeScan As String = ""
    Public toAllocatedCount As Integer 'count of cones requierd to be scanned
    Public allocatedCount As Integer 'count of cones scanned
    Public varCartEndTime As String
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


    Dim coneCount As Integer = 0

    Dim dgvRows As Integer

    Private Sub frmStdCreate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtConeBcode.Focus()


        Select Case frmJobEntry.txtGrade.Text
            Case "Round1"
                Label2.Text = "1"
                stdChkNum = 1
                reqstate = 2
            Case "Round2"
                Label2.Text = "2"
                stdChkNum = 3
                reqstate = 4
            Case "Round3"
                Label2.Text = "3"
                stdChkNum = 5
                reqstate = 6
            Case "STD"
                Label2.Text = "Final"
                stdChkNum = 7
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


            If frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = stdChkNum Then
                'write to the local DGV grid
                DataGridView1.Rows(gridRow).Cells(gridCol).Value = frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value  'Write to Grid Cone Bcode
                DataGridView1.Rows(gridRow).Cells(gridCol).Style.BackColor = Color.LightGreen

                Dim tmpNum As Integer = DataGridView1.Rows(gridRow).Cells(0).Value
                modIdxNum = tmpNum.ToString(fmt)
                frmDGV.DGVdata.Rows(i - 1).Cells("RECHKIDX").Value = modIdxNum


                Select Case frmJobEntry.txtGrade.Text
                    Case "Round1"
                        frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 25            ' CREATE 1st Round  SHEET 1&2
                    Case "Round2"
                        frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 45            ' CREATE 2nd Round SHEET 1
                    Case "Round3"
                        frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 65            ' CREATE 3rd Round SHEET 1
                    Case "STD"
                        frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 75            'CREATE Final Sheet
                End Select

                If My.Settings.debugSet Then
                    Label9.Text = ("Row " & gridRow)
                    Label10.Text = ("Col " & gridCol)
                    Label11.Text = ("Grid count i =" & i)
                End If

                cheeseOK = 1

                Exit For

                'CHECK FOR ALREADY PACKED CHEESE
            ElseIf frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value > stdChkNum Then
                Label8.Visible = True
                Label8.Text = "Cheese already allocated"
                Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
                DelayTM()
                Label8.Visible = False
                cheeseOK = 0
                txtConeBcode.Clear()
                txtConeBcode.Focus()
                Me.KeyPreview = True 'Allows us to look for advace character from barcode
                Exit Sub
                'CHECK FOR WRONG CHEESE SCANNED
            ElseIf Not frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And i - 1 = dgvRows - 1 Then
                Label8.Visible = True
                Label8.Text = "This is not a STD Cheese"
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



        If coneCount = 32 Or coneCount = toAllocatedCount Then jobEnd()

        txtConeBcode.Clear()
        txtConeBcode.Focus()


    End Sub




    Private Sub jobEnd()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Label8.Visible = True
        Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
        Label8.Text = ("Please wait creating packing Excel sheet")


        '****************************** Routine to save database and then recall it from SQL and index on Recheck idx
        UpdateDatabase()

        'frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns("RECHKIDX"), ListSortDirection.Ascending)  'sorts On cone number
        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()

        Select Case frmJobEntry.txtGrade.Text
            Case "Round1"
                frmJobEntry.LExecQuery("Select * FROM Jobs Where Stdstate  = 25 And  PRNUM = '" & frmJobEntry.varProductCode & "' And PRYY = '" & frmJobEntry.year & "' And PRMM = '" & frmJobEntry.month & "'  ORDER BY RECHKIDX ")
            Case "Round2"
                frmJobEntry.LExecQuery("Select * FROM Jobs Where Stdstate  = 45 And  PRNUM = '" & frmJobEntry.varProductCode & "' And PRYY = '" & frmJobEntry.year & "' And PRMM = '" & frmJobEntry.month & "' ORDER BY RECHKIDX ")
            Case "Round3"
                frmJobEntry.LExecQuery("Select * FROM Jobs Where Stdstate  = 65 And  PRNUM = '" & frmJobEntry.varProductCode & "' And PRYY = '" & frmJobEntry.year & "' And PRMM = '" & frmJobEntry.month & "' ORDER BY RECHKIDX ")
            Case "STD"
                frmJobEntry.LExecQuery("Select * FROM Jobs Where Stdstate  = 75 And  PRNUM = '" & frmJobEntry.varProductCode & "' And PRYY = '" & frmJobEntry.year & "' And PRMM = '" & frmJobEntry.month & "' ORDER BY RECHKIDX ")
        End Select

        Dim LCB As SqlCommandBuilder = New SqlCommandBuilder(frmJobEntry.LDA)

        If frmJobEntry.LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = frmJobEntry.LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True



            Select Case frmJobEntry.txtGrade.Text
                Case "Round1"
                    For i = 1 To frmJobEntry.LRecordCount
                        frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 2
                    Next
                Case "Round2"
                    For i = 1 To frmJobEntry.LRecordCount
                        frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 4
                    Next
                Case "Round3"
                    For i = 1 To frmJobEntry.LRecordCount
                        frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 6
                    Next
                Case "STD"
                    For i = 1 To frmJobEntry.LRecordCount
                        frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 7
                    Next
            End Select

        End If





        frmPackRepMain.PackRepMainSub()
        UpdateDatabase()


        If frmPackTodayUpdate.prtError Then
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
        Else
            UpdateDatabase()
            'frmPackRepMain.Close()
            Label8.Visible = False
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
            frmDGV.DGVdata.ClearSelection()
            frmJobEntry.txtLotNumber.Clear()
            frmJobEntry.txtLotNumber.Focus()
            frmJobEntry.Show()
            Me.Close()

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



        'If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        'frmDGV.DGVdata.ClearSelection()
        'frmJobEntry.txtLotNumber.Clear()
        'frmJobEntry.txtLotNumber.Focus()
        'frmJobEntry.Show()
        'Me.Close()



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
    Private Sub frmJobEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            prgContinue()


        End If

    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click

        jobEnd()

    End Sub


End Class