/*
File Explorer Classic

Copyright(C) 2024 Special-Niewbie Softwares

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation version 3.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
#Include libs\XBOX360.ahk
#Persistent
#NoEnv
#SingleInstance Force
SetBatchLines, -1
SetTitleMatchMode, 2

; Get screen resolution
SysGet, ScreenWidth, 78
SysGet, ScreenHeight, 79

; Calculate the position of the window (top right)
ImageWidth := 56  ; Image width
ImageHeight := 56  ; Image height
PosX := ScreenWidth - ImageWidth - 30  ; 30 pixels margin from the right edge
PosY := 700  ; 10 pixels margin from the top edge

; FileInstall to include the image in the project
FileInstall, FEClassic56x56.png, %A_Temp%\FEClassic56x56.png, 1

Menu, Tray, NoStandard
Menu, Tray, Tip, Windows Explorer Classic
Menu, Tray, Add, Restart Windows Explorer Classic, ReloadScript
Menu, Tray, Add, , Separator
Menu, Tray, Add, Close, Close

; Creating a Transparent GUI
Gui, New, +Caption
Gui, +AlwaysOnTop +ToolWindow -Caption +LastFound
Gui, Color, 0x0A3081  ; Background Black Color
Gui, Add, Picture, x0 y0 w%ImageWidth% h%ImageHeight% vMyIcon gMyIconClick, %A_Temp%\FEClassic56x56.png
WinSet, TransColor, 0x0A3081
Gui, Show, NA x%PosX% y%PosY%, File Explorer Touch

; Instance the Controller Manager
manager := new Xbox360LibControllerManager()

; Initialize Controller (0-3), you can use 4 controllers
player1 := manager.InitializeController(0)

; Execute the command at startup
Run, explorer

; Handle dragging
OnMessage(0x201, "WM_LBUTTONDOWN")

; Initialize the variable for double click
lastClickTime := 0

; Timer to check the controller state
SetTimer, CheckController, 100

WM_LBUTTONDOWN()
{
    PostMessage, 0xA1, 2
}

MyIconClick:
    ; Handle double click
    global lastClickTime
    currentTime := A_TickCount
	
    if (currentTime - lastClickTime < 400)
    {
        ; Close the process
        CloseExplorerWindows()
		
        ; Set the vibration
        player1.BV := [65535, 65535]   ; Set both engine speeds to maximum		
        Sleep 260                      ; Wait 260 milliseconds
        player1.BV := [65535, 65535]   ; Set both engine speeds to maximum
        Sleep 260

        player1.BV := [0, 0]           ; Turn off the vibration
        Sleep 50
		
        ; Close the program
        ExitApp
    }
    lastClickTime := currentTime
return

CheckController:
    player1.Update()
    global lastClickTime
    currentTime := A_TickCount

    if (player1.B && currentTime - lastClickTime < 400)
    {
        ; Close the process
        CloseExplorerWindows()

        ; Set the vibration
        player1.BV := [65535, 65535]   ; Set both engine speeds to maximum		
        Sleep 200                      ; Wait 200 milliseconds
        player1.BV := [65535, 65535]   ; Set both engine speeds to maximum		
        Sleep 200
	
        player1.BV := [0, 0]           ; Turn off the vibration
        Sleep 50

        ; Close the program
        ExitApp
    }
    if (player1.B)
    {
        lastClickTime := currentTime
    }
return

CloseExplorerWindows()
{
    WinGet, id, List, ahk_class CabinetWClass
    Loop, %id%
    {
        this_id := id%A_Index%
        WinClose, ahk_id %this_id%
    }
}

GuiClose:
    ExitApp

ReloadScript:
    Reload
return

Close:
	CloseExplorerWindows()
	ExitApp
