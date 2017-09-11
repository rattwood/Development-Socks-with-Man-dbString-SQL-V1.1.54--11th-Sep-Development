<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPackReports
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
        Me.btnEODRep = New System.Windows.Forms.Button()
        Me.btnDailyProdRep = New System.Windows.Forms.Button()
        Me.btnStockWorkRep = New System.Windows.Forms.Button()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnEODRep
        '
        Me.btnEODRep.Location = New System.Drawing.Point(201, 24)
        Me.btnEODRep.Name = "btnEODRep"
        Me.btnEODRep.Size = New System.Drawing.Size(151, 23)
        Me.btnEODRep.TabIndex = 0
        Me.btnEODRep.Text = "EOD Report"
        Me.btnEODRep.UseVisualStyleBackColor = True
        '
        'btnDailyProdRep
        '
        Me.btnDailyProdRep.Location = New System.Drawing.Point(201, 64)
        Me.btnDailyProdRep.Name = "btnDailyProdRep"
        Me.btnDailyProdRep.Size = New System.Drawing.Size(151, 23)
        Me.btnDailyProdRep.TabIndex = 1
        Me.btnDailyProdRep.Text = "Daily Prod Report"
        Me.btnDailyProdRep.UseVisualStyleBackColor = True
        '
        'btnStockWorkRep
        '
        Me.btnStockWorkRep.Location = New System.Drawing.Point(201, 106)
        Me.btnStockWorkRep.Name = "btnStockWorkRep"
        Me.btnStockWorkRep.Size = New System.Drawing.Size(151, 23)
        Me.btnStockWorkRep.TabIndex = 2
        Me.btnStockWorkRep.Text = "Stock Work Report"
        Me.btnStockWorkRep.UseVisualStyleBackColor = True
        '
        'btnReturn
        '
        Me.btnReturn.Location = New System.Drawing.Point(12, 226)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(75, 23)
        Me.btnReturn.TabIndex = 3
        Me.btnReturn.Text = "Return"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(32, 152)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(14, 20)
        Me.lblMessage.TabIndex = 4
        Me.lblMessage.Text = " "
        '
        'frmPackReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 261)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.btnStockWorkRep)
        Me.Controls.Add(Me.btnDailyProdRep)
        Me.Controls.Add(Me.btnEODRep)
        Me.Name = "frmPackReports"
        Me.Text = "Packing Reports"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnEODRep As Button
    Friend WithEvents btnDailyProdRep As Button
    Friend WithEvents btnStockWorkRep As Button
    Friend WithEvents btnReturn As Button
    Friend WithEvents lblMessage As Label
End Class
