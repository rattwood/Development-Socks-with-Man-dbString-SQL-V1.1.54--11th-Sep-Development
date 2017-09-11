

Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmPackPrvGet

    'Dim MyPrevExcel As New Excel.Application
    Public nfree As Integer = 13

    Public Sub PrvGet()

        Dim MyPrevExcel As New Excel.Application
        Dim xpPrevWoorkbook As Excel.Workbook
        Dim xpPrevSheets As Excel.Worksheet

        xpPrevWoorkbook = MyPrevExcel.Workbooks.Open(frmPackRepMain.prevDaysName)


        'FIND NEXT BLANK ROW FOR CONES
        For rcount = 13 To 102
            If MyPrevExcel.Cells(rcount, 4).Value > 0 Then
                Continue For
            Else
                nfree = rcount
                Exit For
            End If
        Next


        Try
            'Close template file but do not save updates to it
            xpPrevWoorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
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