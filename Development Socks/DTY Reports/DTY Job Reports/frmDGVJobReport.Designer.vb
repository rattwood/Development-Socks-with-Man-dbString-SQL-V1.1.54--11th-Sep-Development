<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDGVJobReport
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
        Me.DGVReportData = New System.Windows.Forms.DataGridView()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.lstMCName = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnLoadData = New System.Windows.Forms.Button()
        Me.DGVSort = New System.Windows.Forms.DataGridView()
        Me.DGVJob = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DGVReportData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVSort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVJob, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVReportData
        '
        Me.DGVReportData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVReportData.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DGVReportData.Location = New System.Drawing.Point(0, -87)
        Me.DGVReportData.Name = "DGVReportData"
        Me.DGVReportData.Size = New System.Drawing.Size(414, 350)
        Me.DGVReportData.TabIndex = 0
        Me.DGVReportData.Visible = False
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(18, 12)
        Me.MonthCalendar1.MaxSelectionCount = 14
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.ShowWeekNumbers = True
        Me.MonthCalendar1.TabIndex = 2
        '
        'lstMCName
        '
        Me.lstMCName.FormattingEnabled = True
        Me.lstMCName.Items.AddRange(New Object() {"11D1", "11D2", "12D1", "12D2", "21D1", "21D2"})
        Me.lstMCName.Location = New System.Drawing.Point(311, 79)
        Me.lstMCName.Name = "lstMCName"
        Me.lstMCName.Size = New System.Drawing.Size(40, 95)
        Me.lstMCName.TabIndex = 3
        Me.lstMCName.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(308, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "M/C Selected"
        Me.Label1.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(312, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Label2"
        Me.Label2.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 193)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Start Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(162, 193)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "End Date"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(76, 193)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = " "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(220, 193)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = " "
        '
        'btnLoadData
        '
        Me.btnLoadData.Enabled = False
        Me.btnLoadData.Location = New System.Drawing.Point(279, 79)
        Me.btnLoadData.Name = "btnLoadData"
        Me.btnLoadData.Size = New System.Drawing.Size(120, 61)
        Me.btnLoadData.TabIndex = 10
        Me.btnLoadData.Text = "Load Data"
        Me.btnLoadData.UseVisualStyleBackColor = True
        '
        'DGVSort
        '
        Me.DGVSort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVSort.Location = New System.Drawing.Point(453, 24)
        Me.DGVSort.Name = "DGVSort"
        Me.DGVSort.Size = New System.Drawing.Size(240, 150)
        Me.DGVSort.TabIndex = 11
        Me.DGVSort.Visible = False
        '
        'DGVJob
        '
        Me.DGVJob.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVJob.Location = New System.Drawing.Point(474, 56)
        Me.DGVJob.Name = "DGVJob"
        Me.DGVJob.Size = New System.Drawing.Size(240, 150)
        Me.DGVJob.TabIndex = 12
        Me.DGVJob.Visible = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(414, 165)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(165, 36)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Select Job File to open"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'frmDGVJobReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 263)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DGVJob)
        Me.Controls.Add(Me.DGVSort)
        Me.Controls.Add(Me.btnLoadData)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstMCName)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.Controls.Add(Me.DGVReportData)
        Me.Name = "frmDGVJobReport"
        Me.Text = "DGV Report Viewer"
        CType(Me.DGVReportData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVSort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVJob, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGVReportData As DataGridView
    Friend WithEvents MonthCalendar1 As MonthCalendar
    Friend WithEvents lstMCName As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btnLoadData As Button
    Friend WithEvents DGVSort As DataGridView
    Friend WithEvents DGVJob As DataGridView
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button1 As Button
End Class
