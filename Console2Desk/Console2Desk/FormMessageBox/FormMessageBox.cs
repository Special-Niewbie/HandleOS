using System.Management;
using System.ServiceProcess;
using System.Diagnostics;
using Microsoft.Win32;

namespace Console2Desk.FormMessageBox
{
    public partial class FormMessageBox : Form
    {
        private readonly List<string> _servicesToCheck = new List<string> { "WbioSrvc", "W32Time", "WlanSvc" }; // Servizi da controllare

        private bool _isDragging = false;
        private Point _dragStartPoint;

        public FormMessageBox(Form form, System.Windows.Forms.Timer timer)
        {
            InitializeComponent();

            panelTitleFormMessage.MouseDown += panelTitleFormMessage_MouseDown;
            panelTitleFormMessage.MouseMove += panelTitleFormMessage_MouseMove;
            panelTitleFormMessage.MouseUp += panelTitleFormMessage_MouseUp;
            labelTitleFormMessage.MouseDown += labelTitleFormMessage_MouseDown;
            labelTitleFormMessage.MouseMove += labelTitleFormMessage_MouseMove;
            labelTitleFormMessage.MouseUp += labelTitleFormMessage_MouseUp;
        }

        private void FormMessageBox_Load(object sender, EventArgs e)
        {
            // When the form loads, check the services
            CheckServicesStartupType();
        }

        private void buttonCloseWindowFormMessage_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckServicesStartupType()
        {
            foreach (var serviceName in _servicesToCheck)
            {
                var startupType = GetServiceStartupType(serviceName);

                // Check if the startup type is not "Manual"
                if (startupType != "Manual")
                {
                    listBoxFormMessage.Items.Add($"{serviceName} - {startupType}");
                }
            }
            // Check existence of 'edgeupdate' and 'edgeupdatem' services
            if (ServiceExists("edgeupdate"))
            {
                var edgeUpdateStartupType = GetServiceStartupType("edgeupdate");
                listBoxFormMessage.Items.Add($"edgeupdate - {edgeUpdateStartupType}");
            }

            if (ServiceExists("edgeupdatem"))
            {
                var edgeUpdateMStartupType = GetServiceStartupType("edgeupdatem");
                listBoxFormMessage.Items.Add($"edgeupdatem - {edgeUpdateMStartupType}");
            }

            // Check for the existence of Microsoft Edge and OneDrive folders
            string edgeUpdateFolder = @"C:\Program Files (x86)\Microsoft\EdgeUpdate";
            string oneDriveFolder = @"C:\Program Files (x86)\Microsoft OneDrive";

            if (Directory.Exists(edgeUpdateFolder))
            {
                listBoxFormMessage.Items.Add($"Microsoft EdgeUpdate folder found at: {edgeUpdateFolder}");
            }
            else
            {
                // listBoxFormMessage.Items.Add($"Microsoft EdgeUpdate folder not found");
            }

            if (Directory.Exists(oneDriveFolder))
            {
                listBoxFormMessage.Items.Add($"Microsoft OneDrive folder found at: {oneDriveFolder}");
            }
            else
            {
                // listBoxFormMessage.Items.Add($"Microsoft OneDrive folder not found");
            }
        }

