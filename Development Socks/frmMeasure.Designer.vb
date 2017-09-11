<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMeasure
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
        Me.components = New System.ComponentModel.Container()
        Me.ConeNumTextBox = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnMeasure = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnManual = New System.Windows.Forms.Button()
        Me.btnReMeasuer = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnP50 = New System.Windows.Forms.Button()
        Me.btnM50 = New System.Windows.Forms.Button()
        Me.btnP30 = New System.Windows.Forms.Button()
        Me.btnM30 = New System.Windows.Forms.Button()
        Me.btnP10 = New System.Windows.Forms.Button()
        Me.btnM10 = New System.Windows.Forms.Button()
        Me.deltaC = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnSampleColour = New System.Windows.Forms.Button()
        Me.OutputRichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblDeltaSign = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.VeriColorCom = New System.IO.Ports.SerialPort(Me.components)
        Me.SuspendLayout()
        '
        'ConeNumTextBox
        '
        Me.ConeNumTextBox.Enabled = False
        Me.ConeNumTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.ConeNumTextBox.Location = New System.Drawing.Point(149, 75)
        Me.ConeNumTextBox.Name = "ConeNumTextBox"
        Me.ConeNumTextBox.ReadOnly = True
        Me.ConeNumTextBox.Size = New System.Drawing.Size(94, 62)
        Me.ConeNumTextBox.TabIndex = 84
        Me.ConeNumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.Green
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(81, 365)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(242, 57)
        Me.btnSave.TabIndex = 83
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnMeasure
        '
        Me.btnMeasure.BackColor = System.Drawing.SystemColors.ControlDark
        Me.btnMeasure.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMeasure.Location = New System.Drawing.Point(81, 154)
        Me.btnMeasure.Margin = New System.Windows.Forms.Padding(2)
        Me.btnMeasure.Name = "btnMeasure"
        Me.btnMeasure.Size = New System.Drawing.Size(242, 98)
        Me.btnMeasure.TabIndex = 82
        Me.btnMeasure.Text = "Measure"
        Me.btnMeasure.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(159, 52)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 20)
        Me.Label3.TabIndex = 81
        Me.Label3.Text = "CONE #"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(676, 45)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(230, 15)
        Me.Label2.TabIndex = 80
        Me.Label2.Text = "COLOUR JUDGEMENT STANDARD"
        '
        'btnManual
        '
        Me.btnManual.BackColor = System.Drawing.SystemColors.Info
        Me.btnManual.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnManual.Location = New System.Drawing.Point(626, 365)
        Me.btnManual.Margin = New System.Windows.Forms.Padding(2)
        Me.btnManual.Name = "btnManual"
        Me.btnManual.Size = New System.Drawing.Size(307, 57)
        Me.btnManual.TabIndex = 79
        Me.btnManual.Text = "Manual Overide"
        Me.btnManual.UseVisualStyleBackColor = False
        '
        'btnReMeasuer
        '
        Me.btnReMeasuer.BackColor = System.Drawing.Color.Red
        Me.btnReMeasuer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReMeasuer.Location = New System.Drawing.Point(81, 275)
        Me.btnReMeasuer.Margin = New System.Windows.Forms.Padding(2)
        Me.btnReMeasuer.Name = "btnReMeasuer"
        Me.btnReMeasuer.Size = New System.Drawing.Size(242, 57)
        Me.btnReMeasuer.TabIndex = 78
        Me.btnReMeasuer.Text = "ReMeasure"
        Me.btnReMeasuer.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(426, 125)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 31)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "C"
        '
        'btnP50
        '
        Me.btnP50.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnP50.Enabled = False
        Me.btnP50.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnP50.Location = New System.Drawing.Point(833, 175)
        Me.btnP50.Margin = New System.Windows.Forms.Padding(2)
        Me.btnP50.Name = "btnP50"
        Me.btnP50.Size = New System.Drawing.Size(100, 46)
        Me.btnP50.TabIndex = 73
        Me.btnP50.Text = "+50"
        Me.btnP50.UseVisualStyleBackColor = False
        '
        'btnM50
        '
        Me.btnM50.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnM50.Enabled = False
        Me.btnM50.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnM50.Location = New System.Drawing.Point(625, 175)
        Me.btnM50.Margin = New System.Windows.Forms.Padding(2)
        Me.btnM50.Name = "btnM50"
        Me.btnM50.Size = New System.Drawing.Size(100, 46)
        Me.btnM50.TabIndex = 72
        Me.btnM50.Text = "-50"
        Me.btnM50.UseVisualStyleBackColor = False
        '
        'btnP30
        '
        Me.btnP30.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnP30.Enabled = False
        Me.btnP30.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnP30.Location = New System.Drawing.Point(833, 125)
        Me.btnP30.Margin = New System.Windows.Forms.Padding(2)
        Me.btnP30.Name = "btnP30"
        Me.btnP30.Size = New System.Drawing.Size(100, 46)
        Me.btnP30.TabIndex = 71
        Me.btnP30.Text = "+30"
        Me.btnP30.UseVisualStyleBackColor = False
        '
        'btnM30
        '
        Me.btnM30.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnM30.Enabled = False
        Me.btnM30.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnM30.Location = New System.Drawing.Point(625, 125)
        Me.btnM30.Margin = New System.Windows.Forms.Padding(2)
        Me.btnM30.Name = "btnM30"
        Me.btnM30.Size = New System.Drawing.Size(100, 46)
        Me.btnM30.TabIndex = 70
        Me.btnM30.Text = "-30"
        Me.btnM30.UseVisualStyleBackColor = False
        '
        'btnP10
        '
        Me.btnP10.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnP10.Enabled = False
        Me.btnP10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnP10.Location = New System.Drawing.Point(833, 75)
        Me.btnP10.Margin = New System.Windows.Forms.Padding(2)
        Me.btnP10.Name = "btnP10"
        Me.btnP10.Size = New System.Drawing.Size(100, 46)
        Me.btnP10.TabIndex = 69
        Me.btnP10.Text = "+10"
        Me.btnP10.UseVisualStyleBackColor = False
        '
        'btnM10
        '
        Me.btnM10.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnM10.Enabled = False
        Me.btnM10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnM10.Location = New System.Drawing.Point(625, 75)
        Me.btnM10.Margin = New System.Windows.Forms.Padding(2)
        Me.btnM10.Name = "btnM10"
        Me.btnM10.Size = New System.Drawing.Size(100, 46)
        Me.btnM10.TabIndex = 68
        Me.btnM10.Text = "-10"
        Me.btnM10.UseVisualStyleBackColor = False
        '
        'deltaC
        '
        Me.deltaC.Enabled = False
        Me.deltaC.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.deltaC.Location = New System.Drawing.Point(472, 109)
        Me.deltaC.Name = "deltaC"
        Me.deltaC.ReadOnly = True
        Me.deltaC.Size = New System.Drawing.Size(148, 62)
        Me.deltaC.TabIndex = 85
        Me.deltaC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 86
        Me.Label4.Text = "Label4"
        Me.Label4.Visible = False
        '
        'btnSettings
        '
        Me.btnSettings.Location = New System.Drawing.Point(856, 9)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(75, 23)
        Me.btnSettings.TabIndex = 87
        Me.btnSettings.Text = "Settings"
        Me.btnSettings.UseVisualStyleBackColor = True
        Me.btnSettings.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(175, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 88
        Me.Label5.Text = "Label5"
        Me.Label5.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(284, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 89
        Me.Label6.Text = "Label6"
        Me.Label6.Visible = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button1.Enabled = False
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(729, 75)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 46)
        Me.Button1.TabIndex = 90
        Me.Button1.Text = "STD"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button2.Enabled = False
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(729, 125)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 46)
        Me.Button2.TabIndex = 91
        Me.Button2.Text = "STD"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button3.Enabled = False
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(729, 175)
        Me.Button3.Margin = New System.Windows.Forms.Padding(2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(100, 46)
        Me.Button3.TabIndex = 92
        Me.Button3.Text = "STD"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'btnSampleColour
        '
        Me.btnSampleColour.BackColor = System.Drawing.SystemColors.Info
        Me.btnSampleColour.Enabled = False
        Me.btnSampleColour.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSampleColour.Location = New System.Drawing.Point(626, 234)
        Me.btnSampleColour.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSampleColour.Name = "btnSampleColour"
        Me.btnSampleColour.Size = New System.Drawing.Size(307, 57)
        Me.btnSampleColour.TabIndex = 93
        Me.btnSampleColour.Text = "Sample Colour"
        Me.btnSampleColour.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSampleColour.UseVisualStyleBackColor = False
        Me.btnSampleColour.Visible = False
        '
        'OutputRichTextBox1
        '
        Me.OutputRichTextBox1.Location = New System.Drawing.Point(15, 41)
        Me.OutputRichTextBox1.Name = "OutputRichTextBox1"
        Me.OutputRichTextBox1.Size = New System.Drawing.Size(100, 96)
        Me.OutputRichTextBox1.TabIndex = 94
        Me.OutputRichTextBox1.Text = ""
        Me.OutputRichTextBox1.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(479, 219)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 95
        Me.Label7.Text = "Label7"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(479, 239)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 96
        Me.Label8.Text = "Label8"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(479, 261)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 97
        Me.Label9.Text = "Label9"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(421, 219)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 13)
        Me.Label10.TabIndex = 98
        Me.Label10.Text = "Bat L"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(421, 239)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(32, 13)
        Me.Label11.TabIndex = 99
        Me.Label11.Text = "Bat a"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(421, 261)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(32, 13)
        Me.Label12.TabIndex = 100
        Me.Label12.Text = "Bat b"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(388, 342)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 13)
        Me.Label13.TabIndex = 101
        Me.Label13.Text = "Delta L"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(388, 365)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(42, 13)
        Me.Label14.TabIndex = 102
        Me.Label14.Text = "Delta E"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(388, 388)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(27, 13)
        Me.Label15.TabIndex = 103
        Me.Label15.Text = "Red"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(388, 409)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(36, 13)
        Me.Label16.TabIndex = 104
        Me.Label16.Text = "Green"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(388, 430)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(28, 13)
        Me.Label17.TabIndex = 105
        Me.Label17.Text = "Blue"
        '
        'lblDeltaSign
        '
        Me.lblDeltaSign.AutoSize = True
        Me.lblDeltaSign.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDeltaSign.Location = New System.Drawing.Point(404, 132)
        Me.lblDeltaSign.Name = "lblDeltaSign"
        Me.lblDeltaSign.Size = New System.Drawing.Size(29, 24)
        Me.lblDeltaSign.TabIndex = 106
        Me.lblDeltaSign.Text = "▲"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(482, 341)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(45, 13)
        Me.Label18.TabIndex = 107
        Me.Label18.Text = "Label18"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(482, 365)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(45, 13)
        Me.Label19.TabIndex = 108
        Me.Label19.Text = "Label19"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(482, 388)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(45, 13)
        Me.Label20.TabIndex = 109
        Me.Label20.Text = "Label20"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(482, 409)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(45, 13)
        Me.Label21.TabIndex = 110
        Me.Label21.Text = "Label21"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(482, 430)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(45, 13)
        Me.Label22.TabIndex = 111
        Me.Label22.Text = "Label22"
        '
        'frmMeasure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(944, 502)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.lblDeltaSign)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.OutputRichTextBox1)
        Me.Controls.Add(Me.btnSampleColour)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.deltaC)
        Me.Controls.Add(Me.ConeNumTextBox)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnMeasure)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnManual)
        Me.Controls.Add(Me.btnReMeasuer)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnP50)
        Me.Controls.Add(Me.btnM50)
        Me.Controls.Add(Me.btnP30)
        Me.Controls.Add(Me.btnM30)
        Me.Controls.Add(Me.btnP10)
        Me.Controls.Add(Me.btnM10)
        Me.Name = "frmMeasure"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Meaure "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ConeNumTextBox As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnMeasure As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnManual As Button
    Friend WithEvents btnReMeasuer As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents btnP50 As Button
    Friend WithEvents btnM50 As Button
    Friend WithEvents btnP30 As Button
    Friend WithEvents btnM30 As Button
    Friend WithEvents btnP10 As Button
    Friend WithEvents btnM10 As Button
    Friend WithEvents deltaC As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnSettings As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents btnSampleColour As Button
    Friend WithEvents OutputRichTextBox1 As RichTextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents lblDeltaSign As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents VeriColorCom As IO.Ports.SerialPort
End Class
