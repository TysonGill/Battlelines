Module basMapping

    Friend b As Bitmap = Nothing
    Friend g As Graphics = Nothing

    Friend xMargin As Integer = 2
    Friend yMargin As Integer = 2

    Friend xHexes As Integer = 17
    Friend yHexes As Integer = 12

    Friend HexWidth As Integer = 64
    Friend HexAspect As Double = 0.8

    Friend HexHeight As Integer = CInt(HexWidth * HexAspect)
    Friend HexWidth4 As Integer = HexWidth / 4 ' quarter hex width
    Friend HexHeight2 As Integer = HexHeight / 2 ' half hex height

    Friend Structure HexStruct
        Friend x As Integer
        Friend y As Integer
        Friend side As String
        Friend center As Point
        Friend CharRow As DataRow
    End Structure
    Friend Hex(yHexes, xHexes) As HexStruct

    Friend Structure HexLocStruct
        Friend Row As Integer
        Friend Col As Integer
    End Structure
    Friend Hexes As New List(Of HexLocStruct)

    Dim FontClass As New Font("Gabriola", 12)
    Dim FontRank As New Font("Wingdings 2", 10)

    ' Skyblue ARGB: 255, 135, 206, 235
    ' Tomato ARGB: 255, 255, 99, 71
    ' DarkSeaGreen ARGB: 255, 143, 188, 143
    Friend cParty As Color = Color.Blue
    Friend cEnemy As Color = Color.Red
    Friend cButtonReady As Color = Color.FromArgb(80, Color.SkyBlue.R, Color.SkyBlue.G, Color.SkyBlue.B)
    Friend cButtonActive As Color = Color.SkyBlue
    Friend cCurrent As Color = Color.Yellow     ' Highlighted/current hex
    Friend cHelpful As Color = Color.LimeGreen ' Healing and helpful effects
    Friend cHarmful As Color = Color.OrangeRed ' Damaging effects
    Friend cNeutral As Color = Color.Peru      ' Neutral effects

    Friend Sub CreateMap()
        If b Is Nothing Then b = New Bitmap(frmMain.picMap.ClientSize.Width, frmMain.picMap.ClientSize.Height)
        If g Is Nothing Then g = Graphics.FromImage(b)

        Dim x As Integer = xMargin
        Dim y As Integer = yMargin + HexHeight2
        For c As Integer = 1 To xHexes

            For r As Integer = 1 To yHexes
                HexBorder(x, y)
                Hex(r, c).x = x
                Hex(r, c).y = y
                Hex(r, c).center = New Point(x + HexWidth / 2, y)
                Hex(r, c).side = ""
                Hex(r, c).CharRow = Nothing
                x += GetOffx("S")
                y += GetOffy("S")
            Next
            x += GetOffx("NE")
            y += GetOffy("NE")

            c += 1
            If c > xHexes Then Exit For
            For r As Integer = yHexes To 1 Step -1
                HexBorder(x, y)
                Hex(r, c).x = x
                Hex(r, c).y = y
                Hex(r, c).center = New Point(x + HexWidth / 2, y)
                Hex(r, c).side = ""
                Hex(r, c).CharRow = Nothing
                x += GetOffx("N")
                y += GetOffy("N")
            Next
            x += GetOffx("SE")
            y += GetOffy("SE")
        Next

    End Sub

    Friend Sub DisplaySides()
        DisplaySide(dtParty)
        DisplaySide(dtEnemy)
    End Sub

    Friend Sub DisplaySide(dt As DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i)("CurRow") <> -1 Then
                HexShow(dt.Rows(i)("CurRow"), dt.Rows(i)("CurCol"))
            End If
        Next
    End Sub

    ' Draw an unfilled hex starting at left middle vertex
    Private Sub HexBorder(x As Integer, y As Integer, Optional c As Color = Nothing)

        If c = Nothing Then c = Color.DarkGray

        ' Draw Hex
        Dim lines(5) As Point
        lines(0).X = x : lines(0).Y = y
        lines(1).X = x + HexWidth4 : lines(1).Y = y - HexHeight2
        lines(2).X = x + HexWidth4 * 3 : lines(2).Y = y - HexHeight2
        lines(3).X = x + HexWidth4 * 4 : lines(3).Y = y
        lines(4).X = x + HexWidth4 * 3 : lines(4).Y = y + HexHeight2
        lines(5).X = x + HexWidth4 : lines(5).Y = y + HexHeight2
        g.DrawPolygon(New Pen(Color.DarkGray), lines)

        frmMain.picMap.Image = b

    End Sub

    Friend Sub HexColor(row As Integer, col As Integer, Optional c As Color = Nothing)

        If row < 1 OrElse col < 1 OrElse row > yHexes OrElse col > xHexes Then Exit Sub
        If c = Nothing Then c = Color.White

        ' Get x and y locations of the hex to draw
        Dim x As Integer = Hex(row, col).x
        Dim y As Integer = Hex(row, col).y

        ' Get Hex vertices
        Dim lines(5) As Point
        lines(0).X = x : lines(0).Y = y
        lines(1).X = x + HexWidth4 : lines(1).Y = y - HexHeight2
        lines(2).X = x + HexWidth4 * 3 : lines(2).Y = y - HexHeight2
        lines(3).X = x + HexWidth4 * 4 : lines(3).Y = y
        lines(4).X = x + HexWidth4 * 3 : lines(4).Y = y + HexHeight2
        lines(5).X = x + HexWidth4 : lines(5).Y = y + HexHeight2

        ' Fill hex
        g.FillPolygon(New SolidBrush(c), lines)
        HexBorder(x, y)

    End Sub

    Friend Sub HexRefresh(row As Integer, col As Integer)
        If row = CurCharRow("CurRow") And col = CurCharRow("CurCol") Then HexShow(CurCharRow("CurRow"), CurCharRow("CurCol"), cCurrent) Else HexShow(row, col)
    End Sub

    Friend Sub HexShow(row As Integer, col As Integer, Optional c As Color = Nothing)

        If row < 1 OrElse col < 1 OrElse row > yHexes OrElse col > xHexes Then Exit Sub
        If c = Nothing Then c = Color.White

        ' Fill the hex
        HexColor(row, col, c)

        ' Label the hex
        If Hex(row, col).CharRow IsNot Nothing Then
            Dim Reverse = (c <> Color.White) AndAlso (c <> cCurrent)
            HexLabel(row, col, Hex(row, col).CharRow, Reverse)
        End If

    End Sub

    Friend Sub HexLabel(Row As Integer, Col As Integer, CharRow As DataRow, Optional Reverse As Boolean = False)

        Dim cTeam As Color = IIf(CharRow("Side") = "Party", cParty, cEnemy)
        Dim cOpposing As Color = IIf(CharRow("Side") = "Party", cEnemy, cParty)
        If Reverse Then cTeam = Color.White
        If Reverse Then cOpposing = Color.White
        Dim TeamBrush As New SolidBrush(cTeam)
        Dim OppBrush As New SolidBrush(cOpposing)
        Dim FontCond As New Font("Wingdings 2", 7)

        ' Show rank symbol
        Dim Rank As String = ""
        Select Case CharRow("Rank")
            Case 3
                Rank = ChrW(221)
            Case 2
                Rank = ChrW(212)
        End Select
        Dim txtwid As Integer = g.MeasureString(Rank, FontRank).Width
        If Rank <> "" Then g.DrawString(Rank, FontRank, TeamBrush, New Point(Hex(Row, Col).x + HexWidth / 2 - txtwid / 2 + 2, Hex(Row, Col).y - 23))

        ' Show Archetype
        txtwid = g.MeasureString(CharRow("Archetype"), FontClass).Width
        g.DrawString(CharRow("Archetype"), FontClass, TeamBrush, New Point(Hex(Row, Col).x + HexWidth / 2 - txtwid / 2, Hex(Row, Col).y - 10))

        ' Show buff and debuff indicators
        If CharRow("Shield Raised") > 0 OrElse CharRow("Evasive Stance") > 0 OrElse CharRow("Protective Aura") > 0 Then g.DrawString(ChrW(203), FontCond, TeamBrush, New Point(Hex(Row, Col).x + 20, Hex(Row, Col).y + 10))
        If CharRow("Hobbled") > 0 OrElse CharRow("Disoriented") > 0 OrElse CharRow("Slowed") > 0 OrElse CharRow("Weakened") > 0 OrElse CharRow("Blinded") > 0 Then g.DrawString(ChrW(210), FontCond, OppBrush, New Point(Hex(Row, Col).x + 35, Hex(Row, Col).y + 10))

        frmMain.picMap.Image = b

    End Sub

    Friend Sub HexDarken(row As Integer, col As Integer, c As Color)

        If row < 1 OrElse col < 1 OrElse row > yHexes OrElse col > xHexes Then Exit Sub

        Dim level As Double = 0.9
        For i As Integer = 1 To 9
            c = Color.FromArgb(c.A, c.R * level, c.G * level, c.B * level)
            HexShow(row, col, c)
            Wait(300)
        Next

    End Sub

    Private Function GetOffx(sDir As String) As Integer
        Select Case sDir
            Case "N", "S"
                Return 0
            Case "NW", "SW"
                Return -HexWidth4 * 3
            Case "NE", "SE"
                Return HexWidth4 * 3
        End Select
        Return 0
    End Function

    Private Function GetOffy(sDir As String) As Integer
        Select Case sDir
            Case "N"
                Return -HexHeight2 * 2
            Case "S"
                Return HexHeight2 * 2
            Case "NW", "NE"
                Return -HexHeight2
            Case "SW", "SE"
                Return HexHeight2
        End Select
        Return 0
    End Function

    Friend Function GetHexFromCoords(x As Integer, y As Integer) As Point

        ' Get approximate hex location
        Dim col As Integer = x / ((HexWidth4 * 3) - xMargin)
        Dim row As Integer = (y + IIf(IsOdd(col), 9, -1)) / (HexHeight2 * 2)

        ' Get exact hex location
        Dim xdiff As Integer
        Dim ydiff As Integer
        For r As Integer = row - 1 To row + 1
            For c As Integer = col - 1 To col + 1
                If r < 1 OrElse c < 1 OrElse r > yHexes OrElse c > xHexes Then Continue For
                xdiff = Math.Abs(Hex(r, c).center.X - x)
                ydiff = Math.Abs(Hex(r, c).center.Y - y)
                If xdiff <= (HexWidth / 2) - 10 AndAlso ydiff <= HexHeight2 Then Return New Point(c, r)
            Next
        Next
        Return New Point(0, 0)

    End Function

    Friend Sub ShowHexText(Row As Integer, Col As Integer, Message As String)

        Dim FontStatus As New Font("Comic Sans", 24)
        Dim brush As New SolidBrush(Color.White)

        ' Show Message
        Dim txtwid As Integer = g.MeasureString(Message, FontStatus).Width
        Dim txthgt As Integer = g.MeasureString(Message, FontStatus).Height
        g.DrawString(Message, FontStatus, brush, New Point(Hex(Row, Col).x + HexWidth / 2 - txtwid / 2, Hex(Row, Col).y - txthgt / 2))

        frmMain.picMap.Image = b

    End Sub

    Friend Sub FlashHex(row As Integer, col As Integer, c As Color, Optional Repeats As Integer = 1, Optional FlashDelay As Integer = 500, Optional Message As String = "")
        frmMain.txtPop.Visible = False
        For i As Integer = 1 To Repeats
            If Message = "" Then
                HexShow(row, col, c)
            Else
                HexColor(row, col, c)
                ShowHexText(row, col, Message)
            End If
            Wait(FlashDelay)
            HexShow(row, col)
            If i < Repeats Then Wait(FlashDelay)
        Next
        If row = CurCharRow("CurRow") And col = CurCharRow("CurCol") Then HexShow(CurCharRow("CurRow"), CurCharRow("CurCol"), cCurrent)
    End Sub

    Friend Sub FlashHexes(dtLocs As DataTable, c As Color, Optional Repeats As Integer = 1, Optional FlashDelay As Integer = 500)

        frmMain.txtPop.Visible = False
        For j As Integer = 1 To Repeats
            For i As Integer = 0 To dtLocs.Rows.Count - 1
                HexColor(dtLocs.Rows(i)("Row"), dtLocs.Rows(i)("Col"), c)
                ShowHexText(dtLocs.Rows(i)("Row"), dtLocs.Rows(i)("Col"), dtLocs.Rows(i)("Message").ToString)
            Next
            Wait(FlashDelay)
        Next
        For i As Integer = 0 To dtLocs.Rows.Count - 1
            If dtLocs.Rows(i)("Row") = CurCharRow("CurRow") And dtLocs.Rows(i)("Col") = CurCharRow("CurCol") Then
                HexShow(CurCharRow("CurRow"), CurCharRow("CurCol"), cCurrent)
            Else
                HexShow(dtLocs.Rows(i)("Row"), dtLocs.Rows(i)("Col"))
            End If
        Next

    End Sub

    ' Return distance in hexes
    Friend Function CalcHexDist(r1 As Integer, c1 As Integer, r2 As Integer, c2 As Integer) As Integer

        ' Check for same hex
        If r1 = r2 AndAlso c1 = c2 Then Return 0

        ' Check for same column
        If c1 = c2 Then Return Math.Abs(r1 - r2)

        ' Get approximate distance
        Dim Appx As Integer = Math.Sqrt((r1 - r2) ^ 2 + (c1 - c2) ^ 2)

        ' Find the precise distance
        Dim pTarget As New Point(r2, c2)
        Dim Ring As List(Of Point)
        For Range As Integer = Appx - 2 To Appx + 2
            If Range < 1 Then Continue For
            Ring = GetHexRing(r1, c1, Range)
            If Ring.Contains(pTarget) Then Return Range
        Next
        Return -1

    End Function

    Friend Function GetHexRing(CenterRow As Integer, CenterCol As Integer, Radius As Integer) As List(Of Point)

        Dim Ring As New List(Of Point)

        ' Generate an offset sequence
        Dim MaxDist As Integer = Math.Sqrt((xHexes * xHexes) + (yHexes * yHexes)) + 2
        Dim seq(MaxDist) As Integer
        Dim inc As Integer = 0
        For i As Integer = 0 To MaxDist - 1 Step 2
            seq(i) = inc
            seq(i + 1) = inc
            inc += 1
        Next

        ' Find corner hex
        Dim CornerRow As Integer = CenterRow - seq(IIf(IsOdd(CenterCol), Radius + 1, Radius + 0))
        Dim CornerCol As Integer = CenterCol - Radius

        ' Fill down left side
        For i As Integer = 0 To Radius
            Ring.Add(New Point(CornerRow + i, CenterCol - Radius))
        Next

        ' Fill down right side
        For i As Integer = 0 To Radius
            Ring.Add(New Point(CornerRow + i, CenterCol + Radius))
        Next

        ' Move SE from top including top
        If IsOdd(CenterCol) Then
            For i As Integer = 0 To Radius - 1
                Ring.Add(New Point(CenterRow - Radius + seq(i), CenterCol + i))
            Next
        Else
            For i As Integer = 0 To Radius - 1
                Ring.Add(New Point(CenterRow - Radius + seq(i + 1), CenterCol + i))
            Next
        End If

        ' Move SW from top
        If IsOdd(CenterCol) Then
            For i As Integer = 1 To Radius - 1
                Ring.Add(New Point(CenterRow - Radius + seq(i), CenterCol - i))
            Next
        Else
            For i As Integer = 1 To Radius - 1
                Ring.Add(New Point(CenterRow - Radius + seq(i + 1), CenterCol - i))
            Next
        End If

        ' Move NE from bottom including bottom
        If IsOdd(CenterCol) Then
            For i As Integer = 0 To Radius - 1
                Ring.Add(New Point(CenterRow + Radius - seq(i + 1), CenterCol - i))
            Next
        Else
            For i As Integer = 0 To Radius - 1
                Ring.Add(New Point(CenterRow + Radius - seq(i), CenterCol - i))
            Next
        End If

        ' Move NW from bottom
        If IsOdd(CenterCol) Then
            For i As Integer = 1 To Radius - 1
                Ring.Add(New Point(CenterRow + Radius - seq(i + 1), CenterCol + i))
            Next
        Else
            For i As Integer = 1 To Radius - 1
                Ring.Add(New Point(CenterRow + Radius - seq(i), CenterCol + i))
            Next
        End If

        ' Clean up hexes off screen
        For i As Integer = Ring.Count - 1 To 0 Step -1
            If Ring(i).X < 1 OrElse Ring(i).X > yHexes OrElse Ring(i).Y < 1 OrElse Ring(i).Y > xHexes Then
                Ring.Remove(Ring(i))
            End If
        Next

        Return Ring
    End Function

    Private Sub ShowRing(Ring As List(Of Point))
        For Each p As Point In Ring
            HexShow(p.X, p.Y, cHelpful)
        Next
    End Sub

    ' Check if a hex is adjacent
    Friend Function IsHexAdj(r1 As Integer, c1 As Integer, r2 As Integer, c2 As Integer) As Boolean

        ' If they are in the same column, check if they are next to each other
        If c1 = c2 Then Return Math.Abs(r1 - r2) = 1

        ' Since they are not in the same column, make sure they are in adjacent columns
        If Math.Abs(c1 - c2) <> 1 Then Return False

        ' Since they are in adjacent columns, make sure they are in adjacent rows
        If r1 = r2 Then Return True

        If IsOdd(c1) Then
            If r2 = r1 - 1 Then Return True
        Else
            If r2 = r1 + 1 Then Return True
        End If

        Return False
    End Function

    Friend Function FindOpenHex(row As Integer, col As Integer) As Point

        Dim Ring As List(Of Point)
        Ring = GetHexRing(row, col, 1)
        For i As Integer = 0 To Ring.Count - 1
            If Hex(Ring(i).X, Ring(i).Y).side = "" Then Return Ring(i)
        Next
    End Function

    Public Function RotateImage(image As Image, offset As PointF, angle As Integer) As Bitmap
        If image Is Nothing Then Return Nothing
        Dim rotatedBmp As Bitmap = New Bitmap(image.Width, image.Height)
        rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution)
        Dim g As Graphics = Graphics.FromImage(rotatedBmp)
        g.TranslateTransform(offset.X, offset.Y)
        g.RotateTransform(angle)
        g.TranslateTransform(-offset.X, -offset.Y)
        g.DrawImage(image, offset)
        Return rotatedBmp
    End Function

    Public Function FlipImage(img As Image, flip As RotateFlipType) As Image
        img.RotateFlip(flip)
        Return img
    End Function

End Module