        private string GetServiceStartupType(string serviceName)
        {
            try
            {
                // WMI query to get service startup type
                string query = $"SELECT StartMode FROM Win32_Service WHERE Name = '{serviceName}'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject service in searcher.Get())
                    {
                        return service["StartMode"].ToString(); // Returns the startup type
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Errore durante il controllo del servizio {serviceName}: {ex.Message}");
            }
            return string.Empty; // If there is an error or the service does not exist
        }



        private void checkBoxFormMessage_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBoxFormMessage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void special_Niewbie_ButtonStartRestore_Click(object sender, EventArgs e)
        {
            // Reset services to "Manual" startup type
            foreach (var serviceName in _servicesToCheck)
            {
                var startupType = GetServiceStartupType(serviceName);

                // If the service is not set to "Manual", change it
                if (startupType != "Manual")
                {
                    SetServiceStartupType(serviceName, "Manual");
                }
            }

            if (checkBoxFormMessage.Checked)
            {
                // Ignore the absence of services or folders and start the scripts anyway
                try
                {

                    ScriptExecutor.RunEdgeRemoveScript();

                    ScriptExecutor.RunOneDriveRemoveScript();

                    // Remove specific task schedulers
                    RemoveScheduledTask("MicrosoftEdgeUpdateTaskMachineCore");
                    RemoveScheduledTask("MicrosoftEdgeUpdateTaskMachineUA");
                    RemoveScheduledTask("OneDrive Per-Machine Standalone Update Task");

                    //MessageBox.Show("Gli script sono stati eseguiti correttamente.");
                }
                catch (Exception ex)
                {
                    // Log or handle the error, but continue execution
                    //MessageBox.Show($"Errore nell'esecuzione degli script: {ex.Message}");
                }
            }

            DependencyContainer.MessagesBoxImplementation.ShowMessage("Services have been restored.", "Information", MessageBoxButtons.OK);
            listBoxFormMessage.Items.Clear();
            CheckServicesStartupType(); // Reload the list of services
        }

        private void SetServiceStartupType(string serviceName, string startupType)
        {
            try
            {
                // Change service startup type via WMI
                string query = $"SELECT * FROM Win32_Service WHERE Name = '{serviceName}'";
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject service in searcher.Get())
                    {
                        ManagementBaseObject inParams = service.GetMethodParameters("ChangeStartMode");
                        inParams["StartMode"] = startupType; // Set the startup type

                        // Perform the change
                        service.InvokeMethod("ChangeStartMode", inParams, null);
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Errore durante la modifica del servizio {serviceName}: {ex.Message}");
            }
        }

        // Function to check if a service exists
        private bool ServiceExists(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                ServiceControllerStatus status = sc.Status;
                return true; // The service exists
            }
            catch (InvalidOperationException)
            {
                return false; // The service does not exist
            }
        }
        private void ExecuteScript(string scriptName)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = scriptName;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Errore durante l'esecuzione dello script {scriptName}: {ex.Message}");
            }
        }

        // Function to remove a scheduled task
        private void RemoveScheduledTask(string taskName)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "schtasks.exe";
                process.StartInfo.Arguments = $"/delete /tn \"{taskName}\" /f";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Errore durante la rimozione del task {taskName}: {ex.Message}");
            }
        }

        private void panelTitleFormMessage_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelTitleFormMessage_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging Window
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void panelTitleFormMessage_MouseMove(object sender, MouseEventArgs e)
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

        private void panelTitleFormMessage_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging Window
            _isDragging = false;
        }

        private void labelTitleFormMessage_Click(object sender, EventArgs e)
        {

        }
        private void labelTitleFormMessage_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging Window
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void labelTitleFormMessage_MouseMove(object sender, MouseEventArgs e)
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

        private void labelTitleFormMessage_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging Window
            _isDragging = false;
        }

        private void special_Niewbie_ButtonFixWifi_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Cambia il "Startup Type" del servizio "WlanSvc" in Automatic
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WlanSvc", true);
                    if (key != null)
                    {
                        key.SetValue("Start", 2); // 2 = Automatic, 3 = Manual, 4 = Disabled
                        key.Close();
                        //Console.WriteLine("Il tipo di avvio del servizio WlanSvc è stato impostato su Automatico.");
                    }
                    else
                    {
                        // Console.WriteLine("Impossibile trovare il servizio WlanSvc nel registro di sistema.");
                        return;
                    }

                    // Avvia il servizio "WlanSvc" se non è già in esecuzione
                    ServiceController wlanService = new ServiceController("WlanSvc");

                    if (wlanService.Status != ServiceControllerStatus.Running)
                    {
                        wlanService.Start();
                        wlanService.WaitForStatus(ServiceControllerStatus.Running);
                        // Console.WriteLine("Servizio WlanSvc avviato con successo.");
                    }
                    else
                    {
                        // Console.WriteLine("Il servizio WlanSvc è già in esecuzione.");
                    }
                    DependencyContainer.MessagesBoxImplementation.ShowMessage("WiFi Services have been restored.", "Information", MessageBoxButtons.OK);
                    CheckServicesStartupType();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Errore durante l'operazione: " + ex.Message);
                }
            }
        }
    }
}
