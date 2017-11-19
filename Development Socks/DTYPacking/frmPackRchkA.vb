

Imports System.Data.SqlClient



Public Class frmPackRchkA

    Private SQL As New SQLConn

    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Public LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
        Private LCmd As SqlCommand

        'SQL CONNECTORS
        Public LDA As SqlDataAdapter
        Public LDS As DataSet
        Public LDT As DataTable
        Public LCB As SqlCommandBuilder

        Public LRecordCount As Integer
        Private LException As String
        ' SQL QUERY PARAMETERS
        Public LParams As New List(Of SqlParameter)
        '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        Dim psorterror As String = 0
        Dim varVisConeInspect As String
        Dim coneBarley As String = 0
        Dim coneZero As String = 0
        Dim coneM10 As String = 0
        Dim coneP10 As String = 0
        Dim coneM30 As String = 0
        Dim coneP30 As String = 0
        Dim coneM50 As String = 0
        Dim coneP50 As String = 0
        Dim btnImage As Image
        Dim keepDefcodes As Integer
        Public bcodeScan As String = ""
        Dim clr As String = ""
        Public curcone As String = 0
        Public toAllocatedCount As Integer 'count of cones requierd to be scanned
        Public allocatedCount As Integer 'count of cones scanned
        Public itemCount As Integer = 0
        'ReCheck Params
        Dim reChecked, ReCheckTime As String
        Public removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
        Dim incoming As String
        Public measureOn As String
        Public NoCone As Integer
        Public defect As Integer

        Public varCartStartTime As String   'Record time that we started measuring
        Public varCartEndTime As String
        Public coneNumOffset As Integer
        Dim varConeBCode As String
        Dim fileActive As Integer
        Public varConeNum As Integer
        Private coneCount As Integer
        Public coneState As String
        Public packingActive = 0




    Private Sub frmPackRchkA_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Dim btnNum As Integer
        Dim btnNums As String


        btnNums = 1


        ' SELECT CONE NUMBER RANGE BASED ON CART NUMBER


        btnNum = 1
        coneNumOffset = 0



        'SET CORRECT BUTTUN NUMBERS BASED ON CONE NUMBERS (SPINDEL NUMBERS)
        For i As Integer = 1 To frmDGV.DGVdata.Rows.Count

            Me.Controls("btnCone" & i.ToString).Text = btnNum
            btnNum = btnNum + 1

        Next


        Me.txtCartNum.Text = frmJobEntry.cartSelect
        Me.lblJobNum.Text = frmJobEntry.varJobNum


        'GET NUMBER OF CONES THAT NEED ALLOCATING Count agains Job Barcode
        Dim btnCountStart As Integer = frmDGV.DGVdata.Rows.Count + 1
        Dim totBtn As Integer = 31 - btnCountStart
        For i = btnCountStart To 32
            Me.Controls("btnCone" & i.ToString).Visible = False
        Next


        For i = 1 To frmDGV.DGVdata.Rows.Count

            If frmDGV.DGVdata.Rows(i - 1).Cells(83).Value = "AL" Or frmDGV.DGVdata.Rows(i - 1).Cells(83).Value = "A" Then toAllocatedCount = toAllocatedCount + 1

        Next

        txtboxTotal.Text = toAllocatedCount





        'IF THIS IS AN EXISTING JOB THEN CALL BACK VALUES FROM DATABASE
        If frmJobEntry.coneValUpdate Then UpdateConeVal()
        prgContinue()
        Test()

        Me.KeyPreview = True  'Allows us to look for advace character from barcode
        txtConeBcode.Clear()
        txtConeBcode.Refresh()
        txtConeBcode.Focus()

    End Sub

    Private Sub Test()

        MsgBox("I am here")


    End Sub



    Private Sub UpdateConeVal()

        If My.Settings.debugSet Then frmDGV.Show()


        For rw As Integer = 1 To frmDGV.DGVdata.Rows.Count




            If (frmDGV.DGVdata.Rows(rw - 1).Cells(83).Value = "AL" Or frmDGV.DGVdata.Rows(rw - 1).Cells(83).Value = "A") And frmDGV.DGVdata.Rows(rw - 1).Cells("RECHK").Value = "4" Then
                Me.Controls("btnCone" & rw).BackColor = Color.Green       'Grade A Cone
            End If

            If frmDGV.DGVdata.Rows(rw - 1).Cells("RECHK").Value = "5" Then
                Me.Controls("btnCone" & rw).BackColor = Color.LightGreen       'Grade A Cone
            End If

            Me.Controls("btnCone" & rw).Enabled = False
        Next





    End Sub





    Private Sub btnDefect_Click(sender As Object, e As EventArgs) Handles btnDefect.Click
            Me.Hide()
            packingActive = 1

            frmPackingFault.Show()


        End Sub



    Public Sub prgContinue()




        bcodeScan = txtConeBcode.Text
        Dim curcone As String
        Dim coneCount As Integer = 0
        Dim today As String = DateAndTime.Today
        today = Convert.ToDateTime(today).ToString("dd-MMM-yyyy")



        Dim endval = frmDGV.DGVdata.Rows.Count


        For i = 1 To endval 'frmDGV.DGVdata.Rows.Count


            If frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = "4" And frmDGV.DGVdata.Rows(i - 1).Cells("FLT_S").Value = False Then

                curcone = i
                Me.Controls("btnCone" & curcone.ToString).BackColor = Color.LightGreen       'Grade A Cone
                frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = "5"
                frmDGV.DGVdata.Rows(i - 1).Cells("OPPACK").Value = frmJobEntry.PackOp
                frmDGV.DGVdata.Rows(i - 1).Cells("OPNAME").Value = frmJobEntry.varUserName
                frmDGV.DGVdata.Rows(i - 1).Cells("CARTENDTM").Value = today

                'CHECK TO SEE IF DATE ALREADY SET FOR END TIME
                If IsDBNull(frmDGV.DGVdata.Rows(0).Cells("PACKENDTM").Value) Then
                    For rows As Integer = 1 To frmDGV.DGVdata.Rows.Count
                        If My.Settings.chkUsePack = True Then frmDGV.DGVdata.Rows(rows - 1).Cells("PACKENDTM").Value = DateAndTime.Today  'PACKING CHECK END TIME.
                    Next
                End If


                allocatedCount = allocatedCount + 1
                endCheck()
                curcone = 0

            ElseIf frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And frmDGV.DGVdata.Rows(i - 1).Cells("RECHK").Value = "5" Then
                Label1.Visible = True
                Label1.Text = "Cheese already allocated"
                DelayTM()
                Label1.Visible = False
                'ElseIf frmDGV.DGVdata.Rows(i - 1).Cells("BCODECONE").Value = bcodeScan And frmDGV.DGVdata.Rows(i - 1).Cells(CONESTATE).Value < "9" Or frmDGV.DGVdata.Rows(i - 1).Cells(36).Value = bcodeScan And frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "9" And frmDGV.DGVdata.Rows(i - 1).Cells(43).Value = True Then
                '    curcone = frmDGV.DGVdata.Rows(i - 1).Cells(6).Value
                '    psorterror = 1
                '    Me.Controls("btnCone" & curcone - coneNumOffset.ToString).BackColor = Color.Red      'Wrong Cone scanned
                '    frmDGV.DGVdata.Rows(i - 1).Cells(58).Value = psorterror
                '    frmDGV.DGVdata.Rows(i - 1).Cells(55).Value = frmJobEntry.PackOp
                '    frmDGV.DGVdata.Rows(i - 1).Cells(9).Value = "14"
                '    frmDGV.DGVdata.Rows(i - 1).Cells(32).Value = today




                'Me.Hide()
                'frmRemoveCone.Show()
                'psorterror = 0
                'curcone = 0
                'Continue For
            Else
                txtConeBcode.Clear()
                txtConeBcode.Refresh()
                txtConeBcode.Focus()

            End If




        Next
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
                curcone = 0
                'frmPackReport.packPrint() 'Print the packing report and go back to Job Entry for the next cart


                frmPackRepMain.PackRepMainSub()
                frmPackRepMain.Close()
            'UpdateDatabase()

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
    Private Sub frmPackRchkA_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.Return Then

            prgContinue()


        End If

    End Sub


End Class



