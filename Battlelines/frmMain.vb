Imports Utility

'http://bitistry.com/Battlelines/Battlelines.htm

Public Class frmMain

    Dim IsDeploymentPhase As Boolean
    Dim CharSelectEnabled As Boolean

    Dim MouseRow As Integer
    Dim MouseCol As Integer

    Dim crSwap As Cursor
    Dim crCharSelect As Cursor
    Dim crFootsteps As Cursor

    Dim crSword As Cursor
    Dim crFist As Cursor
    Dim crWarhammer As Cursor
    Dim crDagger As Cursor
    Dim crAxe As Cursor
    Dim crBow As Cursor
    Dim crWand As Cursor

    Dim crRaiseShield As Cursor
    Dim crCharge As Cursor
    Dim crCleave As Cursor

    Dim crRenew As Cursor
    Dim crEvasive As Cursor
    Dim crDisorient As Cursor

    Dim crSlow As Cursor
    Dim crBlind As Cursor
    Dim crWeakness As Cursor

    Dim crHeal As Cursor
    Dim crHarm As Cursor
    Dim crRevive As Cursor

    Dim crHobble As Cursor
    Dim crPrecisionShot As Cursor
    Dim crArrowVolley As Cursor

    Dim crDefensiveAura As Cursor
    Dim crTeleport As Cursor
    Dim crFireball As Cursor

    Dim BlockClick As Boolean = False
    Dim AutoOn As Boolean = False

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor

            BlockClick = True

            ' Restore window to last position
            If My.Settings.LastMainPos.X > 0 Then
                If My.Settings.LastMainPos.X < My.Computer.Screen.WorkingArea.Width AndAlso My.Settings.LastMainPos.Y < My.Computer.Screen.WorkingArea.Height Then
                    Me.Location = My.Settings.LastMainPos
                End If
            End If
            If My.Settings.LastMainSize.Width > 0 Then Me.Size = My.Settings.LastMainSize

            ' Create custom cursors
            crSwap = New Cursor(My.Resources.CrPointer.GetHicon)
            crCharSelect = New Cursor(My.Resources.CrPointer.GetHicon)
            crFootsteps = New Cursor(My.Resources.CrFootsteps.GetHicon)

            crSword = New Cursor(My.Resources.CrSword.GetHicon)
            crFist = New Cursor(My.Resources.CrFist.GetHicon)
            crWarhammer = New Cursor(My.Resources.CrWarhammer.GetHicon)
            crDagger = New Cursor(My.Resources.CrDagger.GetHicon)
            crAxe = New Cursor(My.Resources.CrAxe.GetHicon)
            crBow = New Cursor(My.Resources.CrBow.GetHicon)
            crWand = New Cursor(My.Resources.CrWand.GetHicon)

            crRaiseShield = New Cursor(My.Resources.CrRaiseShield.GetHicon)
            crCharge = New Cursor(My.Resources.CrShield.GetHicon)
            crCleave = New Cursor(My.Resources.CrCleave.GetHicon)

            crRenew = New Cursor(My.Resources.CrRenew.GetHicon)
            crEvasive = New Cursor(My.Resources.CrEvasive.GetHicon)
            crDisorient = New Cursor(My.Resources.CrDisoriented.GetHicon)

            crSlow = New Cursor(My.Resources.CrSlow.GetHicon)
            crBlind = New Cursor(My.Resources.CrBlindness.GetHicon)
            crWeakness = New Cursor(My.Resources.CrWeaken.GetHicon)

            crHeal = New Cursor(My.Resources.CrHeal.GetHicon)
            crHarm = New Cursor(My.Resources.CrHarm.GetHicon)
            crRevive = New Cursor(My.Resources.CrRevive.GetHicon)

            crHobble = New Cursor(My.Resources.CrHobbled.GetHicon)
            crPrecisionShot = New Cursor(My.Resources.CrPrecisionShot.GetHicon)
            crArrowVolley = New Cursor(My.Resources.CrArrowVolley.GetHicon)

            crDefensiveAura = New Cursor(My.Resources.CrAura.GetHicon)
            crTeleport = New Cursor(My.Resources.CrTeleport.GetHicon)
            crFireball = New Cursor(My.Resources.CrFireball.GetHicon)

            ' Create a targets and allies tables for mapping purposes
            dtTargets.Columns.Add("Distance", GetType(Integer))
            dtTargets.Columns.Add("HexRow", GetType(Integer))
            dtTargets.Columns.Add("HexCol", GetType(Integer))
            dtTargets.Columns.Add("Life Left", GetType(Integer))
            dtTargets.Columns.Add("Hit Points Down", GetType(Integer))
            dtTargets.Columns.Add("Moves Left", GetType(Integer))
            dtTargets.Columns.Add("CharRow", GetType(DataRow))
            dtAllies = dtTargets.Clone

            ' Create a table of hexes for mapping purposes
            dtHexes.Columns.Add("Distance", GetType(Integer))
            dtHexes.Columns.Add("Row", GetType(Integer))
            dtHexes.Columns.Add("Col", GetType(Integer))
            dtHexes.Columns.Add("Side", GetType(String))
            dtHexes.Columns.Add("Moves Max", GetType(Integer))
            dtHexes.Columns.Add("Life Left", GetType(Integer))
            dtHexes.Columns.Add("CharRow", GetType(DataRow))
            dtHexes.Columns.Add("AlliesNear", GetType(Integer))
            dtHexes.Columns.Add("EnemiesNear", GetType(Integer))
            dtHexes.Columns.Add("Message", GetType(Integer))

            ' Create a table of conditions
            dtConditions.Columns.Add("Condition", GetType(String))

            ' Create a report table
            dtReport.Columns.Add("Time", GetType(DateTime))
            dtReport.Columns.Add("ID", GetType(Integer))
            dtReport.Columns.Add("Turn", GetType(Integer))
            dtReport.Columns.Add("Move", GetType(Integer))
            dtReport.Columns.Add("Side", GetType(String))
            dtReport.Columns.Add("Archetype", GetType(String))
            dtReport.Columns.Add("Class", GetType(String))
            dtReport.Columns.Add("Rank", GetType(String))
            dtReport.Columns.Add("Row", GetType(Integer))
            dtReport.Columns.Add("Col", GetType(Integer))
            dtReport.Columns.Add("Action", GetType(String))
            dtReport.Columns.Add("Success", GetType(Integer))
            dtReport.Columns.Add("Damage", GetType(Integer))
            dtReport.Columns.Add("Targets", GetType(Integer))

            ' Open a data connection
            Dim dq As New clsDataQuery(ConnectionString)

            ' Draw map
            picMap.Width = (xHexes * HexWidth4 * 3) + (xMargin * 2) + HexWidth4 + 2
            picMap.Height = (yHexes * HexHeight) + (yMargin * 2) + HexHeight2 + 3 + yHexes
            CreateMap()

            ' Locate controls
            lblLog1.Left = picMap.Right + 20
            Me.Height = picMap.Bottom + 50
            Me.Width = lblLog1.Right + 30
            lblCopyright.Top = picMap.Bottom - lblCopyright.Height
            lblAuto.Top = lblCopyright.Top - lblAuto.Height - 12
            lblMove.Top = lblAuto.Top - 34
            lblEndTurn.Top = lblMove.Top - 34
            lblNewGame.Top = lblEndTurn.Top - 34
            lblPrompt.Top = lblSpec3.Bottom + 20
            lblPrompt.Height = lblNewGame.Top - lblPrompt.Top - 12

            ' Create parties
            CreateClassDefs()

            ' Start a new game
            Me.Show()
            NewGame(True)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        Finally
            BlockClick = False
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub NewGame(Optional Startup As Boolean = False)
        Try
            Cursor = Cursors.WaitCursor

            ' Clear the previous winner
            Winner = ""

            ' Clear the previous logs
            ClearLog()
            dtReport.Rows.Clear()

            ' Clear any previous characters from grid
            For row = 1 To yHexes
                For col = 1 To xHexes
                    If Hex(row, col).side <> "" Then
                        HexColor(row, col)
                        Hex(row, col).side = ""
                        Hex(row, col).CharRow = Nothing
                    End If
                Next
            Next

            ' Create and show enemy team
            dtEnemy = CreateRandomParty("Enemy")
            For i As Integer = 0 To dtEnemy.Rows.Count - 1
                dtEnemy.Rows(i)("CurRow") = i + 2
                dtEnemy.Rows(i)("CurCol") = xHexes - 1
                Hex(dtEnemy.Rows(i)("CurRow"), dtEnemy.Rows(i)("CurCol")).side = "Enemy"
                Hex(dtEnemy.Rows(i)("CurRow"), dtEnemy.Rows(i)("CurCol")).CharRow = dtEnemy.Rows(i)
            Next
            DisplaySide(dtEnemy)

            ' Create and show party team
            dtParty = CreateRandomParty("Party")
            If Not Startup Then
                frmSetup.Location = New Point(Me.Left + 12, Me.Top + 26)
                If frmSetup.ShowDialog(Me) <> DialogResult.OK Then Exit Sub
            End If
            For i As Integer = 0 To dtParty.Rows.Count - 1
                dtParty.Rows(i)("CurRow") = i + 2
                dtParty.Rows(i)("CurCol") = 2
                Hex(dtParty.Rows(i)("CurRow"), dtParty.Rows(i)("CurCol")).side = "Party"
                Hex(dtParty.Rows(i)("CurRow"), dtParty.Rows(i)("CurCol")).CharRow = dtParty.Rows(i)
            Next
            DisplaySide(dtParty)

            ' Create tables to hold unconscious characters
            dtPartyGY = dtParty.Clone
            dtEnemyGY = dtEnemy.Clone

            ' Initialize new game
            CurSide = "Party"
            CurCharRow = PickNextChar(CurSide)
            Round = 0
            Turn = 0

            ' Kick off the deployment phase
            SetDeploymentPhase(True)
            UpdateCharStats(CurCharRow)
            HexShow(CurCharRow("CurRow"), CurCharRow("CurCol"), cCurrent)

            ' Show quick start
            ShowQuickStart()

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub SetDeploymentPhase(State As Boolean)
        IsDeploymentPhase = State
        lblEndTurn.Visible = True
        lblMove.Visible = Not IsDeploymentPhase
        lblAuto.Visible = Not IsDeploymentPhase
        lblSpec1.Visible = Not IsDeploymentPhase
        pb1.Visible = Not IsDeploymentPhase
        lblSpec2.Visible = Not IsDeploymentPhase
        pb2.Visible = Not IsDeploymentPhase
        lblSpec3.Visible = Not IsDeploymentPhase
        pb3.Visible = Not IsDeploymentPhase
        If IsDeploymentPhase Then
            lblTurn.Text = "Deploy"
            lblTurn.ForeColor = cParty
            lblPrompt.Top = pnlChar.Bottom + 12
        Else
            ClearLog()
            lblPrompt.Top = lblSpec3.Bottom + 20
        End If
        ShowPrompt()
    End Sub

    Dim LastHexUnder As Point
    Private Sub picMap_MouseMove(sender As Object, e As MouseEventArgs) Handles picMap.MouseMove
        Try
            If BlockClick OrElse AutoOn OrElse Winner <> "" Then Exit Sub

            Dim HexUnder As Point = GetHexFromCoords(e.X, e.Y)
            Dim row As Integer = HexUnder.Y
            Dim col As Integer = HexUnder.X
            If row < 1 OrElse col < 1 OrElse row > yHexes OrElse col > xHexes Then
                LastHexUnder.X = 0
                LastHexUnder.Y = 0
                picMap.Cursor = Cursors.Default
                Exit Sub
            End If
            LastHexUnder.X = row
            LastHexUnder.Y = col

            If CurCharRow Is Nothing Then Exit Sub

            Dim s As String = ""
            Dim FromRow As Integer = CurCharRow("CurRow")
            Dim FromCol As Integer = CurCharRow("CurCol")
            Dim IsSelf As Boolean = (FromRow = row AndAlso FromCol = col)
            Dim IsEmpty As Boolean = (Hex(row, col).side = "")
            Dim IsFriendly As Boolean = Not IsEmpty AndAlso Not IsSelf AndAlso (Hex(FromRow, FromCol).side = Hex(row, col).side)
            Dim IsEnemy As Boolean = Not IsEmpty AndAlso Not IsSelf AndAlso (Hex(FromRow, FromCol).side <> Hex(row, col).side)
            Dim Dist As Integer = CalcHexDist(FromRow, FromCol, row, col)

            ' Set cursor 
            picMap.Cursor = Cursors.Default

            ' See if the user is swapping characters
            If IsDeploymentPhase Then
                If (IsFriendly OrElse IsEmpty) AndAlso col <= 4 Then
                    picMap.Cursor = crSwap
                End If
                GoTo ShowPopup
            End If

            ' See if the user is selecting (and not healing) a different character
            If CharSelectEnabled AndAlso IsFriendly AndAlso Not (CurCharRow("Archetype") = "Cleric" AndAlso SpecStat(1) = SpecState.Active) Then
                picMap.Cursor = crCharSelect
                GoTo ShowPopup
            End If

            If SpecStat(1) = SpecState.Active Then
                ' Special 1
                Select Case CurCharRow("Archetype")
                    Case "Fighter"
                        ' Raise Shield
                        If IsSelf Then picMap.Cursor = crRaiseShield
                    Case "Monk"
                        ' Evasive
                        If IsSelf Then picMap.Cursor = crEvasive
                    Case "Cleric"
                        ' Heal
                        If IsSelf OrElse IsFriendly Then
                            If Dist <= CurCharRow("Spec1 Range") Then picMap.Cursor = crHeal
                        End If
                    Case "Ranger"
                        ' Hobble
                        If IsEnemy AndAlso Dist = 1 Then picMap.Cursor = crHobble
                    Case "Rogue"
                        ' Slow
                        If IsEnemy AndAlso Dist = 1 Then picMap.Cursor = crSlow
                    Case "Wizard"
                        ' Defensive Aura
                        If IsSelf Then picMap.Cursor = crDefensiveAura
                End Select

            ElseIf SpecStat(2) = SpecState.Active Then
                ' Special 2
                Select Case CurCharRow("Archetype")
                    Case "Fighter"
                        ' Charge
                        If IsEnemy AndAlso Dist >= 3 AndAlso Dist <= CurCharRow("Spec2 Range") AndAlso GetHexMap(CurSide, row, col, 3, CurCharRow("Spec2 Range"), False, False, True).Rows.Count > 0 Then picMap.Cursor = crCharge
                    Case "Monk"
                        ' Renew
                        If IsSelf Then picMap.Cursor = crRenew
                    Case "Cleric"
                        ' Harm
                        If IsEnemy AndAlso Dist <= CurCharRow("Spec2 Range") Then picMap.Cursor = crHarm
                    Case "Ranger"
                        If IsEnemy AndAlso Dist <= CurCharRow("Spec2 Range") Then picMap.Cursor = crPrecisionShot
                    Case "Rogue"
                        ' Blind
                        If IsEnemy AndAlso Dist = 1 Then picMap.Cursor = crBlind
                    Case "Wizard"
                        ' Teleport
                        If IsEmpty AndAlso Dist >= 3 AndAlso Dist <= CurCharRow("Spec2 Range") Then picMap.Cursor = crTeleport
                End Select

            ElseIf SpecStat(3) = SpecState.Active Then
                ' Special 3
                Select Case CurCharRow("Archetype")
                    Case "Fighter"
                        ' Cleave
                        If IsEnemy AndAlso Dist = 1 Then picMap.Cursor = crCleave
                    Case "Monk"
                        ' Disorient
                        If IsEnemy AndAlso Dist = 1 Then picMap.Cursor = crDisorient
                    Case "Cleric"
                        ' Revive
                        If IsEmpty AndAlso Dist = 1 AndAlso IIf(CurSide = "Party", dtPartyGY.Rows.Count, dtEnemyGY.Rows.Count) > 0 Then picMap.Cursor = crRevive
                    Case "Rogue"
                        ' Weakness
                        If IsEnemy AndAlso Dist = 1 Then picMap.Cursor = crWeakness
                    Case "Ranger"
                        ' Arrow Volley
                        If Dist <= CurCharRow("Spec3 Range") Then picMap.Cursor = crArrowVolley
                    Case "Wizard"
                        ' Fireball
                        If Dist <= CurCharRow("Spec3 Range") Then picMap.Cursor = crFireball
                End Select

            ElseIf IsEnemy AndAlso CurCharRow("Attacks Left") >= 1 AndAlso Dist <= CurCharRow("Range Max") Then
                ' Attack
                Select Case CurCharRow("Archetype")
                    Case "Fighter"
                        picMap.Cursor = crSword
                    Case "Monk"
                        picMap.Cursor = crFist
                    Case "Cleric"
                        picMap.Cursor = crWarhammer
                    Case "Rogue"
                        picMap.Cursor = crDagger
                    Case "Ranger"
                        If Dist = 1 Then picMap.Cursor = crAxe Else picMap.Cursor = crBow
                    Case "Wizard"
                        picMap.Cursor = crWand
                End Select
            ElseIf IsEmpty AndAlso CurCharRow("Moves Left") >= 1 AndAlso Dist = 1 Then
                ' Movement
                picMap.Cursor = crFootsteps
            End If

