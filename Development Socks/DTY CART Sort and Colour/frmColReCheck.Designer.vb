<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmColReCheck
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.rechkinx = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.bocdecone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rechk1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rechk2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rechkGrade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.rechkdef = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.btnFinish = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.rechkinx, Me.bocdecone, Me.rechk1, Me.rechk2, Me.rechkGrade, Me.rechkdef})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(589, 749)
        Me.DataGridView1.TabIndex = 0
        '
        'rechkinx
        '
        Me.rechkinx.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.NullValue = Nothing
        Me.rechkinx.DefaultCellStyle = DataGridViewCellStyle7
        Me.rechkinx.DividerWidth = 5
        Me.rechkinx.Frozen = True
        Me.rechkinx.HeaderText = "NO."
        Me.rechkinx.Name = "rechkinx"
        Me.rechkinx.ReadOnly = True
        Me.rechkinx.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.rechkinx.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.rechkinx.Width = 50
        '
        'bocdecone
        '
        Me.bocdecone.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bocdecone.DefaultCellStyle = DataGridViewCellStyle8
        Me.bocdecone.DividerWidth = 5
        Me.bocdecone.Frozen = True
        Me.bocdecone.HeaderText = "S/P No."
        Me.bocdecone.Name = "bocdecone"
        Me.bocdecone.ReadOnly = True
        '
        'rechk1
        '
        Me.rechk1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.rechk1.DefaultCellStyle = DataGridViewCellStyle9
        Me.rechk1.DividerWidth = 5
        Me.rechk1.Frozen = True
        Me.rechk1.HeaderText = "ReCheck 1"
        Me.rechk1.Name = "rechk1"
        Me.rechk1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'rechk2
        '
        Me.rechk2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rechk2.DefaultCellStyle = DataGridViewCellStyle10
        Me.rechk2.DividerWidth = 5
        Me.rechk2.Frozen = True
        Me.rechk2.HeaderText = "ReCheck 2"
        Me.rechk2.Name = "rechk2"
        '
        'rechkGrade
        '
        Me.rechkGrade.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rechkGrade.DefaultCellStyle = DataGridViewCellStyle11
        Me.rechkGrade.DividerWidth = 5
        Me.rechkGrade.Frozen = True
        Me.rechkGrade.HeaderText = "Grade"
        Me.rechkGrade.Name = "rechkGrade"
        Me.rechkGrade.ReadOnly = True
        Me.rechkGrade.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'rechkdef
        '
        Me.rechkdef.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.rechkdef.DefaultCellStyle = DataGridViewCellStyle12
        Me.rechkdef.DividerWidth = 5
        Me.rechkdef.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rechkdef.Frozen = True
        Me.rechkdef.HeaderText = "Remark"
        Me.rechkdef.Items.AddRange(New Object() {"K ", "D", "F", "O", "T", "P", "S", "X", "N", "DO", "DH", "CL", "FI", "YN", "HT", "LT"})
        Me.rechkdef.Name = "rechkdef"
        Me.rechkdef.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'btnFinish
        '
        Me.btnFinish.Location = New System.Drawing.Point(706, 135)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(75, 23)
        Me.btnFinish.TabIndex = 1
        Me.btnFinish.Text = "Finish"
        Me.btnFinish.UseVisualStyleBackColor = True
        '
        'frmColReCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1086, 761)
        Me.Controls.Add(Me.btnFinish)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmColReCheck"
        Me.Text = "frmColReCheck"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents rechkinx As DataGridViewTextBoxColumn
    Friend WithEvents bocdecone As DataGridViewTextBoxColumn
    Friend WithEvents rechk1 As DataGridViewTextBoxColumn
    Friend WithEvents rechk2 As DataGridViewTextBoxColumn
    Friend WithEvents rechkGrade As DataGridViewTextBoxColumn
    Friend WithEvents rechkdef As DataGridViewComboBoxColumn
    Friend WithEvents btnFinish As Button
End Class
