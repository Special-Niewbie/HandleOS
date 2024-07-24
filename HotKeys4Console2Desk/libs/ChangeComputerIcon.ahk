/*
ChangeComputerIcon

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
ChangeComputerIcon() {
    global IconListBox  ; Declaring the variable as global (This, must to be like that otherwise it doesn't work)
	Gui, Font, s8.8
    Gui, Add, Text,, Select a new icon desktop for My Handled (This PC):
	Gui, Font, s10.6
    Gui, Add, ListBox, vIconListBox w282 h250 gIconSelected, Ally Black (X) |Ally White |AyaNeo |AyaNeo 2 White |AyaNeo 2 Black 
	|AyaNeo Air Pro Black |AyaNeo Kun Silver |AyaNeo Slide Black |Legion Go |Steam Deck |
    Gui, Add, Button, x+-100 y+8 w102 h50 gApplyIcon, Apply
    Gui, Show, w305 h340, Change Computer Icon
    return

    IconSelected:
    GuiControlGet, IconListBox
    return

    ApplyIcon:
    GuiControlGet, IconListBox
    ; MsgBox, Icon selected: %IconListBox%  ; Debug: check which icon has been selected

    ; Determines the path of the DLL icon based on your selection
    if (IconListBox = "Ally Black (X) ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,0"
    } else if (IconListBox = "Ally White ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,1"
    } else if (IconListBox = "AyaNeo ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,2"
    } else if (IconListBox = "AyaNeo 2 White ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,3"
    } else if (IconListBox = "AyaNeo 2 Black ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,4"
	} else if (IconListBox = "AyaNeo Air Pro Black ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,5"
    } else if (IconListBox = "AyaNeo Kun Silver ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,6"
	} else if (IconListBox = "AyaNeo Slide Black ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,7"
    } else if (IconListBox = "Legion Go ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,8"
	} else if (IconListBox = "Steam Deck ") {
        iconPath := "C:\Windows\System32\HandleOS_icons.dll,9"
    }else {
        MsgBox, 16, Error, Invalid icon selected!
        return
    }
    ; MsgBox, Icon path: %iconPath%  ; Debug: check the icon path

    ; Write the new value into the registry
    RegWrite, REG_SZ, HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{20D04FE0-3AEA-1069-A2D8-08002B30309D}\DefaultIcon, , %iconPath%
    
    ; F5 button on desktop
    ControlSend, , {F5}, ahk_class Progman
    ControlSend, , {F5}, ahk_class WorkerW

    ; Success notification
    MsgBox, 64, Success, The computer icon has been changed successfully!
    ;Gui, Destroy
    return

    GuiClose:
    Gui, Destroy
    return
}
