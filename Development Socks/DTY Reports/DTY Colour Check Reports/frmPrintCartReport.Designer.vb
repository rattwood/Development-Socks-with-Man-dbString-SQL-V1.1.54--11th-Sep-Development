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
        Me.DGVcartReport.Size = New System.Drawing.Size(860, 437)
        Me.DGVcartReport.TabIndex = 0
        '
        'frmPrintCartReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(860, 449)
        Me.Controls.Add(Me.DGVcartReport)
        Me.Name = "frmPrintCartReport"
        Me.Text = "frmPrintCartReport"
        CType(Me.DGVcartReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGVcartReport As DataGridView
End Class
