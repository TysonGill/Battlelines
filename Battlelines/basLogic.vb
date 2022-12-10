Imports Utility

Module basLogic


#Region "Fighter"

    Friend Sub DoTurn_Fighter()

        ' Cannot perform specials if disoriented
        If CurCharRow("Disoriented") > 0 Then
            DoTurn_Standard()
            Exit Sub
        End If

        Dim CurRow As Integer = CurCharRow("CurRow")
        Dim CurCol As Integer = CurCharRow("CurCol")
        Dim Chance As Integer

        ' Charge
        Chance = 50
        If Rand() < Chance AndAlso CurCharRow("Spec2 Cooldown Left") = 0 AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") AndAlso CurCharRow("Moves Left") = CurCharRow("Moves Max") Then

            ' Find all enemies at within range
            Dim dtEnemyHexes As DataTable = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 3, CurCharRow("Spec2 Range"), False, True, False)
            If dtEnemyHexes.Rows.Count > 0 Then

                ' Sort so that the strongest enemy is first
                dtEnemyHexes = db.SortDataTable(dtEnemyHexes, "[Life Left] DESC")
                Dim TarData As DataRow = dtEnemyHexes.Rows(0)

                DoCharge(CurCharRow, TarData("Row"), TarData("Col"))

            End If
        End If

        ' Cleave
        Chance = 70
        If Rand() < Chance AndAlso CurCharRow("Spec3 Cooldown Left") = 0 AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") Then

            ' Find all enemies at within range
            Dim dtEnemyHexes As DataTable = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 1, 1, False, True, False)
            If dtEnemyHexes.Rows.Count > 1 AndAlso Rand() <= dtEnemyHexes.Rows.Count * 30 Then

                ' Sort so that the strongest enemy is first
                dtEnemyHexes = db.SortDataTable(dtEnemyHexes, "[Life Left] DESC")
                Dim TarData As DataRow = dtEnemyHexes.Rows(0)

                DoCleave(CurCharRow, TarData("Row"), TarData("Col"))

            End If
        End If

        ' Raise Shield
        Chance = 80
        If Rand() < Chance AndAlso CurCharRow("Spec1 Cooldown Left") = 0 AndAlso CurCharRow("Shield Raised") = 0 AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") Then

            ' Find all enemies at within range
            Dim dtEnemyHexes As DataTable = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 1, 2, False, True, False)
            If Rand() <= dtEnemyHexes.Rows.Count * 30 + 5 Then

                DoRaiseShield(CurCharRow)

            End If
        End If

        ' Perform standard actions
        DoTurn_Standard()

    End Sub

    Friend Sub DoRaiseShield(CharData As DataRow)
        Try

            SpecStat(1) = SpecState.Active

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to Raise Shield... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec1 Cooldown Left") = CharData("Spec1 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Determine success
            Dim Success As Boolean = (Rand() <= CharData("Spec1 Chance"))

            ' Update log
            If Not Success Then
                msg = " but fails." + vbCrLf
            Else
                CharData("Shield Raised") = 1
                HexLabel(CharData("CurRow"), CharData("CurCol"), CharData)
                msg = " and will deflect some incoming damage." + vbCrLf
                ' Show animation at target location
                FlashHex(CharData("CurRow"), CharData("CurCol"), cHelpful)
                Wait(500)
            End If
            frmMain.UpdateLog(msg)

            'Update UI
            SpecStat(1) = SpecState.Unavailable
            frmMain.txtPop.Text = GetCharSummary(CharData)
            frmMain.UpdateCharStats(CharData)
            ReportAdd(CurCharRow, "Raise Shield", Success)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoCharge(CharData As DataRow, row As Integer, col As Integer)
        Try
            SpecStat(2) = SpecState.Active

            ' Get target data row
            Dim TarData As DataRow = Hex(row, col).CharRow

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to charge the enemy with his shield... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Moves Left") = 0
            CharData("Attacks Left") = 0
            CharData("Spec2 Cooldown Left") = CharData("Spec2 Cooldown")

            ' Find free hexes around the target
            Dim dt As DataTable = GetHexMap(CharData("Side"), row, col, 1, 1, False, False, True)
            If dt.Rows.Count = 0 Then
                msg = " but fails to find an opening." + vbCrLf
                frmMain.UpdateLog(msg)
                frmMain.UpdateCharStats(CharData)
                Exit Sub
            End If

            ' Find nearest free target
            For i As Integer = 0 To dt.Rows.Count - 1
                dt.Rows(i)("Distance") = CalcHexDist(CharData("CurRow"), CharData("CurCol"), dt.Rows(i)("Row"), dt.Rows(i)("Col"))
            Next
            dt = db.SortDataTable(dt, "[Distance]")
            Dim ChargeHex As DataRow = dt.Rows(0)

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Move to target
            DoMove(CharData, ChargeHex("Row"), ChargeHex("Col"))
            Wait(400)

            ' Do damage
            DoDamage(CharData, TarData, "Charge")

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoCleave(CharData As DataRow, row As Integer, col As Integer)
        Try
            SpecStat(3) = SpecState.Active

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " swings his sword in a wide arc... "
            frmMain.UpdateLog(msg)

            ' Get target hexes
            Dim dtLocs As DataTable = GetHexMapOverlap(CharData("Side"), CharData("CurRow"), CharData("CurCol"), row, col)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec3 Cooldown Left") = CharData("Spec3 Cooldown")
            CharData("Shield Raised") = 0 ' Lower Raised Shield

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Deal damage
            Dim TarData As DataRow
            Dim ds As DamageResultsStruct
            Dim TotalDamage As Integer = 0
            For i As Integer = 0 To dtLocs.Rows.Count - 1
                If dtLocs.Rows(i)("Side") = "" Then Continue For
                TarData = dtLocs.Rows(i)("CharRow")
                ds = DoDamage(CharData, TarData, "Cleave")
                dtLocs.Rows(i)("Message") = ds.AdjustedDamage.ToString
                TotalDamage += ds.AdjustedDamage
            Next

            ' Show results
            Dim TargetCount As Integer = dtLocs.Select("[Side] <> ''").Count
            If TotalDamage = 0 Then
                msg = " but fails to hit any anyone." + vbCrLf
            Else
                msg = " and inflicts a total of " + TotalDamage.ToString + " sword damage to " + TargetCount.ToString + " combatants." + vbCrLf
            End If
            frmMain.UpdateLog(msg)

            ' Flash surrounding targets
            FlashHexes(dtLocs, cHarmful, 3, 100)

            'Update UI
            SpecStat(3) = SpecState.Unavailable

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

#End Region

