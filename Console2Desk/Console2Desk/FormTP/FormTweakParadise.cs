using Console2Desk.SettingsButton;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Console2Desk.FormTP
{
    public partial class FormTweakParadise : Form
    {
        private bool _isDragging = false;
        private Point _dragStartPoint;
        public FormTweakParadise(Form form, System.Windows.Forms.Timer timer)
        {
            InitializeComponent();

            panelTitleFormTP1.MouseDown += panelTitleFormTP1_MouseDown;
            panelTitleFormTP1.MouseMove += panelTitleFormTP1_MouseMove;
            panelTitleFormTP1.MouseUp += panelTitleFormTP1_MouseUp;
            labelTitleFormTP.MouseDown += labelTitleFormTP_MouseDown;
            labelTitleFormTP.MouseMove += labelTitleFormTP_MouseMove;
            labelTitleFormTP.MouseUp += labelTitleFormTP_MouseUp;
            panelBase.MouseDown += labelTitleFormTP_MouseDown;
            panelBase.MouseMove += labelTitleFormTP_MouseMove;
            panelBase.MouseUp += labelTitleFormTP_MouseUp;

        }

        private void FormTweakParadise_Load(object sender, EventArgs e)
        {
            CodeForAMD_NoShutterCheck.CheckEnableUlps(controlAMDnoShutter, DependencyContainer.MessagesBoxImplementation);
            GPUAMDSDILowLatencyCheck.CheckLowLatency(controlAMDgpuSDI, DependencyContainer.MessagesBoxImplementation);
            GPUINTELSDILowLatencyCheck.CheckLowLatency(controliNTELgpuSDI, DependencyContainer.MessagesBoxImplementation);
            GPUNVIDIASDILowLatencyCheck.CheckLowLatency(controlNVIDIAgpuSDI, DependencyContainer.MessagesBoxImplementation);


            CodeForMeltdownSpectreProtectionCheck.CheckMeltdownSpectreProtection(controlMeltdownSpectreProtectionOnOff, DependencyContainer.MessagesBoxImplementation);
            CodeForControlCoreIsolation_ExploidCheck.CheckCoreIsolationAndExploitProtection(controlCoreIsolation_Exploid, DependencyContainer.MessagesBoxImplementation);
            CodeForSystemDevicesCheck.CheckSystemDevices(controlSystemDevices, DependencyContainer.MessagesBoxImplementation);
            CodeForControlBCDMemoryUsageCheck.CheckBCDMemoryUsage(controlBCDMemoryUsage, DependencyContainer.MessagesBoxImplementation);
            CodeForReduceWindowsLatencyCheck.CheckReduceWindowsLatency(controlReduceWindowsLatency, DependencyContainer.MessagesBoxImplementation);
            CodeForReduceNetworkLatencyCheck.CheckReduceNetworkLatency(controlReduceNetworkLatency, DependencyContainer.MessagesBoxImplementation);
            CodeForUACCheck.CheckUACStatus(uacToggleSwitch, DependencyContainer.MessagesBoxImplementation);
            CodeForMSGamebarMessagesCheck.CheckGamebarMessagesState(controlGameBarMessages, DependencyContainer.MessagesBoxImplementation);
            HdwAcceleratedGPUSchedulingCheck.CheckHdwAcceleratedGPUSchedulingStatus(controlHWAccelleration, DependencyContainer.MessagesBoxImplementation);
            VRRO_OptWindowedGames_Check.CheckVRRO_OptWindowedGamesStatus(controlVRRO_OptWindowedGames, DependencyContainer.MessagesBoxImplementation);
            RDPOptimization_Check.CheckRDPOptimizationStatus(controlRDPOptimization, DependencyContainer.MessagesBoxImplementation);
            WDDMdriverAMD_Check.CheckWDDMdriverAMDStatus(controlWDDMamd, DependencyContainer.MessagesBoxImplementation);
            WDDMdriverNVIDIA_Check.CheckWDDMdriverNVIDIAStatus(controlWDDMnvidia, DependencyContainer.MessagesBoxImplementation);

        }

        private void buttonCloseFormTP_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void buttonFullScreen_Click(object sender, EventArgs e)
        {
            // Controlla se la finestra è attualmente a tutto schermo
            if (this.WindowState == FormWindowState.Normal)
            {
                // Passa a tutto schermo
                this.FormBorderStyle = FormBorderStyle.None; // Rimuove il bordo della finestra
                this.WindowState = FormWindowState.Maximized; // Massimizza la finestra
            }
            else
            {
                // Torna alla finestra normale
                this.FormBorderStyle = FormBorderStyle.Sizable; // Ripristina il bordo della finestra
                this.WindowState = FormWindowState.Normal; // Ripristina la finestra alla modalità normale
            }
        }

        private void panelTitleFormTP1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelTitleFormTP1_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging Window
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void panelTitleFormTP1_MouseMove(object sender, MouseEventArgs e)
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

        private void panelTitleFormTP1_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging Window
            _isDragging = false;
        }

        private void labelTitleFormTP_Click(object sender, EventArgs e)
        {

        }
        private void labelTitleFormTP_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging Window
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void labelTitleFormTP_MouseMove(object sender, MouseEventArgs e)
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

        private void labelTitleFormTP_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging Window
            _isDragging = false;
        }

        private void panelBase_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelBase_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging Window
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void panelBase_MouseMove(object sender, MouseEventArgs e)
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

        private void panelBase_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging Window
            _isDragging = false;
        }

        private void controlAMDnoShutter_Click(object sender, EventArgs e)
        {
            CodeForAMD_NoShutter.ToggleEnableUlps(controlAMDnoShutter, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlMeltdownSpectreProtectionOnOff_Click(object sender, EventArgs e)
        {
            CodeForMeltdownSpectreProtection.ToggleMeltdownSpectreProtection(controlMeltdownSpectreProtectionOnOff, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlSystemDevices_Click(object sender, EventArgs e)
        {
            CodeForSystemDevices.ToggleSystemDevicesAsync(controlSystemDevices, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlCoreIsolation_Exploid_Click(object sender, EventArgs e)
        {
            CodeForControlCoreIsolation_Exploid.ToggleCoreIsolationAndExploitProtection(controlCoreIsolation_Exploid, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlReduceNetworkLatency_Click(object sender, EventArgs e)
        {
            CodeForReduceNetworkLatency.ToggleReduceNetworkLatency(controlReduceNetworkLatency, DependencyContainer.MessagesBoxImplementation);

            // Show a message to restart the system
            var result = DependencyContainer.MessagesBoxImplementation.ShowMessage(
                "Changes will take effect after a system restart.\n\n" +
                "Please note that this procedure improves latency in online competitive games. However, the improvements apply only to the current network you are connected to. If you connect to a different network, you will need to enable this switch again for each new network. To disable the improvement for a previously activated network, you need to reconnect to that network (keeping the same IP) and then switch it off.\n\n" +
                "Do you want to restart now?",
                "Information",
                MessageBoxButtons.YesNo);


            if (result == DialogResult.Yes)
            {
                // Restart the system
                Process.Start("shutdown.exe", "/r /t 0");
            }
        }

        private void controlReduceWindowsLatency_Click(object sender, EventArgs e)
        {
            CodeForReduceWindowsLatency.ToggleReduceWindowsLatency(controlReduceWindowsLatency, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlBCDMemoryUsage_Click(object sender, EventArgs e)
        {
            CodeForControlBCDMemoryUsage.ToggleBCDMemoryUsage(controlBCDMemoryUsage, DependencyContainer.MessagesBoxImplementation);
        }

        private void uacToggleSwitch_Click(object sender, EventArgs e)
        {
            CodeForUAC.ToggleUAC(uacToggleSwitch, DependencyContainer.MessagesBoxImplementation);

            // Notify the user that the system needs to restart
            var result = DependencyContainer.MessagesBoxImplementation.ShowMessage(
                "Disabling User Account Control (UAC) will stop showing prompts for administrator privileges. Any software will run with administrative privileges without asking for confirmation. "
                + "Changes will take effect after a system restart. Do you want to restart now?", "Information",
                MessageBoxButtons.YesNo);


            if (result == DialogResult.Yes)
            {
                // Restart the system
                Process.Start("shutdown.exe", "/r /t 0");
            }
        }
        private void controlGameBarMessages_Click(object sender, EventArgs e)
        {
            CodeForMSGamebarMessages.ToggleGamebarMessages(controlGameBarMessages, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlAMDgpuSDI_Click(object sender, EventArgs e)
        {
            GPUAMDSDILowLatency.ToggleLowLatency(controlAMDgpuSDI, DependencyContainer.MessagesBoxImplementation);

        }

        private void controlNVIDIAgpuSDI_Click(object sender, EventArgs e)
        {
            GPUNVIDIASDILowLatency.ToggleLowLatency(controlNVIDIAgpuSDI, DependencyContainer.MessagesBoxImplementation);
        }

        private void controliNTELgpuSDI_Click(object sender, EventArgs e)
        {
            GPUINTELSDILowLatency.ToggleLowLatency(controliNTELgpuSDI, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlHWAccelleration_Click(object sender, EventArgs e)
        {
            HdwAcceleratedGPUScheduling.ToggleHwAcceleratedGPUScheduling(controlHWAccelleration, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlVRRO_OptWindowedGames_Click(object sender, EventArgs e)
        {
            VRRO_OptWindowedGames.ApplyVRRO_OptWindowedGamesToggle(controlVRRO_OptWindowedGames, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlRDPOptimization_Click(object sender, EventArgs e)
        {
            RDPOptimization.ApplyRDPOptimizationToggle(controlRDPOptimization, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlWDDMamd_Click(object sender, EventArgs e)
        {
            WDDMdriverAMD.ToggleWDDMdriverAMD(controlWDDMamd, DependencyContainer.MessagesBoxImplementation);
        }

        private void controlWDDMnvidia_Click(object sender, EventArgs e)
        {
            WDDMdriverNVIDIA.ToggleWDDMdriverNVIDIA(controlWDDMnvidia, DependencyContainer.MessagesBoxImplementation);
        }
    }
}
