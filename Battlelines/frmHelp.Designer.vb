<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHelp
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
        Me.lstHelp = New System.Windows.Forms.ListBox()
        Me.txtHelp = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lstHelp
        '
        Me.lstHelp.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lstHelp.Dock = System.Windows.Forms.DockStyle.Left
        Me.lstHelp.FormattingEnabled = True
        Me.lstHelp.ItemHeight = 17
        Me.lstHelp.Location = New System.Drawing.Point(0, 0)
        Me.lstHelp.MinimumSize = New System.Drawing.Size(159, 395)
        Me.lstHelp.Name = "lstHelp"
        Me.lstHelp.Size = New System.Drawing.Size(159, 428)
        Me.lstHelp.TabIndex = 0
        Me.lstHelp.TabStop = False
        '
        'txtHelp
        '
        Me.txtHelp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHelp.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtHelp.Location = New System.Drawing.Point(165, 0)
        Me.txtHelp.Multiline = True
        Me.txtHelp.Name = "txtHelp"
        Me.txtHelp.ReadOnly = True
        Me.txtHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHelp.Size = New System.Drawing.Size(772, 428)
        Me.txtHelp.TabIndex = 1
        Me.txtHelp.TabStop = False
        '
        'frmHelp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(939, 428)
        Me.Controls.Add(Me.txtHelp)
        Me.Controls.Add(Me.lstHelp)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinimumSize = New System.Drawing.Size(800, 340)
        Me.Name = "frmHelp"
        Me.ShowIcon = False
        Me.Text = "Battle Lines Help"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lstHelp As ListBox
    Friend WithEvents txtHelp As TextBox
End Class
