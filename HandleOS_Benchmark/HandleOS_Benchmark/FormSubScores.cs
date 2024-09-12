using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace HandleOS_Benchmark
{

    public partial class FormSubScores : Form
    {
        // Import user32.dll to use for getting window rectangle
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rectangle rect);

        private WinTheme winTheme;

        private bool hasShownNoDataMessage = false;
        public FormSubScores()
        {
            InitializeComponent();
            winTheme = new WinTheme(this.Handle);
            winTheme.ApplyTheme();

            this.Load += FormSubScores_Load;
            this.Paint += FormSubScores_Paint;

            RunWinSatQuery();

        }

        private void FormSubScores_Load(object sender, EventArgs e)
        {
            ThemeSettings.ApplyCurrentTheme();
        }
        private void FormSubScores_Paint(object sender, PaintEventArgs e)
        {
            // As Form1 define the colors for the gradient based on the current theme
            Color color1, color2;
            if (ThemeSettings.CurrentTheme == "Dark")
            {
                // Gradient colors for Dark theme
                color1 = Color.FromArgb(45, 45, 48);
                color2 = Color.FromArgb(28, 28, 30); 
            }
            else
            {
                // Gradient colors for Light theme
                color1 = Color.FromArgb(232, 240, 251); 
                color2 = Color.FromArgb(217, 237, 238); 
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, color1, color2, 0f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        #region All Property Getter for each object theme
        public Label LabelTitleSubscoreDetails
        {
            get { return labelTitleSubscoreDetails; }
        }
        public Label LabelCPU2
        {
            get { return labelCPU2; }
        }
        public Label LabelRam2
        {
            get { return labelRam2; }
        }
        public Label LabelGPU2
        {
            get { return labelGPU2; }
        }
        public Label LabelSSD2
        {
            get { return labelSSD2; }
        }
        public Label LabelCPULZWCompression
        {
            get { return labelCPULZWCompression; }
        }
        public Label LabelCPUAES256Encryption
        {
            get { return labelCPUAES256Encryption; }
        }
        public Label LabelCPUVistaCompression
        {
            get { return labelCPUVistaCompression; }
        }
        public Label LabelCPUSHA1Hash
        {
            get { return labelCPUSHA1Hash; }
        }
        public Label LabelUniprocCPULZWCompression
        {
            get { return labelUniprocCPULZWCompression; }
        }
        public Label LabelUniprocCPUAES256Encryption
        {
            get { return labelUniprocCPUAES256Encryption; }
        }
        public Label LabelUniprocCPUVistaCompression
        {
            get { return labelUniprocCPUVistaCompression; }
        }
        public Label LabelUniprocCPUSHA1Hash
        {
            get { return labelUniprocCPUSHA1Hash; }
        }
        public Label LabelDiskSequentialRead
        {
            get { return labelDiskSequentialRead; }
        }
        public Label LabelDiskRandomRead
        {
            get { return labelDiskRandomRead; }
        }
        public Label LabelDirect3DBatchPerformance
        {
            get { return labelDirect3DBatchPerformance; }
        }
        public Label LabelDirect3DAlphaBlendPerformance
        {
            get { return labelDirect3DAlphaBlendPerformance; }
        }
        public Label LabelcustomProgressBarDirect3DALUPerformance
        {
            get { return labelcustomProgressBarDirect3DALUPerformance; }
        }
        public Label LabelDirect3DTextureLoadPerformance
        {
            get { return labelDirect3DTextureLoadPerformance; }
        }
        public Label LabelDirect3DGeometryPerformance
        {
            get { return labelDirect3DGeometryPerformance; }
        }
        public Label LabelDirect3DConstantBufferPerformance
        {
            get { return labelDirect3DConstantBufferPerformance; }
        }
        public Label LabelVideoMemoryThroughput
        {
            get { return labelVideoMemoryThroughput; }
        }
        public Label LabelDshowVideoEncodeTime
        {
            get { return labelDshowVideoEncodeTime; }
        }
        public Label LabelDshowVideoDecodeTime
        {
            get { return labelDshowVideoDecodeTime; }
        }
        public Label LabelMediaFoundationDecodeTime
        {
            get { return labelMediaFoundationDecodeTime; }
        }
        public Label LabelMemoryPerformance
        {
            get { return labelMemoryPerformance; }
        }
        public RoundedPanel RoundedPanel1
        {
            get { return roundedPanel1; }
        }
        public RoundedPanel RoundedPanel2
        {
            get { return roundedPanel2; }
        }
        public RoundedPanel RoundedPanel3
        {
            get { return roundedPanel3; }
        }
        public RoundedPanel RoundedPanel4
        {
            get { return roundedPanel4; }
        }
        public PictureBox PictureBoxCamera2
        {
            get { return pictureBoxCamera2; }
        }      
        #endregion

        private void RunWinSatQuery()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "winsat";
                process.StartInfo.Arguments = "query";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // MessageBox.Show("Output ricevuto: " + output); // Debug
                ParseWinSatOutput(output);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error running winsat query: {ex.Message}");
            }
        }

        private void ParseWinSatOutput(string output)
        {
            Dictionary<string, float> results = new Dictionary<string, float>();

            string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.TrimStart().StartsWith("> Disk  Sequential 64.0 Read") ||
                    line.TrimStart().StartsWith("> Disk  Random 16.0 Read"))
                {
                    ParseDiskLine(line, results);
                }
                else if (line.TrimStart().StartsWith("> CPU LZW Compression") ||
                         line.TrimStart().StartsWith("> CPU AES256 Encryption") ||
                         line.TrimStart().StartsWith("> CPU Vista Compression") ||
                         line.TrimStart().StartsWith("> CPU SHA1 Hash") ||
                         line.TrimStart().StartsWith("> Uniproc CPU LZW Compression") ||
                         line.TrimStart().StartsWith("> Uniproc CPU AES256 Encryption") ||
                         line.TrimStart().StartsWith("> Uniproc CPU Vista Compression") ||
                         line.TrimStart().StartsWith("> Uniproc CPU SHA1 Hash") ||
                         line.TrimStart().StartsWith("> Memory Performance") ||
                         line.TrimStart().StartsWith("> Direct3D Batch Performance") ||
                         line.TrimStart().StartsWith("> Direct3D Alpha Blend Performance") ||
                         line.TrimStart().StartsWith("> Direct3D ALU Performance") ||
                         line.TrimStart().StartsWith("> Direct3D Texture Load Performance") ||
                         line.TrimStart().StartsWith("> Direct3D Geometry Performance") ||
                         line.TrimStart().StartsWith("> Direct3D Constant Buffer Performance") ||
                         line.TrimStart().StartsWith("> Dshow Video Encode Time") ||
                         line.TrimStart().StartsWith("> Dshow Video Decode Time") ||
                         line.TrimStart().StartsWith("> Media Foundation Decode Time") ||
                         line.TrimStart().StartsWith("> Video Memory Throughput"))
                {
                    ParseCpuLine(line, results);
                }
            }

            UpdateProgressBars(results);
        }

        private void ParseDiskLine(string line, Dictionary<string, float> results)
        {
            // MessageBox.Show($"Parsing disk line: {line}"); // Debug
            string[] parts = line.TrimStart('>').Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 6) // Expected format: Disk Sequential 64.0 Read [value] MB/s [other numbers]
            {
                float value;
                if (float.TryParse(parts[4], out value))
                {
                    string key = string.Join(" ", parts.Take(4));
                    results[key] = value;
                    // MessageBox.Show($"Parsed disk value: {key} = {value}"); // Debug
                }
                else
                {
                    MessageBox.Show($"Failed to parse disk value: {line}"); // Debug
                }
            }
            else
            {
                return;
                //MessageBox.Show($"Unexpected disk line format: {line}"); // Debug
            }
        }

        private void ParseCpuLine(string line, Dictionary<string, float> results)
        {
            // MessageBox.Show($"Parsing CPU line: {line}"); // Debug
            string[] parts = line.TrimStart('>').Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 4)  // Expected format: [CPU type] [value] MB/s
            {
                float value;
                if (float.TryParse(parts[parts.Length - 2], out value))
                {
                    string key = string.Join(" ", parts.Take(parts.Length - 2));
                    results[key] = value;
                    //MessageBox.Show($"Parsed CPU value: {key} = {value}"); // Debug
                }
                else
                {
                    MessageBox.Show($"Failed to parse CPU value: {line}");
                }
            }
            else
            {
                return;
                //MessageBox.Show($"Unexpected CPU line format: {line}"); // Debug
            }
        }

        private void UpdateProgressBars(Dictionary<string, float> results)
        {
            UpdateProgressBar(results, "CPU LZW Compression", customProgressBarCPULZWCompression, labelCPULZWCompression);
            UpdateProgressBar(results, "CPU AES256 Encryption", customProgressBarCPUAES256Encryption, labelCPUAES256Encryption);
            UpdateProgressBar(results, "CPU Vista Compression", customProgressBarCPUVistaCompression, labelCPUVistaCompression);
            UpdateProgressBar(results, "CPU SHA1 Hash", customProgressBarCPUSHA1Hash, labelCPUSHA1Hash);
            UpdateProgressBar(results, "Uniproc CPU LZW Compression", customProgressBarUniprocCPULZWCompression, labelUniprocCPULZWCompression);
            UpdateProgressBar(results, "Uniproc CPU AES256 Encryption", customProgressBarUniprocCPUAES256Encryption, labelUniprocCPUAES256Encryption);
            UpdateProgressBar(results, "Uniproc CPU Vista Compression", customProgressBarUniprocCPUVistaCompression, labelUniprocCPUVistaCompression);
            UpdateProgressBar(results, "Uniproc CPU SHA1 Hash", customProgressBarUniprocCPUSHA1Hash, labelUniprocCPUSHA1Hash);
            UpdateProgressBar(results, "Memory Performance", customProgressBarMemoryPerformance, labelMemoryPerformance);
            UpdateProgressBar(results, "Disk Sequential 64.0 Read", customProgressBarDiskSequentialRead, labelDiskSequentialRead);
            UpdateProgressBar(results, "Disk Random 16.0 Read", customProgressBarDiskRandomRead, labelDiskRandomRead);
            UpdateProgressBar(results, "Direct3D Batch Performance", customProgressBarDirect3DBatchPerformance, labelDirect3DBatchPerformance);
            UpdateProgressBar(results, "Direct3D Alpha Blend Performance", customProgressBarDirect3DAlphaBlendPerformance, labelDirect3DAlphaBlendPerformance);
            UpdateProgressBar(results, "Direct3D ALU Performance", customProgressBar6Direct3DALUPerformance, labelcustomProgressBarDirect3DALUPerformance);
            UpdateProgressBar(results, "Direct3D Texture Load Performance", customProgressBarDirect3DTextureLoadPerformance, labelDirect3DTextureLoadPerformance);
            UpdateProgressBar(results, "Direct3D Geometry Performance", customProgressBarDirect3DGeometryPerformance, labelDirect3DGeometryPerformance);
            UpdateProgressBar(results, "Direct3D Constant Buffer Performance", customProgressBarDirect3DConstantBufferPerformance, labelDirect3DConstantBufferPerformance);
            UpdateProgressBar(results, "Video Memory Throughput", customProgressBarVideoMemoryThroughput, labelVideoMemoryThroughput);
            UpdateProgressBar(results, "Dshow Video Encode Time", customProgressBarDshowVideoEncodeTime, labelDshowVideoEncodeTime);
            UpdateProgressBar(results, "Dshow Video Decode Time", customProgressBarDshowVideoDecodeTime, labelDshowVideoDecodeTime);
            UpdateProgressBar(results, "Media Foundation Decode Time", customProgressBarMediaFoundationDecodeTime, labelMediaFoundationDecodeTime);
        }

        private void UpdateProgressBar(Dictionary<string, float> results, string key, CustomProgressBar progressBar, Label label)
        {
            if (results.ContainsKey(key))
            {
                float value = results[key];
                // Divide by 1024 for Memory Performance only
                if (key == "Memory Performance" || key == "Video Memory Throughput" || key == "CPU AES256 Encryption")
                {
                    value /= 1024f;
                }
                if (key == "Dshow Video Encode Time" || key == "Dshow Video Decode Time" || key == "Media Foundation Decode Time")
                {
                    label.Text = " Null";
                }
                progressBar.Value = (int)Math.Min(value, progressBar.MaxValue);
                progressBar.BarText = key;
                label.Text = $"{value:F2}" + label.Text;
                progressBar.Invalidate();
                // MessageBox.Show($"Updated progress bar for {key}: {value}"); // Debug
            }
            else
            {
                progressBar.Value = 0;
                progressBar.BarText = key;
                label.Text = "N/A";

                if (!hasShownNoDataMessage)
                {
                    MessageBox.Show("No values found. Please run the Benchmark first, before proceeding with result details.", "Incomplete Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hasShownNoDataMessage = true;
                }
            }
        }

        #region All Panels to clean the View 
        private void roundedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void roundedPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void roundedPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void roundedPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        #region All Custom Bars to clean the View 
        private void customProgressBarCPULZWCompression_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarCPUAES256Encryption_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarCPUVistaCompression_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarCPUSHA1Hash_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarUniprocCPULZWCompression_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarUniprocCPUAES256Encryption_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarUniprocCPUVistaCompression_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarUniprocCPUSHA1Hash_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDiskSequentialRead_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDiskRandomRead_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDirect3DBatchPerformance_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDirect3DAlphaBlendPerformance_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBar6Direct3DALUPerformance_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDirect3DTextureLoadPerformance_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDirect3DGeometryPerformance_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDirect3DConstantBufferPerformance_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarVideoMemoryThroughput_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDshowVideoEncodeTime_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarDshowVideoDecodeTime_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarMediaFoundationDecodeTime_Click(object sender, EventArgs e)
        {

        }

        private void customProgressBarMemoryPerformance_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region All Labels to clean the View 
        public void labelTitleSubscoreDetails_Click(object sender, EventArgs e)
        {

        }
        public void labelCPU2_Click(object sender, EventArgs e)
        {

        }

        public void labelRam2_Click(object sender, EventArgs e)
        {

        }

        public void labelGPU2_Click(object sender, EventArgs e)
        {

        }

        public void labelSSD2_Click(object sender, EventArgs e)
        {

        }

        private void labelSeparatorSSD_Click(object sender, EventArgs e)
        {

        }

        private void labelSeparatorGPU_Click(object sender, EventArgs e)
        {

        }

        private void labelSeparatorRAM_Click(object sender, EventArgs e)
        {

        }

        private void labelSeparatorCPU_Click(object sender, EventArgs e)
        {

        }
        private void labelCPULZWCompression_Click(object sender, EventArgs e)
        {

        }

        private void labelCPUAES256Encryption_Click(object sender, EventArgs e)
        {

        }

        private void labelCPUVistaCompression_Click(object sender, EventArgs e)
        {

        }

        private void labelCPUSHA1Hash_Click(object sender, EventArgs e)
        {

        }

        private void labelUniprocCPULZWCompression_Click(object sender, EventArgs e)
        {

        }

        private void labelUniprocCPUAES256Encryption_Click(object sender, EventArgs e)
        {

        }

        private void labelUniprocCPUVistaCompression_Click(object sender, EventArgs e)
        {

        }

        private void labelUniprocCPUSHA1Hash_Click(object sender, EventArgs e)
        {

        }

        private void labelDiskSequentialRead_Click(object sender, EventArgs e)
        {

        }

        private void labelDiskRandomRead_Click(object sender, EventArgs e)
        {

        }

        private void labelDirect3DBatchPerformance_Click(object sender, EventArgs e)
        {

        }

        private void labelDirect3DAlphaBlendPerformance_Click(object sender, EventArgs e)
        {

        }

        private void labelcustomProgressBarDirect3DALUPerformance_Click(object sender, EventArgs e)
        {

        }

        private void labelDirect3DTextureLoadPerformance_Click(object sender, EventArgs e)
        {

        }

        private void labelDirect3DGeometryPerformance_Click(object sender, EventArgs e)
        {

        }

        private void labelDirect3DConstantBufferPerformance_Click(object sender, EventArgs e)
        {

        }

        private void labelVideoMemoryThroughput_Click(object sender, EventArgs e)
        {

        }

        private void labelDshowVideoEncodeTime_Click(object sender, EventArgs e)
        {

        }

        private void labelDshowVideoDecodeTime_Click(object sender, EventArgs e)
        {

        }

        private void labelMediaFoundationDecodeTime_Click(object sender, EventArgs e)
        {

        }

        private void labelMemoryPerformance_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void pictureBoxCamera2_Click(object sender, EventArgs e)
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
    }
}
