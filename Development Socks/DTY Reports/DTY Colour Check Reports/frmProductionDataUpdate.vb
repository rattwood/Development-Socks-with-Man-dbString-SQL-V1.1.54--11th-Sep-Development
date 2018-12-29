
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



    Public Sub sendData()


        jobNum = frmPrintCartReport.DGVcartReport.Rows(1).Cells("BCODEJOB").Value

        'frmDGVSendData.Visible = True


        'CREATE DATE VARIABLES VALUES

        'dateDay = frmDGV.DGVData.Rows(0).Cells("COLENDTM").Value
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
        'MyUpdateExcel.Application.WindowState = Excel.XlWindowState.xlMaximized
        'MyUpdateExcel.Visible = True

        'Select requierd Worksheet 
        CType(MyUpdateExcel.Workbooks(1).Worksheets("Input Data"), Excel.Worksheet).Select()

        'CHECK NEXT FREE DAY CELL AND write in that days Columns


        ''*****************************************  VERSION 1  **************************************************************
        ''********************************  FOR M30,P30,AB,-S and +S Only  *************************************************** 
        ''Location of first day cell then incremment by  B5
        'For i = 1 To 59  '60 occurences of Column groups


        '    If MyUpdateExcel.Cells(6, ncfree).Value = xlDoffNum Or MyUpdateExcel.Cells(3, ncfree).value.ToString = " " Then
        '        Exit For
        '        'ElseIf MyUpdateExcel.Cells(3, ncfree).Value.ToString = " " Or IsNothing(MyUpdateExcel.Cells(3, ncfree).Value.ToString) Then
        '        '    Exit For
        '    Else
        '        ncfree = ncfree + 5
        '    End If

        '    'If MyUpdateExcel.Cells(3, ncfree).Value.ToString = " " Or IsNothing(MyUpdateExcel.Cells(3, ncfree).Value.ToString) Then
        '    '    Exit For
        '    'Else
        '    '    ncfree = ncfree + 5

        '    'End If
        'Next

        ''Update the header infor for Day information
        'MyUpdateExcel.Cells(3, ncfree) = dateDay  'B3
        'MyUpdateExcel.Cells(4, ncfree) = xlDTYProd 'B4
        'MyUpdateExcel.Cells(5, ncfree) = "'" & xlTFNum 'B5
        'MyUpdateExcel.Cells(5, ncfree + 3) = xlChecker 'D5
        'MyUpdateExcel.Cells(6, ncfree) = "'" & xlDoffNum 'B6

        'MyUpdateExcel.Visible = True


        ''FIND P30 CHEESE
        'DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        'LExecQuery("SELECT conenum FROM jobs WHERE P30 > 0 And FLT_S = 'False'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        'If LRecordCount > 0 Then
        '    'LOAD THE DATA FROM dB IN TO THE DATAGRID
        '    DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        '    DGVProdDataSend.DataSource = LDS.Tables(0)
        '    DGVProdDataSend.Rows(0).Selected = True
        '    DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)  'sorts On cone numberchimera4260

        '    For rCount = 1 To DGVProdDataSend.Rows.Count - 1
        '        MyUpdateExcel.Cells((rCount - 1) + 8, ncfree) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

        '    Next

        'End If


        ''FIND M30 CHEESE
        'DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        'LExecQuery("SELECT conenum FROM jobs WHERE M30 > 0 And FLT_S = 'False'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        'If LRecordCount > 0 Then

        '    'LOAD THE DATA FROM dB IN TO THE DATAGRID
        '    DGVProdDataSend.DataSource = LDS.Tables(0)
        '    DGVProdDataSend.Rows(0).Selected = True
        '    DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)  '


        '    For rCount = 1 To DGVProdDataSend.Rows.Count - 1
        '        MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 1) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

        '    Next

        'End If


        ''FIND AB CHEESE
        'DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        'LExecQuery("SELECT conenum,colwaste,flt_s FROM jobs WHERE ((conebarley > 0 or M50 > 0 or P50 > 0 Or ColWaste > 0 and (m30 = 0 and p30 = 0)) OR flt_s = 'True') AND BCODEJOB = '" & jobNum & "'  ORDER BY CONENUM ")

        'If LRecordCount > 0 Then
        '    'LOAD THE DATA FROM dB IN TO THE DATAGRID
        '    DGVProdDataSend.DataSource = LDS.Tables(0)
        '    DGVProdDataSend.Rows(0).Selected = True
        '    DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)  'sorts On cone numberchimera4260

        '    For rCount = 1 To DGVProdDataSend.Rows.Count - 1
        '        MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 2) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance
        '        If DGVProdDataSend.Rows(rCount - 1).Cells(1).Value > 0 Then MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 2).interior.color = Color.LightGreen Else MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 2).interior.color = Color.White
        '        If DGVProdDataSend.Rows(rCount - 1).Cells(2).Value = True Then MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 2).font.color = Color.Red Else MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 2).font.color = Color.Black

        '    Next

        'End If





        ''FIND PShort
        'DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        'LExecQuery("SELECT conenum FROM jobs WHERE P30 > 0 And FLT_S = 'True'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        'If LRecordCount > 0 Then
        '    'LOAD THE DATA FROM dB IN TO THE DATAGRID
        '    DGVProdDataSend.DataSource = LDS.Tables(0)
        '    DGVProdDataSend.Rows(0).Selected = True
        '    DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)  'sorts On cone numberchimera4260

        '    For rCount = 1 To DGVProdDataSend.Rows.Count - 1
        '        MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 3) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

        '    Next

        'End If


        ''FIND MShort
        'DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        'LExecQuery("SELECT conenum FROM jobs WHERE M30 > 0 And FLT_S = 'True'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        'If LRecordCount > 0 Then
        '    'LOAD THE DATA FROM dB IN TO THE DATAGRID
        '    DGVProdDataSend.DataSource = LDS.Tables(0)
        '    DGVProdDataSend.Rows(0).Selected = True
        '    DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)  'sorts On cone numberchimera4260

        '    For rCount = 1 To DGVProdDataSend.Rows.Count - 1
        '        MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 4) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

        '    Next

        'End If

        ''****************************************************** END OF VERSION 1 **********************************************************************





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

        MyUpdateExcel.Visible = True





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
        LExecQuery("SELECT conenum FROM jobs WHERE (conebarley > 0 or M50 > 0 or P50 > 0) and FLT_S = 'False'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 2) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next

        End If

        'FIND SAB CHEESE
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE (conebarley > 0 or M50 > 0 or P50 > 0) and FLT_S = 'True'  and BCODEJOB = '" & jobNum & "' ORDER BY CONENUM ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 3) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next

        End If



        'FIND GRADE "A" SHORT
        DGVProdDataSend.DataSource = Nothing  'THIS CLEARS ANY OLD DATA OUT OF DGV
        LExecQuery("SELECT conenum FROM jobs WHERE FLT_S = 'True' " _
                   & "And P30 = 0 And M30 = 0 And conebarley = 0" _
                   & "And BCODEJOB = '" & jobNum & "' ")

        If LRecordCount > 0 Then
            'LOAD THE DATA FROM dB IN TO THE DATAGRID
            DGVProdDataSend.DataSource = LDS.Tables(0)
            DGVProdDataSend.Rows(0).Selected = True
            DGVProdDataSend.Sort(DGVProdDataSend.Columns(0), ListSortDirection.Ascending)
            For rCount = 1 To DGVProdDataSend.Rows.Count - 1
                MyUpdateExcel.Cells((rCount - 1) + 8, ncfree + 4) = DGVProdDataSend.Rows(rCount - 1).Cells(0).Value.ToString 'Start on row 8 and advance

            Next
        Else
            Exit Sub
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


        'UPDATE TOTAL CEHECK VALUE
        MyUpdateExcel.Cells(6, ncfree + 5) = 192 - (missCount + stdCount)


        '************************************************** END OF VERSION 2 ***************************************************************************



        Try

            'Save changes to new file in Paking Dir
            MyUpdateExcel.DisplayAlerts = False
            xlUpdateWorkbook.SaveAs(Filename:=fileOpenName, FileFormat:=51)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        'CLEAN UP
        MyUpdateExcel.Quit()
        releaseObject(xlUpdateWorkbook)
        releaseObject(MyUpdateExcel)






    End Sub

    Private Sub DataCreateNewForm()

        Dim MyCreateExcel As New Excel.Application

        Dim nfree As Integer  'This will be container for the next row free  
        Dim ncfree As Integer 'This will be container for the next column free  
        Dim colcount As Integer
        Dim xlCreateWorkbook As Excel.Workbook
        Dim xlCreateSheets As Excel.Worksheet

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

        Dim passWord As String = "2813"
        'Select requierd Worksheet 
        CType(MyCreateExcel.Workbooks(1).Worksheets("D1"), Excel.Worksheet).Select()

        MyCreateExcel.Run("UnProtect", passWord)
        Dim tmpMCnum = frmPrintCartReport.DGVcartReport.Rows(0).Cells("MCNUM").Value.ToString
        Dim offSet As String = 0

        Select Case tmpMCnum
            Case 21, 23, 25, 27
                offSet = 1
            Case 22, 24, 26, 28
                offSet = 193
        End Select

        For i = 1 To 192
            MyCreateExcel.Cells(i + 6, 2) = (i - 1) + offSet
        Next


        MyCreateExcel.Run("Protect", passWord)

        Try

            'Save changes to new file in Paking Dir
            MyCreateExcel.DisplayAlerts = False
            xlCreateWorkbook.SaveAs(Filename:=fileOpenName, FileFormat:=51)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        'CLOSE THE TEMPLATE FILE 
        Try
            'Save changes to new file in Paking Dir
            MyCreateExcel.DisplayAlerts = False
            xlCreateWorkbook.Close(SaveChanges:=False)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'CLEAN UP
        MyCreateExcel.Quit()
        releaseObject(MyCreateExcel)
        releaseObject(xlCreateWorkbook)




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