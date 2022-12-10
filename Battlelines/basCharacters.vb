Module Characters

    Friend dtClasses As New DataTable

    Friend Sub CreateClassDefs()

        ' Create the classes definition table
        If dtClasses.Columns.Count = 0 Then

            ' Character Identification 
            dtClasses.Columns.Add("ID", GetType(System.Int32))
            dtClasses.Columns.Add("Rank", GetType(System.Int32))
            dtClasses.Columns.Add("Archetype", GetType(System.String))
            dtClasses.Columns.Add("Class", GetType(System.String))

            ' Basic Stats
            dtClasses.Columns.Add("Life Max", GetType(System.Int32))
            dtClasses.Columns.Add("Moves Max", GetType(System.Int32))
            dtClasses.Columns.Add("Attacks Max", GetType(System.Int32))
            dtClasses.Columns.Add("Melee Hit Max", GetType(System.Int32))
            dtClasses.Columns.Add("Ranged Hit Max", GetType(System.Int32))
            dtClasses.Columns.Add("Range Max", GetType(System.Int32))

            ' Spec Maximums
            dtClasses.Columns.Add("Spec1 Name", GetType(System.String))
            dtClasses.Columns.Add("Spec1 Chance", GetType(System.Int32))
            dtClasses.Columns.Add("Spec1 Amount", GetType(System.Int32))
            dtClasses.Columns.Add("Spec1 Range", GetType(System.Int32))
            dtClasses.Columns.Add("Spec1 Cooldown", GetType(System.Int32))
            dtClasses.Columns.Add("Spec2 Name", GetType(System.String))
            dtClasses.Columns.Add("Spec2 Chance", GetType(System.Int32))
            dtClasses.Columns.Add("Spec2 Amount", GetType(System.Int32))
            dtClasses.Columns.Add("Spec2 Range", GetType(System.Int32))
            dtClasses.Columns.Add("Spec2 Cooldown", GetType(System.Int32))
            dtClasses.Columns.Add("Spec3 Name", GetType(System.String))
            dtClasses.Columns.Add("Spec3 Chance", GetType(System.Int32))
            dtClasses.Columns.Add("Spec3 Amount", GetType(System.Int32))
            dtClasses.Columns.Add("Spec3 Range", GetType(System.Int32))
            dtClasses.Columns.Add("Spec3 Cooldown", GetType(System.Int32))

            ' Map Assignments
            dtClasses.Columns.Add("Side", GetType(System.String))
            dtClasses.Columns.Add("CurRow", GetType(System.Int32))
            dtClasses.Columns.Add("CurCol", GetType(System.Int32))

            ' Remaining Basic Stats
            dtClasses.Columns.Add("Life Left", GetType(System.Int32))
            dtClasses.Columns.Add("Moves Left", GetType(System.Int32))
            dtClasses.Columns.Add("Attacks Left", GetType(System.Int32))

            ' Special counters
            dtClasses.Columns.Add("Spec1 Amount Left", GetType(System.Int32))
            dtClasses.Columns.Add("Spec2 Amount Left", GetType(System.Int32))
            dtClasses.Columns.Add("Spec3 Amount Left", GetType(System.Int32))
            dtClasses.Columns.Add("Spec1 Cooldown Left", GetType(System.Int32))
            dtClasses.Columns.Add("Spec2 Cooldown Left", GetType(System.Int32))
            dtClasses.Columns.Add("Spec3 Cooldown Left", GetType(System.Int32))

            ' Buff counters
            dtClasses.Columns.Add("Shield Raised", GetType(System.Int32))
            dtClasses.Columns.Add("Evasive Stance", GetType(System.Int32))
            dtClasses.Columns.Add("Protective Aura", GetType(System.Int32))

            ' Condition counters
            dtClasses.Columns.Add("Disoriented", GetType(System.Int32))
            dtClasses.Columns.Add("Slowed", GetType(System.Int32))
            dtClasses.Columns.Add("Blinded", GetType(System.Int32))
            dtClasses.Columns.Add("Weakened", GetType(System.Int32))
            dtClasses.Columns.Add("Hobbled", GetType(System.Int32))
            dtClasses.Columns.Add("Disoriented Amount", GetType(System.Int32))
            dtClasses.Columns.Add("Slowed Amount", GetType(System.Int32))
            dtClasses.Columns.Add("Blinded Amount", GetType(System.Int32))
            dtClasses.Columns.Add("Weakened Amount", GetType(System.Int32))
            dtClasses.Columns.Add("Hobbled Amount", GetType(System.Int32))
            dtClasses.Columns.Add("LastRound", GetType(System.Int32))
        End If

        ' Populate the classes table
        dtClasses.Rows.Clear()
        dtClasses.Rows.Add(1, 1, "Fighter", "Recruit", 28, 2, 1, 12, 0, 1, "Raise Shield", 60, 3, 2, 6, "Charge", 80, 10, 4, 5, "Cleave", 75, 6, 3, 5)
        dtClasses.Rows.Add(1, 2, "Fighter", "Veteran", 32, 2, 1, 14, 0, 1, "Raise Shield", 70, 4, 3, 5, "Charge", 85, 12, 5, 5, "Cleave", 80, 7, 3, 4)
        dtClasses.Rows.Add(1, 3, "Fighter", "Champion", 36, 2, 1, 16, 0, 1, "Raise Shield", 80, 5, 4, 4, "Charge", 90, 14, 6, 5, "Cleave", 85, 8, 3, 3)
        dtClasses.Rows.Add(1, 1, "Monk", "Novice", 26, 5, 3, 4, 0, 1, "Evasive", 50, 30, 3, 6, "Renew", 85, 8, 1, 8, "Disorient", 75, -1, -1, 12)
        dtClasses.Rows.Add(1, 2, "Monk", "Learned", 30, 5, 3, 5, 0, 1, "Evasive", 60, 35, 3, 6, "Renew", 90, 12, 2, 8, "Disorient", 80, -1, -1, 12)
        dtClasses.Rows.Add(1, 3, "Monk", "Master", 34, 5, 3, 6, 0, 1, "Evasive", 70, 40, 3, 6, "Renew", 95, 16, 3, 8, "Disorient", 85, -1, -1, 12)
        dtClasses.Rows.Add(1, 1, "Rogue", "Rookie", 22, 4, 2, 4, 0, 1, "Slow", 85, 1, -1, 8, "Blind", 60, 40, -1, 8, "Weaken", 85, 2, -1, 8)
        dtClasses.Rows.Add(1, 2, "Rogue", "Seasoned", 26, 4, 2, 5, 0, 1, "Slow", 90, 2, -1, 8, "Blind", 70, 45, -1, 8, "Weaken", 90, 3, -1, 8)
        dtClasses.Rows.Add(1, 3, "Rogue", "Boss", 30, 4, 2, 6, 0, 1, "Slow", 95, 3, -1, 8, "Blind", 80, 80, 50, 8, "Weaken", 95, 4, -1, 8)
        dtClasses.Rows.Add(1, 1, "Cleric", "Acolyte", 10, 3, 1, 4, 0, 1, "Heal", 85, 8, 2, 3, "Harm", 70, 6, 3, 4, "Revive", 75, 60, -1, 9)
        dtClasses.Rows.Add(1, 2, "Cleric", "Priest", 12, 3, 1, 5, 0, 1, "Heal", 90, 12, 3, 3, "Harm", 80, 7, 4, 4, "Revive", 80, 75, -1, 8)
        dtClasses.Rows.Add(1, 3, "Cleric", "Bishop", 14, 3, 1, 6, 0, 1, "Heal", 95, 16, 4, 3, "Harm", 90, 8, 5, 4, "Revive", 85, 95, -1, 7)
        dtClasses.Rows.Add(1, 1, "Ranger", "Scout", 24, 4, 1, 4, 6, 3, "Hobble", 85, 2, -1, 8, "Precision Shot", 90, 9, 5, 6, "Arrow Volley", 70, 4, 4, 8)
        dtClasses.Rows.Add(1, 2, "Ranger", "Guide", 28, 4, 1, 5, 7, 4, "Hobble", 90, 3, -1, 7, "Precision Shot", 95, 10, 6, 5, "Arrow Volley", 75, 5, 5, 7)
        dtClasses.Rows.Add(1, 3, "Ranger", "Warden", 32, 4, 1, 6, 8, 5, "Hobble", 95, 4, -1, 6, "Precision Shot", 99, 11, 7, 4, "Arrow Volley", 80, 6, 6, 6)
        dtClasses.Rows.Add(1, 1, "Wizard", "Apprentice", 20, 2, 1, 3, 3, 2, "Protective Aura", 75, 12, 3 - 1, 8, "Teleport", 80, -1, 5, 7, "Fireball", 75, 5, 4, 7)
        dtClasses.Rows.Add(1, 2, "Wizard", "Magus", 24, 2, 1, 4, 4, 3, "Protective Aura", 80, 16, -1, 7, "Teleport", 85, -1, 6, 6, "Fireball", 80, 6, 5, 6)
        dtClasses.Rows.Add(1, 3, "Wizard", "Archmage", 28, 2, 1, 5, 5, 4, "Protective Aura", 85, 20, -1, 6, "Teleport", 90, -1, 7, 5, "Fireball", 85, 7, 6, 5)

    End Sub

    Friend Function GetSpecialDesc(Special As String) As String
        Dim s As String = ""
        Select Case Special

            ' Beneficial: Shield Raised, Evasive Stance, Reflective Aura
            ' Detrimentals: Disoriented, Slowed, Blinded, Weakened (physical only), and Hobbled
            ' Active > 0 indicates effect is active. Each round increments Active
            ' Each Round you have a chance of the effect dropping = (Active-1) * TM
            ' You can also lose your beneficial effect if hit by a max blow 
            ' You can also lose your detrimental effect if cured
            ' Turn Multipliers: 5% = 18, 6% = 13, 8% = 8, 10% = 7, 15% = 3, 20% = 2.6

            ' Fighter
            Case "Raise Shield"
                s = "In lieu of attacking, you raise your shield, reducing physical damage received by " + CurCharRow("Spec1 Amount").ToString + "." + vbCrLf + vbCrLf
                s += "Cleaving forces a raised shield to be lowered."

            Case "Charge"
                s = "In lieu of your movement and standard attack, you charge an enemy up to " + CurCharRow("Spec2 Range").ToString + " hexes away"
                s += " and bash with your shield for up to " + CurCharRow("Spec2 Amount").ToString + " damage."
            Case "Cleave"
                s = "In lieu of your standard close attack, you sweep your blade in a wide arc doing up to " + CurCharRow("Spec3 Amount").ToString + " damage"
                s += " to an opponent and anyone on either side of your target." + vbCrLf + vbCrLf
                s += "The fighter must lower their shield to Cleave if it is raised."

               ' Monk
            Case "Evasive"
                s = "In lieu of moving, you adopt an Evasive stance which makes you " + CurCharRow("Spec1 Amount").ToString + "% harder to hit but reduces your movement and attacks by 2." + vbCrLf + vbCrLf
                s += "You remain in your evasive stance until using Renew or Disorient. Your stance can also be disrupted by a particularly strong hit."
            Case "Renew"
                s = "In lieu of attacking, you attempt to mend your wounds for up to " + CurCharRow("Spec2 Amount").ToString + " hit points and purge up to " + CurCharRow("Spec2 Range").ToString + " conditions."
            Case "Disorient"
                s = "In lieu of attacking, you attempt to Disorient an opponent, preventing them from using any special skills." + vbCrLf + vbCrLf
                s += "The disorientation persists until they regain their composure or are cured."

                ' Rogue
            Case "Slow"
                s = "In lieu of one attack, you attempt to Slow an enemy, reducing their attacks by " + CurCharRow("Spec1 Amount").ToString + "." + vbCrLf + vbCrLf
                s += "The slowness persists until they catch their breath or are cured."
            Case "Blind"
                s = "In lieu of one attack, you attempt to Blind an enemy, reducing their accuracy by " + CurCharRow("Spec2 Amount").ToString + "%." + vbCrLf + vbCrLf
                s += "The blindness persists until they clear their vision or are cured."
            Case "Weaken"
                s = "In lieu of one attack, you attempt to Weaken an enemy, reducing the physical damage they deal by " + CurCharRow("Spec3 Amount").ToString + " hit points per hit." + vbCrLf + vbCrLf
                s += "The weakness persists until they recover their strength or are cured."

                ' Cleric
            Case "Heal"
                s = "In lieu of attacking, you attempt to heal an ally up to " + CurCharRow("Spec1 Range").ToString + " hexes away for up to " + CurCharRow("Spec1 Amount").ToString + " hit points and possibly cure all conditions."
            Case "Harm"
                s = "In lieu of your standard attack, you attempt to cast Harm on an enemy at up to " + CurCharRow("Spec2 Range").ToString + " hexes away, inflicting up to " + CurCharRow("Spec2 Amount").ToString + " holy damage."
            Case "Revive"
                s = "In lieu of moving or attacking, you attempt to Revive a random fallen ally." + vbCrLf + vbCrLf
                s += "If the attempt fails, the ally dies."

                ' Ranger
            Case "Hobble"
                s = "In lieu of your standard close attack, you attempt to Hobble your opponent, reducing their movement by " + CurCharRow("Spec1 Amount").ToString + "." + vbCrLf + vbCrLf
                s += "The hobbling persists until they walk it off or are cured."
            Case "Precision Shot"
                s = "In lieu of moving and attacking, take careful aim at a target up to " + CurCharRow("Spec2 Range").ToString + " hexes away dealing up to " + CurCharRow("Spec2 Amount").ToString + " physical damage."
            Case "Arrow Volley"
                s = "In lieu of moving and attacking, you fire a volley of arrows damaging all characters in and around a location up to " + CurCharRow("Spec3 Range").ToString + " hexes away for up to " + CurCharRow("Spec3 Amount").ToString + " physical damage."

                ' Wizard
            Case "Protective Aura"
                s = "In lieu of attacking, you invoke a protective aura that absorbs both physical and magical damage." + vbCrLf + vbCrLf
                s += "The aura persists until you cast Teleport or Fireball or until it absorbs " + CurCharRow("Spec1 Amount").ToString + " total damage."
            Case "Teleport"
                s = "In lieu of moving or attacking, instantly teleport to another location from 3 to " + CurCharRow("Spec2 Range").ToString + " hexes away." + vbCrLf + vbCrLf
            Case "Fireball"
                s = "In lieu of attacking, cast a Fireball damaging all characters in and around a location up to " + CurCharRow("Spec3 Range").ToString + " hexes away for up to " + CurCharRow("Spec3 Amount").ToString + " fire damage."

        End Select
        Return s
    End Function

    Friend Function GetTitle(CharRow As DataRow) As String
        If CharRow Is Nothing Then Return "XXX"
        Return CharRow("Archetype") + " " + CharRow("Class")
    End Function

    Dim ArchCounts As New System.Collections.Specialized.NameValueCollection

    Friend Function CreateRandomParty(side As String) As DataTable
        Dim dt As DataTable = dtClasses.Clone

        ' Reinitialize class counters
        ArchCounts.Clear()
        For i As Integer = 0 To 5
            ArchCounts.Add("Fighter", 0)
            ArchCounts.Add("Monk", 0)
            ArchCounts.Add("Rogue", 0)
            ArchCounts.Add("Cleric", 0)
            ArchCounts.Add("Ranger", 0)
            ArchCounts.Add("Wizard", 0)
        Next

        ' Select leader
        dt.ImportRow(GetRandomClass(3))

        ' Select lieutenants
        For i As Integer = 1 To 3
            dt.ImportRow(GetRandomClass(2))
        Next

        ' Select soldiers
        For i As Integer = 1 To 6
            dt.ImportRow(GetRandomClass(1))
        Next

        ' Update calculated values
        For i As Integer = 0 To 9
            dt.Rows(i)("ID") = i + 1 + IIf(side = "Party", 0, 10)
            dt.Rows(i)("Side") = side
            dt.Rows(i)("CurRow") = -1
            dt.Rows(i)("CurCol") = -1
            InitChar(dt.Rows(i))
        Next

        ' Randomize positions
        dt = RandomizeTable(dt)
        Return dt

    End Function

    Friend Sub InitChar(CharData As DataRow)
        CharData("Life Left") = CharData("Life Max")
        CharData("Moves Left") = CharData("Moves Max")
        CharData("Attacks Left") = CharData("Attacks Max")
        CharData("Spec1 Cooldown Left") = 0
        CharData("Spec2 Cooldown Left") = 0
        CharData("Spec3 Cooldown Left") = 0
        CharData("Spec1 Amount Left") = 0
        CharData("Spec2 Amount Left") = 0
        CharData("Spec3 Amount Left") = 0
        CharData("Shield Raised") = 0
        CharData("Evasive Stance") = 0
        CharData("Protective Aura") = 0
        CharData("Disoriented") = 0
        CharData("Slowed") = 0
        CharData("Blinded") = 0
        CharData("Weakened") = 0
        CharData("Hobbled") = 0
        CharData("Disoriented Amount") = 0
        CharData("Slowed Amount") = 0
        CharData("Blinded Amount") = 0
        CharData("Weakened Amount") = 0
        CharData("Hobbled Amount") = 0
        CharData("LastRound") = 0
    End Sub

    Private Function GetRandomClass(Rank As Integer) As DataRow

        ' Get classes of the desired rank
        Dim rows() As DataRow = dtClasses.Select("[Rank] = " + Rank.ToString)

        ' Find a class that isn't already maxed out
        Dim RandClass As Integer
        Do
            RandClass = Rand(0, 5)
            If ArchCounts(rows(RandClass)("Archetype")) < 3 Then
                ArchCounts(rows(RandClass)("Archetype")) += 1
                Return rows(RandClass)
            End If
        Loop

    End Function

    Friend Function GetWeapon(CharData As DataRow, dist As Integer) As String
        Select Case CharData("Archetype")
            Case "Fighter"
                Select Case CharData("Rank")
                    Case 1
                        Return "a Longsword"
                    Case 2
                        Return "a Fine Longsword"
                    Case 3
                        Return "an Exquisite Longsword"
                End Select
            Case "Monk"
                Select Case CharData("Rank")
                    Case 1
                        Return "an Unarmed Blow"
                    Case 2
                        Return "a Serpentine Strike"
                    Case 3
                        Return "a Dragonclaw Combination"
                End Select
            Case "Cleric"
                Select Case CharData("Rank")
                    Case 1
                        Return "a Warhammer"
                    Case 2
                        Return "a Blessed Warhammer"
                    Case 3
                        Return "a Consecrated Warhammer"
                End Select
            Case "Rogue"
                Select Case CharData("Rank")
                    Case 1
                        Return "a Dagger"
                    Case 2
                        Return "a Keen Dagger"
                    Case 3
                        Return "a Wicked Dagger"
                End Select
            Case "Ranger"
                Select Case CharData("Rank")
                    Case 1
                        If dist = 1 Then Return "an Axe" Else Return "a Longbow"
                    Case 2
                        If dist = 1 Then Return "a Sharpened Axe" Else Return "a Precision Longbow"
                    Case 3
                        If dist = 1 Then Return "a Razor-Edged Axe" Else Return "a Heartseeker Longbow"
                End Select
            Case "Wizard"
                Select Case CharData("Rank")
                    Case 1
                        Return "a Wand"
                    Case 2
                        Return "a Glowing Wand"
                    Case 3
                        Return "an Incandescent Wand"
                End Select
        End Select
    End Function

    Friend Function GetDamage(CharRow As DataRow, Dist As Integer) As Integer
        If Dist = 1 Then
            Return CharRow("Melee Hit Max")
        ElseIf Dist <= CharRow("Range Max") Then
            Return CharRow("Ranged Hit Max")
        End If
        Return 0
    End Function

    Friend Function GetCharSummary(CharData As DataRow) As String

        ' Show basic info
        Dim s As String
        s = GetTitle(CharData) + vbCrLf
        s += "Eff HP: " + CharData("Life Left").ToString + " of " + CharData("Life Max").ToString + vbCrLf
        s += "Movement: " + CharData("Moves Left").ToString + " of " + CharData("Moves Max").ToString + vbCrLf
        s += "Attacks: " + CharData("Attacks Left").ToString + " of " + CharData("Attacks Max").ToString + vbCrLf
        s += "Attack Range: " + CharData("Range Max").ToString + vbCrLf
        s += "Attack Damage: " + CharData("Melee Hit Max").ToString + " / " + IIf(CharData("Ranged Hit Max") = 0, "-", CharData("Ranged Hit Max")).ToString + vbCrLf

        ' Show buff effect
        Dim sEffects As String = ""
        If CharData("Shield Raised") > 0 Then sEffects += "Shield Raised" + vbCrLf
        If CharData("Evasive Stance") > 0 Then sEffects += "Evasive Stance" + vbCrLf
        If CharData("Protective Aura") > 0 Then sEffects += "Protective Aura (" + CharData("Spec1 Amount").ToString + " remaining)" + vbCrLf

        ' Show detriments
        If CharData("Disoriented") > 0 Then sEffects += "Disoriented" + vbCrLf
        If CharData("Slowed") > 0 Then sEffects += "Slowed" + vbCrLf
        If CharData("Blinded") > 0 Then sEffects += "Blinded" + vbCrLf
        If CharData("Weakened") > 0 Then sEffects += "Weakened" + vbCrLf
        If CharData("Hobbled") > 0 Then sEffects += "Hobbled" + vbCrLf
        If sEffects <> "" Then s += "---" + vbCrLf + sEffects

        Return s
    End Function

    Friend Function GetCharConditions(chardata As DataRow) As DataTable
        Dim dt As DataTable = dtConditions.Clone
        If chardata("Hobbled") > 0 Then dt.Rows.Add("Hobbled")
        If chardata("Slowed") > 0 Then dt.Rows.Add("Slowed")
        If chardata("Disoriented") > 0 Then dt.Rows.Add("Disoriented")
        If chardata("Weakened") > 0 Then dt.Rows.Add("Weakened")
        If chardata("Blinded") > 0 Then dt.Rows.Add("Blinded")
        Return dt
    End Function

End Module
