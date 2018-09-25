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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintCartReport))
        Me.DGVcartReport = New System.Windows.Forms.DataGridView()
        Me.btnCartReport = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnExpotData = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.DGVcartReport.Size = New System.Drawing.Size(698, 190)
        Me.DGVcartReport.TabIndex = 0
        Me.DGVcartReport.Visible = False
        '
        'btnCartReport
        '
        Me.btnCartReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCartReport.ForeColor = System.Drawing.Color.Blue
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
        Me.btnCancel.ForeColor = System.Drawing.Color.Red
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
        Me.btnExpotData.ForeColor = System.Drawing.Color.Blue
        Me.btnExpotData.Location = New System.Drawing.Point(463, 83)
        Me.btnExpotData.Name = "btnExpotData"
        Me.btnExpotData.Size = New System.Drawing.Size(217, 43)
        Me.btnExpotData.TabIndex = 3
        Me.btnExpotData.Text = "Export Doff Data"
        Me.btnExpotData.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(179, 167)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 25)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Label1"
        Me.Label1.Visible = False
        '
        'frmPrintCartReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 339)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnExpotData)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCartReport)
        Me.Controls.Add(Me.DGVcartReport)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPrintCartReport"
        Me.Text = "Cart Reports"
        CType(Me.DGVcartReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGVcartReport As DataGridView
    Friend WithEvents btnCartReport As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnExpotData As Button
    Friend WithEvents Label1 As Label
End Class