#Region "Monk"

    Friend Sub DoTurn_Monk()

        ' Cannot perform specials if disoriented
        If CurCharRow("Disoriented") > 0 Then
            DoTurn_Standard()
            Exit Sub
        End If

        Dim CurRow As Integer = CurCharRow("CurRow")
        Dim CurCol As Integer = CurCharRow("CurCol")
        Dim Chance As Integer

        ' Spec 2, Renew: If injured, Self Heal/Cure
        If CurCharRow("Spec2 Cooldown Left") = 0 AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") Then
            Chance = 1
            If CurCharRow("Life Left") <= CurCharRow("Spec2 Amount") Then
                Chance = 75
            ElseIf CurCharRow("Life Max") - CurCharRow("Life Left") >= CurCharRow("Spec2 Amount") Then
                Chance = 25
            End If
            If Rand() < Chance Then
                DoRenew(CurCharRow)
                Wait(TurnDelay)
            End If
        End If

        ' Spec 1, Evasive: Larger chance with more enemies near
        If CurCharRow("Spec1 Cooldown Left") = 0 AndAlso CurCharRow("Moves Left") = CurCharRow("Moves Max") Then
            Dim Enemies As Integer = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 1, 1, False, True, False).Rows.Count
            If Rand() <= Enemies * 25 + 5 Then
                DoEvasive(CurCharRow)
                Wait(TurnDelay)
            End If
        End If

        ' Spec 3, Disorient
        If CurCharRow("Spec3 Cooldown Left") = 0 AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") Then
            Dim dt As DataTable = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 1, 1, False, True, False)
            dt = FilterCondition(dt, "Disoriented", False)
            If dt.Rows.Count > 0 AndAlso Rand() <= dt.Rows.Count * 33 Then
                Dim TarData As DataRow = dt.Rows(0)
                DoDisorient(CurCharRow, TarData("Row"), TarData("Col"))
                Wait(TurnDelay)
            End If
        End If

        ' Perform standard actions
        DoTurn_Standard()

    End Sub

    Friend Sub DoEvasive(CharData As DataRow)

        SpecStat(1) = SpecState.Active

        ' Initiate log
        Dim msg As String
        msg = "Your " + GetTitle(CharData) + " adopts an Evasive stance... "
        frmMain.UpdateLog(msg)

        ' Set flags and counters
        CharData("Moves Left") = 0
        CharData("Spec1 Cooldown Left") = CharData("Spec1 Cooldown")

        ' Show casting animation
        FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 3, 125)

        ' Determine success
        Dim Success As Boolean = (Rand() <= CharData("Spec1 Chance"))

        ' Update log
        If Not Success Then
            msg = " but fails." + vbCrLf
        Else
            ' Activate evasion
            msg = " and becomes " + CharData("Spec1 Amount").ToString + "% harder to hit." + vbCrLf
            CharData("Evasive Stance") = 1
            HexLabel(CharData("CurRow"), CharData("CurCol"), CharData)
        End If
        frmMain.UpdateLog(msg)

        'Update UI
        SpecStat(1) = SpecState.Unavailable
        frmMain.UpdateCharStats(CharData)
        frmMain.txtPop.Text = GetCharSummary(CharData)
        ReportAdd(CurCharRow, "Evasive", Success)

    End Sub

    Friend Sub DoRenew(CharData As DataRow)
        Try

            SpecStat(2) = SpecState.Active

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to renew health... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec2 Cooldown Left") = CharData("Spec2 Cooldown")
            CharData("Evasive Stance") = 0 ' Drop Evasive Stance

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 3, 125)

            ' Determine success
            Dim Success As Boolean = (Rand() <= CharData("Spec2 Chance"))

            ' Determine heal
            Dim MaxHeal As Integer = CharData("Spec2 Amount")
            Dim Damage As Integer = 0
            Dim Purged As Integer = 0
            If Success Then
                Damage = Rand(1, MaxHeal, 2)
                If CharData("Life Left") + Damage > CharData("Life Max") Then Damage = CharData("Life Max") - CharData("Life Left")
                CharData("Life Left") += Damage

                ' Purge conditions
                Dim condition As String
                Dim dtConditions As DataTable
                Dim CuresLeft As Integer = CharData("Spec2 Range")
                Do While CuresLeft > 0
                    If Rand() > CharData("Spec3 Chance") Then Exit Do
                    dtConditions = GetCharConditions(CharData)
                    If dtConditions.Rows.Count = 0 Then Exit Do
                    condition = dtConditions.Rows(Rand(0, dtConditions.Rows.Count - 1))("Condition")
                    CharData(condition) = 0
                    CuresLeft -= 1
                    Purged += 1
                Loop
            End If

            ' Update log
            If Not Success Then
                msg = " but fails." + vbCrLf
            Else
                msg = " and renews " + Damage.ToString + " of " + MaxHeal.ToString + " hit points." + vbCrLf
            End If
            If Purged Then msg += "You purge " + Purged.ToString + " conditions." + vbCrLf
            frmMain.UpdateLog(msg)

            ' Show animation at target location
            FlashHex(CharData("CurRow"), CharData("CurCol"), cHelpful, ,, Damage.ToString)
            Wait(300)

            'Update UI
            HexLabel(CharData("CurRow"), CharData("CurCol"), CharData)
            frmMain.txtPop.Text = GetCharSummary(CharData)
            frmMain.UpdateCharStats(CharData)
            ReportAdd(CurCharRow, "Renew", Success, Damage)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoDisorient(CharData As DataRow, Row As Integer, Col As Integer)
        Try

            SpecStat(3) = SpecState.Active

            ' Get target data row
            Dim TarData As DataRow = Hex(Row, Col).CharRow

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to Disorient a " + GetTitle(TarData) + "... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec3 Cooldown Left") = CharData("Spec3 Cooldown")
            CharData("Evasive Stance") = 0 ' Drop Evasive Stance

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Determine success
            Dim Success As Boolean = (Rand() <= CharData("Spec3 Chance"))

            ' Update log
            If Not Success Then
                msg = " but fails." + vbCrLf
            Else
                msg = " and succeeds!" + vbCrLf
                ' Apply Disoriented condition
                TarData("Disoriented") = 1
                TarData("Disoriented Amount") = CharData("Spec3 Amount")
                ' Disrupt any active defenses
                TarData("Shield Raised") = 0
                TarData("Evasive Stance") = 0
                TarData("Protective Aura") = 0
                ' Show animation at target location
                FlashHex(CharData("CurRow"), CharData("CurCol"), cHarmful)
                HexLabel(Row, Col, TarData)
            End If
            frmMain.UpdateLog(msg)

            'Update UI
            SpecStat(3) = SpecState.Unavailable
            frmMain.txtPop.Text = GetCharSummary(CharData)
            frmMain.UpdateCharStats(CharData)
            ReportAdd(CurCharRow, "Disorient", Success)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

#End Region

