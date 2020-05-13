<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.btnSetSave = New System.Windows.Forms.Button()
        Me.chkUseColour = New System.Windows.Forms.CheckBox()
        Me.chkUseSort = New System.Windows.Forms.CheckBox()
        Me.chkUsePack = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.chkDGV = New System.Windows.Forms.CheckBox()
        Me.chkDisableCreate = New System.Windows.Forms.CheckBox()
        Me.chkAudioAlarm = New System.Windows.Forms.CheckBox()
        Me.txtBoxSearchDays = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBoxCheeseSearchDays = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnSetSave
        '
        Me.btnSetSave.BackColor = System.Drawing.Color.GreenYellow
        Me.btnSetSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSave.Location = New System.Drawing.Point(496, 481)
        Me.btnSetSave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetSave.Name = "btnSetSave"
        Me.btnSetSave.Size = New System.Drawing.Size(248, 48)
        Me.btnSetSave.TabIndex = 2
        Me.btnSetSave.Text = "Save Settings"
        Me.btnSetSave.UseVisualStyleBackColor = False
        Me.btnSetSave.Visible = False
        '
        'chkUseColour
        '
        Me.chkUseColour.AutoSize = True
        Me.chkUseColour.BackColor = System.Drawing.Color.LightSkyBlue
        Me.chkUseColour.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseColour.Location = New System.Drawing.Point(524, 134)
        Me.chkUseColour.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkUseColour.Name = "chkUseColour"
        Me.chkUseColour.Size = New System.Drawing.Size(104, 20)
        Me.chkUseColour.TabIndex = 23
        Me.chkUseColour.Text = "Use Colour"
        Me.chkUseColour.UseVisualStyleBackColor = False
        '
        'chkUseSort
        '
        Me.chkUseSort.AutoSize = True
        Me.chkUseSort.BackColor = System.Drawing.Color.LightSkyBlue
        Me.chkUseSort.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseSort.Location = New System.Drawing.Point(524, 171)
        Me.chkUseSort.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkUseSort.Name = "chkUseSort"
        Me.chkUseSort.Size = New System.Drawing.Size(87, 20)
        Me.chkUseSort.TabIndex = 24
        Me.chkUseSort.Text = "Use Sort"
        Me.chkUseSort.UseVisualStyleBackColor = False
        '
        'chkUsePack
        '
        Me.chkUsePack.AutoSize = True
        Me.chkUsePack.BackColor = System.Drawing.Color.LightSkyBlue
        Me.chkUsePack.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUsePack.Location = New System.Drawing.Point(524, 204)
        Me.chkUsePack.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkUsePack.Name = "chkUsePack"
        Me.chkUsePack.Size = New System.Drawing.Size(115, 20)
        Me.chkUsePack.TabIndex = 25
        Me.chkUsePack.Text = "Use Packing"
        Me.chkUsePack.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(316, 28)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(178, 24)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Software Features"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(643, 334)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 15)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Label4"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(643, 362)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 15)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(492, 334)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 15)
        Me.Label6.TabIndex = 46
        Me.Label6.Text = "Default Moitor Height"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(493, 362)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 15)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Default Moitor Width"
        '
        'chkDGV
        '
        Me.chkDGV.AutoSize = True
        Me.chkDGV.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDGV.Location = New System.Drawing.Point(121, 326)
        Me.chkDGV.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkDGV.Name = "chkDGV"
        Me.chkDGV.Size = New System.Drawing.Size(113, 20)
        Me.chkDGV.TabIndex = 48
        Me.chkDGV.Text = "TurnDGV On"
        Me.chkDGV.UseVisualStyleBackColor = True
        '
        'chkDisableCreate
        '
        Me.chkDisableCreate.AutoSize = True
        Me.chkDisableCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDisableCreate.Location = New System.Drawing.Point(121, 352)
        Me.chkDisableCreate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkDisableCreate.Name = "chkDisableCreate"
        Me.chkDisableCreate.Size = New System.Drawing.Size(171, 20)
        Me.chkDisableCreate.TabIndex = 55
        Me.chkDisableCreate.Text = "Dissable Sort Create"
        Me.chkDisableCreate.UseVisualStyleBackColor = True
        '
        'chkAudioAlarm
        '
        Me.chkAudioAlarm.AutoSize = True
        Me.chkAudioAlarm.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAudioAlarm.Location = New System.Drawing.Point(122, 378)
        Me.chkAudioAlarm.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkAudioAlarm.Name = "chkAudioAlarm"
        Me.chkAudioAlarm.Size = New System.Drawing.Size(164, 20)
        Me.chkAudioAlarm.TabIndex = 56
        Me.chkAudioAlarm.Text = "Audio Alarm Enable"
        Me.chkAudioAlarm.UseVisualStyleBackColor = True
        '
        'txtBoxSearchDays
        '
        Me.txtBoxSearchDays.Location = New System.Drawing.Point(324, 133)
        Me.txtBoxSearchDays.MaxLength = 3
        Me.txtBoxSearchDays.Name = "txtBoxSearchDays"
        Me.txtBoxSearchDays.Size = New System.Drawing.Size(48, 21)
        Me.txtBoxSearchDays.TabIndex = 57
        Me.txtBoxSearchDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label10.Location = New System.Drawing.Point(88, 136)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(230, 15)
        Me.Label10.TabIndex = 58
        Me.Label10.Text = "Packing Previous Days of last Pack"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(148, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 20)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "Packing Settings"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label3.Location = New System.Drawing.Point(42, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(276, 15)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "No of days to search for unpacked Cheese"
        '
        'txtBoxCheeseSearchDays
        '
        Me.txtBoxCheeseSearchDays.Location = New System.Drawing.Point(324, 168)
        Me.txtBoxCheeseSearchDays.MaxLength = 3
        Me.txtBoxCheeseSearchDays.Name = "txtBoxCheeseSearchDays"
        Me.txtBoxCheeseSearchDays.Size = New System.Drawing.Size(48, 21)
        Me.txtBoxCheeseSearchDays.TabIndex = 60
        Me.txtBoxCheeseSearchDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(34, 86)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(363, 179)
        Me.Button1.TabIndex = 62
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(402, 86)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(363, 179)
        Me.Button2.TabIndex = 63
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(503, 99)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(147, 20)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Module Selection"
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Button3.Enabled = False
        Me.Button3.Location = New System.Drawing.Point(402, 271)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(363, 179)
        Me.Button3.TabIndex = 65
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(477, 289)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(229, 20)
        Me.Label9.TabIndex = 66
        Me.Label9.Text = "Display Monitor Information"
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Button4.Enabled = False
        Me.Button4.Location = New System.Drawing.Point(33, 271)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(363, 179)
        Me.Button4.TabIndex = 67
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(64, 289)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(289, 20)
        Me.Label11.TabIndex = 68
        Me.Label11.Text = "Technical Options (do Not Change)"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnCancel.BackgroundImage = Global.Development_Socks.My.Resources.Resources.home_icon_silhouette
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(45, 481)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(248, 48)
        Me.btnCancel.TabIndex = 69
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSkyBlue
        Me.ClientSize = New System.Drawing.Size(793, 568)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtBoxCheeseSearchDays)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtBoxSearchDays)
        Me.Controls.Add(Me.chkAudioAlarm)
        Me.Controls.Add(Me.chkDisableCreate)
        Me.Controls.Add(Me.chkDGV)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkUsePack)
        Me.Controls.Add(Me.chkUseSort)
        Me.Controls.Add(Me.chkUseColour)
        Me.Controls.Add(Me.btnSetSave)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button4)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSetSave As Button
    Friend WithEvents chkUseColour As CheckBox
    Friend WithEvents chkUseSort As CheckBox
    Friend WithEvents chkUsePack As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents chkDGV As CheckBox
    Friend WithEvents chkDisableCreate As CheckBox
    Friend WithEvents chkAudioAlarm As CheckBox
    Friend WithEvents txtBoxSearchDays As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtBoxCheeseSearchDays As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents btnCancel As Button
End Class
