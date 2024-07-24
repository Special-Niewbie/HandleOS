/*
BatteryEffect

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
;---------------------------------------------------------------------------------------------------------------
; To inlude this library in your project, you have 2 functions as below:
; StartBatteryMonitoring(): is a function that initializes battery monitoring and starts an internal self loop to check the battery status.

; BatteryCheck(): is the function that needs to be executed periodically to monitor the battery status. 
; This function should be called within a timer or loop.
*/
; BatteryEffect.ahk
; Set the path of images and sounds
ImagePath := "C:\Program Files\Console2Desk\Battery\"  ; Edit with correct image path
SoundPath := "C:\Windows\Media\"  ; Edit with the correct path of the sounds
ImagePathBCharging := "C:\Program Files\Console2Desk\Battery\"
SoundPathBCharging := "C:\Windows\Media\"
DisconnectSound := SoundPath "Windows Balloon.wav"  ; Path to the Windows Balloon sound

; Battery levels to monitor
BatteryLevels := [20, 10, 5, 2]
BatteryWarnings := {}
BatteryChargingPrev := true ; Assumes that the notebook starts plugged in

Loop % BatteryLevels.MaxIndex() {
    level := BatteryLevels[A_Index]
    BatteryWarnings[level] := false
}

BatteryCheck() {
    ; Get current battery level and charging status
    BatteryLevel := GetBatteryLevel()
    BatteryCharging := GetBatteryChargingStatus()
    global BatteryWarnings, BatteryLevels, ImagePath, SoundPath, BatteryChargingPrev, DisconnectSound
	
	/*
    ; FOR DEBUG Debug message for battery level and charging status
    ToolTip % "Battery level: " BatteryLevel "% - Status: " (BatteryCharging ? "Charging" : "Discharging")
	*/

    ; If the charging status has changed from empty to charging, it shows the charging icon
    if (BatteryCharging && !BatteryChargingPrev) {
        ShowChargingIcon()
    }
	
	; If the charging status has changed from charging to empty, reset the alerts
    if (!BatteryCharging && BatteryChargingPrev) {
        Loop % BatteryLevels.MaxIndex() {
            level := BatteryLevels[A_Index]
            BatteryWarnings[level] := false
        }
		SoundPlay %DisconnectSound%
    }
    BatteryChargingPrev := BatteryCharging

    ; Only check if the battery is not charging (discharged)
    if (!BatteryCharging) {
        Loop % BatteryLevels.MaxIndex() {
            level := BatteryLevels[A_Index]
            if (BatteryLevel <= level && !BatteryWarnings[level]) {
			
                /*
                ; FOR DEBUG Debug message when a critical battery level is detected
                ToolTip % "Battery Warning: " level "%"				
                Sleep 1000 ; Show the ToolTip for 1 second
				*/

                ; Show the image and sound the alert
                ShowBatteryWarning(level)
                BatteryWarnings[level] := true
            }
        }
    }
}

StartBatteryMonitoring() {
    ; Mark how to handle all levels less than or equal to the current level on first launch
    BatteryLevel := GetBatteryLevel()
    Loop % BatteryLevels.MaxIndex() {
        level := BatteryLevels[A_Index]
        if (BatteryLevel <= level) {
            BatteryWarnings[level] := true
        }
    }

    ; Start battery monitoring
    BatteryCheck()
    Sleep 5000  ; Check the battery every 5 seconds
}

GetBatteryLevel() {
    ; Get current battery level via Windows API
    VarSetCapacity(SYSTEM_POWER_STATUS, 1+1+1+1+1+4+4+4+1+1) ; Ensures that the SYSTEM_POWER_STATUS structure is allocated correctly
    DllCall("GetSystemPowerStatus", "Ptr", &SYSTEM_POWER_STATUS)
    BatteryLifePercent := NumGet(SYSTEM_POWER_STATUS, 2, "UChar")
    if (BatteryLifePercent < 0 or BatteryLifePercent > 100)
        BatteryLifePercent := -1
    return BatteryLifePercent
}

