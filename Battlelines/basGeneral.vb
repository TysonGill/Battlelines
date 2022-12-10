Imports Utility

Module basGeneral

    Friend dtParty As New DataTable
    Friend dtEnemy As New DataTable
    Friend dtPartyGY As New DataTable
    Friend dtEnemyGY As New DataTable
    Friend dtTargets As New DataTable
    Friend dtHexes As New DataTable
    Friend dtAllies As New DataTable
    Friend dtConditions As New DataTable
    Friend dtReport As New DataTable
    Friend CurCharRow As DataRow
    Friend CurSide As String
    Friend TurnDelay As Integer = 100 '500
    Friend Turn As Integer
    Friend Round As Integer
    Friend Winner As String = ""

    Friend ConnectionString As String = "Data Source=tcp:s14.winhost.com;Initial Catalog=DB_130918_bitistry;User ID=DB_130918_bitistry_user;Password=bplease;Integrated Security=False;"

    Friend Enum SpecState
        Unavailable
        Available
        Active
    End Enum
    Friend SpecStat(3) As SpecState

    Dim RandGen As System.Random = New System.Random()

    Friend Sub ReportAdd(CharData As DataRow, Action As String, Optional Success As Boolean = True, Optional Damage As Integer = 0, Optional Targets As Integer = 1)
        If Action = "Move" Then Exit Sub
        Dim Move As Integer = 1
        If dtReport.Rows.Count >= 1 Then
            Dim LastRow As DataRow = dtReport.Rows(dtReport.Rows.Count - 1)
            If LastRow("Turn") = Turn Then Move = LastRow("Move") + 1
        End If
        dtReport.Rows.Add(Now, CharData("ID"), Turn, Move, CharData("Side"), CharData("Archetype"), GetTitle(CharData), CharData("Rank"), CharData("CurRow"), CharData("CurCol"), Action, IIf(Success, 1, 0), Damage, Targets)
    End Sub

    Friend Function Rand(Optional Min As Integer = 1, Optional Max As Integer = 100, Optional BestOf As Integer = 1) As Integer
        Dim r As Integer
        Dim CurRand As Integer = RandGen.Next(Min, Max + 1)
        For i As Integer = 1 To BestOf - 1
            r = RandGen.Next(Min, Max + 1)
            If r > CurRand Then CurRand = r
        Next
        Return CurRand
    End Function

    ' Check if a given number is odd or even
    Friend Function IsOdd(num As Integer) As Boolean
        If num Mod 2 = 0 Then Return False Else Return True
    End Function

    ' Randomize record order in a datatable
    Friend Function RandomizeTable(dt As DataTable) As DataTable
        Dim dtr As DataTable = dt.Clone
        Dim index As Integer
        For i = 0 To dt.Rows.Count - 1
            index = Rand(0, dt.Rows.Count - 1)
            dtr.ImportRow(dt.Rows(index))
            dt.Rows.RemoveAt(index)
            dt.AcceptChanges()
        Next
        Return dtr
    End Function

    Dim dsw As New Stopwatch
    Friend Sub Wait(ms As Double, Optional NonBlocking As Boolean = True)
        If ms = 0 Then Exit Sub
        Dim ticks As Long = CLng(ms * 10000) ' 1ms-10K ticks
        dsw.Restart()
        Do
            If NonBlocking Then Application.DoEvents()
        Loop While dsw.ElapsedTicks <= ticks
        dsw.Stop()
    End Sub

End Module
