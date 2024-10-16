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

using ManagedNativeWifi;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk.FormWifi
{
    public partial class FormWifi : Form
    {
        private System.Windows.Forms.Timer wifiUpdateTimer;
        private WifiNetwork selectedNetwork;
        private List<string> savedNetworks;
        private string selectedSSID; // New field to store the selected network SSID for visual effect
        private string password;
        public FormWifi(Form1 from, System.Windows.Forms.Timer timer)
        {
            InitializeComponent();
            savedNetworks = new List<string>();
            selectedSSID = null; // Initialize to null
            password = string.Empty; // Initialize the password as an empty string

        }

        private void FormWifi_Load(object sender, EventArgs e)
        {

            wifiUpdateTimer = new System.Windows.Forms.Timer();
            wifiUpdateTimer.Interval = 10000;
            wifiUpdateTimer.Tick += WifiUpdateTimer_Tick;
            wifiUpdateTimer.Start();

            LoadSavedNetworks();
            LoadWifiNetworks();
            CodeForWiFiOnOffCheck.IsWiFiOn(controlWiFi, DependencyContainer.MessagesBoxImplementation);

        }

        private void WifiUpdateTimer_Tick(object sender, EventArgs e)
        {
            LoadWifiNetworks();
        }

        private void LoadWifiNetworks()
        {
            var networks = GetAvailableNetworks();
            if (networks.Any())
            {
                DisplayWifiNetworks(networks);
            }
            else
            {
                UpdateStatusMessage("No Wi-Fi networks available.");
                //DependencyContainer.MessagesBoxImplementation.ShowMessage($"No Wi-Fi network available.", "Info", MessageBoxButtons.OK);
            }
        }

        private void LoadSavedNetworks()
        {
            savedNetworks.Clear();
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "netsh";
                process.StartInfo.Arguments = "wlan show profiles";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Parse the output to get saved network names
                string[] lines = output.Split('\n');
                foreach (string line in lines)
                {
                    if (line.Contains("All User Profile"))
                    {
                        string networkName = line.Split(':').Last().Trim();
                        savedNetworks.Add(networkName);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateStatusMessage($"Error retrieving saved networks: {ex.Message}");
                /*DependencyContainer.MessagesBoxImplementation.ShowMessage(
                    "Error retrieving saved networks: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK);*/
            }
        }

        private List<WifiNetwork> GetAvailableNetworks()
        {
            List<WifiNetwork> networks = new List<WifiNetwork>();
            Dictionary<string, WifiNetwork> uniqueNetworks = new Dictionary<string, WifiNetwork>();

            try
            {
                var availableNetworks = NativeWifi.EnumerateAvailableNetworks();

                foreach (var network in availableNetworks)
                {
                    string ssid = network.Ssid.ToString();
                    // Check for hidden networks and set appropriate name
                    if (string.IsNullOrWhiteSpace(ssid))
                    {
                        ssid = "(no name) Hidden Network";
                    }

                    int signalQuality = network.SignalQuality;
                    bool isSecure = network.IsSecurityEnabled;

                    // If the network already exists, update only if the signal is stronger
                    if (uniqueNetworks.ContainsKey(ssid))
                    {
                        if (signalQuality > uniqueNetworks[ssid].SignalQuality)
                        {
                            uniqueNetworks[ssid] = new WifiNetwork
                            {
                                SSID = ssid,
                                SignalQuality = signalQuality,
                                IsSecure = isSecure
                            };
                        }
                    }
                    else
                    {
                        uniqueNetworks[ssid] = new WifiNetwork
                        {
                            SSID = ssid,
                            SignalQuality = signalQuality,
                            IsSecure = isSecure
                        };
                    }
                }

                networks = uniqueNetworks.Values.ToList();
            }
            catch (Exception ex)
            {
                UpdateStatusMessage($"Error retrieving Wi-Fi networks: {ex.Message}");
            }

            return networks;
        }

        private void DisplayWifiNetworks(List<WifiNetwork> networks)
        {
            flowLayoutPanel1.Controls.Clear();

            foreach (var network in networks)
            {
                Panel panelNetwork = new Panel();
                panelNetwork.Width = flowLayoutPanel1.Width - 80;
                panelNetwork.Height = 80;
                panelNetwork.BorderStyle = BorderStyle.FixedSingle;
                panelNetwork.Padding = new Padding(10);
                panelNetwork.Tag = network;
                panelNetwork.Click += PanelNetwork_Click;

                // Set the background color based on whether this is the selected network
                if (selectedSSID != null && network.SSID == selectedSSID)
                {
                    panelNetwork.BackColor = Color.DarkGray;
                    // Update the selectedNetwork reference
                    selectedNetwork = network;
                }
                else
                {
                    panelNetwork.BackColor = Color.Transparent;
                }

                Label labelSSID = new Label();
                labelSSID.Text = network.SSID;
                labelSSID.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                labelSSID.ForeColor = Color.White;
                labelSSID.AutoSize = true;

                PictureBox pictureBoxSignal = new PictureBox();
                pictureBoxSignal.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxSignal.Width = 42;
                pictureBoxSignal.Height = 33;

                bool isNetworkSaved = savedNetworks.Contains(network.SSID);
                pictureBoxSignal.Image = GetSignalIcon(network.SignalQuality, network.IsSecure && !isNetworkSaved);

                panelNetwork.Controls.Add(labelSSID);
                labelSSID.Location = new Point(10, 22);
                panelNetwork.Controls.Add(pictureBoxSignal);
                pictureBoxSignal.Location = new Point(panelNetwork.Width - 80, 22);

                flowLayoutPanel1.Controls.Add(panelNetwork);
            }

            // Ensure the password textbox visibility is correct after refresh
            if (selectedNetwork != null)
            {
                bool isNetworkSaved = savedNetworks.Contains(selectedNetwork.SSID);
                textBoxPassWiFi.Visible = selectedNetwork.IsSecure && !isNetworkSaved;
                pictureBoxShowPassword.Visible = selectedNetwork.IsSecure && !isNetworkSaved;
            }
        }

        private Image GetSignalIcon(int signalQuality, bool needsPassword)
        {
            if (needsPassword)
            {
                // Icons with lock
                if (signalQuality > 75)
                    return Properties.Resources.SignalStrongLock;
                else if (signalQuality > 50)
                    return Properties.Resources.SignalGoodLock;
                else if (signalQuality > 25)
                    return Properties.Resources.SignalWeakLock;
                else
                    return Properties.Resources.SignalVeryWeakLock;
            }
            else
            {
                // Regular icons without lock
                if (signalQuality > 75)
                    return Properties.Resources.SignalStrong;
                else if (signalQuality > 50)
                    return Properties.Resources.SignalGood;
                else if (signalQuality > 25)
                    return Properties.Resources.SignalWeak;
                else
                    return Properties.Resources.SignalVeryWeak;
            }
        }

        private void HighlightSelectedNetwork()
        {
            // Reset all panels to transparent
            foreach (Panel panel in flowLayoutPanel1.Controls)
            {
                panel.BackColor = Color.Transparent;

                // If this panel contains the selected network, highlight it
                if (panel.Tag is WifiNetwork network && network.SSID == selectedSSID)
                {
                    panel.BackColor = Color.DarkGray;
                }
            }
        }

        private void PanelNetwork_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;
            if (clickedPanel != null)
            {
                selectedNetwork = clickedPanel.Tag as WifiNetwork;
                selectedSSID = selectedNetwork.SSID;

                // Highlight the selected panel
                HighlightSelectedNetwork();

                // Show password textbox only if network is protected AND not saved
                bool isNetworkSaved = savedNetworks.Contains(selectedNetwork.SSID);
                textBoxPassWiFi.Visible = selectedNetwork.IsSecure && !isNetworkSaved;
                pictureBoxShowPassword.Visible = selectedNetwork.IsSecure && !isNetworkSaved;

                // If the network is saved, clear the password field
                if (isNetworkSaved)
                {
                    textBoxPassWiFi.Text = string.Empty;
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async Task<string> CreateSimplifiedWifiProfileXmlAsync(string ssid, string password)
        {
            // xml specs file
            return $@"<?xml version=""1.0""?>
<WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">
    <name>{ssid}</name>
    <SSIDConfig>
        <SSID>
            <name>{ssid}</name>
        </SSID>
    </SSIDConfig>
    <connectionType>ESS</connectionType>
    <connectionMode>manual</connectionMode>
    <MSM>
        <security>
            <authEncryption>
                <authentication>WPA2PSK</authentication>
                <encryption>AES</encryption>
                <useOneX>false</useOneX>
            </authEncryption>
            <sharedKey>
                <keyType>passPhrase</keyType>
                <protected>false</protected>
                <keyMaterial>{password}</keyMaterial>
            </sharedKey>
        </security>
    </MSM>
</WLANProfile>";
        }

        private async Task<string> ExecuteCommandAsync(string command)
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = System.Text.Encoding.UTF8,
                    StandardErrorEncoding = System.Text.Encoding.UTF8
                }
            };

            var output = new StringBuilder();
            var error = new StringBuilder();

            output.AppendLine($"Executing command: {command}");

            process.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    output.AppendLine($"Output: {e.Data}");
                }
            };

            process.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    error.AppendLine($"Error: {e.Data}");
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            await process.WaitForExitAsync();

            if (error.Length > 0)
            {
                output.AppendLine("Errors encountered:");
                output.Append(error.ToString());
            }

            return output.ToString();
        }

        private async void buttonConnectWiFi_Click(object sender, EventArgs e)
        {
            if (selectedNetwork == null)
            {
                UpdateStatusMessage("Please select a network first");
                return;
            }

            // Disable the button while connecting
            buttonConnectWiFi.Enabled = false;
            try
            {
                // Use the global password variable
                await ConnectToNetworkAsync(selectedNetwork.SSID, password);
            }
            finally
            {
                buttonConnectWiFi.Enabled = true;
            }
        }

        private async Task ConnectToNetworkAsync(string ssid, string password)
        {
            UpdateStatusMessage($"Starting connection to {ssid}...");

            try
            {
                bool isNetworkSaved = savedNetworks.Contains(ssid);
                UpdateStatusMessage($"Network saved status: {isNetworkSaved}, Password length: {password?.Length ?? 0}");

                // Disconnect from the current network
                UpdateStatusMessage("Disconnecting from current network...");
                string disconnectResult = await ExecuteCommandAsync("netsh wlan disconnect");
                UpdateStatusMessage($"Disconnect result: {disconnectResult}");
                await Task.Delay(2000);

                bool profileCreated = false;
                if (!isNetworkSaved)
                {
                    if (string.IsNullOrEmpty(password))
                    {
                        UpdateStatusMessage("Password is required for unsaved networks");
                        return;
                    }

                    //UpdateStatusMessage($"Creating profile for network: {ssid}");
                    // Log dei dettagli del profilo (senza la password)
                    //UpdateStatusMessage($"Profile will be saved to: {Path.GetTempPath()}");
                }

                if (!isNetworkSaved && !string.IsNullOrEmpty(password))
                {
                    try
                    {
                        // Remove existing profile if present
                        //UpdateStatusMessage("Removing old network profile if exists...");
                        await ExecuteCommandAsync($"netsh wlan delete profile name=\"{ssid}\"");
                        await Task.Delay(1000);

                        // Create new XML profile
                        //UpdateStatusMessage("Creating new network profile XML...");
                        string profileXml = await CreateSimplifiedWifiProfileXmlAsync(ssid, password);

                        // Save the profile to a temporary file
                        string tempFile = Path.Combine(Path.GetTempPath(), $"wifiprofile_{DateTime.Now.Ticks}.xml");
                        //UpdateStatusMessage($"Creating profile at: {tempFile}");

                        await File.WriteAllTextAsync(tempFile, profileXml);

                        // Verify that the file exists
                        if (!File.Exists(tempFile))
                        {
                            throw new Exception("Profile file was not created");
                        }

                        // Add profile
                        string addProfileResult = await ExecuteCommandAsync($"netsh wlan add profile filename=\"{tempFile}\" user=current");
                        //UpdateStatusMessage($"Add profile result: {addProfileResult}");

                        if (addProfileResult.Contains("error", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new Exception($"Failed to add profile: {addProfileResult}");
                        }

                        // Verify that the profile has been added
                        string verifyProfile = await ExecuteCommandAsync("netsh wlan show profiles");
                        if (!verifyProfile.Contains(ssid))
                        {
                            throw new Exception("Profile was not added successfully");
                        }

                        profileCreated = true;
                        //UpdateStatusMessage("Profile created successfully");

                        // Clean up the temporary file
                        try { File.Delete(tempFile); } catch { }
                    }
                    catch (Exception ex)
                    {
                        //UpdateStatusMessage($"Failed to create network profile: {ex.Message}");
                        return; // Important: Stop here if profile creation fails
                    }
                }

                // Proceed with the connection only if:
                // 1. The network is already saved, OR
                // 2. We have just created the profile successfully
                if (isNetworkSaved || profileCreated)
                {
                    string interfaceName = await GetWifiInterfaceNameAsync();
                    UpdateStatusMessage($"Using interface: {interfaceName}");

                    // Verify that the network is in range
                    string availableNetworks = await ExecuteCommandAsync("netsh wlan show networks");
                    if (!availableNetworks.Contains(ssid))
                    {
                        UpdateStatusMessage($"Error: Network {ssid} is not in range!");
                        return;
                    }

                    // Try connecting
                    UpdateStatusMessage($"Connecting to {ssid}...");
                    string connectResult = await ExecuteCommandAsync($"netsh wlan connect name=\"{ssid}\" interface=\"{interfaceName}\"");

                    if (connectResult.Contains("failed", StringComparison.OrdinalIgnoreCase) ||
                        connectResult.Contains("error", StringComparison.OrdinalIgnoreCase))
                    {
                        //UpdateStatusMessage($"Connection command failed: {connectResult}");
                        return;
                    }

                    // Check the connection
                    const int MAX_ATTEMPTS = 10;
                    for (int attempt = 1; attempt <= MAX_ATTEMPTS; attempt++)
                    {
                        string interfaceStatus = await ExecuteCommandAsync("netsh wlan show interfaces");
                        if (interfaceStatus.Contains(ssid, StringComparison.OrdinalIgnoreCase) &&
                            interfaceStatus.Contains("connected", StringComparison.OrdinalIgnoreCase))
                        {
                            UpdateStatusMessage($"Successfully connected to {ssid}");
                            LoadSavedNetworks();
                            return;
                        }
                        UpdateStatusMessage($"Waiting for connection (attempt {attempt}/{MAX_ATTEMPTS})...");
                        await Task.Delay(1000);
                    }

                    UpdateStatusMessage("Connection timeout. Please try again.");
                }
                else
                {
                    //UpdateStatusMessage("Cannot connect: No saved profile and profile creation failed");
                }
            }
            catch (Exception ex)
            {
                UpdateStatusMessage($"Connection error: {ex.Message}");
            }
        }
        private async Task<string> GetWifiInterfaceNameAsync()
        {
            var output = await ExecuteCommandAsync("netsh wlan show interfaces");
            foreach (string line in output.Split('\n'))
            {
                if (line.Contains("Name") && !line.Contains("Hosted"))
                {
                    return line.Split(':').LastOrDefault()?.Trim() ?? "WiFi";
                }
            }
            return "WiFi";
        }

        private void pictureBoxBackArrow_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void controlWiFi_Click(object sender, EventArgs e)
        {
            CodeForWiFiOnOff.ToggleWiFi(controlWiFi, DependencyContainer.MessagesBoxImplementation);
            UpdateStatusMessage("WiFi on...");
            Thread.Sleep(1200);
            LoadWifiNetworks();
        }

        private void textBoxInfoConnection_TextChanged(object sender, EventArgs e)
        {

        }

        private void UpdateStatusMessage(string message)
        {
            if (textBoxInfoConnection.InvokeRequired)
            {
                textBoxInfoConnection.Invoke(new Action(() => UpdateStatusMessage(message)));
                return;
            }

            textBoxInfoConnection.Text = message;
            textBoxInfoConnection.Refresh();
            Application.DoEvents();
        }

        private void textBoxPassWiFi_TextChanged(object sender, EventArgs e)
        {
            password = textBoxPassWiFi.Text;
        }

        private void pictureBoxShowPassword_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxPassWiFi.UseSystemPasswordChar = false;
        }

        private void pictureBoxShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxPassWiFi.UseSystemPasswordChar = true;
        }
    }
}
