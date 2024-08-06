/*
Console2Desk

Copyright (C) 2023 Special-Niewbie Softwares

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

using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Console2Desk.TouchButtons;
using Console2Desk.FuturesButtons;
using Console2Desk.BottomWindowButtons;
using Console2Desk.SettingsButton;


namespace Console2Desk
{
    public partial class Form1 : Form
    {
        // Import functions from User32.dll
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW;

        private System.Windows.Forms.Timer _topMostTimer;
        private bool _isAboutBoxOpen = false;

        private System.Windows.Forms.Timer _wifiDelayTimer;
        private bool _isWifiDelayActive = false;
        private const int WifiDelay = 50000; // 50 seconds in milliseconds


        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        private int minWidth; // Minimum desired width
        private int minHeight; // Minimum height desired

        // Some fields for the paths of the two programs
        public string explorerPath = "explorer.exe";
        public string fullscreenAppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Playnite", "Playnite.FullscreenApp.exe");
        public string desktopAppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Playnite", "Playnite.DesktopApp.exe");
        private string defaultFullscreenSteamAppPath = @"C:\Program Files (x86)\Steam\steam.exe";

        private WinTheme winTheme;

        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Load += Form1_Load;
            SetForegroundWindow(this.Handle);

            winTheme = new WinTheme(this.Handle);
            winTheme.ApplyTheme();

            // Read fullscreen app path from settings Settings.ini
            string settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Console2Desk", "Settings.ini");
            if (File.Exists(settingsPath))
            {
                fullscreenAppPath = File.ReadAllLines(settingsPath)[0];
            }
            else
            {
                fullscreenAppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Playnite", "Playnite.FullscreenApp.exe");
            }

            #region First Events part
            panel2.MouseDown += panel2_MouseDown;
            panel2.MouseMove += panel2_MouseMove;
            panel2.MouseUp += panel2_MouseUp;
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseMove += panel1_MouseMove;
            panel1.MouseUp += panel1_MouseUp;

            buttonAllyUltimateRes.MouseEnter += buttonAllyUltimateRes_MouseEnter;
            buttonAllyUltimateRes.MouseLeave += buttonAllyUltimateRes_MouseLeave;
            button1610UltimateRes.MouseEnter += button1610UltimateRes_MouseEnter;
            button1610UltimateRes.MouseLeave += button1610UltimateRes_MouseLeave;

            buttonAllyStockRes.MouseEnter += buttonAllyStockRes_MouseEnter;
            buttonAllyStockRes.MouseLeave += buttonAllyStockRes_MouseLeave;
            button1610_StockRes.MouseEnter += button1610_StockRes_MouseEnter;
            button1610_StockRes.MouseLeave += button1610_StockRes_MouseLeave;

            buttonIntegerScaling.MouseEnter += buttonIntegerScaling_MouseEnter;
            buttonIntegerScaling.MouseLeave += buttonIntegerScaling_MouseLeave;
            button_IncreaseRAM_System.MouseEnter += button_IncreaseRAM_System_MouseEnter;

            desktopButton1.Click += desktopButton1_Click;
            consoleButton1.Click += consoleButton1_Click;
            #endregion
            //this.Resize += Form1_Resize;
            CheckRegistrySettings();
            #region Second Events part
            panel2.Click += Panel2_Click;

            pictureBox1.MouseHover += PictureBox1_MouseHover;
            pictureBox1.MouseLeave += PictureBox1_MouseLeave;
            pictureBox2.MouseHover += PictureBox2_MouseHover;
            pictureBox2.MouseLeave += PictureBox2_MouseLeave;
            pictureBoxStockRes1610.MouseLeave += pictureBoxStockRes1610_MouseLeave;
            pictureBox3.MouseHover += PictureBox3_MouseHover;
            pictureBox3.MouseLeave += PictureBox3_MouseLeave;
            pictureBoxCheckVRAM.MouseHover += pictureBoxCheckVRAM_MouseHover;
            pictureBoxCheckVRAM.MouseLeave += pictureBoxCheckVRAM_MouseLeave;

            pictureBoxAMDadrenaline.MouseDown += pictureBoxAMDadrenaline_MouseDown;
            pictureBoxAMDadrenaline.MouseUp += pictureBoxAMDadrenaline_MouseUp;
            pictureBoxAMDadrenaline.MouseHover += pictureBoxAMDadrenaline_MouseHover;
            pictureBoxAMDadrenaline.MouseLeave += pictureBoxAMDadrenaline_MouseLeave;

            pictureBoxAMDadrenalinePress.MouseUp += pictureBoxAMDadrenalinePress_MouseUp;

            pictureBoxDonation1.MouseDown += pictureBoxDonation1_MouseDown;
            pictureBoxDonation1.MouseUp += pictureBoxDonation1_MouseUp;
            pictureBoxDonation2.MouseUp += pictureBoxDonation2_MouseUp;

            buttonRestorePauseUpgrade.MouseUp += buttonRestorePauseUpgrade_MouseUp;
            buttonOpenFileExplorer.MouseUp += buttonOpenFileExplorer_MouseUp;
            touchScreenEnDbButton.MouseUp += touchScreenEnDbButton_MouseUp;
            buttonXinputTest.MouseUp += buttonXinputTest_MouseUp;
            buttonChangeConsoleSettings.MouseUp += buttonChangeConsoleSettings_MouseUp;
            msStoreButton.MouseUp += msStoreButton_MouseUp;
            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set the form as TopMost
            SetAlwaysOnTopRegistry();
            BringToFront();

            // Timer to keep the window on top
            _topMostTimer = new System.Windows.Forms.Timer();
            _topMostTimer.Interval = 1000; // 1 second
            _topMostTimer.Tick += (s, args) =>
            {
                if (!_isAboutBoxOpen && DependencyContainer.MessagesBoxImplementation != null &&
                    DependencyContainer.MessagesBoxImplementation.IsTimerActive && !_isWifiDelayActive)
                {
                    SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                    SetForegroundWindow(this.Handle);
                    BringToFront();
                }
            };
            _topMostTimer.Start();

            // Timer for handling WiFi button delay
            _wifiDelayTimer = new System.Windows.Forms.Timer();
            _wifiDelayTimer.Interval = WifiDelay; // 10 seconds
            _wifiDelayTimer.Tick += (s, args) =>
            {
                _isWifiDelayActive = false;
                _topMostTimer.Start(); // Resume the main timer after delay
                _wifiDelayTimer.Stop(); // Stop the delay timer
            };

            // Initialize MessagesBoxImplementation and set it in the DependencyContainer
            DependencyContainer.MessagesBoxImplementation = new MessagesBoxImplementation(this, _topMostTimer);

            // Catch the initial size of the window
            minWidth = this.Width;
            minHeight = this.Height;
            this.MinimumSize = new Size(minWidth, minHeight);


            RAMChecker ramChecker = new RAMChecker(pictureBoxCheckVRAM);
            ramChecker.CheckVirtualMemorySettingsAsync(DependencyContainer.MessagesBoxImplementation);

            CodeForAMD_NoShutterCheck.CheckEnableUlps(controlAMDnoShutter, DependencyContainer.MessagesBoxImplementation);
            CodeForSystemDevicesCheck.CheckSystemDevices(controlSystemDevices, DependencyContainer.MessagesBoxImplementation);
            CodeForControlCoreIsolation_ExploidCheck.CheckCoreIsolationAndExploitProtection(controlCoreIsolation_Exploid, DependencyContainer.MessagesBoxImplementation);
            CodeForControlBCDMemoryUsageCheck.CheckBCDMemoryUsage(controlBCDMemoryUsage, DependencyContainer.MessagesBoxImplementation);
            CodeForReduceWindowsLatencyCheck.CheckReduceWindowsLatency(controlReduceWindowsLatency, DependencyContainer.MessagesBoxImplementation);
            msStoreButtonStartupCheck.CheckMsStoreInstallation(msStoreButton, DependencyContainer.MessagesBoxImplementation);

            try
            {
                bool keyFound = false; // Flag to indicate whether the key was found

                // Array containing the registry key paths to search for
                string[] registryKeys = {
            @"SYSTEM\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000",
            @"SYSTEM\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0001",
            @"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000",
            @"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0001"
        };

                // For each registry key path
                foreach (string keyPath in registryKeys)
                {
                    // Attempt to open the registry key
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath))
                    {
                        if (key != null)
                        {
                            // Check if the "DalEmbeddedIntegerScalingSupport" entry exists in the key
                            if (key.GetValue("DalEmbeddedIntegerScalingSupport") != null)
                            {
                                // If the entry was found, set the flag to true and stop the search
                                keyFound = true;
                                break;
                            }
                        }
                    }
                }

                // If the key has been found, make the pictureBox3 visible
                if (keyFound)
                {
                    pictureBox3.Visible = true;
                }
                else
                {
                    pictureBox3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, displays a generic error message
                DependencyContainer.MessagesBoxImplementation.ShowMessage("Error checking registry keys: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Manage window state changes
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                SetForegroundWindow(this.Handle); // Ensures the window stays on top
            }

            // Manage minimum resizing
            if (this.Width < minWidth || this.Height < minHeight)
            {
                // If the size is below the limit, restore the size
                this.Width = Math.Max(this.Width, minWidth);
                this.Height = Math.Max(this.Height, minHeight);
            }
        }

        private void SetAlwaysOnTopRegistry()
        {
            try
            {
                // Registry key path
                string registryPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";

                // Open the existing registry key or create a new key if it does not exist
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true) ?? Registry.CurrentUser.CreateSubKey(registryPath))
                {
                    if (key != null)
                    {
                        // set AlwaysOnTop
                        key.SetValue("AlwaysOnTop", 1, RegistryValueKind.DWord);
                    }
                }
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error editing registry: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        // Function to compare two byte arrays
        private bool ArraysEqual(byte[] a1, byte[] a2)
        {
            if (a1 == null || a2 == null || a1.Length != a2.Length)
            {
                return false;
            }
            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i])
                {
                    return false;
                }
            }
            return true;
        }
        public void CheckRegistrySettings()
        {
            #region Check the Registry Values for Resolutions Checked/Unchecked
            try
            {
                // Gets all the names of the subkeys in the main registry key
                string[] subKeyNames = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}").GetSubKeyNames();

                bool found16_9 = false;
                bool found16_10 = false;

                // Iterate through all subkey names to find the one with the correct DriverDesc
                foreach (string subKeyName in subKeyNames)
                {
                    try
                    {
                        using (RegistryKey key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}"))
                        {
                            if (key != null && (string)key.GetValue("DriverDesc") == "AMD Radeon Graphics")
                            {
                                // Check if the registry keys match the expected values for 16:9 resolutions
                                if (ArraysEqual((byte[])key.GetValue("DALNonStandardModesBCD1"), new byte[] {
                            0x19, 0x20, 0x10, 0x80, 0x17, 0x60, 0x09, 0x90,
                            0x16, 0x00, 0x09, 0x00, 0x13, 0x66, 0x07, 0x68,
                            0x12, 0x80, 0x07, 0x20, 0x11, 0x20, 0x06, 0x30,
                            0x09, 0x60, 0x05, 0x40, 0x16, 0x00, 0x09, 0x00,
                            0x13, 0x66, 0x07, 0x68, 0x12, 0x80, 0x07, 0x20,
                            0x10, 0x24, 0x05, 0x76, 0x09, 0x60, 0x05, 0x40,
                            0x08, 0x54, 0x04, 0x80, 0x06, 0x40, 0x03, 0x60
                        }) &&
                                ArraysEqual((byte[])key.GetValue("DALRestrictedModesBCD1"), new byte[] {
                            0x51, 0x20, 0x28, 0x80, 0x38, 0x40, 0x24, 0x00,
                            0x51, 0x20, 0x21, 0x60, 0x38, 0x40, 0x21, 0x60,
                            0x32, 0x00, 0x18, 0x00, 0x30, 0x72, 0x17, 0x28,
                            0x38, 0x40, 0x16, 0x20, 0x25, 0x60, 0x16, 0x00
                                }) &&
                                ArraysEqual((byte[])key.GetValue("DALRestrictedModesBCD2"), new byte[] {
                            0x20, 0x48, 0x15, 0x36, 0x25, 0x60, 0x14, 0x40,
                            0x19, 0x20, 0x12, 0x00, 0x16, 0x00, 0x12, 0x00,
                            0x16, 0x80, 0x10, 0x50, 0x12, 0x80, 0x10, 0x24,
                            0x16, 0x00, 0x10, 0x00, 0x12, 0x80, 0x08, 0x00
                                }) &&
                                ArraysEqual((byte[])key.GetValue("DALRestrictedModesBCD3"), new byte[] {
                            0x10, 0x24, 0x07, 0x68, 0x11, 0x28, 0x06, 0x34,
                            0x08, 0x00, 0x06, 0x00, 0x06, 0x40, 0x04, 0x80,
                            0x08, 0x54, 0x04, 0x80
                                }) &&
                                ArraysEqual((byte[])key.GetValue("DALRestrictedModesBCD4"), new byte[] {
                            0x10, 0x24, 0x07, 0x68, 0x11, 0x28, 0x06, 0x34,
                            0x08, 0x00, 0x06, 0x00, 0x06, 0x40, 0x04, 0x80,
                            0x08, 0x54, 0x04, 0x80
                                }) &&
                                ArraysEqual((byte[])key.GetValue("DALRestrictedModesBCD5"), new byte[] {
                            0x11, 0x20, 0x06, 0x30, 0x10, 0x24, 0x07, 0x68,
                            0x09, 0x60, 0x05, 0x40, 0x08, 0x54, 0x04, 0x80,
                            0x03, 0x20, 0x02, 0x40
                                }))
                                {
                                    // If the registry keys match the expected values for 16:9, show pictureBox1
                                    pictureBox1.Visible = true;
                                    pictureBox1610.Visible = false;
                                    pictureBox2.Visible = false;
                                    pictureBoxStockRes1610.Visible = false;
                                    found16_9 = true;
                                    break;
                                }

                                // Check if the registry keys match the expected values for 16:10 resolutions
                                if (ArraysEqual((byte[])key.GetValue("DALNonStandardModesBCD1"), new byte[] {
                            0x19, 0x20, 0x12, 0x00, 0x17, 0x60, 0x10, 0x80,
                            0x16, 0x00, 0x10, 0x00, 0x14, 0x40, 0x09, 0x00,
                            0x12, 0x80, 0x08, 0x00, 0x11, 0x20, 0x07, 0x20,
                            0x10, 0x24, 0x06, 0x40, 0x09, 0x60, 0x07, 0x20,
                            0x08, 0x00, 0x05, 0x40, 0x07, 0x20, 0x05, 0x40,
                            0x06, 0x40, 0x04, 0x80
                        }) &&
                                ArraysEqual((byte[])key.GetValue("DALRestrictedModesBCD1"), new byte[] {
                            0x25, 0x60, 0x16, 0x00, 0x19, 0x20, 0x12, 0x00,
                            0x17, 0x60, 0x10, 0x80, 0x16, 0x00, 0x10, 0x00,
                            0x14, 0x40, 0x09, 0x00, 0x12, 0x80, 0x08, 0x00,
                            0x11, 0x20, 0x07, 0x20, 0x10, 0x24, 0x06, 0x40
                                }) &&
                                ArraysEqual((byte[])key.GetValue("DALRestrictedModesBCD2"), new byte[] {
                            0x19, 0x20, 0x12, 0x00, 0x17, 0x60, 0x10, 0x80,
                            0x16, 0x00, 0x10, 0x00, 0x14, 0x40, 0x09, 0x00,
                            0x12, 0x80, 0x08, 0x00, 0x11, 0x20, 0x07, 0x20,
                            0x10, 0x24, 0x06, 0x40
                                }) &&
                                ArraysEqual((byte[])key.GetValue("DALRestrictedModesBCD3"), new byte[] {
                            0x19, 0x20, 0x12, 0x00, 0x17, 0x60, 0x10, 0x80,
                            0x16, 0x00, 0x10, 0x00, 0x14, 0x40, 0x09, 0x00,
                            0x12, 0x80, 0x08, 0x00, 0x11, 0x20, 0x07, 0x20
                                }) &&
                                ArraysEqual((byte[])key.GetValue("DALRestrictedModesBCD4"), new byte[] {
                            0x19, 0x20, 0x12, 0x00, 0x17, 0x60, 0x10, 0x80,
                            0x16, 0x00, 0x10, 0x00, 0x14, 0x40, 0x09, 0x00
                                }))
                                {
                                    // If the registry keys match the expected values for 16:10, show pictureBox1610
                                    pictureBox1610.Visible = true;
                                    pictureBox1.Visible = false;
                                    pictureBox2.Visible = false;
                                    pictureBoxStockRes1610.Visible = false;
                                    found16_10 = true;
                                    break;
                                }
                            }
                        }
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        // Log or handle the exception as needed Debug
                        // Console.WriteLine($"Access denied to registry key: {subKeyName}. Error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        // Handle other potential exceptions here Debug
                        // Console.WriteLine($"Error accessing registry key: {subKeyName}. Error: {ex.Message}");
                    }
                }

                // If no matching 16:9 or 16:10 resolutions found, show pictureBox2
                if (!found16_9 && !found16_10)
                {
                    pictureBox2.Visible = true;
                    pictureBoxStockRes1610.Visible = true;
                    pictureBox1.Visible = false;
                    pictureBox1610.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions for the whole method, such as inability to open the main registry key Debug
                //Console.WriteLine($"Error accessing registry: {ex.Message}");
            }
            #endregion
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Panel2_Click(object? sender, EventArgs e)
        {
            // Check if the button is visible
            if (buttonRestorePauseUpgrade.Visible)
            {
                // If it is visible, it makes it invisible
                UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, controlAMDnoShutter, controlMeltdownSpectreProtectionOnOff,
                    labelAMDnoShutter, labelMeltdown_Spectre, label2, pictureBoxAMDnoShutter, pictureBoxMeltdown_Spectre, panel4Toggle,
                    controlSystemDevices, labelSystemDevices, pictureBoxSystemDevices, controlCoreIsolation_Exploid,
                    labelCoreIsolation_CFG, pictureBoxCoreIsolation_CFG, controlBCDMemoryUsage, labelBCDMemoryUsage,
                    pictureBCDMemoryUsage, controlReduceWindowsLatency, pictureBoxReduceWindowsLatency, labelReduceWindowsLatency);
                UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi);

                // Restore static icon
                timerGifReverse.StartTimerReverseForPictureBoxSettings(pictureBoxSettings);
            }
        }

        // Handle the MouseDown event to begin dragging
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        // Handle the MouseMove event to drag the window
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursor = Cursor.Position;
                int deltaX = currentCursor.X - lastCursor.X;
                int deltaY = currentCursor.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }
        }

        // Handle the MouseUp event to stop dragging
        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursor = Cursor.Position;
            lastForm = this.Location;
        }

        // Handle the MouseMove event to drag the window
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentCursor = Cursor.Position;
                int deltaX = currentCursor.X - lastCursor.X;
                int deltaY = currentCursor.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }
        }

        // Handle the MouseUp event to stop dragging
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }


        //----------------Start - Touch Buttons
        private async void desktopButton1_Click(object sender, EventArgs e)
        {
            // Call the method to handle desktop button click
            DesktopButton.CodeFordesktopButton1(DependencyContainer.MessagesBoxImplementation, desktopButton1, explorerPath);

        }


        private async void consoleButton1_Click(object sender, EventArgs e)
        {           
            // Call the method to handle console button click
            ConsoleButton.CodeForconsoleButton1(consoleButton1, fullscreenAppPath, defaultFullscreenSteamAppPath, DependencyContainer.MessagesBoxImplementation);

        }
        //----------------End - Touch-Buttons



        //############Start - Futures Buttons

        #region All Codes about Futures Buttons

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }
        private void buttonAllyUltimateRes_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxRes.Visible = true;
            pictureBox1.BackColor = Color.MediumSlateBlue;
            pictureBoxRes.BringToFront();
        }

        private void buttonAllyUltimateRes_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxRes.Visible = false;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void button1610UltimateRes_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1610Res.Visible = true;
            pictureBox1610.BackColor = Color.MediumSlateBlue;
            pictureBox1610Res.BringToFront();
        }

        private void button1610UltimateRes_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1610Res.Visible = false;
            pictureBox1610.BackColor = Color.Transparent;
        }

        private void buttonAllyUltimateRes_Click(object sender, EventArgs e)
        {
            // Calling the CodeForallyUltimateButton method from the ButtonallyUltimate.cs file
            ButtonAllyUltimate.CodeForallyUltimateButton(this, DependencyContainer.MessagesBoxImplementation);

            // Create the message box content
            string message = "Please note that the Windows Display Resolution Settings may have limitations in displaying all the resolutions added with this button. To see the complete list of resolutions, including any specific resolution you might need, follow these steps:\n\n" +
                     "1. Go to Settings -> System -> Display -> Advanced display settings.\n" +
                     "2. Click on 'Display adapter properties for Display 1'.\n" +
                     "3. Press the 'List All Modes' button.\n\n" +
                     "Would you like to visit the guide for more information?";

            // Show the message box using the MessagesBoxImplementation class
            DialogResult result = DependencyContainer.MessagesBoxImplementation.ShowMessage(message, "Display Resolution Information", MessageBoxButtons.YesNo);

            // Check if the user clicked Yes
            if (result == DialogResult.Yes)
            {
                // Open the URL in the default browser
                string url = "https://github.com/Special-Niewbie/Asus-Rog-Ally-Ultimate-Resolutions";
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }

        }

        private void button1610UltimateRes_Click(object sender, EventArgs e)
        {
            // Calling the CodeForallyUltimateButton method from the ButtonallyUltimate.cs file
            Button1610Ultimate.CodeFor1610UltimateButton(this, DependencyContainer.MessagesBoxImplementation);

            // Show info message
            string message = "Please note that the Windows Display Resolution Settings may have limitations in displaying all the resolutions added with this button. To see the complete list of resolutions, including any specific resolution you might need, follow these steps:\n\n" +
                     "1. Go to Settings -> System -> Display -> Advanced display settings.\n" +
                     "2. Click on 'Display adapter properties for Display 1'.\n" +
                     "3. Press the 'List All Modes' button.\n\n" +
                     "Would you like to visit the guide for more information?";

            DialogResult result = DependencyContainer.MessagesBoxImplementation.ShowMessage(message, "Display Resolution Information", MessageBoxButtons.YesNo);

            // Check if the user clicked Yes
            if (result == DialogResult.Yes)
            {
                // Open the URL in the default browser
                string url = "https://github.com/Special-Niewbie/Asus-Rog-Ally-Ultimate-Resolutions";
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
        }

        private void buttonAllyStockRes_Click(object sender, EventArgs e)
        {
            // Calling the CodeForallyStockButton method from the ButtonAllyStock.cs file
            ButtonAllyStock.CodeForallyStockButton(this, DependencyContainer.MessagesBoxImplementation);
        }

        private void buttonAllyStockRes_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxStockRes.Visible = true;
            pictureBoxStockRes.BringToFront();
            pictureBox2.BackColor = Color.MediumSlateBlue;
        }

        private void buttonAllyStockRes_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxStockRes.Visible = false;
            pictureBox2.BackColor = Color.Transparent;
        }


        private void button1610_StockRes_Click(object sender, EventArgs e)
        {
            // Calling the CodeFor1610StockButton method from the ButtonAllyStock.cs file
            Button1610Stock.CodeFor1610StockButton(this, DependencyContainer.MessagesBoxImplementation);
        }

        private void button1610_StockRes_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1610StockRes.Visible = true;
            pictureBox1610StockRes.BringToFront();
            pictureBoxStockRes1610.BackColor = Color.MediumSlateBlue;
        }

        private void button1610_StockRes_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1610StockRes.Visible = false;
            pictureBoxStockRes1610.BackColor = Color.Transparent;
        }
        private void buttonIntegerScaling_Click(object sender, EventArgs e)
        {
            AddButtonIntegerScaling.CodeForaddButtonIntegerScaling(pictureBox3, DependencyContainer.MessagesBoxImplementation);
        }

        private void buttonIntegerScaling_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxIntegerScaling.Visible = true;
            pictureBoxIS_Addon.Visible = true;
            pictureBox3.BackColor = Color.MediumSlateBlue;
            pictureBoxIntegerScaling.BringToFront();
            pictureBoxIS_Addon.BringToFront();
        }

        private void buttonIntegerScaling_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxIntegerScaling.Visible = false;
            pictureBoxIS_Addon.Visible = false;
            pictureBox3.BackColor = Color.Transparent;
        }

        private void pictureBoxIntegerScaling_Click(object sender, EventArgs e)
        {

        }

        private void button_IncreaseRAM_System_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxCheckVRAM.BackColor = Color.MediumSlateBlue;
        }

        private void button_IncreaseRAM_System_Click(object sender, EventArgs e)
        {
            Code_for_IncreaseRAM_System increaseRAMSystem = new Code_for_IncreaseRAM_System();
            increaseRAMSystem.IncreaseRAM(DependencyContainer.MessagesBoxImplementation);
        }
        private void button_IncreaseRAM_System_MouseHover(object sender, EventArgs e)
        {
            pictureBoxCheckVRAM.BackColor = Color.MediumSlateBlue;
        }
        private void button_IncreaseRAM_System_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxCheckVRAM.BackColor = Color.Transparent;
        }

        private void buttonMiniConsoleWindow_Click(object sender, EventArgs e)
        {
            ButtonMinimizeConsoleWindow.CodeForbuttonMiniConsoleWindow(this, DependencyContainer.MessagesBoxImplementation);
        }

        #endregion


        //############End - Futures Buttons

        private void labelSeparatoCustomRes_Click(object sender, EventArgs e)
        {

        }

        private void labelSeparatoStockRes_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxIS_Addon_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxRes_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1610_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1610Res_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1610StockRes_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void PictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent; // Restores the default background color when the mouse leaves the pictureBox1
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_MouseHover(object sender, EventArgs e)
        {

        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent; // Restores the default background color when the mouse leaves the pictureBox2
        }

        private void pictureBoxStockRes1610_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxStockRes1610_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxStockRes1610.BackColor = Color.Transparent; // Restores the default background color when the mouse leaves the pictureBoxStockRes1610
        }

        public void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        public void PictureBox3_MouseHover(object sender, EventArgs e)
        {

        }

        public void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent; // Ripristina il colore di sfondo predefinito quando il mouse esce dal pictureBox3
        }

        private void pictureBoxCheckVRAM_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxCheckVRAM_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBoxCheckVRAM_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pictureBoxStockRes_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxWifi_Click(object sender, EventArgs e)
        {
            // Stop the main timer
            _topMostTimer.Stop();

            // Set delay flag and start delay timer
            _isWifiDelayActive = true;
            _wifiDelayTimer.Start();

            pictureBoxWifiFunction.CodeForpictureBoxWifi(this, pictureBoxWifi, DependencyContainer.MessagesBoxImplementation);
        }

        private void pictureBoxAMDadrenaline_Click(object sender, EventArgs e)
        {

        }

        // Method to handle mouse click when pressed
        private void pictureBoxAMDadrenaline_MouseDown(object sender, MouseEventArgs e)
        {
            // Changes the image when the mouse is pressed
            pictureBoxAMDadrenaline.Visible = false;
            pictureBoxAMDadrenalinePress.Visible = true;

            try
            {
                string appFolder = @"C:\Program Files\WindowsApps";
                string[] subDirectories = Directory.GetDirectories(appFolder);

                foreach (string subDir in subDirectories)
                {
                    string[] subDirParts = subDir.Split('\\');
                    string appName = subDirParts.LastOrDefault();

                    // Check if the directory name starts with "Advanced Micro Devices Inc"
                    if (appName.StartsWith("AdvancedMicroDevicesInc"))
                    {
                        string exePath = Path.Combine(subDir, "radeonsoftware", "RadeonSoftware.exe");
                        if (File.Exists(exePath))
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.FileName = exePath;
                            startInfo.Verb = "runas"; // Requires elevation of privilege
                            Process.Start(startInfo);
                            return;
                        }
                    }
                }

                DependencyContainer.MessagesBoxImplementation.ShowMessage("The Radeon Software program could not be found.", "Error", MessageBoxButtons.OK);
                pictureBoxAMDadrenaline.Visible = true;
                pictureBoxAMDadrenalinePress.Visible = false;
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage("Error opening Radeon Software: " + ex.Message, "Error", MessageBoxButtons.OK);
                pictureBoxAMDadrenaline.Visible = true;
                pictureBoxAMDadrenalinePress.Visible = false;
            }
        }

        // Method to handle mouse click release
        private void pictureBoxAMDadrenaline_MouseUp(object sender, MouseEventArgs e)
        {
            // Restores the default image when the mouse is released
            pictureBoxAMDadrenaline.Visible = true;
            pictureBoxAMDadrenalinePress.Visible = false;
        }
        private void pictureBoxAMDadrenaline_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBoxAMDadrenaline_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxAMDadrenaline.BackColor = Color.Transparent;
        }

        private void pictureBoxAMDadrenalinePress_Click(object sender, EventArgs e)
        {

        }
        private void pictureBoxAMDadrenalinePress_MouseUp(object sender, EventArgs e)
        {
            pictureBoxAMDadrenaline.Visible = true;
            pictureBoxAMDadrenalinePress.Visible = false;
        }

        private void pictureBoxDonation1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBoxDonation1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxDonation1.Visible = false;
            pictureBoxDonation2.Visible = true;
            if (e.Button == MouseButtons.Left) // Ensure the click is with the left mouse button
            {
                try
                {
                    // Create a process start info
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd"; // Command Prompt
                    startInfo.Arguments = $"/c start https://www.paypal.com/paypalme/CrisDonate"; // Command to open the URL
                    startInfo.CreateNoWindow = true; // Do not create a window for the process

                    // Start the process
                    Process.Start(startInfo);
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    DependencyContainer.MessagesBoxImplementation.ShowMessage("Unable to open the default browser. Make sure it's properly configured on your system.", "Error", MessageBoxButtons.OK);
                    pictureBoxDonation1.Visible = true;
                    pictureBoxDonation2.Visible = false;
                }
                catch (Exception ex)
                {
                    DependencyContainer.MessagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
                    pictureBoxDonation1.Visible = true;
                    pictureBoxDonation2.Visible = false;
                }
            }
        }
        private void pictureBoxDonation1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxDonation1.Visible = true;
            pictureBoxDonation2.Visible = false;
        }

        private void pictureBoxDonation2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxDonation2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxDonation1.Visible = true;
            pictureBoxDonation2.Visible = false;
        }


        //#########Start - Settings

        #region All Code about Settings
        private void pictureBoxSettings_Click(object sender, EventArgs e)
        {
            TouchScreenStartupCheck.CheckTouchScreenStatus(touchScreenEnDbButton, DependencyContainer.MessagesBoxImplementation);
            // Check your MS Store installation and update the button text accordingly
            msStoreButtonStartupCheck.CheckMsStoreInstallation(msStoreButton, DependencyContainer.MessagesBoxImplementation);

            CodeForAMD_NoShutterCheck.CheckEnableUlps(controlAMDnoShutter, DependencyContainer.MessagesBoxImplementation);
            CodeForMeltdownSpectreProtectionCheck.CheckMeltdownSpectreProtection(controlMeltdownSpectreProtectionOnOff, DependencyContainer.MessagesBoxImplementation);
            CodeForControlCoreIsolation_ExploidCheck.CheckCoreIsolationAndExploitProtection(controlCoreIsolation_Exploid, DependencyContainer.MessagesBoxImplementation);
            CodeForSystemDevicesCheck.CheckSystemDevices(controlSystemDevices, DependencyContainer.MessagesBoxImplementation);
            CodeForControlBCDMemoryUsageCheck.CheckBCDMemoryUsage(controlBCDMemoryUsage, DependencyContainer.MessagesBoxImplementation);
            CodeForReduceWindowsLatencyCheck.CheckReduceWindowsLatency(controlReduceWindowsLatency, DependencyContainer.MessagesBoxImplementation);

            // Check if the button is visible
            if (buttonRestorePauseUpgrade.Visible)
            {
                // Run touchscreen status check when program starts
                TouchScreenStartupCheck.CheckTouchScreenStatus(touchScreenEnDbButton, DependencyContainer.MessagesBoxImplementation);

                // Check your MS Store installation and update the button text accordingly
                msStoreButtonStartupCheck.CheckMsStoreInstallation(msStoreButton, DependencyContainer.MessagesBoxImplementation);

                CodeForAMD_NoShutterCheck.CheckEnableUlps(controlAMDnoShutter, DependencyContainer.MessagesBoxImplementation);

                // If it is visible, it makes it invisible
                // Hides the buttons
                UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, controlAMDnoShutter, controlMeltdownSpectreProtectionOnOff,
                    labelAMDnoShutter, labelMeltdown_Spectre, label2, pictureBoxAMDnoShutter, pictureBoxMeltdown_Spectre, panel4Toggle,
                    controlSystemDevices, labelSystemDevices, pictureBoxSystemDevices, controlCoreIsolation_Exploid,
                    labelCoreIsolation_CFG, pictureBoxCoreIsolation_CFG, controlBCDMemoryUsage, labelBCDMemoryUsage,
                    pictureBCDMemoryUsage, controlReduceWindowsLatency, pictureBoxReduceWindowsLatency, labelReduceWindowsLatency);
                // Enable the desktop button
                UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi);

                // Restore static icon
                timerGifReverse.StartTimerReverseForPictureBoxSettings(pictureBoxSettings);
            }
            else
            {

                // If it is invisible, it makes it visible
                buttonRestorePauseUpgrade.Visible = true;
                buttonOpenFileExplorer.Visible = true;
                touchScreenEnDbButton.Visible = true;
                buttonXinputTest.Visible = true;
                buttonChangeConsoleSettings.Visible = true;
                msStoreButton.Visible = true;
                controlAMDnoShutter.Visible = true;
                controlMeltdownSpectreProtectionOnOff.Visible = true;
                labelAMDnoShutter.Visible = true;
                labelMeltdown_Spectre.Visible = true;
                label2.Visible = true;
                pictureBoxAMDnoShutter.Visible = true;
                pictureBoxMeltdown_Spectre.Visible = true;
                panel4Toggle.Visible = true;
                controlSystemDevices.Visible = true;
                labelSystemDevices.Visible = true;
                pictureBoxSystemDevices.Visible = true;
                controlCoreIsolation_Exploid.Visible = true;
                labelCoreIsolation_CFG.Visible = true;
                pictureBoxCoreIsolation_CFG.Visible = true;
                controlBCDMemoryUsage.Visible = true;
                labelBCDMemoryUsage.Visible = true;
                pictureBCDMemoryUsage.Visible = true;
                controlReduceWindowsLatency.Visible = true;
                pictureBoxReduceWindowsLatency.Visible = true;
                labelReduceWindowsLatency.Visible = true;


                desktopButton1.Enabled = false;
                desktopButton1.BackColor = Color.FromArgb(40, 40, 40);
                desktopButton1.BorderColor = Color.Gray;

                consoleButton1.Enabled = false;
                consoleButton1.BackColor = Color.FromArgb(40, 40, 40);
                consoleButton1.BorderColor = Color.Gray;

                pictureBoxAMDadrenaline.Enabled = false;
                pictureBoxAMDadrenalinePress.Enabled = false;
                pictureBoxRealTime_ON.Enabled = false;
                pictureBox4.Enabled = false;
                pictureBoxRealTime_OFF.Enabled = false;
                pictureBoxWifi.Enabled = false;
                timerGif.StartTimerForPictureBoxSettings(pictureBoxSettings);
            }

        }
        private void buttonRestorePauseUpgrade_Click(object sender, EventArgs e)
        {
            RestorePauseUpgrade.Execute(buttonRestorePauseUpgrade, DependencyContainer.MessagesBoxImplementation);
        }
        private void buttonRestorePauseUpgrade_MouseUp(object sender, EventArgs e)
        {
            // Hides the buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, controlAMDnoShutter, controlMeltdownSpectreProtectionOnOff,
                    labelAMDnoShutter, labelMeltdown_Spectre, label2, pictureBoxAMDnoShutter, pictureBoxMeltdown_Spectre, panel4Toggle,
                    controlSystemDevices, labelSystemDevices, pictureBoxSystemDevices, controlCoreIsolation_Exploid,
                    labelCoreIsolation_CFG, pictureBoxCoreIsolation_CFG, controlBCDMemoryUsage, labelBCDMemoryUsage,
                    pictureBCDMemoryUsage, controlReduceWindowsLatency, pictureBoxReduceWindowsLatency, labelReduceWindowsLatency);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi);
        }

        private void buttonOpenFileExplorer_Click(object sender, EventArgs e)
        {
            try
            {
                // Define the path to the "This Computer" folder
                string folderPath = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

                // Launch File Explorer with the specified path
                Process.Start("explorer.exe", folderPath);
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private void buttonXinputTest_Click(object sender, EventArgs e)
        {
            // Define the path of the program to launch
            string exePath = @"C:\Program Files\Console2Desk\XinputTest\XInput.exe";

            try
            {
                // Start the process
                Process process = new Process();
                process.StartInfo.FileName = exePath;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = false;
                process.Start();

                // Hide the main window
                this.Hide();

                // Wait for the process to finish
                process.WaitForExit();

                // Show the main window again
                this.Show();
                this.BringToFront(); //Brings the main window to the front
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error starting the program: {ex.Message}", "Error", MessageBoxButtons.OK);
            }

        }

        private void buttonOpenFileExplorer_MouseUp(object sender, EventArgs e)
        {
            // Hidesbuttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, controlAMDnoShutter, controlMeltdownSpectreProtectionOnOff,
                    labelAMDnoShutter, labelMeltdown_Spectre, label2, pictureBoxAMDnoShutter, pictureBoxMeltdown_Spectre, panel4Toggle,
                    controlSystemDevices, labelSystemDevices, pictureBoxSystemDevices, controlCoreIsolation_Exploid,
                    labelCoreIsolation_CFG, pictureBoxCoreIsolation_CFG, controlBCDMemoryUsage, labelBCDMemoryUsage,
                    pictureBCDMemoryUsage, controlReduceWindowsLatency, pictureBoxReduceWindowsLatency, labelReduceWindowsLatency);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi);
        }
        public void touchScreenEnDbButton_Click(object sender, EventArgs e)
        {
            TouchScreenEnDbButtonCoding touchScreenCoding = new TouchScreenEnDbButtonCoding();
            touchScreenCoding.code4touchScreenEnDbButton(touchScreenEnDbButton, DependencyContainer.MessagesBoxImplementation);
        }
        public void touchScreenEnDbButton_MouseUp(object sender, EventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, controlAMDnoShutter, controlMeltdownSpectreProtectionOnOff,
                    labelAMDnoShutter, labelMeltdown_Spectre, label2, pictureBoxAMDnoShutter, pictureBoxMeltdown_Spectre, panel4Toggle,
                    controlSystemDevices, labelSystemDevices, pictureBoxSystemDevices, controlCoreIsolation_Exploid,
                    labelCoreIsolation_CFG, pictureBoxCoreIsolation_CFG, controlBCDMemoryUsage, labelBCDMemoryUsage,
                    pictureBCDMemoryUsage, controlReduceWindowsLatency, pictureBoxReduceWindowsLatency, labelReduceWindowsLatency);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi);
        }

        public void buttonXinputTest_MouseUp(object sender, MouseEventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, controlAMDnoShutter, controlMeltdownSpectreProtectionOnOff,
                    labelAMDnoShutter, labelMeltdown_Spectre, label2, pictureBoxAMDnoShutter, pictureBoxMeltdown_Spectre, panel4Toggle,
                    controlSystemDevices, labelSystemDevices, pictureBoxSystemDevices, controlCoreIsolation_Exploid,
                    labelCoreIsolation_CFG, pictureBoxCoreIsolation_CFG, controlBCDMemoryUsage, labelBCDMemoryUsage,
                    pictureBCDMemoryUsage, controlReduceWindowsLatency, pictureBoxReduceWindowsLatency, labelReduceWindowsLatency);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi);
        }

        private void buttonChangeConsoleSettings_Click(object sender, EventArgs e)
        {
            // Open the ConsoleSettingsButton form to configure settings
            using (ConsoleSettingsButton settingsForm = new ConsoleSettingsButton())
            {
                settingsForm.ShowDialog();
            }
        }

        private void buttonChangeConsoleSettings_MouseUp(object sender, MouseEventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, controlAMDnoShutter, controlMeltdownSpectreProtectionOnOff,
                    labelAMDnoShutter, labelMeltdown_Spectre, label2, pictureBoxAMDnoShutter, pictureBoxMeltdown_Spectre, panel4Toggle,
                    controlSystemDevices, labelSystemDevices, pictureBoxSystemDevices, controlCoreIsolation_Exploid,
                    labelCoreIsolation_CFG, pictureBoxCoreIsolation_CFG, controlBCDMemoryUsage, labelBCDMemoryUsage,
                    pictureBCDMemoryUsage, controlReduceWindowsLatency, pictureBoxReduceWindowsLatency, labelReduceWindowsLatency);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi);
        }

        private void msStoreButton_Click(object sender, EventArgs e)
        {
            msStoreButtonCoding.ExecuteMsStoreCommand(msStoreButton, DependencyContainer.MessagesBoxImplementation);
        }

        private void msStoreButton_MouseUp(object sender, MouseEventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, controlAMDnoShutter, controlMeltdownSpectreProtectionOnOff,
                    labelAMDnoShutter, labelMeltdown_Spectre, label2, pictureBoxAMDnoShutter, pictureBoxMeltdown_Spectre, panel4Toggle,
                    controlSystemDevices, labelSystemDevices, pictureBoxSystemDevices, controlCoreIsolation_Exploid,
                    labelCoreIsolation_CFG, pictureBoxCoreIsolation_CFG, controlBCDMemoryUsage, labelBCDMemoryUsage,
                    pictureBCDMemoryUsage, controlReduceWindowsLatency, pictureBoxReduceWindowsLatency, labelReduceWindowsLatency);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi);
        }

        // Toggles Panel Settings
        private void panel4Toggle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBoxAMDnoShutter_Click(object sender, EventArgs e)
        {

        }

        private void labelAMDnoShutter_Click(object sender, EventArgs e)
        {

        }

        private void controlAMDnoShutter_Click(object sender, EventArgs e)
        {
            CodeForAMD_NoShutter.ToggleEnableUlps(controlAMDnoShutter, DependencyContainer.MessagesBoxImplementation);
        }

        private void pictureBoxMeltdown_Spectre_Click(object sender, EventArgs e)
        {

        }

        private void labelMeltdown_Spectre_Click(object sender, EventArgs e)
        {

        }

        private void controlMeltdownSpectreProtectionOnOff_Click(object sender, EventArgs e)
        {
            CodeForMeltdownSpectreProtection.ToggleMeltdownSpectreProtection(controlMeltdownSpectreProtectionOnOff, DependencyContainer.MessagesBoxImplementation);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void controlSystemDevices_Click(object sender, EventArgs e)
        {
            CodeForSystemDevices.ToggleSystemDevicesAsync(controlSystemDevices, DependencyContainer.MessagesBoxImplementation);
        }

        private void labelSystemDevices_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxSystemDevices_Click(object sender, EventArgs e)
        {

        }

        private void controlCoreIsolation_Exploid_Click(object sender, EventArgs e)
        {
            CodeForControlCoreIsolation_Exploid.ToggleCoreIsolationAndExploitProtection(controlCoreIsolation_Exploid, DependencyContainer.MessagesBoxImplementation);
        }

        private void labelCoreIsolation_CFG_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxCoreIsolation_CFG_Click(object sender, EventArgs e)
        {

        }

        private void controlBCDMemoryUsage_Click(object sender, EventArgs e)
        {
            CodeForControlBCDMemoryUsage.ToggleBCDMemoryUsage(controlBCDMemoryUsage, DependencyContainer.MessagesBoxImplementation);
        }

        private void labelBCDMemoryUsage_Click(object sender, EventArgs e)
        {

        }

        private void pictureBCDMemoryUsage_Click(object sender, EventArgs e)
        {

        }

        private void controlReduceWindowsLatency_Click(object sender, EventArgs e)
        {
            CodeForReduceWindowsLatency.ToggleReduceWindowsLatency(controlReduceWindowsLatency, DependencyContainer.MessagesBoxImplementation);
        }

        private void pictureBoxReduceWindowsLatency_Click(object sender, EventArgs e)
        {

        }

        private void labelReduceWindowsLatency_Click(object sender, EventArgs e)
        {

        }

        // Toggles Panel Settings
        #endregion

        //#########End - Settings

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            _isAboutBoxOpen = true;
            AboutBox aboutBox = new AboutBox(this, _topMostTimer);
            aboutBox.FormClosed += (s, args) => _isAboutBoxOpen = false;
            aboutBox.Show();

        }

        public void PauseTimer()
        {
            if (_topMostTimer != null)
            {
                _topMostTimer.Stop();
                Console.WriteLine("Timer paused");
            }
        }

        public void ResumeTimer()
        {
            if (_topMostTimer != null)
            {
                _topMostTimer.Start();
                Console.WriteLine("Timer resumed");
            }
        }

        private void buttonRealTimeProtection_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            WindowsDefender.OpenWindowsDefender(DependencyContainer.MessagesBoxImplementation);
        }

        private void pictureBoxRealTime_ON_Click(object sender, EventArgs e)
        {
            DependencyContainer.MessagesBoxImplementation.ShowMessage("This Windows Defender Real-Time Protection Auto-Enable button only works if Temper-Protection is manually disabled. " +
                "Please make sure Temper-Protection is disabled before using these auto buttons." +
                "\nOtherwise, enabling Real-Time Protection will not succeed.", "Attention", MessageBoxButtons.OK);

            Cursor.Current = Cursors.WaitCursor;

            WindowsDefender.EnableRealTimeProtection(DependencyContainer.MessagesBoxImplementation);
            DependencyContainer.MessagesBoxImplementation.ShowMessage("Windows Defender Real-Time Protection enabled successfully.", "Information", MessageBoxButtons.OK);

            Cursor.Current = Cursors.Default;
        }

        private void pictureBoxRealTime_OFF_Click(object sender, EventArgs e)
        {
            DependencyContainer.MessagesBoxImplementation.ShowMessage("This Windows Defender Real-Time Protection Auto-Disable button only works if Temper-Protection is manually disabled. " +
                "Please make sure Temper-Protection is disabled before using these auto buttons." +
                "\nOtherwise, disabling Real-Time Protection will not succeed.", "Attention", MessageBoxButtons.OK);

            Cursor.Current = Cursors.WaitCursor;

            WindowsDefender.DisableRealTimeProtection(DependencyContainer.MessagesBoxImplementation);
            DependencyContainer.MessagesBoxImplementation.ShowMessage("Windows Defender Real-Time Protection disabled successfully.", "Information", MessageBoxButtons.OK);

            Cursor.Current = Cursors.Default;
        }
    }
}
