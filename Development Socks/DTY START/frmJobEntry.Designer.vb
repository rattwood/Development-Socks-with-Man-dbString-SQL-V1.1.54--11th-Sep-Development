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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJobEntry))
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
        Me.ToolStripTextBox3 = New System.Windows.Forms.ToolStripTextBox()
        Me.AGradeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GradeAReCheckAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SpecialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pilot6ChToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pilot15ChToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pilot20ChToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox2 = New System.Windows.Forms.ToolStripTextBox()
        Me.ReCheckToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PackingHLProductsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox11 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox12 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateHCartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateLCartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox13 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox15 = New System.Windows.Forms.ToolStripTextBox()
        Me.PackGradedHLCartsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HCartsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox10 = New System.Windows.Forms.ToolStripTextBox()
        Me.HDDToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HDToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HMMToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HLToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HLLToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HBToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox18 = New System.Windows.Forms.ToolStripTextBox()
        Me.HSDToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HSMToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HSLToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HSBToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LCartsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox19 = New System.Windows.Forms.ToolStripTextBox()
        Me.LDDToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LDToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LMMToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LLToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LLLToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LBToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox20 = New System.Windows.Forms.ToolStripTextBox()
        Me.LSDToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LSMToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LSLToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LSBToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DailyPackingReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EndOfDayReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StockToProcessReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintFormsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox4 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CompareSTDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Round1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Round2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Round3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StdSheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Round1ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Round2ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Round3ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HLStdToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox5 = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ReCheckToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblMessage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusPanel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblGrade = New System.Windows.Forms.Label()
        Me.txtGrade = New System.Windows.Forms.TextBox()
        Me.lblSelectGrade = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPilotCount = New System.Windows.Forms.TextBox()
        Me.btnHLReport = New System.Windows.Forms.Button()
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
        Me.txtOperator.TabIndex = 5
        Me.txtOperator.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(213, 31)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Operator Name"
        '
        'lblScanType
        '
        Me.lblScanType.AutoSize = True
        Me.lblScanType.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScanType.Location = New System.Drawing.Point(18, 159)
        Me.lblScanType.Name = "lblScanType"
        Me.lblScanType.Size = New System.Drawing.Size(0, 31)
        Me.lblScanType.TabIndex = 7
        '
        'txtLotNumber
        '
        Me.txtLotNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtLotNumber.Location = New System.Drawing.Point(399, 153)
        Me.txtLotNumber.Name = "txtLotNumber"
        Me.txtLotNumber.Size = New System.Drawing.Size(381, 44)
        Me.txtLotNumber.TabIndex = 6
        '
        'btnJobReport
        '
        Me.btnJobReport.Location = New System.Drawing.Point(580, 377)
        Me.btnJobReport.Name = "btnJobReport"
        Me.btnJobReport.Size = New System.Drawing.Size(113, 47)
        Me.btnJobReport.TabIndex = 17
        Me.btnJobReport.Text = "M/C Report"
        Me.btnJobReport.UseVisualStyleBackColor = True
        Me.btnJobReport.Visible = False
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
        Me.btnCartReport.Location = New System.Drawing.Point(20, 316)
        Me.btnCartReport.Name = "btnCartReport"
        Me.btnCartReport.Size = New System.Drawing.Size(113, 47)
        Me.btnCartReport.TabIndex = 12
        Me.btnCartReport.Text = "Create Cart Report"
        Me.btnCartReport.UseVisualStyleBackColor = True
        '
        'btnSearchCone
        '
        Me.btnSearchCone.Location = New System.Drawing.Point(350, 377)
        Me.btnSearchCone.Name = "btnSearchCone"
        Me.btnSearchCone.Size = New System.Drawing.Size(113, 47)
        Me.btnSearchCone.TabIndex = 16
        Me.btnSearchCone.Text = "Search  Cheese"
        Me.btnSearchCone.UseVisualStyleBackColor = True
        '
        'txtBoxCartReport
        '
        Me.txtBoxCartReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtBoxCartReport.Location = New System.Drawing.Point(139, 319)
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
        Me.Label3.Location = New System.Drawing.Point(11, 190)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 37)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Label3"
        Me.Label3.Visible = False
        '
        'btnDefRep
        '
        Me.btnDefRep.Location = New System.Drawing.Point(269, 377)
        Me.btnDefRep.Name = "btnDefRep"
        Me.btnDefRep.Size = New System.Drawing.Size(113, 47)
        Me.btnDefRep.TabIndex = 15
        Me.btnDefRep.Text = "Defect Report"
        Me.btnDefRep.UseVisualStyleBackColor = True
        Me.btnDefRep.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.PackingGradeToolStripMenuItem, Me.PackingHLProductsToolStripMenuItem, Me.ReportsToolStripMenuItem, Me.PrintFormsToolStripMenuItem, Me.PrintToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(821, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Image = CType(resources.GetObject("SettingsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(77, 20)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExChangeCheeseToolStripMenuItem, Me.FindCheeseToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        Me.ToolsToolStripMenuItem.Visible = False
        '
        'ExChangeCheeseToolStripMenuItem
        '
        Me.ExChangeCheeseToolStripMenuItem.Name = "ExChangeCheeseToolStripMenuItem"
        Me.ExChangeCheeseToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ExChangeCheeseToolStripMenuItem.Text = "ExChange Cheese"
        '
        'FindCheeseToolStripMenuItem
        '
        Me.FindCheeseToolStripMenuItem.Name = "FindCheeseToolStripMenuItem"
        Me.FindCheeseToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.FindCheeseToolStripMenuItem.Text = "Find Cheese"
        '
        'PackingGradeToolStripMenuItem
        '
        Me.PackingGradeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox3, Me.AGradeToolStripMenuItem, Me.ASGradeToolStripMenuItem, Me.BALADGradeToolStripMenuItem, Me.BSGradesToolStripMenuItem, Me.ToolStripMenuItem2, Me.WasteToolStripMenuItem, Me.ToolStripSeparator3, Me.ToolStripTextBox2, Me.ReCheckToolStripMenuItem})
        Me.PackingGradeToolStripMenuItem.Name = "PackingGradeToolStripMenuItem"
        Me.PackingGradeToolStripMenuItem.Size = New System.Drawing.Size(154, 20)
        Me.PackingGradeToolStripMenuItem.Text = "Packing Normal Products"
        Me.PackingGradeToolStripMenuItem.Visible = False
        '
        'ToolStripTextBox3
        '
        Me.ToolStripTextBox3.Font = New System.Drawing.Font("Segoe UI Black", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox3.Name = "ToolStripTextBox3"
        Me.ToolStripTextBox3.Size = New System.Drawing.Size(125, 28)
        Me.ToolStripTextBox3.Text = "PACKING TYPE"
        '
        'AGradeToolStripMenuItem
        '
        Me.AGradeToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.AGradeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GradeAReCheckAToolStripMenuItem, Me.SpecialToolStripMenuItem})
        Me.AGradeToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AGradeToolStripMenuItem.Name = "AGradeToolStripMenuItem"
        Me.AGradeToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AGradeToolStripMenuItem.Text = "A"
        '
        'GradeAReCheckAToolStripMenuItem
        '
        Me.GradeAReCheckAToolStripMenuItem.BackColor = System.Drawing.Color.LightGreen
        Me.GradeAReCheckAToolStripMenuItem.Name = "GradeAReCheckAToolStripMenuItem"
        Me.GradeAReCheckAToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.GradeAReCheckAToolStripMenuItem.Text = "Grade A & ReCheck A"
        '
        'SpecialToolStripMenuItem
        '
        Me.SpecialToolStripMenuItem.Enabled = False
        Me.SpecialToolStripMenuItem.Name = "SpecialToolStripMenuItem"
        Me.SpecialToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.SpecialToolStripMenuItem.Text = "Grade A Special"
        '
        'ASGradeToolStripMenuItem
        '
        Me.ASGradeToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.ASGradeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.P15ASToolStripMenuItem, Me.P25ASToolStripMenuItem, Me.P35ASToolStripMenuItem})
        Me.ASGradeToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ASGradeToolStripMenuItem.Name = "ASGradeToolStripMenuItem"
        Me.ASGradeToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ASGradeToolStripMenuItem.Text = "AS"
        '
        'P15ASToolStripMenuItem
        '
        Me.P15ASToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.P15ASToolStripMenuItem.Name = "P15ASToolStripMenuItem"
        Me.P15ASToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.P15ASToolStripMenuItem.Text = "P15 AS"
        '
        'P25ASToolStripMenuItem
        '
        Me.P25ASToolStripMenuItem.BackColor = System.Drawing.Color.Yellow
        Me.P25ASToolStripMenuItem.Name = "P25ASToolStripMenuItem"
        Me.P25ASToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.P25ASToolStripMenuItem.Text = "P25 AS"
        '
        'P35ASToolStripMenuItem
        '
        Me.P35ASToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.P35ASToolStripMenuItem.Name = "P35ASToolStripMenuItem"
        Me.P35ASToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.P35ASToolStripMenuItem.Text = "P35 AS"
        '
        'BALADGradeToolStripMenuItem
        '
        Me.BALADGradeToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.BALADGradeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BToolStripMenuItem, Me.ALToolStripMenuItem, Me.ADToolStripMenuItem})
        Me.BALADGradeToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BALADGradeToolStripMenuItem.Name = "BALADGradeToolStripMenuItem"
        Me.BALADGradeToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.BALADGradeToolStripMenuItem.Text = "B AL AD Grades"
        '
        'BToolStripMenuItem
        '
        Me.BToolStripMenuItem.Name = "BToolStripMenuItem"
        Me.BToolStripMenuItem.Size = New System.Drawing.Size(91, 22)
        Me.BToolStripMenuItem.Text = "B"
        '
        'ALToolStripMenuItem
        '
        Me.ALToolStripMenuItem.Name = "ALToolStripMenuItem"
        Me.ALToolStripMenuItem.Size = New System.Drawing.Size(91, 22)
        Me.ALToolStripMenuItem.Text = "AL"
        '
        'ADToolStripMenuItem
        '
        Me.ADToolStripMenuItem.Name = "ADToolStripMenuItem"
        Me.ADToolStripMenuItem.Size = New System.Drawing.Size(91, 22)
        Me.ADToolStripMenuItem.Text = "AD"
        '
        'BSGradesToolStripMenuItem
        '
        Me.BSGradesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.P20BSToolStripMenuItem, Me.P30BSToolStripMenuItem, Me.P35BSToolStripMenuItem})
        Me.BSGradesToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BSGradesToolStripMenuItem.Name = "BSGradesToolStripMenuItem"
        Me.BSGradesToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.BSGradesToolStripMenuItem.Text = "BS Grades"
        '
        'P20BSToolStripMenuItem
        '
        Me.P20BSToolStripMenuItem.Name = "P20BSToolStripMenuItem"
        Me.P20BSToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.P20BSToolStripMenuItem.Text = "P20 BS"
        '
        'P30BSToolStripMenuItem
        '
        Me.P30BSToolStripMenuItem.Name = "P30BSToolStripMenuItem"
        Me.P30BSToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.P30BSToolStripMenuItem.Text = "P30 BS"
        '
        'P35BSToolStripMenuItem
        '
        Me.P35BSToolStripMenuItem.Name = "P35BSToolStripMenuItem"
        Me.P35BSToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.P35BSToolStripMenuItem.Text = "P35 BS"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Pilot6ChToolStripMenuItem, Me.Pilot15ChToolStripMenuItem, Me.Pilot20ChToolStripMenuItem})
        Me.ToolStripMenuItem2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(185, 22)
        Me.ToolStripMenuItem2.Text = "PILOT Grades"
        '
        'Pilot6ChToolStripMenuItem
        '
        Me.Pilot6ChToolStripMenuItem.Name = "Pilot6ChToolStripMenuItem"
        Me.Pilot6ChToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.Pilot6ChToolStripMenuItem.Text = "Pilot 6Ch"
        '
        'Pilot15ChToolStripMenuItem
        '
        Me.Pilot15ChToolStripMenuItem.Name = "Pilot15ChToolStripMenuItem"
        Me.Pilot15ChToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.Pilot15ChToolStripMenuItem.Text = "Pilot 15Ch"
        '
        'Pilot20ChToolStripMenuItem
        '
        Me.Pilot20ChToolStripMenuItem.Name = "Pilot20ChToolStripMenuItem"
        Me.Pilot20ChToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.Pilot20ChToolStripMenuItem.Text = "Pilot 20Ch"
        '
        'WasteToolStripMenuItem
        '
        Me.WasteToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WasteToolStripMenuItem.Name = "WasteToolStripMenuItem"
        Me.WasteToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.WasteToolStripMenuItem.Text = "Waste"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(182, 6)
        '
        'ToolStripTextBox2
        '
        Me.ToolStripTextBox2.Font = New System.Drawing.Font("Segoe UI Black", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox2.MaxLength = 11
        Me.ToolStripTextBox2.Name = "ToolStripTextBox2"
        Me.ToolStripTextBox2.Size = New System.Drawing.Size(110, 28)
        Me.ToolStripTextBox2.Text = "CREATE CART FORM"
        '
        'ReCheckToolStripMenuItem
        '
        Me.ReCheckToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReCheckToolStripMenuItem.Name = "ReCheckToolStripMenuItem"
        Me.ReCheckToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ReCheckToolStripMenuItem.Text = "ReCheck"
        '
        'PackingHLProductsToolStripMenuItem
        '
        Me.PackingHLProductsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox11, Me.ToolStripMenuItem3, Me.ToolStripSeparator12, Me.ToolStripSeparator7, Me.ToolStripTextBox12, Me.ToolStripMenuItem5, Me.ToolStripSeparator13, Me.ToolStripSeparator9, Me.ToolStripTextBox13, Me.ToolStripMenuItem6, Me.ToolStripSeparator14, Me.ToolStripSeparator8, Me.ToolStripTextBox15, Me.PackGradedHLCartsToolStripMenuItem})
        Me.PackingHLProductsToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PackingHLProductsToolStripMenuItem.Name = "PackingHLProductsToolStripMenuItem"
        Me.PackingHLProductsToolStripMenuItem.Size = New System.Drawing.Size(129, 20)
        Me.PackingHLProductsToolStripMenuItem.Text = "Packing HL Products"
        Me.PackingHLProductsToolStripMenuItem.Visible = False
        '
        'ToolStripTextBox11
        '
        Me.ToolStripTextBox11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox11.Name = "ToolStripTextBox11"
        Me.ToolStripTextBox11.ReadOnly = True
        Me.ToolStripTextBox11.Size = New System.Drawing.Size(100, 22)
        Me.ToolStripTextBox11.Text = "Step 1"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(265, 26)
        Me.ToolStripMenuItem3.Text = "Seperate Cart to HL Carts"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(262, 6)
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(262, 6)
        '
        'ToolStripTextBox12
        '
        Me.ToolStripTextBox12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox12.Name = "ToolStripTextBox12"
        Me.ToolStripTextBox12.ReadOnly = True
        Me.ToolStripTextBox12.Size = New System.Drawing.Size(100, 22)
        Me.ToolStripTextBox12.Text = "Step 2"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateHCartToolStripMenuItem, Me.CreateLCartToolStripMenuItem})
        Me.ToolStripMenuItem5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(265, 26)
        Me.ToolStripMenuItem5.Text = "Create H && L Carts"
        '
        'CreateHCartToolStripMenuItem
        '
        Me.CreateHCartToolStripMenuItem.Name = "CreateHCartToolStripMenuItem"
        Me.CreateHCartToolStripMenuItem.Size = New System.Drawing.Size(180, 26)
        Me.CreateHCartToolStripMenuItem.Text = "Create H Cart"
        '
        'CreateLCartToolStripMenuItem
        '
        Me.CreateLCartToolStripMenuItem.Name = "CreateLCartToolStripMenuItem"
        Me.CreateLCartToolStripMenuItem.Size = New System.Drawing.Size(180, 26)
        Me.CreateLCartToolStripMenuItem.Text = "Create L Cart"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(262, 6)
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(262, 6)
        '
        'ToolStripTextBox13
        '
        Me.ToolStripTextBox13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox13.Name = "ToolStripTextBox13"
        Me.ToolStripTextBox13.ReadOnly = True
        Me.ToolStripTextBox13.Size = New System.Drawing.Size(100, 22)
        Me.ToolStripTextBox13.Text = "Step 3"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(265, 26)
        Me.ToolStripMenuItem6.Text = "Seperate Colour Grading"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(262, 6)
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(262, 6)
        '
        'ToolStripTextBox15
        '
        Me.ToolStripTextBox15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox15.Name = "ToolStripTextBox15"
        Me.ToolStripTextBox15.ReadOnly = True
        Me.ToolStripTextBox15.Size = New System.Drawing.Size(100, 22)
        Me.ToolStripTextBox15.Text = "Step 4"
        '
        'PackGradedHLCartsToolStripMenuItem
        '
        Me.PackGradedHLCartsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HCartsToolStripMenuItem, Me.LCartsToolStripMenuItem})
        Me.PackGradedHLCartsToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PackGradedHLCartsToolStripMenuItem.Name = "PackGradedHLCartsToolStripMenuItem"
        Me.PackGradedHLCartsToolStripMenuItem.Size = New System.Drawing.Size(265, 26)
        Me.PackGradedHLCartsToolStripMenuItem.Text = "Pack Graded H L Carts"
        '
        'HCartsToolStripMenuItem
        '
        Me.HCartsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox10, Me.HDDToolStripMenuItem3, Me.HDToolStripMenuItem3, Me.HMMToolStripMenuItem2, Me.HLToolStripMenuItem2, Me.HLLToolStripMenuItem2, Me.HBToolStripMenuItem2, Me.ToolStripSeparator19, Me.ToolStripSeparator20, Me.ToolStripTextBox18, Me.HSDToolStripMenuItem2, Me.HSMToolStripMenuItem2, Me.HSLToolStripMenuItem2, Me.HSBToolStripMenuItem2})
        Me.HCartsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.HCartsToolStripMenuItem.Name = "HCartsToolStripMenuItem"
        Me.HCartsToolStripMenuItem.Size = New System.Drawing.Size(134, 26)
        Me.HCartsToolStripMenuItem.Text = "H Carts"
        '
        'ToolStripTextBox10
        '
        Me.ToolStripTextBox10.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox10.Name = "ToolStripTextBox10"
        Me.ToolStripTextBox10.Size = New System.Drawing.Size(100, 29)
        Me.ToolStripTextBox10.Text = "Full Cheese"
        '
        'HDDToolStripMenuItem3
        '
        Me.HDDToolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.HDDToolStripMenuItem3.Name = "HDDToolStripMenuItem3"
        Me.HDDToolStripMenuItem3.Size = New System.Drawing.Size(160, 26)
        Me.HDDToolStripMenuItem3.Text = "H DD"
        '
        'HDToolStripMenuItem3
        '
        Me.HDToolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.HDToolStripMenuItem3.Name = "HDToolStripMenuItem3"
        Me.HDToolStripMenuItem3.Size = New System.Drawing.Size(160, 26)
        Me.HDToolStripMenuItem3.Text = "H D"
        '
        'HMMToolStripMenuItem2
        '
        Me.HMMToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.HMMToolStripMenuItem2.Name = "HMMToolStripMenuItem2"
        Me.HMMToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.HMMToolStripMenuItem2.Text = "H MM"
        '
        'HLToolStripMenuItem2
        '
        Me.HLToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.HLToolStripMenuItem2.Name = "HLToolStripMenuItem2"
        Me.HLToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.HLToolStripMenuItem2.Text = "H L"
        '
        'HLLToolStripMenuItem2
        '
        Me.HLLToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.HLLToolStripMenuItem2.Name = "HLLToolStripMenuItem2"
        Me.HLLToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.HLLToolStripMenuItem2.Text = "H LL"
        '
        'HBToolStripMenuItem2
        '
        Me.HBToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.HBToolStripMenuItem2.Name = "HBToolStripMenuItem2"
        Me.HBToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.HBToolStripMenuItem2.Text = "H B"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(157, 6)
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(157, 6)
        '
        'ToolStripTextBox18
        '
        Me.ToolStripTextBox18.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox18.Name = "ToolStripTextBox18"
        Me.ToolStripTextBox18.Size = New System.Drawing.Size(100, 29)
        Me.ToolStripTextBox18.Text = "Short Cheese"
        '
        'HSDToolStripMenuItem2
        '
        Me.HSDToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.HSDToolStripMenuItem2.Name = "HSDToolStripMenuItem2"
        Me.HSDToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.HSDToolStripMenuItem2.Text = "HS D"
        '
        'HSMToolStripMenuItem2
        '
        Me.HSMToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.HSMToolStripMenuItem2.Name = "HSMToolStripMenuItem2"
        Me.HSMToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.HSMToolStripMenuItem2.Text = "HS M"
        '
        'HSLToolStripMenuItem2
        '
        Me.HSLToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.HSLToolStripMenuItem2.Name = "HSLToolStripMenuItem2"
        Me.HSLToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.HSLToolStripMenuItem2.Text = "HS L"
        '
        'HSBToolStripMenuItem2
        '
        Me.HSBToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(221, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.HSBToolStripMenuItem2.Name = "HSBToolStripMenuItem2"
        Me.HSBToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.HSBToolStripMenuItem2.Text = "SH B"
        '
        'LCartsToolStripMenuItem
        '
        Me.LCartsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox19, Me.LDDToolStripMenuItem4, Me.LDToolStripMenuItem4, Me.LMMToolStripMenuItem3, Me.LLToolStripMenuItem3, Me.LLLToolStripMenuItem3, Me.LBToolStripMenuItem3, Me.ToolStripTextBox20, Me.LSDToolStripMenuItem2, Me.LSMToolStripMenuItem2, Me.LSLToolStripMenuItem2, Me.LSBToolStripMenuItem2})
        Me.LCartsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.LCartsToolStripMenuItem.Name = "LCartsToolStripMenuItem"
        Me.LCartsToolStripMenuItem.Size = New System.Drawing.Size(134, 26)
        Me.LCartsToolStripMenuItem.Text = "L Carts"
        '
        'ToolStripTextBox19
        '
        Me.ToolStripTextBox19.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox19.Name = "ToolStripTextBox19"
        Me.ToolStripTextBox19.Size = New System.Drawing.Size(100, 29)
        Me.ToolStripTextBox19.Text = "Full Cheese"
        '
        'LDDToolStripMenuItem4
        '
        Me.LDDToolStripMenuItem4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.LDDToolStripMenuItem4.Name = "LDDToolStripMenuItem4"
        Me.LDDToolStripMenuItem4.Size = New System.Drawing.Size(160, 26)
        Me.LDDToolStripMenuItem4.Text = "L DD"
        '
        'LDToolStripMenuItem4
        '
        Me.LDToolStripMenuItem4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.LDToolStripMenuItem4.Name = "LDToolStripMenuItem4"
        Me.LDToolStripMenuItem4.Size = New System.Drawing.Size(160, 26)
        Me.LDToolStripMenuItem4.Text = "L D"
        '
        'LMMToolStripMenuItem3
        '
        Me.LMMToolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.LMMToolStripMenuItem3.Name = "LMMToolStripMenuItem3"
        Me.LMMToolStripMenuItem3.Size = New System.Drawing.Size(160, 26)
        Me.LMMToolStripMenuItem3.Text = "L MM"
        '
        'LLToolStripMenuItem3
        '
        Me.LLToolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.LLToolStripMenuItem3.Name = "LLToolStripMenuItem3"
        Me.LLToolStripMenuItem3.Size = New System.Drawing.Size(160, 26)
        Me.LLToolStripMenuItem3.Text = "L L"
        '
        'LLLToolStripMenuItem3
        '
        Me.LLLToolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.LLLToolStripMenuItem3.Name = "LLLToolStripMenuItem3"
        Me.LLLToolStripMenuItem3.Size = New System.Drawing.Size(160, 26)
        Me.LLLToolStripMenuItem3.Text = "L LL"
        '
        'LBToolStripMenuItem3
        '
        Me.LBToolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(121, Byte), Integer), CType(CType(125, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.LBToolStripMenuItem3.Name = "LBToolStripMenuItem3"
        Me.LBToolStripMenuItem3.Size = New System.Drawing.Size(160, 26)
        Me.LBToolStripMenuItem3.Text = "L B"
        '
        'ToolStripTextBox20
        '
        Me.ToolStripTextBox20.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox20.Name = "ToolStripTextBox20"
        Me.ToolStripTextBox20.Size = New System.Drawing.Size(100, 29)
        Me.ToolStripTextBox20.Text = "Short Cheese"
        '
        'LSDToolStripMenuItem2
        '
        Me.LSDToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.LSDToolStripMenuItem2.Name = "LSDToolStripMenuItem2"
        Me.LSDToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.LSDToolStripMenuItem2.Text = "SL D"
        '
        'LSMToolStripMenuItem2
        '
        Me.LSMToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.LSMToolStripMenuItem2.Name = "LSMToolStripMenuItem2"
        Me.LSMToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.LSMToolStripMenuItem2.Text = "SL M"
        '
        'LSLToolStripMenuItem2
        '
        Me.LSLToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.LSLToolStripMenuItem2.Name = "LSLToolStripMenuItem2"
        Me.LSLToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.LSLToolStripMenuItem2.Text = "SL L"
        '
        'LSBToolStripMenuItem2
        '
        Me.LSBToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.LSBToolStripMenuItem2.Name = "LSBToolStripMenuItem2"
        Me.LSBToolStripMenuItem2.Size = New System.Drawing.Size(160, 26)
        Me.LSBToolStripMenuItem2.Text = "SL B"
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
        'PrintFormsToolStripMenuItem
        '
        Me.PrintFormsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripTextBox4, Me.ToolStripSeparator6, Me.ToolStripMenuItem1, Me.ToolStripSeparator2, Me.ToolStripSeparator1, Me.ToolStripTextBox1, Me.ToolStripSeparator4, Me.CompareSTDToolStripMenuItem, Me.ToolStripMenuItem4, Me.ToolStripTextBox5, Me.ToolStripSeparator5, Me.ReCheckToolStripMenuItem1})
        Me.PrintFormsToolStripMenuItem.Name = "PrintFormsToolStripMenuItem"
        Me.PrintFormsToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.PrintFormsToolStripMenuItem.Text = "Sort Grades"
        Me.PrintFormsToolStripMenuItem.Visible = False
        '
        'ToolStripTextBox4
        '
        Me.ToolStripTextBox4.Font = New System.Drawing.Font("Segoe UI Black", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox4.Name = "ToolStripTextBox4"
        Me.ToolStripTextBox4.ReadOnly = True
        Me.ToolStripTextBox4.Size = New System.Drawing.Size(100, 28)
        Me.ToolStripTextBox4.Text = "Cart Forms"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(267, 6)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(270, 22)
        Me.ToolStripMenuItem1.Text = "Normal"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(267, 6)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(267, 6)
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI Black", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.ReadOnly = True
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(210, 28)
        Me.ToolStripTextBox1.Text = "CREATE STANDARD FORMS"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(267, 6)
        '
        'CompareSTDToolStripMenuItem
        '
        Me.CompareSTDToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Round1ToolStripMenuItem, Me.Round2ToolStripMenuItem, Me.Round3ToolStripMenuItem, Me.StdSheetToolStripMenuItem})
        Me.CompareSTDToolStripMenuItem.Name = "CompareSTDToolStripMenuItem"
        Me.CompareSTDToolStripMenuItem.Size = New System.Drawing.Size(270, 22)
        Me.CompareSTDToolStripMenuItem.Text = "Create STD Compare Sheets "
        '
        'Round1ToolStripMenuItem
        '
        Me.Round1ToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Round1ToolStripMenuItem.Name = "Round1ToolStripMenuItem"
        Me.Round1ToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.Round1ToolStripMenuItem.Text = "Round1"
        '
        'Round2ToolStripMenuItem
        '
        Me.Round2ToolStripMenuItem.Name = "Round2ToolStripMenuItem"
        Me.Round2ToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.Round2ToolStripMenuItem.Text = "Round2"
        '
        'Round3ToolStripMenuItem
        '
        Me.Round3ToolStripMenuItem.Name = "Round3ToolStripMenuItem"
        Me.Round3ToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.Round3ToolStripMenuItem.Text = "Round3"
        '
        'StdSheetToolStripMenuItem
        '
        Me.StdSheetToolStripMenuItem.Name = "StdSheetToolStripMenuItem"
        Me.StdSheetToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.StdSheetToolStripMenuItem.Text = "STD"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Round1ToolStripMenuItem1, Me.Round2ToolStripMenuItem1, Me.Round3ToolStripMenuItem1, Me.HLStdToolStripMenuItem})
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(270, 22)
        Me.ToolStripMenuItem4.Text = "Create HL STD Compare Sheets "
        '
        'Round1ToolStripMenuItem1
        '
        Me.Round1ToolStripMenuItem1.Name = "Round1ToolStripMenuItem1"
        Me.Round1ToolStripMenuItem1.Size = New System.Drawing.Size(130, 22)
        Me.Round1ToolStripMenuItem1.Text = "HLRound1"
        '
        'Round2ToolStripMenuItem1
        '
        Me.Round2ToolStripMenuItem1.Name = "Round2ToolStripMenuItem1"
        Me.Round2ToolStripMenuItem1.Size = New System.Drawing.Size(130, 22)
        Me.Round2ToolStripMenuItem1.Text = "HLRound2"
        '
        'Round3ToolStripMenuItem1
        '
        Me.Round3ToolStripMenuItem1.Name = "Round3ToolStripMenuItem1"
        Me.Round3ToolStripMenuItem1.Size = New System.Drawing.Size(130, 22)
        Me.Round3ToolStripMenuItem1.Text = "HLRound3"
        '
        'HLStdToolStripMenuItem
        '
        Me.HLStdToolStripMenuItem.Name = "HLStdToolStripMenuItem"
        Me.HLStdToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.HLStdToolStripMenuItem.Text = "HL STD"
        '
        'ToolStripTextBox5
        '
        Me.ToolStripTextBox5.Font = New System.Drawing.Font("Segoe UI Black", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripTextBox5.Name = "ToolStripTextBox5"
        Me.ToolStripTextBox5.Size = New System.Drawing.Size(208, 28)
        Me.ToolStripTextBox5.Text = "Create ReCheck Form"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(267, 6)
        '
        'ReCheckToolStripMenuItem1
        '
        Me.ReCheckToolStripMenuItem1.Name = "ReCheckToolStripMenuItem1"
        Me.ReCheckToolStripMenuItem1.Size = New System.Drawing.Size(270, 22)
        Me.ReCheckToolStripMenuItem1.Text = "ReCheck"
        Me.ReCheckToolStripMenuItem1.Visible = False
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(126, 20)
        Me.PrintToolStripMenuItem.Text = "View Packing Sheets"
        Me.PrintToolStripMenuItem.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.lblMessage, Me.StatusPanel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 448)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(821, 22)
        Me.StatusStrip1.TabIndex = 18
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
        Me.StatusPanel.Size = New System.Drawing.Size(81, 17)
        Me.StatusPanel.Text = "PMP Software"
        '
        'lblGrade
        '
        Me.lblGrade.AutoSize = True
        Me.lblGrade.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrade.Location = New System.Drawing.Point(15, 35)
        Me.lblGrade.Name = "lblGrade"
        Me.lblGrade.Size = New System.Drawing.Size(216, 31)
        Me.lblGrade.TabIndex = 1
        Me.lblGrade.Text = "Selected Grade"
        '
        'txtGrade
        '
        Me.txtGrade.Enabled = False
        Me.txtGrade.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtGrade.Location = New System.Drawing.Point(281, 29)
        Me.txtGrade.Name = "txtGrade"
        Me.txtGrade.Size = New System.Drawing.Size(329, 44)
        Me.txtGrade.TabIndex = 2
        Me.txtGrade.Text = " "
        '
        'lblSelectGrade
        '
        Me.lblSelectGrade.AutoSize = True
        Me.lblSelectGrade.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectGrade.ForeColor = System.Drawing.Color.Red
        Me.lblSelectGrade.Location = New System.Drawing.Point(612, 27)
        Me.lblSelectGrade.Name = "lblSelectGrade"
        Me.lblSelectGrade.Size = New System.Drawing.Size(157, 50)
        Me.lblSelectGrade.TabIndex = 3
        Me.lblSelectGrade.Text = "Please Select" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " Grade First"
        Me.lblSelectGrade.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(233, 254)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(363, 33)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Enter Total Piolt Cheese "
        Me.Label2.Visible = False
        '
        'txtPilotCount
        '
        Me.txtPilotCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPilotCount.Location = New System.Drawing.Point(592, 249)
        Me.txtPilotCount.MaxLength = 2
        Me.txtPilotCount.Name = "txtPilotCount"
        Me.txtPilotCount.Size = New System.Drawing.Size(44, 44)
        Me.txtPilotCount.TabIndex = 9
        Me.txtPilotCount.Visible = False
        '
        'btnHLReport
        '
        Me.btnHLReport.Location = New System.Drawing.Point(21, 263)
        Me.btnHLReport.Name = "btnHLReport"
        Me.btnHLReport.Size = New System.Drawing.Size(113, 47)
        Me.btnHLReport.TabIndex = 11
        Me.btnHLReport.Text = "Create HL Seperation"
        Me.btnHLReport.UseVisualStyleBackColor = True
        '
        'frmJobEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(821, 470)
        Me.Controls.Add(Me.btnHLReport)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
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
    Friend WithEvents PrintFormsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CompareSTDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReCheckToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Round1ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Round2ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Round3ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StdSheetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents Pilot6ChToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Pilot15ChToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Pilot20ChToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripTextBox2 As ToolStripTextBox
    Friend WithEvents ToolStripTextBox3 As ToolStripTextBox
    Friend WithEvents ToolStripTextBox4 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox5 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents GradeAReCheckAToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SpecialToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents Round1ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Round2ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents Round3ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents HLStdToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PackingHLProductsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox11 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator12 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox12 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator13 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox13 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator14 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox15 As ToolStripTextBox
    Friend WithEvents ToolStripMenuItem5 As ToolStripMenuItem
    Friend WithEvents CreateHCartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateLCartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PackGradedHLCartsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HCartsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripTextBox10 As ToolStripTextBox
    Friend WithEvents HDDToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents HDToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents HMMToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents HLToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents HLLToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents HBToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator19 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator20 As ToolStripSeparator
    Friend WithEvents ToolStripTextBox18 As ToolStripTextBox
    Friend WithEvents HSDToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents HSMToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents HSLToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents HSBToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents LCartsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripTextBox19 As ToolStripTextBox
    Friend WithEvents LDDToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents LDToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents LMMToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents LLToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents LLLToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents LBToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents ToolStripTextBox20 As ToolStripTextBox
    Friend WithEvents LSDToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents LSMToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents LSLToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents LSBToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As ToolStripMenuItem
    Friend WithEvents btnHLReport As Button
End Class
