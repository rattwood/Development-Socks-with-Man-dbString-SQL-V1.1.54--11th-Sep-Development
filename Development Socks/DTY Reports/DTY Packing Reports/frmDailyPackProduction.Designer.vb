<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDailyPackProduction
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDailyPackProduction))
        Me.DGVJobsData = New System.Windows.Forms.DataGridView()
        Me.DGVProdData = New System.Windows.Forms.DataGridView()
        Me.DGVJobData = New System.Windows.Forms.DataGridView()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCreate = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        CType(Me.DGVJobsData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVProdData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVJobData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVJobsData
        '
        Me.DGVJobsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVJobsData.Location = New System.Drawing.Point(50, 330)
        Me.DGVJobsData.Name = "DGVJobsData"
        Me.DGVJobsData.Size = New System.Drawing.Size(644, 150)
        Me.DGVJobsData.TabIndex = 0
        Me.DGVJobsData.Visible = False
        '
        'DGVProdData
        '
        Me.DGVProdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVProdData.Location = New System.Drawing.Point(700, 316)
        Me.DGVProdData.Name = "DGVProdData"
        Me.DGVProdData.Size = New System.Drawing.Size(634, 150)
        Me.DGVProdData.TabIndex = 1
        Me.DGVProdData.Visible = False
        '
        'DGVJobData
        '
        Me.DGVJobData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVJobData.Location = New System.Drawing.Point(50, 392)
        Me.DGVJobData.Name = "DGVJobData"
        Me.DGVJobData.Size = New System.Drawing.Size(1301, 374)
        Me.DGVJobData.TabIndex = 2
        Me.DGVJobData.Visible = False
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(50, 51)
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(393, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = " "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(289, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Selected Date"
        '
        'btnCreate
        '
        Me.btnCreate.Enabled = False
        Me.btnCreate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreate.Location = New System.Drawing.Point(312, 120)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(119, 43)
        Me.btnCreate.TabIndex = 13
        Me.btnCreate.Text = "Create Report"
        Me.btnCreate.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(136, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(176, 20)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Daily Packing Report"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(93, 235)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 15
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnCancel.BackgroundImage = Global.Development_Socks.My.Resources.Resources.home_icon_silhouette
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(12, 268)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(121, 48)
        Me.btnCancel.TabIndex = 70
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmDailyPackProduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 328)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCreate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.Controls.Add(Me.DGVJobData)
        Me.Controls.Add(Me.DGVProdData)
        Me.Controls.Add(Me.DGVJobsData)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDailyPackProduction"
        Me.Text = "frmDailyPackProduction"
        CType(Me.DGVJobsData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVProdData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVJobData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGVJobsData As DataGridView
    Friend WithEvents DGVProdData As DataGridView
    Friend WithEvents DGVJobData As DataGridView
    Friend WithEvents MonthCalendar1 As MonthCalendar
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnCreate As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnCancel As Button
End Class
