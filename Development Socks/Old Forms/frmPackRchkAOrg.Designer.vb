﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPackRchkAOrg
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
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lbltotScan = New System.Windows.Forms.Label()
        Me.lbltotCount = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnDefect = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.txtConeBcode = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheeseNum5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseNum1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CheeseAloc1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Red
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(24, 323)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 37)
        Me.Label8.TabIndex = 204
        Me.Label8.Text = "Label8"
        Me.Label8.Visible = False
        '
        'lbltotScan
        '
        Me.lbltotScan.AutoSize = True
        Me.lbltotScan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotScan.Location = New System.Drawing.Point(955, 181)
        Me.lbltotScan.Name = "lbltotScan"
        Me.lbltotScan.Size = New System.Drawing.Size(0, 16)
        Me.lbltotScan.TabIndex = 208
        '
        'lbltotCount
        '
        Me.lbltotCount.AutoSize = True
        Me.lbltotCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotCount.Location = New System.Drawing.Point(955, 154)
        Me.lbltotCount.Name = "lbltotCount"
        Me.lbltotCount.Size = New System.Drawing.Size(0, 16)
        Me.lbltotCount.TabIndex = 207
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(822, 181)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(109, 16)
        Me.Label16.TabIndex = 206
        Me.Label16.Text = "Total Scanned"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(822, 154)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(115, 16)
        Me.Label15.TabIndex = 205
        Me.Label15.Text = "Total In System"
        '
        'btnDefect
        '
        Me.btnDefect.BackColor = System.Drawing.Color.Yellow
        Me.btnDefect.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDefect.Location = New System.Drawing.Point(839, 456)
        Me.btnDefect.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDefect.Name = "btnDefect"
        Me.btnDefect.Size = New System.Drawing.Size(212, 53)
        Me.btnDefect.TabIndex = 203
        Me.btnDefect.Text = "Enter Defect"
        Me.btnDefect.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(839, 375)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(212, 53)
        Me.btnCancel.TabIndex = 202
        Me.btnCancel.Text = "Cancel/Clear"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnFinish
        '
        Me.btnFinish.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnFinish.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinish.Location = New System.Drawing.Point(839, 541)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(212, 53)
        Me.btnFinish.TabIndex = 201
        Me.btnFinish.Text = "Finish"
        Me.btnFinish.UseVisualStyleBackColor = False
        '
        'txtConeBcode
        '
        Me.txtConeBcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtConeBcode.Location = New System.Drawing.Point(867, 288)
        Me.txtConeBcode.Name = "txtConeBcode"
        Me.txtConeBcode.Size = New System.Drawing.Size(171, 29)
        Me.txtConeBcode.TabIndex = 200
        Me.txtConeBcode.Tag = "1"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(898, 250)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 24)
        Me.Label7.TabIndex = 199
        Me.Label7.Text = "Cheese #"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(889, 126)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 16)
        Me.Label6.TabIndex = 198
        Me.Label6.Text = "PRODUCT NAME"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(889, 100)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(129, 16)
        Me.Label5.TabIndex = 197
        Me.Label5.Text = "PRODUCT NAME"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(822, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 16)
        Me.Label4.TabIndex = 196
        Me.Label4.Text = "Prod # :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(930, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 31)
        Me.Label2.TabIndex = 194
        Me.Label2.Text = "B"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(824, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 24)
        Me.Label1.TabIndex = 193
        Me.Label1.Text = "GRADE - "
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
        'CheeseNum4
        '
        Me.CheeseNum4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseNum4.Frozen = True
        Me.CheeseNum4.HeaderText = "Cheese"
        Me.CheeseNum4.Name = "CheeseNum4"
        Me.CheeseNum4.ReadOnly = True
        Me.CheeseNum4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
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
        Me.CheeseAloc3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseAloc3.Frozen = True
        Me.CheeseAloc3.HeaderText = "Number"
        Me.CheeseAloc3.Name = "CheeseAloc3"
        Me.CheeseAloc3.ReadOnly = True
        Me.CheeseAloc3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc3.Width = 50
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
        Me.CheeseAloc2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.CheeseAloc2.Frozen = True
        Me.CheeseAloc2.HeaderText = "Number"
        Me.CheeseAloc2.Name = "CheeseAloc2"
        Me.CheeseAloc2.ReadOnly = True
        Me.CheeseAloc2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CheeseAloc2.Width = 50
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(822, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 16)
        Me.Label3.TabIndex = 195
        Me.Label3.Text = "Prod :"
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
        Me.DataGridView1.Location = New System.Drawing.Point(12, 6)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(790, 900)
        Me.DataGridView1.TabIndex = 192
        '
        'frmPackRchkA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1086, 861)
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
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmPackRchkA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ReCheck A Packing"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label8 As Label
    Friend WithEvents lbltotScan As Label
    Friend WithEvents lbltotCount As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents btnDefect As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnFinish As Button
    Friend WithEvents txtConeBcode As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents CheeseNum5 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc5 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum4 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc4 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum3 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc3 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum2 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc2 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseNum1 As DataGridViewTextBoxColumn
    Friend WithEvents CheeseAloc1 As DataGridViewTextBoxColumn
    Friend WithEvents Label3 As Label
    Friend WithEvents DataGridView1 As DataGridView
End Class
