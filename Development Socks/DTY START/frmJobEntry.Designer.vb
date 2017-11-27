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
        Me.lblScanType = New System.Windows.Forms.Label()
        Me.txtLotNumber = New System.Windows.Forms.TextBox()
        Me.btnJobReport = New System.Windows.Forms.Button()
        Me.ToraydbBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Toraydb = New Development_Socks.Toraydb()
        Me.btnCartReport = New System.Windows.Forms.Button()
        Me.btnSearchCone = New System.Windows.Forms.Button()
        Me.txtBoxCartReport = New System.Windows.Forms.TextBox()
        Me.btnCancelReport = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnDefRep = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExChangeCheeseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindCheeseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PackingGradeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AGradeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ASGradeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.P15ASToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.P25ASToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.P35ASToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BALADGradeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ALToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ADToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BSGradesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.P20BSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.P30BSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.P35BSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReCheckToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DailyPackingReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EndOfDayReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StockToProcessReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblMessage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusPanel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblGrade = New System.Windows.Forms.Label()
        Me.txtGrade = New System.Windows.Forms.TextBox()
        Me.lblSelectGrade = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPilotCount = New System.Windows.Forms.TextBox()
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtOperator
        '
        Me.txtOperator.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtOperator.Location = New System.Drawing.Point(281, 86)
        Me.txtOperator.Name = "txtOperator"
        Me.txtOperator.Size = New System.Drawing.Size(292, 44)
        Me.txtOperator.TabIndex = 2
        Me.txtOperator.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(213, 31)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Operator Name"
        '
        'lblScanType
        '
        Me.lblScanType.AutoSize = True
        Me.lblScanType.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScanType.Location = New System.Drawing.Point(18, 159)
        Me.lblScanType.Name = "lblScanType"
        Me.lblScanType.Size = New System.Drawing.Size(0, 31)
        Me.lblScanType.TabIndex = 3
        '
        'txtLotNumber
        '
        Me.txtLotNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtLotNumber.Location = New System.Drawing.Point(399, 153)
        Me.txtLotNumber.Name = "txtLotNumber"
        Me.txtLotNumber.Size = New System.Drawing.Size(369, 44)
        Me.txtLotNumber.TabIndex = 4
        '
        'btnJobReport
        '
        Me.btnJobReport.Location = New System.Drawing.Point(580, 377)
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
        Me.btnCartReport.Location = New System.Drawing.Point(20, 297)
        Me.btnCartReport.Name = "btnCartReport"
        Me.btnCartReport.Size = New System.Drawing.Size(113, 47)
        Me.btnCartReport.TabIndex = 9
        Me.btnCartReport.Text = "Create Cart Report"
        Me.btnCartReport.UseVisualStyleBackColor = True
        '
        'btnSearchCone
        '
        Me.btnSearchCone.Location = New System.Drawing.Point(425, 377)
        Me.btnSearchCone.Name = "btnSearchCone"
        Me.btnSearchCone.Size = New System.Drawing.Size(113, 47)
        Me.btnSearchCone.TabIndex = 12
        Me.btnSearchCone.Text = "Search  Cheese"
        Me.btnSearchCone.UseVisualStyleBackColor = True
        '
        'txtBoxCartReport
        '
        Me.txtBoxCartReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtBoxCartReport.Location = New System.Drawing.Point(139, 300)
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
        Me.Label3.Location = New System.Drawing.Point(17, 242)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 37)
        Me.Label3.TabIndex = 175
        Me.Label3.Text = "Label3"
        Me.Label3.Visible = False
        '
        'btnDefRep
        '
        Me.btnDefRep.Location = New System.Drawing.Point(269, 377)
        Me.btnDefRep.Name = "btnDefRep"
        Me.btnDefRep.Size = New System.Drawing.Size(113, 47)
        Me.btnDefRep.TabIndex = 178
        Me.btnDefRep.Text = "Defect Report"
        Me.btnDefRep.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.PackingGradeToolStripMenuItem, Me.ReportsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(821, 24)
        Me.MenuStrip1.TabIndex = 179
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExChangeCheeseToolStripMenuItem, Me.FindCheeseToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        Me.ToolsToolStripMenuItem.Visible = False
        '
        'ExChangeCheeseToolStripMenuItem
        '
        Me.ExChangeCheeseToolStripMenuItem.Name = "ExChangeCheeseToolStripMenuItem"
        Me.ExChangeCheeseToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.ExChangeCheeseToolStripMenuItem.Text = "ExChange Cheese"
        '
        'FindCheeseToolStripMenuItem
        '
        Me.FindCheeseToolStripMenuItem.Name = "FindCheeseToolStripMenuItem"
        Me.FindCheeseToolStripMenuItem.Size = New System.Drawing.Size(167, 22)
        Me.FindCheeseToolStripMenuItem.Text = "Find Cheese"
        '
        'PackingGradeToolStripMenuItem
        '
        Me.PackingGradeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AGradeToolStripMenuItem, Me.ASGradeToolStripMenuItem, Me.BALADGradeToolStripMenuItem, Me.BSGradesToolStripMenuItem, Me.ReCheckToolStripMenuItem, Me.WasteToolStripMenuItem})
        Me.PackingGradeToolStripMenuItem.Name = "PackingGradeToolStripMenuItem"
        Me.PackingGradeToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.PackingGradeToolStripMenuItem.Text = "Packing Grade"
        Me.PackingGradeToolStripMenuItem.Visible = False
        '
        'AGradeToolStripMenuItem
        '
        Me.AGradeToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.AGradeToolStripMenuItem.Name = "AGradeToolStripMenuItem"
        Me.AGradeToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.AGradeToolStripMenuItem.Text = "A"
        '
        'ASGradeToolStripMenuItem
        '
        Me.ASGradeToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ASGradeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.P15ASToolStripMenuItem, Me.P25ASToolStripMenuItem, Me.P35ASToolStripMenuItem})
        Me.ASGradeToolStripMenuItem.Name = "ASGradeToolStripMenuItem"
        Me.ASGradeToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.ASGradeToolStripMenuItem.Text = "AS"
        '
        'P15ASToolStripMenuItem
        '
        Me.P15ASToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.P15ASToolStripMenuItem.Name = "P15ASToolStripMenuItem"
        Me.P15ASToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.P15ASToolStripMenuItem.Text = "P15 AS"
        '
        'P25ASToolStripMenuItem
        '
        Me.P25ASToolStripMenuItem.BackColor = System.Drawing.Color.Yellow
        Me.P25ASToolStripMenuItem.Name = "P25ASToolStripMenuItem"
        Me.P25ASToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.P25ASToolStripMenuItem.Text = "P25 AS"
        '
        'P35ASToolStripMenuItem
        '
        Me.P35ASToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.P35ASToolStripMenuItem.Name = "P35ASToolStripMenuItem"
        Me.P35ASToolStripMenuItem.Size = New System.Drawing.Size(110, 22)
        Me.P35ASToolStripMenuItem.Text = "P35 AS"
        '
        'BALADGradeToolStripMenuItem
        '
        Me.BALADGradeToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.BALADGradeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BToolStripMenuItem, Me.ALToolStripMenuItem, Me.ADToolStripMenuItem})
        Me.BALADGradeToolStripMenuItem.Name = "BALADGradeToolStripMenuItem"
        Me.BALADGradeToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.BALADGradeToolStripMenuItem.Text = "B AL AD Grades"
        '
        'BToolStripMenuItem
        '
        Me.BToolStripMenuItem.Name = "BToolStripMenuItem"
        Me.BToolStripMenuItem.Size = New System.Drawing.Size(90, 22)
        Me.BToolStripMenuItem.Text = "B"
        '
        'ALToolStripMenuItem
        '
        Me.ALToolStripMenuItem.Name = "ALToolStripMenuItem"
        Me.ALToolStripMenuItem.Size = New System.Drawing.Size(90, 22)
        Me.ALToolStripMenuItem.Text = "AL"
        '
        'ADToolStripMenuItem
        '
        Me.ADToolStripMenuItem.Name = "ADToolStripMenuItem"
        Me.ADToolStripMenuItem.Size = New System.Drawing.Size(90, 22)
        Me.ADToolStripMenuItem.Text = "AD"
        '
        'BSGradesToolStripMenuItem
        '
        Me.BSGradesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.P20BSToolStripMenuItem, Me.P30BSToolStripMenuItem, Me.P35BSToolStripMenuItem})
        Me.BSGradesToolStripMenuItem.Name = "BSGradesToolStripMenuItem"
        Me.BSGradesToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.BSGradesToolStripMenuItem.Text = "BS Grades"
        '
        'P20BSToolStripMenuItem
        '
        Me.P20BSToolStripMenuItem.Name = "P20BSToolStripMenuItem"
        Me.P20BSToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.P20BSToolStripMenuItem.Text = "P20 BS"
        '
        'P30BSToolStripMenuItem
        '
        Me.P30BSToolStripMenuItem.Name = "P30BSToolStripMenuItem"
        Me.P30BSToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.P30BSToolStripMenuItem.Text = "P30 BS"
        '
        'P35BSToolStripMenuItem
        '
        Me.P35BSToolStripMenuItem.Name = "P35BSToolStripMenuItem"
        Me.P35BSToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.P35BSToolStripMenuItem.Text = "P35 BS"
        '
        'ReCheckToolStripMenuItem
        '
        Me.ReCheckToolStripMenuItem.Name = "ReCheckToolStripMenuItem"
        Me.ReCheckToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.ReCheckToolStripMenuItem.Text = "ReCheck"
        '
        'WasteToolStripMenuItem
        '
        Me.WasteToolStripMenuItem.Name = "WasteToolStripMenuItem"
        Me.WasteToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.WasteToolStripMenuItem.Text = "Waste"
        '
        'ReportsToolStripMenuItem
        '
        Me.ReportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DailyPackingReportToolStripMenuItem, Me.EndOfDayReportToolStripMenuItem, Me.StockToProcessReportToolStripMenuItem})
        Me.ReportsToolStripMenuItem.Name = "ReportsToolStripMenuItem"
        Me.ReportsToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.ReportsToolStripMenuItem.Text = "Reports"
        Me.ReportsToolStripMenuItem.Visible = False
        '
        'DailyPackingReportToolStripMenuItem
        '
        Me.DailyPackingReportToolStripMenuItem.Name = "DailyPackingReportToolStripMenuItem"
        Me.DailyPackingReportToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.DailyPackingReportToolStripMenuItem.Text = "Daily Packing Report"
        '
        'EndOfDayReportToolStripMenuItem
        '
        Me.EndOfDayReportToolStripMenuItem.Name = "EndOfDayReportToolStripMenuItem"
        Me.EndOfDayReportToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.EndOfDayReportToolStripMenuItem.Text = "End Of Day Report"
        '
        'StockToProcessReportToolStripMenuItem
        '
        Me.StockToProcessReportToolStripMenuItem.Name = "StockToProcessReportToolStripMenuItem"
        Me.StockToProcessReportToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.StockToProcessReportToolStripMenuItem.Text = "Stock to Process Report"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.lblMessage, Me.StatusPanel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 448)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(821, 22)
        Me.StatusStrip1.TabIndex = 180
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'lblMessage
        '
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(0, 17)
        '
        'StatusPanel
        '
        Me.StatusPanel.Name = "StatusPanel"
        Me.StatusPanel.Size = New System.Drawing.Size(120, 17)
        Me.StatusPanel.Text = "ToolStripStatusLabel2"
        '
        'lblGrade
        '
        Me.lblGrade.AutoSize = True
        Me.lblGrade.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrade.Location = New System.Drawing.Point(15, 35)
        Me.lblGrade.Name = "lblGrade"
        Me.lblGrade.Size = New System.Drawing.Size(216, 31)
        Me.lblGrade.TabIndex = 181
        Me.lblGrade.Text = "Selected Grade"
        '
        'txtGrade
        '
        Me.txtGrade.Enabled = False
        Me.txtGrade.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtGrade.Location = New System.Drawing.Point(281, 29)
        Me.txtGrade.Name = "txtGrade"
        Me.txtGrade.Size = New System.Drawing.Size(142, 44)
        Me.txtGrade.TabIndex = 1
        '
        'lblSelectGrade
        '
        Me.lblSelectGrade.AutoSize = True
        Me.lblSelectGrade.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectGrade.ForeColor = System.Drawing.Color.Red
        Me.lblSelectGrade.Location = New System.Drawing.Point(429, 41)
        Me.lblSelectGrade.Name = "lblSelectGrade"
        Me.lblSelectGrade.Size = New System.Drawing.Size(282, 25)
        Me.lblSelectGrade.TabIndex = 183
        Me.lblSelectGrade.Text = "Please Select Grade First"
        Me.lblSelectGrade.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(233, 254)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(219, 20)
        Me.Label2.TabIndex = 184
        Me.Label2.Text = "Enter Toatal Piolt Cheese "
        Me.Label2.Visible = False
        '
        'txtPilotCount
        '
        Me.txtPilotCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPilotCount.Location = New System.Drawing.Point(458, 251)
        Me.txtPilotCount.MaxLength = 2
        Me.txtPilotCount.Name = "txtPilotCount"
        Me.txtPilotCount.Size = New System.Drawing.Size(28, 26)
        Me.txtPilotCount.TabIndex = 185
        Me.txtPilotCount.Visible = False
        '
        'frmJobEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(821, 470)
        Me.Controls.Add(Me.txtPilotCount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblSelectGrade)
        Me.Controls.Add(Me.txtGrade)
        Me.Controls.Add(Me.lblGrade)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnDefRep)
        Me.Controls.Add(Me.btnCancelReport)
        Me.Controls.Add(Me.txtBoxCartReport)
        Me.Controls.Add(Me.btnSearchCone)
        Me.Controls.Add(Me.btnCartReport)
        Me.Controls.Add(Me.btnJobReport)
        Me.Controls.Add(Me.txtLotNumber)
        Me.Controls.Add(Me.lblScanType)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtOperator)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmJobEntry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Job Entry"
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtOperator As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblScanType As Label
    Friend WithEvents txtLotNumber As TextBox
    Friend WithEvents ToraydbBindingSource As BindingSource
    Friend WithEvents Toraydb As Toraydb
    Friend WithEvents btnJobReport As Button
    Friend WithEvents btnCartReport As Button
    Friend WithEvents btnSearchCone As Button
    Friend WithEvents txtBoxCartReport As TextBox
    Friend WithEvents btnCancelReport As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btnDefRep As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExChangeCheeseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FindCheeseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PackingGradeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AGradeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ASGradeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents P15ASToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents P25ASToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents P35ASToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BALADGradeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BSGradesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReCheckToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WasteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReportsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DailyPackingReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EndOfDayReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StockToProcessReportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents lblMessage As ToolStripStatusLabel
    Friend WithEvents lblGrade As Label
    Friend WithEvents txtGrade As TextBox
    Friend WithEvents BToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ALToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ADToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents P20BSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents P30BSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents P35BSToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents lblSelectGrade As Label
    Friend WithEvents StatusPanel As ToolStripStatusLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPilotCount As TextBox
End Class
