'THIS IS THE CLASS FORM THAT PERFORMS QUERY'S


Imports System.Data.SqlClient


Public Class SQLConn
    'SQL CONNECTION

    Private SQLConn As New SqlConnection(My.Settings.SQLConn) 'This need to be changed in Project/Propertie/Settings
    Private SQLCmd As SqlCommand

    'SQL DATA
    Public SQLDA As SqlDataAdapter
    Public SQLDS As DataSet
    Public SQLDT As DataTable
    'Public SQLCB As SqlCommandBuilder

    ' SQL QUERY PARAMETERS
    Public Params As New List(Of SqlParameter)

    ' SQL QUERY STATISTICS
    Public RecordCount As Integer
    Public Exception As String

    Public Sub Newdb()

    End Sub

    'Allow User to enter new Connection String
    Public Sub Newdb(ConnectionString As String)
        SQLConn = New SqlConnection(ConnectionString)
    End Sub




    Public Sub ExecQuery(Query As String)
        ' RESET QUERY STATISTCIS
        RecordCount = 0
        Exception = ""

        Try

            'OPEN SQL DATABSE CONNECTION
            SQLConn.Open()

            'CREATE SQL COMMAND
            SQLCmd = New SqlCommand(Query, SQLConn)

            'LOAD PARAMETER INTO SQL COMMAND
            Params.ForEach(Sub(p) SQLCmd.Parameters.Add(p))

            'CLEAR PARAMETER LIST
            Params.Clear()

            'EXECUTE COMMAND AND FILL DATASET
            SQLDS = New DataSet
            SQLDT = New DataTable
            SQLDA = New SqlDataAdapter(SQLCmd)

            RecordCount = SQLDA.Fill(SQLDS)

        Catch ex As Exception

            Exception = "ExecQuery Error: " & vbNewLine & ex.Message
            MsgBox(Exception)

        Finally

            If SQLConn.State = ConnectionState.Open Then SQLConn.Close()

        End Try

    End Sub

    ' ADD PARAMS
    Public Sub AddParam(Name As String, Value As Object)
        Dim NewParam As New SqlParameter(Name, Value)
        Params.Add(NewParam)
    End Sub

    ' ERROR CHECKING
    Public Function HasException(Optional Report As Boolean = False) As Boolean
        If String.IsNullOrEmpty(Exception) Then Return False
        If Report = True Then MsgBox(Exception, MsgBoxStyle.Critical, "Exception:")
        Return True
    End Function


End Class