ShowPopup:
            ' Clear the popup
            If Hex(row, col).CharRow Is Nothing Then
                txtPop.Visible = False
                Exit Sub
            End If

            ' Display popup details
            s = GetCharSummary(Hex(row, col).CharRow)
            ShowMapPopup(row, col, s)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Private Sub picMap_MouseDown(sender As Object, e As MouseEventArgs) Handles picMap.MouseDown
        Try

            If BlockClick OrElse AutoOn OrElse Winner <> "" Then Exit Sub
            If e.Button <> MouseButtons.Left AndAlso e.Button <> MouseButtons.Right Then Exit Sub
            If e.Button = MouseButtons.Right AndAlso Not IsDeploymentPhase Then
                RotateSpec()
                Exit Sub
            End If

            BlockClick = True

            Dim row As Integer = LastHexUnder.X
            Dim col As Integer = LastHexUnder.Y
            If row < 1 Or row > yHexes Or col < 1 Or col > xHexes Then Exit Sub

            ' Perform action
            Select Case picMap.Cursor

                ' Deployment mode
                Case crSwap
                    If e.Button = MouseButtons.Left Then
                        If Hex(row, col).side <> "" Then
                            HexShow(CurCharRow("CurRow"), CurCharRow("CurCol"))
                            CurCharRow = Hex(row, col).CharRow
                        End If
                    Else
                        If Hex(row, col).side = "" Then
                            DoMove(CurCharRow, row, col)
                        Else
                            DoSwap(CurCharRow, Hex(row, col).CharRow)
                        End If
                    End If

                    ' Selection mode
                Case crCharSelect
                    If e.Button = MouseButtons.Left Then
                        HexShow(CurCharRow("CurRow"), CurCharRow("CurCol"))
                        CurCharRow = Hex(row, col).CharRow
                    End If

                    ' Standard
                Case crFootsteps
                    DoMove(CurCharRow, row, col)
                    CurCharRow("Moves Left") -= 1
                Case crSword, crFist, crWarhammer, crDagger, crAxe, crWand, crBow
                    DoMelee(CurCharRow, row, col)

                    ' Fighter
                Case crRaiseShield
                    DoRaiseShield(CurCharRow)
                Case crCharge
                    DoCharge(CurCharRow, row, col)
                Case crCleave
                    DoCleave(CurCharRow, row, col)

                    ' Monk
                Case crEvasive
                    DoEvasive(CurCharRow)
                Case crRenew
                    DoRenew(CurCharRow)
                Case crDisorient
                    DoDisorient(CurCharRow, row, col)

                    ' Rogue
                Case crSlow
                    DoSlow(CurCharRow, row, col)
                Case crBlind
                    DoBlind(CurCharRow, row, col)
                Case crWeakness
                    DoWeaken(CurCharRow, row, col)

                    ' Cleric
                Case crHeal
                    DoHeal(CurCharRow, row, col)
                Case crHarm
                    DoHarm(CurCharRow, row, col)
                Case crRevive
                    DoRevive(CurCharRow, row, col)

                    ' Ranger
                Case crHobble
                    DoHobble(CurCharRow, row, col)
                Case crPrecisionShot
                    DoPrecisionShot(CurCharRow, row, col)
                Case crArrowVolley
                    DoArrowVolley(CurCharRow, row, col)

                    ' Wizard
                Case crDefensiveAura
                    DoDefensiveAura(CurCharRow)
                Case crTeleport
                    DoTeleport(CurCharRow, row, col)
                Case crFireball
                    DoFireball(CurCharRow, row, col)

                Case Else
                    Exit Sub
            End Select

            HideSpecPopup()
            UpdateCharStats(CurCharRow)
            HexShow(CurCharRow("CurRow"), CurCharRow("CurCol"), cCurrent)

            ' Exit Character Select Mode if an action has been taken
            If CharSelectEnabled AndAlso picMap.Cursor <> crCharSelect Then
                CharSelectEnabled = False
                ShowPrompt()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        Finally
            BlockClick = False
        End Try
    End Sub

    Private Sub picMap_MouseLeave(sender As Object, e As EventArgs) Handles picMap.MouseLeave
        Try
            LastHexUnder.X = 0
            LastHexUnder.Y = 0
            txtPop.Visible = False
            txtPop.Parent = Me
            picMap.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Private Sub Advance()
        Try
            If Winner <> "" Then Exit Sub

            ' Clear currently selected hex
            HexShow(CurCharRow("CurRow"), CurCharRow("CurCol"))
            CurCharRow("LastRound") = Round

            ' Advance to next turn/round
            Turn += 1
            If IsOdd(Turn) Then Round += 1

            ' Get the new current character
            If CurSide = "Party" Then
                CurSide = "Enemy"
                CurCharRow = PickNextChar(CurSide)
            Else
                CurSide = "Party"
                CurCharRow = PickNextChar(CurSide)
            End If
            lblTurn.ForeColor = IIf(CurSide = "Party", cParty, cEnemy)
            lblTurn.Text = "Round " + Round.ToString

            ' Reset round-based counters for all characters
            If IsOdd(Turn) Then ResetAllTurnCounters()

            If IsOdd(Turn) Then
                ' Update the current character UI
                CharSelectEnabled = True
                HideSpecPopup()
                UpdateCharStats(CurCharRow)
                HexShow(CurCharRow("CurRow"), CurCharRow("CurCol"), cCurrent)
            Else
                DoTurn()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Private Sub ResetAllTurnCounters()
        For i As Integer = 0 To dtParty.Rows.Count - 1
            ResetTurnCounters(dtParty.Rows(i))
        Next
        For i As Integer = 0 To dtEnemy.Rows.Count - 1
            ResetTurnCounters(dtEnemy.Rows(i))
        Next
    End Sub

    Private Sub ResetTurnCounters(CharData As DataRow)

        ' Decrement all turn-based cooldowns
        If CharData("Spec1 Cooldown Left") > 0 Then CharData("Spec1 Cooldown Left") -= 1
        If CharData("Spec2 Cooldown Left") > 0 Then CharData("Spec2 Cooldown Left") -= 1
        If CharData("Spec3 Cooldown Left") > 0 Then CharData("Spec3 Cooldown Left") -= 1

        ' Increment any active counters
        If CharData("Shield Raised") > 0 Then CharData("Shield Raised") += 1
        If CharData("Evasive Stance") > 0 Then CharData("Evasive Stance") += 1
        If CharData("Protective Aura") > 0 Then CharData("Protective Aura") += 1
        If CharData("Disoriented") > 0 Then CharData("Disoriented") += 1
        If CharData("Slowed") > 0 Then CharData("Slowed") += 1
        If CharData("Blinded") > 0 Then CharData("Blinded") += 1
        If CharData("Weakened") > 0 Then CharData("Weakened") += 1
        If CharData("Hobbled") > 0 Then CharData("Hobbled") += 1

        ' See if any active counters break
        Dim BreakChance As Integer = 1
        If Rand() <= ((CharData("Shield Raised") - 1) * BreakChance) Then
            CharData("Shield Raised") = 0
            UpdateLog(" The " + GetTitle(CharData) + " lets his shield down." + vbCrLf)
        End If
        If Rand() <= ((CharData("Evasive Stance") - 1) * BreakChance) Then
            CharData("Evasive Stance") = 0
            UpdateLog(" The " + GetTitle(CharData) + " drops out of his defensive stance." + vbCrLf)
        End If
        If Rand() <= ((CharData("Protective Aura") - 1) * BreakChance) Then
            CharData("Protective Aura") = 0
            UpdateLog(" The " + GetTitle(CharData) + " loses focus on his protective aura." + vbCrLf)
        End If
        If Rand() <= ((CharData("Disoriented") - 1) * BreakChance) Then
            CharData("Disoriented") = 0
            UpdateLog("The " + GetTitle(CharData) + " snaps out of his disorientation." + vbCrLf)
        End If
        If Rand() <= ((CharData("Slowed") - 1) * BreakChance) Then
            CharData("Slowed") = 0
            UpdateLog("The " + GetTitle(CharData) + " shrugs off his slowness." + vbCrLf)
        End If
        If Rand() <= ((CharData("Blinded") - 1) * BreakChance) Then
            CharData("Blinded") = 0
            UpdateLog("The " + GetTitle(CharData) + " clears his eyes of blindness." + vbCrLf)
        End If
        If Rand() <= ((CharData("Weakened") - 1) * BreakChance) Then
            CharData("Weakened") = 0
            UpdateLog("The " + GetTitle(CharData) + " recovers his strength." + vbCrLf)
        End If
        If Rand() <= ((CharData("Hobbled") - 1) * BreakChance) Then
            CharData("Hobbled") = 0
            UpdateLog("The " + GetTitle(CharData) + " regains his footing." + vbCrLf)
        End If

        ' Reset counters for current character adjusted for debuffs
        CharData("Attacks Left") = CharData("Attacks Max")
        If CharData("Slowed") > 0 Then CharData("Attacks Left") -= CharData("Slowed Amount")
        If CharData("Attacks Left") < 0 Then CharData("Attacks Left") = 0
        CharData("Moves Left") = CharData("Moves Max")
        If CharData("Hobbled") > 0 Then CharData("Moves Left") -= CharData("Hobbled Amount")
        If CharData("Moves Left") < 0 Then CharData("Moves Left") = 0
        HexRefresh(CharData("CurRow"), CharData("CurCol"))

    End Sub

    Private Sub DoTurn()
        Try
            BlockClick = True
            lblMove.BackColor = cButtonActive
            Select Case CurCharRow("Archetype")
                Case "Fighter"
                    DoTurn_Fighter()
                Case "Monk"
                    DoTurn_Monk()
                Case "Cleric"
                    DoTurn_Cleric()
                Case "Rogue"
                    DoTurn_Rogue()
                Case "Ranger"
                    DoTurn_Ranger()
                Case "Wizard"
                    DoTurn_Wizard()
            End Select
            lblMove.BackColor = cButtonReady

            Advance()

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        Finally
            BlockClick = False
        End Try
    End Sub

