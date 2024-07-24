using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Console2Desk.TouchButtons
{
    public partial class ConsoleSettingsButton : Form
    {
        private WinTheme winTheme;
        private string settingsPath;
        public ConsoleSettingsButton()
        {
            InitializeComponent();
            winTheme = new WinTheme(this.Handle);
            winTheme.ApplyTheme();
            settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Console2Desk", "Settings.ini");
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsPath))
            {
                string[] settings = File.ReadAllLines(settingsPath);
                if (settings.Length > 0)
                {
                    switch (settings[0])
                    {
                        case "Playnite":
                            radioButtonPlaynite.Checked = true;
                            break;
                        case "Steam":
                            radioButtonSteam.Checked = true;
                            break;
                        case "Custom":
                            radioButtonCustomLauncher.Checked = true;
                            if (settings.Length > 1)
                            {
                                textBox1.Text = settings[1];
                            }
                            break;
                    }
                }
            }
        }

        private void radioButtonPlaynite_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPlaynite.Checked)
            {
                textBox1.Text = string.Empty; // Clear the custom path
            }
        }

        private void radioButtonSteam_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSteam.Checked)
            {
                textBox1.Text = string.Empty; // Clear the custom path
            }
        }

        private void radioButtonCustomLauncher_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCustomLauncher.Checked)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "All Files|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBox1.Text = openFileDialog.FileName;
                    }
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string selectedPath = string.Empty;
            string settingName = string.Empty;

            if (radioButtonPlaynite.Checked)
            {
                settingName = "Playnite";
                selectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Playnite", "Playnite.FullscreenApp.exe");
            }
            else if (radioButtonSteam.Checked)
            {
                settingName = "Steam";
                selectedPath = @"C:\Program Files (x86)\Steam\steam.exe";
            }
            else if (radioButtonCustomLauncher.Checked)
            {
                settingName = "Custom";
                selectedPath = textBox1.Text;
            }

            if (string.IsNullOrEmpty(selectedPath))
            {
                MessageBox.Show("Please select a valid option.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save settings to file
            Directory.CreateDirectory(Path.GetDirectoryName(settingsPath));
            File.WriteAllLines(settingsPath, new[] { settingName, selectedPath });

            // Update registry
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true))
                {
                    if (key != null)
                    {
                        key.SetValue("Shell", selectedPath);
                        MessageBox.Show("Settings applied successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Registry key not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error modifying registry key: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
