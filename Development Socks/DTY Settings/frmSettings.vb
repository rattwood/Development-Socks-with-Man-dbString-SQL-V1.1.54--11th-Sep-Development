Public Class frmSettings
    Public PortCom
    Public PortBaudRate
    Public softwareActivation

    Dim myPort As Array



    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'ColourDBDataSet.ComSettings' table. You can move, or remove it, as needed.
        'Me.ComSettingsTableAdapter.Fill(Me.ColourDBDataSet.ComSettings)

        myPort = IO.Ports.SerialPort.GetPortNames()
        lstSerialPorts.Items.AddRange(myPort)
        'PortBaudRate = ColourSenseBaudTextBox.Text

        'Shows current settings
        txtBoxTemplates.Text = My.Settings.dirTemplate
        txtBoxCarts.Text = My.Settings.dirCarts
        txtBoxJobs.Text = My.Settings.dirJobs
        txtBoxPack.Text = My.Settings.dirPacking
        txtBoxPackReports.Text = My.Settings.dirPackReports
        txtBoxBarcodectrl.Text = My.Settings.barcodeCTRL

        If My.Settings.chkUseSpectro Then chkUseSpectro.Checked = True Else chkUseSpectro.Checked = False
        If My.Settings.chkUseColour Then chkUseColour.Checked = True Else chkUseColour.Checked = False
        If My.Settings.chkUseSort Then chkUseSort.Checked = True Else chkUseSort.Checked = False
        If My.Settings.chkUsePack Then chkUsePack.Checked = True Else chkUsePack.Checked = False

        If My.Settings.debugSet Then chkDGV.Checked = True Else chkDGV.Checked = False

        lstSerialPorts.Text = My.Settings.comPortNum
        lstBaudRates.Text = My.Settings.comBaudRate

        Label4.Text = SystemInformation.PrimaryMonitorSize.Height
        Label5.Text = SystemInformation.PrimaryMonitorSize.Width



    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmActivate.Show()


    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        FolderBrowserDialog1.ShowDialog()
        txtBoxTemplates.Text = FolderBrowserDialog1.SelectedPath

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        FolderBrowserDialog1.ShowDialog()
        txtBoxCarts.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        FolderBrowserDialog1.ShowDialog()
        txtBoxJobs.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        FolderBrowserDialog1.ShowDialog()
        txtBoxPack.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        FolderBrowserDialog1.ShowDialog()
        txtBoxPackReports.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub btnSetSave_click(sender As Object, e As EventArgs) Handles btnSetSave.Click
        ' get number from drop down list

        'frmCart1.VeriColorCom.PortName = lstSerialPorts.Text
        'PortCom = frmCart1.VeriColorCom.PortName
        'frmCart1.VeriColorCom.Open()

        My.Settings.comPortNum = PortCom
        My.Settings.comBaudRate = PortBaudRate

        My.Settings.chkUseSpectro = chkUseSpectro.CheckState
        My.Settings.chkUseColour = chkUseColour.CheckState
        My.Settings.chkUseSort = chkUseSort.CheckState
        My.Settings.chkUsePack = chkUsePack.CheckState
        My.Settings.debugSet = chkDGV.CheckState
        My.Settings.dirTemplate = txtBoxTemplates.Text
        My.Settings.dirCarts = txtBoxCarts.Text
        My.Settings.dirJobs = txtBoxJobs.Text
        My.Settings.dirPacking = txtBoxPack.Text
        My.Settings.dirPackReports = txtBoxPackReports.Text
        My.Settings.barcodeCTRL = txtBoxBarcodectrl.Text
        Me.Hide()
    End Sub

    Private Sub chkUseSpectro_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseSpectro.CheckedChanged

    End Sub

    Private Sub chkUseColour_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseColour.CheckedChanged

        chkUseSort.CheckState = False
        chkUsePack.CheckState = False
        chkUseSort.Checked = False
        chkUsePack.Checked = False

    End Sub

    Private Sub chkUseSort_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseSort.CheckedChanged
        chkUseColour.CheckState = False
        chkUsePack.CheckState = False
        chkUseColour.Checked = False
        chkUsePack.Checked = False
    End Sub

    Private Sub chkUsePack_CheckedChanged(sender As Object, e As EventArgs) Handles chkUsePack.CheckedChanged
        chkUseSort.CheckState = False
        chkUseColour.CheckState = False
        chkUseSort.Checked = False
        chkUseColour.Checked = False
    End Sub


End Class