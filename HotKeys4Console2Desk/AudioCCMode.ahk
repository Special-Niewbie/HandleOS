#Persistent
#SingleInstance, Force
#NoTrayIcon
SetWorkingDir, %A_ScriptDir%

; Global variables
global volumeControlActive := false
global initialShellValue := ""
global VolumeBar
global VolumeText

; Initialize the module
InitializeAudioControl()

InitializeAudioControl() {
    ; Create GUI for volume bar and percentage text
    Gui, VolumeBar:New, +AlwaysOnTop +ToolWindow -Caption
	Gui, Color, 1d1d1d
    Gui, VolumeBar:Add, Progress, x10 y10 w200 h20 vVolumeBar
	Gui, VolumeBar:Font, s12 cWhite, Roboto UI
    Gui, VolumeBar:Add, Text, x220 y10 w45 h40 vVolumeText
    Gui, VolumeBar:Show, x-9999 y-9999 NoActivate ; Hide the GUI off-screen initially
    Gui, VolumeBar:Hide

    ; Initialize Registry Monitor
    RegRead, initialShellValue, HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon, Shell
    
    ; Initial check and setup
    UpdateVolumeControl(initialShellValue)

    ; Set up timer for continuous checking
    SetTimer, CheckShellValueTimer, 250
}

; Timer function to call CheckShellValue
CheckShellValueTimer:
CheckShellValue()
return

; Function to check the value of the registry key
CheckShellValue() {
    global initialShellValue
    RegRead, currentShellValue, HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon, Shell
    if (currentShellValue != initialShellValue) {
        initialShellValue := currentShellValue
        UpdateVolumeControl(currentShellValue)
    }
}

; Function to update volume control based on shell value
UpdateVolumeControl(shellValue) {
    global volumeControlActive
    if (shellValue != "explorer.exe") {
        if (!volumeControlActive) {
            ; Enable volume control
            Hotkey, Volume_Up, IncreaseVolume, On
            Hotkey, Volume_Down, DecreaseVolume, On
            volumeControlActive := true
        }
    } else {
        if (volumeControlActive) {
            ; Disable volume control
            Hotkey, Volume_Up, IncreaseVolume, Off
            Hotkey, Volume_Down, DecreaseVolume, Off
            volumeControlActive := false
        }
    }
}

; Function to increase the volume
IncreaseVolume:
    if (volumeControlActive) {
        SoundSet, +5
        ShowVolumeBar()
    }
return

; Function to decrease the volume
DecreaseVolume:
    if (volumeControlActive) {
        SoundSet, -5
        ShowVolumeBar()
    }
return

; Function to show the volume bar
ShowVolumeBar() {
    SoundGet, currentVolume
    currentVolume := Round(currentVolume)
    GuiControl, VolumeBar:, VolumeBar, %currentVolume%
    GuiControl, VolumeBar:, VolumeText, %currentVolume%`%
    Gui, VolumeBar:Show, xCenter yCenter w270 h40 NoActivate
    SetTimer, HideVolumeBar, -1500
}

; Function to hide the volume bar
HideVolumeBar:
    Gui, VolumeBar:Hide
return