Imports Utility

Public Class frmSetup

    Dim dr1() As DataRow
    Dim dr2() As DataRow
    Dim dr3() As DataRow

    Dim crNext As Cursor

    Private Sub frmSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblInstructions.Text = "Select a class for each slot by clicking repeatedly." + vbCrLf + "You can recruit a maximum of 3 team members from each class."

        dr3 = dtClasses.Select("[Rank] = 3")
        dr2 = dtClasses.Select("[Rank] = 2")
        dr1 = dtClasses.Select("[Rank] = 1")

        LoadRandomClasses()
        dtParty.Rows.Clear()

    End Sub

    Private Sub btnRandomize_Click(sender As Object, e As EventArgs) Handles btnRandomize.Click
        LoadRandomClasses()
    End Sub

    Private Sub btnFinished_Click(sender As Object, e As EventArgs) Handles btnFinished.Click

        ' Create the party
        CreateParty()

        ' Check if more than three of any one class have been selected
        If dtParty.Select("[Archetype] = 'Fighter'").Count > 3 Then
            MessageBox.Show("You cannot have more than 3 Fighters in your party.", "Cannot Proceed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If dtParty.Select("[Archetype] = 'Monk'").Count > 3 Then
            MessageBox.Show("You cannot have more than 3 Monks in your party.", "Cannot Proceed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If dtParty.Select("[Archetype] = 'Rogue'").Count > 3 Then
            MessageBox.Show("You cannot have more than 3 Rogues in your party.", "Cannot Proceed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If dtParty.Select("[Archetype] = 'Cleric'").Count > 3 Then
            MessageBox.Show("You cannot have more than 3 Cleric in your party.", "Cannot Proceed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If dtParty.Select("[Archetype] = 'Ranger'").Count > 3 Then
            MessageBox.Show("You cannot have more than 3 Rangers in your party.", "Cannot Proceed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If dtParty.Select("[Archetype] = 'Wizard'").Count > 3 Then
            MessageBox.Show("You cannot have more than 3 Wizards in your party.", "Cannot Proceed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Exit the dialog
        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub lblCaptain_MouseDown(sender As Object, e As MouseEventArgs) Handles lblCaptain.MouseDown
        Dim n As Integer = sender.tag
        Dim nn As Integer
        If e.Button = MouseButtons.Left Then
            nn = n + 1
            If nn > dr3.Count - 1 Then nn = 0
        ElseIf e.Button = MouseButtons.Right Then
            nn = n - 1
            If nn < 0 Then nn = dr3.Count - 1
        End If
        sender.text = dr3(nn)("Archetype") + " " + dr3(nn)("Class")
        sender.tag = nn
    End Sub

    Private Sub lblLieutenant_MouseDown(sender As Object, e As MouseEventArgs) Handles lblLieutenant1.MouseDown, lblLieutenant2.MouseDown, lblLieutenant3.MouseDown
        Dim n As Integer = sender.tag
        Dim nn As Integer
        If e.Button = MouseButtons.Left Then
            nn = n + 1
            If nn > dr2.Count - 1 Then nn = 0
        ElseIf e.Button = MouseButtons.Right Then
            nn = n - 1
            If nn < 0 Then nn = dr2.Count - 1
        End If
        sender.text = dr2(nn)("Archetype") + " " + dr2(nn)("Class")
        sender.tag = nn
    End Sub

    Private Sub lblSoldier_MouseDown(sender As Object, e As MouseEventArgs) Handles lblSoldier1.MouseDown, lblSoldier2.MouseDown, lblSoldier3.MouseDown, lblSoldier4.MouseDown, lblSoldier5.MouseDown, lblSoldier6.MouseDown
        Dim n As Integer = sender.tag
        Dim nn As Integer
        If e.Button = MouseButtons.Left Then
            nn = n + 1
            If nn > dr1.Count - 1 Then nn = 0
        ElseIf e.Button = MouseButtons.Right Then
            nn = n - 1
            If nn < 0 Then nn = dr1.Count - 1
        End If
        sender.text = dr1(nn)("Archetype") + " " + dr1(nn)("Class")
        sender.tag = nn
    End Sub

    Private Sub LoadRandomClasses()
        Dim dt As DataTable = CreateRandomParty("Party")
        dt = db.SortDataTable(dt, "[Rank] DESC")

        lblCaptain.Text = GetTitle(dt.Rows(0))
        lblCaptain.Tag = GetClassPos(dt.Rows(0)("Archetype"))

        lblLieutenant1.Text = GetTitle(dt.Rows(1))
        lblLieutenant1.Tag = GetClassPos(dt.Rows(1)("Archetype"))
        lblLieutenant2.Text = GetTitle(dt.Rows(2))
        lblLieutenant2.Tag = GetClassPos(dt.Rows(2)("Archetype"))
        lblLieutenant3.Text = GetTitle(dt.Rows(3))
        lblLieutenant3.Tag = GetClassPos(dt.Rows(3)("Archetype"))

        lblSoldier1.Text = GetTitle(dt.Rows(4))
        lblSoldier1.Tag = GetClassPos(dt.Rows(4)("Archetype"))
        lblSoldier2.Text = GetTitle(dt.Rows(5))
        lblSoldier2.Tag = GetClassPos(dt.Rows(5)("Archetype"))
        lblSoldier3.Text = GetTitle(dt.Rows(6))
        lblSoldier3.Tag = GetClassPos(dt.Rows(6)("Archetype"))
        lblSoldier4.Text = GetTitle(dt.Rows(7))
        lblSoldier4.Tag = GetClassPos(dt.Rows(7)("Archetype"))
        lblSoldier5.Text = GetTitle(dt.Rows(8))
        lblSoldier5.Tag = GetClassPos(dt.Rows(8)("Archetype"))
        lblSoldier6.Text = GetTitle(dt.Rows(9))
        lblSoldier6.Tag = GetClassPos(dt.Rows(9)("Archetype"))

    End Sub

    Private Function GetClassPos(Archetype As String) As Integer
        Select Case Archetype
            Case "Fighter"
                Return 0
            Case "Monk"
                Return 1
            Case "Rogue"
                Return 2
            Case "Cleric"
                Return 3
            Case "Ranger"
                Return 4
            Case "Wizard"
                Return 5
        End Select
    End Function

    Private Sub CreateParty()
        Dim dt As DataTable = dtClasses.Clone

        ' Import leader
        dt.ImportRow(dr3(lblCaptain.Tag))

        ' Import lieutenants
        dt.ImportRow(dr2(lblLieutenant1.Tag))
        dt.ImportRow(dr2(lblLieutenant2.Tag))
        dt.ImportRow(dr2(lblLieutenant3.Tag))

        ' Import soldiers
        dt.ImportRow(dr1(lblSoldier1.Tag))
        dt.ImportRow(dr1(lblSoldier2.Tag))
        dt.ImportRow(dr1(lblSoldier3.Tag))
        dt.ImportRow(dr1(lblSoldier4.Tag))
        dt.ImportRow(dr1(lblSoldier5.Tag))
        dt.ImportRow(dr1(lblSoldier6.Tag))

        ' Update calculated values
        For i As Integer = 0 To 9
            dt.Rows(i)("ID") = i + 1
            dt.Rows(i)("Side") = "Party"
            dt.Rows(i)("CurRow") = -1
            dt.Rows(i)("CurCol") = -1
            InitChar(dt.Rows(i))
        Next

        ' Randomize positions
        dtParty = RandomizeTable(dt)
    End Sub

    Private Sub lblLieutenant3_MouseMove(sender As Object, e As MouseEventArgs) Handles lblSoldier6.MouseMove, lblSoldier5.MouseMove, lblSoldier4.MouseMove, lblSoldier3.MouseMove, lblSoldier2.MouseMove, lblSoldier1.MouseMove, lblLieutenant3.MouseMove, lblLieutenant2.MouseMove, lblLieutenant1.MouseMove, lblCaptain.MouseMove
        sender.cursor = Cursors.Hand
    End Sub

    Private Sub lblLieutenant3_MouseLeave(sender As Object, e As EventArgs) Handles lblSoldier6.MouseLeave, lblSoldier5.MouseLeave, lblSoldier4.MouseLeave, lblSoldier3.MouseLeave, lblSoldier2.MouseLeave, lblSoldier1.MouseLeave, lblLieutenant3.MouseLeave, lblLieutenant2.MouseLeave, lblLieutenant1.MouseLeave, lblCaptain.MouseLeave
        sender.cursor = DefaultCursor
    End Sub

    Private Sub frmSetup_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If dtParty.Rows.Count = 0 Then CreateParty()
        Me.DialogResult = DialogResult.OK
    End Sub

End Class