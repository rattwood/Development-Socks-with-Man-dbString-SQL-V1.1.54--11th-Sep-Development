﻿

Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmPackPrvGet

    'Dim MyPrevExcel As New Excel.Application
    Public nfree As Integer
    Public ncfree As Integer
    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError

    Public Sub PrvGet()

        Dim MyPrevExcel As New Excel.Application
        Dim xpPrevWoorkbook As Excel.Workbook
        Dim xpPrevSheets As Excel.Worksheet

        xpPrevWoorkbook = MyPrevExcel.Workbooks.Open(frmPackRepMain.prevDaysName)


        'FIND NEXT BLANK ROW FOR CONES
        Select Case frmJobEntry.txtGrade.Text
            Case "A", "B", "AL", "AD", "Waste"
                For rcount = 13 To 102
                    If MyPrevExcel.Cells(rcount, 4).Value > 0 Then
                        Continue For
                    Else
                        nfree = rcount
                        Exit For
                    End If
                Next

            Case "P35 AS", "P35 BS"
                'WE NEED TO CHECK ROW D12 TO D41, THEN H12 TO H41 THEN L12 TO L41

                Dim colCount As Integer = 4
                Dim endloop As Integer = 0

                For ccount = 1 To 3

                    For rcount = 12 To 41
                        If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then  'C9-C40
                            Continue For
                        Else
                            nfree = rcount
                            ncfree = colCount
                            endloop = 1
                            Exit For
                        End If
                    Next

                    If endloop Then
                        Exit For
                    Else
                        If colCount < 12 Then colCount = colCount + 4
                    End If

                Next



            Case "P25 AS", "P30 BS"
                'WE NEED TO CHECK ROW D12 TO D51, THEN H12 TO H51 THEN L12 TO L51
                Dim colCount As Integer = 4
                Dim endloop As Integer = 0

                For ccount = 1 To 3

                    For rcount = 12 To 51
                        If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then  'C9-C40
                            Continue For
                        Else
                            nfree = rcount
                            ncfree = colCount
                            endloop = 1
                            Exit For
                        End If
                    Next

                    If endloop Then
                        Exit For
                    Else
                        If colCount < 12 Then colCount = colCount + 4
                    End If

                Next






            Case "P15 AS", "P20 BS"
                'WE NEED TO CHECK ROW D13 TO D64, THEN H13 TO H64 THEN L13 TO L64
                ''Dim colCount As Integer = 4

                ''For ccount = 1 To 4
                ''    If ccount < 4 Then
                ''        For rcount = 14 To 66
                ''            If MyPrevExcel.Cells(rcount, colCount).Value > 0 Then  'C9-C40
                ''                Continue For
                ''            Else
                ''                nfree = rcount
                ''                ncfree = colCount
                ''                Exit For
                ''            End If
                ''        Next
                ''    Else
                ''        For rcount = 14 To 66
                ''            If MyPrevExcel.Cells(rcount, colCount).Value > 0 Then  'C9-C40
                ''                Continue For
                ''            Else
                ''                nfree = rcount
                ''                ncfree = colCount
                ''                Exit For
                ''            End If
                ''        Next
                ''    End If

                ''    If colCount < 16 Then colCount = colCount + 4
                ''Next

                Dim colCount As Integer = 4
                Dim endloop As Integer

                For ccount = 1 To 4  'Three sets of columns
                    If ccount < 4 Then
                        For rcount = 14 To 65
                            If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then
                                Continue For
                            Else
                                nfree = rcount
                                ncfree = colCount
                                endloop = 1
                                Exit For
                            End If
                        Next
                    Else
                        For rcount = 14 To 52
                            If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then
                                Continue For
                            Else
                                nfree = rcount
                                ncfree = colCount
                                endloop = 1
                                Exit For
                            End If
                        Next
                    End If


                    If endloop Then
                        Exit For
                    Else
                        colCount = colCount + 4
                    End If
                Next






            Case "ReCheck"
                For rcount = 9 To 40
                    If MyPrevExcel.Cells(rcount, 3).Value > 0 Then  'C9-C40
                        Continue For
                    Else
                        nfree = rcount
                        Exit For
                    End If
                Next
            Case "Pilot 6Ch"
                For rcount = 13 To 102
                    If MyPrevExcel.Cells(rcount, 4).Value > 0 Then
                        Continue For
                    Else
                        nfree = rcount
                        Exit For
                    End If
                Next

            Case "Pilot 15Ch"
                'WE NEED TO CHECK ROW D12 TO D71, THEN H12 TO H71 THEN L12 TO L71 Then P12 TO P56


                Dim colCount As Integer = 4
                Dim endloop As Integer

                For ccount = 1 To 4  'Three sets of columns
                    If ccount < 4 Then
                        For rcount = 12 To 71
                            If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then

                                Continue For
                            Else
                                nfree = rcount
                                ncfree = colCount
                                endloop = 1
                                Exit For
                            End If
                        Next
                    Else
                        For rcount = 12 To 56
                            If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then

                                Continue For
                            Else
                                nfree = rcount
                                ncfree = colCount
                                endloop = 1
                                Exit For
                            End If
                        Next
                    End If


                    If endloop Then
                        Exit For
                    Else
                        If colCount < 16 Then colCount = colCount + 4
                    End If
                Next



            Case "Pilot 20Ch"
                'WE NEED TO CHECK ROW D13 TO D64, THEN H13 TO H64 THEN L13 TO L64
                Dim colCount As Integer = 4
                Dim endloop As Integer

                For ccount = 1 To 5

                    For rcount = 12 To 71
                        If MyPrevExcel.Cells(rcount, colCount).Value > "0" Then  'C9-C40
                            Continue For
                        Else
                            nfree = rcount
                            ncfree = colCount
                            endloop = 1
                            Exit For
                        End If
                    Next

                    If endloop Then
                        Exit For
                    Else
                        If colCount < 20 Then colCount = colCount + 4
                    End If

                Next

        End Select









        Try
            'Close template file but do not save updates to it
            xpPrevWoorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Close Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Close Error", ex.ToString, False, "System Fault")
            MsgBox(ex.Message)
        End Try


        'CLEAN UP
        MyPrevExcel.Quit()
        releaseObject(xpPrevSheets)
        releaseObject(xpPrevWoorkbook)
        releaseObject(MyPrevExcel)

        progress()
        Me.Close()
    End Sub


    Private Sub progress()

        frmPackCreateNew.CreateNew()


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




End Class