#Region "Support"

    Private Sub ShowMapPopup(row As Integer, col As Integer, s As String)
        If txtPop.Tag = New Point(row, col) AndAlso txtPop.Visible Then Exit Sub
        txtPop.Text = s
        Dim x As Integer = picMap.Left + Hex(row, col).x - txtPop.Width - 4
        Dim y As Integer = Hex(row, col).y - 20
        If y < 2 Then y = 2
        txtPop.Left = x
        txtPop.Top = y
        If txtPop.Top > Me.Height - txtPop.Height - 44 Then
            txtPop.Top = Me.Height - txtPop.Height - 44
        End If
        txtPop.BringToFront()
        txtPop.Visible = True
        txtPop.Tag = New Point(row, col)
    End Sub

    Private Sub ShowSpecPopup(sender As Object, s As String, Optional nHeight As Integer = 120)
        txtSpecPop.Text = s
        If sender.Parent Is pnlChar Then
            txtSpecPop.Left = pnlChar.Right + 6
            txtSpecPop.Top = sender.Top + pnlChar.Top
        Else
            txtSpecPop.Left = sender.Right + 6
            txtSpecPop.Top = sender.Top
        End If
        txtSpecPop.Height = nHeight
        txtSpecPop.Width = 260
        If txtSpecPop.Bottom > Me.Height - 48 Then
            txtSpecPop.Top = Me.Height - txtSpecPop.Height - 48
        End If
        txtSpecPop.BringToFront()
        txtSpecPop.Visible = True
    End Sub

    Private Sub ClearLog()
        lblLog1.Text = ""
        lblLog2.Text = ""
        lblLog3.Text = ""
        lblLog4.Text = ""
        lblLog5.Text = ""
        lblLog6.Text = ""
        lblLog7.Text = ""
        lblLog8.Text = ""
        lblLog9.Text = ""
        lblLog10.Text = ""
        lblLog11.Text = ""
        lblLog12.Text = ""
        lblLog13.Text = ""
        lblLog14.Text = ""
        lblLog15.Text = ""
        lblLog16.Text = ""
        lblLog17.Text = ""
        lblLog18.Text = ""
        lblLog19.Text = ""
        lblLog20.Text = ""
        lblLog2.Left = lblLog1.Left
        lblLog3.Left = lblLog1.Left
        lblLog4.Left = lblLog1.Left
        lblLog5.Left = lblLog1.Left
        lblLog6.Left = lblLog1.Left
        lblLog7.Left = lblLog1.Left
        lblLog8.Left = lblLog1.Left
        lblLog9.Left = lblLog1.Left
        lblLog10.Left = lblLog1.Left
        lblLog11.Left = lblLog1.Left
        lblLog12.Left = lblLog1.Left
        lblLog13.Left = lblLog1.Left
        lblLog14.Left = lblLog1.Left
        lblLog15.Left = lblLog1.Left
        lblLog16.Left = lblLog1.Left
        lblLog17.Left = lblLog1.Left
        lblLog18.Left = lblLog1.Left
        lblLog19.Left = lblLog1.Left
        lblLog20.Left = lblLog1.Left
        lblLog2.Visible = False
        lblLog3.Visible = False
        lblLog4.Visible = False
        lblLog5.Visible = False
        lblLog6.Visible = False
        lblLog7.Visible = False
        lblLog8.Visible = False
        lblLog9.Visible = False
        lblLog10.Visible = False
        lblLog11.Visible = False
        lblLog12.Visible = False
        lblLog13.Visible = False
        lblLog14.Visible = False
        lblLog15.Visible = False
        lblLog16.Visible = False
        lblLog17.Visible = False
        lblLog18.Visible = False
        lblLog19.Visible = False
        lblLog20.Visible = False
        lblLog1.BorderStyle = BorderStyle.None
        lblLog2.BorderStyle = BorderStyle.None
        lblLog3.BorderStyle = BorderStyle.None
        lblLog4.BorderStyle = BorderStyle.None
        lblLog5.BorderStyle = BorderStyle.None
        lblLog6.BorderStyle = BorderStyle.None
        lblLog7.BorderStyle = BorderStyle.None
        lblLog8.BorderStyle = BorderStyle.None
        lblLog9.BorderStyle = BorderStyle.None
        lblLog10.BorderStyle = BorderStyle.None
        lblLog11.BorderStyle = BorderStyle.None
        lblLog12.BorderStyle = BorderStyle.None
        lblLog13.BorderStyle = BorderStyle.None
        lblLog14.BorderStyle = BorderStyle.None
        lblLog15.BorderStyle = BorderStyle.None
        lblLog16.BorderStyle = BorderStyle.None
        lblLog17.BorderStyle = BorderStyle.None
        lblLog18.BorderStyle = BorderStyle.None
        lblLog19.BorderStyle = BorderStyle.None
        lblLog20.BorderStyle = BorderStyle.None
    End Sub

    Friend Sub UpdateLog(msg As String, Optional TextSize As Integer = 9)

        If msg = "REDRAW" Then GoTo RedrawOnly
        lblLog1.Font = New Font(lblLog1.Font.Name, TextSize)
        If Not lblLog1.Text.EndsWith(vbCrLf) Then
            If CurSide = "Party" Then lblLog1.ForeColor = cParty Else lblLog1.ForeColor = cEnemy
            If lblLog1.Text = "" Then msg = Round.ToString + ") " + msg
            lblLog1.Text += msg
            lblLog1.Tag = Turn
        Else
            ' Scroll the log and add the new msg to the top
            lblLog20.ForeColor = lblLog19.ForeColor
            lblLog20.Text = lblLog19.Text
            lblLog20.Tag = lblLog19.Tag
            lblLog20.Visible = (lblLog20.Text <> "")
            lblLog19.ForeColor = lblLog18.ForeColor
            lblLog19.Text = lblLog18.Text
            lblLog19.Tag = lblLog18.Tag
            lblLog19.Visible = (lblLog19.Text <> "")
            lblLog18.ForeColor = lblLog17.ForeColor
            lblLog18.Text = lblLog17.Text
            lblLog18.Tag = lblLog17.Tag
            lblLog18.Visible = (lblLog18.Text <> "")
            lblLog17.ForeColor = lblLog16.ForeColor
            lblLog17.Text = lblLog16.Text
            lblLog17.Tag = lblLog16.Tag
            lblLog17.Visible = (lblLog17.Text <> "")
            lblLog16.ForeColor = lblLog15.ForeColor
            lblLog16.Text = lblLog15.Text
            lblLog16.Tag = lblLog15.Tag
            lblLog16.Visible = (lblLog16.Text <> "")
            lblLog15.ForeColor = lblLog14.ForeColor
            lblLog15.Text = lblLog14.Text
            lblLog15.Tag = lblLog14.Tag
            lblLog15.Visible = (lblLog15.Text <> "")
            lblLog14.ForeColor = lblLog13.ForeColor
            lblLog14.Text = lblLog13.Text
            lblLog14.Tag = lblLog13.Tag
            lblLog14.Visible = (lblLog14.Text <> "")
            lblLog13.ForeColor = lblLog12.ForeColor
            lblLog13.Text = lblLog12.Text
            lblLog13.Tag = lblLog12.Tag
            lblLog13.Visible = (lblLog13.Text <> "")
            lblLog12.ForeColor = lblLog11.ForeColor
            lblLog12.Text = lblLog11.Text
            lblLog12.Tag = lblLog11.Tag
            lblLog12.Visible = (lblLog12.Text <> "")
            lblLog11.ForeColor = lblLog10.ForeColor
            lblLog11.Text = lblLog10.Text
            lblLog11.Tag = lblLog10.Tag
            lblLog11.Visible = (lblLog11.Text <> "")
            lblLog10.ForeColor = lblLog9.ForeColor
            lblLog10.Text = lblLog9.Text
            lblLog10.Tag = lblLog9.Tag
            lblLog10.Visible = (lblLog10.Text <> "")
            lblLog9.ForeColor = lblLog8.ForeColor
            lblLog9.Text = lblLog8.Text
            lblLog9.Tag = lblLog8.Tag
            lblLog9.Visible = (lblLog9.Text <> "")
            lblLog8.ForeColor = lblLog7.ForeColor
            lblLog8.Text = lblLog7.Text
            lblLog8.Tag = lblLog7.Tag
            lblLog8.Visible = (lblLog8.Text <> "")
            lblLog7.ForeColor = lblLog6.ForeColor
            lblLog7.Text = lblLog6.Text
            lblLog7.Tag = lblLog6.Tag
            lblLog7.Visible = (lblLog7.Text <> "")
            lblLog6.ForeColor = lblLog5.ForeColor
            lblLog6.Text = lblLog5.Text
            lblLog6.Tag = lblLog5.Tag
            lblLog6.Visible = (lblLog6.Text <> "")
            lblLog5.ForeColor = lblLog4.ForeColor
            lblLog5.Text = lblLog4.Text
            lblLog5.Tag = lblLog4.Tag
            lblLog5.Visible = (lblLog5.Text <> "")
            lblLog4.ForeColor = lblLog3.ForeColor
            lblLog4.Text = lblLog3.Text
            lblLog4.Tag = lblLog3.Tag
            lblLog4.Visible = (lblLog4.Text <> "")
            lblLog3.ForeColor = lblLog2.ForeColor
            lblLog3.Text = lblLog2.Text
            lblLog3.Tag = lblLog2.Tag
            lblLog3.Visible = (lblLog3.Text <> "")
            lblLog2.ForeColor = lblLog1.ForeColor
            lblLog2.Text = lblLog1.Text
            lblLog2.Tag = lblLog1.Tag
            lblLog2.Visible = (lblLog2.Text <> "")
            If CurSide = "Party" Then lblLog1.ForeColor = cParty Else lblLog1.ForeColor = cEnemy
            lblLog1.Text = Round.ToString + ") " + msg
            lblLog1.Tag = Turn
        End If
        Me.Refresh()
        Application.DoEvents()

