Imports ProGrid.ProGrid
Imports Utility

Public Class frmReport

    Private Sub frmReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pg.Tag = "Characters"
        pg.ButtonQuickSet("Raw", "Actions", "Classes", "Characters")
    End Sub

    Private Sub frmReport_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub frmReport_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        pg_UserClickedButton(pg.Tag)
    End Sub

    Private Sub pg_UserClickedButton(ButtonTag As String) Handles pg.UserClickedButton
        Select Case ButtonTag
            Case "Raw"
                ShowRaw()
            Case "Actions"
                ShowActions()
            Case "Classes"
                ShowClasses()
            Case "Characters"
                ShowCharacters()
        End Select
        pg.Tag = ButtonTag
    End Sub

    Private Sub ShowRaw()
        pg.GridShow(dtReport)
    End Sub

    Private Sub ShowActions()

        Dim dt As New DataTable
        dt.Columns.Add("Action", GetType(System.String))
        dt.Columns.Add("Avg Uses", GetType(System.Int32))
        dt.Columns.Add("% Success", GetType(System.Int32))
        dt.Columns.Add("Avg Damage", GetType(System.Int32))

        Dim rows() As DataRow
        Dim dtActions As DataTable
        Dim Actions As Collection = db.GetDistinct(dtReport, "Action")
        For Each Action In Actions

            ' Get all actions of that type
            rows = dtReport.Select("[Action] = " + fmt.q(Action))
            If rows.Count = 0 Then Continue For
            dtActions = rows.CopyToDataTable

            Dim totUses As Integer = dtActions.Compute("SUM([Targets])", "")
            Dim totSuccesses As Integer = dtActions.Compute("SUM([Success])", "")
            Dim totDamage As Integer = dtActions.Compute("SUM([Damage])", "")
            Dim totChars As Integer = db.GetDistinct(dtActions, "ID").Count
            dt.Rows.Add(Action, totUses / totChars, totSuccesses / dtActions.Rows.Count * 100, totDamage / totChars)
        Next
        dt.AcceptChanges()
        dt = db.SortDataTable(dt, "Action")

        pg.GridShow(dt)

    End Sub

    Private Sub ShowClasses()

        Dim dt As New DataTable
        dt.Columns.Add("Class", GetType(System.String))
        dt.Columns.Add("Rank", GetType(System.Int32))
        dt.Columns.Add("Avg Damage", GetType(System.Int32))

        Dim rows() As DataRow
        Dim dtActions As DataTable
        Dim Classes As Collection = db.GetDistinct(dtReport, "Class")
        For Each Title In Classes

            ' Get all actions for that class
            rows = dtReport.Select("[Class] = " + fmt.q(Title))
            If rows.Count = 0 Then Continue For
            dtActions = rows.CopyToDataTable

            Dim totDamage As Integer = dtActions.Compute("SUM([Damage])", "")
            Dim totChars As Integer = db.GetDistinct(dtActions, "ID").Count
            dt.Rows.Add(Title, dtActions.Rows(0)("Rank"), totDamage / totChars)
        Next
        dt.AcceptChanges()
        dt = db.SortDataTable(dt, "Class")

        pg.GridShow(dt)

    End Sub

    Private Sub ShowCharacters()

        Dim dt As New DataTable
        dt.Columns.Add("ID", GetType(System.Int32))
        dt.Columns.Add("Class", GetType(System.String))
        dt.Columns.Add("Rank", GetType(System.Int32))
        dt.Columns.Add("Action", GetType(System.String))
        dt.Columns.Add("Uses", GetType(System.Int32))
        dt.Columns.Add("Successes", GetType(System.Int32))
        dt.Columns.Add("Total Damage", GetType(System.Int32))

        Dim rows() As DataRow
        Dim rows2() As DataRow
        Dim Chars As DataTable
        Dim Actions As Collection
        Dim Moves As DataTable
        For i As Integer = 1 To 20

            ' Get all report records for the character
            rows = dtReport.Select("[ID] = " + i.ToString)
            If rows.Count = 0 Then Continue For
            Chars = rows.CopyToDataTable

            ' Get all actions for the character
            Actions = db.GetDistinct(Chars, "Action")
            If Actions.Count = 0 Then Continue For

            ' Report on each action
            For Each Action As String In Actions

                ' Get all actions of that type
                rows2 = Chars.Select("[Action] = " + fmt.q(Action))
                If rows2.Count = 0 Then Continue For
                Moves = rows2.CopyToDataTable

                ' Add to report
                Dim count As Integer = Moves.Compute("SUM([Targets])", "")
                Dim success As Integer = Moves.Compute("SUM([Success])", "")
                Dim total As Integer = Moves.Compute("SUM([Damage])", "")
                dt.Rows.Add(rows2(0)("ID"), rows2(0)("Archetype"), rows2(0)("Rank"), Action, count, success, total)

            Next
        Next
        dt.AcceptChanges()
        dt = db.SortDataTable(dt, "[ID], [Action]")

        pg.GridShow(dt)

    End Sub

    Private Sub pg_UserClickedRefresh() Handles pg.UserClickedRefresh
        pg_UserClickedButton(pg.Tag)
    End Sub

End Class