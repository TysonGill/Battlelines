<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetup
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
        Me.btnRandomize = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCaptain = New System.Windows.Forms.Label()
        Me.lblLieutenant1 = New System.Windows.Forms.Label()
        Me.lblLieutenant2 = New System.Windows.Forms.Label()
        Me.lblSoldier1 = New System.Windows.Forms.Label()
        Me.lblSoldier2 = New System.Windows.Forms.Label()
        Me.lblSoldier3 = New System.Windows.Forms.Label()
        Me.lblSoldier6 = New System.Windows.Forms.Label()
        Me.lblSoldier5 = New System.Windows.Forms.Label()
        Me.lblSoldier4 = New System.Windows.Forms.Label()
        Me.lblLieutenant3 = New System.Windows.Forms.Label()
        Me.lblInstructions = New System.Windows.Forms.Label()
        Me.btnFinished = New System.Windows.Forms.Button()
        Me.rbVanquish = New System.Windows.Forms.RadioButton()
        Me.rbCapture = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'btnRandomize
        '
        Me.btnRandomize.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRandomize.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRandomize.Location = New System.Drawing.Point(515, 8)
        Me.btnRandomize.Name = "btnRandomize"
        Me.btnRandomize.Size = New System.Drawing.Size(91, 44)
        Me.btnRandomize.TabIndex = 35
        Me.btnRandomize.TabStop = False
        Me.btnRandomize.Text = "Randomize"
        Me.btnRandomize.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(20, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 15)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Captain"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(184, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 15)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "Lieutenants"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(349, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 15)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Soldiers"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'lblCaptain
        '
        Me.lblCaptain.BackColor = System.Drawing.Color.AliceBlue
        Me.lblCaptain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCaptain.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblCaptain.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaptain.Location = New System.Drawing.Point(23, 90)
        Me.lblCaptain.Name = "lblCaptain"
        Me.lblCaptain.Size = New System.Drawing.Size(127, 24)
        Me.lblCaptain.TabIndex = 49
        Me.lblCaptain.Tag = "1"
        Me.lblCaptain.Text = "Fighter"
        Me.lblCaptain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLieutenant1
        '
        Me.lblLieutenant1.BackColor = System.Drawing.Color.AliceBlue
        Me.lblLieutenant1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLieutenant1.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblLieutenant1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLieutenant1.Location = New System.Drawing.Point(187, 90)
        Me.lblLieutenant1.Name = "lblLieutenant1"
        Me.lblLieutenant1.Size = New System.Drawing.Size(127, 24)
        Me.lblLieutenant1.TabIndex = 50
        Me.lblLieutenant1.Tag = "1"
        Me.lblLieutenant1.Text = "Monk"
        Me.lblLieutenant1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLieutenant2
        '
        Me.lblLieutenant2.BackColor = System.Drawing.Color.AliceBlue
        Me.lblLieutenant2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLieutenant2.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblLieutenant2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLieutenant2.Location = New System.Drawing.Point(187, 122)
        Me.lblLieutenant2.Name = "lblLieutenant2"
        Me.lblLieutenant2.Size = New System.Drawing.Size(127, 24)
        Me.lblLieutenant2.TabIndex = 51
        Me.lblLieutenant2.Tag = "1"
        Me.lblLieutenant2.Text = "Rogue"
        Me.lblLieutenant2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSoldier1
        '
        Me.lblSoldier1.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSoldier1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSoldier1.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblSoldier1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoldier1.Location = New System.Drawing.Point(352, 90)
        Me.lblSoldier1.Name = "lblSoldier1"
        Me.lblSoldier1.Size = New System.Drawing.Size(127, 24)
        Me.lblSoldier1.TabIndex = 52
        Me.lblSoldier1.Tag = "1"
        Me.lblSoldier1.Text = "Cleric"
        Me.lblSoldier1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSoldier2
        '
        Me.lblSoldier2.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSoldier2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSoldier2.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblSoldier2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoldier2.Location = New System.Drawing.Point(352, 122)
        Me.lblSoldier2.Name = "lblSoldier2"
        Me.lblSoldier2.Size = New System.Drawing.Size(127, 24)
        Me.lblSoldier2.TabIndex = 53
        Me.lblSoldier2.Tag = "1"
        Me.lblSoldier2.Text = "Ranger"
        Me.lblSoldier2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSoldier3
        '
        Me.lblSoldier3.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSoldier3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSoldier3.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblSoldier3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoldier3.Location = New System.Drawing.Point(352, 154)
        Me.lblSoldier3.Name = "lblSoldier3"
        Me.lblSoldier3.Size = New System.Drawing.Size(127, 24)
        Me.lblSoldier3.TabIndex = 54
        Me.lblSoldier3.Tag = "1"
        Me.lblSoldier3.Text = "Wizard"
        Me.lblSoldier3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSoldier6
        '
        Me.lblSoldier6.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSoldier6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSoldier6.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblSoldier6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoldier6.Location = New System.Drawing.Point(352, 250)
        Me.lblSoldier6.Name = "lblSoldier6"
        Me.lblSoldier6.Size = New System.Drawing.Size(127, 24)
        Me.lblSoldier6.TabIndex = 57
        Me.lblSoldier6.Tag = "1"
        Me.lblSoldier6.Text = "Wizard"
        Me.lblSoldier6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSoldier5
        '
        Me.lblSoldier5.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSoldier5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSoldier5.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblSoldier5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoldier5.Location = New System.Drawing.Point(352, 218)
        Me.lblSoldier5.Name = "lblSoldier5"
        Me.lblSoldier5.Size = New System.Drawing.Size(127, 24)
        Me.lblSoldier5.TabIndex = 56
        Me.lblSoldier5.Tag = "1"
        Me.lblSoldier5.Text = "Ranger"
        Me.lblSoldier5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSoldier4
        '
        Me.lblSoldier4.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSoldier4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSoldier4.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblSoldier4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoldier4.Location = New System.Drawing.Point(352, 186)
        Me.lblSoldier4.Name = "lblSoldier4"
        Me.lblSoldier4.Size = New System.Drawing.Size(127, 24)
        Me.lblSoldier4.TabIndex = 55
        Me.lblSoldier4.Tag = "1"
        Me.lblSoldier4.Text = "Cleric"
        Me.lblSoldier4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLieutenant3
        '
        Me.lblLieutenant3.BackColor = System.Drawing.Color.AliceBlue
        Me.lblLieutenant3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLieutenant3.Cursor = System.Windows.Forms.Cursors.PanEast
        Me.lblLieutenant3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLieutenant3.Location = New System.Drawing.Point(187, 154)
        Me.lblLieutenant3.Name = "lblLieutenant3"
        Me.lblLieutenant3.Size = New System.Drawing.Size(127, 24)
        Me.lblLieutenant3.TabIndex = 58
        Me.lblLieutenant3.Tag = "1"
        Me.lblLieutenant3.Text = "Wizard"
        Me.lblLieutenant3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInstructions
        '
        Me.lblInstructions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInstructions.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInstructions.ForeColor = System.Drawing.Color.Sienna
        Me.lblInstructions.Location = New System.Drawing.Point(23, 8)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.Size = New System.Drawing.Size(456, 44)
        Me.lblInstructions.TabIndex = 59
        Me.lblInstructions.Text = "Assign each character slot by clicking repeatedly to select the desired class. Yo" &
    "u can recruit a maximum of 3 of each class."
        Me.lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnFinished
        '
        Me.btnFinished.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFinished.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinished.Location = New System.Drawing.Point(515, 250)
        Me.btnFinished.Name = "btnFinished"
        Me.btnFinished.Size = New System.Drawing.Size(91, 24)
        Me.btnFinished.TabIndex = 60
        Me.btnFinished.Text = "Finished"
        Me.btnFinished.UseVisualStyleBackColor = True
        '
        'rbVanquish
        '
        Me.rbVanquish.AutoSize = True
        Me.rbVanquish.Checked = True
        Me.rbVanquish.Location = New System.Drawing.Point(23, 254)
        Me.rbVanquish.Name = "rbVanquish"
        Me.rbVanquish.Size = New System.Drawing.Size(108, 17)
        Me.rbVanquish.TabIndex = 61
        Me.rbVanquish.TabStop = True
        Me.rbVanquish.Text = "Vanquish Match"
        Me.rbVanquish.UseVisualStyleBackColor = True
        '
        'rbCapture
        '
        Me.rbCapture.AutoSize = True
        Me.rbCapture.Enabled = False
        Me.rbCapture.Location = New System.Drawing.Point(23, 231)
        Me.rbCapture.Name = "rbCapture"
        Me.rbCapture.Size = New System.Drawing.Size(101, 17)
        Me.rbCapture.TabIndex = 62
        Me.rbCapture.Text = "Capture Match"
        Me.rbCapture.UseVisualStyleBackColor = True
        '
        'frmSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(630, 293)
        Me.Controls.Add(Me.rbCapture)
        Me.Controls.Add(Me.rbVanquish)
        Me.Controls.Add(Me.btnFinished)
        Me.Controls.Add(Me.lblInstructions)
        Me.Controls.Add(Me.lblLieutenant3)
        Me.Controls.Add(Me.lblSoldier6)
        Me.Controls.Add(Me.lblSoldier5)
        Me.Controls.Add(Me.lblSoldier4)
        Me.Controls.Add(Me.lblSoldier3)
        Me.Controls.Add(Me.lblSoldier2)
        Me.Controls.Add(Me.lblSoldier1)
        Me.Controls.Add(Me.lblLieutenant2)
        Me.Controls.Add(Me.lblLieutenant1)
        Me.Controls.Add(Me.lblCaptain)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btnRandomize)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetup"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Battle Lines New Game Setup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRandomize As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblCaptain As Label
    Friend WithEvents lblLieutenant1 As Label
    Friend WithEvents lblLieutenant2 As Label
    Friend WithEvents lblSoldier1 As Label
    Friend WithEvents lblSoldier2 As Label
    Friend WithEvents lblSoldier3 As Label
    Friend WithEvents lblSoldier6 As Label
    Friend WithEvents lblSoldier5 As Label
    Friend WithEvents lblSoldier4 As Label
    Friend WithEvents lblLieutenant3 As Label
    Friend WithEvents lblInstructions As Label
    Friend WithEvents btnFinished As Button
    Friend WithEvents rbVanquish As RadioButton
    Friend WithEvents rbCapture As RadioButton
End Class
