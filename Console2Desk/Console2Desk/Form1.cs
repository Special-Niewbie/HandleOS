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
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

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

            winTheme = new WinTheme(this.Handle);
            winTheme.ApplyTheme();

            // Leggi il percorso dell'applicazione full screen dal file Settings.ini
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
            buttonAllyStockRes.MouseEnter += buttonAllyStockRes_MouseEnter;
            buttonAllyStockRes.MouseLeave += buttonAllyStockRes_MouseLeave;
            buttonIntegerScaling.MouseEnter += buttonIntegerScaling_MouseEnter;
            buttonIntegerScaling.MouseLeave += buttonIntegerScaling_MouseLeave;

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
            // Set the process window as TopMost
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

            BringToFront();

            // Catch the initial size of the window
            minWidth = this.Width;
            minHeight = this.Height;

            // Set this size as the minimum window size
            this.MinimumSize = new Size(minWidth, minHeight);
            RAMChecker ramChecker = new RAMChecker(pictureBoxCheckVRAM);
            ramChecker.CheckVirtualMemorySettingsAsync();

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
                MessageBox.Show("Error checking registry keys: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.Width < minWidth || this.Height < minHeight)
            {
                // If the size is below the limit, reset the size
                this.Width = Math.Max(this.Width, minWidth);
                this.Height = Math.Max(this.Height, minHeight);
            }
        }


        public void CheckRegistrySettings()
        {
            #region Check the Registry Values for Resolutions Checked/Unchecked
            try
            {
                // Gets all the names of the subkeys in the main registry key
                string[] subKeyNames = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}").GetSubKeyNames();

                // Search the subkey names for the correct value to use
                string subKeyName = null;
                foreach (string name in subKeyNames)
                {
                    if (name.EndsWith("0000") || name.EndsWith("0001"))
                    {
                        subKeyName = name;
                        break;
                    }
                }

                if (subKeyName == null)
                {
                    MessageBox.Show("Unable to find appropriate subkey.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Opens the registry subkey for reading
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Control\Class\{{4d36e968-e325-11ce-bfc1-08002be10318}}\{subKeyName}"))
                {
                    // Check if the registry keys match the expected values
                    if (key != null &&
                        ArraysEqual((byte[])key.GetValue("DALNonStandardModesBCD1"), new byte[] {
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
                        // If the registry keys match the expected values, show the pictureBox1
                        pictureBox1.Visible = true;
                        pictureBox2.Visible = false;
                    }
                    else
                    {
                        // If the registry keys do not match the expected values, hide the pictureBox1
                        pictureBox1.Visible = false;
                        pictureBox2.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, it hides the pictureBox1 and displays an error message
                pictureBox1.Visible = false;
                MessageBox.Show("Error checking registry keys: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton, buttonXinputTest, buttonChangeConsoleSettings, msStoreButton);
                UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1);

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
        private void desktopButton1_Click(object sender, EventArgs e)
        {
            DesktopButton.CodeFordesktopButton1(desktopButton1, explorerPath);
        }


        private void consoleButton1_Click(object sender, EventArgs e)
        {
            ConsoleButton.CodeForconsoleButton1(consoleButton1, fullscreenAppPath, defaultFullscreenSteamAppPath);
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

        private void buttonAllyUltimateRes_Click(object sender, EventArgs e)
        {
            // Calling the CodeForallyUltimateButton method from the ButtonallyUltimate.cs file
            ButtonAllyUltimate.CodeForallyUltimateButton(this);

            // Show info message
            string message = "Please note that the Windows Display Resolution Settings may have limitations in displaying all the resolutions added with this button. To see the complete list of resolutions, including any specific resolution you might need, follow these steps:\n\n" +
                     "1. Go to Settings -> System -> Display -> Advanced display settings.\n" +
                     "2. Click on 'Display adapter properties for Display 1'.\n" +
                     "3. Press the 'List All Modes' button.\n\n" +
                     "Would you like to visit the guide for more information?";

            DialogResult result = MessageBox.Show(message, "Display Resolution Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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
            ButtonAllyStock.CodeForallyStockButton(this);
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

        private void buttonIntegerScaling_Click(object sender, EventArgs e)
        {
            AddButtonIntegerScaling.CodeForaddButtonIntegerScaling(pictureBox3);
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

        private void button_IncreaseRAM_System_Click(object sender, EventArgs e)
        {
            Code_for_IncreaseRAM_System increaseRAMSystem = new Code_for_IncreaseRAM_System();
            increaseRAMSystem.IncreaseRAM();
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
            ButtonMinimizeConsoleWindow.CodeForbuttonMiniConsoleWindow(this);
        }

        #endregion


        //############End - Futures Buttons


        private void pictureBoxIS_Addon_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxRes_Click(object sender, EventArgs e)
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
            pictureBoxWifiFunction.CodeForpictureBoxWifi(this, pictureBoxWifi);
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

                MessageBox.Show("Impossibile trovare il programma RadeonSoftware.exe.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBoxAMDadrenaline.Visible = true;
                pictureBoxAMDadrenalinePress.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante l'apertura di Radeon Software: " + ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Unable to open the default browser. Make sure it's properly configured on your system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pictureBoxDonation1.Visible = true;
                    pictureBoxDonation2.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            TouchScreenStartupCheck.CheckTouchScreenStatus(touchScreenEnDbButton);
            // Check your MS Store installation and update the button text accordingly
            msStoreButtonStartupCheck.CheckMsStoreInstallation(msStoreButton);
            // Check if the button is visible
            if (buttonRestorePauseUpgrade.Visible)
            {
                // Run touchscreen status check when program starts
                TouchScreenStartupCheck.CheckTouchScreenStatus(touchScreenEnDbButton);

                // Check your MS Store installation and update the button text accordingly
                msStoreButtonStartupCheck.CheckMsStoreInstallation(msStoreButton);

                // If it is visible, it makes it invisible
                // Hides the buttons
                UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton, buttonXinputTest, buttonChangeConsoleSettings, msStoreButton);
                // Enable the desktop button
                UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1);

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


                desktopButton1.Enabled = false;
                desktopButton1.BackColor = Color.FromArgb(40, 40, 40);
                desktopButton1.BorderColor = Color.Gray;

                consoleButton1.Enabled = false;
                consoleButton1.BackColor = Color.FromArgb(40, 40, 40);
                consoleButton1.BorderColor = Color.Gray;
                timerGif.StartTimerForPictureBoxSettings(pictureBoxSettings);
            }

        }
        private void buttonRestorePauseUpgrade_Click(object sender, EventArgs e)
        {
            RestorePauseUpgrade.Execute(buttonRestorePauseUpgrade);
        }
        private void buttonRestorePauseUpgrade_MouseUp(object sender, EventArgs e)
        {
            // Hides the buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton, buttonXinputTest, buttonChangeConsoleSettings, msStoreButton);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1);
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
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonXinputTest_Click(object sender, EventArgs e)
        {
            // Definisci il percorso del programma da avviare
            string exePath = @"C:\Program Files\Console2Desk\XinputTest\XInput.exe";

            try
            {
                // Avvia il processo
                Process process = new Process();
                process.StartInfo.FileName = exePath;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = false;
                process.Start();

                // Nascondi la finestra principale
                this.Hide();

                // Attendi che il processo termini
                process.WaitForExit();

                // Mostra di nuovo la finestra principale
                this.Show();
                this.BringToFront(); // Porta la finestra principale in primo piano
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
                MessageBox.Show($"Errore nell'avvio del programma: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonOpenFileExplorer_MouseUp(object sender, EventArgs e)
        {
            // Hidesbuttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton, buttonXinputTest, buttonChangeConsoleSettings, msStoreButton);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1);
        }
        public void touchScreenEnDbButton_Click(object sender, EventArgs e)
        {
            TouchScreenEnDbButtonCoding touchScreenCoding = new TouchScreenEnDbButtonCoding();
            touchScreenCoding.code4touchScreenEnDbButton(touchScreenEnDbButton);
        }
        public void touchScreenEnDbButton_MouseUp(object sender, EventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton, buttonXinputTest, buttonChangeConsoleSettings, msStoreButton);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1);
        }

        public void buttonXinputTest_MouseUp(object sender, MouseEventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton, buttonXinputTest, buttonChangeConsoleSettings, msStoreButton);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1);
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
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton, buttonXinputTest, buttonChangeConsoleSettings, msStoreButton);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1);
        }

        private void msStoreButton_Click(object sender, EventArgs e)
        {
            msStoreButtonCoding.ExecuteMsStoreCommand(msStoreButton);
        }

        private void msStoreButton_MouseUp(object sender, MouseEventArgs e)
        {
            // Hides buttons
            UISettingsControlManager.HideButtons(buttonOpenFileExplorer, buttonRestorePauseUpgrade, touchScreenEnDbButton, buttonXinputTest, buttonChangeConsoleSettings, msStoreButton);

            // Enable the desktop button
            UISettingsControlManager.EnableDesktopButton(desktopButton1, consoleButton1);
        }
        #endregion

        //#########End - Settings


        private void buttonAbout_Click(object sender, EventArgs e)
        {
            // Creates an instance of the AboutBox window
            AboutBox aboutBox = new AboutBox();

            // Show the "About" window
            aboutBox.ShowDialog(); // Use ShowDialog to make it a modal window

        }

        private void buttonRealTimeProtection_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            WindowsDefender.OpenWindowsDefender();
        }

        private void pictureBoxRealTime_ON_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Windows Defender Real-Time Protection Auto-Enable button only works if Temper-Protection is manually disabled. " +
                "Please make sure Temper-Protection is disabled before using these auto buttons." +
                "\nOtherwise, enabling Real-Time Protection will not succeed.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Cursor.Current = Cursors.WaitCursor;

            WindowsDefender.EnableRealTimeProtection();
            MessageBox.Show("Windows Defender Real-Time Protection enabled successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Cursor.Current = Cursors.Default;
        }

        private void pictureBoxRealTime_OFF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Windows Defender Real-Time Protection Auto-Disable button only works if Temper-Protection is manually disabled. " +
                "Please make sure Temper-Protection is disabled before using these auto buttons." +
                "\nOtherwise, disabling Real-Time Protection will not succeed.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Cursor.Current = Cursors.WaitCursor;

            WindowsDefender.DisableRealTimeProtection();
            MessageBox.Show("Windows Defender Real-Time Protection disabled successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Cursor.Current = Cursors.Default;
        }
    }
}
