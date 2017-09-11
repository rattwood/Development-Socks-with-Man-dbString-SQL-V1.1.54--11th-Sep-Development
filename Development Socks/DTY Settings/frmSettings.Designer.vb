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
        Me.lblSerialPortSelect = New System.Windows.Forms.Label()
        Me.lstSerialPorts = New System.Windows.Forms.ListBox()
        Me.btnSetSave = New System.Windows.Forms.Button()
        Me.lstBaudRates = New System.Windows.Forms.ListBox()
        Me.chkUseSpectro = New System.Windows.Forms.CheckBox()
        Me.chkUseColour = New System.Windows.Forms.CheckBox()
        Me.chkUseSort = New System.Windows.Forms.CheckBox()
        Me.chkUsePack = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxTemplates = New System.Windows.Forms.TextBox()
        Me.txtBoxCarts = New System.Windows.Forms.TextBox()
        Me.txtBoxJobs = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.chkDGV = New System.Windows.Forms.CheckBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.txtBoxPack = New System.Windows.Forms.TextBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.txtBoxPackReports = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBoxBarcodectrl = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lblSerialPortSelect
        '
        Me.lblSerialPortSelect.AutoSize = True
        Me.lblSerialPortSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSerialPortSelect.Location = New System.Drawing.Point(16, 10)
        Me.lblSerialPortSelect.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSerialPortSelect.Name = "lblSerialPortSelect"
        Me.lblSerialPortSelect.Size = New System.Drawing.Size(150, 24)
        Me.lblSerialPortSelect.TabIndex = 0
        Me.lblSerialPortSelect.Text = "Comm Settings"
        Me.lblSerialPortSelect.Visible = False
        '
        'lstSerialPorts
        '
        Me.lstSerialPorts.FormattingEnabled = True
        Me.lstSerialPorts.ItemHeight = 15
        Me.lstSerialPorts.Location = New System.Drawing.Point(21, 76)
        Me.lstSerialPorts.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstSerialPorts.Name = "lstSerialPorts"
        Me.lstSerialPorts.Size = New System.Drawing.Size(159, 109)
        Me.lstSerialPorts.TabIndex = 1
        Me.lstSerialPorts.Visible = False
        '
        'btnSetSave
        '
        Me.btnSetSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetSave.Location = New System.Drawing.Point(327, 351)
        Me.btnSetSave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnSetSave.Name = "btnSetSave"
        Me.btnSetSave.Size = New System.Drawing.Size(344, 48)
        Me.btnSetSave.TabIndex = 2
        Me.btnSetSave.Text = "Save Settings"
        Me.btnSetSave.UseVisualStyleBackColor = True
        '
        'lstBaudRates
        '
        Me.lstBaudRates.FormattingEnabled = True
        Me.lstBaudRates.ItemHeight = 15
        Me.lstBaudRates.Items.AddRange(New Object() {"4800", "9600", "19200", "38400", "57600"})
        Me.lstBaudRates.Location = New System.Drawing.Point(21, 247)
        Me.lstBaudRates.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lstBaudRates.Name = "lstBaudRates"
        Me.lstBaudRates.Size = New System.Drawing.Size(159, 109)
        Me.lstBaudRates.TabIndex = 16
        Me.lstBaudRates.Visible = False
        '
        'chkUseSpectro
        '
        Me.chkUseSpectro.AutoSize = True
        Me.chkUseSpectro.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseSpectro.Location = New System.Drawing.Point(718, 76)
        Me.chkUseSpectro.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkUseSpectro.Name = "chkUseSpectro"
        Me.chkUseSpectro.Size = New System.Drawing.Size(113, 20)
        Me.chkUseSpectro.TabIndex = 22
        Me.chkUseSpectro.Text = "Use Spectro"
        Me.chkUseSpectro.UseVisualStyleBackColor = True
        '
        'chkUseColour
        '
        Me.chkUseColour.AutoSize = True
        Me.chkUseColour.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseColour.Location = New System.Drawing.Point(718, 123)
        Me.chkUseColour.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkUseColour.Name = "chkUseColour"
        Me.chkUseColour.Size = New System.Drawing.Size(104, 20)
        Me.chkUseColour.TabIndex = 23
        Me.chkUseColour.Text = "Use Colour"
        Me.chkUseColour.UseVisualStyleBackColor = True
        '
        'chkUseSort
        '
        Me.chkUseSort.AutoSize = True
        Me.chkUseSort.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUseSort.Location = New System.Drawing.Point(718, 171)
        Me.chkUseSort.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkUseSort.Name = "chkUseSort"
        Me.chkUseSort.Size = New System.Drawing.Size(87, 20)
        Me.chkUseSort.TabIndex = 24
        Me.chkUseSort.Text = "Use Sort"
        Me.chkUseSort.UseVisualStyleBackColor = True
        '
        'chkUsePack
        '
        Me.chkUsePack.AutoSize = True
        Me.chkUsePack.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUsePack.Location = New System.Drawing.Point(718, 218)
        Me.chkUsePack.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkUsePack.Name = "chkUsePack"
        Me.chkUsePack.Size = New System.Drawing.Size(115, 20)
        Me.chkUsePack.TabIndex = 25
        Me.chkUsePack.Text = "Use Packing"
        Me.chkUsePack.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(713, 18)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(178, 24)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Software Features"
        '
        'txtBoxTemplates
        '
        Me.txtBoxTemplates.Location = New System.Drawing.Point(367, 84)
        Me.txtBoxTemplates.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxTemplates.Name = "txtBoxTemplates"
        Me.txtBoxTemplates.Size = New System.Drawing.Size(287, 21)
        Me.txtBoxTemplates.TabIndex = 27
        '
        'txtBoxCarts
        '
        Me.txtBoxCarts.Location = New System.Drawing.Point(367, 131)
        Me.txtBoxCarts.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxCarts.Name = "txtBoxCarts"
        Me.txtBoxCarts.Size = New System.Drawing.Size(287, 21)
        Me.txtBoxCarts.TabIndex = 28
        '
        'txtBoxJobs
        '
        Me.txtBoxJobs.Location = New System.Drawing.Point(367, 179)
        Me.txtBoxJobs.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxJobs.Name = "txtBoxJobs"
        Me.txtBoxJobs.Size = New System.Drawing.Size(287, 21)
        Me.txtBoxJobs.TabIndex = 29
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(45, 58)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Port Number"
        Me.Label2.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(45, 222)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "Baud Rate"
        Me.Label3.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label8.Location = New System.Drawing.Point(393, 18)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(150, 24)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "Directory Paths"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(21, 452)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(240, 48)
        Me.Button1.TabIndex = 40
        Me.Button1.Text = "Activate Software"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(216, 84)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(123, 22)
        Me.Button2.TabIndex = 41
        Me.Button2.Text = "Templates"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(216, 131)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(123, 22)
        Me.Button3.TabIndex = 42
        Me.Button3.Text = "Save Carts"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(216, 179)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(123, 22)
        Me.Button4.TabIndex = 43
        Me.Button4.Text = "Save Jobs"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(838, 469)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 15)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Label4"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(838, 497)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 15)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(687, 469)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 15)
        Me.Label6.TabIndex = 46
        Me.Label6.Text = "Default Moitor Height"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(688, 497)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 15)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Default Moitor Width"
        '
        'chkDGV
        '
        Me.chkDGV.AutoSize = True
        Me.chkDGV.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDGV.Location = New System.Drawing.Point(20, 389)
        Me.chkDGV.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkDGV.Name = "chkDGV"
        Me.chkDGV.Size = New System.Drawing.Size(113, 20)
        Me.chkDGV.TabIndex = 48
        Me.chkDGV.Text = "TurnDGV On"
        Me.chkDGV.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(216, 223)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(123, 22)
        Me.Button5.TabIndex = 50
        Me.Button5.Text = "Save Packing"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'txtBoxPack
        '
        Me.txtBoxPack.Location = New System.Drawing.Point(367, 223)
        Me.txtBoxPack.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxPack.Name = "txtBoxPack"
        Me.txtBoxPack.Size = New System.Drawing.Size(287, 21)
        Me.txtBoxPack.TabIndex = 49
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(216, 263)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(144, 22)
        Me.Button6.TabIndex = 52
        Me.Button6.Text = "Save Pack Reports"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'txtBoxPackReports
        '
        Me.txtBoxPackReports.Location = New System.Drawing.Point(367, 263)
        Me.txtBoxPackReports.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxPackReports.Name = "txtBoxPackReports"
        Me.txtBoxPackReports.Size = New System.Drawing.Size(287, 21)
        Me.txtBoxPackReports.TabIndex = 51
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(15, 362)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(147, 16)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "Barcode Control Chr"
        '
        'txtBoxBarcodectrl
        '
        Me.txtBoxBarcodectrl.Location = New System.Drawing.Point(168, 359)
        Me.txtBoxBarcodectrl.Name = "txtBoxBarcodectrl"
        Me.txtBoxBarcodectrl.Size = New System.Drawing.Size(48, 21)
        Me.txtBoxBarcodectrl.TabIndex = 54
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 579)
        Me.Controls.Add(Me.txtBoxBarcodectrl)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.txtBoxPackReports)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.txtBoxPack)
        Me.Controls.Add(Me.chkDGV)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBoxJobs)
        Me.Controls.Add(Me.txtBoxCarts)
        Me.Controls.Add(Me.txtBoxTemplates)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkUsePack)
        Me.Controls.Add(Me.chkUseSort)
        Me.Controls.Add(Me.chkUseColour)
        Me.Controls.Add(Me.chkUseSpectro)
        Me.Controls.Add(Me.lstBaudRates)
        Me.Controls.Add(Me.btnSetSave)
        Me.Controls.Add(Me.lstSerialPorts)
        Me.Controls.Add(Me.lblSerialPortSelect)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configuration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSerialPortSelect As Label
    Friend WithEvents lstSerialPorts As ListBox
    Friend WithEvents btnSetSave As Button
    Friend WithEvents lstBaudRates As ListBox
    Friend WithEvents chkUseSpectro As CheckBox
    Friend WithEvents chkUseColour As CheckBox
    Friend WithEvents chkUseSort As CheckBox
    Friend WithEvents chkUsePack As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtBoxTemplates As TextBox
    Friend WithEvents txtBoxCarts As TextBox
    Friend WithEvents txtBoxJobs As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents chkDGV As CheckBox
    Friend WithEvents Button5 As Button
    Friend WithEvents txtBoxPack As TextBox
    Friend WithEvents Button6 As Button
    Friend WithEvents txtBoxPackReports As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtBoxBarcodectrl As TextBox
End Class
