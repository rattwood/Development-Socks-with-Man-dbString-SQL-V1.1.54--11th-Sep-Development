<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackingPrintLastSheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPackingPrintLastSheet))
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.lstBoxFiles = New System.Windows.Forms.ListBox()
        Me.lblListJobs = New System.Windows.Forms.Label()
        Me.lblSelectedDate = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(2, 4)
        Me.MonthCalendar1.MaxSelectionCount = 1
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 0
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(36, 249)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(50, 24)
        Me.lblDate.TabIndex = 1
        Me.lblDate.Text = "date"
        '
        'btnSelect
        '
        Me.btnSelect.BackColor = System.Drawing.Color.LimeGreen
        Me.btnSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect.Location = New System.Drawing.Point(27, 276)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(132, 40)
        Me.btnSelect.TabIndex = 2
        Me.btnSelect.Text = "Select Date"
        Me.btnSelect.UseVisualStyleBackColor = False
        '
        'lstBoxFiles
        '
        Me.lstBoxFiles.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstBoxFiles.FormattingEnabled = True
        Me.lstBoxFiles.ItemHeight = 24
        Me.lstBoxFiles.Location = New System.Drawing.Point(258, 48)
        Me.lstBoxFiles.Name = "lstBoxFiles"
        Me.lstBoxFiles.Size = New System.Drawing.Size(311, 580)
        Me.lstBoxFiles.TabIndex = 3
        '
        'lblListJobs
        '
        Me.lblListJobs.AutoSize = True
        Me.lblListJobs.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblListJobs.Location = New System.Drawing.Point(297, 21)
        Me.lblListJobs.Name = "lblListJobs"
        Me.lblListJobs.Size = New System.Drawing.Size(149, 24)
        Me.lblListJobs.TabIndex = 4
        Me.lblListJobs.Text = "Jobs found for "
        '
        'lblSelectedDate
        '
        Me.lblSelectedDate.AutoSize = True
        Me.lblSelectedDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedDate.Location = New System.Drawing.Point(452, 21)
        Me.lblSelectedDate.Name = "lblSelectedDate"
        Me.lblSelectedDate.Size = New System.Drawing.Size(105, 24)
        Me.lblSelectedDate.TabIndex = 5
        Me.lblSelectedDate.Text = "selectdate"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Red
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(27, 594)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(132, 40)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmPackingPrintLastSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(602, 646)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblSelectedDate)
        Me.Controls.Add(Me.lblListJobs)
        Me.Controls.Add(Me.lstBoxFiles)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPackingPrintLastSheet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Print Last Sheet"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MonthCalendar1 As MonthCalendar
    Friend WithEvents lblDate As Label
    Friend WithEvents btnSelect As Button
    Friend WithEvents lstBoxFiles As ListBox
    Friend WithEvents lblListJobs As Label
    Friend WithEvents lblSelectedDate As Label
    Friend WithEvents btnCancel As Button
End Class
