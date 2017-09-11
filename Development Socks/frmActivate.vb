Imports System.Net.NetworkInformation



Public Class frmActivate
    Dim x As Long
    Dim activationNum


    Private Sub frmActivate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Get the Windows Management Instrumentation object.
        Dim wmi As Object = GetObject("WinMgmts:")

        ' Get the "base boards" (mother boards).
        Dim serial_numbers As String = ""
        Dim mother_boards As Object =
        wmi.InstancesOf("Win32_BaseBoard")
        For Each board As Object In mother_boards
            serial_numbers &= ", " & board.SerialNumber
        Next board
        If serial_numbers.Length > 0 Then serial_numbers =
        serial_numbers.Substring(2)

        MsgBox(serial_numbers)


        x = x * x + 53 / x + 113 * (x / 4)

        TextBox1.Text = x
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        activationNum = x

    End Sub
End Class