/*
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
#Include libs\XBOX360.ahk
#Include libs\BatteryEffect.ahk
#Include libs\ChangeComputerIcon.ahk

#Persistent

IniFile := A_ScriptDir . "\on_off.ini"
CheckAndCreateIni()

Console2DeskExePath := "C:\Program Files\Console2Desk\Console2Desk.exe"
PlayniteFullscreenExePath := A_AppData . "\..\Local\Playnite\Playnite.FullscreenApp.exe"
PlayniteDesktopExePath := A_AppData . "\..\Local\Playnite\Playnite.DesktopApp.exe"

; Variabili globali
global isVideoPlaying := false
global playniteStarted := false
global firstRun := true
global timerStarted := false 

; Check for Updates
CheckForUpdates()

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
Menu, Tray, Add, Print Service ON/OFF, ToggleSpoolerSvc  ; SpoolerSVC toggle option
Menu, Tray, Add, Update Service ON/OFF, ToggleUpdateSvc  ; UpdateSVC toggle option
Menu, Tray, Add, Splash Video ON/OFF, ToggleVideo  ; Video toggle option
Menu, Tray, Add, Exit, QuitNow ; added exit script option

; Update SpoolerSVC menu item at startup
UpdateSpoolerSvcMenu()
; Update UpdateSVC menu item at startup
UpdateUpdateSvcMenu()
; Update Video menu item at startup
UpdateVideoMenu()

;Instance the Controller Manager
manager := new Xbox360LibControllerManager()

;Initialize Controller (0-3), you can use 4 controllers
player1 := manager.InitializeController(0)

SetTimer, CheckController, 350  ; Runs the Check Controller function every 350 milliseconds.
SetTimer, CheckPlaynite, 250  ; Runs the Check Playnite function every 500 milliseconds after initial script run.

; Function to check the status of the controller.
CheckController:

	player1.Update()
	BatteryCheck()

	; Manages the BACK+START key combination
    if (player1.BACK && player1.START) {
        ;Run, explorer.exe "C:\Program Files\Console2Desk\Console2Desk.exe"  Old implementation	
		;Run, %ComSpec% /c ""%Console2DeskExePath%"", , Hide	             Old implementation	
		; Stop the scheduled task first
        Run, schtasks /end /tn "Console2Desk",, Hide
		
		Sleep 250
		; Then Start the scheduled task
        Run, schtasks /run /tn "Console2Desk",, Hide
		
		; Set the vibration
        player1.BV := [65535, 65535]   ; Set both engine speeds to maximum
        Sleep 400                      ; Wait 400 milliseconds
        player1.BV := [0, 0]           ; Turn off the vibration
		
	}
	
	; Manages BACK + LB for the ALT key
    if (player1.BACK && player1.LB) {
        Send, {Alt down}
    } else {
        Send, {Alt up}
    }

    ; Gestisce RB
    if (player1.RB) {
        if (player1.BACK && player1.LB) {
            ; If BACK and LB are pressed, minimize all windows and send TAB
            WinMinimizeAll
			
            Sleep, 200  ; Wait to make sure all windows have been minimized
            Send, {Tab}
            player1.BV := [65535, 65535]
            Sleep, 100  ; Delay to prevent rapid multiple presses
            player1.BV := [0, 0]
        }
        ; If BACK and LB are not pressed, do nothing (RB works normally)
    }
	
	; Manages LB + LS for the ALT key
    if (player1.LB && player1.LS) {
        Send, {Alt down}
    } else {
        Send, {Alt up}
    }
	
	; Manages X
    if (player1.X) {
        if (player1.LB && player1.LS) {
            ; If LB and LS are pressed, minimize all windows and send TAB
            WinMinimizeAll
			
            Sleep, 200  ; Wait to make sure all windows have been minimized
            Send, {Tab}
            player1.BV := [65535, 65535]
            Sleep, 100  ; Delay to prevent rapid multiple presses
            player1.BV := [0, 0]
        }
        ; If LB and LS are not pressed, do nothing (X works normally)
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
    player1.BV := [65535, 65535]   ; Set both engine speeds to maximum
    Sleep 400                      ; Wait 400 milliseconds
    player1.BV := [0, 0]           ; Turn off the vibration

return

OpenScriptSite() {
    Run, https://github.com/Special-Niewbie/
}

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
            MsgBox, 64, Update Available, A new version is available: %latestVersion%`n`nYou are currently using version: %localVersion%`n`nWould you like to visit the project page on GitHub to Download it?
            ifMsgBox, OK
                Run, https://github.com/Special-Niewbie/HandleOS
        }
    }
}

UpdateSpoolerSvcMenu() {
    ; Get the current status of the Spooler service
    RunWait, %ComSpec% /c sc query spooler | find "RUNNING", , Hide
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

; Function to check and create the ini file if it doesn't exist
CheckAndCreateIni() {
    global IniFile
    if (!FileExist(IniFile)) {
        FileAppend, [Settings]`nVideoState=0, %IniFile%
    }
}

; Function to update the video menu item based on the ini file value
UpdateVideoMenu() {
    global IniFile
    IniRead, VideoState, %IniFile%, Settings, VideoState, 0
    if (VideoState = 0) {
        Menu, Tray, Icon, Splash Video ON/OFF, Imageres.dll, 19  ; Icon for Video ON
    } else {
        Menu, Tray, Icon, Splash Video ON/OFF, Imageres.dll, 94  ; Icon for Video OFF
    }
}

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

ToggleSpoolerSvc:
    ; Get the current status of the Spooler service
    RunWait, %ComSpec% /c sc query spooler | find "RUNNING", , Hide
    if (ErrorLevel = 0) {
        ; Spooler service is running, so stop it
        Run, %ComSpec% /c sc config spooler start= disabled && net stop spooler && net stop printnotify,, Hide
    } else {
        ; Spooler service is not running, so start it
        Run, %ComSpec% /c sc config spooler start= auto && net start spooler && net start printnotify,, Hide
    }
	Sleep 500
    ; Update the menu text
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
	Sleep 500
    ; Update the menu text
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
    Sleep 500
    UpdateVideoMenu()
return

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

TabsNavigation() {

}


; Function to check if a process is running.
ProcessExists(processName) {
    Process, Exist, %processName%
    return ErrorLevel
}

#IfWinExist ahk_exe ffplay.exe
^F3:: ; CTRL-F3 = Esci dal lettore e dallo script
    SoundBeep, 1000
    Process, Close, ffplay.exe
    ExitApp
#IfWinExist

; PAUSE:
	;Pause Toggle
; return

QuitNow: ; exit script label 
	ExitApp 
return

; Start battery monitoring at startup
StartBatteryMonitoring()