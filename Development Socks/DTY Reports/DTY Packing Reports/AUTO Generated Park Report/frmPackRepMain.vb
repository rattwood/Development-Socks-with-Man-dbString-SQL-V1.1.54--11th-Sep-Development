Imports System.IO

Public Class frmPackRepMain

    'STRINGS
    Dim prodNameMod As String
    Dim saveString As String
    Dim yestname1 As String
    Dim yestname2 As String
    Dim yestname3 As String

    Public prevDays As String
    Public sheetName As String
    Public savename As String
    Public template As String
    Public prevDaysName As String

    'DIRECTORY PATHS ALL PUBLIC
    Public finPath As String
    Dim todayPath As String
    Dim PrevPath1 As String
    Dim PrevPath2 As String
    Dim PrevPath3 As String



    Public Sub PackRepMainSub()




        'CREATE PRODUCT NAME STRING USED WHEN SAVING FILE
        prodNameMod = frmDGV.DGVdata.Rows(0).Cells(52).Value.ToString
        prodNameMod = prodNameMod.Replace("/", "_")

        If frmJobEntry.txtGrade.Text = "ReCheckA" Then

            'CREATE THE SHEET NAME But as this Cheese is from ReCheck we will assign to A grade sheet
            sheetName = prodNameMod.Substring(prodNameMod.Length - 4) & "_A"

            'CREATE THE FULL NAME FOR SAVING THE FILE
            saveString = (prodNameMod & " " _
                & frmDGV.DGVdata.Rows(0).Cells(7).Value.ToString & "_" _
                & frmDGV.DGVdata.Rows(0).Cells(2).Value.ToString) & " A"
        Else
            'CREATE THE SHEET NAME WHICH IS THE 4 LETTER REFRENCE AT THE END OF PRODUCT NAME
            sheetName = prodNameMod.Substring(prodNameMod.Length - 4) & "_" & frmJobEntry.txtGrade.Text

            'CREATE THE FULL NAME FOR SAVING THE FILE
            saveString = (prodNameMod & " " _
                & frmDGV.DGVdata.Rows(0).Cells(7).Value.ToString & "_" _
                & frmDGV.DGVdata.Rows(0).Cells(2).Value.ToString) & " " & frmJobEntry.txtGrade.Text
        End If



        ''CREATE THE FULL NAME FOR SAVING THE FILE
        'saveString = (prodNameMod & " " _
        '    & frmDGV.DGVdata.Rows(0).Cells(7).Value.ToString & "_" _
        '    & frmDGV.DGVdata.Rows(0).Cells(2).Value.ToString) & " " & frmJobEntry.txtGrade.Text


        'CALL SUB TO GET TODAYS SAVE DIRECTORY
        todayDir()



        'create the save name of the file
        savename = (todayPath & "\" & saveString & ".xlsx").ToString


        'SELECT CORRECT PRINT TEMPLATE

        Select Case frmJobEntry.txtGrade.Text
            Case "A", "ReCheckA"
                template = (My.Settings.dirTemplate & "\" & "PackingTemplate.xlsx").ToString
            Case "B"
                template = (My.Settings.dirTemplate & "\" & "Packing Template Grade B.xlsx").ToString
            Case "AL"
                template = (My.Settings.dirTemplate & "\" & "Packing Template Grade AL.xlsx").ToString
            Case "AD"
                template = (My.Settings.dirTemplate & "\" & "Packing Template Grade AD.xlsx").ToString
            Case "Waste"
                template = (My.Settings.dirTemplate & "\" & "Packing Template Grade B.xlsx").ToString
            Case "P15 AS"
                template = (My.Settings.dirTemplate & "\" & "Packing P15 AS Template.xlsx").ToString
            Case "P25 AS"
                template = (My.Settings.dirTemplate & "\" & "Packing P25 AS Template.xlsx").ToString
            Case "P35 AS"
                template = (My.Settings.dirTemplate & "\" & "Packing P35 AS Template.xlsx").ToString
            Case "P20 BS"
                template = (My.Settings.dirTemplate & "\" & "Packing P20 BS Template.xlsx").ToString
            Case "P30 BS"
                template = (My.Settings.dirTemplate & "\" & "Packing P30 BS Template.xlsx").ToString
            Case "P35 BS"
                template = (My.Settings.dirTemplate & "\" & "Packing P35 BS Template.xlsx").ToString
            Case "ReCheck"
                template = (My.Settings.dirTemplate & "\" & "Recheck Template.xlsx").ToString
            Case "Round1", "Round2", "Round3", "STD" 'FORM FOR STANDARd COMPARE FROM SORT
                template = (My.Settings.dirTemplate & "\" & "STDCompare Template.xlsx").ToString
            Case "Pilot 6Ch"
                template = (My.Settings.dirTemplate & "\" & "PILOT 6Ch..xlsx").ToString
            Case "Pilot 15Ch"
                template = (My.Settings.dirTemplate & "\" & "PILOT 15Ch..xlsx").ToString
            Case "Pilot 20Ch"
                template = (My.Settings.dirTemplate & "\" & "PILOT 20Ch..xlsx").ToString

        End Select



        'Create PREVIOUS THREE DAYS CHECK NAMES
        yestname1 = (PrevPath1 & "\" & saveString & ".xlsx").ToString
        yestname2 = (PrevPath2 & "\" & saveString & ".xlsx").ToString
        yestname3 = (PrevPath3 & "\" & saveString & ".xlsx").ToString

        'CHECK TO SEE IF THE TEMPLATE DIRECTORY HAS A REFRENCE OTHERWISE QUIT
        If template = "" Then
            MsgBox("Please set template file location in Settings")
            Me.Close()
            frmJobEntry.Show()
        End If


        'CHECK TO SEE IF THERE IS ALREADY A FILE STARTED FOR PRODUCT NUMBER
        'IN TODATY DIRECTORY
        If File.Exists(savename) Then

            Select Case frmJobEntry.txtGrade.Text

                Case "A", "ReCheckA"
                    frmPackTodayUpdate.TodayUpdate()
                Case "B", "AD", "AL", "Waste"
                    frmPackTodayUpdate.TodayUpdateB_AL_AD()
                Case "P35 AS", "P35 BS"
                    frmPackTodayUpdate.TodatUpdateBS_AS_35()
                Case "P25 AS", "P30 BS"
                    frmPackTodayUpdate.TodayUpdateBS_AS_30()
                Case "P15 AS", "P20 BS"
                    frmPackTodayUpdate.TodayUpdateBS_AS_20()
                Case "ReCheck"
                    frmPackTodayUpdate.todayUpdate_ReCheck()
                Case "Round1", "Round2", "Round3", "STD" 'FORM FOR STANDARE COMPARE FROM SORT
                    frmPackTodayUpdate.todayUpdate_STD()
                Case "Pilot 6Ch"
                    frmPackTodayUpdate.todayUpdate_pilot6()
                Case "Pilot 15Ch"
                    frmPackTodayUpdate.todayUpdate_pilot15()
                Case "Pilot 20Ch"
                    frmPackTodayUpdate.todayUpdate_pilot20()

            End Select

            frmPackTodayUpdate.Close()
            Exit Sub


        Else

            If frmJobEntry.txtGrade.Text <> "ReCheck" And frmJobEntry.txtGrade.Text <> "Round1" And frmJobEntry.txtGrade.Text <> "Round2" And frmJobEntry.txtGrade.Text <> "Round3" And frmJobEntry.txtGrade.Text <> "STD" Then  'IF RECHECK DO NOT GET SHEETS FROM PREVIOUS DAY

                If File.Exists(yestname1) Then      'ONE DAY AGO
                    prevDaysName = yestname1
                    prevDays = Date.Now.AddDays(-1).ToString("ddMMyyyy")
                    'MsgBox("I am ready to continue with sheet from Yesterday  " & prevDays)
                    frmPackPrvGet.PrvGet()
                    Me.Close()
                ElseIf File.Exists(yestname2) Then  'TWO DAYS AGO
                    prevDaysName = yestname2
                    prevDays = Date.Now.AddDays(-2).ToString("ddMMyyyy")
                    'MsgBox("I am ready to continue with sheet from Two days ago  " & prevDays)
                    frmPackPrvGet.PrvGet()
                    Me.Close()
                ElseIf File.Exists(yestname3) Then  'THREE DAYS AGO
                    prevDaysName = yestname3
                    prevDays = Date.Now.AddDays(-3).ToString("ddMMyyyy")
                    'MsgBox("I am ready to continue with sheet from three days ago  " & prevDays)
                    frmPackPrvGet.PrvGet()
                    Me.Close()
                Else
                    'MsgBox("I am ready to create a new sheet")
                    frmPackCreateNew.CreateNew()
                    Me.Close()
                End If
            Else
                'MsgBox("I am ready to create a new sheet")
                frmPackCreateNew.CreateNew()
                Me.Close()
            End If


        End If




    End Sub

    'SUBROUTINE TO CHECK IF DAY DIRECTORIES EXIST IF NOT THEY ARE CREATED
    Private Sub todayDir()

        ' routine to check if a today directory exists otherwise creat a new one
        PrevPath1 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-1).ToString("dd_MM_yyyy"))
        PrevPath2 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-2).ToString("dd_MM_yyyy"))
        PrevPath3 = (My.Settings.dirPacking & "\" & Date.Now.AddDays(-3).ToString("dd_MM_yyyy"))


        todayPath = (My.Settings.dirPacking & "\" & Date.Now.ToString("dd_MM_yyyy"))
        finPath = (My.Settings.dirPackReports & "\" & Date.Now.ToString("dd_MM_yyyy"))

        If Not Directory.Exists(todayPath) Then
            Directory.CreateDirectory(todayPath)
        End If

        If Not Directory.Exists(finPath) Then
            Directory.CreateDirectory(finPath)
        End If

    End Sub

    Private Sub frmPackRepMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class