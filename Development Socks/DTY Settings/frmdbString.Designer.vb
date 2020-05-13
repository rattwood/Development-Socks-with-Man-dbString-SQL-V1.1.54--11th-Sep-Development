<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmdbString
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmdbString))
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.txtBoxPackReports = New System.Windows.Forms.TextBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.txtBoxPack = New System.Windows.Forms.TextBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtBoxJobs = New System.Windows.Forms.TextBox()
        Me.txtBoxCarts = New System.Windows.Forms.TextBox()
        Me.txtBoxTemplates = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.txtLogReport = New System.Windows.Forms.TextBox()
        Me.chkUseLogs = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label1.Location = New System.Drawing.Point(160, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(477, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Example of db String :-  Server=192.168.1.211,1433;Database=Toraydb;User ID=sa;Pa" &
    "ssword=*****"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(205, 92)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(432, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label2.Location = New System.Drawing.Point(150, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "db String"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnCancel.BackgroundImage = Global.Development_Socks.My.Resources.Resources.home_icon_silhouette
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(43, 501)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(248, 48)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.GreenYellow
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(499, 508)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(248, 48)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        Me.btnSave.Visible = False
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(50, 387)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(144, 22)
        Me.Button6.TabIndex = 63
        Me.Button6.Text = "Save Pack Reports"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'txtBoxPackReports
        '
        Me.txtBoxPackReports.Location = New System.Drawing.Point(201, 387)
        Me.txtBoxPackReports.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxPackReports.Name = "txtBoxPackReports"
        Me.txtBoxPackReports.Size = New System.Drawing.Size(534, 20)
        Me.txtBoxPackReports.TabIndex = 62
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(50, 347)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(123, 22)
        Me.Button5.TabIndex = 61
        Me.Button5.Text = "Save Packing"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'txtBoxPack
        '
        Me.txtBoxPack.Location = New System.Drawing.Point(201, 347)
        Me.txtBoxPack.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxPack.Name = "txtBoxPack"
        Me.txtBoxPack.Size = New System.Drawing.Size(534, 20)
        Me.txtBoxPack.TabIndex = 60
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(50, 303)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(123, 22)
        Me.Button4.TabIndex = 59
        Me.Button4.Text = "Save Jobs"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(50, 255)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(123, 22)
        Me.Button3.TabIndex = 58
        Me.Button3.Text = "Save Carts"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(50, 208)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(123, 22)
        Me.Button2.TabIndex = 57
        Me.Button2.Text = "Templates"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label8.Location = New System.Drawing.Point(365, 176)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(150, 24)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "Directory Paths"
        '
        'txtBoxJobs
        '
        Me.txtBoxJobs.Location = New System.Drawing.Point(201, 303)
        Me.txtBoxJobs.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxJobs.Name = "txtBoxJobs"
        Me.txtBoxJobs.Size = New System.Drawing.Size(534, 20)
        Me.txtBoxJobs.TabIndex = 55
        '
        'txtBoxCarts
        '
        Me.txtBoxCarts.Location = New System.Drawing.Point(201, 255)
        Me.txtBoxCarts.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxCarts.Name = "txtBoxCarts"
        Me.txtBoxCarts.Size = New System.Drawing.Size(534, 20)
        Me.txtBoxCarts.TabIndex = 54
        '
        'txtBoxTemplates
        '
        Me.txtBoxTemplates.Location = New System.Drawing.Point(201, 208)
        Me.txtBoxTemplates.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtBoxTemplates.Name = "txtBoxTemplates"
        Me.txtBoxTemplates.Size = New System.Drawing.Size(534, 20)
        Me.txtBoxTemplates.TabIndex = 53
        '
        'Button7
        '
        Me.Button7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button7.Location = New System.Drawing.Point(50, 425)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(144, 22)
        Me.Button7.TabIndex = 65
        Me.Button7.Text = "Log File Path"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'txtLogReport
        '
        Me.txtLogReport.Location = New System.Drawing.Point(201, 425)
        Me.txtLogReport.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtLogReport.Name = "txtLogReport"
        Me.txtLogReport.Size = New System.Drawing.Size(534, 20)
        Me.txtLogReport.TabIndex = 64
        '
        'chkUseLogs
        '
        Me.chkUseLogs.AutoSize = True
        Me.chkUseLogs.BackColor = System.Drawing.Color.LightSkyBlue
        Me.chkUseLogs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.chkUseLogs.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.chkUseLogs.Location = New System.Drawing.Point(50, 453)
        Me.chkUseLogs.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.chkUseLogs.Name = "chkUseLogs"
        Me.chkUseLogs.Size = New System.Drawing.Size(137, 20)
        Me.chkUseLogs.TabIndex = 66
        Me.chkUseLogs.Text = "Create log Files"
        Me.chkUseLogs.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(303, 25)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(269, 24)
        Me.Label3.TabIndex = 67
        Me.Label3.Text = "Database Connection String"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(29, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(732, 132)
        Me.Button1.TabIndex = 68
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button8
        '
        Me.Button8.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Button8.Enabled = False
        Me.Button8.Location = New System.Drawing.Point(29, 167)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(732, 324)
        Me.Button8.TabIndex = 69
        Me.Button8.UseVisualStyleBackColor = False
        '
        'frmdbString
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSkyBlue
        Me.ClientSize = New System.Drawing.Size(793, 568)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkUseLogs)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.txtLogReport)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.txtBoxPackReports)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.txtBoxPack)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtBoxJobs)
        Me.Controls.Add(Me.txtBoxCarts)
        Me.Controls.Add(Me.txtBoxTemplates)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button8)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmdbString"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Database Connection Setup"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button6 As Button
    Friend WithEvents txtBoxPackReports As TextBox
    Friend WithEvents Button5 As Button
    Friend WithEvents txtBoxPack As TextBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents txtBoxJobs As TextBox
    Friend WithEvents txtBoxCarts As TextBox
    Friend WithEvents txtBoxTemplates As TextBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents chkUseLogs As CheckBox
    Friend WithEvents Button7 As Button
    Friend WithEvents txtLogReport As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button8 As Button
End Class
