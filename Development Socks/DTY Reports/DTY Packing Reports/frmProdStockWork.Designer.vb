<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProdStockWork
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
        Me.DGVNextJobsData = New System.Windows.Forms.DataGridView()
        Me.DGVOutputData = New System.Windows.Forms.DataGridView()
        Me.DGVPackWeight = New System.Windows.Forms.DataGridView()
        CType(Me.DGVNextJobsData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVOutputData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVPackWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVNextJobsData
        '
        Me.DGVNextJobsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVNextJobsData.Location = New System.Drawing.Point(12, 12)
        Me.DGVNextJobsData.Name = "DGVNextJobsData"
        Me.DGVNextJobsData.Size = New System.Drawing.Size(240, 150)
        Me.DGVNextJobsData.TabIndex = 0
        '
        'DGVOutputData
        '
        Me.DGVOutputData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVOutputData.Location = New System.Drawing.Point(438, 12)
        Me.DGVOutputData.Name = "DGVOutputData"
        Me.DGVOutputData.Size = New System.Drawing.Size(240, 150)
        Me.DGVOutputData.TabIndex = 1
        '
        'DGVPackWeight
        '
        Me.DGVPackWeight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVPackWeight.Location = New System.Drawing.Point(218, 178)
        Me.DGVPackWeight.Name = "DGVPackWeight"
        Me.DGVPackWeight.Size = New System.Drawing.Size(240, 150)
        Me.DGVPackWeight.TabIndex = 2
        '
        'frmProdStockWork
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 340)
        Me.Controls.Add(Me.DGVPackWeight)
        Me.Controls.Add(Me.DGVOutputData)
        Me.Controls.Add(Me.DGVNextJobsData)
        Me.Name = "frmProdStockWork"
        Me.Text = "frmProdStockWork"
        CType(Me.DGVNextJobsData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVOutputData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVPackWeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGVNextJobsData As DataGridView
    Friend WithEvents DGVOutputData As DataGridView
    Friend WithEvents DGVPackWeight As DataGridView
End Class
