<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmB_AL_AD_W
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtConeBcode = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnDefect = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lbltotCount = New System.Windows.Forms.Label()
        Me.lbltotScan = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CheeseAloc1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.CausesValidation = False
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CheeseAloc1, Me.CheeseNum1, Me.CheeseAloc2, Me.CheeseNum2, Me.CheeseAloc3, Me.CheeseNum3, Me.CheeseAloc4, Me.CheeseNum4, Me.CheeseAloc5, Me.CheeseNum5})
        Me.DataGridView1.Enabled = False
        Me.DataGridView1.Location = New System.Drawing.Point(1, 6)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(792, 843)
        Me.DataGridView1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(801, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "GRADE - "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(907, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 31)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "B"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(799, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 24)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Prod :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(799, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 24)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Prod # :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(899, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(174, 24)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "PRODUCT NAME"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(899, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(174, 24)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "PRODUCT NAME"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(875, 230)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 24)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Cheese #"
        '
        'txtConeBcode
        '
        Me.txtConeBcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtConeBcode.Location = New System.Drawing.Point(844, 268)
        Me.txtConeBcode.Name = "txtConeBcode"
        Me.txtConeBcode.Size = New System.Drawing.Size(171, 29)
        Me.txtConeBcode.TabIndex = 144
        Me.txtConeBcode.Tag = "1"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Red
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(221, 310)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 37)
        Me.Label8.TabIndex = 178
        Me.Label8.Text = "Label8"
        Me.Label8.Visible = False
        '
        'btnDefect
        '
        Me.btnDefect.BackColor = System.Drawing.Color.Yellow
        Me.btnDefect.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDefect.Location = New System.Drawing.Point(816, 436)
        Me.btnDefect.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDefect.Name = "btnDefect"
        Me.btnDefect.Size = New System.Drawing.Size(212, 53)
        Me.btnDefect.TabIndex = 177
        Me.btnDefect.Text = "Enter Defect"
        Me.btnDefect.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(816, 355)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(212, 53)
        Me.btnCancel.TabIndex = 176
        Me.btnCancel.Text = "Cancel/Clear"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFinish.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinish.Location = New System.Drawing.Point(816, 521)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(212, 53)
        Me.btnFinish.TabIndex = 175
        Me.btnFinish.Text = "Finish"
        Me.btnFinish.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(801, 152)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(152, 24)
        Me.Label15.TabIndex = 185
        Me.Label15.Text = "Total In System"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(805, 183)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(145, 24)
        Me.Label16.TabIndex = 186
        Me.Label16.Text = "Total Scanned"
        '
        'lbltotCount
        '
        Me.lbltotCount.AutoSize = True
        Me.lbltotCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotCount.Location = New System.Drawing.Point(949, 145)
        Me.lbltotCount.Name = "lbltotCount"
        Me.lbltotCount.Size = New System.Drawing.Size(110, 31)
        Me.lbltotCount.TabIndex = 187
        Me.lbltotCount.Text = "123456"
        '
        'lbltotScan
        '
        Me.lbltotScan.AutoSize = True
        Me.lbltotScan.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotScan.ForeColor = System.Drawing.Color.Red
        Me.lbltotScan.Location = New System.Drawing.Point(951, 176)
        Me.lbltotScan.Name = "lbltotScan"
        Me.lbltotScan.Size = New System.Drawing.Size(94, 31)
        Me.lbltotScan.TabIndex = 188
        Me.lbltotScan.Text = "45678"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(866, 686)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 189
        Me.Label9.Text = "Label9"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(866, 736)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 13)
        Me.Label10.TabIndex = 190
        Me.Label10.Text = "Label10"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(866, 770)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 191
        Me.Label11.Text = "Label11"
        '
        'CheeseAloc1
        '
        Me.CheeseAloc1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheeseAloc1.DefaultCellStyle = DataGridViewCellStyle1
        Me.CheeseAloc1.Frozen = True
        Me.CheeseAloc1.HeaderText = "Number"
        Me.CheeseAloc1.Name = "CheeseAloc1"
        Me.CheeseAloc1.ReadOnly = True
        Me.CheeseAloc1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc1.Width = 5
        '
        'CheeseNum1
        '
        Me.CheeseNum1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
        Me.CheeseNum1.Frozen = True
        Me.CheeseNum1.HeaderText = "Cheese"
        Me.CheeseNum1.Name = "CheeseNum1"
        Me.CheeseNum1.ReadOnly = True
        Me.CheeseNum1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseNum1.Width = 5
        '
        'CheeseAloc2
        '
        Me.CheeseAloc2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseAloc2.Frozen = True
        Me.CheeseAloc2.HeaderText = "Number"
        Me.CheeseAloc2.Name = "CheeseAloc2"
        Me.CheeseAloc2.ReadOnly = True
        Me.CheeseAloc2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc2.Width = 50
        '
        'CheeseNum2
        '
        Me.CheeseNum2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseNum2.Frozen = True
        Me.CheeseNum2.HeaderText = "Cheese"
        Me.CheeseNum2.Name = "CheeseNum2"
        Me.CheeseNum2.ReadOnly = True
        Me.CheeseNum2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CheeseAloc3
        '
        Me.CheeseAloc3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseAloc3.Frozen = True
        Me.CheeseAloc3.HeaderText = "Number"
        Me.CheeseAloc3.Name = "CheeseAloc3"
        Me.CheeseAloc3.ReadOnly = True
        Me.CheeseAloc3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc3.Width = 50
        '
        'CheeseNum3
        '
        Me.CheeseNum3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseNum3.Frozen = True
        Me.CheeseNum3.HeaderText = "Cheese"
        Me.CheeseNum3.Name = "CheeseNum3"
        Me.CheeseNum3.ReadOnly = True
        Me.CheeseNum3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CheeseAloc4
        '
        Me.CheeseAloc4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseAloc4.Frozen = True
        Me.CheeseAloc4.HeaderText = "Number"
        Me.CheeseAloc4.Name = "CheeseAloc4"
        Me.CheeseAloc4.ReadOnly = True
        Me.CheeseAloc4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc4.Width = 50
        '
        'CheeseNum4
        '
        Me.CheeseNum4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseNum4.Frozen = True
        Me.CheeseNum4.HeaderText = "Cheese"
        Me.CheeseNum4.Name = "CheeseNum4"
        Me.CheeseNum4.ReadOnly = True
        Me.CheeseNum4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CheeseAloc5
        '
        Me.CheeseAloc5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseAloc5.Frozen = True
        Me.CheeseAloc5.HeaderText = "Number"
        Me.CheeseAloc5.Name = "CheeseAloc5"
        Me.CheeseAloc5.ReadOnly = True
        Me.CheeseAloc5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc5.Width = 50
        '
        'CheeseNum5
        '
        Me.CheeseNum5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseNum5.Frozen = True
        Me.CheeseNum5.HeaderText = "Cheese"
        Me.CheeseNum5.Name = "CheeseNum5"
        Me.CheeseNum5.ReadOnly = True
        Me.CheeseNum5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'frmB_AL_AD_W
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(1086, 749)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lbltotScan)
        Me.Controls.Add(Me.lbltotCount)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.btnDefect)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnFinish)
        Me.Controls.Add(Me.txtConeBcode)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmB_AL_AD_W"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PACKING for B, AL, AD and Waste"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtConeBcode As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents btnDefect As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnFinish As Button
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents lbltotCount As Label
    Friend WithEvents lbltotScan As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents CheeseAloc1 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum1 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc2 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum2 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc3 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum3 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc4 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum4 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc5 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum5 As DataGridViewTextBoxColumn
End Class
