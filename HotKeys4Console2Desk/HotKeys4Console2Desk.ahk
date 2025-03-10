﻿/*
HotKeys4Console2Desk

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

;@Ahk2Exe-SetName HotKeys4Console2Desk
;@Ahk2Exe-SetDescription HotKeys4Console2Desk companion tool for Console2Desk
;@Ahk2Exe-SetVersion 3.8.5
;@Ahk2Exe-SetOrigFilename HotKeys4Console2Desk.ahk
;@Ahk2Exe-SetCompanyName Special-Niewbie Softwares
;@Ahk2Exe-SetCopyright ©2024 Special-Niewbie Softwares. All rights reserved.

#Include libs\XBOX360.ahk
#Include libs\SwitchControls.ahk
#Include libs\BatteryEffect.ahk
#Include libs\ChangeComputerIcon.ahk

#Persistent

IniFile := A_ScriptDir . "\on_off.ini"
CheckAndCreateOnOffIni()

CheckAndCreateSwitch_ControlsIni()

Console2DeskExePath := "C:\Program Files\Console2Desk\Console2Desk.exe"
PlayniteFullscreenExePath := A_AppData . "\..\Local\Playnite\Playnite.FullscreenApp.exe"
PlayniteDesktopExePath := A_AppData . "\..\Local\Playnite\Playnite.DesktopApp.exe"

; Global variables
global isVideoPlaying := false
global playniteStarted := false
global firstRun := true
global timerStarted := false
; Tab Global variables
global windowList := []
global currentWindowIndex := 0
global isMenuVisible := false
global menuGui := 0
global highlightedIndex := 0
global PreviewBar, PreviewText, JustDotText, Icon1, Icon2, Icon3, Icon4, Icon5, TitleText, ImagePic
global lastActivity := A_TickCount

Menu, Tray, Tip, HotKeys4Console2Desk

; Menu System Tray icon
Menu, Tray, NoStandard

Menu, Tray, Add, Project site for Updates / Donate, OpenScriptSite
Menu, Tray, Add, , Separator
Menu, Tray, Add, Show Version, ShowVersionInfo
Menu, Tray, Add, Change Computer Icon, ChangeComputerIcon  ; Change Computer Icon option
Menu, Tray, Icon, Change Computer Icon, Imageres.dll, 187
; Menu, Tray, Add, Pause, PAUSE
Menu, Tray, Add, , Separator
Menu, Tray, Add, Cursor Theme Dark/Light, ToggleCursorTheme
Menu, Tray, Add, Tablet Mode ON/OFF, ToggleTabletModeSvc  ; Opzione per attivare/disattivare il Tablet Mode
Menu, Tray, Add, ClipBoard History ON/OFF, ToggleClipboardHistorySvc  
Menu, Tray, Add, Microsoft using your Webcam ON/OFF, ToggleWebcamTelemetry
Menu, Tray, Add, Print Service ON/OFF, ToggleSpoolerSvc  ; SpoolerSVC toggle option
Menu, Tray, Add, Update Service ON/OFF, ToggleUpdateSvc  ; UpdateSVC toggle option
Menu, Tray, Add, Splash Video ON/OFF, ToggleVideo  ; Video toggle option
Menu, Tray, Add, Configure Switch Buttons, ShowConfigGUI
Menu, Tray, Icon, Configure Switch Buttons, HandleOS_icons.dll, 11
Menu, Tray, Add, Exit, QuitNow ; added exit script option

; Check for Updates
CheckForUpdates()

; Refresh menu when program starts
; Update ToggleCursorTheme menu item at startup
UpdateCursorThemeMenu()
; Update ToggleTabletModeSvc menu item at startup
UpdateTabletModeSvcMenu()
; Update UpdateClipboardHistorySvcMenu menu item at startup
UpdateClipboardHistorySvcMenu()
; Update UpdateWebcamTelemetryMenu menu item at startup
UpdateWebcamTelemetryMenu()
; Update SpoolerSVC menu item at startup
UpdateSpoolerSvcMenu()
; Update UpdateSVC menu item at startup
UpdateUpdateSvcMenu()
; Update Video menu item at startup
UpdateVideoMenu()

CheckPlaynite()

; Function to check and create the ini file if it doesn't exist
CheckAndCreateOnOffIni() {
    global IniFile
    if (!FileExist(IniFile)) {
        FileAppend, [Settings]`nVideoState=0, %IniFile%
    }
}

Run, %A_ScriptDir%\AudioCCMode.exe

SetTimer, CheckControllers, 150
; Timer that monitors inactivity
SetTimer, CheckInactivity, 1000

;Instance the Controller Manager
manager := new Xbox360LibControllerManager()

;Initialize Controller (0-3), you can use 4 controllers
player1 := manager.InitializeController(0)

; Initialize Controllers (0-3), up to 4 controllers
controllers := []
Loop 4 {
    controllers[A_Index] := manager.InitializeController(A_Index - 1)
}

; Function to check the status of the controllers.
CheckControllers:
    BatteryCheck()
    Loop 4 {
        controller := controllers[A_Index]
        controller.Update()

        ; If the controller is active, check the specific buttons
        if (controller.BACK && controller.START) {
            Run, schtasks /end /tn "Console2Desk",, Hide
            Sleep 250
            Run, schtasks /run /tn "Console2Desk",, Hide
            controller.BV := [65535, 65535]
            Sleep 400
            controller.BV := [0, 0]
        }

        if (CheckSwitchCombination(controller)) {
            if (windowList.Length() == 0) {
                UpdateWindowList()
            }

            SwitchToNextWindow()

            if (!isMenuVisible) {
                isMenuVisible := true
                ShowWindowMenu()            
            } else {
                UpdateWindowMenu()
            }

            controller.BV := [65535, 65535]
            Sleep 150
            controller.BV := [0, 0]
            ; Update last active time when a key is pressed
            lastActivity := A_TickCount
        } else if (isMenuVisible) {
            if (controller.A) {
                ActivateCurrentWindow()
                controller.BV := [65535, 65535]
                Sleep 150
                controller.BV := [0, 0]               
            } else if (controller.LEFT) {
                NavigateMenu(-1)               
            } else if (controller.RIGHT) {
                NavigateMenu(1)                
            }
        }
    }
return

; Manages the Ctrl+Shift+F7 key combination
^+F7::
    ;Run, explorer.exe "C:\Program Files\Console2Desk\Console2Desk.exe"   Old implementation	
	;Run, %ComSpec% /c ""%Console2DeskExePath%"", , Hide                  Old implementation	
	; Stop the scheduled task first
    Run, schtasks /end /tn "Console2Desk",, Hide
	
	Sleep 250	
	; Then Start the scheduled task
    Run, schtasks /run /tn "Console2Desk",, Hide
	
	; Set the vibration
    control.BV := [65535, 65535]   ; Set both engine speeds to maximum
    Sleep 400                      ; Wait 400 milliseconds
    control.BV := [0, 0]           ; Turn off the vibration

return

CheckForUpdates() {
    localVersionFile := A_ScriptDir . "\version"
    if (!FileExist(localVersionFile)) {
        MsgBox, 16, Error, The version file was not found.
        ExitApp
    }

    FileRead, localVersion, %localVersionFile%
    localVersion := Trim(localVersion)

    url := "https://raw.githubusercontent.com/Special-Niewbie/HandleOS/main/version"
    latestVersionFile := A_ScriptDir . "\latest_version"
    
    ; Download the latest version file
    UrlDownloadToFile, %url%, %latestVersionFile%

    ; Checks whether the latest version file exists and contains valid data
    if (FileExist(latestVersionFile)) {
        FileRead, latestVersion, %latestVersionFile%
        latestVersion := Trim(latestVersion)
        FileDelete, %latestVersionFile%

        ; Check if the downloaded content is not an HTTP error (ex. 404: Not Found)
        if (InStr(latestVersion, "404: Not Found") = 0 && localVersion != latestVersion) {
            ; Show a simple message box with YES and NO buttons
            MsgBox, 4,, A new version is available: %latestVersion%`n`nYou are currently using version: %localVersion%`n`nWould you like to visit the project page on GitHub to Download it?
            IfMsgBox, Yes
            {
                Run, https://github.com/Special-Niewbie/HandleOS/releases
            }
        }
        else {
            ;MsgBox, 64, No Update, You are already using the latest version.
        }
    }
    else {
        ; Handle error if the file could not be downloaded or if it contains HTTP errors
        ;MsgBox, 16, Error, Unable to check for updates. Please check your internet connection or try again later.
    }
}

; Function to update the list of active windows
UpdateWindowList() {
    windowList := []
    WinGet, id, List,,, Program Manager
    Loop, %id%
    {
        this_id := id%A_Index%
        WinGetTitle, title, ahk_id %this_id%
        WinGet, style, Style, ahk_id %this_id%
        WinGet, exePath, ProcessPath, ahk_id %this_id%

        ; Include all windows with a non-empty title
        if (title != "" && IsWindow(this_id))
            windowList.Push({id: this_id, title: title, path: exePath})
    }
}

; Function to switch to the next window
SwitchToNextWindow() {
    if (windowList.Length() > 0) {
        currentWindowIndex := Mod(currentWindowIndex + 1, windowList.Length())
        if (currentWindowIndex = 0)
            currentWindowIndex := windowList.Length()
        
        ; Aggiorna l'indice evidenziato
        highlightedIndex := Min(windowList.Length(), Max(1, currentWindowIndex - 2))
        
        nextWindow := windowList[currentWindowIndex]
        WinActivate, % "ahk_id " . nextWindow.id
        WinRestore, % "ahk_id " . nextWindow.id
        
        UpdateWindowMenu()
    }
}

; Function to show the window selection menu
ShowWindowMenu() {
    ; Calculate the position to center the GUI on the screen
    ScreenWidth := A_ScreenWidth
    ScreenHeight := A_ScreenHeight
    GuiWidth := 350
    GuiHeight := 240
    X := (ScreenWidth-200 - GuiWidth) // 2
    Y := (ScreenHeight - GuiHeight) // 2
    
    ; Updates the window list and current index
    UpdateWindowList()
    currentWindowIndex := 1
    highlightedIndex := 1
    
    ; Create the GUI if it doesn't already exist
    if !menuGui {
        Gui, New, +AlwaysOnTop -Caption +ToolWindow, Window Switcher
        Gui, Color, 1d1d1d
        Gui, Font, s10 cWhite, Roboto

        ; Add elements to the GUI
        Gui, Add, Progress, w325 h30 Background333333 vPreviewBar
        Gui, Add, Text, xp+5 yp+5 w315 h20 BackgroundTrans vPreviewText Center, PreviewText
        
		Gui, Font, s12 c6A5CCD, Roboto
		Gui, Add, Text, xm+92 w10 h20 vJustDotText, ⌄
		Gui, Font, s10 cE5E4E8, Roboto Light
        Gui, Add, Picture, xm+80 y+1 w36 h36 vIcon1
        Gui, Add, Picture, x+10 w36 h36 vIcon2
        Gui, Add, Picture, x+10 w36 h36 vIcon3
        Gui, Add, Picture, x+10 w36 h36 vIcon4
        Gui, Add, Picture, x+10 w36 h36 vIcon5
        Gui, Add, Text, xm y+30 w325 h40 vTitleText Center
		Gui, Add, Picture, xm+40 w256 h55 vImagePic, %A_ScriptDir%\sources\DPAD-HandleOS.png

    }
    
    ; Show GUI
    Gui, Show, x%X% y%Y% w%GuiWidth% h%GuiHeight%, Window Switcher
    menuGui := WinExist("A")
    GuiControl,, PreviewText, % "HandleOS Tab View"  ; Assicura che il testo di anteprima venga visualizzato
    GuiControl,, TitleText, % ""  ; Rimuovi eventuali testi residui
    UpdateWindowMenu()
}


; Function to update the window menu
UpdateWindowMenu() {
    if (!menuGui) {
        return
    }
    
    Gui, %menuGui%:Default
    
    numIcons := Min(windowList.Length(), 5)
    
    Loop, 5 {
        index := A_Index + currentWindowIndex - 1
        if (index >= 1 && index <= windowList.Length()) {
            window := windowList[index]
            ; Load and set the icon for the Picture control
            hIcon := GetWindowIcon(window.path)
            GuiControl,, Icon%A_Index%, % "HICON:" . hIcon
        } else {
            GuiControl,, Icon%A_Index%, ; Remove icon if not visible
        }
    }
    
    currentWindow := windowList[currentWindowIndex]
    GuiControl,, TitleText, % currentWindow.title
    
    ; Refresh the highlighted index and show the correct icon
    UpdateHighlight()
}

; Function to update the highlighted icon
UpdateHighlight() {
    if (!menuGui) {
        return
    }
    
    Gui, %menuGui%:Default
    
    Loop, 5 {
        if (A_Index = highlightedIndex) {
            ; Show highlighted icon
            GuiControl, +Border, Icon%A_Index%, w70 h70
        } else {
            ; Hide highlighted icon
            GuiControl, -Border, Icon%A_Index%
        }
    }
}

; Function to get the window icon
GetWindowIcon(exePath) {
    ; Load the icon from the application executable
    hIcon := 0
    if (exePath != "") {
        ; Use the ExtractIcon function to get the icon
        hIcon := DllCall("Shell32.dll\ExtractIcon", "Ptr", 0, "Str", exePath, "UInt", 0, "Ptr")
    }
    if (hIcon = 0) {
        ; Use default icon if failed
        hIcon := DllCall("User32.dll\LoadIcon", "Ptr", 0, "Str", 32512)  ; IDI_APPLICATION
    }
    return hIcon
}

; Function to activate the current window and hide the menu
ActivateCurrentWindow() {
    if (windowList.Length() > 0) {
        WinActivate, % "ahk_id " . windowList[currentWindowIndex].id
        WinRestore, % "ahk_id " . windowList[currentWindowIndex].id
    }
    HideWindowMenu()
}

NoActivityCurrentWindow() {

    HideWindowMenu()
}

; Function to navigate the menu
NavigateMenu(direction) {
    if (windowList.Length() > 0) {
        newIndex := currentWindowIndex + direction
        
        ; Manage the menu end
        if (newIndex < 1) {
            newIndex := windowList.Length()
        } else if (newIndex > windowList.Length()) {
            newIndex := 1
        }
        
        currentWindowIndex := newIndex
        highlightedIndex := Min(windowList.Length(), Max(1, currentWindowIndex - 2))
        
        UpdateWindowMenu()
    }
}

; Check whether the target window is activation target
IsWindow(hWnd){
    WinGet, dwStyle, Style, ahk_id %hWnd%
    if ((dwStyle&0x08000000) || !(dwStyle&0x10000000)) {
        return false
    }
    WinGet, dwExStyle, ExStyle, ahk_id %hWnd%
    if (dwExStyle & 0x00000080) {
        return false
    }
    WinGetClass, szClass, ahk_id %hWnd%
    if (szClass = "TApplication") {
        return false
    }
    return true
}

; Function to hide the window menu
HideWindowMenu() {
    if (menuGui) {
        Gui, %menuGui%:Destroy
        menuGui := 0
        isMenuVisible := false
    } else {
        MsgBox, The GUI is not created or invalid.
    }
}

; Function to update status of Cursor Theme Dark/Light
UpdateCursorThemeMenu() {
    ; Reads the current cursor value from the register
    RegRead, currentCursorTheme, HKEY_CURRENT_USER\Control Panel\Cursors, Arrow
    
    ; Resolve full path with environment variable
    EnvGet, systemRoot, SystemRoot

    ; Solve the actual path for comparison
    darkPath := systemRoot "\Cursors\Cursors\HandleOS_Dark\pointer.cur"
    lightPath := systemRoot "\Cursors\HandleOS_Light\pointer.cur"

    ; Check if the current cursor is set to Light or Dark theme
    if (currentCursorTheme = darkPath) {
        Menu, Tray, Icon, Cursor Theme Dark/Light, HandleOS_icons.dll, 13  ; Icon Dark
    } else if (currentCursorTheme = lightPath) {
        Menu, Tray, Icon, Cursor Theme Dark/Light, HandleOS_icons.dll, 12  ; Icon Light
    } else {
        ; If it is not recognized, set a default icon
        Menu, Tray, Icon, Cursor Theme Dark/Light, Imageres.dll, 232 ; Icon Alert
    }
}

; Function to change Cursor Theme Dark/Light
ApplyCursorScheme(schemeName, cursorDirectory) {
    RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, Scheme Source, %schemeName%

    ; Imposta tutti i cursori associati al tema
    RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, Arrow, %SystemRoot%\%cursorDirectory%\pointer.cur
    RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, AppStarting, %SystemRoot%\%cursorDirectory%\work.ani
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, Crosshair, %SystemRoot%\%cursorDirectory%\cross.cur
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, Hand, %SystemRoot%\%cursorDirectory%\link.cur
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, Help, %SystemRoot%\%cursorDirectory%\help.cur
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, IBeam, %SystemRoot%\%cursorDirectory%\text.cur
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, No, %SystemRoot%\%cursorDirectory%\unavailiable.cur
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, NWPen, %SystemRoot%\%cursorDirectory%\handwriting.cur
    RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, Wait, %SystemRoot%\%cursorDirectory%\busy.ani
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, SizeAll, %SystemRoot%\%cursorDirectory%\move.cur
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, SizeNESW, %SystemRoot%\%cursorDirectory%\dgn2.cur
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, SizeNS, %SystemRoot%\%cursorDirectory%\vert.cur
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, SizeNWSE, %SystemRoot%\%cursorDirectory%\dgn1.cur
	RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, SizeWE, %SystemRoot%\%cursorDirectory%\horz.cur    
    RegWrite, REG_SZ, HKEY_CURRENT_USER\Control Panel\Cursors, UpArrow, %SystemRoot%\%cursorDirectory%\alternate.cur
    
}

; Function to update the status of the Tablet Mode menu
UpdateTabletModeSvcMenu() {
    ; Check the value of the ExpandableTaskbar key
    RegRead, expandableTaskbarValue, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced, ExpandableTaskbar
    ; Check the value of the TabletPostureTaskbar
    RegRead, tabletPostureTaskbarValue, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer, TabletPostureTaskbar

    ; If either key does not exist or the value is 0, set Tablet Mode icon OFF
    if (ErrorLevel or expandableTaskbarValue = 0 or tabletPostureTaskbarValue = 0) {
        Menu, Tray, Icon, Tablet Mode ON/OFF, Imageres.dll, 231  ; Icon for Tablet Mode OFF
    } else {
        ; If both keys exist and have value 1, set Tablet Mode icon ON
        Menu, Tray, Icon, Tablet Mode ON/OFF, DDORes.dll, 17  ; Icon for Tablet Mode ON
    }
}

; Function to update the status of the Clipboard History menu
UpdateClipboardHistorySvcMenu() {
    ; Check the value of the Enable Clipboard History key
    RegRead, clipboardHistoryValue, HKEY_CURRENT_USER\Software\Microsoft\Clipboard, EnableClipboardHistory

    if (ErrorLevel or clipboardHistoryValue = 0) {
        ; If the key does not exist or the value is 0, Clipboard History icon OFF
        Menu, Tray, Icon, ClipBoard History ON/OFF, Imageres.dll, 231  ; Icon for Clipboard History OFF
    } else {
        ; If the key exists and is 1, Clipboard History icon ON
        Menu, Tray, Icon, ClipBoard History ON/OFF, Imageres.dll, 113  ; Icon for Clipboard History ON
    }
}

UpdateWebcamTelemetryMenu() {
    ; Check the current status of the webcam telemetry task
    RunWait, %ComSpec% /c schtasks /query /TN "Microsoft\Windows\Device Information\Device" | find "Disabled", , Hide
    if (ErrorLevel = 1) {
        ; Task is enabled
        Menu, Tray, Icon, Microsoft using your Webcam ON/OFF, shell32.dll, 118  ; Icon for telemetry ON
    } else {
        ; Task is disabled
        Menu, Tray, Icon, Microsoft using your Webcam ON/OFF, Imageres.dll, 231 ; Icon for telemetry OFF
    }
}

UpdateSpoolerSvcMenu() {
    ; Get the current status of the Spooler service
    RunWait, %ComSpec% /c sc query Spooler | find "RUNNING", , Hide
    if (ErrorLevel = 0) {
        ; Spooler service is running
        Menu, Tray, Icon, Print Service ON/OFF, Imageres.dll, 45  ; Icon for SpoolerSVC ON
    } else {
        ; Spooler service is not running
        Menu, Tray, Icon, Print Service ON/OFF, Imageres.dll, 231 ; Icon for SpoolerSVC OFF
    }
}

UpdateUpdateSvcMenu() {
    ; Get the current status of the Update service
    RunWait, %ComSpec% /c sc query wuauserv | find "RUNNING", , Hide
    if (ErrorLevel = 0) {
        ; Update service is running
        Menu, Tray, Icon, Update Service ON/OFF, Imageres.dll, 102  ; Icon for UpdateSVC ON
    } else {
        ; Update service is not running
        Menu, Tray, Icon, Update Service ON/OFF, Imageres.dll, 203 ; Icon for UpdateSVC OFF
    }
}

; Function to update the video menu item based on the ini file value
UpdateVideoMenu() {
    global IniFile
    IniRead, VideoState, %IniFile%, Settings, VideoState, 0
    if (VideoState = 0) {
        Menu, Tray, Icon, Splash Video ON/OFF, Imageres.dll, 19  ; Icon for Video ON
    } else {
        Menu, Tray, Icon, Splash Video ON/OFF, Imageres.dll, 231  ; Icon for Video OFF
    }
}

; Function to check if a process is running.
ProcessExists(processName) {
    Process, Exist, %processName%
    return ErrorLevel
}

; Function to check if Playnite is started and start the splash screen video
CheckPlaynite() {
    global isVideoPlaying, playniteStarted, firstRun, timerStarted, IniFile

    if (firstRun) {
        firstRun := false
        Sleep 500
        if (ProcessExists("Playnite.FullscreenApp.exe") || ProcessExists("Playnite.DesktopApp.exe")) {
            playniteStarted := true
            timerStarted := true  ; If Playnite is already running, disable the timer
            return
        }
    }

    if (!timerStarted) {
        SetTimer, CheckPlaynite, 500
        timerStarted := true
    }

    if (playniteStarted) {
        return
    }

    if (!(ProcessExists("Playnite.FullscreenApp.exe") || ProcessExists("Playnite.DesktopApp.exe"))) {
        return
    }

    IniRead, VideoState, %IniFile%, Settings, VideoState, 0
    if (VideoState != 0) {
        return  ; Exit if the video function is turned off
    }

    playniteStarted := true
    isVideoPlaying := true
    mp4 := "C:\Program Files\Console2Desk\vid\splash_screen_video.mp4"
    RunWait, cmd /c start "" "C:\Program Files\Console2Desk\ffplay.exe" -hide_banner -loglevel panic -i -alwaysontop "%mp4%" -fs -noborder -window_title "Splash Video" -autoexit
    isVideoPlaying := false
    SetTimer, CheckPlaynite, Off
}

CheckInactivity:
    ; Check if more than 5000 milliseconds (5 seconds) have passed since the last input
    if (A_TickCount - lastActivity > 2500) {
        ; Only call Activate Current Window if the GUI is visible
        if (isMenuVisible) {
            NoActivityCurrentWindow()
        }
        ; Update the last active time to avoid repeated actions
        lastActivity := A_TickCount
    }
return

; YES/No Buttons for Updates Custom Window
YesButton:
    ; Open the GitHub page
    Run, https://github.com/Special-Niewbie/HandleOS
	Gui, Destroy
return

NoButton:
	Gui, Destroy
return

OpenScriptSite:
    Run, https://github.com/Special-Niewbie/
return

; Function to show version information
ShowVersionInfo:
    versionFile := A_ScriptDir . "\version"
    if (!FileExist(versionFile)) {
        MsgBox, 16, Error, The version file was not found.
        ExitApp
    }
    FileRead, currentVersion, %versionFile%
    MsgBox, 64, Version Info, Script Version: %currentVersion% `n`nAuthor: Special-Niewbie Softwares `nCopyright(C) 2024 Special-Niewbie Softwares
return

; Function to activate/deactivate the Dark or Light theme
ToggleCursorTheme:
    ; Resolve full path with environment variable
    EnvGet, systemRoot, SystemRoot
    RegRead, currentCursorTheme, HKEY_CURRENT_USER\Control Panel\Cursors, Arrow
    
    ; Solve the actual path for comparison
    darkPath := systemRoot "\Cursors\Cursors\HandleOS_Dark\pointer.cur"
    lightPath := systemRoot "\Cursors\HandleOS_Light\pointer.cur"
    
    if (A_Username != "HandleOS") {
        MsgBox, This function is exclusive to HandleOS users.
        return
    }
	; MsgBox, Tema corrente: %currentCursorTheme%`nPercorso Dark: %darkPath%`nPercorso Light: %lightPath% ///FOR DEBUG

    if (currentCursorTheme = darkPath) {
        ; If the current theme is Dark, change to Light theme
        ApplyCursorScheme("HandleOS_Light", "Cursors\HandleOS_Light")
        ; MsgBox, Cambiato a Light! ///FOR DEBUG
    } else {
        ; If the current theme is Light or other, change to Dark theme
        ApplyCursorScheme("HandleOS_Dark", "Cursors\Cursors\HandleOS_Dark")
        ; MsgBox, Cambiato a Dark! ///FOR DEBUG
    }

    ; Call the SystemParametersInfo function to update the cursor theme
    DllCall("SystemParametersInfo", UInt, 0x57, UInt, 0, UInt, 0)

    Sleep 250
    ; Update menu status after changing cursor theme
    UpdateCursorThemeMenu()
return

; Function to enable/disable Tablet Mode
ToggleTabletModeSvc:
    ; Check the current state of the ExpandableTaskbar key
    RegRead, expandableTaskbarValue, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced, ExpandableTaskbar
    RegRead, tabletPostureTaskbarValue, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer, TabletPostureTaskbar
    
    if (ErrorLevel or expandableTaskbarValue = 0 or tabletPostureTaskbarValue = 0) {
        ; If one of the keys does not exist or the value is 0, enable Tablet Mode
        RegWrite, REG_DWORD, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced, ExpandableTaskbar, 1
        RegWrite, REG_DWORD, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer, TabletPostureTaskbar, 1
    } else {
        ; If both keys exist and the value is 1, disable Tablet Mode
        RegWrite, REG_DWORD, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced, ExpandableTaskbar, 0
        RegWrite, REG_DWORD, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer, TabletPostureTaskbar, 0
    }
	
	Sleep 250
    ; Update menu state after changing value
    UpdateTabletModeSvcMenu()
return

; Function to enable/disable Clipboard History
ToggleClipboardHistorySvc:
    ; Check the current state of the Enable Clipboard History key
    RegRead, clipboardHistoryValue, HKEY_CURRENT_USER\Software\Microsoft\Clipboard, EnableClipboardHistory
    
    if (ErrorLevel or clipboardHistoryValue = 0) {
        ; If the key does not exist or the value is 0, enable clipboard history
        RegWrite, REG_DWORD, HKEY_CURRENT_USER\Software\Microsoft\Clipboard, EnableClipboardHistory, 1
		RegWrite, REG_DWORD, HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System, AllowClipboardHistory, 1
    } else {
        ; If the key exists and is 1, disable clipboard history
        RegWrite, REG_DWORD, HKEY_CURRENT_USER\Software\Microsoft\Clipboard, EnableClipboardHistory, 0
		RegWrite, REG_DWORD, HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System, AllowClipboardHistory, 0
    }
	
	Sleep 250
    ; Update menu state after changing value
    UpdateClipboardHistorySvcMenu()
	
	; Ask the user if they want to reboot the system
    MsgBox, 262180, IR4, Would you like to restart your PC now to apply the changes?
    IfMsgBox, Yes
        ShutDown, 2	
return

ToggleWebcamTelemetry:
    ; Check the current status of the webcam telemetry task
    RunWait, %ComSpec% /c schtasks /query /TN "Microsoft\Windows\Device Information\Device" | find "Disabled", , Hide
    if (ErrorLevel = 1) {
        ; Task is enabled, so disable it
        RunWait, %ComSpec% /c schtasks /change /TN "Microsoft\Windows\Device Information\Device" /Disable, , Hide
    } else {
        ; Task is disabled, so enable it
        RunWait, %ComSpec% /c schtasks /change /TN "Microsoft\Windows\Device Information\Device" /Enable, , Hide
    }
	
	Sleep 250
    ; Update the menu icon to reflect the new status
    UpdateWebcamTelemetryMenu()
return

ToggleSpoolerSvc:
    ; Get the current status of the Spooler service
    RunWait, %ComSpec% /c sc query Spooler | find "RUNNING", , Hide
    if (ErrorLevel = 0) {
        ; Spooler service is running, so stop it
        Run, %ComSpec% /c sc config Spooler start= disabled && net stop Spooler && net stop printnotify,, Hide
    } else {
        ; Spooler service is not running, so start it
        Run, %ComSpec% /c sc config Spooler start= auto && net start Spooler && net start printnotify,, Hide
    }
	
	Sleep 250
	; Update SpoolerSVC menu item at startup
	UpdateSpoolerSvcMenu()
return

ToggleUpdateSvc:
    ; Get the current status of the Update service
    RunWait, %ComSpec% /c sc query wuauserv | find "RUNNING", , Hide
    if (ErrorLevel = 0) {
        ; Update service is running, so stop it
        Run, %ComSpec% /c sc config wuauserv start= disabled && sc config bits start= disabled && sc config UsoSvc start= demand && net stop wuauserv && net stop bits && net stop UsoSvc,, Hide
    } else {
        ; Update service is not running, so start it
        Run, %ComSpec% /c sc config wuauserv start= demand && sc config bits start= demand && sc config UsoSvc start= delayed-auto && net start wuauserv && net start bits && net start UsoSvc,, Hide
    }
	
	Sleep 250
	; Update UpdateSVC menu item at startup
	UpdateUpdateSvcMenu()
return

; Function to toggle the video state in the ini file
ToggleVideo:
    global IniFile
    IniRead, VideoState, %IniFile%, Settings, VideoState, 0
    if (VideoState = 0) {
        IniWrite, 1, %IniFile%, Settings, VideoState
    } else {
        IniWrite, 0, %IniFile%, Settings, VideoState
    }
	
	Sleep 150
	; Update Video menu item at startup
	UpdateVideoMenu()
return

QuitNow: ; exit script label 
	RunWait, %ComSpec% /c taskkill /F /IM AudioCCMode.exe,, Hide
	Sleep 250
	ExitApp 
return

; Start battery monitoring at startup
StartBatteryMonitoring()