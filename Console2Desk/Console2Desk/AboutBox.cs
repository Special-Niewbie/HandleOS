using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Console2Desk
{
    public partial class AboutBox : Form
    {
        private WinTheme winTheme;

        private Form _form;
        private System.Windows.Forms.Timer _timer;

        public AboutBox(Form form, System.Windows.Forms.Timer timer)
        {
            InitializeComponent();
            _form = form;
            _timer = timer;
            

            WinTheme winTheme = new WinTheme(this.Handle);
            winTheme.ApplyTheme();
            this.MaximizeBox = false;

            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.SelectionLength = 0;

            pictureBox2.MouseDown += pictureBox2_MouseDown;

            this.Shown += AboutBox_Shown;
            this.FormClosed += AboutBox_FormClosed;
        }
        private void AboutBox_Load(object sender, EventArgs e)
        {
            string versionFilePath = Path.Combine(Application.StartupPath, "version");
            if (File.Exists(versionFilePath))
            {
                string version = File.ReadAllText(versionFilePath).Trim();
                labelVersionDescription.Text = $"Version: {version}";
            }
            else
            {
                labelVersionDescription.Text = "Version: Unknown";
            }
        }

        private void AboutBox_Shown(object sender, EventArgs e)
        {
            PauseTimer(); // Pause the timer when the AboutBox is shown
        }

        private void AboutBox_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResumeTimer(); // Resume the timer when the AboutBox is closed
        }

        private void PauseTimer()
        {
            _timer?.Stop();
        }

        private void ResumeTimer()
        {
            _timer?.Start();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelVisitMySite_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Ensure the click is with the left mouse button
            {
                try
                {
                    // Create a process start info
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "cmd"; // Command Prompt
                    startInfo.Arguments = $"/c start https://github.com/Special-Niewbie"; // Command to open the URL
                    startInfo.CreateNoWindow = true; // Do not create a window for the process

                    // Start the process
                    Process.Start(startInfo);
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    DependencyContainer.MessagesBoxImplementation.ShowMessage("Unable to open the default browser. Make sure it's properly configured on your system.", "Error", MessageBoxButtons.OK);

                }
                catch (Exception ex)
                {
                    DependencyContainer.MessagesBoxImplementation.ShowMessage($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK);

                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelVersionDescription_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
