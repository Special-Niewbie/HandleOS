using System.Diagnostics;


namespace Console2Desk.FormMessageBox
{
    public class ScriptExecutor
    {
        public static void RunEdgeRemoveScript()
        {         
            string edgeRemoveScript = @"
@echo off
setlocal

REM Check if running with admin permissions
REM Get Admin permissions
net session >nul 2>&1
if not %errorLevel% == 0 (
    echo You must run this script with administrative privileges!
    pause
    exit /b 1
)

REM Flags
REM /s = silent (no printing)
set silent_mode=false
set edge_only_mode=false

REM Check command line arguments
if ""%~1""=="" /s"" set silent_mode=true
if ""%~1""=="" /e"" set edge_only_mode=true
if ""%~1""=="" /?"" (
    echo Usage:
    echo  /s   Silent
    pause
    exit /b
)

REM Edge
taskkill.exe /F /IM ""MicrosoftEdgeUpdate.exe""
if exist ""C:\Program Files (x86)\Microsoft\Edge\Application"" (
    if not %silent_mode% == true echo Removing Microsoft Edge
    rd /s /q ""C:\Program Files (x86)\Microsoft\Edge\Application"" 2>nul
)

REM Remove Edge Appx Packages
set ""user_sid=""
for /f ""tokens=2 delims=="" %%I in ('wmic useraccount where name^=""%username%"" get sid ^| find ""S-1-""') do set ""user_sid=%%I""
if defined user_sid (
    for /f ""delims="" %%A in ('powershell -NoProfile -Command ""Get-AppxPackage -AllUsers | Where-Object {$_.PackageFullName -like '*microsoftedge*'} | Select-Object -ExpandProperty PackageFullName""') do (
        reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Appx\AppxAllUserStore\Deprovisioned"" /v ""%%~nA"" /f >nul
        reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Appx\AppxAllUserStore\EndOfLife\%user_sid%"" /v ""%%~nA"" /f >nul
        reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Appx\AppxAllUserStore\EndOfLife\S-1-5-18"" /v ""%%~nA"" /f >nul
        powershell -Command ""Remove-AppxPackage -Package '%%A' -AllUsers -ErrorAction SilentlyContinue >$null""
        powershell -Command ""Remove-AppxPackage -Package '%%A' -ErrorAction SilentlyContinue >$null""
    )
)

REM Edge Update - Leftovers
rmdir /s /q ""C:\ProgramData\Microsoft\EdgeUpdate"" 2>nul
rmdir /s /q ""C:\Program Files (x86)\Microsoft\EdgeUpdate"" 2>nul

REM Desktop Icons
for /d %%D in (C:\Users\*) do (
    del /f /q ""%%D\Desktop\edge.lnk"" 2>nul
    del /f /q ""%%D\Desktop\Microsoft Edge.lnk"" 2>nul
)

REM Start Menu Icon
del /f /q ""C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Microsoft Edge.lnk"" 2>nul

REM Tasks
schtasks /delete /tn MicrosoftEdge* /f >nul 2>&1

REM Edge Update Services
sc delete edgeupdate >nul 2>&1
sc delete edgeupdatem >nul 2>&1
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\edgeupdate"" /f >nul 2>&1
reg delete ""HKLM\SYSTEM\CurrentControlSet\Services\edgeupdatem"" /f >nul 2>&1

REM Edge Update - Remaining
reg delete ""HKLM\SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate"" /f >nul 2>&1

REM Remaining Edge Keys
if not exist ""C:\Program Files (x86)\Microsoft\Edge\Application\pwahelper.exe"" (
    reg delete ""HKLM\SOFTWARE\WOW6432Node\Microsoft\Edge"" /f >nul 2>&1
)

REM Folders
for /d %%F in (C:\Windows\SystemApps\Microsoft.MicrosoftEdge*) do (
    takeown /f ""%%F"" /r /d y >nul 2>&1
    icacls ""%%F"" /grant administrators:F /t >nul 2>&1
    rmdir /s /q ""%%F""
)

REM System32 Files
for %%F in (C:\Windows\System32\MicrosoftEdge*.exe) do (
    takeown /f ""%%F"" >nul 2>&1
    icacls ""%%F"" /inheritance:e /grant ""%username%:(OI)(CI)F"" /T /C >nul 2>&1
    del /f /q ""%%F""
)

REM Remaining File
if exist ""C:\Program Files (x86)\Microsoft\Edge\Edge.dat"" (
    del /f /q ""C:\Program Files (x86)\Microsoft\Edge\Edge.dat""
)

REM Left over folders
rmdir /s /q ""C:\Program Files (x86)\Microsoft\Temp"" 2>nul

echo Removal completed.
exit /b
";

            // Create temporary .bat file
            string tempFile = Path.Combine(Path.GetTempPath(), "EdgeRemoveScript.bat");
            File.WriteAllText(tempFile, edgeRemoveScript);

            // Run the script as administrator
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = tempFile,
                UseShellExecute = true,
                Verb = "runas" // Requires administrative privileges
            };

