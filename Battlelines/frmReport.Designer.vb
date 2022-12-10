<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport
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
        Me.pg = New ProGrid.ProGrid()
        Me.SuspendLayout()
        '
        'pg
        '
        Me.pg.AccessibleRole = Nothing
        Me.pg.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.pg.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.pg.AutoSizeMode = Nothing
        Me.pg.BackgroundImageLayout = Nothing
        Me.pg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pg.CausesValidation = False
        Me.pg.CellTextMaxLength = "200"
        Me.pg.ContextMenuStrip = Nothing
        Me.pg.Cursor = System.Windows.Forms.Cursors.Default
        Me.pg.DefaultDateFormat = "M/d/yyyy"
        Me.pg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pg.EnableAddButton = True
        Me.pg.EnableColorAdjustment = False
        Me.pg.EnableDeleteButton = True
        Me.pg.EnableEditHighlighting = True
        Me.pg.EnableEditing = False
        Me.pg.EnableFiltering = True
        Me.pg.EnableFullRowSelection = False
        Me.pg.EnableIdentityHide = True
        Me.pg.EnableInvalidCellHandling = ProGrid.ProGrid.ProGridInvalidMethods.MarkAndShowTip
        Me.pg.EnableOptionsButton = True
        Me.pg.EnableOptionsCopy = True
        Me.pg.EnableOptionsExport = True
        Me.pg.EnableOptionsFind = True
        Me.pg.EnableOptionsPrint = True
        Me.pg.EnableOptionsSort = True
        Me.pg.EnableRefreshButton = True
        Me.pg.LicenseFolder = ""
        Me.pg.Location = New System.Drawing.Point(0, 0)
        Me.pg.Name = "pg"
        Me.pg.PrintTitle = "ProGrid Report"
        Me.pg.Size = New System.Drawing.Size(800, 450)
        Me.pg.TabIndex = 0
        Me.pg.UseWaitCursor = Nothing
        '
        'frmReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pg)
        Me.Name = "frmReport"
        Me.ShowIcon = False
        Me.Text = "Battle Lines Report"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pg As ProGrid.ProGrid
End Class
