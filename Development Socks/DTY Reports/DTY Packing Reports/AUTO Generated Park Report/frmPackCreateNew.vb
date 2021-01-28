Imports Excel = Microsoft.Office.Interop.Excel


Public Class frmPackCreateNew
    'Dim MyCrExcel As New Excel.Application
    Dim SheetCodeString As String

    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError
    Dim modBarcode As String

    Public Sub CreateNew()
        Dim MyPakExcel As New Excel.Application
        Dim boxCount As Integer = 0
        Dim nfree As Integer  'This will be container for the next row free  
        Dim ncfree As Integer 'This will be container for the next column free  
        Dim colcount As Integer
        Dim xlWorkbook As Excel.Workbook
        Dim xlSheets As Excel.Worksheet


        'OPEN A NEW WORKSHEET
        xlWorkbook = MyPakExcel.Workbooks.Open(frmPackRepMain.template)
        'ReName the work sheet 
        CType(MyPakExcel.Workbooks(1).Worksheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Name = frmPackRepMain.sheetName


        'CREATE CORRECT HEADER FOR SHEET
        Select Case frmPackRepMain.TmpGrade       'frmJobEntry.txtGrade.Text

            Case "A"
                nfree = 13

                Dim prodTf As String

                prodTf = (frmPacking.DGVPakingA.Rows(0).Cells("PRODNAME").Value & "  " & frmPacking.DGVPakingA.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(7, 4) = prodTf

                'Product Code
                MyPakExcel.Cells(7, 6) = frmPacking.DGVPakingA.Rows(0).Cells("PRNUM").Value        'F7

                'DATE
                MyPakExcel.Cells(5, 3) = Date.Now.ToString("dd MM yyyy")              'C5

                'CHEESE WEIGHT
                MyPakExcel.Cells(13, 5) = frmJobEntry.varProdWeight                   'E13

                'PACKER NAME
                MyPakExcel.Cells(13, 8) = frmJobEntry.PackOp     'H13

                createBarcode()

                'New positions for barcode
                MyPakExcel.Cells(5, 8) = SheetCodeString
                MyPakExcel.Cells(9, 10) = modBarcode



                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                If frmPackPrvGet.nfree > 0 Then
                    nfree = frmPackPrvGet.nfree
                    For usedrow = 13 To nfree - 1
                        MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                    Next

                End If

            Case "ReCheckA"
                nfree = 13

                Dim prodTf As String

                prodTf = (frmPackRchkA.DGVPakingRecA.Rows(0).Cells("PRODNAME").Value & "  " & frmPackRchkA.DGVPakingRecA.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(7, 4) = prodTf

                'Product Code
                MyPakExcel.Cells(7, 6) = frmPackRchkA.DGVPakingRecA.Rows(0).Cells("PRNUM").Value        'F7
                'DATE
                MyPakExcel.Cells(5, 3) = Date.Now.ToString("dd MM yyyy")              'C5
                'CHEESE WEIGHT
                MyPakExcel.Cells(13, 5) = frmJobEntry.varProdWeight                   'E13
                'PACKER NAME
                MyPakExcel.Cells(13, 8) = frmJobEntry.PackOp     'H13

                createBarcode()


                'New positions for barcode
                MyPakExcel.Cells(5, 8) = SheetCodeString
                MyPakExcel.Cells(9, 10) = modBarcode

                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                If frmPackPrvGet.nfree > 0 Then
                    nfree = frmPackPrvGet.nfree
                    For usedrow = 13 To nfree - 1
                        MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                    Next

                End If


            Case "B", "AL", "AD"
                nfree = 13

                Dim prodTf As String

                prodTf = (frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value & "  " & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(7, 4) = prodTf

                'Product Code
                MyPakExcel.Cells(7, 6) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value        'F7
                'DATE
                MyPakExcel.Cells(5, 3) = Date.Now.ToString("dd MM yyyy")              'C5
                'CHEESE WEIGHT
                MyPakExcel.Cells(13, 5) = frmJobEntry.varProdWeight                   'E13
                'PACKER NAME
                MyPakExcel.Cells(13, 8) = frmJobEntry.PackOp     'H13

                createBarcode()
                MyPakExcel.Cells(1, 4) = SheetCodeString

                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                If frmPackPrvGet.nfree > 0 Then
                    nfree = frmPackPrvGet.nfree
                    For usedrow = 13 To nfree - 1
                        MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                    Next

                End If

                Select Case frmJobEntry.txtGrade.Text
                    Case "B", "AL", "AD"
                        MyPakExcel.Cells(64, 14) = frmJobEntry.PackOp

                        createBarcode()
                        MyPakExcel.Cells(1, 4) = SheetCodeString
                End Select

            Case "P35 AS", "P35 BS"
                nfree = 12

                Dim prodTf As String

                prodTf = (frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value & "  " & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(6, 8) = prodTf

                'Product Code
                MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'L6
                'DATE
                MyPakExcel.Cells(5, 4) = Date.Now.ToString("dd MM yyyy")              'D5

                'PACKER NAME
                MyPakExcel.Cells(43, 4) = frmJobEntry.PackOp 'frmDGV.DGVdata.Rows(0).Cells(55).Value      'D43

                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                Select Case frmPackPrvGet.ncfree
                    Case 12
                        'This will write date to the first two cone columns
                        colcount = 4
                        For ccount = 1 To 2
                            For rcount = 12 To 41
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 12) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 8
                        'This will write date to the first One cone columns
                        For rcount = 12 To 41
                            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                        Next


                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 11 To nfree - 1
                                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 4

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                            Next
                        End If
                End Select

                createBarcode()
                MyPakExcel.Cells(1, 4) = SheetCodeString



            Case "P25 AS", "P30 BS"
                nfree = 13

                Dim prodTf As String

                prodTf = (frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value & "  " & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(6, 8) = prodTf

                'Product Code
                MyPakExcel.Cells(6, 12) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'L6
                'DATE
                MyPakExcel.Cells(5, 4) = Date.Now.ToString("dd MM yyyy")              'D5

                'PACKER NAME
                MyPakExcel.Cells(53, 4) = frmJobEntry.PackOp  'frmDGV.DGVdata.Rows(0).Cells(55).Value      'D53


                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                Select Case frmPackPrvGet.ncfree
                    Case 12
                        'This will write date to the first two cone columns
                        colcount = 4
                        For ccount = 1 To 2
                            For rcount = 12 To 51
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 12) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 8
                        'This will write date to the first One cone columns
                        For rcount = 12 To 51
                            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                        Next


                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 4

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                            Next
                        End If
                End Select


                createBarcode()
                MyPakExcel.Cells(1, 4) = SheetCodeString

            Case "P15 AS", "P20 BS"
                nfree = 14
                Dim prodTf As String

                prodTf = (frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value & "  " & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(7, 9) = prodTf

                ''Product Name
                'MyPakExcel.Cells(7, 9) = frmDGV.DGVdata.Rows(0).Cells(52).Value       'E8
                'Product Code
                MyPakExcel.Cells(7, 16) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'N8
                'DATE
                MyPakExcel.Cells(5, 4) = Date.Now.ToString("dd MM yyyy")              'D6
                'CHEESE WEIGHT
                'MyPakExcel.Cells(14, 5) = frmJobEntry.varProdWeight                   'E13
                'BARCODE IN
                MyPakExcel.Cells(54, 17) = frmJobEntry.txtOperator.Text    'P55


                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                Select Case frmPackPrvGet.ncfree
                    Case 16
                        'This will write date to the first three cone columns
                        colcount = 4
                        For ccount = 1 To 3
                            For rcount = 14 To 52
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 14 To nfree - 1
                                MyPakExcel.Cells(usedrow, 16) = frmPackRepMain.prevDays
                            Next

                        End If

                    Case 12
                        colcount = 4
                        For ccount = 1 To 2
                            For rcount = 14 To 65
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 14 To nfree - 1
                                MyPakExcel.Cells(usedrow, 16) = frmPackRepMain.prevDays
                            Next

                        End If


                    Case 8
                        'This will write date to the first One cone columns
                        For rcount = 14 To 65
                            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                        Next


                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 14 To nfree - 1
                                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 4

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 14 To nfree - 1
                                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                            Next
                        End If
                End Select


                createBarcode()
                MyPakExcel.Cells(1, 4) = SheetCodeString

            Case "ReCheck"
                nfree = 9

                Dim prodTf As String

                prodTf = (frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value & "  " & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(5, 4) = prodTf 'frmDGV.DGVdata.Rows(0).Cells(52).Value     'D5

                'Product Code
                MyPakExcel.Cells(5, 7) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'G5
                'DATE
                MyPakExcel.Cells(4, 7) = Date.Now.ToString("dd MM yyyy")              'G4
                'CHEESE WEIGHT
                MyPakExcel.Cells(4, 5) = frmJobEntry.varProdWeight                   'E4
                'PACKER NAME
                MyPakExcel.Cells(42, 3) = frmJobEntry.txtOperator.Text      'D53

                createBarcode()
                MyPakExcel.Cells(1, 3) = SheetCodeString

            Case "Round1", "Round2", "Round3", "STD", "HLRound1", "HLRound2", "HLRound3", "HL STD"
                nfree = 9
                Dim prodTf As String

                prodTf = (frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value & "  " & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(5, 4) = prodTf

                ''Product Name
                'MyPakExcel.Cells(5, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value       'D5
                'Product Code
                MyPakExcel.Cells(5, 7) = frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value       'G5
                'DATE
                MyPakExcel.Cells(4, 7) = Date.Now.ToString("dd MM yyyy")              'G4
                'CHEESE WEIGHT
                MyPakExcel.Cells(4, 5) = frmJobEntry.varProdWeight                   'E4
                'PACKER NAME
                MyPakExcel.Cells(42, 3) = frmJobEntry.txtOperator.Text      'D53
                'Machine Number
                MyPakExcel.Cells(4, 3) = frmJobEntry.varMachineName

                Select Case frmJobEntry.txtGrade.Text
                    Case "Round1"
                        'Sheet Name
                        MyPakExcel.Cells(2, 2) = "Compare STD 1"
                    Case "Round2"
                        'Sheet Name
                        MyPakExcel.Cells(2, 2) = "Compare STD 2"
                    Case "Round3"
                        'Sheet Name
                        MyPakExcel.Cells(2, 2) = "Compare STD 3"
                    Case "STD"
                        'Sheet Name
                        MyPakExcel.Cells(2, 2) = "Compare STD"
                    Case "HLRound1"
                        'Sheet Name
                        MyPakExcel.Cells(2, 2) = "Compare HL STD 1"
                    Case "HLRound2"
                        'Sheet Name
                        MyPakExcel.Cells(2, 2) = "Compare HL STD 2"
                    Case "HLRound3"
                        'Sheet Name
                        MyPakExcel.Cells(2, 2) = "Compare HL STD 3"
                    Case "HL STD"
                        'Sheet Name
                        MyPakExcel.Cells(2, 2) = "Compare HL STD"
                End Select


                createBarcode()
                MyPakExcel.Cells(1, 3) = SheetCodeString
            Case "Pilot 6Ch"

                nfree = 13
                Dim prodTf As String

                prodTf = (frmPacking.DGVPakingA.Rows(0).Cells("PRODNAME").Value & "  " & frmPacking.DGVPakingA.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(7, 4) = prodTf

                ''Product Name
                'MyPakExcel.Cells(7, 4) = frmDGV.DGVdata.Rows(0).Cells(52).Value       'D7
                'Product Code
                MyPakExcel.Cells(7, 6) = frmPacking.DGVPakingA.Rows(0).Cells("PRNUM").Value        'F7
                'DATE
                MyPakExcel.Cells(5, 3) = Date.Now.ToString("dd MM yyyy")              'C5
                'CHEESE WEIGHT
                MyPakExcel.Cells(13, 6) = frmJobEntry.varProdWeight                   'E13
                'Barcode In
                MyPakExcel.Cells(61, 14) = frmJobEntry.PackOp

                createBarcode()
                MyPakExcel.Cells(1, 3) = SheetCodeString

                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                If frmPackPrvGet.nfree > 0 Then
                    nfree = frmPackPrvGet.nfree
                    For usedrow = 13 To nfree - 1
                        MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                    Next

                End If

            Case "Pilot 15Ch"
                nfree = 12

                Dim prodTf As String

                prodTf = (frmPacking.DGVPakingA.Rows(0).Cells("PRODNAME").Value & "  " & frmPacking.DGVPakingA.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(6, 8) = prodTf

                ''Product Name
                'MyPakExcel.Cells(6, 8) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                'Product Code
                MyPakExcel.Cells(6, 12) = frmPacking.DGVPakingA.Rows(0).Cells("PRNUM").Value
                'Packer Name
                MyPakExcel.Cells(73, 4) = frmJobEntry.PackOp
                'DATE
                MyPakExcel.Cells(4, 4) = Date.Now.ToString("dd MM yyyy")
                'CHEESE WEIGHT
                MyPakExcel.Cells(12, 5) = frmJobEntry.varProdWeight
                'Add Barcode to Sheet
                createBarcode()
                MyPakExcel.Cells(1, 4) = SheetCodeString

                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                Select Case frmPackPrvGet.ncfree
                    Case 16
                        'This will write date to the first three cone columns
                        colcount = 4
                        For ccount = 1 To 3
                            For rcount = 12 To 71
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 16) = frmPackRepMain.prevDays
                            Next

                        End If

                    Case 12
                        colcount = 4
                        For ccount = 1 To 2
                            For rcount = 12 To 71
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 12) = frmPackRepMain.prevDays
                            Next

                        End If


                    Case 8
                        'This will write date to the first One cone columns
                        For rcount = 12 To 71
                            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                        Next


                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 4

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                            Next
                        End If
                End Select



            Case "Pilot 20Ch"
                Dim prodTf As String

                prodTf = (frmPacking.DGVPakingA.Rows(0).Cells("PRODNAME").Value & "  " & frmPacking.DGVPakingA.Rows(0).Cells("MERGENUM").Value)
                'PRODUCT NAME
                MyPakExcel.Cells(6, 8) = prodTf

                ''Product Name
                'MyPakExcel.Cells(6, 8) = frmDGV.DGVdata.Rows(0).Cells(52).Value
                'Product Code
                MyPakExcel.Cells(6, 12) = frmPacking.DGVPakingA.Rows(0).Cells("PRNUM").Value
                'Packer Name
                MyPakExcel.Cells(73, 4) = frmJobEntry.PackOp
                'DATE
                MyPakExcel.Cells(4, 4) = Date.Now.ToString("dd MM yyyy")
                'CHEESE WEIGHT
                MyPakExcel.Cells(12, 5) = frmJobEntry.varProdWeight
                'Add Barcode to Sheet
                createBarcode()
                MyPakExcel.Cells(1, 4) = SheetCodeString

                'THIS IS USED TO WRITE DATE IN TO USED ROWS
                Select Case frmPackPrvGet.ncfree

                    Case 20
                        'This will write date to the first three cone columns
                        colcount = 4
                        For ccount = 1 To 4
                            For rcount = 12 To 71
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 20) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 16
                        'This will write date to the first three cone columns
                        colcount = 4
                        For ccount = 1 To 3
                            For rcount = 12 To 71
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 16) = frmPackRepMain.prevDays
                            Next

                        End If

                    Case 12
                        colcount = 4
                        For ccount = 1 To 2
                            For rcount = 12 To 71
                                MyPakExcel.Cells(rcount, colcount) = frmPackRepMain.prevDays
                            Next
                            colcount = colcount + 4
                        Next

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 12) = frmPackRepMain.prevDays
                            Next

                        End If


                    Case 8
                        'This will write date to the first One cone columns
                        For rcount = 13 To 66
                            MyPakExcel.Cells(rcount, 4) = frmPackRepMain.prevDays
                        Next


                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 8) = frmPackRepMain.prevDays
                            Next

                        End If
                    Case 4

                        If frmPackPrvGet.nfree > 0 Then
                            nfree = frmPackPrvGet.nfree
                            For usedrow = 12 To nfree - 1
                                MyPakExcel.Cells(usedrow, 4) = frmPackRepMain.prevDays
                            Next
                        End If
                End Select



        End Select


        If boxCount = 0 Then boxCount = 1


        Try

            'Save changes to new file in Paking Dir
            MyPakExcel.DisplayAlerts = False
            xlWorkbook.SaveAs(Filename:=frmPackRepMain.savename, FileFormat:=51)
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("Excel Save Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Excel Save Error", ex.ToString, False, "System Fault")

            MsgBox(ex.Message)
        End Try

        'CLOSE THE TEMPLATE FILE 
        Try
            'Save changes to new file in Paking Dir
            MyPakExcel.DisplayAlerts = False
            xlWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("Excel Close Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("Excel Close Error", ex.ToString, False, "System Fault")

            MsgBox(ex.Message)
        End Try

        'CLEAN UP
        MyPakExcel.Quit()
        '  releaseObject(xlSheets)
        releaseObject(xlWorkbook)
        releaseObject(MyPakExcel)


        Select Case frmJobEntry.txtGrade.Text
            Case "A", "ReCheckA"
                frmPackTodayUpdate.TodayUpdate()
            Case "B", "AL", "AD"
                frmPackTodayUpdate.TodayUpdateB_AL_AD()
            Case "P35 AS", "P35 BS"
                frmPackTodayUpdate.TodatUpdateBS_AS_35()
            Case "P25 AS", "P30 BS"
                frmPackTodayUpdate.TodayUpdateBS_AS_30()
            Case "P15 AS", "P20 BS"
                frmPackTodayUpdate.TodayUpdateBS_AS_20()
            Case "ReCheck"
                frmPackTodayUpdate.todayUpdate_ReCheck()
            Case "Round1", "Round2", "Round3", "STD", "HLRound1", "HLRound2", "HLRound3", "HL STD"
                frmPackTodayUpdate.todayUpdate_STD()
            Case "Pilot 6Ch"
                frmPackTodayUpdate.todayUpdate_pilot6()
            Case "Pilot 15Ch"
                frmPackTodayUpdate.todayUpdate_pilot15()
            Case "Pilot 20Ch"
                frmPackTodayUpdate.todayUpdate_pilot20()

        End Select

        Me.Close()

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

    Public Sub createBarcode()

        Dim today As String = Date.Now
        Dim day As String
        Dim month As String
        Dim year As String
        Dim gradeTxt As String

        'Routine to get date brocken down
        today = Convert.ToDateTime(today).ToString("dd MM yyyy")
        day = today.Substring(0, 2)
        month = today.Substring(3, 2)
        year = today.Substring(8, 2)


        Select Case frmJobEntry.txtGrade.Text
            Case "A"
                gradeTxt = "A" 'A Grade
            Case "B"
                gradeTxt = "B" 'B Grade
            Case "AL"
                gradeTxt = "AL" 'AL Grade
            Case "AD"
                gradeTxt = "AD" 'AD Grade
            Case "P35 AS"
                gradeTxt = "P35AS" 'P35 AS Grade
            Case "P35 BS"
                gradeTxt = "P35BS" 'P35 BS Grade
            Case "P25 AS"
                gradeTxt = "P25AS" 'P25 AS Grade
            Case "P30 BS"
                gradeTxt = "P30BS" 'P30 BS Grade
            Case "P15 AS"
                gradeTxt = "P15AS" 'P15 AS Grade
            Case "P20 BS"
                gradeTxt = "P20BS" 'P20 BS Grade
            Case "ReCheck"
                gradeTxt = "RECHECK" 'ReCheck Grade
            Case "Round1"
                gradeTxt = "R1" 'ReCheck Grade
            Case "Round2"
                gradeTxt = "R2" 'ReCheck Grade
            Case "Round3"
                gradeTxt = "R3" 'ReCheck Grade
            Case "STD"
                gradeTxt = "STD" 'ReCheck Grade
            Case "HLRound1"
                gradeTxt = "R1" 'ReCheck Grade
            Case "HLRound2"
                gradeTxt = "R2" 'ReCheck Grade
            Case "HLRound3"
                gradeTxt = "R3" 'ReCheck Grade
            Case "HL STD"
                gradeTxt = "STD" 'ReCheck Grade
            Case "Pilot 6Ch"
                gradeTxt = "PI06" 'A Grade 6 Cheese per box
            Case "Pilot 15Ch"
                gradeTxt = "PI15" 'A Grade 15 Cheese per box
            Case "Pilot 20Ch"
                gradeTxt = "PI20" 'A Grade 20 Cheese per box

        End Select



        SheetCodeString = ("*" & frmJobEntry.varProductCode & year & month & day & gradeTxt & "1" & "*")
        modBarcode = SheetCodeString.Replace("*", "")
    End Sub

    Private Sub frmPackCreateNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class