#Region "Rogue"

    Friend Sub DoTurn_Rogue()

        ' Cannot perform specials if disoriented
        If CurCharRow("Disoriented") > 0 Then
            DoTurn_Standard()
            Exit Sub
        End If

        Dim CurRow As Integer = CurCharRow("CurRow")
        Dim CurCol As Integer = CurCharRow("CurCol")
        Dim Chance As Integer

        ' Get nearby targets for a debuff
        Dim dt As DataTable = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 1, 1, False, True, False)
        Dim dtUnSlowed As DataTable
        Dim dtUnBlinded As DataTable
        Dim dtUnWeakened As DataTable

        ' See if the rogue debuffs a target
        Chance = 25
        Dim Debuff As Integer = 0
        Dim TarData As DataRow
        For i As Integer = 1 To 2

            If CurCharRow("Spec1 Cooldown Left") > 0 AndAlso CurCharRow("Spec2 Cooldown Left") > 0 AndAlso CurCharRow("Spec3 Cooldown Left") > 0 Then Exit For
            If CurCharRow("Attacks Left") = 0 Then Exit For

            If Rand() <= Chance * i Then Continue For

            ' Get lists of nearby enemies without the various conditions
            dtUnSlowed = FilterCondition(dt, "Slowed", False)
            dtUnBlinded = FilterCondition(dt, "Blinded", False)
            dtUnWeakened = FilterCondition(dt, "Weakened", False)
            If dtUnSlowed.Rows.Count = 0 AndAlso dtUnBlinded.Rows.Count = 0 AndAlso dtUnWeakened.Rows.Count = 0 Then Exit For

            Dim Slowed As Boolean = False
            Dim Blinded As Boolean = False
            Dim Weakened As Boolean = False
            Dim TriedSlow As Boolean = False
            Dim TriedBlind As Boolean = False
            Dim TriedWeak As Boolean = False
            Do
                Select Case Rand(1, 3)
                    Case 1
                        If dtUnSlowed.Rows.Count > 0 AndAlso CurCharRow("Spec1 Cooldown Left") = 0 Then
                            ' Spec 1, Slow
                            TarData = dtUnSlowed.Rows(0)
                            DoSlow(CurCharRow, TarData("Row"), TarData("Col"))
                            Wait(TurnDelay)
                            Slowed = True
                        End If
                        TriedSlow = True
                    Case 2
                        If dtUnBlinded.Rows.Count > 0 AndAlso CurCharRow("Spec2 Cooldown Left") = 0 Then
                            ' Spec 2, Blind
                            TarData = dtUnBlinded.Rows(0)
                            DoBlind(CurCharRow, TarData("Row"), TarData("Col"))
                            Wait(TurnDelay)
                            Blinded = True
                        End If
                        TriedBlind = True
                    Case 3
                        If dtUnWeakened.Rows.Count > 0 AndAlso CurCharRow("Spec3 Cooldown Left") = 0 Then
                            ' Spec 3, Weaken
                            TarData = dtUnWeakened.Rows(0)
                            DoWeaken(CurCharRow, TarData("Row"), TarData("Col"))
                            Wait(TurnDelay)
                            Weakened = True
                        End If
                        TriedWeak = True
                End Select
            Loop Until (Slowed OrElse Blinded OrElse Weakened) OrElse (TriedSlow AndAlso TriedBlind AndAlso TriedWeak)
        Next

        ' Perform standard actions
        DoTurn_Standard()

    End Sub

    Friend Sub DoSlow(CharData As DataRow, Row As Integer, Col As Integer)
        Try

            SpecStat(1) = SpecState.Active

            ' Get target data row
            Dim TarData As DataRow = Hex(Row, Col).CharRow

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to Slow a " + GetTitle(TarData) + "... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") -= 1
            CharData("Spec1 Cooldown Left") = CharData("Spec1 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Determine success
            Dim Success As Boolean = (Rand() <= CharData("Spec1 Chance"))

            ' Update log
            If Not Success Then
                msg = " but fails." + vbCrLf
            Else
                msg = " and succeeds!" + vbCrLf
                ' Apply Slowed condition
                TarData("Slowed") = 1
                TarData("Slowed Amount") = CharData("Spec1 Amount")
                TarData("Attacks Left") -= CharData("Spec1 Amount")
                If TarData("Attacks Left") < 0 Then TarData("Attacks Left") = 0
                ' Show animation at target location
                FlashHex(TarData("CurRow"), TarData("CurCol"), cHarmful)
                HexLabel(Row, Col, TarData)
            End If
            frmMain.UpdateLog(msg)

            'Update UI
            SpecStat(1) = SpecState.Unavailable
            frmMain.txtPop.Text = GetCharSummary(CharData)
            frmMain.UpdateCharStats(CharData)
            ReportAdd(CurCharRow, "Slow", Success)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoBlind(CharData As DataRow, Row As Integer, Col As Integer)
        Try

            SpecStat(2) = SpecState.Active

            ' Get target data row
            Dim TarData As DataRow = Hex(Row, Col).CharRow

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to Blind a " + GetTitle(TarData) + "... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") -= 1
            CharData("Spec2 Cooldown Left") = CharData("Spec2 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Determine success
            Dim Success As Boolean = (Rand() <= CharData("Spec2 Chance"))

            ' Update log
            If Not Success Then
                msg = " but fails." + vbCrLf
            Else
                msg = " and succeeds!" + vbCrLf
                ' Apply Blinded condition
                TarData("Blinded") = 1
                TarData("Blinded Amount") = CharData("Spec2 Amount")
                ' Show animation at target location
                FlashHex(TarData("CurRow"), TarData("CurCol"), cHarmful)
                HexLabel(Row, Col, TarData)
            End If
            frmMain.UpdateLog(msg)

            'Update UI
            SpecStat(2) = SpecState.Unavailable
            frmMain.txtPop.Text = GetCharSummary(CharData)
            frmMain.UpdateCharStats(CharData)
            ReportAdd(CurCharRow, "Blind", Success)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoWeaken(CharData As DataRow, Row As Integer, Col As Integer)
        Try

            SpecStat(3) = SpecState.Active

            ' Get target data row
            Dim TarData As DataRow = Hex(Row, Col).CharRow

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to Weaken a " + GetTitle(TarData) + "... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") -= 1
            CharData("Spec3 Cooldown Left") = CharData("Spec3 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Determine success
            Dim Success As Boolean = (Rand() <= CharData("Spec3 Chance"))

            ' Update log
            If Not Success Then
                msg = " but fails." + vbCrLf
            Else
                msg = " and succeeds!" + vbCrLf
                ' Apply Weakened condition
                TarData("Weakened") = 1
                TarData("Weakened Amount") = CharData("Spec3 Amount")
                ' Show animation at target location
                FlashHex(TarData("CurRow"), TarData("CurCol"), cHarmful)
                HexLabel(Row, Col, TarData)
            End If
            frmMain.UpdateLog(msg)

            'Update UI
            SpecStat(3) = SpecState.Unavailable
            frmMain.txtPop.Text = GetCharSummary(CharData)
            frmMain.UpdateCharStats(CharData)
            ReportAdd(CurCharRow, "Weaken", Success)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

#End Region

#Region "Cleric"

    Friend Sub DoTurn_Cleric()

        ' Cannot perform specials if disoriented
        If CurCharRow("Disoriented") > 0 Then
            DoTurn_Standard()
            Exit Sub
        End If

        Dim TarData As DataRow
        Dim HealRange As Integer = CurCharRow("Spec1 Range")
        Dim CurRow As Integer = CurCharRow("CurRow")
        Dim CurCol As Integer = CurCharRow("CurCol")
        Dim Chance As Integer

        ' See if an ally needs revive
        Dim dtGY As DataTable = IIf(CurSide = "Party", dtPartyGY, dtEnemyGY)
        If CurCharRow("Spec3 Cooldown Left") = 0 AndAlso dtGY.Rows.Count > 0 Then
            If Rand() <= dtGY.Rows.Count * 15 Then
                Dim pTar As Point = FindOpenHex(CurCharRow("CurRow"), CurCharRow("CurCol"))
                If pTar.X > 0 Then
                    DoRevive(CurCharRow, pTar.X, pTar.Y)
                    Wait(TurnDelay)
                End If
            End If
        End If

        ' Map targets
        MapTargets()

        ' See if any ally in range is in need of critical healing
        If CurCharRow("Attacks Left") > 0 Then
            TarData = FindMostHurt(HealRange, 10)
            If TarData IsNot Nothing Then
                DoHeal(CurCharRow, TarData("CurRow"), TarData("CurCol"))
                Wait(TurnDelay)
            End If
        End If

        ' See if any ally in range is in need of some healing
        If CurCharRow("Attacks Left") > 0 Then
            TarData = FindMostHurt(HealRange, 20)
            If TarData IsNot Nothing Then
                DoHeal(CurCharRow, TarData("CurRow"), TarData("CurCol"))
                Wait(TurnDelay)
            End If
        End If

        ' Cast Harm
        Chance = 65
        If Rand() <= Chance AndAlso CurCharRow("Spec2 Cooldown Left") = 0 AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") Then
            Dim dtTargetHexes As DataTable = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 2, CurCharRow("Spec2 Range"), False, True, False)
            If dtTargetHexes.Rows.Count >= 1 Then
                dtTargetHexes = RandomizeTable(dtTargetHexes)
                dtTargetHexes = db.SortDataTable(dtTargetHexes, "[Life Left] DESC")
                TarData = dtTargetHexes.Rows(0)
                DoHarm(CurCharRow, TarData("Row"), TarData("Col"))
            End If
        End If

        ' Move toward a wounded ally
        If CurCharRow("Moves Left") > 0 Then
            TarData = FindMostHurt(10)
            If TarData IsNot Nothing Then
                MoveTowardLocation(CurCharRow, TarData)
                Wait(TurnDelay)
            End If
        End If

        ' Perform standard actions
        DoTurn_Standard()

    End Sub

    Friend Sub DoHeal(CharData As DataRow, row As Integer, col As Integer)
        Try

            SpecStat(1) = SpecState.Active

            ' Get target data row
            Dim TarData As DataRow = Hex(row, col).CharRow

            ' Initiate log
            Dim msg As String
            If TarData Is CharData Then
                msg = "Your " + GetTitle(CharData) + " attempts to heal self... "
            Else
                msg = "Your " + GetTitle(CharData) + " attempts to heal an ally... "
            End If
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec1 Cooldown Left") = CharData("Spec1 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 3, 125)

            ' Determine success
            Dim Damage As Integer = 0
            Dim Purged As Integer = 0
            Dim MaxHeal As Integer = CharData("Spec1 Amount")
            Dim Success As Boolean = Rand() <= CharData("Spec1 Chance")
            If Success Then
                Damage = Rand(1, MaxHeal, 2)
                If TarData("Life Left") + Damage > TarData("Life Max") Then Damage = TarData("Life Max") - TarData("Life Left")
                TarData("Life Left") += Damage

                ' Attempt to purge all conditions
                Dim dt As DataTable = GetCharConditions(CharData)
                For i As Integer = 0 To dt.Rows.Count - 1
                    CharData(dt.Rows(i)("Condition")) = 0
                    Purged += 1
                Next
            End If

            ' Update log
            If Not Success Then
                msg = " but fails." + vbCrLf
            Else
                msg = " and heals " + Damage.ToString + " of " + MaxHeal.ToString + " possible hit points." + vbCrLf
                ' Show animation at target location
                FlashHex(row, col, cHelpful, , , Damage.ToString)
                Wait(300)
            End If
            If Purged Then msg += "You cure " + Purged.ToString + " conditions." + vbCrLf
            frmMain.UpdateLog(msg)

            'Update UI
            SpecStat(1) = SpecState.Unavailable
            frmMain.UpdateCharStats(CharData)
            frmMain.txtPop.Text = GetCharSummary(TarData)
            ReportAdd(CurCharRow, "Heal", Damage > 0, Damage)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoHarm(CharData As DataRow, row As Integer, col As Integer)
        Try

            SpecStat(2) = SpecState.Active

            ' Get target data row
            Dim TarData As DataRow = Hex(row, col).CharRow

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to cast Harm on an ally... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec2 Cooldown Left") = CharData("Spec2 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Do damage
            DoDamage(CharData, TarData, "Harm")

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoRevive(CharData As DataRow, row As Integer, col As Integer)
        Try

            SpecStat(3) = SpecState.Active

            ' Get character data row to revive
            Dim dtGY As DataTable = IIf(CurSide = "Party", dtPartyGY, dtEnemyGY)
            Dim TarData As DataRow = dtGY.Rows(Rand(0, dtGY.Rows.Count - 1))

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to Revive a fallen " + GetTitle(TarData) + "... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Moves Left") = 0
            CharData("Attacks Left") = 0
            CharData("Spec2 Cooldown Left") = CharData("Spec2 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 5, 125)

            ' Determine success
            Dim Success As Boolean = Rand() <= CharData("Spec3 Chance")

            ' Update log
            If Not Success Then
                msg = " but he Is lost forever." + vbCrLf
            Else
                msg = " and Is successful." + vbCrLf
                ' Revive character
                CharData.Table.ImportRow(TarData)
                Dim RevRow As DataRow = CharData.Table.Rows(CharData.Table.Rows.Count - 1)
                RevRow("CurRow") = row
                RevRow("CurCol") = col
                RevRow("Life Left") = RevRow("Life Max")
                Hex(RevRow("CurRow"), RevRow("CurCol")).side = RevRow("Side")
                Hex(RevRow("CurRow"), RevRow("CurCol")).CharRow = RevRow
                ' Show animation at target location
                FlashHex(row, col, cHelpful)
            End If
            frmMain.UpdateLog(msg)

            ' Regardless of success, remove target from GY
            dtGY.Rows.Remove(TarData)
            dtGY.AcceptChanges()

            'Update UI
            SpecStat(3) = SpecState.Unavailable
            frmMain.txtPop.Text = GetCharSummary(CharData)
            frmMain.UpdateCharStats(CharData)
            ReportAdd(CurCharRow, "Revive", Success)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

#End Region

#Region "Ranger"

    Friend Sub DoTurn_Ranger()

        ' Cannot perform specials if disoriented
        If CurCharRow("Disoriented") > 0 Then
            DoTurn_Standard()
            Exit Sub
        End If

        Dim CurRow As Integer = CurCharRow("CurRow")
        Dim CurCol As Integer = CurCharRow("CurCol")
        Dim TarData As DataRow
        Dim Chance As Integer

        ' Hobble an opponent in close melee range
        Chance = 75
        If Rand() <= Chance AndAlso CurCharRow("Spec1 Cooldown Left") = 0 AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") Then

            ' Find all enemies at close range
            Dim dtEnemyHexes As DataTable = GetHexMap(CurCharRow("Side"), CurCharRow("CurRow"), CurCharRow("CurCol"), 1, 1, False, True, False)
            If dtEnemyHexes.Rows.Count > 0 Then

                ' Sort so that the fastest enemy with the most life left is first
                dtEnemyHexes = db.SortDataTable(dtEnemyHexes, "[Moves Max] DESC, [Life Left] DESC")
                If dtEnemyHexes.Rows.Count > 0 Then

                    ' Find an enemy who isn't already Hobbled
                    Dim TarIndex As Integer
                    For TarIndex = 0 To dtEnemyHexes.Rows.Count - 1
                        If dtEnemyHexes.Rows(TarIndex)("CharRow")("Hobbled") = 0 Then Exit For
                    Next

                    ' Attempt to Hobble
                    If TarIndex < dtEnemyHexes.Rows.Count Then
                        DoHobble(CurCharRow, dtEnemyHexes.Rows(TarIndex)("Row"), dtEnemyHexes.Rows(TarIndex)("Col"))
                    End If

                End If
            End If
        End If

        ' Precision Shot - target highest hp opponent
        Chance = 40
        If Rand() <= Chance AndAlso CurCharRow("Spec2 Cooldown Left") = 0 AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") AndAlso CurCharRow("Moves Left") = CurCharRow("Moves Max") Then
            Dim dtTargetHexes As DataTable = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 1, CurCharRow("Spec2 Range"), False, True, False)
            If dtTargetHexes.Rows.Count >= 1 Then
                dtTargetHexes = RandomizeTable(dtTargetHexes)
                dtTargetHexes = db.SortDataTable(dtTargetHexes, "[Life Left] DESC")
                TarData = dtTargetHexes.Rows(0)
                DoPrecisionShot(CurCharRow, TarData("Row"), TarData("Col"))
            End If
        End If

        ' Arrow Volley
        Chance = 33
        If Rand() <= Chance AndAlso CurCharRow("Spec3 Cooldown Left") = 0 AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") AndAlso CurCharRow("Moves Left") = CurCharRow("Moves Max") Then

            Dim ToRow As Integer
            Dim ToCol As Integer

            ' Find all open hexes for available target destinations in range
            Dim dtTargetHexes As DataTable = GetHexMap(CurCharRow("Side"), CurCharRow("CUrRow"), CurCharRow("CurCol"), 3, CurCharRow("Spec3 Range"), True, True, True)

            ' Add counts of allies and enemies around each open hex
            dtTargetHexes = AddNearsToHexMap(CurCharRow("Side"), dtTargetHexes)

            ' Sort so that the best target is first
            dtTargetHexes = RandomizeTable(dtTargetHexes)
            dtTargetHexes = db.SortDataTable(dtTargetHexes, "[EnemiesNear] DESC, [AlliesNear]")
            If dtTargetHexes.Rows.Count > 0 AndAlso dtTargetHexes.Rows(0)("EnemiesNear") >= 3 AndAlso dtTargetHexes.Rows(0)("AlliesNear") <= 2 Then

                ' Fire the volley
                ToRow = dtTargetHexes.Rows(0)("Row")
                ToCol = dtTargetHexes.Rows(0)("Col")
                DoArrowVolley(CurCharRow, ToRow, ToCol)

            End If
        End If

        ' Perform standard actions
        DoTurn_Standard()

    End Sub

    Friend Sub DoHobble(CharData As DataRow, Row As Integer, Col As Integer)
        Try

            SpecStat(1) = SpecState.Active

            ' Get target data row
            Dim TarData As DataRow = Hex(Row, Col).CharRow

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to Hobble a " + GetTitle(TarData) + "... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec1 Cooldown Left") = CharData("Spec1 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Determine success
            Dim Success As Boolean = (Rand() <= CharData("Spec1 Chance"))

            ' Update log
            If Not Success Then
                msg = " but fails." + vbCrLf
            Else
                msg = " and succeeds!" + vbCrLf
                ' Apply Hobbled condition
                TarData("Hobbled") = 1
                TarData("Hobbled Amount") = CharData("Spec1 Amount")
                TarData("Moves Left") -= TarData("Hobbled Amount")
                If TarData("Moves Left") < 0 Then TarData("Moves Left") = 0
                ' Show animation at target location
                FlashHex(CharData("CurRow"), CharData("CurCol"), cHarmful)
                HexLabel(Row, Col, TarData)
            End If
            frmMain.UpdateLog(msg)

            'Update UI
            SpecStat(1) = SpecState.Unavailable
            frmMain.txtPop.Text = GetCharSummary(CharData)
            frmMain.UpdateCharStats(CharData)
            ReportAdd(CurCharRow, "Hobble", Success)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoPrecisionShot(CharData As DataRow, Row As Integer, Col As Integer)
        Try

            SpecStat(2) = SpecState.Active

            ' Get target data row
            Dim TarData As DataRow = Hex(Row, Col).CharRow

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to fire a Precision Shot at a " + GetTitle(TarData) + "... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec2 Cooldown Left") = CharData("Spec2 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 2, 125)

            ' Do damage
            DoDamage(CharData, TarData, "Precision Shot")

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoArrowVolley(CharData As DataRow, row As Integer, col As Integer)
        Try

            SpecStat(3) = SpecState.Active

            ' Update log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " releases a volley of arrows... "
            frmMain.UpdateLog(msg)

            ' Get target hexes
            Dim dtLocs As DataTable = GetHexMap(CharData("Side"), row, col, 0, 1, True, True, True)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Moves Left") = 0
            CharData("Spec3 Cooldown Left") = CharData("Spec3 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 4, 125)
            Wait(300)

            ' Show animation at target location
            HexShow(row, col, cHarmful)
            Wait(300)

            ' Deal damage
            Dim TarData As DataRow
            Dim ds As DamageResultsStruct
            Dim TotalDamage As Integer = 0
            For i As Integer = 0 To dtLocs.Rows.Count - 1
                If dtLocs.Rows(i)("Side") = "" Then Continue For
                TarData = dtLocs.Rows(i)("CharRow")
                ds = DoDamage(CharData, TarData, "Arrow Volley")
                dtLocs.Rows(i)("Message") = ds.AdjustedDamage.ToString
                TotalDamage += ds.AdjustedDamage
            Next

            ' Show results
            Dim TargetCount As Integer = dtLocs.Select("[Side] <> ''").Count
            If TotalDamage = 0 Then
                msg = " but fails to hit any anyone." + vbCrLf
            Else
                msg = " and inflicts a total of " + TotalDamage.ToString + " arrow damage to " + TargetCount.ToString + " combatants." + vbCrLf
            End If
            frmMain.UpdateLog(msg)

            ' Flash surrounding targets
            FlashHexes(dtLocs, cHarmful, 1, 900)

            'Update UI
            SpecStat(3) = SpecState.Unavailable

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

#End Region

#Region "Wizard"

    Friend Sub DoTurn_Wizard()

        ' Cannot perform specials if disoriented
        If CurCharRow("Disoriented") > 0 Then
            DoTurn_Standard()
            Exit Sub
        End If

        Dim CurRow As Integer = CurCharRow("CurRow")
        Dim CurCol As Integer = CurCharRow("CurCol")
        Dim Chance As Integer

        ' See if the wizard raises a reflective aura
        Chance = 75
        If Rand() < Chance AndAlso CurCharRow("Spec1 Cooldown Left") = 0 AndAlso CurCharRow("Spec1 Amount Left") = 0 AndAlso CurCharRow("Moves Left") = CurCharRow("Moves Max") Then

            DoDefensiveAura(CurCharRow)

        End If

        ' See if the wizard needs to teleport
        If CurCharRow("Spec2 Cooldown Left") = 0 AndAlso CurCharRow("Moves Left") = CurCharRow("Moves Max") AndAlso CurCharRow("Attacks Left") = CurCharRow("Attacks Max") Then

            ' The more threats, the more likely to teleport
            Chance = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 1, 1, False, True, False).Rows.Count
            If Chance > 0 AndAlso Rand(1, Chance * 15) < 100 Then

                ' Attempt to teleport
                Dim ToRow As Integer
                Dim ToCol As Integer

                ' Find all open hexes for available teleport destinations in range
                Dim dtOpenHexes As DataTable = GetHexMap(CurCharRow("Side"), CurRow, CurCol, 3, CurCharRow("Spec2 Range"), False, False, True)

                ' Add counts of allies and enemies around each open hex
                dtOpenHexes = AddNearsToHexMap(CurCharRow("Side"), dtOpenHexes)

                ' Sort so that the safest random location is first
                dtOpenHexes = RandomizeTable(dtOpenHexes)
                dtOpenHexes = db.SortDataTable(dtOpenHexes, "[EnemiesNear], [AlliesNear] DESC")
                If dtOpenHexes.Rows.Count > 0 Then

                    ' Teleport to safe location
                    ToRow = dtOpenHexes.Rows(0)("Row")
                    ToCol = dtOpenHexes.Rows(0)("Col")
                    DoTeleport(CurCharRow, ToRow, ToCol)

                End If
            End If
        End If

        ' See if the wizard casts fireball
        Chance = 50
        If Rand() < Chance AndAlso CurCharRow("Spec3 Cooldown Left") = 0 AndAlso CurCharRow("Moves Left") = CurCharRow("Moves Max") Then

            ' Attempt to cast fireball
            Dim ToRow As Integer
            Dim ToCol As Integer

            ' Find all open hexes for available teleport destinations in range
            Dim dtTargetHexes As DataTable = GetHexMap(CurCharRow("Side"), CurCharRow("CurRow"), CurCharRow("CurCol"), 3, CurCharRow("Spec3 Range"), True, True, True)

            ' Add counts of allies and enemies around each open hex
            dtTargetHexes = AddNearsToHexMap(CurCharRow("Side"), dtTargetHexes)

            ' Sort so that the best target is first
            dtTargetHexes = RandomizeTable(dtTargetHexes)
            dtTargetHexes = db.SortDataTable(dtTargetHexes, "[EnemiesNear] DESC, [AlliesNear]")
            If dtTargetHexes.Rows.Count > 0 AndAlso dtTargetHexes.Rows(0)("EnemiesNear") >= 3 AndAlso dtTargetHexes.Rows(0)("AlliesNear") <= 2 Then

                ' Cast the fireball
                ToRow = dtTargetHexes.Rows(0)("Row")
                ToCol = dtTargetHexes.Rows(0)("Col")
                DoFireball(CurCharRow, ToRow, ToCol)

            End If
        End If

        ' Perform standard actions
        DoTurn_Standard()

    End Sub

    Friend Sub DoDefensiveAura(CharData As DataRow)
        Try

            SpecStat(1) = SpecState.Active

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to conjure a Reflective Aura... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec1 Cooldown Left") = CharData("Spec1 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 3, 125)

            ' Determine success
            Dim Success As Boolean = (Rand() <= CharData("Spec1 Chance"))
            If Success Then
                CharData("Spec1 Amount Left") = CharData("Spec1 Amount")
                CharData("Protective Aura") = 1
                msg = " and succeeds In producing a " + CharData("Spec1 Amount").ToString + " HP defensive aura!" + vbCrLf
                HexLabel(CharData("CurRow"), CharData("CurCol"), CharData)
            Else
                msg = " but fails." + vbCrLf
            End If

            ' Update log
            frmMain.UpdateLog(msg)

            'Update UI
            SpecStat(1) = SpecState.Unavailable
            frmMain.UpdateCharStats(CharData)
            frmMain.txtPop.Text = GetCharSummary(CharData)
            ReportAdd(CurCharRow, "Defensive Aura", Success)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoTeleport(CharData As DataRow, NewRow As Integer, NewCol As Integer)
        Try

            SpecStat(2) = SpecState.Active

            ' Initiate log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to Teleport... "
            frmMain.UpdateLog(msg)

            ' Set flags and counters
            CharData("Moves Left") = 0
            CharData("Attacks Left") = 0
            CharData("Spec2 Cooldown Left") = CharData("Spec2 Cooldown")
            CharData("Protective Aura") = 0 ' Drop Reflective Aura

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 5, 125)

            ' Determine success
            Dim Success As Boolean = (Rand() <= CharData("Spec2 Chance"))
            If Success Then
                ' Move to the new hex
                DoMove(CharData, NewRow, NewCol)
                ' Show animation at target location
                FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 3, 125)
                msg = " and succeeds!" + vbCrLf
            Else
                msg = " but fails." + vbCrLf
            End If

            ' Update log
            frmMain.UpdateLog(msg)

            'Update UI
            SpecStat(2) = SpecState.Unavailable
            frmMain.UpdateCharStats(CharData)
            frmMain.txtPop.Text = GetCharSummary(CharData)
            ReportAdd(CurCharRow, "Teleport", Success)

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoFireball(CharData As DataRow, row As Integer, col As Integer)
        Try

            SpecStat(3) = SpecState.Active

            ' Update log
            Dim msg As String
            msg = "Your " + GetTitle(CharData) + " attempts to cast a Fireball... "
            frmMain.UpdateLog(msg)

            ' Get target hexes
            Dim dtLocs As DataTable = GetHexMap(CharData("Side"), row, col, 0, 1, True, True, True)

            ' Set flags and counters
            CharData("Attacks Left") = 0
            CharData("Spec3 Cooldown Left") = CharData("Spec3 Cooldown")

            ' Show casting animation
            FlashHex(CharData("CurRow"), CharData("CurCol"), cNeutral, 4, 125)
            Wait(300)

            ' Show animation at target location
            HexShow(row, col, cHarmful)
            Wait(300)

            ' Deal damage
            Dim TarData As DataRow
            Dim ds As DamageResultsStruct
            Dim TotalDamage As Integer = 0
            For i As Integer = 0 To dtLocs.Rows.Count - 1
                If dtLocs.Rows(i)("Side") = "" Then Continue For
                TarData = dtLocs.Rows(i)("CharRow")
                ds = DoDamage(CharData, TarData, "Fireball")
                dtLocs.Rows(i)("Message") = ds.AdjustedDamage.ToString
                TotalDamage += ds.AdjustedDamage
            Next

            ' Show results
            Dim TargetCount As Integer = dtLocs.Select("[Side] <> ''").Count
            If TotalDamage = 0 Then
                msg = " but fails to hit any anyone." + vbCrLf
            Else
                msg = " and inflicts a total of " + TotalDamage.ToString + " fire damage to " + TargetCount.ToString + " combatants." + vbCrLf
            End If
            frmMain.UpdateLog(msg)

            ' Flash surrounding targets
            FlashHexes(dtLocs, cHarmful, 1, 900)

            'Update UI
            SpecStat(3) = SpecState.Unavailable

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