RedrawOnly:
        ' Reposition scrolled messages
        Dim TurnSpacing As Integer = 16
        lblLog2.Top = lblLog1.Bottom + IIf(lblLog2.Tag = lblLog1.Tag, 6, TurnSpacing)
        lblLog3.Top = lblLog2.Bottom + IIf(lblLog3.Tag = lblLog2.Tag, 6, TurnSpacing)
        lblLog4.Top = lblLog3.Bottom + IIf(lblLog4.Tag = lblLog3.Tag, 6, TurnSpacing)
        lblLog5.Top = lblLog4.Bottom + IIf(lblLog5.Tag = lblLog4.Tag, 6, TurnSpacing)
        lblLog6.Top = lblLog5.Bottom + IIf(lblLog6.Tag = lblLog5.Tag, 6, TurnSpacing)
        lblLog7.Top = lblLog6.Bottom + IIf(lblLog7.Tag = lblLog6.Tag, 6, TurnSpacing)
        lblLog8.Top = lblLog7.Bottom + IIf(lblLog8.Tag = lblLog7.Tag, 6, TurnSpacing)
        lblLog9.Top = lblLog8.Bottom + IIf(lblLog9.Tag = lblLog8.Tag, 6, TurnSpacing)
        lblLog10.Top = lblLog9.Bottom + IIf(lblLog10.Tag = lblLog9.Tag, 6, TurnSpacing)
        lblLog11.Top = lblLog10.Bottom + IIf(lblLog11.Tag = lblLog10.Tag, 6, TurnSpacing)
        lblLog12.Top = lblLog11.Bottom + IIf(lblLog12.Tag = lblLog11.Tag, 6, TurnSpacing)
        lblLog13.Top = lblLog12.Bottom + IIf(lblLog13.Tag = lblLog12.Tag, 6, TurnSpacing)
        lblLog14.Top = lblLog13.Bottom + IIf(lblLog14.Tag = lblLog13.Tag, 6, TurnSpacing)
        lblLog15.Top = lblLog14.Bottom + IIf(lblLog15.Tag = lblLog14.Tag, 6, TurnSpacing)
        lblLog16.Top = lblLog15.Bottom + IIf(lblLog16.Tag = lblLog15.Tag, 6, TurnSpacing)
        lblLog17.Top = lblLog16.Bottom + IIf(lblLog17.Tag = lblLog16.Tag, 6, TurnSpacing)
        lblLog18.Top = lblLog17.Bottom + IIf(lblLog18.Tag = lblLog17.Tag, 6, TurnSpacing)
        lblLog19.Top = lblLog18.Bottom + IIf(lblLog19.Tag = lblLog18.Tag, 6, TurnSpacing)
        lblLog20.Top = lblLog19.Bottom + IIf(lblLog20.Tag = lblLog19.Tag, 6, TurnSpacing)
        Me.Refresh()
        Application.DoEvents()

        ' Hide messages that have scrolled past bottom
        lblLog20.Visible = (lblLog20.Bottom <= picMap.Bottom)
        lblLog19.Visible = (lblLog19.Bottom <= picMap.Bottom)
        lblLog18.Visible = (lblLog18.Bottom <= picMap.Bottom)
        lblLog17.Visible = (lblLog17.Bottom <= picMap.Bottom)
        lblLog16.Visible = (lblLog16.Bottom <= picMap.Bottom)
        lblLog15.Visible = (lblLog15.Bottom <= picMap.Bottom)
        lblLog14.Visible = (lblLog14.Bottom <= picMap.Bottom)
        lblLog13.Visible = (lblLog13.Bottom <= picMap.Bottom)
        lblLog12.Visible = (lblLog12.Bottom <= picMap.Bottom)

    End Sub

    Private Sub AutoSet(AutoState As Boolean)

        If IsDeploymentPhase Then Exit Sub

        If AutoState Then

            lblAuto.Text = "Halt Auto (A)"
            lblPrompt.Text = ""
            lblAuto.BackColor = cButtonActive
            txtSpecPop.Visible = False
            txtPop.Visible = False
            lblNewGame.Visible = False
            lblEndTurn.Visible = False
            lblMove.Visible = False
            BlockClick = True
            AutoOn = True
            Do
                DoTurn()
                Application.DoEvents()
            Loop While AutoOn AndAlso Winner = ""
            lblAuto.Text = "Start Auto (A)"
            lblAuto.BackColor = cButtonReady
            lblNewGame.Visible = True
            lblEndTurn.Visible = True
            lblMove.Visible = True
            BlockClick = False
            lblAuto.Enabled = True
        Else
            AutoOn = False
            lblAuto.Text = "Halting..."
            lblAuto.Enabled = False
        End If

    End Sub

    Friend Sub ShowWinner()

        ' Turn off auto mode if running
        If AutoOn Then AutoSet(False)
        lblEndTurn.Visible = False
        lblMove.Visible = False
        lblAuto.Visible = False

        ' Show winner in log
        Dim msg As String
        Dim score As Integer = 300 - Turn - (10 * (10 - dtParty.Rows.Count))
        If score < 0 Then score = 0
        If Winner = "Enemy" Then
            msg = "Your opponent has won." + vbCrLf
        ElseIf score > My.Settings.LastHighScore Then
            My.Settings.LastHighScore = score
            msg = "You have won with a new high score of " + score.ToString + "!" + vbCrLf
        Else
            msg = "You have won with a score of " + score.ToString + "!" + vbCrLf
        End If
        UpdateLog(msg, 12)

    End Sub

    Friend Sub UpdateCharStats(CharData As DataRow)

        ' Update character summary
        UpdateCharUI(CharData)

        ' Update special buttons
        UpdateSpecs(CharData)

        ' Update turn buttons
        If CharData("Moves Left") = 0 AndAlso CharData("Attacks Left") = 0 Then lblEndTurn.BackColor = cButtonActive Else lblEndTurn.BackColor = cButtonReady
        If CharData("Attacks Left") = 0 Then picMap.Cursor = Cursors.Default

    End Sub

    Friend Sub UpdateCharUI(CharData As DataRow)
        lblCharName.Text = GetTitle(CharData)
        lblMaxHP.Text = "Eff HP: " + CharData("Life Left").ToString + " of " + CharData("Life Max").ToString
        lblMaxMove.Text = "Movement: " + CharData("Moves Left").ToString + " of " + CharData("Moves Max").ToString
        lblMaxAttacks.Text = "Attacks: " + CharData("Attacks Left").ToString + " of " + CharData("Attacks Max").ToString
        lblMaxRange.Text = "Attack Range: " + CharData("Range Max").ToString
        lblMaxDamage.Text = "Attack Damage: " + CharData("Melee Hit Max").ToString + " / " + IIf(CharData("Ranged Hit Max") = 0, "-", CharData("Ranged Hit Max")).ToString
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        ' Turn off auto mode if running
        If AutoOn Then AutoSet(False)

        If frmH IsNot Nothing Then frmH.Close()
        If frmR IsNot Nothing Then frmR.Close()
        My.Settings.LastMainPos = Me.Location
        My.Settings.LastMainSize = Me.Size
        My.Settings.Save()

    End Sub

    Private Sub lblMove_Click(sender As Object, e As EventArgs) Handles lblMove.Click
        AutoTurn()
    End Sub

    Private Sub AutoTurn()
        If BlockClick OrElse IsDeploymentPhase OrElse AutoOn OrElse Winner <> "" Then Exit Sub
        lblMove.BackColor = cButtonActive
        Wait(10)
        DoTurn()
        lblMove.BackColor = cButtonReady
    End Sub

    Private Sub lblAuto_Click(sender As Object, e As EventArgs) Handles lblAuto.Click
        AutoSet(Not AutoOn)
    End Sub

    Private Sub lblEndTurn_Click(sender As Object, e As EventArgs) Handles lblEndTurn.Click
        AutoEnd()
    End Sub

    Private Sub AutoEnd()
        If AutoOn Then Exit Sub
        lblEndTurn.BackColor = cButtonActive
        Wait(300)
        If IsDeploymentPhase Then
            SetDeploymentPhase(False)
            CurSide = "Enemy"
        End If
        Advance()
        lblEndTurn.BackColor = cButtonReady
    End Sub

    Private Sub lblNewGame_Click(sender As Object, e As EventArgs) Handles lblNewGame.Click
        If Winner = "" Then
            If MessageBox.Show("Starting a new game will terminate any match currently in progress." + vbCrLf + vbCrLf + "Do you wish to proceed?", "New Game Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> vbYes Then Exit Sub
        End If
        sender.backcolor = cButtonActive
        NewGame()
        sender.backcolor = cButtonReady
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.KeyCode.A
                AutoSet(Not AutoOn)
            Case Keys.KeyCode.X
                AutoEnd()
            Case e.KeyCode.Space
                AutoTurn()
            Case Keys.KeyCode.D1
                DoSpecClick(1)
            Case Keys.KeyCode.D2
                DoSpecClick(2)
            Case Keys.KeyCode.D3
                DoSpecClick(3)
        End Select
    End Sub

    Private Sub lblSpec1_MouseMove(sender As Object, e As MouseEventArgs) Handles lblSpec1.MouseMove, lblSpec2.MouseMove, lblSpec3.MouseMove
        If BlockClick OrElse AutoOn OrElse txtSpecPop.Visible OrElse CurCharRow Is Nothing OrElse Winner <> "" Then Exit Sub
        Dim s As String
        Select Case sender.tag
            Case 1
                s = GetSpecialDesc(CurCharRow("Spec1 Name"))
            Case 2
                s = GetSpecialDesc(CurCharRow("Spec2 Name"))
            Case 3
                s = GetSpecialDesc(CurCharRow("Spec3 Name"))
        End Select
        ShowSpecPopup(sender, s)
    End Sub

    Private Sub HideSpecPopup()
        txtSpecPop.Visible = False
        txtSpecPop.Parent = Me
        txtSpecPop.Cursor = Cursors.Default
    End Sub

    Private Sub lblSpec_Click(sender As Object, e As EventArgs) Handles lblSpec1.Click, lblSpec2.Click, lblSpec3.Click
        DoSpecClick(sender.tag)
    End Sub

    Private Sub DoSpecClick(SpecNum As Integer)

        If BlockClick OrElse AutoOn OrElse IsDeploymentPhase OrElse Winner <> "" Then Exit Sub

        If SpecStat(SpecNum) = SpecState.Unavailable Then Exit Sub

        If SpecStat(SpecNum) = SpecState.Active Then
            SpecStat(SpecNum) = SpecState.Available
        Else
            If SpecStat(1) = SpecState.Active Then SpecStat(1) = SpecState.Available
            If SpecStat(2) = SpecState.Active Then SpecStat(2) = SpecState.Available
            If SpecStat(3) = SpecState.Active Then SpecStat(3) = SpecState.Available
            SpecStat(SpecNum) = SpecState.Active
        End If
        ShowSpecStates()

    End Sub

    Private Sub UpdateSpecs(CharRow As DataRow)
        Try
            ' Assign names
            lblSpec1.Text = CharRow("Spec1 Name")
            lblSpec2.Text = CharRow("Spec2 Name")
            lblSpec3.Text = CharRow("Spec3 Name")

            ' Assign labels and states
            Select Case CharRow("Archetype")
                Case "Fighter"
                    ' Raise Shield
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec1 Cooldown Left") = 0 AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(1) = SpecState.Available
                    Else
                        SpecStat(1) = SpecState.Unavailable
                    End If

                    ' Charge
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec2 Cooldown Left") = 0 AndAlso CharRow("Moves Left") = CharRow("Moves Max") AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(2) = SpecState.Available
                    Else
                        SpecStat(2) = SpecState.Unavailable
                    End If

                    ' Cleave
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec3 Cooldown Left") = 0 AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(3) = SpecState.Available
                    Else
                        SpecStat(3) = SpecState.Unavailable
                    End If

                Case "Monk"
                    ' Evasive
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec1 Cooldown Left") = 0 AndAlso CharRow("Moves Left") = CharRow("Moves Max") Then
                        SpecStat(1) = SpecState.Available
                    Else
                        SpecStat(1) = SpecState.Unavailable
                    End If

                    ' Renew
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec2 Cooldown Left") = 0 AndAlso CharRow("Attacks Left") = CharRow("Attacks Max") Then
                        SpecStat(2) = SpecState.Available
                    Else
                        SpecStat(2) = SpecState.Unavailable
                    End If

                    ' Disorient
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec3 Cooldown Left") = 0 AndAlso CharRow("Attacks Left") = CharRow("Attacks Max") Then
                        SpecStat(3) = SpecState.Available
                    Else
                        SpecStat(3) = SpecState.Unavailable
                    End If

                Case "Rogue"
                    ' Slow
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec1 Cooldown Left") = 0 AndAlso CharRow("Attacks Left") >= 1 Then
                        SpecStat(1) = SpecState.Available
                    Else
                        SpecStat(1) = SpecState.Unavailable
                    End If

                    ' Blind
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec2 Cooldown Left") = 0 AndAlso CharRow("Attacks Left") >= 1 Then
                        SpecStat(2) = SpecState.Available
                    Else
                        SpecStat(2) = SpecState.Unavailable
                    End If

                    ' Weaken
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec3 Cooldown Left") = 0 AndAlso CharRow("Attacks Left") >= 1 Then
                        SpecStat(3) = SpecState.Available
                    Else
                        SpecStat(3) = SpecState.Unavailable
                    End If

                Case "Cleric"
                    ' Heal
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec1 Cooldown Left") = 0 AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(1) = SpecState.Available
                    Else
                        SpecStat(1) = SpecState.Unavailable
                    End If

                    ' Cure
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec2 Cooldown Left") = 0 AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(2) = SpecState.Available
                    Else
                        SpecStat(2) = SpecState.Unavailable
                    End If

                    ' Revive
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec3 Cooldown Left") = 0 AndAlso CharRow("Moves Left") = CharRow("Moves Max") AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(3) = SpecState.Available
                    Else
                        SpecStat(3) = SpecState.Unavailable
                    End If

                Case "Ranger"
                    ' Hobble
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec1 Cooldown Left") = 0 AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(1) = SpecState.Available
                    Else
                        SpecStat(1) = SpecState.Unavailable
                    End If

                    ' Percision Shot
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec2 Cooldown Left") = 0 AndAlso CharRow("Moves Left") = CharRow("Moves Max") AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(2) = SpecState.Available
                    Else
                        SpecStat(2) = SpecState.Unavailable
                    End If

                    ' Arrow Volley
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec3 Cooldown Left") = 0 AndAlso CharRow("Moves Left") = CharRow("Moves Max") AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(3) = SpecState.Available
                    Else
                        SpecStat(3) = SpecState.Unavailable
                    End If

                Case "Wizard"
                    ' Reflective Aura
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec1 Cooldown Left") = 0 AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(1) = SpecState.Available
                    Else
                        SpecStat(1) = SpecState.Unavailable
                    End If

                    ' Teleport
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec2 Cooldown Left") = 0 AndAlso CharRow("Moves Left") = CharRow("Moves Max") AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(2) = SpecState.Available
                    Else
                        SpecStat(2) = SpecState.Unavailable
                    End If

                    ' Fireball
                    If CharRow("Disoriented") = 0 AndAlso CharRow("Spec3 Cooldown Left") = 0 AndAlso CharRow("Attacks Max") = CharRow("Attacks Left") Then
                        SpecStat(3) = SpecState.Available
                    Else
                        SpecStat(3) = SpecState.Unavailable
                    End If

            End Select
            ShowSpecStates()

            ' Update progress bar popup messages
            pb1.Value = (CharRow("Spec1 Cooldown") - CharRow("Spec1 Cooldown Left")) / CharRow("Spec1 Cooldown") * 100
            If CharRow("Disoriented") > 0 Then
                pb1.Tag = "You are currently Disoriented and cannot use your special abilities."
            ElseIf CharRow("Spec1 Cooldown Left") = 0 AndAlso SpecStat(1) = SpecState.Available Then
                pb1.Tag = "This ability is off cooldown and ready to use. You will need to wait for " + CharRow("Spec1 Cooldown").ToString + " rounds after using it."
            ElseIf CharRow("Spec1 Cooldown Left") = 0 AndAlso SpecStat(1) = SpecState.Unavailable Then
                pb1.Tag = "This ability is off cooldown but is disabled because you have already used your movement or attack for this round."
            Else
                pb1.Tag = "This ability is on cooldown for " + CharRow("Spec1 Cooldown Left").ToString + " of " + CharRow("Spec1 Cooldown").ToString + " rounds."
            End If
            pb2.Value = (CharRow("Spec2 Cooldown") - CharRow("Spec2 Cooldown Left")) / CharRow("Spec2 Cooldown") * 100
            If CharRow("Disoriented") > 0 Then
                pb2.Tag = "You are currently Disoriented and cannot use your special abilities."
            ElseIf CharRow("Spec2 Cooldown Left") = 0 AndAlso SpecStat(1) = SpecState.Available Then
                pb2.Tag = "This ability is off cooldown and ready to use. You will need to wait for " + CharRow("Spec1 Cooldown").ToString + " rounds after using it."
            ElseIf CharRow("Spec2 Cooldown Left") = 0 AndAlso SpecStat(1) = SpecState.Unavailable Then
                pb2.Tag = "This ability is off cooldown but disabled because you have already used your movement or attack for this round."
            Else
                pb2.Tag = "This ability is on cooldown for " + CharRow("Spec2 Cooldown Left").ToString + " of " + CharRow("Spec2 Cooldown").ToString + " rounds."
            End If
            pb3.Value = (CharRow("spec3 Cooldown") - CharRow("spec3 Cooldown Left")) / CharRow("spec3 Cooldown") * 100
            If CharRow("Disoriented") > 0 Then
                pb3.Tag = "You are currently Disoriented and cannot use your special abilities."
            ElseIf CharRow("spec3 Cooldown Left") = 0 AndAlso SpecStat(1) = SpecState.Available Then
                pb3.Tag = "This ability is off cooldown and ready to use. You will need to wait for " + CharRow("Spec1 Cooldown").ToString + " rounds after using it."
            ElseIf CharRow("spec3 Cooldown Left") = 0 AndAlso SpecStat(1) = SpecState.Unavailable Then
                pb3.Tag = "This ability is off cooldown but disabled because you have already used your movement or attack for this round."
            Else
                pb3.Tag = "This ability is on cooldown for " + CharRow("spec3 Cooldown Left").ToString + " of " + CharRow("spec3 Cooldown").ToString + " rounds."
            End If

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub ShowSpecStates()

        Select Case SpecStat(1)
            Case SpecState.Unavailable
                lblSpec1.BackColor = Color.Transparent
            Case SpecState.Available
                lblSpec1.BackColor = cButtonReady
            Case SpecState.Active
                lblSpec1.BackColor = cButtonActive
        End Select
        Select Case SpecStat(2)
            Case SpecState.Unavailable
                lblSpec2.BackColor = Color.Transparent
            Case SpecState.Available
                lblSpec2.BackColor = cButtonReady
            Case SpecState.Active
                lblSpec2.BackColor = cButtonActive
        End Select
        Select Case SpecStat(3)
            Case SpecState.Unavailable
                lblSpec3.BackColor = Color.Transparent
            Case SpecState.Available
                lblSpec3.BackColor = cButtonReady
            Case SpecState.Active
                lblSpec3.BackColor = cButtonActive
        End Select
        ShowPrompt()

    End Sub

    Private Sub RotateSpec()

        ' Just exit if all buttons are on cooldown
        If SpecStat(1) = SpecState.Unavailable AndAlso SpecStat(2) = SpecState.Unavailable AndAlso SpecStat(3) = SpecState.Unavailable Then Exit Sub

        If SpecStat(1) = SpecState.Active Then
            If SpecStat(2) = SpecState.Available Then
                DoSpecClick(2)
            ElseIf SpecStat(3) = SpecState.Available Then
                DoSpecClick(3)
            Else
                DoSpecClick(1)
            End If
        ElseIf SpecStat(2) = SpecState.Active Then
            If SpecStat(3) = SpecState.Available Then
                DoSpecClick(3)
            Else
                DoSpecClick(2)
            End If
        ElseIf SpecStat(3) = SpecState.Active Then
            DoSpecClick(3)
        Else
            ' None active, click first available button
            If SpecStat(1) = SpecState.Available Then
                DoSpecClick(1)
            ElseIf SpecStat(2) = SpecState.Available Then
                DoSpecClick(2)
            Else
                DoSpecClick(3)
            End If
        End If

    End Sub

    Private Sub ShowPrompt()

        If AutoOn Then Exit Sub
        If Winner <> "" Then
            lblPrompt.Text = "The match is over." + vbCrLf + vbCrLf + "Click New Game to start another!"
            Exit Sub
        End If
        If IsDeploymentPhase Then
            lblPrompt.Text = "Left-click to select a character." + vbCrLf + vbCrLf + "Right-click a location in the first 4 columns to move or swap positions." + vbCrLf + vbCrLf + "Click End Turn or press X when ready to start the match."
            Exit Sub
        End If
        If CharSelectEnabled Then
            lblPrompt.Text = "Left-click on allies to select a character for this turn. Any other action locks in that character."
            Exit Sub
        End If

        lblPrompt.Text = ""
        Select Case CurCharRow("Archetype")
            Case "Fighter"
                If SpecStat(1) = SpecState.Active Then lblPrompt.Text = "Click yourself to Raise Shield and gain damage mitigation."
                If SpecStat(2) = SpecState.Active Then lblPrompt.Text = "Click an enemy in range to Charge forward and bash with your shield."
                If SpecStat(3) = SpecState.Active Then lblPrompt.Text = "Click a nearby enemy to strike and Cleave through all adjacent positions."
            Case "Monk"
                If SpecStat(1) = SpecState.Active Then lblPrompt.Text = "Click yourself to assume an Evasive Stance making you harder to hit."
                If SpecStat(2) = SpecState.Active Then lblPrompt.Text = "Click yourself to attempt to heal wounds and cure detriments."
                If SpecStat(3) = SpecState.Active Then lblPrompt.Text = "Click a nearby enemy to attempt to Disorient them."
            Case "Rogue"
                If SpecStat(1) = SpecState.Active Then lblPrompt.Text = "Click a nearby enemy to attempt to Slow them."
                If SpecStat(2) = SpecState.Active Then lblPrompt.Text = "Click a nearby enemy to attempt to Blind them."
                If SpecStat(3) = SpecState.Active Then lblPrompt.Text = "Click a nnearby enemy to attempt to Weaken them."
            Case "Cleric"
                If SpecStat(1) = SpecState.Active Then lblPrompt.Text = "Click an ally in range to attempt to Heal their wounds and cure detriments."
                If SpecStat(2) = SpecState.Active Then lblPrompt.Text = "Click an enemy in range to attempt to Harm them."
                If SpecStat(3) = SpecState.Active Then lblPrompt.Text = "Click on an empty nearby location to attempt to revive a fallen ally."
            Case "Ranger"
                If SpecStat(1) = SpecState.Active Then lblPrompt.Text = "Click a nearby enemy to attempt to attempt to Hobble them."
                If SpecStat(2) = SpecState.Active Then lblPrompt.Text = "Click an enemy in range to attempt to hit them with a Precision Shot."
                If SpecStat(3) = SpecState.Active Then lblPrompt.Text = "Click any location in range to fire an Arrow Volley, hitting all surrounding locations."
            Case "Wizard"
                If SpecStat(1) = SpecState.Active Then lblPrompt.Text = "Click yourself to raise a Defensive Aura that absorbs all incoming damage."
                If SpecStat(2) = SpecState.Active Then lblPrompt.Text = "Click any open location in range to attempt to Teleport to that position."
                If SpecStat(3) = SpecState.Active Then lblPrompt.Text = "Click any location in range to launch a Fireball damaging all in and around that position."
        End Select

    End Sub

    Private Sub lblSpec_MouseLeave(sender As Object, e As EventArgs) Handles lblSpec1.MouseLeave, lblSpec2.MouseLeave, lblSpec3.MouseLeave, lblMaxHP.MouseLeave, lblMaxMove.MouseLeave, lblMaxAttacks.MouseLeave, lblMaxDamage.MouseLeave, lblMaxRange.MouseLeave, lblNewGame.MouseLeave, lblEndTurn.MouseLeave, lblMove.MouseLeave, lblAuto.MouseLeave, pb3.MouseLeave, pb2.MouseLeave, pb1.MouseLeave, lblHelp.MouseLeave
        HideSpecPopup()
    End Sub

    Private Sub lblNewGame_MouseMove(sender As Object, e As MouseEventArgs) Handles lblNewGame.MouseMove
        If BlockClick OrElse AutoOn Then Exit Sub
        Dim s As String = "Ends the current game and starts a new one."
        ShowSpecPopup(sender, s, 40)
    End Sub

    Private Sub lblHelp_MouseMove(sender As Object, e As MouseEventArgs) Handles lblHelp.MouseMove
        If BlockClick Then Exit Sub
        Dim s As String
        s = "Display the Help Window."
        ShowSpecPopup(sender, s, 40)
    End Sub

    Private Sub lblEndTurn_MouseMove(sender As Object, e As MouseEventArgs) Handles lblEndTurn.MouseMove
        If BlockClick OrElse Winner <> "" Then Exit Sub
        Dim s As String
        If IsDeploymentPhase Then
            s = "End Deployment Phase and begin turn-based match play."
        Else
            s = "End the current turn taking no further actions."
        End If
        ShowSpecPopup(sender, s, 40)
    End Sub

    Private Sub lblMove_MouseMove(sender As Object, e As MouseEventArgs) Handles lblMove.MouseMove
        If BlockClick OrElse AutoOn OrElse Winner <> "" Then Exit Sub
        Dim s As String = "Let the computer automatically complete the current turn for you."
        ShowSpecPopup(sender, s, 40)
    End Sub

    Private Sub lblAuto_MouseMove(sender As Object, e As MouseEventArgs) Handles lblAuto.MouseMove
        If BlockClick OrElse AutoOn OrElse Winner <> "" Then Exit Sub
        Dim s As String = "Start fully automatic play." + vbCrLf + "Press again at any time to halt."
        ShowSpecPopup(sender, s, 40)
    End Sub

    Private Sub lblMaxHP_MouseMove(sender As Object, e As MouseEventArgs) Handles lblMaxHP.MouseMove
        If BlockClick OrElse AutoOn Then Exit Sub
        Dim s As String = "Effective Hit Points are the remaining / maximum damage a character can withstand. It factors in physical capacity as well as protection provided by armor or training." + vbCrLf + vbCrLf + "Lost Hit Points can be restored by Healing."
        ShowSpecPopup(sender, s, 100)
    End Sub

    Private Sub lblMaxMove_MouseMove(sender As Object, e As MouseEventArgs) Handles lblMaxMove.MouseMove
        If BlockClick OrElse AutoOn Then Exit Sub
        Dim s As String = "Movement is the remaining / maximum hexes that this character can move per turn." + vbCrLf + vbCrLf + "Movement can be reduced by the Hobbled condition."
        ShowSpecPopup(sender, s, 80)
    End Sub

    Private Sub lblMaxAttacks_MouseMove(sender As Object, e As MouseEventArgs) Handles lblMaxAttacks.MouseMove
        If BlockClick OrElse AutoOn Then Exit Sub
        Dim s As String = "Attacks are the remaining / maximum number of standard attacks that this character can attempt per turn. Multiple attacks can be spread out against one or more opponents." + vbCrLf + vbCrLf + "Attacks can be reduced by the Slowed condition."
        ShowSpecPopup(sender, s, 120)
    End Sub

    Private Sub lblMaxRange_MouseMove(sender As Object, e As MouseEventArgs) Handles lblMaxRange.MouseMove
        If BlockClick OrElse AutoOn Then Exit Sub
        Dim s As String = "Range is the maximum hex distance from which this character can attempt a standard attack." + vbCrLf + vbCrLf + "Some characters will automatically swap between melee or ranged weapon depending on the distance to the target."
        ShowSpecPopup(sender, s, 120)
    End Sub

    Private Sub lblMaxDamage_MouseMove(sender As Object, e As MouseEventArgs) Handles lblMaxDamage.MouseMove
        If BlockClick OrElse AutoOn Then Exit Sub
        Dim s As String = "Damage is the maximum melee / ranged damage that this character can inflict in one standard attack." + vbCrLf + vbCrLf + "Damage can be reduced by the Weakened condition."
        ShowSpecPopup(sender, s, 110)
    End Sub

    Private Sub pb_MouseMove(sender As Object, e As MouseEventArgs) Handles pb3.MouseMove, pb2.MouseMove, pb1.MouseMove
        If BlockClick OrElse AutoOn OrElse Winner <> "" Then Exit Sub
        ShowSpecPopup(sender, sender.tag, 60)
    End Sub

    Dim frmH As frmHelp
    Private Sub lblHelp_Click(sender As Object, e As EventArgs) Handles lblHelp.Click
        If frmH Is Nothing Then
            frmH = New frmHelp
            frmH.Show()
        Else
            frmH.WindowState = FormWindowState.Normal
            If Not frmH.Visible Then frmH.Show()
            frmH.BringToFront()
        End If
    End Sub

    Dim frmR As frmReport
    Private Sub lblCopyright_MouseClick(sender As Object, e As MouseEventArgs) Handles lblCopyright.MouseClick
        ReportShow
    End Sub

    Private Sub ReportShow()
        If frmR Is Nothing Then
            frmR = New frmReport
            frmR.Show()
        Else
            frmR.WindowState = FormWindowState.Normal
            If Not frmR.Visible Then frmR.Show()
            frmR.BringToFront()
        End If
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Me.Height = picMap.Bottom + 50
        lblLog1.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog1.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog2.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog2.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog3.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog3.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog4.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog4.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog5.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog5.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog6.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog6.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog7.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog7.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog8.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog8.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog9.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog9.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog10.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog10.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog11.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog11.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog12.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog12.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog13.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog13.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog14.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog14.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog15.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog15.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog16.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog16.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog17.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog17.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog18.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog18.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog19.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog19.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)
        lblLog20.MinimumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MinimumSize.Height)
        lblLog20.MaximumSize = New Drawing.Size(Me.Width - lblLog1.Left - 34, lblLog1.MaximumSize.Height)

        UpdateLog("REDRAW")
    End Sub

    Private Sub ShowQuickStart()

        UpdateLog("For further help and program details, review the Help Window displayed by clicking the question mark in the upper left." + vbCrLf)
        UpdateLog("You can also use your Right-Click to toggle through available Special Abilities to activate them." + vbCrLf)
        UpdateLog("You can also use Special Abilities when availble. Click on a Special Ability button on the left to activate that ability and then click on the target to perform it." + vbCrLf)
        UpdateLog("On your turn you can move and perform basic attacks. The cursor will change to show you who you can attack." + vbCrLf)
        UpdateLog("At the start of each turn, you can select the character you wish to play that turn. Your first action commits that character." + vbCrLf)
        UpdateLog("If you would like to restart and choose a new team, click on New Game." + vbCrLf)
        UpdateLog("You are in the Deployment Phase during which you can position your characters. Check the specific instructions on the left of the screen." + vbCrLf)
        UpdateLog("Quick Start Tips" + vbCrLf)

    End Sub
#End Region

End Class
