Public Class frmHelp

    Dim Hints() As String

    Private Sub frmHelp_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Restore window to last position
        If My.Settings.LastHelpPos.X > 0 Then
            If My.Settings.LastHelpPos.X < My.Computer.Screen.WorkingArea.Width AndAlso My.Settings.LastHelpPos.Y < My.Computer.Screen.WorkingArea.Height Then
                Me.Location = My.Settings.LastHelpPos
            End If
        End If
        If My.Settings.LastHelpSize.Width > 0 Then Me.Size = My.Settings.LastHelpSize

        ' Get Hints
        Hints = My.Resources.Hints.Split(vbCrLf)

        ' Load hints
        For i As Integer = 0 To Hints.Count - 2
            If InStr(Hints(i), "*") > 0 Then lstHelp.Items.Add(Hints(i).Substring(2, Hints(i).Length - 2).Trim)
        Next
        lstHelp.SelectedIndex = 0

    End Sub

    Private Sub lstHelp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstHelp.SelectedIndexChanged
        txtHelp.Text = ""
        For i As Integer = 0 To Hints.Count - 1
            If InStr(Hints(i), "* " + lstHelp.Text) > 0 Then
                For j As Integer = i + 1 To Hints.Count - 2
                    If InStr(Hints(j), "*") > 0 Then Exit Sub
                    txtHelp.Text += Hints(j) + vbCrLf
                Next
            End If
        Next
    End Sub

    Private Sub frmHelp_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then e.Cancel = True
        My.Settings.LastHelpPos = Me.Location
        My.Settings.LastHelpSize = Me.Size
        My.Settings.Save()
        Me.Hide()
    End Sub

End Class