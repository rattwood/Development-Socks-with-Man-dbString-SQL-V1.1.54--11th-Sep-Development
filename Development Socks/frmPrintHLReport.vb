Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel



Public Class frmPrintHLReport

    Dim sp_nums As String
    Dim template As String
    Dim FileInName As String
    Dim MyExcel As New Excel.Application
    Dim abortPrint As Integer = 0
    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError


    Private Sub frmPrintCartReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        prtHLSheet()
    End Sub

    Public Sub prtHLSheet()


        'Left Side Rows and Columns on Excel Sheet
        Dim exNcRw As Integer = 9
        Dim exNcCl As Integer = 2
        Dim exM30Rw As Integer = 19  '14 was original on old form
        Dim exM30Cl As Integer = 2
        Dim exP30Rw As Integer = 14   '19 was original on old form
        Dim exP30Cl As Integer = 2
        Dim exABRw As Integer = 24
        Dim exABCl As Integer = 2
        Dim exDfRw As Integer = 29
        Dim exDfCl As Integer = 2

        'Right Side Rows and Columns on Excel Sheet
        Dim ex0Rw As Integer = 9
        Dim ex0Cl As Integer = 12
        Dim exM10Rw As Integer = 14
        Dim exM10Cl As Integer = 12
        Dim exP10Rw As Integer = 19
        Dim exP10Cl As Integer = 12
        Dim exM50Rw As Integer = 24
        Dim exM50Cl As Integer = 12
        Dim exP50Rw As Integer = 29
        Dim exP50Cl As Integer = 12

        Dim exNcVal = 0
        Dim exWasVal As String = 0
        Dim exL = 0
        Dim exH = 0
        Dim exLVal = 0
        Dim exHVal = 0
        Dim exABVal = 0
        Dim exDfVal = 0
        Dim ex0Val = 0
        Dim exM10Val = 0
        Dim exP10Val = 0
        Dim exM50Val = 0
        Dim exP50Val = 0
        Dim exSTDVal = 0
        Dim STDFLAG As Integer = 0

        Dim exConeH As String = 0
        Dim exCconeL As String = 0

        Dim savename As String

        template = (My.Settings.dirTemplate & "\" & "HLReportTemplate.xlsx").ToString

        'Dim ExcelApp As New Excel.Application
        Dim workbook As Excel.Workbook
        Dim sheet As Excel.Worksheet

        Dim saveString As String
        ' Dim sp_nums As String

        saveString = DGVHLReport.Rows(0).Cells("BCODEJOB").Value.ToString  'gets the BCODEJOB Value

        savename = (My.Settings.dirCarts & "\" & saveString & "HLSep.xlsx").ToString


        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Me.Close()
            frmJobEntry.Show()
        End If

        'Call IsFileOpen(New FileInfo(savename))


        If abortPrint Then
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Visible = True
            frmJobEntry.txtLotNumber.Focus()
            frmJobEntry.cartReport = 0
            frmJobEntry.txtBoxCartReport.Text = ""
            frmJobEntry.Show()
            Me.Close()
            Exit Sub
        End If


        'Make worksheet visible
        ' MyExcel.Visible = True


        If DGVHLReport.Rows(0).Cells("MCNUM").Value = 21 Or DGVHLReport.Rows(0).Cells("MCNUM").Value = 23 Or DGVHLReport.Rows(0).Cells("MCNUM").Value = 25 Or DGVHLReport.Rows(0).Cells("MCNUM").Value = 27 Then sp_nums = "1 - 192"
        If DGVHLReport.Rows(0).Cells("MCNUM").Value = 22 Or DGVHLReport.Rows(0).Cells("MCNUM").Value = 24 Or DGVHLReport.Rows(0).Cells("MCNUM").Value = 26 Or DGVHLReport.Rows(0).Cells("MCNUM").Value = 28 Then sp_nums = "193 - 384"
        If DGVHLReport.Rows(0).Cells("MCNUM").Value = 30 Or DGVHLReport.Rows(0).Cells("MCNUM").Value = 32 Then sp_nums = "1 - 144"
        If DGVHLReport.Rows(0).Cells("MCNUM").Value = 31 Or DGVHLReport.Rows(0).Cells("MCNUM").Value = 33 Then sp_nums = "145 - 288"
        If DGVHLReport.Rows(0).Cells("MCNUM").Value = 29 Then sp_nums = "1 - " & DGVHLReport.Rows.Count.ToString


        'Wait Curson
        Label1.Visible = True
        Label1.Text = "Creating HLCart Report Please wait "
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        workbook = MyExcel.Workbooks.Open(template)

        'MyExcel.Visible = True

        'Change header on sheet
        MyExcel.Cells(2, 1) = "DTY HL SELECTION SHEET"

        'Change +30 to H  and M30 to L
        MyExcel.Cells(15, 1) = ""
        MyExcel.Cells(16, 1) = "H"
        MyExcel.Cells(20, 1) = ""
        MyExcel.Cells(21, 1) = "L"

        'Change

        'Date and Time
        MyExcel.Cells(3, 18) = DateAndTime.Now.ToString("dd MM yyy")
        'MachineName
        MyExcel.Cells(4, 2) = DGVHLReport.Rows(0).Cells("MCNAME").Value
        'ProductName
        MyExcel.Cells(4, 5) = DGVHLReport.Rows(0).Cells("PRODNAME").Value
        'MERGE #
        MyExcel.Cells(4, 9) = DGVHLReport.Rows(0).Cells("MERGENUM").Value

        'DoffingNum
        MyExcel.Cells(4, 12) = DGVHLReport.Rows(0).Cells("DOFFNUM").Value
        'sp_nums RANGE
        'sp_nums = ((DGVHLReport.Rows(0).Cells(6).Value) & "-" & DGVHLReport.Rows(191).Cells(6).Value)
        MyExcel.Cells(4, 15) = sp_nums
        'STD Machine number from Barcode
        'MyExcel.Cells(6, 6) = DGVHLReport.Rows(0).Cells(1).Value

        If frmJobEntry.varProdWeight = Nothing Then frmJobEntry.varProdWeight = 0
        'PRODUCT WEIGHT
        MyExcel.Cells(4, 18) = frmJobEntry.varProdWeight.ToString



        Dim missCount As Integer = 0   'VAR TO COUNT MISSING CONES
        Dim stdCount As Integer = 0   ' VAR TO COUNT STD CHEESE
        Dim JudCount As Integer = 0
        Dim gradeHCount, gradeHSCount, gradeDefCount, gradeLcount, gradeLSCount As Integer

        For dgvRW As Integer = 1 To DGVHLReport.Rows.Count

            'Routine for Spindles

            'line to get row/colum dat from DatGridView for NoCone and write to Excel Sheet 
            exNcVal = DGVHLReport.Rows(dgvRW - 1).Cells("MISSCONE").Value  'Missing Cheese

            If exNcVal > 0 Then
                MyExcel.Cells(exNcRw, exNcCl) = exNcVal
                If exNcRw = 12 Then
                    exNcCl = exNcCl + 1
                    missCount = missCount + 1
                    exNcRw = 9
                Else
                    exNcRw = exNcRw + 1
                    missCount = missCount + 1
                End If
            End If

            If Not IsDBNull(DGVHLReport.Rows(dgvRW - 1).Cells("STDSTATE").Value) Then
                If Not (DGVHLReport.Rows(dgvRW - 1).Cells("STDSTATE").Value = "") Then
                    If DGVHLReport.Rows(dgvRW - 1).Cells("STDSTATE").Value > 0 AndAlso DGVHLReport.Rows(dgvRW - 1).Cells("STDSTATE").Value < 10 Then
                        exSTDVal = DGVHLReport.Rows(dgvRW - 1).Cells("CONENUM").Value 'STD Cheese
                    End If
                End If
            End If

            If exSTDVal > 0 Then
                STDFLAG = 1
                MyExcel.Cells(exNcRw, exNcCl).interior.color = Color.Orange
                MyExcel.Cells(exNcRw, exNcCl) = exSTDVal
                If exNcRw = 12 Then
                    exNcCl = exNcCl + 1
                    stdCount = stdCount + 1
                    exNcRw = 9
                Else
                    exNcRw = exNcRw + 1
                    stdCount = stdCount + 1
                End If
            End If




            'LINE TO GET WASTE CONES FOUND IN SORTING AND REPORT IN MISSING CONE SECTION

            If DGVHLReport.Rows(dgvRW - 1).Cells("FLT_W").Value = True And Not STDFLAG Then  'CHECK FOR WASTE CONE CHECKED
                exWasVal = DGVHLReport.Rows(dgvRW - 1).Cells("CONENUM").Value.ToString  'GET CONE NUMBER

                If exWasVal > 0 Then
                    MyExcel.Cells(exNcRw, exNcCl) = (exWasVal & "W")
                    If exNcRw = 12 Then
                        exNcCl = exNcCl + 1
                        missCount = missCount + 1
                        exNcRw = 9
                    Else
                        exNcRw = exNcRw + 1
                        missCount = missCount + 1
                    End If
                End If
            End If


            'line to get row/colum dat from DatGridView for L and write to Excel Sheet 
            If Not IsDBNull(DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value) Then
                Dim tmpHL As String = DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value
                Select Case tmpHL
                    Case "H"
                        If Not STDFLAG Then
                            'Get cone number
                            exHVal = DGVHLReport.Rows(dgvRW - 1).Cells("CONENUM").Value
                            MyExcel.Cells(exM30Rw, exM30Cl) = exHVal


                            MyExcel.Cells(exP30Rw, exP30Cl) = exHVal
                            If DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True Then MyExcel.Cells(exP30Rw, exP30Cl).interior.color = Color.LightSalmon
                            If exP30Rw = 17 Then    'Original 22
                                exP30Cl = exP30Cl + 1
                                JudCount = JudCount + 1
                                exP30Rw = 14    'Original 19
                            Else
                                exP30Rw = exP30Rw + 1
                                JudCount = JudCount + 1
                            End If
                        End If



                    Case "L"

                        If Not STDFLAG Then
                            'Get cone number
                            exLVal = DGVHLReport.Rows(dgvRW - 1).Cells("CONENUM").Value
                            MyExcel.Cells(exM30Rw, exM30Cl) = exLVal

                            If DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True Then MyExcel.Cells(exM30Rw, exM30Cl).interior.color = Color.LightSalmon
                            If exM30Rw = 22 Then   'Original value 17
                                exM30Cl = exM30Cl + 1
                                JudCount = JudCount + 1
                                exM30Rw = 19    'Original value 14
                            Else
                                exM30Rw = exM30Rw + 1
                                JudCount = JudCount + 1
                            End If
                        End If





                End Select


            End If


            'If exL > 0 And Not STDFLAG Then
            '    MyExcel.Cells(exM30Rw, exM30Cl) = exL
            '    If DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True Then MyExcel.Cells(exM30Rw, exM30Cl).interior.color = Color.LightSalmon
            '    If exM30Rw = 22 Then   'Original value 17
            '        exM30Cl = exM30Cl + 1
            '        JudCount = JudCount + 1
            '        exM30Rw = 19    'Original value 14
            '    Else
            '        exM30Rw = exM30Rw + 1
            '        JudCount = JudCount + 1
            '    End If
            'End If

            ''line to get row/colum dat from DatGridView for H and write to Excel Sheet 
            'exH = DGVHLReport.Rows(dgvRW - 1).Cells("P30").Value
            'If exH > 0 And Not STDFLAG Then
            '    MyExcel.Cells(exP30Rw, exP30Cl) = exH
            '    If DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True Then MyExcel.Cells(exP30Rw, exP30Cl).interior.color = Color.LightSalmon
            '    If exP30Rw = 17 Then    'Original 22
            '        exP30Cl = exP30Cl + 1
            '        JudCount = JudCount + 1
            '        exP30Rw = 14    'Original 19
            '    Else
            '        exP30Rw = exP30Rw + 1
            '        JudCount = JudCount + 1
            '    End If
            'End If

            ''line to get row/colum dat from DatGridView for AB (Barley) Or Defect Cones and write to Excel Sheet 
            'If DGVHLReport.Rows(dgvRW - 1).Cells("CONEBARLEY").Value > 0 And Not STDFLAG Then   'Barley
            '    exABVal = DGVHLReport.Rows(dgvRW - 1).Cells(6).Value
            'ElseIf DGVHLReport.Rows(dgvRW - 1).Cells("M50").Value > 0 And Not STDFLAG Then  'M50
            '    exABVal = DGVHLReport.Rows(dgvRW - 1).Cells(6).Value
            'ElseIf DGVHLReport.Rows(dgvRW - 1).Cells("P50").Value > 0 And Not STDFLAG Then  'P50
            '    exABVal = DGVHLReport.Rows(dgvRW - 1).Cells(6).Value
            'End If


            'If exABVal > 0 And Not STDFLAG Then
            '    MyExcel.Cells(exABRw, exABCl) = exABVal
            '    If DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True Then MyExcel.Cells(exABRw, exABCl).interior.color = Color.LightSalmon
            '    If exABRw = 27 Then
            '        exABCl = exABCl + 1
            '        JudCount = JudCount + 1
            '        exABRw = 24
            '    Else
            '        exABRw = exABRw + 1
            '        JudCount = JudCount + 1
            '    End If
            'End If



            'line to get row/colum dat from DatGridView for Waste (Dyefect) > 50 and write to Excel Sheet 


            'If DGVHLReport.Rows(dgvRW - 1).Cells("COLWASTE").Value > 0 And Not STDFLAG Then
            '    exDfVal = DGVHLReport.Rows(dgvRW - 1).Cells("CONENUM").Value ' Colour Waste

            'End If




            If exDfVal > 0 And Not STDFLAG Then
                MyExcel.Cells(exDfRw, exDfCl) = exDfVal
                If DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True Then MyExcel.Cells(exDfRw, exDfCl).interior.color = Color.LightSalmon
                If exDfRw = 32 Then
                    exDfCl = exDfCl + 1
                    JudCount = JudCount + 1
                    exDfRw = 29
                Else
                    exDfRw = exDfRw + 1
                    JudCount = JudCount + 1
                End If
            End If




            'COUNT GRADE H CONES
            If Not IsDBNull(DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value) Then
                If DGVHLReport.Rows(dgvRW - 1).Cells("CONESTATE").Value = 9 And DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = False And DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value = "H" Or
                    DGVHLReport.Rows(dgvRW - 1).Cells("CONESTATE").Value = 15 And DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = False And DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value = "H" Then gradeHCount = gradeHCount + 1
                'COUNT GRADE HS CONES
                If DGVHLReport.Rows(dgvRW - 1).Cells("CONESTATE").Value = 9 And DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True And DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value = "H" Or
                    DGVHLReport.Rows(dgvRW - 1).Cells("CONESTATE").Value = 15 And DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True And DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value = "H" Then gradeHSCount = gradeHSCount + 1

                'COUNT GRADE H CONES
                If DGVHLReport.Rows(dgvRW - 1).Cells("CONESTATE").Value = 9 And DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = False And DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value = "L" Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("CONESTATE").Value = 15 And DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = False And DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value = "L" Then gradeLcount = gradeLcount + 1
                'COUNT GRADE HS CONES
                If DGVHLReport.Rows(dgvRW - 1).Cells("CONESTATE").Value = 9 And DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True And DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value = "L" Or
                    DGVHLReport.Rows(dgvRW - 1).Cells("CONESTATE").Value = 15 And DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True And DGVHLReport.Rows(dgvRW - 1).Cells("HHLL").Value = "L" Then gradeLSCount = gradeLSCount + 1
            End If


            'COUNT SORT DEFECT CONES
            If DGVHLReport.Rows(dgvRW - 1).Cells("DEFCONE").Value > 0 And DGVHLReport.Rows(dgvRW - 1).Cells("CONEBARLEY").Value = 0 Then
                If DGVHLReport.Rows(dgvRW - 1).Cells("FLT_K").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_D").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_F").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_O").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_T").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_P").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_S").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_X").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_N").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_W").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_H").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_TR").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_B").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_C").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_DO").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_DH").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_CL").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_FI").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_YN").Value = True Or DGVHLReport.Rows(dgvRW - 1).Cells("FLT_HT").Value = True Or
                        DGVHLReport.Rows(dgvRW - 1).Cells("FLT_LT").Value = True Then

                    gradeDefCount = gradeDefCount + 1
                End If
            End If


            exNcVal = 0
            ex0Val = 0
            exM10Val = 0
            exP10Val = 0
            exL = 0
            exH = 0
            exM50Val = 0
            exP50Val = 0
            exABVal = 0
            exDfVal = 0
            STDFLAG = 0
            exSTDVal = 0


        Next

        'TOTALMISSING CONES
        MyExcel.Cells(5, 18) = JudCount
        'TOTAL OF CONES ON CART  192 LESS MISSING CONES
        MyExcel.Cells(7, 19) = (DGVHLReport.Rows.Count() - (missCount + stdCount))
        'TOTAL OF GRADE H FULL CONES
        MyExcel.Cells(35, 9) = gradeHCount - stdCount
        'TOTAL SHORT GRADE HS CONES
        MyExcel.Cells(36, 9) = gradeHSCount
        'TOTAL OF GRADE H FULL CONES
        MyExcel.Cells(35, 6) = gradeLcount
        'TOTAL SHORT GRADE HS CONES
        MyExcel.Cells(36, 6) = gradeLSCount



        'TOTAL SORT DEFECT CONES
        MyExcel.Cells(37, 9) = gradeDefCount
        MyExcel.Cells(38, 9) = stdCount

        'Routine to get the product weight
        Dim prNumInt As Integer = DGVHLReport.Rows(0).Cells(2).Value
        Dim prNum As String = prNumInt.ToString("000")
        frmJobEntry.LExecQuery("SELECT * FROM product WHERE PRNUM = '" & prNum & "' ")
        If frmJobEntry.LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGV.DGVdata.DataSource = frmJobEntry.LDS.Tables(0)
            frmDGV.DGVdata.Rows(0).Selected = True
            'CHEESE WEIGHT
            MyExcel.Cells(4, 18) = frmDGV.DGVdata.Rows(0).Cells(11).Value
        Else
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Label1.Visible = False

            MsgBox("This product is not in the database")

            MyExcel.Quit()

            releaseObject(sheet)
            releaseObject(workbook)
            releaseObject(MyExcel)

            Me.Close()
            frmJobEntry.Show()
            frmJobEntry.txtLotNumber.Visible = True
            frmJobEntry.txtLotNumber.Focus()
            frmJobEntry.HLReport = 0
            frmJobEntry.txtBoxCartReport.Text = ""
            Me.Close()
            Exit Sub

        End If

        Try

            'Save changes to new file in CKCarts
            MyExcel.DisplayAlerts = False
            workbook.SaveAs(Filename:=savename, FileFormat:=51)
            MyExcel.DisplayAlerts = True
        Catch ex As Exception
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Label1.Visible = False
            'Write error to Log File
            writeerrorLog.writelog("Fault Report Save Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Fault Report Save Error", ex.ToString, False, "System Fault")

            MsgBox(ex.Message)

        End Try

        Try
            'Close template file but do not save updates to it

            workbook.Close(SaveChanges:=False)

        Catch ex As Exception
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Label1.Visible = False
            'Write error to Log File
            writeerrorLog.writelog("Fault Report Close Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Fault Report Close Error", ex.ToString, False, "System Fault")


            MsgBox(ex.Message)
        End Try


        MyExcel.Quit()

        releaseObject(sheet)
        releaseObject(workbook)
        releaseObject(MyExcel)

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Label1.Visible = False

        MsgBox("Job Report " & savename & " Created")
        Me.Close()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Visible = True
        frmJobEntry.txtLotNumber.Focus()
        frmJobEntry.HLReport = 0
        frmJobEntry.btnCartReport.Visible = True
        frmJobEntry.btnHLReport.Visible = True
        frmJobEntry.txtBoxCartReport.Text = ""
        frmJobEntry.lblScanType.Visible = True
        frmJobEntry.btnJobReport.Visible = False
        frmJobEntry.btnCancelReport.Visible = True



        Quit()

    End Sub

    Private Sub releaseObject(ByVal obj As Object)

        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub IsFileOpen(ByVal file As FileInfo)
        Dim stream As FileStream = Nothing
        Try
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            stream.Close()
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Open Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Open Error", ex.ToString, False, "System Fault")

            ' do something here, either close the file if you have a handle, show a msgbox, retry  or as a last resort terminate the process - which could cause corruption and lose data
            MsgBox("Excel file is Open, Please close and retry")

            MyExcel.DisplayAlerts = False
            MyExcel.Quit()
            abortPrint = 1

        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Quit()
    End Sub





    Private Sub Quit()

        Me.Close()
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Visible = True
        frmJobEntry.txtLotNumber.Focus()
        frmJobEntry.cartReport = 0
        frmJobEntry.txtBoxCartReport.Text = ""

    End Sub

End Class
