
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class liveGraph

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
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.colLive = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.btnUpdateGraph = New System.Windows.Forms.Button()
        Me.DGVReportJobs = New System.Windows.Forms.DataGridView()
        Me.DGVReportInput = New System.Windows.Forms.DataGridView()
        Me.DGVReportOutput = New System.Windows.Forms.DataGridView()
        Me.PRNUM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRODNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Full = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.re_check = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.short_Cone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblEffVal = New System.Windows.Forms.Label()
        Me.lblTotSort = New System.Windows.Forms.Label()
        Me.lblTotRcd = New System.Windows.Forms.Label()
        Me.lblTotChecked = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        CType(Me.colLive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVReportJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVReportInput, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVReportOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'colLive
        '
        Me.colLive.BackColor = System.Drawing.Color.DarkGray
        Me.colLive.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
        ChartArea2.Name = "ChartArea1"
        Me.colLive.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.colLive.Legends.Add(Legend2)
        Me.colLive.Location = New System.Drawing.Point(12, 32)
        Me.colLive.Name = "colLive"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Me.colLive.Series.Add(Series2)
        Me.colLive.Size = New System.Drawing.Size(1157, 504)
        Me.colLive.TabIndex = 0
        Me.colLive.Text = " "
        '
        'btnUpdateGraph
        '
        Me.btnUpdateGraph.Location = New System.Drawing.Point(427, 3)
        Me.btnUpdateGraph.Name = "btnUpdateGraph"
        Me.btnUpdateGraph.Size = New System.Drawing.Size(122, 23)
        Me.btnUpdateGraph.TabIndex = 5
        Me.btnUpdateGraph.Text = "Update Graph"
        Me.btnUpdateGraph.UseVisualStyleBackColor = True
        '
        'DGVReportJobs
        '
        Me.DGVReportJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVReportJobs.Location = New System.Drawing.Point(1093, 105)
        Me.DGVReportJobs.Name = "DGVReportJobs"
        Me.DGVReportJobs.Size = New System.Drawing.Size(240, 150)
        Me.DGVReportJobs.TabIndex = 6
        Me.DGVReportJobs.Visible = False
        '
        'DGVReportInput
        '
        Me.DGVReportInput.AllowUserToAddRows = False
        Me.DGVReportInput.AllowUserToDeleteRows = False
        Me.DGVReportInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVReportInput.Location = New System.Drawing.Point(1158, 218)
        Me.DGVReportInput.Name = "DGVReportInput"
        Me.DGVReportInput.ReadOnly = True
        Me.DGVReportInput.Size = New System.Drawing.Size(240, 150)
        Me.DGVReportInput.TabIndex = 7
        Me.DGVReportInput.Visible = False
        '
        'DGVReportOutput
        '
        Me.DGVReportOutput.AllowUserToAddRows = False
        Me.DGVReportOutput.AllowUserToDeleteRows = False
        Me.DGVReportOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVReportOutput.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PRNUM, Me.PRODNAME, Me.Full, Me.re_check, Me.short_Cone})
        Me.DGVReportOutput.Enabled = False
        Me.DGVReportOutput.Location = New System.Drawing.Point(841, 374)
        Me.DGVReportOutput.Name = "DGVReportOutput"
        Me.DGVReportOutput.ReadOnly = True
        Me.DGVReportOutput.RowHeadersVisible = False
        Me.DGVReportOutput.Size = New System.Drawing.Size(505, 150)
        Me.DGVReportOutput.TabIndex = 8
        Me.DGVReportOutput.Visible = False
        '
        'PRNUM
        '
        Me.PRNUM.HeaderText = "PRNUM"
        Me.PRNUM.Name = "PRNUM"
        Me.PRNUM.ReadOnly = True
        '
        'PRODNAME
        '
        Me.PRODNAME.HeaderText = "PRODNAME"
        Me.PRODNAME.Name = "PRODNAME"
        Me.PRODNAME.ReadOnly = True
        '
        'Full
        '
        Me.Full.HeaderText = "Full"
        Me.Full.Name = "Full"
        Me.Full.ReadOnly = True
        '
        're_check
        '
        Me.re_check.HeaderText = "ReCheck"
        Me.re_check.Name = "re_check"
        Me.re_check.ReadOnly = True
        '
        'short_Cone
        '
        Me.short_Cone.HeaderText = "Short"
        Me.short_Cone.Name = "short_Cone"
        Me.short_Cone.ReadOnly = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(843, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(122, 23)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Location = New System.Drawing.Point(1004, 374)
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.ShowWeekNumbers = True
        Me.MonthCalendar1.TabIndex = 10
        Me.MonthCalendar1.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(79, 548)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Start Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(366, 548)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "End Date"
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(151, 548)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(0, 13)
        Me.lblStartDate.TabIndex = 13
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Location = New System.Drawing.Point(448, 548)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(0, 13)
        Me.lblEndDate.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(794, 548)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Productivity"
        '
        'lblEffVal
        '
        Me.lblEffVal.AutoSize = True
        Me.lblEffVal.Location = New System.Drawing.Point(882, 548)
        Me.lblEffVal.Name = "lblEffVal"
        Me.lblEffVal.Size = New System.Drawing.Size(0, 13)
        Me.lblEffVal.TabIndex = 16
        '
        'lblTotSort
        '
        Me.lblTotSort.AutoSize = True
        Me.lblTotSort.Location = New System.Drawing.Point(638, 548)
        Me.lblTotSort.Name = "lblTotSort"
        Me.lblTotSort.Size = New System.Drawing.Size(0, 13)
        Me.lblTotSort.TabIndex = 18
        '
        'lblTotRcd
        '
        Me.lblTotRcd.AutoSize = True
        Me.lblTotRcd.Location = New System.Drawing.Point(561, 548)
        Me.lblTotRcd.Name = "lblTotRcd"
        Me.lblTotRcd.Size = New System.Drawing.Size(78, 13)
        Me.lblTotRcd.TabIndex = 17
        Me.lblTotRcd.Text = "DTY Received"
        '
        'lblTotChecked
        '
        Me.lblTotChecked.AutoSize = True
        Me.lblTotChecked.Location = New System.Drawing.Point(636, 566)
        Me.lblTotChecked.Name = "lblTotChecked"
        Me.lblTotChecked.Size = New System.Drawing.Size(0, 13)
        Me.lblTotChecked.TabIndex = 20
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(561, 566)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(75, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "DTY Checked"
        '
        'liveGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1247, 587)
        Me.Controls.Add(Me.lblTotChecked)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblTotSort)
        Me.Controls.Add(Me.lblTotRcd)
        Me.Controls.Add(Me.lblEffVal)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblEndDate)
        Me.Controls.Add(Me.lblStartDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MonthCalendar1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.DGVReportOutput)
        Me.Controls.Add(Me.DGVReportInput)
        Me.Controls.Add(Me.DGVReportJobs)
        Me.Controls.Add(Me.btnUpdateGraph)
        Me.Controls.Add(Me.colLive)
        Me.Name = "liveGraph"
        Me.Text = "liveGraph"
        CType(Me.colLive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVReportJobs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVReportInput, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVReportOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label

    Friend WithEvents Label4 As Label

    Friend WithEvents btnUpdateGraph As Button

    Friend WithEvents DGVReportJobs As DataGridView

    Friend WithEvents DGVReportInput As DataGridView

    Friend WithEvents DGVReportOutput As DataGridView

    Friend WithEvents PRNUM As DataGridViewTextBoxColumn

    Friend WithEvents PRODNAME As DataGridViewTextBoxColumn

    Friend WithEvents Full As DataGridViewTextBoxColumn

    Friend WithEvents re_check As DataGridViewTextBoxColumn

    Friend WithEvents short_Cone As DataGridViewTextBoxColumn

    Friend WithEvents Button2 As Button

    Protected Friend WithEvents colLive As DataVisualization.Charting.Chart

    Friend WithEvents MonthCalendar1 As MonthCalendar

    Friend WithEvents Label1 As Label

    Friend WithEvents Label2 As Label

    Friend WithEvents lblStartDate As Label

    Friend WithEvents lblEndDate As Label

    Friend WithEvents Label5 As Label

    Friend WithEvents lblEffVal As Label

    Friend WithEvents lblTotSort As Label

    Friend WithEvents lblTotRcd As Label

    Friend WithEvents lblTotChecked As Label

    Friend WithEvents Label9 As Label

End Class