Imports System.Data.SqlClient

Public Class frmB_AL_AD_W

    'GIVES ACCESS TO GLOBAL SQL CLASS
    Private SQLL As New SQLConn

    Public packingActive = 0
    Public bcodeScan As String = ""
    Public toAllocatedCount As Integer 'count of cones requierd to be scanned
    Public allocatedCount As Integer 'count of cones scanned

    'Public varCartEndTime As String
    Public packedFlag As Integer
    Public gradePackActive As Integer = 1

    'Index for DGV
    Dim gridRow As Integer = 0
    Dim gridCol As Integer = 1

    'INDEX FOR DGV1
    Dim dgv1gridRow As Integer = 0
    Dim dgv1gridCol As Integer = 0

    'Index for changing input location in DGV
    Dim tmpNum As Integer
    Dim coneCount As Integer = 0
    Dim dgvRows As Integer
    Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height
    Dim pauseScan As Integer = 0  'Stop barcode entry when 0

    'Create an array to get a list of the scanned cheeses
    Dim conelist(200) As String

    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError



    Private Sub frmB_AL_AD_W_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtConeBcode.Focus()

        'Header Information
        Label2.Text = frmJobEntry.txtGrade.Text
        Label5.Text = frmJobEntry.varProductName
        Label6.Text = frmJobEntry.varProductCode

        'CHECK SCREEN SIZE AND ADJUST VEIW
        If screenHeight <= 770 Then Me.WindowState = FormWindowState.Maximized Else Me.WindowState = FormWindowState.Normal

        Select Case frmJobEntry.txtGrade.Text
            Case "B", "AL", "AD", "P35 AS", "P35 BS"

                SetupDGV()

                For i = 6 To 9  'Columns to Hide
                    DataGridView1.Columns(i).Visible = False
                Next

                'create rows 
                DataGridView1.Rows.Add(30)

                DataGridView1.RowHeadersVisible = False

                'NUMBER THE 90 CELLS
                For nums = 1 To 90


                    DataGridView1.Rows(dgv1gridRow).Cells(dgv1gridCol).Value = nums
                    dgv1gridRow = dgv1gridRow + 1

                    If dgv1gridRow = 30 And dgv1gridCol < 4 Then
                        dgv1gridRow = 0
                        dgv1gridCol = dgv1gridCol + 2
                    End If

                Next


            Case "P25 AS", "P30 BS"

                SetupDGV()

                For i = 8 To 9   'Columns to Hide
                    DataGridView1.Columns(i).Visible = False
                Next

                'create rows 
                DataGridView1.Rows.Add(30)

                DataGridView1.RowHeadersVisible = False

                'NUMBER THE 120 CELLS
                For nums = 1 To 120


                    DataGridView1.Rows(dgv1gridRow).Cells(dgv1gridCol).Value = nums
                    dgv1gridRow = dgv1gridRow + 1

                    If dgv1gridRow = 30 And dgv1gridCol < 6 Then
                        dgv1gridRow = 0
                        dgv1gridCol = dgv1gridCol + 2
                    End If

                Next

            Case "P15 AS", "P20 BS"
                SetupDGV()

                'create rows 
                DataGridView1.Rows.Add(39)
                DataGridView1.RowHeadersVisible = False

                'NUMBER THE 195 CELLS
                For nums = 1 To 195


                    DataGridView1.Rows(dgv1gridRow).Cells(dgv1gridCol).Value = nums
                    dgv1gridRow = dgv1gridRow + 1

                    If dgv1gridRow = 39 And dgv1gridCol < 8 Then
                        dgv1gridRow = 0
                        dgv1gridCol = dgv1gridCol + 2
                    End If

                Next
            Case "ReCheck"


                'Dim anteWidth As Integer = Me.Width
                'Dim anteHeight As Integer = Me.Height
                'Dim screenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
                'Dim screenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height
                'Dim WidthRatio As Integer = screenWidth / anteWidth
                'Dim HeightRatio As Integer = screenHeight / anteHeight

                ' MsgBox("Height = " & screenHeight & vbCrLf & "width = " & screenWidth & vbCrLf & "window Hight = " & anteHeight & vbCrLf & "window Width = " & anteWidth)

                SetupDGV()


                For i = 4 To 9   'Columns to Hide
                    DataGridView1.Columns(i).Visible = False
                Next

                'create rows 
                DataGridView1.Rows.Add(16)
                DataGridView1.RowHeadersVisible = False

                'NUMBER THE 32 CELLS
                For nums = 1 To 32


                    DataGridView1.Rows(dgv1gridRow).Cells(dgv1gridCol).Value = nums
                    dgv1gridRow = dgv1gridRow + 1

                    If dgv1gridRow = 16 And dgv1gridCol < 4 Then
                        dgv1gridRow = 0
                        dgv1gridCol = dgv1gridCol + 2
                    End If


                Next
            Case "Waste"
                MsgBox("not written yet")

        End Select



        toAllocatedCount = frmDGV.DGVdata.Rows.Count

        lbltotCount.Text = toAllocatedCount

        Me.KeyPreview = True 'Allows us to look for advace character from barcode

        'THESE TWO LINES CONE SCANNED CHEESE FROM JOBENTRY IN TO THE FIRST ROW OF THE FORM AS WE KNOW IT IS THE CORRECT GRADE

        If My.Settings.debugSet Then frmDGV.Show()

        txtConeBcode.Text = frmJobEntry.txtLotNumber.Text
        If My.Settings.chkUseSort Then btnDefect.Visible = False
        prgContinue()



    End Sub

    Private Sub SetupDGV()

        'CHECK SCREEN HEIGHT AND ADJUST TO MAXIMIZED IF TO SMALL
        If screenHeight <= 770 Then Me.WindowState = FormWindowState.Maximized Else Me.WindowState = FormWindowState.Normal

        Select Case frmJobEntry.txtGrade.Text

            Case "B", "AL", "AD", "P35 AS", "P35 BS"

                With DataGridView1.ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Microsoft Sans Serif", 18, FontStyle.Bold)

                End With

                With DataGridView1
                    .Name = " DataGridView1"
                    '.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .RowTemplate.Height = 21.5
                    .BorderStyle = BorderStyle.Fixed3D


                    ' .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
                    .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
                    .CellBorderStyle = DataGridViewCellBorderStyle.Single
                    .GridColor = Color.Black

                    .RowHeadersVisible = False


                    .Columns(0).DefaultCellStyle.Font =
                   New Font("Microsoft Sans Serif", 18, FontStyle.Bold)
                    .Columns(0).Width = 75
                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(1).DefaultCellStyle.Font =
                    New Font("Microsoft Sans Serif", 18, FontStyle.Bold)
                    .Columns(1).Width = 284
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(2).DefaultCellStyle.Font =
                   New Font("Microsoft Sans Serif", 18, FontStyle.Bold)
                    .Columns(2).Width = 75
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(3).DefaultCellStyle.Font =
                    New Font("Microsoft Sans Serif", 18, FontStyle.Bold)
                    .Columns(3).Width = 284
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(4).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 18, FontStyle.Bold)
                    .Columns(4).Width = 75
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(5).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 18, FontStyle.Bold)
                    .Columns(5).Width = 284
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter


                End With

            Case "P25 AS", "P30 BS"

                With DataGridView1.ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Microsoft Sans Serif", 17, FontStyle.Bold)

                End With

                With DataGridView1
                    .Name = " DataGridView1"
                    '.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .RowTemplate.Height = 21.8
                    .BorderStyle = BorderStyle.Fixed3D


                    ' .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
                    .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
                    .CellBorderStyle = DataGridViewCellBorderStyle.Single
                    .GridColor = Color.Black

                    .RowHeadersVisible = False


                    .Columns(0).DefaultCellStyle.Font =
                   New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                    .Columns(0).Width = 60
                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(1).DefaultCellStyle.Font =
                    New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                    .Columns(1).Width = 209
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(2).DefaultCellStyle.Font =
                   New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                    .Columns(2).Width = 60
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(3).DefaultCellStyle.Font =
                    New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                    .Columns(3).Width = 209
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(4).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                    .Columns(4).Width = 60
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(5).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                    .Columns(5).Width = 209
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(6).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                    .Columns(6).Width = 60
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(7).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 14, FontStyle.Bold)
                    .Columns(7).Width = 209
                    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter




                End With





            Case "P15 AS", "P20 BS"

                With DataGridView1.ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)

                End With

                With DataGridView1
                    .Name = " DataGridView1"
                    '.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .RowTemplate.Height = 16.51

                    .BorderStyle = BorderStyle.Fixed3D


                    ' .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
                    .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
                    .CellBorderStyle = DataGridViewCellBorderStyle.Single
                    .GridColor = Color.Black

                    .RowHeadersVisible = False


                    .Columns(0).DefaultCellStyle.Font =
                   New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(0).Width = 51
                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                    .Columns(1).DefaultCellStyle.Font =
                    New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(1).Width = 165
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                    .Columns(2).DefaultCellStyle.Font =
                   New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(2).Width = 51
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                    .Columns(3).DefaultCellStyle.Font =
                    New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(3).Width = 165
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                    .Columns(4).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(4).Width = 51
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                    .Columns(5).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(5).Width = 165
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                    .Columns(6).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(6).Width = 51
                    .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                    .Columns(7).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(7).Width = 165
                    .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                    .Columns(8).DefaultCellStyle.Font =
               New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(8).Width = 51
                    .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                    .Columns(9).DefaultCellStyle.Font =
                  New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    .Columns(9).Width = 165
                    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Columns(9).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft


                End With





            Case "ReCheck"

                With DataGridView1.ColumnHeadersDefaultCellStyle
                    .BackColor = Color.Navy
                    .ForeColor = Color.White
                    .Font = New Font("Microsoft Sans Serif", 20, FontStyle.Bold)

                End With




                With DataGridView1
                    .Name = " DataGridView1"
                    '.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                    .RowTemplate.Height = 41
                    .BorderStyle = BorderStyle.Fixed3D


                    ' .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
                    '.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised
                    .CellBorderStyle = DataGridViewCellBorderStyle.Single
                    .GridColor = Color.Black
                    .RowHeadersVisible = False


                    .Columns(0).DefaultCellStyle.Font =
               New Font("Microsoft Sans Serif", 20, FontStyle.Bold)
                    .Columns(0).Width = 100
                    .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(1).DefaultCellStyle.Font =
                New Font("Microsoft Sans Serif", 20, FontStyle.Bold)
                    .Columns(1).Width = 450
                    .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(2).DefaultCellStyle.Font =
               New Font("Microsoft Sans Serif", 20, FontStyle.Bold)
                    .Columns(2).Width = 100
                    .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                    .Columns(3).DefaultCellStyle.Font =
                New Font("Microsoft Sans Serif", 20, FontStyle.Bold)
                    .Columns(3).Width = 400
                    .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter


                End With











        End Select












    End Sub





    Private Sub btnDefect_Click(sender As Object, e As EventArgs) Handles btnDefect.Click
        Me.Hide()
        packingActive = 1

        frmPackingFault.Show()


    End Sub



    'Private Sub txtConeBcode_TextChanged(sender As Object, e As EventArgs) Handles txtConeBcode.TextChanged

    Public Sub prgContinue()



        dgvRows = toAllocatedCount

        If txtConeBcode.TextLength <> 15 Then
            Label8.Visible = True
            Label8.Text = "BARCODE ERROR not a cheese BARCODE"
            DelayTM()
            Label8.Visible = False
            Exit Sub
        End If




        bcodeScan = txtConeBcode.Text



        Dim fmt As String = "00"
        Dim modIdxNum As String
        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")


        Try

            For i = 1 To dgvRows

                'CHECK FOR UNPACKED CHEESE AND ALLOCATE
                'If frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And frmDGV.DGVdata.Rows(i - 1).Cells(33).Value = 0 And IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) Then
                If Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value) Then
                    If frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = "" Then frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = Nothing
                End If

                If frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) And frmJobEntry.txtGrade.Text <> "ReCheck" Or
                frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) And frmJobEntry.txtGrade.Text = "ReCheck" And
                IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value) Then
                    'write to the local DGV grid
                    DataGridView1.Rows(gridRow).Cells(gridCol).Value = frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value

                    'new part to get list of scanned cheeses in to an array which we will use for the database update
                    Dim arrayLen As Integer

                    For Each element As String In conelist
                        If element > 0 Then arrayLen = arrayLen + 1
                    Next




                    If arrayLen = 0 Then
                        conelist(arrayLen) = frmDGV.DGVdata.Rows(i - 1).Cells("id_product").Value
                    Else
                        conelist(arrayLen) = frmDGV.DGVdata.Rows(i - 1).Cells("id_product").Value
                    End If


                    If My.Settings.debugSet Then ListBox1.Visible = True

                    ListBox1.Items.Clear()
                    If arrayLen = 0 Then
                        ListBox1.Items.Add(conelist(0).ToString)
                    Else

                        For y = 0 To arrayLen
                            ListBox1.Items.Add(conelist(y).ToString)
                        Next

                    End If




                    'Write to Grid Cone Bcode
                    ' DataGridView1.Rows(gridRow).Cells(gridCol).Style.BackColor = Color.LightGreen






                    If frmJobEntry.txtGrade.Text = "ReCheck" And frmJobEntry.stdReChk = 0 Then  'IF RECHK THEN SET FLAG=1 SET TIME AND SET NUBER 1-32
                        frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = 1
                        frmDGV.DGVdata.Rows(i - 1).Cells("RECHKSTARTTM").Value = DateAndTime.Today
                        frmDGV.DGVdata.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.txtOperator.Text
                        frmDGV.DGVdata.Rows(i - 1).Cells("OPNAME").Value = frmJobEntry.txtOperator.Text
                        '************************************************************************************************************
                        ' routine to get index count from second column
                        If gridCol = 1 Then
                            tmpNum = DataGridView1.Rows(gridRow).Cells(0).Value  'format first 9 cheese to have leading Zero before sending to db
                            modIdxNum = tmpNum.ToString(fmt)
                        Else
                            tmpNum = DataGridView1.Rows(gridRow).Cells(2).Value  'format first 9 cheese to have leading Zero before sending to db
                            modIdxNum = tmpNum.ToString(fmt)
                        End If
                        '**************************************************************************************************************
                        frmDGV.DGVdata.Rows(i - 1).Cells("RECHKIDX").Value = modIdxNum
                    ElseIf frmJobEntry.txtGrade.Text = "ReCheck" And frmJobEntry.stdReChk Then
                        frmDGV.DGVdata.Rows(i - 1).Cells("STDSTATE").Value = 11
                        frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = 1
                        frmDGV.DGVdata.Rows(i - 1).Cells("RECHKSTARTTM").Value = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")
                        frmDGV.DGVdata.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.txtOperator.Text
                        frmDGV.DGVdata.Rows(i - 1).Cells("OPNAME").Value = frmJobEntry.txtOperator.Text

                        tmpNum = DataGridView1.Rows(gridRow).Cells(0).Value  'format first 9 cheese to have leading Zero before sending to db
                        modIdxNum = tmpNum.ToString(fmt)
                        frmDGV.DGVdata.Rows(i - 1).Cells("RECHKIDX").Value = modIdxNum
                    Else
                        'ROUTINE TO CREATE INDEX OF SCAN ORDER OF CHEESE FOR PRINTING IN SAME ORDER
                        tmpNum = DataGridView1.Rows(gridRow).Cells(gridCol - 1).Value  'GET SHEET CHEESE POSITION NUMBER 
                        modIdxNum = tmpNum.ToString(fmt)
                        frmDGV.DGVdata.Rows(i - 1).Cells("PACKIDX").Value = modIdxNum
                        'Update DGV that Cheese has been alocated, update Packendtm
                        frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value = DateAndTime.Now.ToString("yyyy-MMM-dd HH:mm:ss")
                        frmDGV.DGVdata.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.txtOperator.Text
                        frmDGV.DGVdata.Rows(i - 1).Cells("OPNAME").Value = frmJobEntry.txtOperator.Text
                    End If

                    'Write to Grid Cone Bcode
                    DataGridView1.Rows(gridRow).Cells(gridCol).Style.BackColor = Color.LightGreen

                    packedFlag = 1


                    Exit For
                    'CHECK FOR ALREADY PACKED CHEESE
                ElseIf frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("PACKENDTM").Value) And Not frmJobEntry.txtGrade.Text = "ReCheck" Then
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
                ElseIf frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And Not IsDBNull(frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value) And frmJobEntry.txtGrade.Text = "ReCheck" Then
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
                ElseIf frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value <> bcodeScan And i = dgvrows And packedFlag = 0 Then


                    frmRemoveCone.Show()


                    frmDGV.DGVdata.Rows(i - 1).Cells("PSORTERROR").Value = 1
                    frmDGV.DGVdata.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.txtOperator.Text
                    frmDGV.DGVdata.Rows(i - 1).Cells("CARTENDTM").Value = today

                    Label8.Visible = False
                    txtConeBcode.Clear()
                    txtConeBcode.Focus()
                    Exit Sub

                End If

            Next
        Catch ex As Exception
            'Write error to Log File
            Dim errorDetail As String

            errorDetail = "Operator " & frmJobEntry.txtOperator.Text & "Barcode " & bcodeScan & "Computer " & bcodeScan & System.Environment.MachineName

            writeerrorLog.writelog("Scan Error", errorDetail, False, "System Fault")
            writeerrorLog.writelog("Scan Error", ex.ToString, False, "System Fault")
            writeerrorLog.writelog("Scan Detail", ex.ToString, False, "System Fault")
            Label8.Visible = False
            txtConeBcode.Clear()
            txtConeBcode.Focus()
            Exit Sub
        End Try



        'UPDATE TOTAL COUNTED
        lbltotScan.Text = coneCount + 1


        'ROUTINE TO MOVE TO NEW COLUMN WHEN COLUMN IS FULL




        Select Case frmJobEntry.txtGrade.Text




            Case "B", "AL", "AD", "P35 AS", "P35 BS"

                If gridRow < 29 Then DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow + 1)

                gridRow = gridRow + 1
                coneCount = coneCount + 1

                If gridRow = 30 And gridCol < 5 Then
                    gridRow = 0
                    gridCol = gridCol + 2
                    DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow)
                End If

                If coneCount = 90 Or coneCount = toAllocatedCount Then jobEnd()

            Case "P25 AS", "P30 BS"

                If gridRow < 29 Then DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow + 1)

                gridRow = gridRow + 1
                coneCount = coneCount + 1

                If gridRow = 30 And gridCol < 7 Then
                    gridRow = 0
                    gridCol = gridCol + 2
                    DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow)
                End If

                If coneCount = 120 Or coneCount = toAllocatedCount Then jobEnd()

            Case "P15 AS", "P20 BS"


                If gridRow < 38 Then DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow + 1)

                gridRow = gridRow + 1
                coneCount = coneCount + 1

                If gridRow = 39 And gridCol < 9 Then
                    gridRow = 0
                    gridCol = gridCol + 2
                    DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow)
                End If

                If coneCount = 195 Or coneCount = toAllocatedCount Then jobEnd()

            Case "ReCheck"

                If gridRow < 15 Then DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow + 1)

                gridRow = gridRow + 1
                coneCount = coneCount + 1

                If gridRow = 16 And gridCol < 3 Then
                    gridRow = 0
                    gridCol = gridCol + 2
                    DataGridView1.CurrentCell = DataGridView1(gridCol, gridRow)
                End If


                If coneCount = 32 Or coneCount = toAllocatedCount Then jobEnd()

        End Select





        packedFlag = 0




        txtConeBcode.Clear()
        txtConeBcode.Focus()







    End Sub




    Private Sub jobEnd()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Label8.Visible = True

        pauseScan = 1 'Stop Barcode entry
        Label8.Text = ("Please wait creating packing Excel sheet")

        Try
            'Change the number to the column index that you want to sort
            If frmJobEntry.txtGrade.Text IsNot "ReCheck" Then
                frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns("PACKIDX"), System.ComponentModel.ListSortDirection.Ascending)
            End If


            frmPackRepMain.PackRepMainSub()


            'frmDGV.DGVdata.Sort(frmDGV.DGVdata.Columns(0), System.ComponentModel.ListSortDirection.Ascending)  ' Is this needed ?


            If frmPackTodayUpdate.prtError Then
                frmPackRepMain.Close()
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Label8.Visible = False
                Me.KeyPreview = False 'Turns off BARCODE INPUT WHILE ERROR MESSAGE
                frmPackTodayUpdate.Close()
                MsgBox("Error, could not create print sheet." & vbCrLf & "Please press Finish Again")
                gradePackActive = 0
                pauseScan = 0 'Stop Barcode entry
                Me.Close()
                Exit Sub
            Else

                frmPackRepMain.Close()
                ' UpdateDatabase()
                Label8.Visible = False
                Me.Cursor = System.Windows.Forms.Cursors.Default
                gradePackActive = 0
                Me.Close()
                frmJobEntry.Show()
                frmJobEntry.txtLotNumber.Clear()

            End If
        Catch ex As Exception
            'Write error to Log File
            Dim errorDetail As String
            errorDetail = "Operator " & frmJobEntry.txtOperator.Text & "Computer " & System.Environment.MachineName

            writeerrorLog.writelog("Error during Print", errorDetail, False, "System Fault")
            writeerrorLog.writelog("Error during Print", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Error during Print", ex.ToString, False, "System Fault")
            Me.Close()
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Clear()
        End Try



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
        gradePackActive = 0
        Me.Close()
    End Sub






    Private Sub UpdateDatabase()

        pauseScan = 1 'Stop Barcode entry

        'tsbtnSave()

        ''New save to SQL routine
        Dim arrayLen As Integer 'used to store the count of entries in the array

        For Each element As String In conelist
            If element > 0 Then arrayLen = arrayLen + 1
        Next

        'If arrayLen = 0 Then
        '    ListBox1.Items.Add(conelist(0).ToString)
        'Else

        'For y = 0 To arrayLen
        '        ListBox1.Items.Add(conelist(y).ToString)
        '    Next

        ' End If
        Try
            Dim rcount As Integer

            For i = 1 To arrayLen



                SQLL.AddParam("@id", conelist(i - 1).ToString) 'frmDGV.DGVdata.Rows(i - 1).Cells("id_Product").Value)
                Dim id_val As String = (conelist(i - 1).ToString)
                'We must find correct row in dgv to get data for parameters

                For x = 1 To dgvRows
                    If frmDGV.DGVdata.Rows(x - 1).Cells("id_product").Value = id_val Then
                        rcount = x - 1
                    End If
                Next


                SQLL.AddParam("@opname", frmDGV.DGVdata.Rows(rcount).Cells("OpName").Value)
                SQLL.AddParam("@conestate", frmDGV.DGVdata.Rows(rcount).Cells("conestate").Value)
                SQLL.AddParam("@shortcone", frmDGV.DGVdata.Rows(rcount).Cells("SHORTCONE").Value)
                SQLL.AddParam("@defcone", frmDGV.DGVdata.Rows(rcount).Cells("DEFCONE").Value)
                SQLL.AddParam("@cartendtm", frmDGV.DGVdata.Rows(rcount).Cells("CartEndTm").Value)
                SQLL.AddParam("@rechk", frmDGV.DGVdata.Rows(rcount).Cells("RECHK").Value)
                SQLL.AddParam("@flt_k", frmDGV.DGVdata.Rows(rcount).Cells("FLT_K").Value)
                SQLL.AddParam("@flt_d", frmDGV.DGVdata.Rows(rcount).Cells("FLT_D").Value)
                SQLL.AddParam("@flt_f", frmDGV.DGVdata.Rows(rcount).Cells("FLT_F").Value)
                SQLL.AddParam("@flt_o", frmDGV.DGVdata.Rows(rcount).Cells("FLT_O").Value)
                SQLL.AddParam("@flt_t", frmDGV.DGVdata.Rows(rcount).Cells("FLT_T").Value)
                SQLL.AddParam("@flt_p", frmDGV.DGVdata.Rows(rcount).Cells("FLT_P").Value)
                SQLL.AddParam("@flt_s", frmDGV.DGVdata.Rows(rcount).Cells("FLT_S").Value)
                SQLL.AddParam("@flt_n", frmDGV.DGVdata.Rows(rcount).Cells("FLT_N").Value)
                SQLL.AddParam("@flt_w", frmDGV.DGVdata.Rows(rcount).Cells("FLT_W").Value)
                SQLL.AddParam("@flt_h", frmDGV.DGVdata.Rows(rcount).Cells("FLT_H").Value)
                SQLL.AddParam("@flt_tr", frmDGV.DGVdata.Rows(rcount).Cells("FLT_TR").Value)
                SQLL.AddParam("@flt_b", frmDGV.DGVdata.Rows(rcount).Cells("FLT_B").Value)
                SQLL.AddParam("@flt_c", frmDGV.DGVdata.Rows(rcount).Cells("FLT_C").Value)
                SQLL.AddParam("@oppack", frmDGV.DGVdata.Rows(rcount).Cells("OpPack").Value)
                SQLL.AddParam("@psorterror", frmDGV.DGVdata.Rows(rcount).Cells("PSORTERROR").Value)
                SQLL.AddParam("@packendtm", frmDGV.DGVdata.Rows(rcount).Cells("Packendtm").Value)
                SQLL.AddParam("@packsheet", frmDGV.DGVdata.Rows(rcount).Cells("PACKSHEETBCODE").Value)
                SQLL.AddParam("@carton", frmDGV.DGVdata.Rows(rcount).Cells("CARTONNUM").Value)
                SQLL.AddParam("@packidx", frmDGV.DGVdata.Rows(rcount).Cells("PACKIDX").Value)
                SQLL.AddParam("@rechkidx", frmDGV.DGVdata.Rows(rcount).Cells("RECHKIDX").Value)
                SQLL.AddParam("@recheckbarcode", frmDGV.DGVdata.Rows(rcount).Cells("RECHECKBARCODE").Value)
                SQLL.AddParam("@rechkstarttm", frmDGV.DGVdata.Rows(rcount).Cells("RECHKSTARTTM").Value)
                SQLL.AddParam("@rechkendtm", frmDGV.DGVdata.Rows(rcount).Cells("RECHKENDTM").Value)
                SQLL.AddParam("@stdstate", frmDGV.DGVdata.Rows(rcount).Cells("STDSTATE").Value)

                '   MsgBox("ID = " & @id.tostring)

                SQLL.ExecQuery(" Update jobs set CONESTATE = @conestate, OPPACK = @oppack, OPNAME = @opname, PACKENDTM = @packendtm, " _
                            & "SHORTCONE = @shortcone, DEFCONE = @defcone," _
                            & "FLT_K =  @flt_k, FLT_D = @flt_d, FLT_F = @flt_f, FLT_O = @flt_o, FLT_T = @flt_t, FLT_P = @flt_p, " _
                            & "FLT_S = @flt_s, FLT_N = @flt_n, FLT_W = @flt_w, FLT_H = @flt_h, FLT_TR = @flt_tr, FLT_B = @flt_b,FLT_C = @flt_c, " _
                            & "PSORTERROR = @psorterror, CARTENDTM = @cartendtm,RECHK = @rechk,PACKSHEETBCODE = @packsheet, CARTONNUM = @carton, PACKIDX = @packidx, " _
                            & "RECHKIDX = @rechkidx, RECHECKBARCODE = @recheckbarcode, RECHKSTARTTM = @rechkstarttm, RECHKENDTM = rechkendtm, STDSTATE = @stdstate  " _
                            & "Where id_product = @id")



            Next

        Catch dbcx As DBConcurrencyException
            Dim Response As String

            Response = dbcx.Row.ToString
            writeerrorLog.writelog("db B_AL_AD_W Con Error", Response, False, "B_AL_AD_Pk Con Fault")
            Response = dbcx.RowCount.ToString
            writeerrorLog.writelog("db B_AL_AD_W_Pk Con Error", Response, False, "B_AL_AD_Pk Con Fault")




        Catch ex As Exception
            'Write error to Log File
            Dim ErrorMsg As String = "Computer " & System.Environment.MachineName
            writeerrorLog.writelog("db B_AL_AD_W Update Error", ErrorMsg, False, "System Fault")
            writeerrorLog.writelog("db B_AL_AD_W Update Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("db B_AL_AD_W Update Error", ex.ToString, False, "System Fault")

            MsgBox("Update Error: " & vbNewLine & ex.Message)
            pauseScan = 0 'Allow barcode entry
        End Try




        ''Save ReCheck details
        'If frmJobEntry.txtGrade.Text = "ReCheck" And frmJobEntry.stdReChk = 0 Then


        'End If


        ''Save details for stdrecheck
        'If frmJobEntry.txtGrade.Text = "ReCheck" And frmJobEntry.stdReChk Then





        'End If



        '******************   THIS WILL WRITE ANY CHANGES MADE TO THE DATAGRID BACK TO THE DATABASE ******************

        'Try

        '    If frmJobEntry.LDS.HasChanges Then


        '        'LDA.UpdateCommand = New Oracle.ManagedDataAccess.Client.OracleCommandBuilder(frmJobEntry.LDA).GetUpdateCommand

        '        frmJobEntry.LDA.Update(frmJobEntry.LDS.Tables(0))

        '    End If






        'Catch dbcx As DBConcurrencyException
        '    Dim Response As String

        '    Response = dbcx.Row.ToString
        '    writeerrorLog.writelog("db B_AL_AD_W Con Error", Response, False, "reChkA_Pk Con Fault")
        '    Response = dbcx.RowCount.ToString
        '    writeerrorLog.writelog("db B_AL_AD_W_Pk Con Error", Response, False, "ReChkA_Pk Con Fault")




        'Catch ex As Exception
        '    'Write error to Log File
        '    Dim ErrorMsg As String = "Computer " & System.Environment.MachineName
        '    writeerrorLog.writelog("db B_AL_AD_W Update Error", ErrorMsg, False, "System Fault")
        '    writeerrorLog.writelog("db B_AL_AD_W Update Error", ex.Message, False, "System Fault")
        '    writeerrorLog.writelog("db B_AL_AD_W Update Error", ex.ToString, False, "System Fault")

        '    MsgBox("Update Error: " & vbNewLine & ex.Message)
        '    pauseScan = 0 'Allow barcode entry
        'End Try



        If frmJobEntry.LConn.State = ConnectionState.Open Then frmJobEntry.LConn.Close()
        frmDGV.DGVdata.ClearSelection()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        frmJobEntry.Show()
        gradePackActive = 0
        pauseScan = 0 'Allow barcode entry
        Me.Close()



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
        If pauseScan = 0 Then
            If e.KeyCode = Keys.Return Then
                prgContinue()
            End If
        End If

    End Sub

    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click

        jobEnd()

    End Sub


End Class




