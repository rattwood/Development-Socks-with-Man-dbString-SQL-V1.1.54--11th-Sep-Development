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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnFullReport = New System.Windows.Forms.Button()
        Me.btnShortReport = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblMessage = New System.Windows.Forms.Label()
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
        Me.DGVNextJobsData.Visible = False
        '
        'DGVOutputData
        '
        Me.DGVOutputData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVOutputData.Location = New System.Drawing.Point(438, 12)
        Me.DGVOutputData.Name = "DGVOutputData"
        Me.DGVOutputData.Size = New System.Drawing.Size(240, 150)
        Me.DGVOutputData.TabIndex = 1
        Me.DGVOutputData.Visible = False
        '
        'DGVPackWeight
        '
        Me.DGVPackWeight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVPackWeight.Location = New System.Drawing.Point(218, 178)
        Me.DGVPackWeight.Name = "DGVPackWeight"
        Me.DGVPackWeight.Size = New System.Drawing.Size(240, 150)
        Me.DGVPackWeight.TabIndex = 2
        Me.DGVPackWeight.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(127, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(245, 20)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Stock Ready to Pack Reports"
        Me.Label1.Visible = False
        '
        'btnFullReport
        '
        Me.btnFullReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFullReport.Location = New System.Drawing.Point(276, 75)
        Me.btnFullReport.Name = "btnFullReport"
        Me.btnFullReport.Size = New System.Drawing.Size(182, 53)
        Me.btnFullReport.TabIndex = 18
        Me.btnFullReport.Text = "Full Report Sort, Colour and Packing"
        Me.btnFullReport.UseVisualStyleBackColor = True
        Me.btnFullReport.Visible = False
        '
        'btnShortReport
        '
        Me.btnShortReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShortReport.Location = New System.Drawing.Point(12, 75)
        Me.btnShortReport.Name = "btnShortReport"
        Me.btnShortReport.Size = New System.Drawing.Size(182, 53)
        Me.btnShortReport.TabIndex = 20
        Me.btnShortReport.Text = "Sort and Colour Only"
        Me.btnShortReport.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(173, 168)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(163, 53)
        Me.btnCancel.TabIndex = 21
        Me.btnCancel.Text = "CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Location = New System.Drawing.Point(90, 95)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(0, 20)
        Me.lblMessage.TabIndex = 22
        '
        'frmProdStockWork
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 232)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnShortReport)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnFullReport)
        Me.Controls.Add(Me.DGVPackWeight)
        Me.Controls.Add(Me.DGVOutputData)
        Me.Controls.Add(Me.DGVNextJobsData)
        Me.Name = "frmProdStockWork"
        Me.Text = "frmProdStockWork"
        CType(Me.DGVNextJobsData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVOutputData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVPackWeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGVNextJobsData As DataGridView
    Friend WithEvents DGVOutputData As DataGridView
    Friend WithEvents DGVPackWeight As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents btnFullReport As Button
    Friend WithEvents btnShortReport As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblMessage As Label
End Class
