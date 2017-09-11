Public Class InfoStore
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'InsertNewJob()

        'ADD ORA PARAMETERS & RUN THE COMMAND
        'ORA.AddParam("@mcnum", frmJobEntry.varMachineCode)
        'ORA.AddParam("@prodnum", frmJobEntry.varProductCode)
        'ORA.AddParam("@YY", frmJobEntry.varYear)
        'ORA.AddParam("@MM", frmJobEntry.varMonth)
        'ORA.AddParam("@doff", frmJobEntry.varDoffingNum)
        'ORA.AddParam("@cone", modConeNum)
        'ORA.AddParam("@merge", "") 'TODO
        'ORA.AddParam("@user", frmJobEntry.txtOperator.Text)
        'ORA.AddParam("@conestate", "")
        'ORA.AddParam("@shortcone", "")
        'ORA.AddParam("@nocone", "")
        'ORA.AddParam("@defectcone", "")
        'ORA.AddParam("@cartnum", frmJobEntry.varCartSelect)
        'ORA.AddParam("@cartname", cartName)  'TODO
        'ORA.AddParam("@passzero", "")
        'ORA.AddParam("@barley", "")
        'ORA.AddParam("@m10", "")
        'ORA.AddParam("@p10", "")
        'ORA.AddParam("@m30", "")
        'ORA.AddParam("@p30", "")
        'ORA.AddParam("@m50", "")
        'ORA.AddParam("@p50", "")
        'ORA.AddParam("@ledr", "")
        'ORA.AddParam("@ledg", "")
        'ORA.AddParam("@ledb", "")
        'ORA.AddParam("@ciel", "")
        'ORA.AddParam("@ciea", "")
        'ORA.AddParam("@cieb", "")
        'ORA.AddParam("@ciedl", "")
        'ORA.AddParam("@ciede", "")
        'ORA.AddParam("@cartstart", "")
        'ORA.AddParam("@cartend", "")
        'ORA.AddParam("@recheck", "")
        'ORA.AddParam("@rechecktm", "")
        'ORA.AddParam("@barcart", frmJobEntry.txtLotNumber.Text)
        'ORA.AddParam("@barcone", coneBarcode)
        'ORA.AddParam("@fk", "")
        'ORA.AddParam("@fd", "")
        'ORA.AddParam("@ff", "")
        'ORA.AddParam("@fo", "")
        'ORA.AddParam("@ft", "")
        'ORA.AddParam("@fp", "")
        'ORA.AddParam("@fs", "")
        'ORA.AddParam("@fx", "")
        'ORA.AddParam("@fn", "")
        'ORA.AddParam("@fw", "")
        'ORA.AddParam("@fh", "")
        'ORA.AddParam("@ftr", "")
        'ORA.AddParam("@fb", "")
        'ORA.AddParam("@fc", "")
        'ORA.AddParam("@mcname", frmJobEntry.varMachineName)
        'ORA.AddParam("@prodname", frmJobEntry.varProductName)

        'ORA.ExecQuery("INSERT INTO JOBS (MCNUM,PRNUM,PRYY,PRMM,DOFFNUM,CONENUM,MERGENUM,OPNAME,CONESTATE," _
        '        & "SHORTCONE,MISSCONE,DEFCONE,CARTNUM,CARTNAME,CONEZERO,CONEBARLEY,M10,P10,M30,P30,M50," _
        '        & "P50,LEDR,LEDG,LEDB,CIEL,CIEA,CIEB,CIEDL,CIEDE,CARTSTARTTM,CARTENDTM,RECHK,RECHKTM,BCODECART," _
        '        & "BCODECONE, FLT_K, FLT_D, FLT_F, FLT_O, FLT_T, FLT_P, FLT_S, FLT_X, FLT_N, FLT_W, FLT_H, FLT_TR, FLT_B, FLT_C, MCNAME, PRODNAME) " _
        '        & "VALUES (@mcnum, @prodnum,@YY,@MM,@doff,@cone,@merge,@user,@conestate,@shortcone,@nocone,@defectcone,@cartnum,@cartname,@passzero," _
        '        & "@barley,@m10,@p10,@m30,@p30,@m50,@p50,@ledr,@lerg,@ledb,@ciel,@ciea,@cieb,@ciedl,@ciede,@cartstart,@cartend,@recheck,@rechecktm," _
        '        & "@barcart,@barcone,@fk,@fd,@ff,@fo,@ft,@fp,@fs,@fx,@fn,@fw,@fh,@ftr,@fb,@fc,@mcname,@prodname)")






        'REPORT & ABORT ERRORS
        'If ORA.HasException(True) Then Exit Sub


        'How to write to a cell in DataGridView
        'dgvNewData.Item(7, 1).Value = "Test"   'Format is  Item(Column, Row)   if Row and Cells are used  Rows(x) and Cells(y)




        'DATAGRIDVIEW Creat Columns

        'Clear DGV
        'dgvNewData.Rows.Clear()

        'PROPERTIES
        'dgvNewData.SelectionMode = DataGridViewSelectionMode.FullRowSelect      'Always WORK ON FULL ROW
        'dgvNewData.ColumnCount = 52                                             'NUMBER OF COLUMNS
        'Construct the Columns

        'CREATE COLUM HEADERS
        'dgvNewData.Columns(0).Name = "M/C Code"      'machine number from BarCode
        'dgvNewData.Columns(1).Name = "M/C Name"      'machineName
        'dgvNewData.Columns(2).Name = "Prod Code"     'productNum from BarCode
        'dgvNewData.Columns(3).Name = "Prod Name"     'productName  TODO
        'dgvNewData.Columns(4).Name = "Year"          'prodYY  from BarCode
        'dgvNewData.Columns(5).Name = "MM"            'prodMM  from BarCode
        'dgvNewData.Columns(6).Name = "Doff #"        'doffNum  from BarCode
        'dgvNewData.Columns(7).Name = "Cone #"        'spindleNum  from BarCode
        'dgvNewData.Columns(8).Name = "Merge #"       'mergeNum    TODO
        'dgvNewData.Columns(9).Name = "User"          'operatorName   fron entry screen
        'dgvNewData.Columns(10).Name = "Cone State"   'coneState
        'dgvNewData.Columns(11).Name = "Short Cone"   'shortCone
        'dgvNewData.Columns(12).Name = "NoCone"       'missingCone
        'dgvNewData.Columns(13).Name = "Defect Cone"  'defectCone
        'dgvNewData.Columns(14).Name = "Cart #"       'cartNum  from Job Screen
        'dgvNewData.Columns(15).Name = "Cart Name"    'cartName  from BarCode
        'dgvNewData.Columns(16).Name = "Passed Cone Zero Value"  'passCone 
        'dgvNewData.Columns(17).Name = "Cone Barley"   'Cone with large colour defect
        'dgvNewData.Columns(18).Name = "Cone -10"      'coneM10
        'dgvNewData.Columns(19).Name = "Cone +10"      'coneP10
        'dgvNewData.Columns(20).Name = "Cone -30"      'coneM30
        'dgvNewData.Columns(21).Name = "Cone +30"      'coneP30
        'dgvNewData.Columns(22).Name = "Cone -50"      'coneM50
        'dgvNewData.Columns(23).Name = "cone +50"      'coneP50
        'dgvNewData.Columns(24).Name = "varLedR"       'ledR
        'dgvNewData.Columns(25).Name = "varLedG"       'ledG
        'dgvNewData.Columns(26).Name = "varLedB"       'ledB
        'dgvNewData.Columns(27).Name = "varCIE_L"      'CIE_L
        'dgvNewData.Columns(28).Name = "varCIE_a"      'CIE_a
        'dgvNewData.Columns(29).Name = "varCIE_b"      'CIE_b
        'dgvNewData.Columns(30).Name = "varCIE_dL"     'CIE_dL
        'dgvNewData.Columns(31).Name = "varCIE_dE"     'CIE_dE
        'dgvNewData.Columns(32).Name = "varCartStartTime"  'cartStratTime
        'dgvNewData.Columns(33).Name = "varCartEndTime"    'cartEndTime
        'dgvNewData.Columns(34).Name = "reChecked"    'Cone has been reChecked
        'dgvNewData.Columns(35).Name = "reCheckTime"  'Cone has been reChecked
        'dgvNewData.Columns(36).Name = "barCodeCart"  'Cone has been reChecked
        'dgvNewData.Columns(37).Name = "coneBarcode"  'Cone Barcode actual Barcode
        'dgvNewData.Columns(38).Name = "Fault K"     'KEBA Fault
        'dgvNewData.Columns(39).Name = "Fault D"     'DIRTY Fault
        'dgvNewData.Columns(40).Name = "Fault F"     'FORM AB Fault
        'dgvNewData.Columns(41).Name = "Fault O"     'OVERTHROWN Fault
        'dgvNewData.Columns(42).Name = "Fault T"     'TENSION AB. Fault
        'dgvNewData.Columns(43).Name = "Fault P"     'PAPER TUBE AB. Fault
        'dgvNewData.Columns(44).Name = "Fault S"     'SHORT CHEESE Fault
        'dgvNewData.Columns(45).Name = "Fault X"     'No HAVE CHEESE Fault
        'dgvNewData.Columns(46).Name = "Fault N"     'NO TAIL & ABNORMAL Fault
        'dgvNewData.Columns(47).Name = "Fault W"     'WASTE Fault
        'dgvNewData.Columns(48).Name = "Fault H"     'HITTING Fault
        'dgvNewData.Columns(49).Name = "Fault TR"    'TARUMI Fault
        'dgvNewData.Columns(50).Name = "Fault B"     'B- GRADE BY M/C  Fault
        'dgvNewData.Columns(51).Name = "Fault C"     'C- GRADE BY M/C  Fault

        'LOOP THROUGH ROWS OF DATAGRID OR DATABASE AND GET REQUIERD VALUES

        'For Each r As DataRow In frmDGV.DGVdata.Rows
        'chk_K.Checked = r.Item("FLT_K")
        'chk_D.Checked = r.Item("FLT_D")
        'chk_F.Checked = r.Item("FLT_F")
        'chk_O.Checked = r.Item("FLT_O")
        'chk_T.Checked = r.Item("FLT_T")
        'chk_P.Checked = r.Item("FLT_P")
        'chk_S.Checked = r.Item("FLT_S")
        'chk_X.Checked = r.Item("FLT_X")
        'chk_N.Checked = r.Item("FLT_N")
        'chk_W.Checked = r.Item("FLT_W")
        'chk_H.Checked = r.Item("FLT_H")
        'chk_TR.Checked = r.Item("FLT_TR")
        'chk_B.Checked = r.Item("FLT_B")
        'chk_C.Checked = r.Item("FLT_C")
        'Next

        'EXCEL File Handeling

        'Private Sub Form_Initialize()
        'get month
        'MyMonth = Format(Now, "mm")
        'get year
        'MyYear = Format(Now, "yyyy")
        'working directory
        'Mydirectory = "c:\YourDirectory\"
        'Excels extension
        ' MyExtension = ".xls"
        'complete path and file name
        'MyFileName = Mydirectory + MyMonth + "_" + MyYear + MyExtension

        'On Error Resume Next
        'create Excel object
        'Set ExcelApp = CreateObject("Excel.Application")
        'if file exists, place file name in FileCheck
        'FileCheck = Dir$(MyFileName)
        'If FileCheck = MyMonth + "_" + MyYear + MyExtension Then
        'Workbook exists, open it
        'Set ExcelWorkbook = ExcelApp.Workbooks.Open(MyFileName)
        'Set ExcelSheet = ExcelWorkbook.Worksheets(1)
        'Else
        'Workbook doesn't exist, create new workbook
        'Set ExcelWorkbook = ExcelApp.Workbooks.Add
        'Set ExcelSheet = ExcelWorkbook.Worksheets(1)
        'ExcelApp.Columns("A:C").ColumnWidth = 20
        'ExcelSheet.Cells(1, 1).Value = "Your"
        'ExcelSheet.Cells(1, 2).Value = "Columb"
        'ExcelSheet.Cells(1, 3).Value = "Headers"
        'ExcelApp.Range("A1:C1").Select
        'ExcelApp.Selection.Font.Bold = True
        'write some data
        'ExcelSheet.Cells(9, 2).Value = "123"
        'End If
        'End Sub

        ' Private Sub Form_Unload(Cancel As Integer)
        'If FileCheck = MyMonth + "_" + MyYear + MyExtension Then
        'Save existing workbook
        'ExcelWorkbook.Save
        'Else
        'Save new workbook
        'ExcelWorkbook.SaveAs MyFileName
        'End If

        'Close Excel
        'ExcelWorkbook.Close savechanges:=False
        'ExcelApp.Quit
        'Set ExcelApp = Nothing
        'Set ExcelWorkbook = Nothing
        'Set ExcelSheet = Nothing

        'End Sub




        'EXCLE Routines
        ' Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        '        xlApp.DisplayAlerts = False

        'Dim workbook As Excel.Workbook = xlApp.Workbooks.Open(savename, 0, False, 5, "", "",
        '                                                   False, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
        '                                                  True, False, 0, True, False, False)
        'Dim worksheets As Excel.Sheets = workbook.Worksheets
        'Dim xlNewSheet = DirectCast(worksheets.Add(worksheets(1), Type.Missing, Type.Missing, Type.Missing), Excel.Worksheet)

        'xlworkbook.sheets.count()
        'xlNewSheet.Name = "newsheet"
        'xlNewSheet.Cells(1, 1) = "new sheet content"
        'xlNewSheet = workbook.Sheets("sheet1")

        'workbook.Save()
        'workbook.Close()


        'Animated GIF

        'pbSearching.Visible = True
        'pbSearching.BringToFront()
        'Dim sqldatasourceenumerator1 As SqlDataSourceEnumerator = SqlDataSourceEnumerator.Instance
        'Dim datatable1 As DataTable = sqldatasourceenumerator1.GetDataSources()
        'DataGridView1.DataSource = datatable1
        'pbSearching.Visible = False
        'Me.BringToFront()

        ''Another way using Background worker
        'Public Class frmMain

        '    Private progress As frmProgress

        '    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        '        Button1.Enabled = False
        '        progress = New frmProgress
        '        progress.Show()
        '        BackgroundWorker1.WorkerSupportsCancellation = True
        '        BackgroundWorker1.RunWorkerAsync()
        '        Call RefreshGrid  'Here I get data from a remote database server that took some time
        '        BackgroundWorker1.CancelAsync()
        '    End Sub

        '    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        '        ' ... this is running in a different thread! ...
        '        For i As Integer = 1 To 10
        '            System.Threading.Thread.Sleep(1000)
        '            Debug.Print(i)
        '        Next
        '    End Sub

        '    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        '        If Not IsNothing(progress) AndAlso Not progress.IsDisposed Then
        '            progress.Close()
        '        End If
        '        Button1.Enabled = True
        '        MessageBox.Show("Done!")
        '    End Sub

        'End Class

    End Sub
End Class