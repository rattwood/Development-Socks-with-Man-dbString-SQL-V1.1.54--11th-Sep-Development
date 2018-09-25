<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintCartReport
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
        Me.DGVcartReport = New System.Windows.Forms.DataGridView()
        Me.btnCartReport = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnExpotData = New System.Windows.Forms.Button()
        CType(Me.DGVcartReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVcartReport
        '
        Me.DGVcartReport.AllowUserToAddRows = False
        Me.DGVcartReport.AllowUserToDeleteRows = False
        Me.DGVcartReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVcartReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.DGVcartReport.Location = New System.Drawing.Point(0, 0)
        Me.DGVcartReport.Name = "DGVcartReport"
        Me.DGVcartReport.ReadOnly = True
        Me.DGVcartReport.Size = New System.Drawing.Size(698, 371)
        Me.DGVcartReport.TabIndex = 0
        '
        'btnCartReport
        '
        Me.btnCartReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCartReport.Location = New System.Drawing.Point(12, 83)
        Me.btnCartReport.Name = "btnCartReport"
        Me.btnCartReport.Size = New System.Drawing.Size(217, 43)
        Me.btnCartReport.TabIndex = 1
        Me.btnCartReport.Text = "Create Cart Report"
        Me.btnCartReport.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(233, 260)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(217, 43)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnExpotData
        '
        Me.btnExpotData.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExpotData.Location = New System.Drawing.Point(463, 83)
        Me.btnExpotData.Name = "btnExpotData"
        Me.btnExpotData.Size = New System.Drawing.Size(217, 43)
        Me.btnExpotData.TabIndex = 3
        Me.btnExpotData.Text = "Export Data"
        Me.btnExpotData.UseVisualStyleBackColor = True
        '
        'frmPrintCartReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 339)
        Me.Controls.Add(Me.btnExpotData)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCartReport)
        Me.Controls.Add(Me.DGVcartReport)
        Me.Name = "frmPrintCartReport"
        Me.Text = "frmPrintCartReport"
        CType(Me.DGVcartReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGVcartReport As DataGridView
    Friend WithEvents btnCartReport As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnExpotData As Button
End Class
