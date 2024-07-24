using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Console2Desk
{
    public partial class AboutBox : Form
    {
        private WinTheme winTheme;
        public AboutBox()
        {
            InitializeComponent();
            winTheme = new WinTheme(this.Handle);
            winTheme.ApplyTheme();
            this.MaximizeBox = false;

            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.SelectionLength = 0;

            pictureBox2.MouseDown += pictureBox2_MouseDown;

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
                    MessageBox.Show("Unable to open the default browser. Make sure it's properly configured on your system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelVersionDescription_Click(object sender, EventArgs e)
        {
            
        }
    }
}
