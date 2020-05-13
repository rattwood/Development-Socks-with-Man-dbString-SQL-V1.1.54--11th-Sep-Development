Public Class frmSettings


    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Shows current settings

        txtBoxSearchDays.Text = My.Settings.searchDays
        txtBoxCheeseSearchDays.Text = My.Settings.SearchDaysCheese

        If My.Settings.chkUseColour Then chkUseColour.Checked = True Else chkUseColour.Checked = False
        If My.Settings.chkUseSort Then chkUseSort.Checked = True Else chkUseSort.Checked = False
        If My.Settings.chkUsePack Then chkUsePack.Checked = True Else chkUsePack.Checked = False
        If My.Settings.chkDisableCreate Then chkDisableCreate.Checked = True Else chkDisableCreate.Checked = False
        If My.Settings.audioAlarm Then chkAudioAlarm.Checked = True Else chkAudioAlarm.Checked = False
        If My.Settings.debugSet Then chkDGV.Checked = True Else chkDGV.Checked = False

        Label4.Text = SystemInformation.PrimaryMonitorSize.Height
        Label5.Text = SystemInformation.PrimaryMonitorSize.Width



    End Sub




    Private Sub btnSetSave_click(sender As Object, e As EventArgs) Handles btnSetSave.Click

        btnSetSave.Visible = False
        My.Settings.chkUseColour = chkUseColour.CheckState
        My.Settings.chkUseSort = chkUseSort.CheckState
        My.Settings.chkUsePack = chkUsePack.CheckState
        My.Settings.chkDisableCreate = chkDisableCreate.CheckState
        My.Settings.debugSet = chkDGV.CheckState
        My.Settings.audioAlarm = chkAudioAlarm.CheckState
        My.Settings.searchDays = txtBoxSearchDays.Text
        My.Settings.SearchDaysCheese = txtBoxCheeseSearchDays.Text
        My.Settings.Save()

    End Sub


    Private Sub chkUseColour_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseColour.CheckedChanged

        chkUseSort.CheckState = False
        chkUsePack.CheckState = False
        chkUseSort.Checked = False
        chkUsePack.Checked = False

        btnSetSave.Visible = True

    End Sub

    Private Sub chkUseSort_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseSort.CheckedChanged
        chkUseColour.CheckState = False
        chkUsePack.CheckState = False
        chkUseColour.Checked = False
        chkUsePack.Checked = False

        btnSetSave.Visible = True
    End Sub

    Private Sub chkUsePack_CheckedChanged(sender As Object, e As EventArgs) Handles chkUsePack.CheckedChanged
        chkUseSort.CheckState = False
        chkUseColour.CheckState = False
        chkUseSort.Checked = False
        chkUseColour.Checked = False

        btnSetSave.Visible = True
    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmJobEntry.Show()
        frmJobEntry.txtLotNumber.Clear()
        frmJobEntry.txtLotNumber.Focus()
        Me.Close()
    End Sub

    Private Sub txtBoxCheeseSearchDays_TextChanged(sender As Object, e As EventArgs) Handles txtBoxCheeseSearchDays.TextChanged
        btnSetSave.Visible = True
    End Sub

    Private Sub txtBoxSearchDays_TextChanged(sender As Object, e As EventArgs) Handles txtBoxSearchDays.TextChanged
        btnSetSave.Visible = True
    End Sub

    Private Sub chkDGV_CheckedChanged(sender As Object, e As EventArgs) Handles chkDGV.CheckedChanged
        btnSetSave.Visible = True
    End Sub

    Private Sub chkDisableCreate_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisableCreate.CheckedChanged
        btnSetSave.Visible = True
    End Sub

    Private Sub chkAudioAlarm_CheckedChanged(sender As Object, e As EventArgs) Handles chkAudioAlarm.CheckedChanged
        btnSetSave.Visible = True
    End Sub
End Class