<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmConeSearch
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtBoxJob = New System.Windows.Forms.TextBox()
        Me.txtBoxConeBC = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnJobSearch = New System.Windows.Forms.Button()
        Me.btnConeSearch = New System.Windows.Forms.Button()
        Me.txtBoxSpindle = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtBoxProdName = New System.Windows.Forms.TextBox()
        Me.txtBoxDoff = New System.Windows.Forms.TextBox()
        Me.txtBoxPackDate = New System.Windows.Forms.TextBox()
        Me.txtBoxPacker = New System.Windows.Forms.TextBox()
        Me.txtBoxColour = New System.Windows.Forms.TextBox()
        Me.txtBoxDef = New System.Windows.Forms.TextBox()
        Me.txtBoxGrad = New System.Windows.Forms.TextBox()
        Me.txtBoxShort = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnHome = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtBoxMCNum = New System.Windows.Forms.TextBox()
        Me.txtBoxCartonNum = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(280, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(293, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search for Cheese Information"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(-1, 145)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Job Barcode #"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(-1, 208)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(157, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Cheese Barcode #"
        '
        'txtBoxJob
        '
        Me.txtBoxJob.Location = New System.Drawing.Point(160, 142)
        Me.txtBoxJob.Name = "txtBoxJob"
        Me.txtBoxJob.Size = New System.Drawing.Size(285, 26)
        Me.txtBoxJob.TabIndex = 3
        '
        'txtBoxConeBC
        '
        Me.txtBoxConeBC.Location = New System.Drawing.Point(160, 205)
        Me.txtBoxConeBC.Name = "txtBoxConeBC"
        Me.txtBoxConeBC.Size = New System.Drawing.Size(212, 26)
        Me.txtBoxConeBC.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(467, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 20)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Spindle #"
        '
        'btnJobSearch
        '
        Me.btnJobSearch.Location = New System.Drawing.Point(648, 142)
        Me.btnJobSearch.Name = "btnJobSearch"
        Me.btnJobSearch.Size = New System.Drawing.Size(100, 30)
        Me.btnJobSearch.TabIndex = 7
        Me.btnJobSearch.Text = "Search"
        Me.btnJobSearch.UseVisualStyleBackColor = True
        '
        'btnConeSearch
        '
        Me.btnConeSearch.Location = New System.Drawing.Point(378, 205)
        Me.btnConeSearch.Name = "btnConeSearch"
        Me.btnConeSearch.Size = New System.Drawing.Size(96, 30)
        Me.btnConeSearch.TabIndex = 8
        Me.btnConeSearch.Text = "Search"
        Me.btnConeSearch.UseVisualStyleBackColor = True
        '
        'txtBoxSpindle
        '
        Me.txtBoxSpindle.Location = New System.Drawing.Point(571, 142)
        Me.txtBoxSpindle.Name = "txtBoxSpindle"
        Me.txtBoxSpindle.Size = New System.Drawing.Size(71, 26)
        Me.txtBoxSpindle.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(41, 330)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 20)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Product"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(567, 332)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 20)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Defects"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(41, 438)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 20)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Date Packed"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(292, 368)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 20)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Packer"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(41, 368)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(59, 20)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Doff #"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(292, 404)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(132, 20)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Colour Checker"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(567, 368)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(114, 20)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "Grade A or B"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(567, 404)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 20)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Short"
        '
        'txtBoxProdName
        '
        Me.txtBoxProdName.Enabled = False
        Me.txtBoxProdName.Location = New System.Drawing.Point(182, 325)
        Me.txtBoxProdName.Name = "txtBoxProdName"
        Me.txtBoxProdName.Size = New System.Drawing.Size(363, 26)
        Me.txtBoxProdName.TabIndex = 19
        '
        'txtBoxDoff
        '
        Me.txtBoxDoff.Enabled = False
        Me.txtBoxDoff.Location = New System.Drawing.Point(182, 363)
        Me.txtBoxDoff.Name = "txtBoxDoff"
        Me.txtBoxDoff.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxDoff.TabIndex = 20
        '
        'txtBoxPackDate
        '
        Me.txtBoxPackDate.Enabled = False
        Me.txtBoxPackDate.Location = New System.Drawing.Point(182, 433)
        Me.txtBoxPackDate.Name = "txtBoxPackDate"
        Me.txtBoxPackDate.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxPackDate.TabIndex = 21
        '
        'txtBoxPacker
        '
        Me.txtBoxPacker.Enabled = False
        Me.txtBoxPacker.Location = New System.Drawing.Point(461, 363)
        Me.txtBoxPacker.Name = "txtBoxPacker"
        Me.txtBoxPacker.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxPacker.TabIndex = 22
        '
        'txtBoxColour
        '
        Me.txtBoxColour.Enabled = False
        Me.txtBoxColour.Location = New System.Drawing.Point(461, 403)
        Me.txtBoxColour.Name = "txtBoxColour"
        Me.txtBoxColour.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxColour.TabIndex = 23
        '
        'txtBoxDef
        '
        Me.txtBoxDef.Enabled = False
        Me.txtBoxDef.Location = New System.Drawing.Point(710, 328)
        Me.txtBoxDef.Name = "txtBoxDef"
        Me.txtBoxDef.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxDef.TabIndex = 24
        '
        'txtBoxGrad
        '
        Me.txtBoxGrad.Enabled = False
        Me.txtBoxGrad.Location = New System.Drawing.Point(710, 363)
        Me.txtBoxGrad.Name = "txtBoxGrad"
        Me.txtBoxGrad.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxGrad.TabIndex = 25
        '
        'txtBoxShort
        '
        Me.txtBoxShort.Enabled = False
        Me.txtBoxShort.Location = New System.Drawing.Point(710, 399)
        Me.txtBoxShort.Name = "txtBoxShort"
        Me.txtBoxShort.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxShort.TabIndex = 26
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(3, 237)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(831, 83)
        Me.DataGridView1.TabIndex = 27
        Me.DataGridView1.Visible = False
        '
        'btnHome
        '
        Me.btnHome.Location = New System.Drawing.Point(711, 196)
        Me.btnHome.Name = "btnHome"
        Me.btnHome.Size = New System.Drawing.Size(112, 32)
        Me.btnHome.TabIndex = 28
        Me.btnHome.Text = "Home"
        Me.btnHome.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(41, 404)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(91, 20)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "Machine #"
        '
        'txtBoxMCNum
        '
        Me.txtBoxMCNum.Enabled = False
        Me.txtBoxMCNum.Location = New System.Drawing.Point(182, 397)
        Me.txtBoxMCNum.Name = "txtBoxMCNum"
        Me.txtBoxMCNum.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxMCNum.TabIndex = 30
        '
        'txtBoxCartonNum
        '
        Me.txtBoxCartonNum.Enabled = False
        Me.txtBoxCartonNum.Location = New System.Drawing.Point(710, 436)
        Me.txtBoxCartonNum.Name = "txtBoxCartonNum"
        Me.txtBoxCartonNum.Size = New System.Drawing.Size(100, 26)
        Me.txtBoxCartonNum.TabIndex = 32
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(567, 438)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(78, 20)
        Me.Label14.TabIndex = 31
        Me.Label14.Text = "Carton #"
        '
        'frmConeSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(835, 483)
        Me.Controls.Add(Me.txtBoxCartonNum)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtBoxMCNum)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btnHome)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtBoxShort)
        Me.Controls.Add(Me.txtBoxGrad)
        Me.Controls.Add(Me.txtBoxDef)
        Me.Controls.Add(Me.txtBoxColour)
        Me.Controls.Add(Me.txtBoxPacker)
        Me.Controls.Add(Me.txtBoxPackDate)
        Me.Controls.Add(Me.txtBoxDoff)
        Me.Controls.Add(Me.txtBoxProdName)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtBoxSpindle)
        Me.Controls.Add(Me.btnConeSearch)
        Me.Controls.Add(Me.btnJobSearch)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtBoxConeBC)
        Me.Controls.Add(Me.txtBoxJob)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "frmConeSearch"
        Me.Text = "Cone Search"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtBoxJob As TextBox
    Friend WithEvents txtBoxConeBC As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnJobSearch As Button
    Friend WithEvents btnConeSearch As Button
    Friend WithEvents txtBoxSpindle As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents txtBoxProdName As TextBox
    Friend WithEvents txtBoxDoff As TextBox
    Friend WithEvents txtBoxPackDate As TextBox
    Friend WithEvents txtBoxPacker As TextBox
    Friend WithEvents txtBoxColour As TextBox
    Friend WithEvents txtBoxDef As TextBox
    Friend WithEvents txtBoxGrad As TextBox
    Friend WithEvents txtBoxShort As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnHome As Button
    Friend WithEvents Label13 As Label
    Friend WithEvents txtBoxMCNum As TextBox
    Friend WithEvents txtBoxCartonNum As TextBox
    Friend WithEvents Label14 As Label
End Class
