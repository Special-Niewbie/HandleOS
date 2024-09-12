/*
Console2Desk and HandleOs Benchmark are:

Copyright (C) 2024 Special-Niewbie Softwares

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
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace HandleOS_Benchmark
{
    public partial class Form1 : Form
    {
        // Import user32.dll to use for getting window rectangle
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rectangle rect);

        private bool _isDragging = false;
        private Point _dragStartPoint;

        private FormBench benchForm;
        private CancellationTokenSource cancellationTokenSource;

        public Form1()
        {
            InitializeComponent();
            // Populate the comboBoxTheme with available themes
            PopulateComboBoxTheme();

            // Force the change theme - for the theme change event
            ThemeSettings.ThemeChanged += OnThemeChanged;

            // Apply current theme on startup
            ThemeSettings.ApplyCurrentTheme();

            ThemeSettings.panelTitle = panelTitle;
            ThemeSettings.panelCentral = panelCentral;
            ThemeSettings.pictureBoxButton4FormSubScores = pictureBoxButton4FormSubScores;
            ThemeSettings.pictureBoxButton4FormSubScoresPress = pictureBoxButton4FormSubScoresPress;
            ThemeSettings.pictureBoxCamera = pictureBoxCamera;

            ThemeSettings.customButtonNiewbieSTART = customButtonNiewbieSTART;
            ThemeSettings.comboBoxTheme = comboBoxTheme;

            ThemeSettings.label1 = label1;
            ThemeSettings.label2 = label2;
            ThemeSettings.labelSSD = labelSSD;
            ThemeSettings.labelDirect3D = labelDirect3D;
            ThemeSettings.labelCPU = labelCPU;
            ThemeSettings.labelRAM = labelRAM;
            ThemeSettings.labelGPU = labelGPU;
            ThemeSettings.labelSubScoreCPU = labelSubScoreCPU;
            ThemeSettings.labelSubScoreRAM = labelSubScoreRAM;
            ThemeSettings.labelSubScoreGPU = labelSubScoreGPU;
            ThemeSettings.labelSubScoreSSD = labelSubScoreSSD;
            ThemeSettings.labelSubScoreDirect3D = labelSubScoreDirect3D;
            ThemeSettings.labelNameTitle = labelNameTitle;
            ThemeSettings.labelProductDeatils = labelProductDeatils;
            ThemeSettings.labelNameProduct = labelNameProduct;
            ThemeSettings.labelDate = labelDate;

            ThemeSettings.roundedPanelAverageScore = roundedPanelAverageScore;
            ThemeSettings.roundedPanel4LabelTitleScore = roundedPanel4LabelTitleScore;
            ThemeSettings.roundedPanelSubScores = roundedPanelSubScores;
            ThemeSettings.roundedPanelTheme = roundedPanelTheme;
            ThemeSettings.roundedPanelComponents = roundedPanelComponents;

            ThemeSettings.customProgressBarCPU = customProgressBarCPU;
            ThemeSettings.customProgressBarRAM = customProgressBarRAM;
            ThemeSettings.customProgressBarSSD = customProgressBarSSD;
            ThemeSettings.customProgressBarGPU = customProgressBarGPU;
            ThemeSettings.customProgressBarDirect3D = customProgressBarDirect3D;
            ThemeSettings.customProgressBarResult = customProgressBarResult;


            // Load theme from file at startup
            ThemeSettings.LoadThemeFromFile();
            ThemeSettings.ApplyCurrentTheme();

            // List theme change event to comboBox
            comboBoxTheme.OnSelectedIndexChanged += comboBoxTheme_OnSelectedIndexChanged;

            // Set the selected theme in the comboBox
            comboBoxTheme.SelectedItem = ThemeSettings.CurrentTheme;

            panelTitle.MouseDown += panelTitle_MouseDown;
            panelTitle.MouseMove += panelTitle_MouseMove;
            panelTitle.MouseUp += panelTitle_MouseUp;
            label1.MouseDown += label1_MouseDown;
            label1.MouseMove += label1_MouseMove;
            label1.MouseUp += label1_MouseUp;

            this.Paint += Form1_Paint;
        }

        private void OnThemeChanged()
        {
            // Force the form to repaint
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Define the colors for the gradient based Window on the current theme
            Color color1, color2;
            if (ThemeSettings.CurrentTheme == "Dark")
            {
                // Gradient colors for Dark theme
                color1 = Color.FromArgb(45, 45, 48); // Dark Color
                color2 = Color.FromArgb(28, 28, 30); // Darker Color
            }
            else
            {
                // Gradient colors for Light theme
                color1 = Color.FromArgb(232, 240, 251); // Light Color
                color2 = Color.FromArgb(217, 237, 238); // Lighter Color
            }

            // Create a linear gradient brush
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, color1, color2, 0f))
            {
                // Paint the gradient on the form
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            DisplayPCName();
            DisplayHardwareDetails();
            LoadLastWinSatDate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadIconFromRegistryToPictureBox(pictureBoxConsole, @"Software\Microsoft\Windows\CurrentVersion\Explorer\CLSID\{20D04FE0-3AEA-1069-A2D8-08002B30309D}\DefaultIcon");
            UpdateProgressBars();
        }

        private void LoadIconFromRegistryToPictureBox(PictureBox pictureBox, string registryKeyPath)
        {
            // Read the value of the registry key
            string iconValue = GetRegistryKeyValue(registryKeyPath);
            if (string.IsNullOrEmpty(iconValue))
            {
                SetGenericIcon(pictureBox);
                return;
            }

            // Split the .dll file path and icon index
            string[] parts = iconValue.Split(',');
            if (parts.Length != 2)
            {
                SetGenericIcon(pictureBox);
                return;
            }

            string dllPath = parts[0].Trim();
            string iconIndexStr = parts[1].Trim();

            // Check if the DLL file is HandleOS_icons.dll
            if (!dllPath.EndsWith("HandleOS_icons.dll", StringComparison.OrdinalIgnoreCase))
            {
                SetGenericIcon(pictureBox);
                return;
            }

            // Look for the corresponding icon in the Resources folder
            string iconFileName = $"{iconIndexStr}.png";
            string resourcePath = Path.Combine(Application.StartupPath, "handled", iconFileName);

            if (File.Exists(resourcePath))
            {
                try
                {
                    using (var image = Image.FromFile(resourcePath))
                    {
                        pictureBox.Image = new Bitmap(image);
                    }
                }
                catch
                {
                    SetGenericIcon(pictureBox);
                }
            }
            else
            {
                SetGenericIcon(pictureBox);
            }
        }

        private void SetGenericIcon(PictureBox pictureBox)
        {
            string genericIconPath = Path.Combine(Application.StartupPath, "handled", "gen.png");
            if (File.Exists(genericIconPath))
            {
                try
                {
                    using (var image = Image.FromFile(genericIconPath))
                    {
                        pictureBox.Image = new Bitmap(image);
                    }
                }
                catch
                {
                    // If this fails, do nothing or set a default image
                    pictureBox.Image = null;
                }
            }
            else
            {
                pictureBox.Image = null;
            }
        }

        private string GetRegistryKeyValue(string registryKeyPath)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKeyPath))
            {
                return key?.GetValue(null)?.ToString();
            }
        }

        private void UpdateProgressBars()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_WinSAT");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // Get the scores
                    double cpuScore = Convert.ToDouble(queryObj["CPUScore"]);
                    double d3dScore = Convert.ToDouble(queryObj["D3DScore"]);
                    double diskScore = Convert.ToDouble(queryObj["DiskScore"]);
                    double graphicsScore = Convert.ToDouble(queryObj["GraphicsScore"]);
                    double memoryScore = Convert.ToDouble(queryObj["MemoryScore"]);

                    // Convert scores to 1-100 scale
                    int cpuProgress = (int)(cpuScore * 10);
                    int d3dProgress = (int)(d3dScore * 10);
                    int diskProgress = (int)(diskScore * 10);
                    int graphicsProgress = (int)(graphicsScore * 10);
                    int memoryProgress = (int)(memoryScore * 10);

                    // Update the progress bars and labels
                    UpdateProgressBarAndLabel(customProgressBarCPU, labelSubScoreCPU, cpuProgress, cpuScore);
                    UpdateProgressBarAndLabel(customProgressBarDirect3D, labelSubScoreDirect3D, d3dProgress, d3dScore);
                    UpdateProgressBarAndLabel(customProgressBarSSD, labelSubScoreSSD, diskProgress, diskScore);
                    UpdateProgressBarAndLabel(customProgressBarGPU, labelSubScoreGPU, graphicsProgress, graphicsScore);
                    UpdateProgressBarAndLabel(customProgressBarRAM, labelSubScoreRAM, memoryProgress, memoryScore);

                    // Calculate the average of the scores
                    double totalScore = cpuScore + d3dScore + diskScore + graphicsScore + memoryScore;
                    double averageScore = totalScore / 5;
                    int averagePercentile = (int)(averageScore * 10);

                    // Update the label "labelResults" with the percentile average
                    //labelResults.Text = $"{averagePercentile}";
                    customProgressBarResult.BarText = $"{averagePercentile}";

                    // Usiamo solo il primo risultato
                    break;
                }
            }
            catch (ManagementException ex)
            {
                MessageBox.Show($"Errore durante l'accesso alle informazioni di WinSAT: {ex.Message}");
            }
            catch (Exception ex)
            {
                return;
                // No need below message
                //MessageBox.Show($"Errore durante l'aggiornamento delle progress bar: {ex.Message}\n\nStackTrace: {ex.StackTrace}");
            }
        }

        private void UpdateProgressBarAndLabel(CustomProgressBar progressBar, Label label, int progressValue, double score)
        {
            progressBar.Value = progressValue;

            // Convert the score to a percentage value
            int scorePercentile = (int)(score * 10);

            // Show the converted score to each labels
            label.Text = $"{scorePercentile}"; // Show the score converted from 1 to 100
        }

        private void DisplayPCName()
        {
            string pcName = Environment.MachineName;
            labelNameProduct.Text = !string.IsNullOrEmpty(pcName) ? pcName : "No name product gived";
        }

        private void DisplayHardwareDetails()
        {
            string cpu = GetCpuDetails();
            string ssd = GetSsdDetails();
            string gpu = GetGpuDetails();
            string ram = GetRamDetails();

            labelProductDeatils.Text = $"• CPU: {cpu}\n• SSD: {ssd}\n• GPU: {gpu}\n• RAM: {ram}";
        }

        private string GetCpuDetails()
        {
            string cpuDetails = string.Empty;
            using (var searcher = new ManagementObjectSearcher("select Name, NumberOfCores from Win32_Processor"))
            {
                foreach (var item in searcher.Get())
                {
                    cpuDetails = $"{item["Name"]} ({item["NumberOfCores"]} Cores)";
                }
            }
            return cpuDetails;
        }

        private string GetSsdDetails()
        {
            string ssdDetails = string.Empty;
            using (var searcher = new ManagementObjectSearcher("select Model, Size from Win32_DiskDrive where MediaType='Fixed hard disk media'"))
            {
                foreach (var item in searcher.Get())
                {
                    ssdDetails = $"{item["Model"]} ({Math.Round(Convert.ToDouble(item["Size"]) / (1024 * 1024 * 1024), 2)} GB)";
                }
            }
            return ssdDetails;
        }

        private string GetGpuDetails()
        {
            string gpuDetails = string.Empty;
            using (var searcher = new ManagementObjectSearcher("select Name, AdapterRAM from Win32_VideoController"))
            {
                foreach (var item in searcher.Get())
                {
                    gpuDetails = $"{item["Name"]} ({Math.Round(Convert.ToDouble(item["AdapterRAM"]) / (1024 * 1024 * 1024), 2)} GB VRAM)";
                }
            }
            return gpuDetails;
        }

        private string GetRamDetails()
        {
            string ramDetails = string.Empty;
            using (var searcher = new ManagementObjectSearcher("select Capacity from Win32_PhysicalMemory"))
            {
                double totalCapacity = 0;
                foreach (var item in searcher.Get())
                {
                    totalCapacity += Convert.ToDouble(item["Capacity"]);
                }
                ramDetails = $"{Math.Round(totalCapacity / (1024 * 1024 * 1024), 2)} GB";
            }
            return ramDetails;
        }

        private void buttonCloseWindow_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panelTitle_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging Window
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void panelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Calculate the new position of the window
                var deltaX = e.X - _dragStartPoint.X;
                var deltaY = e.Y - _dragStartPoint.Y;
                var newLocation = new Point(this.Left + deltaX, this.Top + deltaY);
                this.Location = newLocation;
            }
        }

        private void panelTitle_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging Window
            _isDragging = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging Window
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Calculate the new position of the window
                var deltaX = e.X - _dragStartPoint.X;
                var deltaY = e.Y - _dragStartPoint.Y;
                var newLocation = new Point(this.Left + deltaX, this.Top + deltaY);
                this.Location = newLocation;
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging Window
            _isDragging = false;
        }



        private void pictureBoxButton4FormSubScores_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxButton4FormSubScores.Visible = false;
            pictureBoxButton4FormSubScoresPress.Visible = true;
            Task.Delay(100).Wait();
            pictureBoxButton4FormSubScores.Visible = true;
            pictureBoxButton4FormSubScoresPress.Visible = false;

            // Check if there is already an open FormSubScores instance
            Form existingForm = Application.OpenForms["FormSubScores"];
            if (existingForm == null) // If it doesn't exist, we create it and display it
            {
                FormSubScores formSubScores = new FormSubScores();

                // Use public property to access controls
                ThemeSettings.labelTitleSubscoreDetails = formSubScores.LabelTitleSubscoreDetails;
                ThemeSettings.labelCPU2 = formSubScores.LabelCPU2;
                ThemeSettings.labelRam2 = formSubScores.LabelRam2;
                ThemeSettings.labelGPU2 = formSubScores.LabelGPU2;
                ThemeSettings.labelSSD2 = formSubScores.LabelSSD2;
                ThemeSettings.labelCPULZWCompression = formSubScores.LabelCPULZWCompression;
                ThemeSettings.labelCPUAES256Encryption = formSubScores.LabelCPUAES256Encryption;
                ThemeSettings.labelCPUVistaCompression = formSubScores.LabelCPUVistaCompression;
                ThemeSettings.labelCPUSHA1Hash = formSubScores.LabelCPUSHA1Hash;
                ThemeSettings.labelUniprocCPULZWCompression = formSubScores.LabelUniprocCPULZWCompression;
                ThemeSettings.labelUniprocCPUAES256Encryption = formSubScores.LabelUniprocCPUAES256Encryption;
                ThemeSettings.labelUniprocCPUVistaCompression = formSubScores.LabelUniprocCPUVistaCompression;
                ThemeSettings.labelUniprocCPUSHA1Hash = formSubScores.LabelUniprocCPUSHA1Hash;
                ThemeSettings.labelDiskSequentialRead = formSubScores.LabelDiskSequentialRead;
                ThemeSettings.labelDiskRandomRead = formSubScores.LabelDiskRandomRead;
                ThemeSettings.labelDirect3DBatchPerformance = formSubScores.LabelDirect3DBatchPerformance;
                ThemeSettings.labelDirect3DAlphaBlendPerformance = formSubScores.LabelDirect3DAlphaBlendPerformance;
                ThemeSettings.labelcustomProgressBarDirect3DALUPerformance = formSubScores.LabelcustomProgressBarDirect3DALUPerformance;
                ThemeSettings.labelDirect3DTextureLoadPerformance = formSubScores.LabelDirect3DTextureLoadPerformance;
                ThemeSettings.labelDirect3DGeometryPerformance = formSubScores.LabelDirect3DGeometryPerformance;
                ThemeSettings.labelDirect3DConstantBufferPerformance = formSubScores.LabelDirect3DConstantBufferPerformance;
                ThemeSettings.labelVideoMemoryThroughput = formSubScores.LabelVideoMemoryThroughput;
                ThemeSettings.labelDshowVideoEncodeTime = formSubScores.LabelDshowVideoEncodeTime;
                ThemeSettings.labelDshowVideoDecodeTime = formSubScores.LabelDshowVideoDecodeTime;
                ThemeSettings.labelMediaFoundationDecodeTime = formSubScores.LabelMediaFoundationDecodeTime;
                ThemeSettings.labelMemoryPerformance = formSubScores.LabelMemoryPerformance;

                ThemeSettings.roundedPanel1 = formSubScores.RoundedPanel1;
                ThemeSettings.roundedPanel2 = formSubScores.RoundedPanel2;
                ThemeSettings.roundedPanel3 = formSubScores.RoundedPanel3;
                ThemeSettings.roundedPanel4 = formSubScores.RoundedPanel4;

                ThemeSettings.pictureBoxCamera2 = formSubScores.PictureBoxCamera2;

                formSubScores.ShowDialog(); // Use ShowDialog to freeze Form1 until FormSubScores is closed

            }
        }

        #region Unused to clean the code view

        private void panelCentral_Paint(object sender, PaintEventArgs e)
        {

        }
        private void labelSlogan_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void customProgressBarCPU_Click(object sender, EventArgs e)
        {

        }
        private void customProgressBarRAM_Click(object sender, EventArgs e)
        {

        }
        private void customProgressBarSSD_Click(object sender, EventArgs e)
        {

        }
        private void customProgressBarGPU_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDirect3D_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarResult_Click(object sender, EventArgs e)
        {

        }

        private void labelSubScoreCPU_Click(object sender, EventArgs e)
        {

        }

        private void labelSubScoreDirect3D_Click(object sender, EventArgs e)
        {

        }

        private void labelSubScoreSSD_Click(object sender, EventArgs e)
        {

        }

        private void labelSubScoreGPU_Click(object sender, EventArgs e)
        {

        }

        private void labelSubScoreRAM_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxButton4FormSubScores_Click(object sender, EventArgs e)
        {

        }
        private void roundedPanel4LabelTitleScore_Paint(object sender, PaintEventArgs e)
        {

        }
        private void roundedPanelComponents_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roundedPanelTheme_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roundedPanelSubScores_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roundedPanelAverageScore_Paint(object sender, PaintEventArgs e)
        {

        }
        private void pictureBoxConsole_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxButton4FormSubScores_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void pictureBoxButton4FormSubScoresPress_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxButton4FormSubScoresPress_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBoxButton4FormSubScoresPress_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void labelNameProduct_Click(object sender, EventArgs e)
        {

        }

        private void labelProductDeatils_Click(object sender, EventArgs e)
        {

        }
        private void labelDate_Click(object sender, EventArgs e)
        {

        }
        #endregion
        private void pictureBoxCamera_Click(object sender, EventArgs e)
        {
            // Call the method to take a screenshot
            TakeScreenshot();
        }

        private void TakeScreenshot()
        {
            try
            {
                // Create a rectangle to capture the window
                Rectangle rect = new Rectangle();
                GetWindowRect(this.Handle, ref rect);

                // Calculate width and height of the window
                int width = rect.Width - rect.X;
                int height = rect.Height - rect.Y;

                // Create a bitmap of the appropriate size
                using (Bitmap screenshot = new Bitmap(width, height))
                {
                    // Capture the screen into the bitmap
                    using (Graphics g = Graphics.FromImage(screenshot))
                    {
                        g.CopyFromScreen(rect.Left, rect.Top, 0, 0, new Size(width, height));
                    }

                    // Open a SaveFileDialog to allow the user to choose where to save the screenshot
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PNG Image|*.png";
                        saveFileDialog.Title = "Save the screenshot";
                        saveFileDialog.FileName = "screenshot.png"; // Default file name

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Save the captured image to the chosen file
                            screenshot.Save(saveFileDialog.FileName);

                            // Show a confirmation message
                            MessageBox.Show("Screenshot captured and saved as " + saveFileDialog.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                MessageBox.Show("Error capturing screenshot: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void customButtonNiewbieSTART_Click(object sender, EventArgs e)
        {
            benchForm = new FormBench();
            benchForm.RequestCancelBenchmark += (s, args) =>
            {
                // Trigger cancellation
                cancellationTokenSource?.Cancel();
            };
            benchForm.Show();

            cancellationTokenSource = new CancellationTokenSource();
            await RunWinSatFormalAsync(cancellationTokenSource.Token);

            benchForm.Close();
            benchForm.Dispose();

            Task.Delay(1000).Wait(); // Wait for a second
            UpdateProgressBars();
        }
        private async Task RunWinSatFormalAsync(CancellationToken cancellationToken)
        {
            int totalSteps = 28;
            int currentStep = 0;
            StringBuilder fullOutput = new StringBuilder();
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "winsat";
                process.StartInfo.Arguments = "formal";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                while (!process.HasExited)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        process.Kill();
                        break;
                    }

                    string output = await process.StandardOutput.ReadLineAsync();
                    if (output != null)
                    {
                        fullOutput.AppendLine(output);
                        if (output.StartsWith("> Running:") || output.StartsWith("> Assessing"))
                        {
                            currentStep++;
                            benchForm.UpdateProgress(Math.Min(currentStep, totalSteps), totalSteps);
                        }
                        // Console.WriteLine(output); // For debugging
                    }
                    await Task.Delay(100);
                }
                // Ensure the progress bar reaches 100% at the end
                benchForm.UpdateProgress(totalSteps, totalSteps);
            }
        }
        private void PopulateComboBoxTheme()
        {
            comboBoxTheme.Items.Clear(); // Clears any (IF) existing old entries left theme
            comboBoxTheme.Items.Add("Light");
            comboBoxTheme.Items.Add("Dark");
        }
        private void comboBoxTheme_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTheme = comboBoxTheme.SelectedItem.ToString();
            ThemeSettings.CurrentTheme = selectedTheme;
        }

        private void LoadLastWinSatDate()
        {
            // Path to WinSAT DataStore folder
            string winSatDataStorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Performance\WinSAT\DataStore");

            // Check if the folder exists
            if (Directory.Exists(winSatDataStorePath))
            {
                // Get all .xml files in the folder
                var xmlFiles = Directory.GetFiles(winSatDataStorePath, "*.xml");

                if (xmlFiles.Length > 0)
                {
                    // Find the file with the latest date and time
                    var lastWinSatFile = xmlFiles
                        .Select(filePath => new FileInfo(filePath))
                        .OrderByDescending(file => file.Name) // Sort by file name descending
                        .FirstOrDefault();

                    if (lastWinSatFile != null)
                    {
                        // Extract date and time from file name
                        string fileName = lastWinSatFile.Name;
                        string[] parts = fileName.Split(' ');
                        if (parts.Length >= 2)
                        {
                            // Remove the last part of the time (.XXX)
                            string timePart = parts[1];
                            string[] timeParts = timePart.Split('.');

                            // Replace the dot between hours and minutes with a colon
                            if (timeParts.Length >= 2)
                            {
                                string formattedTime = timeParts[0] + ":" + timeParts[1]; // Let's fix the format as "HH:MM"
                                string dateTimePart = parts[0] + " " + formattedTime;
                                labelDate.Text = "Last benchmark date:  " + dateTimePart;
                            }
                        }
                    }
                }
                else
                {
                    // If there are no .xml files
                    labelDate.Text = "No benchmark started yet";
                }
            }
            else
            {
                // If the folder does not exist
                //labelDate.Text = "Cartella WinSAT non trovata"; We don't need it
                return;
            }
        }

        private void customButtonNiewbieClearData_Click(object sender, EventArgs e)
        {
            // Path to the WinSAT DataStore folder
            string winSatDataStorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Performance\WinSAT\DataStore");

            // Confirm with the user if they want to delete all XML files
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete all past score data?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Check if the directory exists
                    if (Directory.Exists(winSatDataStorePath))
                    {
                        // Get all XML files in the directory
                        string[] xmlFiles = Directory.GetFiles(winSatDataStorePath, "*.xml");

                        // Delete each XML file
                        foreach (string file in xmlFiles)
                        {
                            File.Delete(file);
                        }

                        // Inform the user that the files have been deleted
                        MessageBox.Show("All past score data has been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Task.Delay(500).Wait(); //wait for half second
                        UpdateProgressBars();
                    }
                    else
                    {
                        // Inform the user that the directory was not found
                        MessageBox.Show("DataStore directory not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during deletion
                    MessageBox.Show("An error occurred while deleting files: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // If the user pressed No, do nothing
        }
    }
}

