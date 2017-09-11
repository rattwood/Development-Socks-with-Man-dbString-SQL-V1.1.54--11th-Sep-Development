<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEODReport
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
        Me.txtLotNumber = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnEOD = New System.Windows.Forms.Button()
        Me.btnCancelReport = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtLotNumber
        '
        Me.txtLotNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLotNumber.Location = New System.Drawing.Point(161, 49)
        Me.txtLotNumber.Name = "txtLotNumber"
        Me.txtLotNumber.Size = New System.Drawing.Size(266, 26)
        Me.txtLotNumber.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(233, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Job Number"
        '
        'btnEOD
        '
        Me.btnEOD.Enabled = False
        Me.btnEOD.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEOD.Location = New System.Drawing.Point(176, 137)
        Me.btnEOD.Name = "btnEOD"
        Me.btnEOD.Size = New System.Drawing.Size(211, 47)
        Me.btnEOD.TabIndex = 177
        Me.btnEOD.Text = "Create Last Cart of Day"
        Me.btnEOD.UseVisualStyleBackColor = True
        '
        'btnCancelReport
        '
        Me.btnCancelReport.Location = New System.Drawing.Point(12, 318)
        Me.btnCancelReport.Name = "btnCancelReport"
        Me.btnCancelReport.Size = New System.Drawing.Size(113, 47)
        Me.btnCancelReport.TabIndex = 178
        Me.btnCancelReport.Text = "Cancel"
        Me.btnCancelReport.UseVisualStyleBackColor = True
        '
        'EODReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(548, 377)
        Me.Controls.Add(Me.btnCancelReport)
        Me.Controls.Add(Me.btnEOD)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtLotNumber)
        Me.Name = "EODReport"
        Me.Text = "End Of Day Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtLotNumber As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnEOD As Button
    Friend WithEvents btnCancelReport As Button
End Class
