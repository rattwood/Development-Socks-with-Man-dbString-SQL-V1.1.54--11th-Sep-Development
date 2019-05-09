Imports System.IO
Imports Microsoft.Office.Interop

Public Class xlConeCount
    'METHOD for CHECKING HOW MANY CHEESE ARE ALREADY SCANNED ON TO A GRADE SHEET AND PASS INFORMATION BACK TO PACKING SCREEN FOR A and ReCheck A


    Dim prodNameMod As String
    Dim savestring As String
    Dim savename As String
    Dim yestname1 As String
    Dim yestname2 As String
    Dim yestname3 As String
    Dim PrevPath1 As String
    Dim PrevPath2 As String
    Dim PrevPath3 As String


    Dim todaypath As String
    Dim sheetCount As Integer
    Dim SearchDate As String
    Public searchBarcode As String  'THIS IS THE SEARCH STRING FOR SQL ON PACKING A AND REACHECKA SHEETS

    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError


    Public Sub xlCheck()


        ''CREATE PRODUCT NAME STRING USED WHEN SAVING FILE

        Select Case frmJobEntry.txtGrade.Text
            Case "ReCheckA"
                'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
                prodNameMod = frmPackRchkA.DGVPakingRecA.Rows(0).Cells("PRODNAME").Value.ToString
                prodNameMod = prodNameMod.Replace("/", "_")

                'CREATE THE FULL NAME FOR SAVING THE FILE
                savestring = (prodNameMod & " " _
                    & frmPackRchkA.DGVPakingRecA.Rows(0).Cells("MERGENUM").Value.ToString & "_" _
                    & frmPackRchkA.DGVPakingRecA.Rows(0).Cells("PRNUM").Value.ToString) & " A"
            Case "A"
                'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
                prodNameMod = frmPacking.DGVPakingA.Rows(0).Cells("PRODNAME").Value.ToString
                prodNameMod = prodNameMod.Replace("/", "_")

                'CREATE THE FULL NAME FOR SAVING THE FILE
                savestring = (prodNameMod & " " _
                    & frmPacking.DGVPakingA.Rows(0).Cells("MERGENUM").Value.ToString & "_" _
                    & frmPacking.DGVPakingA.Rows(0).Cells("PRNUM").Value.ToString) & " A"

        End Select



        'CALL SUB TO GET TODAYS SAVE DIRECTORY
        todayDir()

        'create the save name of the file
        savename = (todaypath & "\" & savestring & ".xlsx").ToString


        'Create PREVIOUS THREE DAYS CHECK NAMES
        yestname1 = (PrevPath1 & "\" & savestring & ".xlsx").ToString
        yestname2 = (PrevPath2 & "\" & savestring & ".xlsx").ToString
        yestname3 = (PrevPath3 & "\" & savestring & ".xlsx").ToString



        'CHECK TO SEE IF THERE IS ALREADY A FILE STARTED FOR PRODUCT NUMBER
        'IN TODATY DIRECTORY
        If File.Exists(savename) Then
            SearchDate = Date.Now.ToString("dd_MM_yyyy")
            getCounts()
            Exit Sub

        Else

            If File.Exists(yestname1) Then      'ONE DAY AGO
                savename = yestname1
                SearchDate = Date.Now.AddDays(-1).ToString("dd_MM_yyyy")
                getCounts()
                Exit Sub
            ElseIf File.Exists(yestname2) Then  'TWO DAYS AGO
                savename = yestname2
                SearchDate = Date.Now.AddDays(-2).ToString("dd_MM_yyyy")
                getCounts()
                Exit Sub
            ElseIf File.Exists(yestname3) Then  'THREE DAYS AGO
                savename = yestname3
                SearchDate = Date.Now.AddDays(-3).ToString("dd_MM_yyyy")
                getCounts()
            End If
        End If

    End Sub

    Private Sub getCounts()
        Dim MyTodyExcel As New Excel.Application
        Dim xlTodyWorkbook As Excel.Workbook


        Try
            'GET SHEET COUNT FOR DOCUMENT SO WE CAN USE TO SEACK SQL AND GET COUNT OF PACKED CHEESE
            xlTodyWorkbook = MyTodyExcel.Workbooks.Open(savename)
            sheetCount = xlTodyWorkbook.Worksheets.Count
            createBarcode()

            'Close the Excel file but do not save updates to it
            xlTodyWorkbook.Close(SaveChanges:=False)

        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Close Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Close Error", ex.ToString, False, "System Fault")
            MsgBox(ex.ToString)
        End Try


        'CLEAN UP
        MyTodyExcel.Quit()
        releaseObject(xlTodyWorkbook)
        releaseObject(MyTodyExcel)

    End Sub

    Private Sub todayDir()

        ' routine to check if a today directory exists otherwise creat a new one
        PrevPath1 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-1).ToString("dd_MM_yyyy"))
        PrevPath2 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-2).ToString("dd_MM_yyyy"))
        PrevPath3 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-3).ToString("dd_MM_yyyy"))


        todaypath = (My.Settings.dirPacking & "\" & Date.Now.ToString("dd_MM_yyyy"))


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


        Dim day As String
        Dim month As String
        Dim year As String
        Dim gradeTxt As String


        day = SearchDate.Substring(0, 2)
        month = SearchDate.Substring(3, 2)
        year = SearchDate.Substring(8, 2)
        Try
            Select Case frmJobEntry.txtGrade.Text
                Case "A", "ReCheckA"
                    gradeTxt = "A" 'A Grade

            End Select

        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog(" Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Close Error", ex.ToString, False, "System Fault")
            MsgBox(ex.ToString)
        End Try

        searchBarcode = (frmJobEntry.varProductCode & year & month & day & gradeTxt & sheetCount)

    End Sub


End Class