#End Region

#Region "General"

    Friend Sub DoTurn_Standard()
        Try

            If CurCharRow("Attacks Left") = 0 AndAlso CurCharRow("Moves Left") = 0 Then Exit Sub

            ' Advance as necessary to engage target at close range
            Dim TarRow As DataRow
            Dim TarCharRow As DataRow
            Dim Dist As Integer
            Dim CurRow As Integer = CurCharRow("CurRow")
            Dim CurCol As Integer = CurCharRow("CurCol")
            Dim PrevRow As Integer = -1
            Dim PrevCol As Integer = -1
            Dim Tries As Integer = 0 ' needed in case a character is blocked by allies and cannot move
            Do
                Application.DoEvents()
                MapTargets()
                If dtTargets Is Nothing OrElse dtTargets.Rows.Count = 0 Then Exit Do
                TarRow = dtTargets.Rows(0)
                TarCharRow = dtTargets.Rows(0)("CharRow")
                If TarRow("Distance") > CurCharRow("Range Max") AndAlso CurCharRow("Moves Left") > 0 Then
                    Dist = MoveTowardLocation(CurCharRow, TarCharRow, PrevRow, PrevCol)
                    If Dist > 0 Then
                        PrevRow = CurRow
                        PrevCol = CurCol
                        CurRow = CurCharRow("CurRow")
                        CurCol = CurCharRow("CurCol")
                        Wait(TurnDelay)
                        ReportAdd(CurCharRow, "Move")
                    End If
                ElseIf TarRow("Distance") <= CurCharRow("Range Max") AndAlso CurCharRow("Attacks Left") > 0 Then
                    DoMelee(CurCharRow, TarRow("HexRow"), TarRow("HexCol"))
                    Wait(TurnDelay)
                    PrevRow = -1
                    PrevCol = -1
                Else
                    Exit Do
                End If
                Tries += 1
            Loop While Tries < 10 AndAlso Winner = ""

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoMelee(CharData As DataRow, Row As Integer, Col As Integer)
        Try
            ' Get target data
            Dim TarData As DataRow = Hex(Row, Col).CharRow
            Dim dist As Integer = CalcHexDist(CharData("CurRow"), CharData("CurCol"), Row, Col)

            ' Initiate log
            Dim msg As String
            If CharData("Side") = "Party" Then
                msg = "Your " + GetTitle(CharData) + " attacks a " + TarData("Archetype") + " with " + GetWeapon(CharData, dist) + "..."
            Else
                msg = "A " + GetTitle(CurCharRow) + " attacks your " + GetTitle(TarData) + " with " + GetWeapon(CurCharRow, dist) + "..."
            End If
            frmMain.UpdateLog(msg)

            ' Deduct attacks
            CharData("Attacks Left") -= 1

            ' Do damage
            DoDamage(CharData, TarData, IIf(dist = 1, "Melee", "Ranged"))

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Structure DamageResultsStruct
        Dim BaseChance As Integer ' The base chance to hit
        Dim Evaded As Boolean ' Missed due to Evasion
        Dim Blinded As Boolean ' Missed due to Binded condition
        Dim Missed As Boolean ' The attacker missed the hit
        Dim MaxDamage As Integer ' The maximum damage that the attack can cause
        Dim Absorbed As Integer ' Damage reduced by Defensive Aura
        Dim Blocked As Integer ' Physical damage reduced by Raised Shield
        Dim Weakened As Integer ' Physical damage reduced by Weakened effect
        Dim RolledDamage As Integer ' The actual amount of the maximum damage rolled
        Dim AdjustedDamage As Integer ' The final adjusted damage inflicted
        Dim Killed As Boolean ' Whether the target has been killed
        Dim Message As String ' The summary message to display
    End Structure

    Private Function DoDamage(CharData As DataRow, TarData As DataRow, AttackType As String) As DamageResultsStruct

        Dim ds As New DamageResultsStruct
        ds.BaseChance = 0
        ds.Evaded = False
        ds.Blinded = False
        ds.Missed = False
        ds.MaxDamage = 0
        ds.Absorbed = 0
        ds.Blocked = 0
        ds.Weakened = 0
        ds.RolledDamage = 0
        ds.AdjustedDamage = 0
        ds.Killed = False
        ds.Message = ""

        Dim ShowResults As Boolean = True
        Dim IsMagical As Boolean = (AttackType = "Harm" OrElse CharData("Archetype") = "Wizard")

        Select Case AttackType

            Case "Melee"
                ds.BaseChance = 90 + (CharData("Rank") - TarData("Rank")) * 5
                ds.MaxDamage = CharData("Melee Hit Max")

            Case "Ranged"
                ds.BaseChance = 80 + (CharData("Rank") - TarData("Rank")) * 5
                ds.MaxDamage = CharData("Ranged Hit Max")

            Case "Charge", "Harm", "Precision Shot"
                ds.BaseChance = CharData("Spec2 Chance")
                ds.MaxDamage = CharData("Spec2 Amount")

            Case "Cleave", "Arrow Volley", "Fireball"
                ds.BaseChance = CharData("Spec3 Chance")
                ds.MaxDamage = CharData("Spec3 Amount")
                ShowResults = False

        End Select

        ' Check if base chance to hit is successful
        If Rand() > ds.BaseChance Then
            ds.Missed = True
            GoTo AttackMissed
        End If

        ' Check if Blinded
        If CharData("Blinded") > 0 Then
            If Rand() <= TarData("Blinded Amount") Then
                ds.Blinded = True
                GoTo AttackMissed
            End If
        End If

        ' Check if Evasive Stance
        If CharData("Evasive Stance") > 0 Then
            If Rand() <= TarData("Spec1 Amount") Then
                ds.Evaded = True
                GoTo AttackMissed
            End If
        End If

        ' Check for physical mitigation
        If Not IsMagical Then

            ' Adjust for Weakened
            If CharData("Weakened") > 0 Then ds.Weakened = CharData("Weakened Amount")

            ' Adjust for Raised Shield
            If TarData("Shield Raised") > 0 Then ds.Blocked = TarData("Spec1 Amount")

        End If
        ds.RolledDamage = Rand(1, ds.MaxDamage)
        ds.AdjustedDamage = ds.RolledDamage - ds.Weakened - ds.Blocked

        ' Adjust for absorption
        If TarData("Protective Aura") > 0 Then
            If TarData("Spec1 Amount") > ds.AdjustedDamage Then
                ds.Absorbed = ds.AdjustedDamage
                TarData("Spec1 Amount") -= ds.AdjustedDamage
                ds.AdjustedDamage = 0
            Else
                TarData("Protective Aura") = 0
                ds.Absorbed = TarData("Spec1 Amount")
                ds.AdjustedDamage -= TarData("Spec1 Amount")
                HexShow(TarData("CurRow"), TarData("CurCol"))
            End If
        End If

        ' Apply damage
        If ds.AdjustedDamage < 0 Then ds.AdjustedDamage = 0
        TarData("Life Left") -= ds.AdjustedDamage
        ds.Killed = (TarData("Life Left") <= 0)

