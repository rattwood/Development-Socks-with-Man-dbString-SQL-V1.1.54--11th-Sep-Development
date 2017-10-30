<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmB_AL_AD_W
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CheeseNum1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1.CausesValidation = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CheeseNum1, Me.CheeseAloc1, Me.CheeseNum2, Me.CheeseAloc2, Me.CheeseNum3, Me.CheeseAloc3})
        Me.DataGridView1.Enabled = False
        Me.DataGridView1.Location = New System.Drawing.Point(16, 106)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(496, 698)
        Me.DataGridView1.TabIndex = 0
        '
        'CheeseNum1
        '
        Me.CheeseNum1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseNum1.Frozen = True
        Me.CheeseNum1.HeaderText = "Cheese"
        Me.CheeseNum1.Name = "CheeseNum1"
        Me.CheeseNum1.ReadOnly = True
        Me.CheeseNum1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CheeseAloc1
        '
        Me.CheeseAloc1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseAloc1.Frozen = True
        Me.CheeseAloc1.HeaderText = "Number"
        Me.CheeseAloc1.Name = "CheeseAloc1"
        Me.CheeseAloc1.ReadOnly = True
        Me.CheeseAloc1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc1.Width = 50
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
        'CheeseAloc2
        '
        Me.CheeseAloc2.Frozen = True
        Me.CheeseAloc2.HeaderText = "Number"
        Me.CheeseAloc2.Name = "CheeseAloc2"
        Me.CheeseAloc2.ReadOnly = True
        Me.CheeseAloc2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc2.Width = 50
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
        'CheeseAloc3
        '
        Me.CheeseAloc3.Frozen = True
        Me.CheeseAloc3.HeaderText = "Number"
        Me.CheeseAloc3.Name = "CheeseAloc3"
        Me.CheeseAloc3.ReadOnly = True
        Me.CheeseAloc3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc3.Width = 50
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(369, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "GRADE - "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(458, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 31)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "B"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(174, 24)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "PRODUCT NAME"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(523, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(125, 24)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "PRODUCT #"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(212, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(174, 24)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "PRODUCT NAME"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(654, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(174, 24)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "PRODUCT NAME"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(23, 841)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(124, 29)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Cheese #"
        '
        'txtConeBcode
        '
        Me.txtConeBcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtConeBcode.Location = New System.Drawing.Point(170, 817)
        Me.txtConeBcode.Name = "txtConeBcode"
        Me.txtConeBcode.Size = New System.Drawing.Size(433, 62)
        Me.txtConeBcode.TabIndex = 144
        Me.txtConeBcode.Tag = "1"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Red
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(680, 261)
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
        Me.btnDefect.Location = New System.Drawing.Point(1011, 915)
        Me.btnDefect.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDefect.Name = "btnDefect"
        Me.btnDefect.Size = New System.Drawing.Size(138, 80)
        Me.btnDefect.TabIndex = 177
        Me.btnDefect.Text = "Enter Defect"
        Me.btnDefect.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(16, 914)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(197, 80)
        Me.btnCancel.TabIndex = 176
        Me.btnCancel.Text = "Cancel and Clear"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFinish.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinish.Location = New System.Drawing.Point(392, 914)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(372, 80)
        Me.btnFinish.TabIndex = 175
        Me.btnFinish.Text = "Finish"
        Me.btnFinish.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(611, 146)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(175, 25)
        Me.Label15.TabIndex = 185
        Me.Label15.Text = "Total In System"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(611, 184)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(164, 25)
        Me.Label16.TabIndex = 186
        Me.Label16.Text = "Total Scanned"
        '
        'lbltotCount
        '
        Me.lbltotCount.AutoSize = True
        Me.lbltotCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotCount.Location = New System.Drawing.Point(828, 146)
        Me.lbltotCount.Name = "lbltotCount"
        Me.lbltotCount.Size = New System.Drawing.Size(0, 25)
        Me.lbltotCount.TabIndex = 187
        '
        'lbltotScan
        '
        Me.lbltotScan.AutoSize = True
        Me.lbltotScan.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotScan.Location = New System.Drawing.Point(828, 184)
        Me.lbltotScan.Name = "lbltotScan"
        Me.lbltotScan.Size = New System.Drawing.Size(0, 25)
        Me.lbltotScan.TabIndex = 188
        '
        'frmB_AL_AD_W
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1664, 1011)
        Me.Controls.Add(Me.lbltotScan)
        Me.Controls.Add(Me.lbltotCount)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label8)
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
        Me.Name = "frmB_AL_AD_W"
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
    Friend WithEvents CheeseNum1 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc1 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum2 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc2 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum3 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc3 As DataGridViewTextBoxColumn
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents lbltotCount As Label
    Friend WithEvents lbltotScan As Label
End Class
