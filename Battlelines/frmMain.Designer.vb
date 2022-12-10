<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
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
        Me.picMap = New System.Windows.Forms.PictureBox()
        Me.txtPop = New System.Windows.Forms.Label()
        Me.lblEndTurn = New System.Windows.Forms.Label()
        Me.lblMove = New System.Windows.Forms.Label()
        Me.lblAuto = New System.Windows.Forms.Label()
        Me.lblNewGame = New System.Windows.Forms.Label()
        Me.lblSpec1 = New System.Windows.Forms.Label()
        Me.lblSpec2 = New System.Windows.Forms.Label()
        Me.lblSpec3 = New System.Windows.Forms.Label()
        Me.txtSpecPop = New System.Windows.Forms.TextBox()
        Me.lblTurn = New System.Windows.Forms.Label()
        Me.pnlChar = New System.Windows.Forms.Panel()
        Me.lblMaxDamage = New System.Windows.Forms.Label()
        Me.lblMaxRange = New System.Windows.Forms.Label()
        Me.lblCharName = New System.Windows.Forms.Label()
        Me.lblMaxMove = New System.Windows.Forms.Label()
        Me.lblMaxAttacks = New System.Windows.Forms.Label()
        Me.lblMaxHP = New System.Windows.Forms.Label()
        Me.lblLog1 = New System.Windows.Forms.Label()
        Me.lblLog2 = New System.Windows.Forms.Label()
        Me.lblLog3 = New System.Windows.Forms.Label()
        Me.lblLog4 = New System.Windows.Forms.Label()
        Me.lblLog5 = New System.Windows.Forms.Label()
        Me.lblLog6 = New System.Windows.Forms.Label()
        Me.lblLog7 = New System.Windows.Forms.Label()
        Me.lblLog8 = New System.Windows.Forms.Label()
        Me.lblLog9 = New System.Windows.Forms.Label()
        Me.lblLog10 = New System.Windows.Forms.Label()
        Me.lblLog11 = New System.Windows.Forms.Label()
        Me.lblLog12 = New System.Windows.Forms.Label()
        Me.lblPrompt = New System.Windows.Forms.Label()
        Me.pb1 = New System.Windows.Forms.ProgressBar()
        Me.pb2 = New System.Windows.Forms.ProgressBar()
        Me.pb3 = New System.Windows.Forms.ProgressBar()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblLog15 = New System.Windows.Forms.Label()
        Me.lblLog14 = New System.Windows.Forms.Label()
        Me.lblLog13 = New System.Windows.Forms.Label()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.lblLog16 = New System.Windows.Forms.Label()
        Me.lblLog17 = New System.Windows.Forms.Label()
        Me.lblLog18 = New System.Windows.Forms.Label()
        Me.lblLog19 = New System.Windows.Forms.Label()
        Me.lblLog20 = New System.Windows.Forms.Label()
        CType(Me.picMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlChar.SuspendLayout()
        Me.SuspendLayout()
        '
        'picMap
        '
        Me.picMap.BackColor = System.Drawing.Color.White
        Me.picMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picMap.Location = New System.Drawing.Point(175, 10)
        Me.picMap.Name = "picMap"
        Me.picMap.Size = New System.Drawing.Size(645, 307)
        Me.picMap.TabIndex = 0
        Me.picMap.TabStop = False
        '
        'txtPop
        '
        Me.txtPop.AutoSize = True
        Me.txtPop.BackColor = System.Drawing.Color.White
        Me.txtPop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPop.Location = New System.Drawing.Point(273, 348)
        Me.txtPop.Name = "txtPop"
        Me.txtPop.Size = New System.Drawing.Size(33, 17)
        Me.txtPop.TabIndex = 3
        Me.txtPop.Text = "Map"
        Me.txtPop.Visible = False
        '
        'lblEndTurn
        '
        Me.lblEndTurn.BackColor = System.Drawing.Color.AliceBlue
        Me.lblEndTurn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEndTurn.Location = New System.Drawing.Point(12, 501)
        Me.lblEndTurn.Name = "lblEndTurn"
        Me.lblEndTurn.Size = New System.Drawing.Size(148, 23)
        Me.lblEndTurn.TabIndex = 4
        Me.lblEndTurn.Text = "End Turn (X)"
        Me.lblEndTurn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMove
        '
        Me.lblMove.BackColor = System.Drawing.Color.AliceBlue
        Me.lblMove.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblMove.Location = New System.Drawing.Point(12, 537)
        Me.lblMove.Name = "lblMove"
        Me.lblMove.Size = New System.Drawing.Size(148, 23)
        Me.lblMove.TabIndex = 9
        Me.lblMove.Text = "Do Turn (Space)"
        Me.lblMove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAuto
        '
        Me.lblAuto.BackColor = System.Drawing.Color.AliceBlue
        Me.lblAuto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAuto.Location = New System.Drawing.Point(12, 573)
        Me.lblAuto.Name = "lblAuto"
        Me.lblAuto.Size = New System.Drawing.Size(148, 23)
        Me.lblAuto.TabIndex = 10
        Me.lblAuto.Text = "Start Auto (A)"
        Me.lblAuto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNewGame
        '
        Me.lblNewGame.BackColor = System.Drawing.Color.AliceBlue
        Me.lblNewGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNewGame.Location = New System.Drawing.Point(12, 462)
        Me.lblNewGame.Name = "lblNewGame"
        Me.lblNewGame.Size = New System.Drawing.Size(148, 23)
        Me.lblNewGame.TabIndex = 22
        Me.lblNewGame.Text = "New Game"
        Me.lblNewGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpec1
        '
        Me.lblSpec1.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSpec1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSpec1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpec1.Location = New System.Drawing.Point(12, 185)
        Me.lblSpec1.Name = "lblSpec1"
        Me.lblSpec1.Size = New System.Drawing.Size(148, 23)
        Me.lblSpec1.TabIndex = 23
        Me.lblSpec1.Tag = "1"
        Me.lblSpec1.Text = "Special One"
        Me.lblSpec1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpec2
        '
        Me.lblSpec2.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSpec2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSpec2.Location = New System.Drawing.Point(12, 228)
        Me.lblSpec2.Name = "lblSpec2"
        Me.lblSpec2.Size = New System.Drawing.Size(148, 23)
        Me.lblSpec2.TabIndex = 24
        Me.lblSpec2.Tag = "2"
        Me.lblSpec2.Text = "Special Two"
        Me.lblSpec2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpec3
        '
        Me.lblSpec3.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSpec3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSpec3.Location = New System.Drawing.Point(12, 271)
        Me.lblSpec3.Name = "lblSpec3"
        Me.lblSpec3.Size = New System.Drawing.Size(148, 23)
        Me.lblSpec3.TabIndex = 25
        Me.lblSpec3.Tag = "3"
        Me.lblSpec3.Text = "Special Three"
        Me.lblSpec3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSpecPop
        '
        Me.txtSpecPop.BackColor = System.Drawing.Color.White
        Me.txtSpecPop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpecPop.Enabled = False
        Me.txtSpecPop.Location = New System.Drawing.Point(175, 348)
        Me.txtSpecPop.Multiline = True
        Me.txtSpecPop.Name = "txtSpecPop"
        Me.txtSpecPop.ReadOnly = True
        Me.txtSpecPop.Size = New System.Drawing.Size(70, 23)
        Me.txtSpecPop.TabIndex = 26
        Me.txtSpecPop.Text = "Spec"
        Me.txtSpecPop.Visible = False
        '
        'lblTurn
        '
        Me.lblTurn.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblTurn.Location = New System.Drawing.Point(12, 10)
        Me.lblTurn.Name = "lblTurn"
        Me.lblTurn.Size = New System.Drawing.Size(117, 21)
        Me.lblTurn.TabIndex = 27
        Me.lblTurn.Text = "Turn:"
        '
        'pnlChar
        '
        Me.pnlChar.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlChar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlChar.Controls.Add(Me.lblMaxDamage)
        Me.pnlChar.Controls.Add(Me.lblMaxRange)
        Me.pnlChar.Controls.Add(Me.lblCharName)
        Me.pnlChar.Controls.Add(Me.lblMaxMove)
        Me.pnlChar.Controls.Add(Me.lblMaxAttacks)
        Me.pnlChar.Controls.Add(Me.lblMaxHP)
        Me.pnlChar.Location = New System.Drawing.Point(12, 34)
        Me.pnlChar.Name = "pnlChar"
        Me.pnlChar.Size = New System.Drawing.Size(148, 138)
        Me.pnlChar.TabIndex = 28
        '
        'lblMaxDamage
        '
        Me.lblMaxDamage.BackColor = System.Drawing.Color.Transparent
        Me.lblMaxDamage.Location = New System.Drawing.Point(1, 110)
        Me.lblMaxDamage.Name = "lblMaxDamage"
        Me.lblMaxDamage.Size = New System.Drawing.Size(146, 16)
        Me.lblMaxDamage.TabIndex = 33
        Me.lblMaxDamage.Text = "Max Damage"
        Me.lblMaxDamage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMaxRange
        '
        Me.lblMaxRange.BackColor = System.Drawing.Color.Transparent
        Me.lblMaxRange.Location = New System.Drawing.Point(1, 89)
        Me.lblMaxRange.Name = "lblMaxRange"
        Me.lblMaxRange.Size = New System.Drawing.Size(146, 16)
        Me.lblMaxRange.TabIndex = 34
        Me.lblMaxRange.Text = "Max Range"
        Me.lblMaxRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCharName
        '
        Me.lblCharName.BackColor = System.Drawing.Color.Transparent
        Me.lblCharName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblCharName.Location = New System.Drawing.Point(1, 3)
        Me.lblCharName.Name = "lblCharName"
        Me.lblCharName.Size = New System.Drawing.Size(146, 16)
        Me.lblCharName.TabIndex = 32
        Me.lblCharName.Text = "Character Name"
        Me.lblCharName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMaxMove
        '
        Me.lblMaxMove.BackColor = System.Drawing.Color.Transparent
        Me.lblMaxMove.Location = New System.Drawing.Point(1, 47)
        Me.lblMaxMove.Name = "lblMaxMove"
        Me.lblMaxMove.Size = New System.Drawing.Size(146, 16)
        Me.lblMaxMove.TabIndex = 31
        Me.lblMaxMove.Text = "Max Movement"
        Me.lblMaxMove.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMaxAttacks
        '
        Me.lblMaxAttacks.BackColor = System.Drawing.Color.Transparent
        Me.lblMaxAttacks.Location = New System.Drawing.Point(1, 68)
        Me.lblMaxAttacks.Name = "lblMaxAttacks"
        Me.lblMaxAttacks.Size = New System.Drawing.Size(146, 16)
        Me.lblMaxAttacks.TabIndex = 30
        Me.lblMaxAttacks.Text = "Max Attacks"
        Me.lblMaxAttacks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMaxHP
        '
        Me.lblMaxHP.BackColor = System.Drawing.Color.Transparent
        Me.lblMaxHP.Location = New System.Drawing.Point(1, 27)
        Me.lblMaxHP.Name = "lblMaxHP"
        Me.lblMaxHP.Size = New System.Drawing.Size(146, 16)
        Me.lblMaxHP.TabIndex = 29
        Me.lblMaxHP.Text = "Effective Hit Points"
        Me.lblMaxHP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLog1
        '
        Me.lblLog1.AutoSize = True
        Me.lblLog1.BackColor = System.Drawing.Color.Transparent
        Me.lblLog1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblLog1.Location = New System.Drawing.Point(854, 10)
        Me.lblLog1.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog1.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog1.Name = "lblLog1"
        Me.lblLog1.Size = New System.Drawing.Size(299, 20)
        Me.lblLog1.TabIndex = 29
        Me.lblLog1.Tag = "1"
        '
        'lblLog2
        '
        Me.lblLog2.AutoSize = True
        Me.lblLog2.BackColor = System.Drawing.Color.Transparent
        Me.lblLog2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog2.Location = New System.Drawing.Point(854, 39)
        Me.lblLog2.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog2.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog2.Name = "lblLog2"
        Me.lblLog2.Size = New System.Drawing.Size(299, 20)
        Me.lblLog2.TabIndex = 30
        Me.lblLog2.Tag = "1"
        '
        'lblLog3
        '
        Me.lblLog3.AutoSize = True
        Me.lblLog3.BackColor = System.Drawing.Color.Transparent
        Me.lblLog3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog3.Location = New System.Drawing.Point(854, 68)
        Me.lblLog3.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog3.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog3.Name = "lblLog3"
        Me.lblLog3.Size = New System.Drawing.Size(299, 20)
        Me.lblLog3.TabIndex = 31
        Me.lblLog3.Tag = "1"
        '
        'lblLog4
        '
        Me.lblLog4.AutoSize = True
        Me.lblLog4.BackColor = System.Drawing.Color.Transparent
        Me.lblLog4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog4.Location = New System.Drawing.Point(854, 97)
        Me.lblLog4.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog4.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog4.Name = "lblLog4"
        Me.lblLog4.Size = New System.Drawing.Size(299, 20)
        Me.lblLog4.TabIndex = 32
        Me.lblLog4.Tag = "1"
        '
        'lblLog5
        '
        Me.lblLog5.AutoSize = True
        Me.lblLog5.BackColor = System.Drawing.Color.Transparent
        Me.lblLog5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog5.Location = New System.Drawing.Point(854, 126)
        Me.lblLog5.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog5.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog5.Name = "lblLog5"
        Me.lblLog5.Size = New System.Drawing.Size(299, 20)
        Me.lblLog5.TabIndex = 33
        Me.lblLog5.Tag = "1"
        '
        'lblLog6
        '
        Me.lblLog6.AutoSize = True
        Me.lblLog6.BackColor = System.Drawing.Color.Transparent
        Me.lblLog6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog6.Location = New System.Drawing.Point(854, 155)
        Me.lblLog6.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog6.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog6.Name = "lblLog6"
        Me.lblLog6.Size = New System.Drawing.Size(299, 20)
        Me.lblLog6.TabIndex = 34
        Me.lblLog6.Tag = "1"
        '
        'lblLog7
        '
        Me.lblLog7.AutoSize = True
        Me.lblLog7.BackColor = System.Drawing.Color.Transparent
        Me.lblLog7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog7.Location = New System.Drawing.Point(854, 184)
        Me.lblLog7.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog7.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog7.Name = "lblLog7"
        Me.lblLog7.Size = New System.Drawing.Size(299, 20)
        Me.lblLog7.TabIndex = 35
        Me.lblLog7.Tag = "1"
        '
        'lblLog8
        '
        Me.lblLog8.AutoSize = True
        Me.lblLog8.BackColor = System.Drawing.Color.Transparent
        Me.lblLog8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog8.Location = New System.Drawing.Point(854, 213)
        Me.lblLog8.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog8.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog8.Name = "lblLog8"
        Me.lblLog8.Size = New System.Drawing.Size(299, 20)
        Me.lblLog8.TabIndex = 36
        Me.lblLog8.Tag = "1"
        '
        'lblLog9
        '
        Me.lblLog9.AutoSize = True
        Me.lblLog9.BackColor = System.Drawing.Color.Transparent
        Me.lblLog9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog9.Location = New System.Drawing.Point(854, 242)
        Me.lblLog9.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog9.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog9.Name = "lblLog9"
        Me.lblLog9.Size = New System.Drawing.Size(299, 20)
        Me.lblLog9.TabIndex = 37
        Me.lblLog9.Tag = "1"
        '
        'lblLog10
        '
        Me.lblLog10.AutoSize = True
        Me.lblLog10.BackColor = System.Drawing.Color.Transparent
        Me.lblLog10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog10.Location = New System.Drawing.Point(854, 271)
        Me.lblLog10.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog10.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog10.Name = "lblLog10"
        Me.lblLog10.Size = New System.Drawing.Size(299, 20)
        Me.lblLog10.TabIndex = 38
        Me.lblLog10.Tag = "1"
        '
        'lblLog11
        '
        Me.lblLog11.AutoSize = True
        Me.lblLog11.BackColor = System.Drawing.Color.Transparent
        Me.lblLog11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog11.Location = New System.Drawing.Point(854, 300)
        Me.lblLog11.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog11.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog11.Name = "lblLog11"
        Me.lblLog11.Size = New System.Drawing.Size(299, 20)
        Me.lblLog11.TabIndex = 39
        Me.lblLog11.Tag = "1"
        '
        'lblLog12
        '
        Me.lblLog12.AutoSize = True
        Me.lblLog12.BackColor = System.Drawing.Color.Transparent
        Me.lblLog12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog12.Location = New System.Drawing.Point(854, 329)
        Me.lblLog12.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog12.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog12.Name = "lblLog12"
        Me.lblLog12.Size = New System.Drawing.Size(299, 20)
        Me.lblLog12.TabIndex = 40
        Me.lblLog12.Tag = "1"
        '
        'lblPrompt
        '
        Me.lblPrompt.ForeColor = System.Drawing.Color.Sienna
        Me.lblPrompt.Location = New System.Drawing.Point(12, 318)
        Me.lblPrompt.Name = "lblPrompt"
        Me.lblPrompt.Size = New System.Drawing.Size(148, 85)
        Me.lblPrompt.TabIndex = 60
        Me.lblPrompt.Text = "Instructions"
        '
        'pb1
        '
        Me.pb1.ForeColor = System.Drawing.Color.DeepSkyBlue
        Me.pb1.Location = New System.Drawing.Point(12, 208)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(148, 6)
        Me.pb1.TabIndex = 61
        Me.pb1.Value = 50
        '
        'pb2
        '
        Me.pb2.ForeColor = System.Drawing.Color.DeepSkyBlue
        Me.pb2.Location = New System.Drawing.Point(12, 251)
        Me.pb2.Name = "pb2"
        Me.pb2.Size = New System.Drawing.Size(148, 6)
        Me.pb2.TabIndex = 62
        Me.pb2.Value = 50
        '
        'pb3
        '
        Me.pb3.ForeColor = System.Drawing.Color.DeepSkyBlue
        Me.pb3.Location = New System.Drawing.Point(12, 294)
        Me.pb3.Name = "pb3"
        Me.pb3.Size = New System.Drawing.Size(148, 6)
        Me.pb3.TabIndex = 63
        Me.pb3.Value = 50
        '
        'lblCopyright
        '
        Me.lblCopyright.ForeColor = System.Drawing.Color.Sienna
        Me.lblCopyright.Location = New System.Drawing.Point(12, 606)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(148, 22)
        Me.lblCopyright.TabIndex = 64
        Me.lblCopyright.Text = "Bitistry Software © 2021"
        Me.lblCopyright.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lblLog15
        '
        Me.lblLog15.AutoSize = True
        Me.lblLog15.BackColor = System.Drawing.Color.Transparent
        Me.lblLog15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog15.Location = New System.Drawing.Point(854, 416)
        Me.lblLog15.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog15.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog15.Name = "lblLog15"
        Me.lblLog15.Size = New System.Drawing.Size(299, 20)
        Me.lblLog15.TabIndex = 67
        Me.lblLog15.Tag = "1"
        '
        'lblLog14
        '
        Me.lblLog14.AutoSize = True
        Me.lblLog14.BackColor = System.Drawing.Color.Transparent
        Me.lblLog14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog14.Location = New System.Drawing.Point(854, 387)
        Me.lblLog14.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog14.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog14.Name = "lblLog14"
        Me.lblLog14.Size = New System.Drawing.Size(299, 20)
        Me.lblLog14.TabIndex = 66
        Me.lblLog14.Tag = "1"
        '
        'lblLog13
        '
        Me.lblLog13.AutoSize = True
        Me.lblLog13.BackColor = System.Drawing.Color.Transparent
        Me.lblLog13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog13.Location = New System.Drawing.Point(854, 358)
        Me.lblLog13.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog13.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog13.Name = "lblLog13"
        Me.lblLog13.Size = New System.Drawing.Size(299, 20)
        Me.lblLog13.TabIndex = 65
        Me.lblLog13.Tag = "1"
        '
        'lblHelp
        '
        Me.lblHelp.BackColor = System.Drawing.Color.AliceBlue
        Me.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblHelp.Location = New System.Drawing.Point(135, 10)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(25, 21)
        Me.lblHelp.TabIndex = 68
        Me.lblHelp.Tag = "3"
        Me.lblHelp.Text = "?"
        Me.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLog16
        '
        Me.lblLog16.AutoSize = True
        Me.lblLog16.BackColor = System.Drawing.Color.Transparent
        Me.lblLog16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog16.Location = New System.Drawing.Point(854, 445)
        Me.lblLog16.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog16.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog16.Name = "lblLog16"
        Me.lblLog16.Size = New System.Drawing.Size(299, 20)
        Me.lblLog16.TabIndex = 69
        Me.lblLog16.Tag = "1"
        '
        'lblLog17
        '
        Me.lblLog17.AutoSize = True
        Me.lblLog17.BackColor = System.Drawing.Color.Transparent
        Me.lblLog17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog17.Location = New System.Drawing.Point(854, 474)
        Me.lblLog17.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog17.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog17.Name = "lblLog17"
        Me.lblLog17.Size = New System.Drawing.Size(299, 20)
        Me.lblLog17.TabIndex = 70
        Me.lblLog17.Tag = "1"
        '
        'lblLog18
        '
        Me.lblLog18.AutoSize = True
        Me.lblLog18.BackColor = System.Drawing.Color.Transparent
        Me.lblLog18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog18.Location = New System.Drawing.Point(854, 503)
        Me.lblLog18.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog18.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog18.Name = "lblLog18"
        Me.lblLog18.Size = New System.Drawing.Size(299, 20)
        Me.lblLog18.TabIndex = 71
        Me.lblLog18.Tag = "1"
        '
        'lblLog19
        '
        Me.lblLog19.AutoSize = True
        Me.lblLog19.BackColor = System.Drawing.Color.Transparent
        Me.lblLog19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog19.Location = New System.Drawing.Point(854, 532)
        Me.lblLog19.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog19.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog19.Name = "lblLog19"
        Me.lblLog19.Size = New System.Drawing.Size(299, 20)
        Me.lblLog19.TabIndex = 72
        Me.lblLog19.Tag = "1"
        '
        'lblLog20
        '
        Me.lblLog20.AutoSize = True
        Me.lblLog20.BackColor = System.Drawing.Color.Transparent
        Me.lblLog20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLog20.Location = New System.Drawing.Point(854, 561)
        Me.lblLog20.MaximumSize = New System.Drawing.Size(299, 400)
        Me.lblLog20.MinimumSize = New System.Drawing.Size(299, 20)
        Me.lblLog20.Name = "lblLog20"
        Me.lblLog20.Size = New System.Drawing.Size(299, 20)
        Me.lblLog20.TabIndex = 73
        Me.lblLog20.Tag = "1"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1346, 637)
        Me.Controls.Add(Me.lblLog20)
        Me.Controls.Add(Me.lblLog19)
        Me.Controls.Add(Me.lblLog18)
        Me.Controls.Add(Me.lblLog17)
        Me.Controls.Add(Me.lblLog16)
        Me.Controls.Add(Me.lblHelp)
        Me.Controls.Add(Me.lblLog15)
        Me.Controls.Add(Me.lblLog14)
        Me.Controls.Add(Me.lblLog13)
        Me.Controls.Add(Me.lblCopyright)
        Me.Controls.Add(Me.pb3)
        Me.Controls.Add(Me.pb2)
        Me.Controls.Add(Me.pb1)
        Me.Controls.Add(Me.lblPrompt)
        Me.Controls.Add(Me.lblLog12)
        Me.Controls.Add(Me.lblLog11)
        Me.Controls.Add(Me.lblLog10)
        Me.Controls.Add(Me.lblLog9)
        Me.Controls.Add(Me.lblLog8)
        Me.Controls.Add(Me.lblLog7)
        Me.Controls.Add(Me.lblLog6)
        Me.Controls.Add(Me.lblLog5)
        Me.Controls.Add(Me.lblLog4)
        Me.Controls.Add(Me.lblLog3)
        Me.Controls.Add(Me.lblLog2)
        Me.Controls.Add(Me.lblLog1)
        Me.Controls.Add(Me.pnlChar)
        Me.Controls.Add(Me.txtSpecPop)
        Me.Controls.Add(Me.lblSpec3)
        Me.Controls.Add(Me.lblSpec2)
        Me.Controls.Add(Me.lblSpec1)
        Me.Controls.Add(Me.lblNewGame)
        Me.Controls.Add(Me.lblAuto)
        Me.Controls.Add(Me.lblMove)
        Me.Controls.Add(Me.lblEndTurn)
        Me.Controls.Add(Me.txtPop)
        Me.Controls.Add(Me.picMap)
        Me.Controls.Add(Me.lblTurn)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1362, 676)
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Battle Lines"
        CType(Me.picMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlChar.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents picMap As PictureBox
    Friend WithEvents txtPop As Label
    Friend WithEvents lblEndTurn As Label
    Friend WithEvents lblMove As Label
    Friend WithEvents lblAuto As Label
    Friend WithEvents lblNewGame As Label
    Friend WithEvents lblSpec1 As Label
    Friend WithEvents lblSpec2 As Label
    Friend WithEvents lblSpec3 As Label
    Friend WithEvents txtSpecPop As TextBox
    Friend WithEvents lblTurn As Label
    Friend WithEvents pnlChar As Panel
    Friend WithEvents lblMaxHP As Label
    Friend WithEvents lblCharName As Label
    Friend WithEvents lblMaxMove As Label
    Friend WithEvents lblMaxAttacks As Label
    Friend WithEvents lblMaxDamage As Label
    Friend WithEvents lblMaxRange As Label
    Friend WithEvents lblLog1 As Label
    Friend WithEvents lblLog2 As Label
    Friend WithEvents lblLog3 As Label
    Friend WithEvents lblLog4 As Label
    Friend WithEvents lblLog5 As Label
    Friend WithEvents lblLog6 As Label
    Friend WithEvents lblLog7 As Label
    Friend WithEvents lblLog8 As Label
    Friend WithEvents lblLog9 As Label
    Friend WithEvents lblLog10 As Label
    Friend WithEvents lblLog11 As Label
    Friend WithEvents lblLog12 As Label
    Friend WithEvents lblPrompt As Label
    Friend WithEvents pb1 As ProgressBar
    Friend WithEvents pb2 As ProgressBar
    Friend WithEvents pb3 As ProgressBar
    Friend WithEvents lblCopyright As Label
    Friend WithEvents lblLog15 As Label
    Friend WithEvents lblLog14 As Label
    Friend WithEvents lblLog13 As Label
    Friend WithEvents lblHelp As Label
    Friend WithEvents lblLog16 As Label
    Friend WithEvents lblLog17 As Label
    Friend WithEvents lblLog18 As Label
    Friend WithEvents lblLog19 As Label
    Friend WithEvents lblLog20 As Label
End Class
