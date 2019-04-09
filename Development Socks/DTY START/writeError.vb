Public Class writeError
    'Method to turn logging on/off via settings
    Dim write_log As Boolean = My.Settings.chkUseLogs

    Public Sub writelog(ByVal title As String, ByVal logdata As String, ByVal isError As Boolean, ByVal logname As String)

        Try
            Dim data_con As String
            data_con = "<table border=""1"" bordercolor=""#0099FF"" style=""background-color:#FFFFFF"" width=""100%"" cellpadding=""2"" cellspacing=""2"">" & vbCrLf &
                "<tr>" & vbCrLf
            If isError = True Then
                data_con = data_con & "<td WIDTH=""15%"" bgcolor=""#FFFF99"" bordercolor=""RED""><b><FONT COLOR=""RED"">" & title & "</font></b></td>" & vbCrLf
                data_con = data_con & "<td WIDTH=""70%"" bgcolor=""#FFFF99"" bordercolor=""RED"">" & logdata & "</td>" & vbCrLf &
                              "<td WIDTH=""15%"" bgcolor=""#FFFF99"" bordercolor=""RED"">" & DateAndTime.Now & "</td>" & vbCrLf &
                              "</tr></table>"
            Else
                data_con = data_con & "<td WIDTH=""15%""><b><FONT COLOR=""GREEN"">" & title & "</font></b></td>" & vbCrLf
                data_con = data_con & "<td WIDTH=""70%"">" & logdata & "</td>" & vbCrLf &
                     "<td WIDTH=""15%"">" & DateAndTime.Now & "</td>" & vbCrLf &
                              "</tr></table>"
            End If

            My.Computer.FileSystem.WriteAllText(My.Settings.dirLogs & "/" & logname & ".html", data_con & vbCrLf, True)

        Catch ex As Exception
        End Try

    End Sub

End Class
