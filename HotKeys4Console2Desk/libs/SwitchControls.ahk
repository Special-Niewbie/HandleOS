/*
SwitchControls

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
SetWorkingDir, %A_ScriptDir%

imagePathController := A_ScriptDir . "\sources\Controller_List.png"

; Function to check and create .ini file if it does not exist
CheckAndCreateSwitch_ControlsIni() {
    if (!FileExist("switch_controls.ini")) {
        FileAppend, control.BACK && control.LB && control.RB, switch_controls.ini
    }
}

; Function to check if a control is present in the configuration
IsControlChecked(existingConfig, control) {
    return RegExMatch(existingConfig, "control\." . control . "(\s*&&|\s*$)")
}

ShowConfigGUI() {
    global
    
    ; Read existing configuration
    FileRead, existingConfig, switch_controls.ini
	
	windowWidth := 580
    
    Gui, New, , Button Configuration for Window Switching
    Gui, Font, s15
    
    ; Controls Array
    controlList := ["GUIDE", "START", "BACK", "UP", "DOWN", "LEFT", "RIGHT", "A", "B", "X", "Y", "LB", "RB", "LT", "RT", "LS", "RS", "LSX", "LSY", "RSX", "RSY"]
    
    ; First column
	Gui, Add, Text, x+10 y-15
    for i, control in ["GUIDE", "START", "BACK"] {
        checked := IsControlChecked(existingConfig, control) ? "Checked" : ""
        Gui, Add, Checkbox, v%control% w100 h40 %checked%, %control%
    }
	Gui, Add, Button, x10 y+55 w80 gSaveConfig, Save Configuration
    
    ; Second column (UP, DOWN, LEFT, RIGHT)
    Gui, Add, Text, x+15 y-16
    for i, control in ["UP", "DOWN", "LEFT", "RIGHT"] {
        checked := IsControlChecked(existingConfig, control) ? "Checked" : ""
        Gui, Add, Checkbox, v%control% w100 h40 %checked%, %control%
    }
    
    ; Third column (A, B, X, Y)
    Gui, Add, Text, x+32 y-17
    for i, control in ["A", "B", "X", "Y"] {
        checked := IsControlChecked(existingConfig, control) ? "Checked" : ""
        Gui, Add, Checkbox, v%control% w100 h40 %checked%, %control%
    }
    
    ; Quarta colonna
    Gui, Add, Text, x+4 y-18
    for i, control in ["LB", "RB", "LT", "RT", "LS"] {
        checked := IsControlChecked(existingConfig, control) ? "Checked" : ""
        Gui, Add, Checkbox, v%control% w100 h40 %checked%, %control%
    }
    
    ; Fifth column
    Gui, Add, Text, x+10 y-18
    for i, control in ["RS", "LSX", "LSY", "RSX", "RSY"] {
        checked := IsControlChecked(existingConfig, control) ? "Checked" : ""
        Gui, Add, Checkbox, v%control% w100 h40 %checked%, %control%
    }
    
    ; Add image to center under columns
    Gui, Add, Text, x10 y+20 w550 0x10  ; Linea separatrice
    Gui, Add, Picture, x15 y+10 w550 h-1 Center, %imagePathController%
    
    Gui, Font, s10
    Gui, Show, w%windowWidth%
}

SaveConfig() {
    Gui, Submit, NoHide
    configString := ""
    
    controlList := ["GUIDE", "START", "BACK", "UP", "DOWN", "LEFT", "RIGHT", "A", "B", "X", "Y", "LB", "RB", "LT", "RT", "LS", "RS", "LSX", "LSY", "RSX", "RSY"]
    
    for index, control in controlList {
        if (%control% == 1) {
            if (configString != "")
                configString .= " && "
            configString .= "control." . control
        }
    }
    
    if (configString != "") {
        FileDelete, switch_controls.ini
        FileAppend, %configString%, switch_controls.ini
        MsgBox, Configuration saved successfully!
    } else {
        MsgBox, No controls selected. Please select at least one control.
		return
    }
    Gui, Destroy
}

CheckSwitchCombination(control) {
    
    FileRead, configString, switch_controls.ini
    if (ErrorLevel) {
        MsgBox, Error reading switch_controls.ini file.
        return false
    }
    
    conditions := StrSplit(configString, " && ")
    for index, condition in conditions {
        parts := StrSplit(condition, ".")
        if (parts.Length() == 2 && parts[1] == "control") {
            if (!control[parts[2]]) {
                return false
            }
        }
    }
    return true
}