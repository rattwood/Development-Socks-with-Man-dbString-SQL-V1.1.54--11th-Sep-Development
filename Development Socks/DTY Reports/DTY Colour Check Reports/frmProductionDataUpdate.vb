
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports System.ComponentModel



Public Class frmProductionDataUpdate


    '---------------------------------------    SETTING UP LOCAL INSTANCE FOR SQL LINK FOR DATAGRID TO SYNC CORRECTLY WITH SQL -------------------------------------
    Private LConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private LCmd As SqlCommand

    'SQL CONNECTORS
    Private LDA As SqlDataAdapter
    Private LDS As DataSet
    Private LDT As DataTable
    Private LCB As SqlCommandBuilder

    Private LRecordCount As Integer
    Private LException As String
    ' SQL QUERY PARAMETERS
    Private LParams As New List(Of SqlParameter)




    Dim sp_nums As String
    Dim template As String
    Dim FileInName As String
    Dim MyExcel As New Excel.Application
    Dim abortPrint As Integer = 0
    Dim jobNum As String

    Dim dateDay As String
    Dim dateMMM As String
    Dim dateYY As String
    Dim yyPath As String
    Dim mmmPath As String
    Dim fileOpenName As String
    Dim nfree As Integer  'This will be container for the next row free  
    Dim ncfree As Integer = 2 'This is the location of the first day number on the sheet 
    Dim colcount As Integer
    Dim PilotCount As Integer
    Dim FirstTime As Integer
    'REFRENCE CELL LOCATIONS
    'HEADERS

    Dim xlMMYYHead
    Dim xlMcNumHead
    Dim xlDTYProdHead
    Dim xlMergeNumHead
    Dim xlPOYProdHead
    Dim xlWeightHead

    'VALUE CELLS
    Dim xlDay
    Dim xlDTYProd
    Dim xlTFNum
    Dim xlChecker
    Dim xlDoffNum
    Dim xlTotCheck
    Dim xlP30
    Dim xlM30
    Dim xlAB
    Dim xlPShort
    Dim xlMShort
    Dim xlCartSize
    Dim xlD1ColIdx As Integer
    Dim XLD1NCFree As Integer
    Dim colLetter As String
    'THIS INITIATES WRITING TO ERROR LOG
    Private writeerrorLog As New writeError



    Public Sub sendData()


        jobNum = frmPrintCartReport.DGVcartReport.Rows(1).Cells("BCODEJOB").Value

        checkJobFileExists()

    End Sub




    Public Sub LExecQuery(Query As String)
        ' RESET QUERY STATISTCIS
        LRecordCount = 0
        LException = ""


        If LConn.State = ConnectionState.Open Then LConn.Close()
        Try

            'OPEN SQL DATABSE CONNECTION
            LConn.Open()

            'CREATE SQL COMMAND
            LCmd = New SqlCommand(Query, LConn)

            'LOAD PARAMETER INTO SQL COMMAND
            LParams.ForEach(Sub(p) LCmd.Parameters.Add(p))

            'CLEAR PARAMETER LIST
            LParams.Clear()

            'EXECUTE COMMAND AND FILL DATASET
            LDS = New DataSet
            LDT = New DataTable
            LDA = New SqlDataAdapter(LCmd)

            LRecordCount = LDA.Fill(LDS)

        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("SQL Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("SQL Error", ex.ToString, False, "System Fault")

            LException = "ExecQuery Error: " & vbNewLine & ex.Message
            MsgBox(LException)

        End Try

    End Sub











    'SUBROUTINE TO CHECK IF DAY DIRECTORIES EXIST IF NOT THEY ARE CREATED
    Private Sub checkJobFileExists()

        '11D1 MC MAY'18 751D-72-WF0A TF02    EXAMPLE OF FILE NAME ON CURRENT SYSTEM

        'CREATE SAVE NAME WHICH WILL ALSO BE THE SEARCH NAME
        'saveName = 
        dateDay = Date.Now.ToString("dd")   'GET TODAYS DAY
        dateMMM = Date.Now.ToString("MMM")   'GET TODAYS MONTH
        dateYY = Date.Now.ToString("yy")    'GET TODAYS YEAR
        yyPath = (My.Settings.dirJobs & "\" & Date.Now.ToString("yy").ToString) 'dd_MMM_yyyy
        mmmPath = (yyPath & "\" & Date.Now.ToString("MMM"))

        xlMMYYHead = dateMMM & "'" & dateYY
        xlMcNumHead = frmPrintCartReport.DGVcartReport.Rows(0).Cells("MCNAME").Value.ToString
        xlDTYProdHead = frmPrintCartReport.DGVcartReport.Rows(0).Cells("PRODNAME").Value.ToString
        xlMergeNumHead = frmPrintCartReport.DGVcartReport.Rows(0).Cells("MERGENUM").Value.ToString
        xlDTYProd = frmPrintCartReport.DGVcartReport.Rows(0).Cells("PRODNAME").Value.ToString
        xlTFNum = frmPrintCartReport.DGVcartReport.Rows(0).Cells("MERGENUM").Value.ToString
        xlDoffNum = frmPrintCartReport.DGVcartReport.Rows(0).Cells("DOFFNUM").Value.ToString
        xlChecker = frmPrintCartReport.DGVcartReport.Rows(0).Cells("OPCOLOUR").Value.ToString
        xlWeightHead = frmPrintCartReport.DGVcartReport.Rows(0).Cells("WEIGHT").Value.ToString
        xlPOYProdHead = frmPrintCartReport.DGVcartReport.Rows(0).Cells("WEIGHT").Value.ToString

        'CHANGE IN PRODUCT NAME FORMAT SO IT CAN BE USED IN FILE NAME
        Dim prodNameMod = frmPrintCartReport.DGVcartReport.Rows(0).Cells("PRODNAME").Value.ToString
        prodNameMod = prodNameMod.Replace("/", "")

        '***** Here we need to get information on the  


        fileOpenName = (mmmPath & "\" & xlMcNumHead & " MC " & dateMMM & " " & dateYY & " " &
                prodNameMod & " " & xlTFNum & ".xlsx")




        If Not Directory.Exists(yyPath) Then
            Directory.CreateDirectory(yyPath)
            If Not Directory.Exists(mmmPath) Then
                Directory.CreateDirectory(mmmPath)
            End If
        End If

        If Not Directory.Exists(mmmPath) Then
            Directory.CreateDirectory(mmmPath)
        End If




        If File.Exists(fileOpenName) Then
            DataUpdate()
            MsgBox("Data Update Finished")
            Exit Sub
        End If

        If Not File.Exists(fileOpenName) Then
            DataCreateNewForm()
            DataUpdate()
        End If


    End Sub

    Private Sub DataUpdate()

        Dim MyUpdateExcel As New Excel.Application

        Dim xlUpdateWorkbook As Excel.Workbook
        'Dim xlUpdateSheets As Excel.Worksheet

        Dim missCount As Integer = 0
        Dim WasteCount As Integer = 0
        Dim stdCount As Integer = 0



        ncfree = 2

        'OPEN THE REQUIERD FILE

        xlUpdateWorkbook = MyUpdateExcel.Workbooks.Open(fileOpenName)


        'Select requierd Worksheet 
        CType(MyUpdateExcel.Workbooks(1).Worksheets("Input Data"), Excel.Worksheet).Select()



        '***************************************************** VERSION 2  *****************************************************************************
        '******************************************* CATERS FOR P30,M30,FAB,SAB,-S,+S and Missing *****************************************************
        'Location of first day cell then incremment by  B5
        For i = 1 To 59  '60 occurences of Column groups

            If Not IsNothing(MyUpdateExcel.Cells(6, ncfree).Value) Then
                If MyUpdateExcel.Cells(6, ncfree).Value.ToString = xlDoffNum Then
                    Exit For
                End If
            End If

            If IsNothing(MyUpdateExcel.Cells(3, ncfree).value) Then
                Exit For
            ElseIf MyUpdateExcel.Cells(3, ncfree).value.ToString = " " Then
                Exit For
            Else
                ncfree = ncfree + 8

            End If
        Next


        'Check to see if this is an update if it is do not change the date use original date





        'Update the header infor for Day information
        If IsNothing(MyUpdateExcel.Cells(3, ncfree).Value) Then
            MyUpdateExcel.Cells(3, ncfree) = dateDay  'B3
        ElseIf MyUpdateExcel.Cells(3, ncfree).Value.ToString = " " Then
            MyUpdateExcel.Cells(3, ncfree) = dateDay  'B3
        End If

        ' MyUpdateExcel.Cells(3, ncfree) = dateDay  'B3
        MyUpdateExcel.Cells(4, ncfree) = xlDTYProd 'B4
        MyUpdateExcel.Cells(5, ncfree) = xlTFNum 'B5
        MyUpdateExcel.Cells(5, ncfree + 5) = xlChecker 'G5
        MyUpdateExcel.Cells(6, ncfree) = xlDoffNum 'B6

        'MyUpdateExcel.Visible = True





        'FIND P30 CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE P30 > 0 And FLT_S = 'False'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next

        End If


        'FIND M30 CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE M30 > 0 And FLT_S = 'False'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then

            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)

            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 1) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next

        End If


        'FIND Full AB CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE (conebarley > 0 or M50 > 0 or P50 > 0 or DEFCONE > 0) and FLT_S = 'False'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 2) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next

        End If

        'FIND Full Waste CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE (DYEFLECK > 0 Or COLWASTE > 0) and FLT_S = 'False'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 2) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 2).interior.color = Color.LightGreen
            Next

        End If

        'FIND SAB CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE (conebarley > 0 or M50 > 0 or P50 > 0 or defcone > 0) and FLT_S = 'True'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 3) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next

        End If

        'FIND SAB WASTE CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE (DYEFLECK > 0 Or COLWASTE > 0) and FLT_S = 'True'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 3) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 3).interior.color = Color.LightGreen
            Next

        End If




        'FIND GRADE "A" SHORT
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE FLT_S = 'True' " _
                   & "And P30 = 0 And M30 = 0 And conebarley = 0 and defcone = 0" _
                   & "And BCODEJOB = '" & jobNum & "' ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 4) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next
            ' Else
            '   Exit Sub
        End If

        'FIND PShort
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE P30 > 0 And FLT_S = 'True'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 5) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next

        End If


        'FIND MShort
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE M30 > 0 And FLT_S = 'True'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 6) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next

        End If





        'FIND MISSING CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE misscone > 0 And bcodejob = '" & jobNum & "' ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 7) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance
                missCount = missCount + 1  'count of toatl missing cheese
            Next

        End If


        'FIND STD CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE STDSTATE Between 1 And 9 and  bcodejob = '" & jobNum & "' ")

        If LRecordCount > 0 Then
            stdCount = LRecordCount
        End If

        'FIND WASTE CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE FLT_W = 'True' and  bcodejob = '" & jobNum & "' ")

        If LRecordCount > 0 Then
            WasteCount = LRecordCount
        End If

        'Get total number of cheese in job
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT * FROM jobs WHERE  bcodejob = '" & jobNum & "' ")

        If LRecordCount > 0 Then
            PilotCount = LRecordCount
        End If


        'UPDATE TOTAL CEHECK VALUE
        Select Case xlMcNumHead
            Case "Pilot"
                MyUpdateExcel.Cells(6, ncfree + 5) = 12 - (missCount + stdCount)
                'xlCartSize = (7 + 11)

            Case "31D1", "31D2", "32D1", "32D2"
                MyUpdateExcel.Cells(6, ncfree + 5) = 144 - (missCount + stdCount)
                'xlCartSize = (7 + 143)  'Calculate new value of last cell for counting grade A

            Case Else
                MyUpdateExcel.Cells(6, ncfree + 5) = 192 - (missCount + stdCount)
                'xlCartSize = (7 + 191)  'Calculate new value of last cell for counting grade A
        End Select

        'If Not (FirstTime) Then


        '    Dim passWord As String = "2813"
        '    'Select requierd Worksheet 
        '    CType(MyUpdateExcel.Workbooks(1).Worksheets("D1"), Excel.Worksheet).Select()

        '    MyUpdateExcel.Run("UnProtect", passWord)


        '    xlD1ColNum() 'Get the D! Column number to write to

        '    Dim sizeCalc As String = "=COUNTBLANK(" & colLetter & "7:" & colLetter & xlCartSize.ToString & ")"  'Create string for A Calc range

        '    MyUpdateExcel.Range("C204:BJ204").Value = sizeCalc  'Writes the A count calculation to all cells

        '    MyUpdateExcel.Run("Protect", passWord)

        '    '*****************************************************************************************************************************************




        'End If






        Try

            'Save changes to new file in Paking Dir
            MyUpdateExcel.DisplayAlerts = False
            xlUpdateWorkbook.SaveAs(Filename:=fileOpenName, FileFormat:=51)
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Save Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Save Error", ex.ToString, False, "System Fault")

            MsgBox(ex.Message)
        End Try



        'CLEAN UP
        MyUpdateExcel.Quit()
        releaseObject(xlUpdateWorkbook)
        releaseObject(MyUpdateExcel)



    End Sub

    Private Sub xlD1ColNum()
        XLD1NCFree = (ncfree / 8) + 1

        If XLD1NCFree < 2 Then XLD1NCFree = 1

        xlD1ColIdx = XLD1NCFree + 2 'This calculates which Column we need to write to

        Select Case XLD1NCFree

            Case 1
                colLetter = "C"
            Case 2
                colLetter = "D"
            Case 3
                colLetter = "E"
            Case 4
                colLetter = "F"
            Case 5
                colLetter = "G"
            Case 6
                colLetter = "H"
            Case 7
                colLetter = "I"
            Case 8
                colLetter = "J"
            Case 9
                colLetter = "K"
            Case 10
                colLetter = "L"
            Case 11
                colLetter = "M"
            Case 12
                colLetter = "N"
            Case 13
                colLetter = "O"
            Case 14
                colLetter = "p"
            Case 15
                colLetter = "Q"
            Case 16
                colLetter = "R"
            Case 17
                colLetter = "S"
            Case 18
                colLetter = "T"
            Case 19
                colLetter = "U"
            Case 20
                colLetter = "V"
            Case 21
                colLetter = "W"
            Case 22
                colLetter = "X"
            Case 23
                colLetter = "Y"
            Case 24
                colLetter = "Z"
            Case 25
                colLetter = "AA"
            Case 26
                colLetter = "AB"
            Case 27
                colLetter = "AC"
            Case 28
                colLetter = "AD"
            Case 29
                colLetter = "AE"
            Case 30
                colLetter = "AF"
            Case 31
                colLetter = "AG"
            Case 32
                colLetter = "AH"
            Case 33
                colLetter = "AI"
            Case 34
                colLetter = "AJ"
            Case 35
                colLetter = "AK"
            Case 36
                colLetter = "AL"
            Case 37
                colLetter = "AM"
            Case 38
                colLetter = "AN"
            Case 39
                colLetter = "AO"
            Case 40
                colLetter = "AP"
            Case 41
                colLetter = "AQ"'
            Case 42
                colLetter = "AR"
            Case 43
                colLetter = "AS"
            Case 44
                colLetter = "AT"
            Case 45
                colLetter = "AU"
            Case 46
                colLetter = "AV"
            Case 47
                colLetter = "AW"
            Case 48
                colLetter = "AX"
            Case 49
                colLetter = "AY"
            Case 50
                colLetter = "AZ"
            Case 51
                colLetter = "BA"
            Case 52
                colLetter = "BC"
            Case 53
                colLetter = "BD"
            Case 54
                colLetter = "BE"
            Case 55
                colLetter = "BF"
            Case 56
                colLetter = "BG"
            Case 57
                colLetter = "BH"
            Case 58
                colLetter = "BI"
            Case 59
                colLetter = "BJ"
            Case Else
                colLetter = "C"

        End Select

    End Sub


    Private Sub DataCreateNewForm()

        Dim MyCreateExcel As New Excel.Application

        '  Dim nfree As Integer  'This will be container for the next row free  
        Dim ncfree As Integer 'This will be container for the next column free  
        '  Dim colcount As Integer
        Dim xlCreateWorkbook As Excel.Workbook
        ' Dim xlCreateSheets As Excel.Worksheet

        Dim missCount As Integer = 0
        'Dim WasteCount As Integer = 0
        Dim stdCount As Integer = 0



        template = (My.Settings.dirTemplate & "\" & "PROD REPORT TEMPLATE ANY MACHINE.xlsm")
        'OPEN A NEW WORKSHEET
        xlCreateWorkbook = MyCreateExcel.Workbooks.Open(template)


        'Select requierd Worksheet 
        CType(MyCreateExcel.Workbooks(1).Worksheets("Input Data"), Excel.Worksheet).Select()


        'CREATE HEADER INFORMATION  row, Col
        MyCreateExcel.Cells(1, 1) = xlMMYYHead   'A1
        MyCreateExcel.Cells(2, 1) = ("'" & "M/C:" & xlMcNumHead)  'A2
        MyCreateExcel.Cells(2, 2) = (xlDTYProdHead & " " & xlTFNum)  'B2
        MyCreateExcel.Cells(2, 10) = ("POY: " & xlPOYProdHead)  'G2
        MyCreateExcel.Cells(2, 18) = ("Weight: " & xlWeightHead & "Kg") 'L2



        'Count Missing Cheese
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE misscone > 0 And bcodejob = '" & jobNum & "' ")
        If LRecordCount > 0 Then
            missCount = LRecordCount

        End If

        'Count STD CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE STDSTATE Between 1 And 9 and  bcodejob = '" & jobNum & "' ")

        If LRecordCount > 0 Then
            stdCount = LRecordCount
        End If

        'Get total number of cheese in job
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT * FROM jobs WHERE  bcodejob = '" & jobNum & "' ")

        If LRecordCount > 0 Then
            PilotCount = LRecordCount
        End If

        'Select Case xlMcNumHead
        '    Case "Pilot"
        '        MyCreateExcel.Cells(6, ncfree + 5) = 12 - (missCount + stdCount)
        '    Case "30D1", "30D2", "31D1", "31D2", "32D1", "31D2", "32D1", "31D2"
        '        MyCreateExcel.Cells(6, ncfree + 5) = 144 - (missCount + stdCount)
        '    Case Else
        '        MyCreateExcel.Cells(6, ncfree + 5) = 192 - (missCount + stdCount)
        'End Select




        Dim passWord As String = "2813"
        'Select requierd Worksheet 
        CType(MyCreateExcel.Workbooks(1).Worksheets("D1"), Excel.Worksheet).Select()

        MyCreateExcel.Run("UnProtect", passWord)
        Dim tmpMCnum = frmPrintCartReport.DGVcartReport.Rows(0).Cells("MCNUM").Value.ToString
        Dim offSet As String = 0

        Select Case tmpMCnum
            Case 21, 23, 25, 27, 29
                offSet = 1
            Case 22, 24, 26, 28
                offSet = 193
            Case 30, 32
                offSet = 1
            Case 31, 32
                offSet = 145

        End Select

        For i = 1 To 192
            MyCreateExcel.Cells(i + 6, 2) = (i - 1) + offSet
        Next

        'UPDATE TOTAL CEHECK VALUE
        Select Case xlMcNumHead
            Case "Pilot"
                xlCartSize = (7 + 11)
            Case "31D1", "31D2", "32D1", "32D2"
                xlCartSize = (7 + 143)  'Calculate new value of last cell for counting grade A
            Case Else
                xlCartSize = (7 + 191)  'Calculate new value of last cell for counting grade A
        End Select

        xlD1ColNum() 'Get the D! Column number to write to

        Dim sizeCalc As String = "=COUNTBLANK(" & colLetter & "7:" & colLetter & xlCartSize.ToString & ")"  'Create string for A Calc range

        MyCreateExcel.Range("C204:BJ204").Value = sizeCalc  'Writes the A count calculation to all cells

        MyCreateExcel.Run("Protect", passWord)

        '*****************************************************************************************************************************************


        Try

            'Save changes to new file in Paking Dir
            MyCreateExcel.DisplayAlerts = False
            xlCreateWorkbook.SaveAs(Filename:=fileOpenName, FileFormat:=51)
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Save Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Save Error", ex.ToString, False, "System Fault")
            MsgBox(ex.Message)
        End Try





        'CLOSE THE TEMPLATE FILE 
        Try
            'Save changes to new file in Paking Dir
            MyCreateExcel.DisplayAlerts = False
            xlCreateWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            'Write error to Log File
            writeerrorLog.writelog("File Close Error", ex.Message, False, "System Fault")
            writeerrorLog.writelog("File Close Error", ex.ToString, False, "System Fault")
            MsgBox(ex.Message)
        End Try

        'CLEAN UP
        MyCreateExcel.Quit()
        releaseObject(MyCreateExcel)
        releaseObject(xlCreateWorkbook)

        FirstTime = 1  'Flag so that updatdata section does not recalculate the equation for A count


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

    Private Sub frmSendData_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class