<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProductMod
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductMod))
        Me.DGVProduct = New System.Windows.Forms.DataGridView()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.Toraydb = New Development_Socks.Toraydb()
        Me.ToraydbBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.RowsToUpdate = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBoxOperator = New System.Windows.Forms.TextBox()
        Me.BbtnEnter = New System.Windows.Forms.Button()
        CType(Me.DGVProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVProduct
        '
        Me.DGVProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DGVProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVProduct.Location = New System.Drawing.Point(0, 0)
        Me.DGVProduct.Name = "DGVProduct"
        Me.DGVProduct.Size = New System.Drawing.Size(894, 524)
        Me.DGVProduct.TabIndex = 0
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnUpdate.BackColor = System.Drawing.Color.LawnGreen
        Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(357, 531)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(212, 58)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Text = "Update View"
        Me.btnUpdate.UseVisualStyleBackColor = False
        Me.btnUpdate.Visible = False
        '
        'Toraydb
        '
        Me.Toraydb.DataSetName = "Toraydb"
        Me.Toraydb.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ToraydbBindingSource
        '
        Me.ToraydbBindingSource.DataSource = Me.Toraydb
        Me.ToraydbBindingSource.Position = 0
        '
        'RowsToUpdate
        '
        Me.RowsToUpdate.FormattingEnabled = True
        Me.RowsToUpdate.Location = New System.Drawing.Point(28, 31)
        Me.RowsToUpdate.Name = "RowsToUpdate"
        Me.RowsToUpdate.Size = New System.Drawing.Size(120, 95)
        Me.RowsToUpdate.TabIndex = 3
        Me.RowsToUpdate.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(240, 270)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 24)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Enter Name"
        Me.Label1.Visible = False
        '
        'txtBoxOperator
        '
        Me.txtBoxOperator.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxOperator.Location = New System.Drawing.Point(357, 267)
        Me.txtBoxOperator.Name = "txtBoxOperator"
        Me.txtBoxOperator.Size = New System.Drawing.Size(258, 29)
        Me.txtBoxOperator.TabIndex = 5
        Me.txtBoxOperator.Visible = False
        '
        'BbtnEnter
        '
        Me.BbtnEnter.BackColor = System.Drawing.Color.GreenYellow
        Me.BbtnEnter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BbtnEnter.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.BbtnEnter.Location = New System.Drawing.Point(400, 319)
        Me.BbtnEnter.Name = "BbtnEnter"
        Me.BbtnEnter.Size = New System.Drawing.Size(116, 30)
        Me.BbtnEnter.TabIndex = 6
        Me.BbtnEnter.Text = "Enter"
        Me.BbtnEnter.UseVisualStyleBackColor = False
        Me.BbtnEnter.Visible = False
        '
        'frmProductMod
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 594)
        Me.Controls.Add(Me.BbtnEnter)
        Me.Controls.Add(Me.txtBoxOperator)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RowsToUpdate)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.DGVProduct)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmProductMod"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Product Update"
        CType(Me.DGVProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Toraydb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToraydbBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGVProduct As DataGridView
    Friend WithEvents btnUpdate As Button
    Friend WithEvents ToraydbBindingSource As BindingSource
    Friend WithEvents Toraydb As Toraydb
    Friend WithEvents RowsToUpdate As ListBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtBoxOperator As TextBox
    Friend WithEvents BbtnEnter As Button
End Class