AttackMissed:

        ' Show results
        If ShowResults Then
            If ds.Missed Then
                ds.Message += " but misses."
                FlashHex(TarData("CurRow"), TarData("CurCol"), cHarmful,,, "")
            ElseIf ds.Evaded Then
                ds.Message += " but the Monk evades."
                FlashHex(TarData("CurRow"), TarData("CurCol"), cHarmful,,, "E")
            ElseIf ds.Blinded Then
                ds.Message += " but misses due to blindness."
                FlashHex(TarData("CurRow"), TarData("CurCol"), cHarmful,,, "B")
            Else
                If ds.Weakened > 0 Then ds.Message += " but weakness reduces the damage inflicted by " + ds.Weakened.ToString
                If ds.Blocked > 0 Then ds.Message += " and the target's shield blocks " + ds.Blocked.ToString
                If ds.Absorbed > 0 Then ds.Message += " and the target's aura absorbs " + ds.Absorbed.ToString
                ds.Message += " resulting in " + ds.AdjustedDamage.ToString + " damage."
                FlashHex(TarData("CurRow"), TarData("CurCol"), cHarmful,,, ds.AdjustedDamage.ToString)
            End If
            ds.Message += vbCrLf
            If ds.Killed Then ds.Message += "The target falls unconscious!" + vbCrLf
            Wait(300)
        End If

        'Update target's UI data
        frmMain.txtPop.Text = GetCharSummary(TarData)

        ' Append the log
        frmMain.UpdateLog(ds.Message)

        ' Update the attacker stats
        frmMain.UpdateCharStats(CharData)

        ' Handle death
        If ds.Killed Then HandleDeath(TarData)

        ' Log the action
        ReportAdd(CharData, AttackType, True, ds.AdjustedDamage)

        Return ds

    End Function

    Friend Sub DoMove(CharData As DataRow, Row As Integer, Col As Integer)
        Try

            ' Draw the new hex
            Hex(Row, Col).side = CharData("Side")
            Hex(Row, Col).CharRow = CharData
            HexShow(Row, Col, cCurrent)

            ' Clear old hex
            HexColor(CharData("CurRow"), CharData("CurCol"))
            Hex(CharData("CurRow"), CharData("CurCol")).side = ""
            Hex(CharData("CurRow"), CharData("CurCol")).CharRow = Nothing

            ' Update the player location
            CharData("CurRow") = Row
            CharData("CurCol") = Col

        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub DoSwap(Char1 As DataRow, Char2 As DataRow)
        Try
            Dim Row1 As Integer = Char1("CurRow")
            Dim Col1 As Integer = Char1("CurCol")
            DoMove(Char1, Char2("CurRow"), Char2("CurCol"))
            Char2("CurRow") = Row1
            Char2("CurCol") = Col1
            Hex(Row1, Col1).side = Char2("Side")
            Hex(Row1, Col1).CharRow = Char2
            HexShow(Row1, Col1)
        Catch ex As Exception
            MessageBox.Show(ex.GetBaseException.ToString)
        End Try
    End Sub

    Friend Sub MapTargets()

        Dim dtA As DataTable
        Dim dtE As DataTable
        Dim Dist As Integer
        Dim CharRow As DataRow

        If CurSide = "Party" Then
            dtA = dtParty
            dtE = dtEnemy
        Else
            dtA = dtEnemy
            dtE = dtParty
        End If

        dtAllies.Rows.Clear()
        For i As Integer = 0 To dtA.Rows.Count - 1
            CharRow = dtA.Rows(i)
            Dist = CalcHexDist(CurCharRow("CurRow"), CurCharRow("CurCol"), CharRow("CurRow"), CharRow("CurCol"))
            dtAllies.Rows.Add(Dist, CharRow("CurRow"), CharRow("CurCol"), CharRow("Life Left"), CharRow("Life Max") - CharRow("Life Left"), CharRow("Moves Left"), CharRow)
        Next
        dtAllies.AcceptChanges()
        dtAllies = db.SortDataTable(dtAllies, "Distance")

        dtTargets.Rows.Clear()
        For i As Integer = 0 To dtE.Rows.Count - 1
            CharRow = dtE.Rows(i)
            Dist = CalcHexDist(CurCharRow("CurRow"), CurCharRow("CurCol"), CharRow("CurRow"), CharRow("CurCol"))
            dtTargets.Rows.Add(Dist, CharRow("CurRow"), CharRow("CurCol"), CharRow("Life Left"), CharRow("Life Max") - CharRow("Life Left"), CharRow("Moves Left"), CharRow)
        Next
        dtTargets.AcceptChanges()
        dtTargets = RandomizeTable(dtTargets)
        dtTargets = db.SortDataTable(dtTargets, "Distance")

    End Sub

    Dim dtChars As New DataTable
    Friend Function PickNextChar(Side As String) As DataRow

        If dtChars.Columns.Count = 0 Then
            dtChars.Columns.Add("TotalCD", GetType(Integer))
            dtChars.Columns.Add("LastRound", GetType(Integer))
            dtChars.Columns.Add("CharRow", GetType(DataRow))
        End If
        dtChars.Rows.Clear()

        Dim CharData As DataRow
        Dim dtTeam As DataTable = SideParty(Side)
        For i As Integer = 0 To dtTeam.Rows.Count - 1
            CharData = dtTeam.Rows(i)
            dtChars.Rows.Add(CharData("Spec1 Cooldown Left") + CharData("Spec2 Cooldown Left") + CharData("Spec3 Cooldown Left"), CharData("LastRound"), CharData)
        Next
        dtChars = RandomizeTable(dtChars)
        dtChars = db.SortDataTable(dtChars, "[TotalCD], [LastRound]")

        ' Pick a random character
        Dim Tops As Integer = 3
        If Tops > dtTeam.Rows.Count Then Tops = dtChars.Rows.Count
        CharData = dtChars.Rows(Rand(0, Tops - 1))("CharRow")
        Return CharData

    End Function

    Friend Function SideParty(Side As String) As DataTable
        If Side = "Party" Then Return dtParty Else Return dtEnemy
    End Function

    Friend Function GetHexMap(AllySide As String, FromRow As Integer, FromCol As Integer, MinRange As Integer, MaxRange As Integer, Optional IncludeAllies As Boolean = True, Optional IncludeEnemies As Boolean = True, Optional IncludeEmpty As Boolean = True) As DataTable

        ' Get the hexes to include in map
        Dim dt As DataTable = dtHexes.Clone
        Dim Dist As Integer
        For row As Integer = FromRow - MaxRange - 1 To FromRow + MaxRange + 1
            For col As Integer = FromCol - MaxRange - 1 To FromCol + MaxRange + 1
                If row < 1 OrElse col < 1 OrElse row > yHexes OrElse col > xHexes Then Continue For
                Dist = CalcHexDist(FromRow, FromCol, row, col)
                If Dist < MinRange OrElse Dist > MaxRange Then Continue For
                If IncludeEmpty AndAlso Hex(row, col).CharRow Is Nothing Then
                    dt.Rows.Add(Dist, row, col, "", 0, 0, Nothing)
                ElseIf IncludeAllies AndAlso Hex(row, col).side = AllySide Then
                    dt.Rows.Add(Dist, row, col, Hex(row, col).side, Hex(row, col).CharRow("Moves Max"), Hex(row, col).CharRow("Life Left"), Hex(row, col).CharRow)
                ElseIf IncludeEnemies AndAlso Hex(row, col).side <> "" AndAlso Hex(row, col).side <> AllySide Then
                    dt.Rows.Add(Dist, row, col, Hex(row, col).side, Hex(row, col).CharRow("Moves Max"), Hex(row, col).CharRow("Life Left"), Hex(row, col).CharRow)
                End If
            Next
        Next

        Return dt

    End Function

    Friend Function FilterCondition(dt As DataTable, Condition As String, Include As Boolean) As DataTable
        Dim dtf As DataTable = dt.Copy
        For i As Integer = dtf.Rows.Count - 1 To 0 Step -1
            If (Include AndAlso dtf.Rows(i)("CharRow")(Condition) = 0) OrElse (Not Include AndAlso dtf.Rows(i)("CharRow")(Condition) > 0) Then
                dtf.Rows.Remove(dtf.Rows(i))
            End If
        Next
        dtf.AcceptChanges()
        dtf = RandomizeTable(dtf)
        Return dtf
    End Function

    Friend Function AddNearsToHexMap(AllySide As String, dtHexMap As DataTable) As DataTable

        ' Add nearby ally count
        For i As Integer = 0 To dtHexMap.Rows.Count - 1
            dtHexMap.Rows(i)("AlliesNear") = GetHexMap(AllySide, dtHexMap.Rows(i)("Row"), dtHexMap.Rows(i)("Col"), 1, 1, True, False, False).Rows.Count
        Next

        ' Add nearby enemy count
        For i As Integer = 0 To dtHexMap.Rows.Count - 1
            dtHexMap.Rows(i)("EnemiesNear") = GetHexMap(AllySide, dtHexMap.Rows(i)("Row"), dtHexMap.Rows(i)("Col"), 1, 1, False, True, False).Rows.Count
        Next

        Return dtHexMap

    End Function

    Friend Function GetHexMapOverlap(AllySide As String, FromRow As Integer, FromCol As Integer, ToRow As Integer, ToCol As Integer) As DataTable
        Dim HexesFrom As DataTable = GetHexMap(AllySide, FromRow, FromCol, 1, 1, True, True, False)
        Dim HexesTo As DataTable = GetHexMap(AllySide, ToRow, ToCol, 1, 1, True, True, False)
        Dim TargetIncluded As Boolean = False
        Dim dt As DataTable = dtHexes.Clone
        For i As Integer = 0 To HexesFrom.Rows.Count - 1
            For j As Integer = 0 To HexesTo.Rows.Count - 1
                If HexesFrom.Rows(i)("Row") = HexesTo.Rows(j)("Row") AndAlso HexesFrom.Rows(i)("Col") = HexesTo.Rows(j)("Col") Then
                    dt.Rows.Add(1, HexesFrom.Rows(i)("Row"), HexesFrom.Rows(i)("Col"), HexesFrom.Rows(i)("Side"), HexesFrom.Rows(i)("Moves Max"), HexesFrom.Rows(i)("Life Left"), HexesFrom.Rows(i)("CharRow"))
                ElseIf Not TargetIncluded AndAlso HexesFrom.Rows(i)("Row") = ToRow AndAlso HexesFrom.Rows(i)("Col") = ToCol Then
                    dt.Rows.Add(1, HexesFrom.Rows(i)("Row"), HexesFrom.Rows(i)("Col"), HexesFrom.Rows(i)("Side"), HexesFrom.Rows(i)("Moves Max"), HexesFrom.Rows(i)("Life Left"), HexesFrom.Rows(i)("CharRow"))
                    TargetIncluded = True
                End If
            Next
        Next
        Return dt
    End Function

    Private Function CountTargetsInRange(dtTargets As DataTable, Dist As Integer) As Integer
        Dim n As Integer = 0
        For i As Integer = 0 To dtTargets.Rows.Count - 1
            If dtTargets.Rows(i)("Distance") <= Dist Then n += 1
        Next
        Return n
    End Function

    Friend Function FindMostHurt(Optional MaxRange As Integer = 100, Optional MaxLeft As Integer = 100) As DataRow

        Dim HealTars As DataTable = db.SortDataTable(dtAllies.Copy, "[Distance], [Life Left], [Hit Points Down] DESC")
        Dim HealRow As DataRow
        For i As Integer = 1 To HealTars.Rows.Count - 1
            HealRow = HealTars.Rows(i)
            If HealRow("Distance") <= MaxRange AndAlso HealRow("Life Left") <= MaxLeft AndAlso HealRow("Hit Points Down") >= 3 Then Return HealRow("CharRow")
        Next
        Return Nothing
    End Function

    ' Move one step closer to a location and return new distance
    Private Function MoveTowardLocation(CharData As DataRow, TarData As DataRow, Optional PrevRow As Integer = -1, Optional PrevCol As Integer = -1) As Integer

        Dim CurRow As Integer = CharData("CurRow")
        Dim CurCol As Integer = CharData("CurCol")

        Dim TarRow As Integer = TarData("CurRow")
        Dim TarCol As Integer = TarData("CurCol")

        If CalcHexDist(CurRow, CurCol, TarRow, TarCol) = 1 Then Return 1

        ' Check all adjacent hexes to see which is closest to target position
        Dim MinDis As Integer = 100
        Dim tmpDis As Integer
        Dim ToRow As Integer = -1
        Dim ToCol As Integer = -1
        For r As Integer = CurRow - 1 To CurRow + 1
            If r < 1 Or r > yHexes Then Continue For
            For c As Integer = CurCol - 1 To CurCol + 1
                If c < 1 OrElse c > xHexes Then Continue For
                If r = PrevRow AndAlso c = PrevCol Then Continue For
                If r = CurRow AndAlso c = CurCol Then Continue For
                If Hex(r, c).CharRow Is Nothing Then
                    tmpDis = CalcHexDist(r, c, TarRow, TarCol)
                    If (tmpDis < MinDis) AndAlso (CalcHexDist(CurRow, CurCol, r, c) = 1) Then
                        MinDis = tmpDis
                        ToRow = r
                        ToCol = c
                    End If
                End If
            Next
        Next
        If ToRow = -1 Or ToCol = -1 Then Return 0

        ' Move toward enemy
        If CharData("Moves Left") > 0 Then
            DoMove(CharData, ToRow, ToCol)
            CharData("Moves Left") -= 1
        End If
        frmMain.UpdateCharStats(CharData)
        Return MinDis

    End Function

    Private Sub HandleDeath(TarDataRow As DataRow)

        If TarDataRow Is Nothing Then Exit Sub

        ' Show death effect
        HexShow(TarDataRow("CurRow"), TarDataRow("CurCol"), cHarmful)
        HexDarken(TarDataRow("CurRow"), TarDataRow("CurCol"), cHarmful)
        Wait(300)

        ' Clear the hex
        HexColor(TarDataRow("CurRow"), TarDataRow("CurCol"))
        Hex(TarDataRow("CurRow"), TarDataRow("CurCol")).side = ""
        Hex(TarDataRow("CurRow"), TarDataRow("CurCol")).CharRow = Nothing

        ' Remove the dead character from the character table
        If TarDataRow("Side") = "Party" Then
            dtPartyGY.ImportRow(TarDataRow)
            dtParty.Rows.Remove(TarDataRow)
            dtParty.AcceptChanges()
            dtPartyGY.AcceptChanges()
            If dtParty.Rows.Count = 0 Then Winner = "Enemy"
        Else
            dtEnemyGY.ImportRow(TarDataRow)
            dtEnemy.Rows.Remove(TarDataRow)
            dtEnemy.AcceptChanges()
            dtEnemyGY.AcceptChanges()
            If dtEnemy.Rows.Count = 0 Then Winner = "Party"
        End If
        If Winner <> "" Then frmMain.ShowWinner()

    End Sub

#End Region

End Module
