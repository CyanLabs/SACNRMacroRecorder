Imports System.Speech.Synthesis
Public Class Form1
    Dim speaker As New SpeechSynthesizer()
    Private WithEvents kbHook As New kbhook
    Dim isrecording As Boolean = False
    Dim convertedkeycode As String = ""
    Dim firstkey As Boolean = False
    Dim inisettings As New ini(Application.StartupPath & "\keybinds.sav")
    Dim keybindkey As String = ""
    Dim macro As String = ""
    Dim x As Integer = 0
    Private Sub kbHook_KeyDown(ByVal Key As System.Windows.Forms.Keys) Handles kbHook.KeyDown
        If FixKey(Key) = TextBox1.Text Then
            If isrecording Then
                PictureBox1.Image = My.Resources._stop
                speaker.SpeakAsync("Macro saved. Recording stopped")
                Timer1.Stop()
                isrecording = False
                inisettings.WriteString("Macros", "Macro" & x, macro)
                x = x + 1
                macro = ""
                speaker.SpeakAsync("To use your new macro, Press the " & keybindkey & " key.")
                Exit Sub
            Else
                isrecording = True
                speaker.SpeakAsync("Press the key you wish to assign")
                firstkey = True
                PictureBox1.Image = My.Resources.record
                Timer1.Start()
                Exit Sub
            End If
        ElseIf isrecording Then
            If Not Key = Keys.Scroll Then
                If firstkey Then
                    firstkey = False
                    keybindkey = FixKey(Key).ToString
                    inisettings.WriteString("Keys", "Key" & x, keybindkey)
                    speaker.Speak("The " & keybindkey & " key was assigned. Recording started.")
                Else
                    Debug.Write(FixKey(Key))
                    If Key = 8 Then
                        macro = macro.Substring(0, macro.Length - 1)
                    Else
                        macro &= FixKey(Key)
                    End If

                    Timer1.Interval = 1000
                End If
            End If
        Else
            For i As Integer = 1 To x
                If FixKey(Key) = inisettings.GetString("Keys", "Key" & i, "NOTHING") Then
                    SendKeys.SendWait(inisettings.GetString("Macros", "Macro" & i, ""))
                End If
            Next
        End If
    End Sub
    Public Function FixKey(ByVal keycode As Integer)
        Select Case keycode
            Case 48 To 57
                If My.Computer.Keyboard.ShiftKeyDown Then
                    Select Case keycode
                        Case 48
                            convertedkeycode = ")"
                        Case 49
                            convertedkeycode = "!"
                        Case 50
                            convertedkeycode = """"
                        Case 51
                            convertedkeycode = "£"
                        Case 52
                            convertedkeycode = "$"
                        Case 53
                            convertedkeycode = "%"
                        Case 54
                            convertedkeycode = "^"
                        Case 55
                            convertedkeycode = "&"
                        Case 56
                            convertedkeycode = "*"
                        Case 57
                            convertedkeycode = "("
                    End Select
                Else
                    convertedkeycode = Convert.ToChar(keycode)
                End If
            Case 65 To 90
                If My.Computer.Keyboard.ShiftKeyDown Or My.Computer.Keyboard.CapsLock Then
                    convertedkeycode = Convert.ToChar(keycode)
                Else
                    convertedkeycode = Convert.ToChar(keycode + 32)
                End If
            Case 1
                convertedkeycode = ""
                'Case 2
                '    convertedkeycode = "<RCLICK>"
            Case 8
                convertedkeycode = ""
                'Case 9
                '    convertedkeycode = "<TAB>"
            Case 12
                convertedkeycode = "5"
            Case 13
                convertedkeycode = "{ENTER}"
                'Case 17
                '    convertedkeycode = "<CTRL>"
                'Case 18
                '    convertedkeycode = "<ALT>"
                'Case 19
                '    convertedkeycode = "<PAUSE>"
            Case 20
                convertedkeycode = ""
                'Case 27
                '    convertedkeycode = "<ESC>"
            Case 32
                convertedkeycode = " "
                'Case 33
                '    convertedkeycode = "<PAGEUP>"
                'Case 34
                '    convertedkeycode = "<PAGEDOWN>"
                'Case 35
                '    convertedkeycode = "<END>"
                'Case 36
                '    convertedkeycode = "<HOME>"
                'Case 37
                '    convertedkeycode = "<LEFT>"
                'Case 38
                '    convertedkeycode = "<UP>"
                'Case 39
                '    convertedkeycode = "<RIGHT>"
                'Case 40
                '    convertedkeycode = "<DOWN>"
                'Case 44
                '    convertedkeycode = "<PRNTSCRN>"
                'Case 45
                '    convertedkeycode = "<INSERT>"
                'Case 46
                '    convertedkeycode = "<DEL>"
                'Case 47
                '    convertedkeycode = "<HELP>"
                'Case 91
                '    convertedkeycode = "<WIN>"
                'Case 93
                '    convertedkeycode = "<CONTEXT>"
            Case 96
                convertedkeycode = "0"
            Case 97
                convertedkeycode = "1"
            Case 98
                convertedkeycode = "2"
            Case 99
                convertedkeycode = "3"
            Case 100
                convertedkeycode = "4"
            Case 101
                convertedkeycode = "5"
            Case 102
                convertedkeycode = "6"
            Case 103
                convertedkeycode = "7"
            Case 104
                convertedkeycode = "8"
            Case 105
                convertedkeycode = "9"
            Case 106
                convertedkeycode = "*"
            Case 107
                convertedkeycode = "+"
            Case 109
                convertedkeycode = "-"
            Case 110
                convertedkeycode = "."
            Case 111
                convertedkeycode = "/"
            Case 112
                convertedkeycode = "F1"
            Case 113
                convertedkeycode = "F2"
            Case 114
                convertedkeycode = "F3"
            Case 115
                convertedkeycode = "F4"
            Case 116
                convertedkeycode = "F5"
            Case 117
                convertedkeycode = "F6"
            Case 118
                convertedkeycode = "F7"
            Case 119
                convertedkeycode = "F8"
            Case 120
                convertedkeycode = "F9"
            Case 121
                convertedkeycode = "F10"
            Case 122
                convertedkeycode = "F11"
            Case 123
                convertedkeycode = "F12"
                'Case 144
                '    convertedkeycode = "<NUMLOCK>"
                'Case 162
                '    convertedkeycode = "<LCTRL>"
                'Case 163
                '    convertedkeycode = "<RCTRL>"
                'Case 164
                '    convertedkeycode = "<ALT>"
                'Case 165
                '    convertedkeycode = "<ALTGR>"
            Case 186
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = ":"
                Else
                    convertedkeycode = ";"
                End If
            Case 187
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "+"
                Else
                    convertedkeycode = "="
                End If
            Case 188
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "<"
                Else
                    convertedkeycode = ","
                End If
            Case 189
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "_"
                Else
                    convertedkeycode = "-"
                End If
            Case 190
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = ">"
                Else
                    convertedkeycode = "."
                End If
            Case 191
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "?"
                Else
                    convertedkeycode = "/"
                End If
            Case 192
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "@"
                Else
                    convertedkeycode = "'"
                End If
            Case 219
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "{"
                Else
                    convertedkeycode = "["
                End If
            Case 220
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "|"
                Else
                    convertedkeycode = "\"
                End If
            Case 221
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "}"
                Else
                    convertedkeycode = "]"
                End If
            Case 222
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "~"
                Else
                    convertedkeycode = "#"
                End If
            Case 223
                If My.Computer.Keyboard.ShiftKeyDown Then
                    convertedkeycode = "¬"
                Else
                    convertedkeycode = "`"
                End If
            Case Else
                convertedkeycode = ""
        End Select
        Return convertedkeycode
    End Function

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If isrecording Then
            Exit Sub
        End If
        If e.KeyValue = 145 Then
            e.SuppressKeyPress = True
            Exit Sub
        End If
        sender.text = FixKey(e.KeyValue)
        Button1.Focus()
    End Sub
    Private Declare Sub keybd_event Lib "user32" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
    Private Const VK_SCROLL As Integer = &H91
    Private Const KEYEVENTF_EXTENDEDKEY As Integer = &H1
    Private Const KEYEVENTF_KEYUP As Integer = &H2

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        keybd_event(VK_SCROLL, &H45, KEYEVENTF_EXTENDEDKEY Or 0, 0)
        keybd_event(VK_SCROLL, &H45, KEYEVENTF_EXTENDEDKEY Or KEYEVENTF_KEYUP, 0)
        If Not firstkey Then
            If PictureBox1.Visible Then
                PictureBox1.Visible = False
            Else
                PictureBox1.Visible = True
            End If
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        inisettings.WriteInteger("Settings", "MacroCount", x - 1)
        inisettings.WriteString("Settings", "RecordKey", TextBox1.Text)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = inisettings.GetString("Settings", "RecordKey", TextBox1.Text)
        x = inisettings.GetInteger("Settings", "MacroCount", 1)
    End Sub

    Private Sub TextBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseDown
        speaker.Speak("Press the key you would like to use to toggle recording.")
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        speaker.Speak("Welcome Beta Testers. Please turn off your music to be able to hear the instructions")
    End Sub
End Class
