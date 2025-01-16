/*
Console2Desk

Copyright (C) 2023 Special-Niewbie Softwares

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Console2Desk.TouchButtons;
using Console2Desk.FuturesButtons;
using Console2Desk.BottomWindowButtons;
using Console2Desk.SettingsButton;
using Console2Desk.ToggleSwitchDev;
using XInputium.XInput;
using XInputium;
using System.IO.Pipes;
using System.Text;
using System;
using Console2Desk.FormWifi;
using Console2Desk.PowerM;
using Console2Desk.HiberSleep;
using System.Reflection;


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

        // Importazione della funzione per simulare la pressione/rilascio di tasti
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int extraInfo);

        // Codici virtuali per ALT
        private const int VK_MENU = 0x12; // Codice virtuale per ALT
        private const int VK_R = 0x52;    // Codice virtuale per R
        private const int KEYEVENTF_KEYUP = 0x02; // Segnale per rilascio del tasto

        //###################################
        // For trak the Window and suspend the TopMost timer when is open by the buttonOpenFileExplorer
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private Process _explorerProcess;
        private bool _isExplorerOpen;
        private IntPtr _explorerWindowHandle;

        //###################################

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW;

        private System.Windows.Forms.Timer _topMostTimer;
        private bool _isAboutBoxOpen = false;
        private bool _isConsoleSettingsOpen = false;

        private System.Windows.Forms.Timer _wifiDelayTimer;
        private bool _isWifiDelayActive = false;
        private const int WifiDelay = 120000; // 50 seconds in milliseconds

        private bool _isWindowMinimized = false;

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

        private System.Windows.Forms.Timer TimerLabelCountDown;
        private int countDown = 6;

        private System.Windows.Forms.Timer updateTimer;
        private readonly object lockObject = new object();

        private WinTheme winTheme;

        private readonly WifiOrchestrator _wifiOrchestrator;

        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Load += Form1_Load;
            this.Resize += Form1_Resize;

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
            pictureBoxVramPlus.MouseHover += pictureBoxVramPlus_MouseHover;
            pictureBoxVramPlus.MouseLeave += pictureBoxVramPlus_MouseLeave;


            pictureBoxResetTouchKeyboard.MouseUp += pictureBoxResetTouchKeyboard_MouseUp;
            pictureBoxResetTouchKeyboard.MouseDown += pictureBoxResetTouchKeyboard_MouseDown;
            pictureBoxResetTouchKeyboardPress.MouseUp += pictureBoxResetTouchKeyboardPress_MouseUp;
            pictureBoxResetTouchKeyboardPress.MouseDown += pictureBoxResetTouchKeyboardPress_MouseDown;


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

            // Configura il TimerLabelCountDown
            TimerLabelCountDown = new System.Windows.Forms.Timer();
            TimerLabelCountDown.Interval = 1000; // 1 secondo
            TimerLabelCountDown.Tick += TimerLabelCountDown_Tick; // Associa l'evento Tick

            // Define the path of the program to launch
            string exePath = @"C:\Program Files\Console2Desk\inputs2desk\inputs2desk.exe";

            try
            {
                // Start the process
                Process process = new Process();
                process.StartInfo.FileName = exePath;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = false;
                process.Start();

            }
            catch (Exception ex)
            {
                // Handle any exceptions
                // For Debug DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error starting the program: {ex.Message}", "Error", MessageBoxButtons.OK);
            }

            // Inizializza il timer per verificare periodicamente la pipe
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 1500; // 1.5 secondi
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();

            _wifiOrchestrator = new WifiOrchestrator(this, pictureBoxWifi, _topMostTimer, DependencyContainer.MessagesBoxImplementation);

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
                if (this.WindowState == FormWindowState.Minimized)
                {
                    return; // Non eseguire il codice se la finestra è minimizzata
                }
                if (!_isAboutBoxOpen && !_isConsoleSettingsOpen && !_isExplorerOpen && DependencyContainer.MessagesBoxImplementation != null &&
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
            _wifiDelayTimer.Interval = WifiDelay; // 50 seconds
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


            RAMChecker ramChecker = new RAMChecker(pictureBoxCheckVRAM, pictureBoxVramPlus);
            ramChecker.CheckVirtualMemorySettingsAsync(DependencyContainer.MessagesBoxImplementation);
            AddButtonIntegerScalingCheck.CodeForaddButtonIntegerScalingCheck(pictureBox3, DependencyContainer.MessagesBoxImplementation);
            CodeForResetTouchKeyboardCheck.CheckTouchKeyboardState(pictureBoxResetTouchKeyboard, DependencyContainer.MessagesBoxImplementation);
            msStoreButtonStartupCheck.CheckMsStoreInstallation(msStoreButton, DependencyContainer.MessagesBoxImplementation);
            RealTimeProtectionEnabled_Check.CheckAndSetRealTimeProtectionAsync(pictureBoxRealTime_ON, pictureBoxRealTime_OFF);

            CustomListBoxUpdate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Manage window state changes
            if (this.WindowState == FormWindowState.Minimized)
            {
                // Ferma il timer quando la finestra è minimizzata
                if (_topMostTimer != null && _topMostTimer.Enabled)
                {
                    _topMostTimer.Stop();
                }
                _isWindowMinimized = true;

                //Keep the code below to be more aggressive on TopMost if necessary
                //this.WindowState = FormWindowState.Normal;
                SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                SetForegroundWindow(this.Handle); // Ensures the window stays on top
            }
            else if (this.WindowState == FormWindowState.Normal && _isWindowMinimized)
            {
                // Riavvia il timer quando la finestra è ripristinata
                if (_topMostTimer != null && !_topMostTimer.Enabled)
                {
                    _topMostTimer.Start();
                }
                _isWindowMinimized = false;
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

        private async void CustomListBoxUpdate()
        {
            var checkForUpdates = new CheckForUpdatesJson();
            await checkForUpdates.CheckForUpdatesAsync(DependencyContainer.MessagesBoxImplementation);

            var newsItems = NewsLoader.LoadNews(@"C:\Program Files\Console2Desk\sources\local_news_file.json");
            if (newsItems.Count == 0)
            {
                MessageBox.Show("NO News found.");
            }

            customListBox1.NewsItems = newsItems;
            customListBox1.Items.Clear(); // Pulisce gli elementi esistenti
            customListBox1.Items.AddRange(newsItems.Select(n => n.Title).ToArray()); // Aggiungi i titoli per il binding
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
        #region Controllers Pictures
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Avvia la lettura del file in un task separato
            Task.Run(() => ReadStatusFromFile());
        }

        private void ReadStatusFromFile()
        {
            try
            {
                string filePath = Path.Combine(Path.GetTempPath(), "controller_status.txt");

                // Leggi lo stato dei controller dal file
                if (File.Exists(filePath))
                {
                    string data = File.ReadAllText(filePath);
                    int controllerCount;

                    if (int.TryParse(data, out controllerCount))
                    {
                        // Chiama il metodo per aggiornare il PictureBox dal thread principale
                        this.Invoke(new Action(() => UpdatePictureBox(controllerCount)));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore durante la lettura dello stato dal file: " + ex.Message);
            }
        }

        private void UpdatePictureBox(int controllerCount)
        {
            lock (lockObject)
            {
                switch (controllerCount)
                {
                    case 0:
                        pictureBoxControllers.Image = Properties.Resources.controller0;
                        break;
                    case 1:
                        pictureBoxControllers.Image = Properties.Resources.controller1;
                        break;
                    case 2:
                        pictureBoxControllers.Image = Properties.Resources.controller2;
                        break;
                    case 3:
                        pictureBoxControllers.Image = Properties.Resources.controller3;
                        break;
                    case 4:
                        pictureBoxControllers.Image = Properties.Resources.controller4;
                        break;
                    default:
                        pictureBoxControllers.Image = null;
                        break;
                }
            }
        }

        #endregion
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
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, special_Niewbie_ButtonHOB,
                    special_Niewbie_ButtonRestoreBoost, buttonTempDM, buttonHiberSleep);
                UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxResetTouchKeyboard, pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi, buttonMiniConsoleWindowNew);

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
            // Disabilitare Form1 per impedire l'interazione dell'utente
            this.Enabled = false;
            // Minimize the form at the start
            this.WindowState = FormWindowState.Minimized;
            ShareDisableTouchscreenClass.DisableTouchscreen(pictureBoxResetTouchKeyboard);

            Task.Delay(2500).Wait();

            // Call the method to handle desktop button click
            DesktopButton.CodeForTouchDesktopButton(DependencyContainer.MessagesBoxImplementation, desktopButton1, explorerPath);

            Task.Delay(1500).Wait();
            ShareDisableTouchscreenClass.DisableTouchscreen(pictureBoxResetTouchKeyboard);
            Task.Delay(1500).Wait();

            // Disabilitare Form1 per impedire l'interazione dell'utente
            this.Enabled = true;
            // Restore the form to its normal state
            this.WindowState = FormWindowState.Normal;
            //await Task.Delay(800);
            //this.Close();
        }


        private async void consoleButton1_Click(object sender, EventArgs e)
        {
            // Disabilitare Form1 per impedire l'interazione dell'utente
            this.Enabled = false;
            // Minimize the form at the start
            this.WindowState = FormWindowState.Minimized;
            ShareDisableTouchscreenClass.DisableTouchscreen(pictureBoxResetTouchKeyboard);

            Task.Delay(1500).Wait();

            // Call the method to handle console button click
            ConsoleTouchButton.CodeForTouchConsoleButton(DependencyContainer.MessagesBoxImplementation, consoleButton1, fullscreenAppPath, defaultFullscreenSteamAppPath, this, _topMostTimer);

            Task.Delay(1500).Wait();
            ShareDisableTouchscreenClass.DisableTouchscreen(pictureBoxResetTouchKeyboard);
            Task.Delay(500).Wait();

            // Disabilitare Form1 per impedire l'interazione dell'utente
            this.Enabled = true;
            // Restore the form to its normal state
            this.WindowState = FormWindowState.Normal;
            //await Task.Delay(800);
            //this.Close();
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
            pictureBoxVramPlus.BackColor = Color.MediumSlateBlue;
        }

        private void button_IncreaseRAM_System_Click(object sender, EventArgs e)
        {
            Code_for_IncreaseRAM_System increaseRAMSystem = new Code_for_IncreaseRAM_System();
            increaseRAMSystem.IncreaseRAM(DependencyContainer.MessagesBoxImplementation);
        }
        private void button_IncreaseRAM_System_MouseHover(object sender, EventArgs e)
        {
            pictureBoxCheckVRAM.BackColor = Color.MediumSlateBlue;
            pictureBoxVramPlus.BackColor = Color.MediumSlateBlue;
        }
        private void button_IncreaseRAM_System_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxCheckVRAM.BackColor = Color.Transparent;
            pictureBoxVramPlus.BackColor = Color.Transparent;
        }

        /*
        OLD IMPLEMENTATION BUTTON Keep temporary
        private void buttonMiniConsoleWindow_Click(object sender, EventArgs e)
        {
            ButtonMinimizeConsoleWindow.CodeForbuttonMiniConsoleWindow(this, DependencyContainer.MessagesBoxImplementation);
        }*/

        private void buttonMiniConsoleWindowNew_Click(object sender, EventArgs e)
        {
            ButtonMinimizeConsoleWindow.CodeForbuttonMiniConsoleWindow(this, DependencyContainer.MessagesBoxImplementation);
        }

        #endregion


        //############End - Futures Buttons
        #region All unused Events Method for Futures Buttons
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBoxStockRes1610_Click(object sender, EventArgs e)
        {

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

        private void pictureBoxVramPlus_Click(object sender, EventArgs e)
        {

        }
        private void pictureBoxVramPlus_MouseHover(object sender, EventArgs e)
        {

        }

        private void pictureBoxVramPlus_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pictureBoxStockRes_Click(object sender, EventArgs e)
        {

        }

        public void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        public void PictureBox3_MouseHover(object sender, EventArgs e)
        {

        }

        private void labelCountDown_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxAMDadrenaline_MouseHover(object sender, EventArgs e)
        {

        }
        #endregion


        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent; // Restores the default background color when the mouse leaves the pictureBox1
        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent; // Restores the default background color when the mouse leaves the pictureBox2
        }

        public void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent; // Ripristina il colore di sfondo predefinito quando il mouse esce dal pictureBox3
        }

        private void pictureBoxStockRes1610_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxStockRes1610.BackColor = Color.Transparent; // Restores the default background color when the mouse leaves the pictureBoxStockRes1610
        }

        private void pictureBoxWifi_Click(object sender, EventArgs e)
        {
            // Stop the main timer
            _topMostTimer.Stop();

            // Set delay flag and start delay timer
            _isWifiDelayActive = true;
            _wifiDelayTimer.Start();

            _wifiOrchestrator.HandleWifiButtonClick();
        }

        private void pictureBoxAMDadrenaline_Click(object sender, EventArgs e)
        {

        }


        private async void pictureBoxAMDadrenaline_MouseDown(object sender, MouseEventArgs e)
        {
            // Cambia l'immagine quando il mouse è premuto
            pictureBoxAMDadrenaline.Visible = false;
            pictureBoxAMDadrenalinePress.Visible = true;

            try
            {
                // Simula la pressione di ALT
                keybd_event(VK_MENU, 0, 0, 0); // Premi ALT

                // Pausa per simulare il tempo che l'utente tiene premuto ALT
                await Task.Delay(100);

                // Simula la pressione di R
                keybd_event(VK_R, 0, 0, 0); // Premi R

                // Pausa per assicurarsi che ALT+R venga processato
                await Task.Delay(100);

                // Rilascia R
                keybd_event(VK_R, 0, KEYEVENTF_KEYUP, 0); // Rilascia R

                // Rilascia ALT
                keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, 0); // Rilascia ALT
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage("Error simulating ALT+R: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
            finally
            {
                // Ripristina l'immagine predefinita
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

            // Check if the button is visible
            if (buttonRestorePauseUpgrade.Visible)
            {
                // Run touchscreen status check when program starts
                TouchScreenStartupCheck.CheckTouchScreenStatus(touchScreenEnDbButton, DependencyContainer.MessagesBoxImplementation);

                // Check your MS Store installation and update the button text accordingly
                msStoreButtonStartupCheck.CheckMsStoreInstallation(msStoreButton, DependencyContainer.MessagesBoxImplementation);

                // If it is visible, it makes it invisible
                // Hides the buttons
                UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, special_Niewbie_ButtonHOB,
                    special_Niewbie_ButtonRestoreBoost, buttonTempDM, buttonHiberSleep);
                // Enable the desktop button
                UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxResetTouchKeyboard, pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi, buttonMiniConsoleWindowNew);

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
                special_Niewbie_ButtonHOB.Visible = true;
                special_Niewbie_ButtonRestoreBoost.Visible = true;
                buttonTempDM.Visible = true;
                buttonHiberSleep.Visible = true;

                desktopButton1.Enabled = false;
                desktopButton1.BackColor = Color.FromArgb(40, 40, 40);
                desktopButton1.BorderColor = Color.Gray;

                consoleButton1.Enabled = false;
                consoleButton1.BackColor = Color.FromArgb(40, 40, 40);
                consoleButton1.BorderColor = Color.Gray;

                pictureBoxResetTouchKeyboard.Enabled = false;
                pictureBoxAMDadrenaline.Enabled = false;
                pictureBoxAMDadrenalinePress.Enabled = false;
                pictureBoxRealTime_ON.Enabled = false;
                pictureBox4.Enabled = false;
                pictureBoxRealTime_OFF.Enabled = false;
                pictureBoxWifi.Enabled = false;
                buttonMiniConsoleWindowNew.Enabled = false;
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
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, special_Niewbie_ButtonHOB,
                    special_Niewbie_ButtonRestoreBoost, buttonTempDM, buttonHiberSleep);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxResetTouchKeyboard, pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi, buttonMiniConsoleWindowNew);
        }
        /*private void MonitorExplorerProcess()  // old implementation later to be deleted
        {
            try
            {
                // Wait for the Explorer process to exit
                _explorerProcess.WaitForExit();

                // Once the process exits, reset the flag and restart the timer
                _isExplorerOpen = false;
                _topMostTimer.Start();
            }
            catch (Exception ex)
            {
                // Handle any exceptions related to the monitoring process
                DependencyContainer.MessagesBoxImplementation.ShowMessage($"An error occurred while monitoring the process: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }*/

        private IntPtr GetFileExplorerWindowHandle()
        {
            IntPtr explorerWindowHandle = IntPtr.Zero;
            EnumWindows((hWnd, lParam) =>
            {
                // Check if this is a File Explorer window
                if (IsExplorerWindow(hWnd))
                {
                    explorerWindowHandle = hWnd;
                    return false; // Stop enumerating
                }
                return true; // Continue enumerating
            }, IntPtr.Zero);
            return explorerWindowHandle;
        }

        private bool IsExplorerWindow(IntPtr hWnd)
        {
            // Add additional checks if needed
            string className = GetClassName(hWnd);
            return className == "CabinetWClass" || className == "ExploreWClass";
        }

        private string GetClassName(IntPtr hWnd)
        {
            const int nChars = 256;
            StringBuilder sb = new StringBuilder(nChars);
            if (GetClassName(hWnd, sb, nChars) != 0)
            {
                return sb.ToString();
            }
            return null;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        private async void buttonOpenFileExplorer_Click(object sender, EventArgs e)
        {
            try
            {
                // Define the path to the "This Computer" folder
                string folderPath = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

                // Launch File Explorer with the specified path
                _explorerProcess = Process.Start("explorer.exe", folderPath);
                _isExplorerOpen = true;
                _topMostTimer.Stop(); // Stop the timer when File Explorer is opened

                // Minimize Form1
                this.WindowState = FormWindowState.Minimized;

                // Wait for the File Explorer window to appear
                await Task.Delay(500); // Wait asynchronously

                // Get the handle of the File Explorer window
                _explorerWindowHandle = GetFileExplorerWindowHandle();

                // Monitor the Explorer window
                System.Windows.Forms.Timer windowMonitorTimer = new System.Windows.Forms.Timer();
                windowMonitorTimer.Interval = 1000; // Check every second
                windowMonitorTimer.Tick += (s, args) =>
                {
                    if (_explorerWindowHandle == IntPtr.Zero || !IsWindow(_explorerWindowHandle))
                    {
                        // Window is closed, restart the TopMost timer
                        _isExplorerOpen = false;
                        _topMostTimer.Start();
                        windowMonitorTimer.Stop(); // Stop monitoring

                        // Restore Form1 to normal state
                        this.WindowState = FormWindowState.Normal;
                        Task.Delay(250).Wait();
                        this.BringToFront(); // Optionally bring Form1 to the front
                    }
                };
                windowMonitorTimer.Start();
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
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, special_Niewbie_ButtonHOB,
                    special_Niewbie_ButtonRestoreBoost, buttonTempDM, buttonHiberSleep);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxResetTouchKeyboard, pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi, buttonMiniConsoleWindowNew);
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
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, special_Niewbie_ButtonHOB,
                    special_Niewbie_ButtonRestoreBoost, buttonTempDM, buttonHiberSleep);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxResetTouchKeyboard, pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi, buttonMiniConsoleWindowNew);
        }

        public void buttonXinputTest_MouseUp(object sender, MouseEventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, special_Niewbie_ButtonHOB,
                    special_Niewbie_ButtonRestoreBoost, buttonTempDM, buttonHiberSleep);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxResetTouchKeyboard, pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi, buttonMiniConsoleWindowNew);
        }

        private void buttonChangeConsoleSettings_Click(object sender, EventArgs e)
        {
            _isConsoleSettingsOpen = true;
            // Open the ConsoleSettingsButton form to configure settings
            using (ConsoleSettingsButton settingsForm = new ConsoleSettingsButton(this, _topMostTimer))
            {
                settingsForm.FormClosed += (s, args) => _isConsoleSettingsOpen = false;
                settingsForm.ShowDialog();
            }
        }

        private void buttonChangeConsoleSettings_MouseUp(object sender, MouseEventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, special_Niewbie_ButtonHOB,
                    special_Niewbie_ButtonRestoreBoost, buttonTempDM, buttonHiberSleep);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxResetTouchKeyboard, pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi, buttonMiniConsoleWindowNew);
        }

        private void msStoreButton_Click(object sender, EventArgs e)
        {
            msStoreButtonCoding.ExecuteMsStoreCommand(msStoreButton, DependencyContainer.MessagesBoxImplementation);
        }

        private void msStoreButton_MouseUp(object sender, MouseEventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton,
                    buttonXinputTest, buttonChangeConsoleSettings, msStoreButton, special_Niewbie_ButtonHOB,
                    special_Niewbie_ButtonRestoreBoost, buttonTempDM, buttonHiberSleep);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1, pictureBoxAMDadrenaline,
                pictureBoxResetTouchKeyboard, pictureBoxAMDadrenalinePress, pictureBoxRealTime_ON, pictureBox4, pictureBoxRealTime_OFF,
                pictureBoxWifi, buttonMiniConsoleWindowNew);
        }

        private void special_Niewbie_ButtonHOB_Click(object sender, EventArgs e)
        {
            // Define the path of the program to launch
            string exePath = @"C:\Program Files\Console2Desk\HOB\HandleOS_Benchmark.exe";

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

        private void buttonTempDM_Click(object sender, EventArgs e)
        {
            // Minimize the form at the start
            this.WindowState = FormWindowState.Minimized;

            // Disabilitare Form1 per impedire l'interazione dell'utente
            this.Enabled = false;

            ShareDisableTouchscreenClass.DisableTouchscreen(pictureBoxResetTouchKeyboard);

            Task.Delay(500).Wait();
            // Call the method to handle desktop button click
            TempDM.CodeForTouchTempDM(DependencyContainer.MessagesBoxImplementation, buttonTempDM, explorerPath);

            Task.Delay(1000).Wait();
            ShareDisableTouchscreenClass.DisableTouchscreen(pictureBoxResetTouchKeyboard);

            // Disabilitare Form1 per impedire l'interazione dell'utente
            this.Enabled = true;

            Task.Delay(500).Wait();
            // Restore the form to its normal state
            this.WindowState = FormWindowState.Normal;
        }

        #endregion

        //#########End - Settings
        #region All unused Events Method for Bottom Window Buttons
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void pictureBoxResetTouchKeyboard_Click(object sender, EventArgs e)
        {

        }
        private void pictureBoxResetTouchKeyboard_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void pictureBoxResetTouchKeyboardPress_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBoxResetTouchKeyboardPress_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxResetTouchKeyboardPress_MouseUp(object sender, EventArgs e)
        {

        }
        #endregion
        private void TimerLabelCountDown_Tick(object sender, EventArgs e)
        {
            countDown--; // Decrementa il conto alla rovescia
            labelCountDown.Text = countDown.ToString(); // Aggiorna il testo dell'etichetta

            if (countDown == 0)
            {
                TimerLabelCountDown.Stop(); // Ferma il timer
                labelCountDown.Visible = false; // Nascondi l'etichetta
            }
        }

        private void pictureBoxResetTouchKeyboard_MouseDown(object sender, EventArgs e)
        {
            pictureBoxResetTouchKeyboardPress.Visible = true;
            pictureBoxResetTouchKeyboard.Visible = false;

            var result = DependencyContainer.MessagesBoxImplementation.ShowMessage(
                "Do you want to reset the Touch Keyboard service?\n\n" +
                "- Click YES to reset the service.\n" +
                "- Click NO to Disable/Enable temporary the Touch Keyboard Panel.\n\n" +
                "- CANCEL to do nothing.",
                "Touch Keyboard fix keep popping-up",
                MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                // Avvia il countdown
                countDown = 5; // Resetta il conto alla rovescia
                labelCountDown.Visible = true; // Mostra l'etichetta
                labelCountDown.Text = countDown.ToString(); // Imposta il testo iniziale
                TimerLabelCountDown.Start(); // Avvia il timer

                // Avvia la logica per resettare la tastiera touch             
                Cursor = Cursors.WaitCursor;
                CodeForResetTouchKeyboard.ResetTouchKeyboardAsync(pictureBoxResetTouchKeyboard);
                Cursor = Cursors.Default;
                DependencyContainer.MessagesBoxImplementation.ShowMessage("The Touch Keyboard has been reset successfully. " +
                    "If the keyboard reappears, please close it manually and reapply" +
                    " the fix or try to disable temporary it by selcting `NO`.", "Success",
                    MessageBoxButtons.OK);

                pictureBoxResetTouchKeyboardPress.Visible = false;
                pictureBoxResetTouchKeyboard.Visible = true;
            }
            else if (result == DialogResult.No)
            {
                // Disattiva il touchscreen impostando la chiave di registro
                ShareDisableTouchscreenClass.DisableTouchscreen(pictureBoxResetTouchKeyboard);

            }
            // Se il risultato è Cancel, non fare nulla
            pictureBoxResetTouchKeyboardPress.Visible = false;
            pictureBoxResetTouchKeyboard.Visible = true;
        }


        private void buttonAbout_Click(object sender, EventArgs e)
        {
            _isAboutBoxOpen = true;
            AboutBox aboutBox = new AboutBox(this, _topMostTimer);
            aboutBox.FormClosed += (s, args) => _isAboutBoxOpen = false;
            aboutBox.Show();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            WindowsDefender.OpenWindowsDefender(DependencyContainer.MessagesBoxImplementation);
        }

        private async void pictureBoxRealTime_ON_Click(object sender, EventArgs e)
        {
            pictureBoxWaitingDefender.Visible = true;
            // Controlla lo stato di 'IsTamperProtected' prima di eseguire qualsiasi azione
            bool isTamperProtected = IsTamperProtected_Check.CheckIsTamperProtected();

            if (isTamperProtected)
            {
                // Mostra il messaggio e non proseguire se Tamper Protection è attivo
                DependencyContainer.MessagesBoxImplementation.ShowMessage(
                    "This Windows Defender Real-Time Protection Auto-Disable button only works if Tamper Protection is manually disabled. " +
                    "Please make sure Tamper Protection is disabled before using these auto buttons." +
                    "\nOtherwise, disabling Real-Time Protection will not succeed.",
                    "Attention",
                    MessageBoxButtons.OK);
            }
            else
            {
                
                // Tamper Protection è disabilitato, procedi con la disabilitazione della protezione in tempo reale
                //Cursor.Current = Cursors.WaitCursor;
                await WindowsDefender.DisableRealTimeProtection(DependencyContainer.MessagesBoxImplementation);
                DependencyContainer.MessagesBoxImplementation.ShowMessage(
                    "Windows Defender Real-Time Protection disabled successfully.",
                    "Information",
                    MessageBoxButtons.OK);

                // Nascondi il pulsante ON e mostra il pulsante OFF
                pictureBoxRealTime_ON.Visible = false;
                pictureBoxRealTime_OFF.Visible = true;

                //Cursor.Current = Cursors.Default;
                
            }
            pictureBoxWaitingDefender.Visible = false;
        }

        private async void pictureBoxRealTime_OFF_Click(object sender, EventArgs e)
        {
            pictureBoxWaitingDefender.Visible = true;
            // Controlla lo stato di 'IsTamperProtected' prima di eseguire qualsiasi azione
            bool isTamperProtected = IsTamperProtected_Check.CheckIsTamperProtected();

            if (isTamperProtected)
            {
                // Mostra il messaggio e non proseguire se Tamper Protection è attivo
                DependencyContainer.MessagesBoxImplementation.ShowMessage(
                    "This Windows Defender Real-Time Protection Auto-Disable button only works if Tamper Protection is manually disabled. " +
                    "Please make sure Tamper Protection is disabled before using these auto buttons." +
                    "\nOtherwise, disabling Real-Time Protection will not succeed.",
                    "Attention",
                    MessageBoxButtons.OK);
            }
            else
            {
                
                // Tamper Protection è disabilitato, procedi con l'abilitazione della protezione in tempo reale
                //Cursor.Current = Cursors.WaitCursor;
                await WindowsDefender.EnableRealTimeProtection(DependencyContainer.MessagesBoxImplementation);
                DependencyContainer.MessagesBoxImplementation.ShowMessage(
                    "Windows Defender Real-Time Protection enabled successfully.",
                    "Information",
                    MessageBoxButtons.OK);

                // Nascondi il pulsante OFF e mostra il pulsante ON
                pictureBoxRealTime_OFF.Visible = false;
                pictureBoxRealTime_ON.Visible = true;

                //Cursor.Current = Cursors.Default;
                
            }
            pictureBoxWaitingDefender.Visible = false;
        }

        private void pictureBoxControllers_Click(object sender, EventArgs e)
        {

        }

        private void customListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void special_Niewbie_ButtonRestoreBoost_Click(object sender, EventArgs e)
        {
            try
            {
                if (Environment.UserName == "HandleOS")
                {
                    _isAboutBoxOpen = true;
                    FormMessageBox.FormMessageBox formMessageBox = new FormMessageBox.FormMessageBox(this, _topMostTimer);
                    formMessageBox.FormClosed += (s, args) => _isAboutBoxOpen = false;
                    formMessageBox.ShowDialog();
                    this.BringToFront();
                }
                else
                {
                    DependencyContainer.MessagesBoxImplementation.ShowMessage(
                        "This section is exclusive to HandleOS users. This tool is specifically designed to maintain HandleOS optimized, and these changes are not recommended for a regular system!",
                        "Exclusive Section",
                        MessageBoxButtons.OK
                    );
                }
            }
            catch (Exception ex)
            {

                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error starting the program: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            _isAboutBoxOpen = true;
            info.FormInfoControllers formInfoControllers = new info.FormInfoControllers(this, _topMostTimer);
            formInfoControllers.FormClosed += (s, args) => _isAboutBoxOpen = false;
            formInfoControllers.ShowDialog();
            this.BringToFront();
        }

        private void pictureBoxTweakParadise_Click(object sender, EventArgs e)
        {

            try
            {
                if (Environment.UserName == "HandleOS")
                {
                    // Avvia l'apertura
                    timerGifTweakParadise.StartTimerForPictureTweakParadise(pictureBoxTweakParadise);
                    _isAboutBoxOpen = true;
                    FormTP.FormTweakParadise formTweakParadise = new FormTP.FormTweakParadise(this, _topMostTimer);
                    formTweakParadise.FormClosed += (s, args) =>
                    {
                        _isAboutBoxOpen = false;

                        // Avvia la chiusura (Reverse)
                        timerGifTweakParadise.StartTimerReverseForPictureTweakParadise(pictureBoxTweakParadise);
                    };
                    formTweakParadise.ShowDialog();
                    this.BringToFront();
                }
                else
                {
                    DependencyContainer.MessagesBoxImplementation.ShowMessage(
                        "This section is exclusive to HandleOS users. This tool is specifically designed to maintain HandleOS optimized, and these changes are not recommended for a regular system!",
                        "Exclusive Section",
                        MessageBoxButtons.OK
                    );
                }
            }
            catch (Exception ex)
            {

                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error starting the program: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private void dropDownMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void specialButtonDropDownMenu_Click(object sender, EventArgs e)
        {
            dropDownMenu.Show(specialButtonDropDownMenu, specialButtonDropDownMenu.Width + 2, -186); //-276 with Sleep Mode temporary not included
        }

        private void turnOffScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PowerManagement.TurnOffScreen();
            /*if (MessageBox.Show("Vuoi spegnere lo schermo?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
            }*/
        }

        private void sleepModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PowerManagement.SetSleepMode();
            /*if (MessageBox.Show("Vuoi mettere il computer in modalità sleep?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
            }
            */


        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PowerManagement.RestartComputer();
            /*if (MessageBox.Show("Vuoi riavviare il computer?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
            }
            */
        }

        private void powerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PowerManagement.ShutdownComputer();
            /*if (MessageBox.Show("Vuoi spegnere il computer?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
            }
            */
        }

        private void buttonHiberSleep_Click(object sender, EventArgs e)
        {
            try
            {
                if (Environment.UserName == "HandleOS")
                {
                    _isAboutBoxOpen = true;
                    FormHiberSleep formHiberSleep = new FormHiberSleep(this, _topMostTimer);
                    formHiberSleep.FormClosed += (s, args) => _isAboutBoxOpen = false;
                    formHiberSleep.ShowDialog();
                    this.BringToFront();
                }
                else
                {
                    DependencyContainer.MessagesBoxImplementation.ShowMessage(
                        "This section is exclusive to HandleOS users. This tool is specifically designed to maintain HandleOS optimized, and these changes are not recommended for a regular system!",
                        "Exclusive Section",
                        MessageBoxButtons.OK
                    );
                }
            }
            catch (Exception ex)
            {

                DependencyContainer.MessagesBoxImplementation.ShowMessage($"Error starting the program: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private void pictureBoxWaitingDefender_Click(object sender, EventArgs e)
        {

        }
    }
}
