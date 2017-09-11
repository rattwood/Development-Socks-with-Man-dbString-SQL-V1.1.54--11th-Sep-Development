<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmJobEntry
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
        Me.components = New System.ComponentModel.Container()
        Me.txtOperator = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtLotNumber = New System.Windows.Forms.TextBox()
        Me.btnJobReport = New System.Windows.Forms.Button()
        Me.ToraydbBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Toraydb = New Development_Socks.Toraydb()
        Me.btnCartReport = New System.Windows.Forms.Button()
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.btnExChangeCone = New System.Windows.Forms.Button()
        Me.btnSearchCone = New System.Windows.Forms.Button()
        Me.txtBoxCartReport = New System.Windows.Forms.TextBox()
        Me.btnCancelReport = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnReports = New System.Windows.Forms.Button()
        Me.btnDefRep = New System.Windows.Forms.Button()
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtOperator
        '
        Me.txtOperator.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtOperator.Location = New System.Drawing.Point(273, 69)
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.Size = New System.Drawing.Size(471, 44)
        Me.txtOperator.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(213, 31)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Operator Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 153)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(210, 31)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Scan Bar Code"
        '
        'txtLotNumber
        '
        Me.txtLotNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtLotNumber.Location = New System.Drawing.Point(273, 140)
        Me.txtLotNumber.Name = "txtLotNumber"
        Me.txtLotNumber.Size = New System.Drawing.Size(471, 44)
        Me.txtLotNumber.TabIndex = 4
        '
        'btnJobReport
        '
        Me.btnJobReport.Location = New System.Drawing.Point(253, 377)
        Me.btnJobReport.Name = "btnJobReport"
        Me.btnJobReport.Size = New System.Drawing.Size(113, 47)
        Me.btnJobReport.TabIndex = 8
        Me.btnJobReport.Text = "M/C Report"
        Me.btnJobReport.UseVisualStyleBackColor = True
        '
        'ToraydbBindingSource
        '
        Me.ToraydbBindingSource.DataSource = Me.Toraydb
        Me.ToraydbBindingSource.Position = 0
        '
        'Toraydb
        '
        Me.Toraydb.DataSetName = "Toraydb"
        Me.Toraydb.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btnCartReport
        '
        Me.btnCartReport.Location = New System.Drawing.Point(20, 271)
        Me.btnCartReport.Name = "btnCartReport"
        Me.btnCartReport.Size = New System.Drawing.Size(113, 47)
        Me.btnCartReport.TabIndex = 9
        Me.btnCartReport.Text = "Create Cart Report"
        Me.btnCartReport.UseVisualStyleBackColor = True
        '
        'btnSettings
        '
        Me.btnSettings.Location = New System.Drawing.Point(12, 12)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(75, 23)
        Me.btnSettings.TabIndex = 10
        Me.btnSettings.Text = "Settings"
        Me.btnSettings.UseVisualStyleBackColor = True
        '
        'btnExChangeCone
        '
        Me.btnExChangeCone.Location = New System.Drawing.Point(407, 324)
        Me.btnExChangeCone.Name = "btnExChangeCone"
        Me.btnExChangeCone.Size = New System.Drawing.Size(113, 47)
        Me.btnExChangeCone.TabIndex = 11
        Me.btnExChangeCone.Text = "ExChange Cheese"
        Me.btnExChangeCone.UseVisualStyleBackColor = True
        Me.btnExChangeCone.Visible = False
        '
        'btnSearchCone
        '
        Me.btnSearchCone.Location = New System.Drawing.Point(253, 324)
        Me.btnSearchCone.Name = "btnSearchCone"
        Me.btnSearchCone.Size = New System.Drawing.Size(113, 47)
        Me.btnSearchCone.TabIndex = 12
        Me.btnSearchCone.Text = "Search  Cheese"
        Me.btnSearchCone.UseVisualStyleBackColor = True
        '
        'txtBoxCartReport
        '
        Me.txtBoxCartReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtBoxCartReport.Location = New System.Drawing.Point(139, 274)
        Me.txtBoxCartReport.Name = "txtBoxCartReport"
        Me.txtBoxCartReport.Size = New System.Drawing.Size(471, 44)
        Me.txtBoxCartReport.TabIndex = 13
        Me.txtBoxCartReport.Visible = False
        '
        'btnCancelReport
        '
        Me.btnCancelReport.Location = New System.Drawing.Point(20, 373)
        Me.btnCancelReport.Name = "btnCancelReport"
        Me.btnCancelReport.Size = New System.Drawing.Size(113, 47)
        Me.btnCancelReport.TabIndex = 14
        Me.btnCancelReport.Text = "Cancel"
        Me.btnCancelReport.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Red
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 197)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 37)
        Me.Label3.TabIndex = 175
        Me.Label3.Text = "Label3"
        Me.Label3.Visible = False
        '
        'btnReports
        '
        Me.btnReports.Location = New System.Drawing.Point(555, 324)
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(113, 47)
        Me.btnReports.TabIndex = 177
        Me.btnReports.Text = "Reports"
        Me.btnReports.UseVisualStyleBackColor = True
        Me.btnReports.Visible = False
        '
        'btnDefRep
        '
        Me.btnDefRep.Location = New System.Drawing.Point(407, 377)
        Me.btnDefRep.Name = "btnDefRep"
        Me.btnDefRep.Size = New System.Drawing.Size(113, 47)
        Me.btnDefRep.TabIndex = 178
        Me.btnDefRep.Text = "Defect Report"
        Me.btnDefRep.UseVisualStyleBackColor = True
        '
        'frmJobEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(821, 432)
        Me.Controls.Add(Me.btnDefRep)
        Me.Controls.Add(Me.btnReports)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCancelReport)
        Me.Controls.Add(Me.txtBoxCartReport)
        Me.Controls.Add(Me.btnSearchCone)
        Me.Controls.Add(Me.btnExChangeCone)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.btnCartReport)
        Me.Controls.Add(Me.btnJobReport)
        Me.Controls.Add(Me.txtLotNumber)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtOperator)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmJobEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Job Entry"
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtOperator As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtLotNumber As TextBox
    Friend WithEvents ToraydbBindingSource As BindingSource
    Friend WithEvents Toraydb As Toraydb
    Friend WithEvents btnJobReport As Button
    Friend WithEvents btnCartReport As Button
    Friend WithEvents btnSettings As Button
    Friend WithEvents btnExChangeCone As Button
    Friend WithEvents btnSearchCone As Button
    Friend WithEvents txtBoxCartReport As TextBox
    Friend WithEvents btnCancelReport As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btnReports As Button
    Friend WithEvents btnDefRep As Button
End Class