            try
            {
                Process proc = Process.Start(psi);
                proc.WaitForExit(); // Wait for the script to finish executing

                // Delete the temporary .bat file
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore nell'esecuzione dello script: " + ex.Message);
            }
        }

        public static void RunOneDriveRemoveScript()
        {
            string oneDriveRemoveScript = @"
@echo off

REM Description: This script will remove and disable OneDrive integration.
echo Kill OneDrive process...
taskkill.exe /F /IM ""OneDrive.exe""
taskkill.exe /F /IM ""explorer.exe""

echo Remove OneDrive.
if exist ""%systemroot%\System32\OneDriveSetup.exe"" (
    ""%systemroot%\System32\OneDriveSetup.exe"" /uninstall
)
if exist ""%systemroot%\SysWOW64\OneDriveSetup.exe"" (
    ""%systemroot%\SysWOW64\OneDriveSetup.exe"" /uninstall
)

echo Removing OneDrive leftovers...
rmdir /s /q ""%localappdata%\Microsoft\OneDrive"" 2>nul
rmdir /s /q ""%programdata%\Microsoft OneDrive"" 2>nul
rmdir /s /q ""%systemdrive%\OneDriveTemp"" 2>nul

REM Check if directory is empty before removing:
if not exist ""%userprofile%\OneDrive\*"" (
    rmdir /s /q ""%userprofile%\OneDrive"" 2>nul
)

echo Disable OneDrive via Group Policies.
reg add ""HKLM\SOFTWARE\Wow6432Node\Policies\Microsoft\Windows\OneDrive"" /v ""DisableFileSyncNGSC"" /t REG_DWORD /d 1 /f

echo Remove Onedrive from explorer sidebar.
reg load ""HKU\Default"" ""%userprofile%\Default\NTUSER.DAT""
reg delete ""HKEY_USERS\Default\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"" /v ""OneDriveSetup"" /f
reg unload ""HKU\Default""

echo Removing run hook for new users...
reg load ""hku\Default"" ""C:\Users\Default\NTUSER.DAT""
reg delete ""HKEY_USERS\Default\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"" /v ""OneDriveSetup"" /f
reg unload ""hku\Default""

echo Removing startmenu entry...
del /f /q ""%userprofile%\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\OneDrive.lnk"" 2>nul

echo Removing scheduled task...
schtasks.exe /delete /tn ""OneDrive*"" /f 2>nul

echo Restarting explorer...
start explorer.exe

echo Waiting for explorer to complete loading...
ping -n 6 127.0.0.1 > nul
";

            // Create temporary .bat file
            string tempFile = Path.Combine(Path.GetTempPath(), "OneDriveRemoveScript.bat");
            File.WriteAllText(tempFile, oneDriveRemoveScript);

            // Run the script as administrator
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = tempFile,
                UseShellExecute = true,
                Verb = "runas" // Requires administrative privileges
            };

            try
            {
                Process proc = Process.Start(psi);
                proc.WaitForExit(); // Wait for the script to finish executing

                // Delete the temporary .bat file
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore nell'esecuzione dello script: " + ex.Message);
            }
        }

    }
}
