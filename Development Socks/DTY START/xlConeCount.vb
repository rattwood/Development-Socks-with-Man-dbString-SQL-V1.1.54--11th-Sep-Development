Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Data.SqlClient


Public Class xlConeCount
    'METHOD for CHECKING HOW MANY CHEESE ARE ALREADY SCANNED ON TO A GRADE SHEET AND PASS INFORMATION BACK TO PACKING SCREEN FOR A and ReCheck A
    Private SQLL As New SQLConn

    Dim prodNameMod As String
    Dim savestring As String
    Dim savename As String
    Dim yestname1 As String
    Dim yestname2 As String
    Dim yestname3 As String
    Dim PrevPath1 As String
    Dim PrevPath2 As String
    Dim PrevPath3 As String
    Public TmpGrade As String
    Public sheetName As String

    Dim todaypath As String
    Dim sheetCount As Integer
    Dim SearchDate As String
    Public searchBarcode As String  'THIS IS THE SEARCH STRING FOR SQL ON PACKING A AND REACHECKA SHEETS
    Public sheetSearch As String
    Dim sheetDate As String
    Dim tmp_sheetdate As Date
    Dim prodNum As String
    Public nfree As Integer

    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError


    Public Sub xlCheck()


        TmpGrade = frmJobEntry.txtGrade.Text

        If frmJobEntry.txtGrade.Text = "A" And frmJobEntry.reCheck = 1 Then  'TMPGRADE is set to A but if recheck A we change
            TmpGrade = "ReCheckA"
        End If

        ''CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
        Try

            Select Case TmpGrade'frmJobEntry.txtGrade.Text
                Case "ReCheckA"   '"ReCheckA"
                    'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
                    prodNameMod = frmPackRchkA.DGVPakingRecA.Rows(0).Cells("PRODNAME").Value.ToString
                    prodNameMod = prodNameMod.Replace("/", "_")

                    'CREATE THE SHEET NAME But as this Cheese is from ReCheck we will assign to A grade sheet
                    sheetName = prodNameMod.Substring(prodNameMod.Length - 5) & "_A"

                    'CREATE THE FULL NAME FOR SAVING THE FILE
                    savestring = (prodNameMod & " " _
                    & frmPackRchkA.DGVPakingRecA.Rows(0).Cells("MERGENUM").Value.ToString & "_" _
                    & frmPackRchkA.DGVPakingRecA.Rows(0).Cells("PRNUM").Value.ToString) & " A"

                    'CREATE SQL Search String
                    prodNum = frmPackRchkA.DGVPakingRecA.Rows(0).Cells("PRNUM").Value.ToString
                    sheetSearch = prodNum & "______A"


                Case "A"
                    'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
                    prodNameMod = frmPacking.DGVPakingA.Rows(0).Cells("PRODNAME").Value.ToString
                    prodNameMod = prodNameMod.Replace("/", "_")

                    'CREATE THE SHEET NAME WHICH IS THE 4 LETTER REFRENCE AT THE END OF PRODUCT NAME
                    sheetName = prodNameMod.Substring(prodNameMod.Length - 5) & "_A"

                    'CREATE THE FULL NAME FOR SAVING THE FILE
                    savestring = (prodNameMod & " " _
                    & frmPacking.DGVPakingA.Rows(0).Cells("MERGENUM").Value.ToString & "_" _
                    & frmPacking.DGVPakingA.Rows(0).Cells("PRNUM").Value.ToString) & " A"

                    'CREATE SQL Search String
                    prodNum = frmPacking.DGVPakingA.Rows(0).Cells("PRNUM").Value.ToString
                    sheetSearch = prodNum & "______A"

                Case Else
                    'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
                    prodNameMod = frmDGV.DGVdata.Rows(0).Cells("PRODNAME").Value.ToString
                    prodNameMod = prodNameMod.Replace("/", "_")

                    'CREATE THE SHEET NAME WHICH IS THE 4 LETTER REFRENCE AT THE END OF PRODUCT NAME
                    sheetName = prodNameMod.Substring(prodNameMod.Length - 5) & "_" & frmJobEntry.txtGrade.Text

                    'CREATE THE FULL NAME FOR SAVING THE FILE
                    savestring = (prodNameMod & " " _
                & frmDGV.DGVdata.Rows(0).Cells("MERGENUM").Value.ToString & "_" _
                & frmDGV.DGVdata.Rows(0).Cells("PRNUM").Value.ToString) & " " & frmJobEntry.txtGrade.Text

            End Select



        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Close Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Close Error", ex.ToString, False, "System Fault")
            MsgBox(ex.ToString)
        End Try




        'CALL SUB TO GET TODAYS SAVE DIRECTORY
        todayDir()

        'create the save name of the file
        savename = (todaypath & "\" & savestring & ".xlsx").ToString


        'Create PREVIOUS THREE DAYS CHECK NAMES
        yestname1 = (PrevPath1 & "\" & savestring & ".xlsx").ToString




        'CHECK TO SEE IF THERE IS ALREADY A FILE STARTED FOR PRODUCT NUMBER
        'IN TODATY DIRECTORY
        If File.Exists(savename) Then
            SearchDate = Date.Now.ToString("dd_MM_yyyy")
            getCounts()

            Exit Sub

        Else

            If File.Exists(yestname1) Then      'ONE DAY AGO
                savename = yestname1
                SearchDate = sheetDate 'Date.Now.AddDays(-1).ToString("dd_MM_yyyy")
                getCounts()

                Exit Sub

            End If
        End If

    End Sub

    Private Sub getCounts()
        Dim MyTodyExcel As New Excel.Application
        Dim xlTodyWorkbook As Excel.Workbook


        Try
            'GET SHEET COUNT FOR DOCUMENT SO WE CAN USE TO SEACK SQL AND GET COUNT OF PACKED CHEESE
            xlTodyWorkbook = MyTodyExcel.Workbooks.Open(savename)
            Dim tmpCount As Integer


            sheetCount = xlTodyWorkbook.Worksheets.Count
            For rCount As Integer = 13 To 102
                If MyTodyExcel.Cells(rCount, 4).Value > 0 Then
                    tmpCount = tmpCount + 1
                    Continue For
                Else
                    nfree = tmpCount
                    Exit For
                End If
            Next

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

        SQLL.AddParam("@searchsheet", sheetSearch)
        Dim daysstring As Integer = "-" & My.Settings.searchDays
        SQLL.AddParam("@days", daysstring)

        SQLL.ExecQuery("Select MAX(PACKENDTM) PACKENDTM from jobs where packendtm between DateAdd(DD, @days, GETDATE()) and GetDATE() and (packsheetbcode like  '%' +  @searchsheet  + '%')")

        If SQLL.RecordCount > 0 Then


            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            frmDGVconeCount.DGVconeCount.DataSource = SQLL.SQLDS.Tables(0)
            frmDGVconeCount.DGVconeCount.Rows(0).Selected = True


            If Not IsDBNull(frmDGVconeCount.DGVconeCount.Rows(0).Cells("PACKENDTM").Value) Then
                ' MsgBox(DGVSheetDate.Rows(0).Cells("PACKENDTM").Value.ToString)

                tmp_sheetdate = frmDGVconeCount.DGVconeCount.Rows(0).Cells("PACKENDTM").Value

                sheetDate = tmp_sheetdate.ToString("dd_MM_yyyy")

            End If
        End If


        PrevPath1 = (My.Settings.dirPacking & "\" & sheetDate)

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
