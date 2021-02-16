Public Class frmMeasure

    'Dim dC, Blue, BlueGreen, Green, GreenYellow, Yellow, YellowRed, Red, RedBlue As String
    Dim removeChar() As Char = {"<", "0", "0", ">", vbCrLf}
    Public Original_incoming As String


    Private Sub scnMeasuer_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim comerrormsg As String
        comerrormsg = "Please Select Go to SETTINGS and select Com Port"

        Me.ConeNumTextBox.Text = frmCart1.varConeNum


        Me.btnSave.Visible = False 'Hide Save button when form opens
        Me.btnReMeasuer.Visible = False  'Hide Cancel button when form opens
        Me.btnManual.Visible = False
        Me.btnSettings.Visible = False
        Me.btnReMeasuer.Visible = False




        If Label4.Text = "" Then
            Label4.Text = comerrormsg
            Me.btnSettings.Visible = True
        End If




    End Sub

    'Private Sub btnMeasure_Click(sender As Object, e As EventArgs) Handles btnMeasure.Click

    'If VeriColorCom.IsOpen = False Then
    'VeriColorCom.Open()
    ' End If

    ' VeriColorCom.WriteLine("ma")
    ' VeriColorCom.WriteLine("01gr")

    'frmDelay.Show()
    ' MeaDisplay()

    'OutputRichTextBox1.Text = Original_incoming




    ' Me.btnSave.Visible = True 'Show Save button when form opens
    'Me.btnReMeasuer.Visible = True  'Show Cancel button when form opens
    'Me.btnManual.Visible = True
    ' Me.btnMeasure.Enabled = False


    'Original_incoming = ""  'Clear out existing data in Original_incoming String



    'End Sub

    Private Sub MeaDisplay()                                    'Display measure results



        Original_incoming = Original_incoming.TrimStart(removeChar)
        Original_incoming = Original_incoming.TrimEnd(removeChar)

        Dim dC As String = ""
        Dim Blue As String = ""
        Dim BlueGreen As String = ""
        Dim Green As String = ""
        Dim GreenYellow As String = ""
        Dim Yellow As String = ""
        Dim YellowRed As String = ""
        Dim Red As String = ""
        Dim RedBlue As String = ""

        Dim strArray() As String
        Dim intCount As Integer


        strArray = Split(Original_incoming, ",")

        For intCount = 0 To UBound(strArray)
            dC = strArray(0)
            Blue = strArray(1)
            BlueGreen = strArray(2)
            Green = strArray(3)
            GreenYellow = strArray(4)
            Yellow = strArray(5)
            YellowRed = strArray(6)
            Red = strArray(7)
            strArray(8) = strArray(8).TrimEnd(removeChar)
            RedBlue = strArray(8) / 100
        Next



        deltaC.Text = dC / 100  'Display dC value with rescale
        'Blue = Blue * 2.55 
        'Green = Green * 2.55 
        'Red = Red * 2.55 

        ' Color Maths
        Dim var_R, var_G, var_B As String
        Dim var_X, var_Y, var_Z As String
        Dim X, Y, Z As String
        ' Dim ref_X, ref_Y, Ref_Z As String
        Dim CIEbat_L, CIEbat_a, CIEbat_b As String
        Dim CIEdelta_L, CIEdelta_E As String
        Dim CIEstd_L, CIEstd_a, CIEstd_b As String

        CIEstd_L = 38.87
        CIEstd_a = -7.11
        CIEstd_b = -37.26

        var_R = (Red / 255) ' R from 0 To 255
        var_G = (Green / 255) ' G from 0 To 255
        var_B = (Blue / 255) ' B from 0 To 255



        If (var_R > 0.04045) Then var_R = ((var_R + 0.055) / 1.055) ^ 2.4 Else var_R = var_R / 12.92
        If (var_G > 0.04045) Then var_G = ((var_G + 0.055) / 1.055) ^ 2.4 Else var_G = var_G / 12.92
        If (var_B > 0.04045) Then var_B = ((var_B + 0.055) / 1.055) ^ 2.4 Else var_B = var_B / 12.92

        var_R = var_R * 100
        var_G = var_G * 100
        var_B = var_B * 100

        '//Observer. = 2°, Illuminant = D65
        X = var_R * 0.4124 + var_G * 0.3576 + var_B * 0.1805
        Y = var_R * 0.2126 + var_G * 0.7152 + var_B * 0.0722
        Z = var_R * 0.0193 + var_G * 0.1192 + var_B * 0.9505


        'xyzToCIELab()

        var_X = X / 94.811   'ref_X = 95.047   Observer= 2°, Illuminant= D65    10° 94.811
        var_Y = Y / 100.0    'ref_Y = 100.0                                         100
        var_Z = Z / 107.304 'ref_Z = 108.883                                       107.304

        If (var_X > 0.008856) Then var_X = var_X ^ (1 / 3) Else var_X = (7.787 * var_X) + (16 / 116)
        If (var_Y > 0.008856) Then var_Y = var_Y ^ (1 / 3) Else var_Y = (7.787 * var_Y) + (16 / 116)
        If (var_Z > 0.008856) Then var_Z = var_Z ^ (1 / 3) Else var_Z = (7.787 * var_Z) + (16 / 116)

        CIEbat_L = (116 * var_Y) - 16
        CIEbat_a = 500 * (var_X - var_Y)
        CIEbat_b = 200 * (var_Y - var_Z)


        ' Delta CIE L
        CIEdelta_L = CIEbat_L - CIEstd_L                   'reversed as Toray take std away from batch so if batch is lighter result is negative

        'CIE Delta E
        CIEdelta_E = Math.Sqrt(((CIEstd_L - CIEbat_L) ^ 2) + ((CIEstd_a - CIEbat_a) ^ 2) + ((CIEstd_b - CIEbat_b) ^ 2))

        Blue = Blue / 100
        Green = Green / 100
        Red = Red / 100

        'ReScale outputs
        'CIEbat_L = CIEbat_L
        'CIEbat_a = CIEbat_a
        'CIEbat_b = CIEbat_b
        'CIEdelta_L = CIEdelta_L
        'CIEdelta_E = CIEdelta_E


        'to Display sample colour conver strings to Integers
        Dim RedI As Integer = CInt(Red)
        Dim GreenI As Integer = CInt(Green)
        Dim BlueI As Integer = CInt(Blue)



        Label7.Text = CIEbat_L
        Label8.Text = CIEbat_a
        Label9.Text = CIEbat_b

        Label18.Text = CIEdelta_L
        Label19.Text = CIEdelta_E
        Label20.Text = var_R
        Label21.Text = var_G
        Label22.Text = var_B

        btnSampleColour.BackColor = Color.FromArgb(RedI, GreenI, BlueI)  'takes measuerd value in RGB and displays color sample

    End Sub





    Private Sub btnDefect_Click(sender As Object, e As EventArgs)

        Me.btnMeasure.Enabled = False
        Me.btnSave.Visible = True 'Show Save button when form opens
        Me.btnReMeasuer.Visible = True  'Show Cancel button when form opens


    End Sub

    Private Sub btnReMeasure_Click(sender As Object, e As EventArgs) Handles btnReMeasuer.Click

        Me.btnSave.Visible = False 'Hide Save button when form opens
        Me.btnReMeasuer.Visible = False  'Hide Cancel button when form opens
        Me.btnManual.Visible = False
        Me.btnMeasure.Visible = True
        Me.btnMeasure.Enabled = True
        Me.btnManual.Visible = False





    End Sub

    Private Sub btnManual_Click(sender As Object, e As EventArgs) Handles btnManual.Click

        btnM10.Enabled = True
        btnM30.Enabled = True
        btnM50.Enabled = True
        btnP10.Enabled = True
        btnP30.Enabled = True
        btnP50.Enabled = True
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click



        'Set the color of Measuerd button if Spectro used
        If frmCart1.varConeNum = 1 Then
            frmCart1.btnCone1.Enabled = False
            frmCart1.btnCone1.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 2 Then
            frmCart1.btnCone2.Enabled = False
            frmCart1.btnCone2.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 3 Then
            frmCart1.btnCone3.Enabled = False
            frmCart1.btnCone3.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 4 Then
            frmCart1.btnCone4.Enabled = False
            frmCart1.btnCone4.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 5 Then
            frmCart1.btnCone5.Enabled = False
            frmCart1.btnCone5.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 6 Then
            frmCart1.btnCone6.Enabled = False
            frmCart1.btnCone6.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 7 Then
            frmCart1.btnCone7.Enabled = False
            frmCart1.btnCone7.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 8 Then
            frmCart1.btnCone8.Enabled = False
            frmCart1.btnCone8.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 9 Then
            frmCart1.btnCone9.Enabled = False
            frmCart1.btnCone9.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 10 Then
            frmCart1.btnCone10.Enabled = False
            frmCart1.btnCone10.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 11 Then
            frmCart1.btnCone11.Enabled = False
            frmCart1.btnCone11.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 12 Then
            frmCart1.btnCone12.Enabled = False
            frmCart1.btnCone12.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 13 Then
            frmCart1.btnCone13.Enabled = False
            frmCart1.btnCone13.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 14 Then
            frmCart1.btnCone14.Enabled = False
            frmCart1.btnCone14.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 15 Then
            frmCart1.btnCone15.Enabled = False
            frmCart1.btnCone15.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 16 Then
            frmCart1.btnCone16.Enabled = False
            frmCart1.btnCone16.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 17 Then
            frmCart1.btnCone17.Enabled = False
            frmCart1.btnCone17.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 18 Then
            frmCart1.btnCone18.Enabled = False
            frmCart1.btnCone18.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 19 Then
            frmCart1.btnCone20.Enabled = False
            frmCart1.btnCone20.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 21 Then
            frmCart1.btnCone21.Enabled = False
            frmCart1.btnCone21.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 22 Then
            frmCart1.btnCone22.Enabled = False
            frmCart1.btnCone22.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 23 Then
            frmCart1.btnCone23.Enabled = False
            frmCart1.btnCone23.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 24 Then
            frmCart1.btnCone24.Enabled = False
            frmCart1.btnCone24.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 25 Then
            frmCart1.btnCone25.Enabled = False
            frmCart1.btnCone25.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 26 Then
            frmCart1.btnCone26.Enabled = False
            frmCart1.btnCone26.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 27 Then
            frmCart1.btnCone27.Enabled = False
            frmCart1.btnCone27.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 28 Then
            frmCart1.btnCone28.Enabled = False
            frmCart1.btnCone28.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 29 Then
            frmCart1.btnCone29.Enabled = False
            frmCart1.btnCone29.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 30 Then
            frmCart1.btnCone30.Enabled = False
            frmCart1.btnCone30.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 31 Then
            frmCart1.btnCone31.Enabled = False
            frmCart1.btnCone31.BackColor = Color.Green

        ElseIf frmCart1.varConeNum = 32 Then
            frmCart1.btnCone32.Enabled = False
            frmCart1.btnCone32.BackColor = Color.Green

        End If





        If frmCart1.coneCount = 31 Then 'this value needs to be one less than total as 
            'Clean Up and return to cone selection screen

            Me.btnMeasure.Enabled = True
            frmCart1.coneCount = 1 + frmCart1.coneCount
            'frmCart1.endCount()
            'frmCart1.lblConeCount.Text = frmCart1.coneCount
            frmCart1.Show()
            Me.Close()
        Else
            'Clean Up and return to cone selection screen

            Me.btnMeasure.Enabled = True
            frmCart1.coneCount = 1 + frmCart1.coneCount
            'frmCart1.lblConeCount.Text = frmCart1.coneCount
            frmCart1.Show()
            Me.Close()
        End If

        CSV()
        frmCart1.Show()
        Me.Close()

    End Sub


    'Create csv file
    Private Sub CSV()

        Dim csvFile As String = My.Application.Info.DirectoryPath & "\Test.csv"
        Dim outFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(csvFile, False)

        outFile.WriteLine("Cone# , Defect, No Cone, DL")
        outFile.WriteLine(frmCart1.varConeNum, "", "", Original_incoming)
        outFile.Close()


    End Sub

    'Delegate Sub DataDelegate(ByVal sdata As String)

    'Private Sub StoreReceivedData(ByVal sdata As String)



    'Original_incoming &= sdata


    'End Sub

    'Private Sub VeriColorCom_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs)
    'Dim ReceivedData As String
    'Try
    '    ReceivedData = VeriColorCom.ReadLine
    'Catch ex As Exception
    '     ReceivedData = ex.Message
    'End Try

    'Dim adre As New DataDelegate(AddressOf StoreReceivedData)

    'Me.Invoke(adre, ReceivedData)

    ' End Sub


    ' Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click

    '    frmSettings.Show()

    ' End Sub












End Class