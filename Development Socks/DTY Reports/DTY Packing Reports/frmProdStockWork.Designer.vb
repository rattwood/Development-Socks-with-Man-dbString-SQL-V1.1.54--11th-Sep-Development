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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProdStockWork))
        Me.DGVNextJobsData = New System.Windows.Forms.DataGridView()
        Me.DGVOutputData = New System.Windows.Forms.DataGridView()
        Me.DGVPackWeight = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnFullReport = New System.Windows.Forms.Button()
        Me.btnShortReport = New System.Windows.Forms.Button()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnHome = New System.Windows.Forms.Button()
        Me.lblSearchRange = New System.Windows.Forms.Label()
        CType(Me.DGVNextJobsData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVOutputData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVPackWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVNextJobsData
        '
        Me.DGVNextJobsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVNextJobsData.Location = New System.Drawing.Point(282, 166)
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
        Me.DGVPackWeight.Location = New System.Drawing.Point(12, 41)
        Me.DGVPackWeight.Name = "DGVPackWeight"
        Me.DGVPackWeight.Size = New System.Drawing.Size(240, 150)
        Me.DGVPackWeight.TabIndex = 2
        Me.DGVPackWeight.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(167, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 20)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Packing Stock Reports"
        '
        'btnFullReport
        '
        Me.btnFullReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFullReport.Location = New System.Drawing.Point(301, 75)
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
        Me.btnShortReport.Location = New System.Drawing.Point(52, 75)
        Me.btnShortReport.Name = "btnShortReport"
        Me.btnShortReport.Size = New System.Drawing.Size(182, 53)
        Me.btnShortReport.TabIndex = 20
        Me.btnShortReport.Text = "Stock in Sort and Colour Only"
        Me.btnShortReport.UseVisualStyleBackColor = True
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Location = New System.Drawing.Point(57, 188)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(0, 20)
        Me.lblMessage.TabIndex = 22
        Me.lblMessage.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 178)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Label2"
        Me.Label2.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 208)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Label3"
        Me.Label3.Visible = False
        '
        'btnHome
        '
        Me.btnHome.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnHome.BackgroundImage = Global.Development_Socks.My.Resources.Resources.home_icon_silhouette
        Me.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnHome.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHome.Location = New System.Drawing.Point(12, 268)
        Me.btnHome.Name = "btnHome"
        Me.btnHome.Size = New System.Drawing.Size(121, 48)
        Me.btnHome.TabIndex = 71
        Me.btnHome.UseVisualStyleBackColor = False
        '
        'lblSearchRange
        '
        Me.lblSearchRange.AutoSize = True
        Me.lblSearchRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchRange.Location = New System.Drawing.Point(119, 41)
        Me.lblSearchRange.Name = "lblSearchRange"
        Me.lblSearchRange.Size = New System.Drawing.Size(192, 20)
        Me.lblSearchRange.TabIndex = 72
        Me.lblSearchRange.Text = "Packing Stock Reports"
        '
        'frmProdStockWork
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(534, 328)
        Me.Controls.Add(Me.lblSearchRange)
        Me.Controls.Add(Me.btnHome)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnFullReport)
        Me.Controls.Add(Me.DGVOutputData)
        Me.Controls.Add(Me.btnShortReport)
        Me.Controls.Add(Me.DGVNextJobsData)
        Me.Controls.Add(Me.DGVPackWeight)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(550, 367)
        Me.Name = "frmProdStockWork"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
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
    Friend WithEvents lblMessage As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnHome As Button
    Friend WithEvents lblSearchRange As Label
End Class