GetBatteryChargingStatus() {
    ; Get battery charge status via Windows API
    VarSetCapacity(SYSTEM_POWER_STATUS, 1+1+1+1+1+4+4+4+1+1) ; Ensures that the SYSTEM_POWER_STATUS structure is allocated correctly
    DllCall("GetSystemPowerStatus", "Ptr", &SYSTEM_POWER_STATUS)
    ACLineStatus := NumGet(SYSTEM_POWER_STATUS, 0, "UChar")
    return (ACLineStatus = 1) ; 1 means it is connected to the power (charging)
}

ShowChargingIcon() {
    global ImagePathBCharging, SoundPathBCharging
	
    BChargingFile := ImagePathBCharging "BCharging.png"
	SoundBChargingFile := SoundPathBCharging "Windows Battery Low.wav"

    ; Get screen resolution
    SysGet, ScreenWidth, 78
    SysGet, ScreenHeight, 79

    ; Calculate the position of the window (top right)
    ImageWidth := 45  ; Image width
    ImageHeight := 22  ; Image height
    PosX := ScreenWidth - ImageWidth - 30  ; 30 pixels margin from the right edge
    PosY := 10  ; 10 pixel margin from the top edge

    ; Show Image
    Gui, New
    Gui, +AlwaysOnTop +ToolWindow -Caption +LastFound
    Gui, Color, 0x000000  ; Background Black Color
    Gui, Add, Picture, x0 y0 w%ImageWidth% h%ImageHeight%, %BChargingFile%
    WinSet, TransColor, 0x000000
    Gui, Show, NoActivate x%PosX% y%PosY%
	
	; Play sound
    SoundPlay %SoundBChargingFile%
    
    ; Shows the image for 3 seconds and then destroys it
    Sleep 3000
    Gui, Destroy
}

ShowBatteryWarning(level) {
    global ImagePath, SoundPath

    ImageFile := ImagePath "B" level ".png"
    SoundFile := SoundPath "Speech Off.wav"

    /*
    ; Debug messages to verify file paths
    ToolTip % "ImageFile: " ImageFile "`nSoundFile: " SoundFile
    Sleep 2000 ; Show the ToolTip for 2 seconds
	*/

    ; Check the existence of the files before proceeding
    if (!FileExist(ImageFile)) {
        MsgBox, 16, Errore, Impossibile trovare il file immagine: %ImageFile%
        return
    }
    if (!FileExist(SoundFile)) {
        MsgBox, 16, Errore, Impossibile trovare il file audio: %SoundFile%
        return
    }
    
    ; Get screen resolution
    SysGet, ScreenWidth, 78
    SysGet, ScreenHeight, 79

    ; Calculate the position of the window (top right)
    ImageWidth := 45  ; Image width
    ImageHeight := 22  ; Image height
    PosX := ScreenWidth - ImageWidth - 30  ; 30 pixels margin from the right edge
    PosY := 10  ; 10 pixel margin from the top edge

    ; Show Image 1
    Gui, New
    Gui, +AlwaysOnTop +ToolWindow -Caption +LastFound
    Gui, Color, 0x000000  ; Background Black Color
    Gui, Add, Picture, x0 y0 w%ImageWidth% h%ImageHeight%, %ImageFile%
    WinSet, TransColor, 0x000000
    Gui, Show, NoActivate x%PosX% y%PosY%
    
    ; Play Sound
    SoundPlay %SoundFile%
    
    Sleep 1000
    Gui, Destroy
    Sleep 1000
    
    ; Show Image 2
    Gui, New
    Gui, +AlwaysOnTop +ToolWindow -Caption +LastFound
    Gui, Color, 0x000000  ; Background Black Color
    Gui, Add, Picture, x0 y0 w%ImageWidth% h%ImageHeight%, %ImageFile%
    WinSet, TransColor, 0x000000
    Gui, Show, NoActivate x%PosX% y%PosY%
    
    Sleep 1000
    Gui, Destroy
    Sleep 1000
    
    ; Show Image 3
    Gui, New
    Gui, +AlwaysOnTop +ToolWindow -Caption +LastFound
    Gui, Color, 0x000000  ; Background Black Color
    Gui, Add, Picture, x0 y0 w%ImageWidth% h%ImageHeight%, %ImageFile%
    WinSet, TransColor, 0x000000
    Gui, Show, NoActivate x%PosX% y%PosY%
    
    Sleep 1000
    Gui, Destroy
}

