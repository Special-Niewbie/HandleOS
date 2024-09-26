namespace Console2Desk.FormTP
{
    partial class FormTweakParadise
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonCloseFormTP = new Button();
            panelTitleFormTP1 = new Panel();
            pictureBox1 = new PictureBox();
            labelTitleFormTP = new Label();
            pictureBoxAMDtp = new PictureBox();
            pictureBoxINTELtp = new PictureBox();
            pictureBoxNVIDIAtp = new PictureBox();
            pictureBoxMICROSOFTtp = new PictureBox();
            labelAMD_INTEL = new Label();
            label2 = new Label();
            panelBase = new Panel();
            controliNTELgpuSDI = new ToggleSwitchDev.ControlToggles();
            labeliNTELgpuSDI = new Label();
            pictureBoxINTELgpuSDI = new PictureBox();
            label3 = new Label();
            controlNVIDIAgpuSDI = new ToggleSwitchDev.ControlToggles();
            labelNVIDIAgpuSDI = new Label();
            pictureBoxNVIDIAgpuSDI = new PictureBox();
            controlAMDgpuSDI = new ToggleSwitchDev.ControlToggles();
            labelAMDgpuSDI = new Label();
            pictureBoxAMDgpuSDI = new PictureBox();
            controlGameBarMessages = new ToggleSwitchDev.ControlToggles();
            labelGameBarMessages = new Label();
            pictureBoxGameBarMessages = new PictureBox();
            uacToggleSwitch = new ToggleSwitchDev.ControlToggles();
            labelUAC = new Label();
            pictureBoxUAC = new PictureBox();
            labelReduceNetworkLatency = new Label();
            pictureBoxReduceNetworkLatency = new PictureBox();
            controlReduceNetworkLatency = new ToggleSwitchDev.ControlToggles();
            controlReduceWindowsLatency = new ToggleSwitchDev.ControlToggles();
            pictureBoxReduceWindowsLatency = new PictureBox();
            labelReduceWindowsLatency = new Label();
            controlBCDMemoryUsage = new ToggleSwitchDev.ControlToggles();
            labelBCDMemoryUsage = new Label();
            pictureBCDMemoryUsage = new PictureBox();
            labelCoreIsolation_CFG = new Label();
            pictureBoxCoreIsolation_CFG = new PictureBox();
            controlCoreIsolation_Exploid = new ToggleSwitchDev.ControlToggles();
            controlSystemDevices = new ToggleSwitchDev.ControlToggles();
            controlMeltdownSpectreProtectionOnOff = new ToggleSwitchDev.ControlToggles();
            labelSystemDevices = new Label();
            pictureBoxSystemDevices = new PictureBox();
            labelMeltdown_Spectre = new Label();
            pictureBoxMeltdown_Spectre = new PictureBox();
            controlAMDnoShutter = new ToggleSwitchDev.ControlToggles();
            labelAMDnoShutter = new Label();
            pictureBoxAMDnoShutter = new PictureBox();
            panelTitleFormTP1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAMDtp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxINTELtp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNVIDIAtp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMICROSOFTtp).BeginInit();
            panelBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxINTELgpuSDI).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNVIDIAgpuSDI).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAMDgpuSDI).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGameBarMessages).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUAC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxReduceNetworkLatency).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxReduceWindowsLatency).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBCDMemoryUsage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCoreIsolation_CFG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSystemDevices).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMeltdown_Spectre).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAMDnoShutter).BeginInit();
            SuspendLayout();
            // 
            // buttonCloseFormTP
            // 
            buttonCloseFormTP.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCloseFormTP.BackColor = Color.FromArgb(40, 40, 40);
            buttonCloseFormTP.FlatAppearance.BorderSize = 0;
            buttonCloseFormTP.FlatStyle = FlatStyle.Flat;
            buttonCloseFormTP.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCloseFormTP.ForeColor = Color.Red;
            buttonCloseFormTP.Location = new Point(1041, 0);
            buttonCloseFormTP.Name = "buttonCloseFormTP";
            buttonCloseFormTP.Size = new Size(61, 45);
            buttonCloseFormTP.TabIndex = 0;
            buttonCloseFormTP.Text = "X";
            buttonCloseFormTP.UseVisualStyleBackColor = false;
            buttonCloseFormTP.Click += buttonCloseFormTP_Click;
            // 
            // panelTitleFormTP1
            // 
            panelTitleFormTP1.BorderStyle = BorderStyle.FixedSingle;
            panelTitleFormTP1.Controls.Add(pictureBox1);
            panelTitleFormTP1.Controls.Add(labelTitleFormTP);
            panelTitleFormTP1.Controls.Add(buttonCloseFormTP);
            panelTitleFormTP1.Dock = DockStyle.Top;
            panelTitleFormTP1.Location = new Point(0, 0);
            panelTitleFormTP1.Name = "panelTitleFormTP1";
            panelTitleFormTP1.Size = new Size(1105, 46);
            panelTitleFormTP1.TabIndex = 1;
            panelTitleFormTP1.Paint += panelTitleFormTP1_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.TweakParadiseIconOpened;
            pictureBox1.Location = new Point(8, 9);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(42, 26);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // labelTitleFormTP
            // 
            labelTitleFormTP.Anchor = AnchorStyles.Top;
            labelTitleFormTP.AutoSize = true;
            labelTitleFormTP.Font = new Font("Roboto Medium", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitleFormTP.ForeColor = SystemColors.ControlLight;
            labelTitleFormTP.Location = new Point(433, 3);
            labelTitleFormTP.Name = "labelTitleFormTP";
            labelTitleFormTP.Size = new Size(239, 38);
            labelTitleFormTP.TabIndex = 1;
            labelTitleFormTP.Text = "Welcome to Tweak Paradise\r\n          Let's Boost FPS";
            labelTitleFormTP.Click += labelTitleFormTP_Click;
            // 
            // pictureBoxAMDtp
            // 
            pictureBoxAMDtp.Image = Properties.Resources.AMD;
            pictureBoxAMDtp.Location = new Point(96, 55);
            pictureBoxAMDtp.Name = "pictureBoxAMDtp";
            pictureBoxAMDtp.Size = new Size(60, 50);
            pictureBoxAMDtp.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxAMDtp.TabIndex = 5;
            pictureBoxAMDtp.TabStop = false;
            // 
            // pictureBoxINTELtp
            // 
            pictureBoxINTELtp.Anchor = AnchorStyles.Top;
            pictureBoxINTELtp.Image = Properties.Resources.INTEL;
            pictureBoxINTELtp.Location = new Point(378, 55);
            pictureBoxINTELtp.Name = "pictureBoxINTELtp";
            pictureBoxINTELtp.Size = new Size(60, 50);
            pictureBoxINTELtp.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxINTELtp.TabIndex = 6;
            pictureBoxINTELtp.TabStop = false;
            // 
            // pictureBoxNVIDIAtp
            // 
            pictureBoxNVIDIAtp.Anchor = AnchorStyles.Top;
            pictureBoxNVIDIAtp.Image = Properties.Resources.NVIDIA;
            pictureBoxNVIDIAtp.Location = new Point(662, 55);
            pictureBoxNVIDIAtp.Name = "pictureBoxNVIDIAtp";
            pictureBoxNVIDIAtp.Size = new Size(60, 50);
            pictureBoxNVIDIAtp.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxNVIDIAtp.TabIndex = 7;
            pictureBoxNVIDIAtp.TabStop = false;
            // 
            // pictureBoxMICROSOFTtp
            // 
            pictureBoxMICROSOFTtp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxMICROSOFTtp.Image = Properties.Resources.Microsoft;
            pictureBoxMICROSOFTtp.Location = new Point(940, 55);
            pictureBoxMICROSOFTtp.Name = "pictureBoxMICROSOFTtp";
            pictureBoxMICROSOFTtp.Size = new Size(60, 50);
            pictureBoxMICROSOFTtp.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxMICROSOFTtp.TabIndex = 8;
            pictureBoxMICROSOFTtp.TabStop = false;
            // 
            // labelAMD_INTEL
            // 
            labelAMD_INTEL.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            labelAMD_INTEL.BorderStyle = BorderStyle.Fixed3D;
            labelAMD_INTEL.Location = new Point(265, 69);
            labelAMD_INTEL.Name = "labelAMD_INTEL";
            labelAMD_INTEL.Size = new Size(3, 502);
            labelAMD_INTEL.TabIndex = 9;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            label2.BorderStyle = BorderStyle.Fixed3D;
            label2.Location = new Point(827, 69);
            label2.Name = "label2";
            label2.Size = new Size(3, 502);
            label2.TabIndex = 11;
            // 
            // panelBase
            // 
            panelBase.BorderStyle = BorderStyle.FixedSingle;
            panelBase.Controls.Add(controliNTELgpuSDI);
            panelBase.Controls.Add(labeliNTELgpuSDI);
            panelBase.Controls.Add(pictureBoxINTELgpuSDI);
            panelBase.Controls.Add(label3);
            panelBase.Controls.Add(controlNVIDIAgpuSDI);
            panelBase.Controls.Add(labelNVIDIAgpuSDI);
            panelBase.Controls.Add(pictureBoxNVIDIAgpuSDI);
            panelBase.Controls.Add(controlAMDgpuSDI);
            panelBase.Controls.Add(labelAMDgpuSDI);
            panelBase.Controls.Add(pictureBoxAMDgpuSDI);
            panelBase.Controls.Add(controlGameBarMessages);
            panelBase.Controls.Add(labelGameBarMessages);
            panelBase.Controls.Add(pictureBoxGameBarMessages);
            panelBase.Controls.Add(uacToggleSwitch);
            panelBase.Controls.Add(labelUAC);
            panelBase.Controls.Add(pictureBoxUAC);
            panelBase.Controls.Add(labelReduceNetworkLatency);
            panelBase.Controls.Add(pictureBoxReduceNetworkLatency);
            panelBase.Controls.Add(controlReduceNetworkLatency);
            panelBase.Controls.Add(controlReduceWindowsLatency);
            panelBase.Controls.Add(pictureBoxReduceWindowsLatency);
            panelBase.Controls.Add(labelReduceWindowsLatency);
            panelBase.Controls.Add(controlBCDMemoryUsage);
            panelBase.Controls.Add(labelBCDMemoryUsage);
            panelBase.Controls.Add(pictureBCDMemoryUsage);
            panelBase.Controls.Add(labelCoreIsolation_CFG);
            panelBase.Controls.Add(pictureBoxCoreIsolation_CFG);
            panelBase.Controls.Add(controlCoreIsolation_Exploid);
            panelBase.Controls.Add(controlSystemDevices);
            panelBase.Controls.Add(controlMeltdownSpectreProtectionOnOff);
            panelBase.Controls.Add(labelSystemDevices);
            panelBase.Controls.Add(pictureBoxSystemDevices);
            panelBase.Controls.Add(labelMeltdown_Spectre);
            panelBase.Controls.Add(pictureBoxMeltdown_Spectre);
            panelBase.Controls.Add(controlAMDnoShutter);
            panelBase.Controls.Add(labelAMDnoShutter);
            panelBase.Controls.Add(pictureBoxAMDnoShutter);
            panelBase.Dock = DockStyle.Fill;
            panelBase.Location = new Point(0, 0);
            panelBase.Name = "panelBase";
            panelBase.Size = new Size(1105, 600);
            panelBase.TabIndex = 12;
            panelBase.Paint += panelBase_Paint;
            // 
            // controliNTELgpuSDI
            // 
            controliNTELgpuSDI.Anchor = AnchorStyles.Top;
            controliNTELgpuSDI.BorderColor = Color.LightGray;
            controliNTELgpuSDI.ForeColor = Color.White;
            controliNTELgpuSDI.IsOn = false;
            controliNTELgpuSDI.Location = new Point(492, 132);
            controliNTELgpuSDI.Name = "controliNTELgpuSDI";
            controliNTELgpuSDI.OffColor = Color.DarkGray;
            controliNTELgpuSDI.OffText = "OFF";
            controliNTELgpuSDI.OnColor = Color.MediumSlateBlue;
            controliNTELgpuSDI.OnText = "ON";
            controliNTELgpuSDI.Size = new Size(34, 19);
            controliNTELgpuSDI.TabIndex = 79;
            controliNTELgpuSDI.TextEnabled = true;
            controliNTELgpuSDI.Click += controliNTELgpuSDI_Click;
            // 
            // labeliNTELgpuSDI
            // 
            labeliNTELgpuSDI.Anchor = AnchorStyles.Top;
            labeliNTELgpuSDI.AutoSize = true;
            labeliNTELgpuSDI.ForeColor = SystemColors.ControlLightLight;
            labeliNTELgpuSDI.Location = new Point(329, 126);
            labeliNTELgpuSDI.Name = "labeliNTELgpuSDI";
            labeliNTELgpuSDI.Size = new Size(124, 30);
            labeliNTELgpuSDI.TabIndex = 80;
            labeliNTELgpuSDI.Text = "INTEL XE GPU SDI Low\r\nLatency (set to ON):";
            // 
            // pictureBoxINTELgpuSDI
            // 
            pictureBoxINTELgpuSDI.Anchor = AnchorStyles.Top;
            pictureBoxINTELgpuSDI.BackColor = Color.Transparent;
            pictureBoxINTELgpuSDI.Image = Properties.Resources.SDI;
            pictureBoxINTELgpuSDI.Location = new Point(284, 126);
            pictureBoxINTELgpuSDI.Name = "pictureBoxINTELgpuSDI";
            pictureBoxINTELgpuSDI.Size = new Size(33, 30);
            pictureBoxINTELgpuSDI.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxINTELgpuSDI.TabIndex = 81;
            pictureBoxINTELgpuSDI.TabStop = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Location = new Point(546, 69);
            label3.Name = "label3";
            label3.Size = new Size(3, 502);
            label3.TabIndex = 78;
            // 
            // controlNVIDIAgpuSDI
            // 
            controlNVIDIAgpuSDI.Anchor = AnchorStyles.Top;
            controlNVIDIAgpuSDI.BorderColor = Color.LightGray;
            controlNVIDIAgpuSDI.ForeColor = Color.White;
            controlNVIDIAgpuSDI.IsOn = false;
            controlNVIDIAgpuSDI.Location = new Point(773, 132);
            controlNVIDIAgpuSDI.Name = "controlNVIDIAgpuSDI";
            controlNVIDIAgpuSDI.OffColor = Color.DarkGray;
            controlNVIDIAgpuSDI.OffText = "OFF";
            controlNVIDIAgpuSDI.OnColor = Color.MediumSlateBlue;
            controlNVIDIAgpuSDI.OnText = "ON";
            controlNVIDIAgpuSDI.Size = new Size(34, 19);
            controlNVIDIAgpuSDI.TabIndex = 75;
            controlNVIDIAgpuSDI.TextEnabled = true;
            controlNVIDIAgpuSDI.Click += controlNVIDIAgpuSDI_Click;
            // 
            // labelNVIDIAgpuSDI
            // 
            labelNVIDIAgpuSDI.Anchor = AnchorStyles.Top;
            labelNVIDIAgpuSDI.AutoSize = true;
            labelNVIDIAgpuSDI.ForeColor = SystemColors.ControlLightLight;
            labelNVIDIAgpuSDI.Location = new Point(611, 126);
            labelNVIDIAgpuSDI.Name = "labelNVIDIAgpuSDI";
            labelNVIDIAgpuSDI.Size = new Size(116, 30);
            labelNVIDIAgpuSDI.TabIndex = 76;
            labelNVIDIAgpuSDI.Text = "NVIDIA GPU SDI Low\r\nLatency (set to ON):";
            // 
            // pictureBoxNVIDIAgpuSDI
            // 
            pictureBoxNVIDIAgpuSDI.Anchor = AnchorStyles.Top;
            pictureBoxNVIDIAgpuSDI.BackColor = Color.Transparent;
            pictureBoxNVIDIAgpuSDI.Image = Properties.Resources.SDI;
            pictureBoxNVIDIAgpuSDI.Location = new Point(565, 126);
            pictureBoxNVIDIAgpuSDI.Name = "pictureBoxNVIDIAgpuSDI";
            pictureBoxNVIDIAgpuSDI.Size = new Size(33, 30);
            pictureBoxNVIDIAgpuSDI.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxNVIDIAgpuSDI.TabIndex = 77;
            pictureBoxNVIDIAgpuSDI.TabStop = false;
            // 
            // controlAMDgpuSDI
            // 
            controlAMDgpuSDI.BorderColor = Color.LightGray;
            controlAMDgpuSDI.ForeColor = Color.White;
            controlAMDgpuSDI.IsOn = false;
            controlAMDgpuSDI.Location = new Point(213, 168);
            controlAMDgpuSDI.Name = "controlAMDgpuSDI";
            controlAMDgpuSDI.OffColor = Color.DarkGray;
            controlAMDgpuSDI.OffText = "OFF";
            controlAMDgpuSDI.OnColor = Color.MediumSlateBlue;
            controlAMDgpuSDI.OnText = "ON";
            controlAMDgpuSDI.Size = new Size(34, 19);
            controlAMDgpuSDI.TabIndex = 72;
            controlAMDgpuSDI.TextEnabled = true;
            controlAMDgpuSDI.Click += controlAMDgpuSDI_Click;
            // 
            // labelAMDgpuSDI
            // 
            labelAMDgpuSDI.AutoSize = true;
            labelAMDgpuSDI.ForeColor = SystemColors.ControlLightLight;
            labelAMDgpuSDI.Location = new Point(56, 162);
            labelAMDgpuSDI.Name = "labelAMDgpuSDI";
            labelAMDgpuSDI.Size = new Size(112, 30);
            labelAMDgpuSDI.TabIndex = 73;
            labelAMDgpuSDI.Text = "AMD GPU SDI Low\r\nLatency (set to ON):";
            // 
            // pictureBoxAMDgpuSDI
            // 
            pictureBoxAMDgpuSDI.BackColor = Color.Transparent;
            pictureBoxAMDgpuSDI.Image = Properties.Resources.SDI;
            pictureBoxAMDgpuSDI.Location = new Point(10, 162);
            pictureBoxAMDgpuSDI.Name = "pictureBoxAMDgpuSDI";
            pictureBoxAMDgpuSDI.Size = new Size(33, 30);
            pictureBoxAMDgpuSDI.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxAMDgpuSDI.TabIndex = 74;
            pictureBoxAMDgpuSDI.TabStop = false;
            // 
            // controlGameBarMessages
            // 
            controlGameBarMessages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlGameBarMessages.BorderColor = Color.LightGray;
            controlGameBarMessages.ForeColor = Color.White;
            controlGameBarMessages.IsOn = false;
            controlGameBarMessages.Location = new Point(1047, 312);
            controlGameBarMessages.Name = "controlGameBarMessages";
            controlGameBarMessages.OffColor = Color.DarkGray;
            controlGameBarMessages.OffText = "OFF";
            controlGameBarMessages.OnColor = Color.MediumSlateBlue;
            controlGameBarMessages.OnText = "ON";
            controlGameBarMessages.Size = new Size(34, 19);
            controlGameBarMessages.TabIndex = 71;
            controlGameBarMessages.TextEnabled = true;
            controlGameBarMessages.Click += controlGameBarMessages_Click;
            // 
            // labelGameBarMessages
            // 
            labelGameBarMessages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelGameBarMessages.AutoSize = true;
            labelGameBarMessages.ForeColor = SystemColors.ControlLightLight;
            labelGameBarMessages.Location = new Point(886, 306);
            labelGameBarMessages.Name = "labelGameBarMessages";
            labelGameBarMessages.Size = new Size(117, 30);
            labelGameBarMessages.TabIndex = 70;
            labelGameBarMessages.Text = "Game-Bar Messages \r\nPop-up (set to OFF):";
            // 
            // pictureBoxGameBarMessages
            // 
            pictureBoxGameBarMessages.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxGameBarMessages.BackColor = Color.Transparent;
            pictureBoxGameBarMessages.Image = Properties.Resources.gamebarMessage;
            pictureBoxGameBarMessages.Location = new Point(844, 306);
            pictureBoxGameBarMessages.Name = "pictureBoxGameBarMessages";
            pictureBoxGameBarMessages.Size = new Size(33, 30);
            pictureBoxGameBarMessages.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxGameBarMessages.TabIndex = 69;
            pictureBoxGameBarMessages.TabStop = false;
            // 
            // uacToggleSwitch
            // 
            uacToggleSwitch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            uacToggleSwitch.BorderColor = Color.LightGray;
            uacToggleSwitch.ForeColor = Color.White;
            uacToggleSwitch.IsOn = false;
            uacToggleSwitch.Location = new Point(1048, 348);
            uacToggleSwitch.Name = "uacToggleSwitch";
            uacToggleSwitch.OffColor = Color.DarkGray;
            uacToggleSwitch.OffText = "OFF";
            uacToggleSwitch.OnColor = Color.MediumSlateBlue;
            uacToggleSwitch.OnText = "ON";
            uacToggleSwitch.Size = new Size(34, 19);
            uacToggleSwitch.TabIndex = 68;
            uacToggleSwitch.TextEnabled = true;
            uacToggleSwitch.Click += uacToggleSwitch_Click;
            // 
            // labelUAC
            // 
            labelUAC.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelUAC.AutoSize = true;
            labelUAC.ForeColor = SystemColors.ControlLightLight;
            labelUAC.Location = new Point(886, 342);
            labelUAC.Name = "labelUAC";
            labelUAC.Size = new Size(124, 30);
            labelUAC.TabIndex = 67;
            labelUAC.Text = "User Account Control \r\n(UAC) (set to OFF):";
            // 
            // pictureBoxUAC
            // 
            pictureBoxUAC.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxUAC.BackColor = Color.Transparent;
            pictureBoxUAC.Image = Properties.Resources.UAC;
            pictureBoxUAC.Location = new Point(844, 342);
            pictureBoxUAC.Name = "pictureBoxUAC";
            pictureBoxUAC.Size = new Size(33, 30);
            pictureBoxUAC.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxUAC.TabIndex = 66;
            pictureBoxUAC.TabStop = false;
            // 
            // labelReduceNetworkLatency
            // 
            labelReduceNetworkLatency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelReduceNetworkLatency.AutoSize = true;
            labelReduceNetworkLatency.ForeColor = SystemColors.ControlLightLight;
            labelReduceNetworkLatency.Location = new Point(886, 234);
            labelReduceNetworkLatency.Name = "labelReduceNetworkLatency";
            labelReduceNetworkLatency.Size = new Size(138, 30);
            labelReduceNetworkLatency.TabIndex = 65;
            labelReduceNetworkLatency.Text = "Reduce Network Latency\r\n(set to ON):";
            // 
            // pictureBoxReduceNetworkLatency
            // 
            pictureBoxReduceNetworkLatency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxReduceNetworkLatency.BackColor = Color.Transparent;
            pictureBoxReduceNetworkLatency.Image = Properties.Resources.NetworkLowLatency;
            pictureBoxReduceNetworkLatency.Location = new Point(844, 234);
            pictureBoxReduceNetworkLatency.Name = "pictureBoxReduceNetworkLatency";
            pictureBoxReduceNetworkLatency.Size = new Size(33, 30);
            pictureBoxReduceNetworkLatency.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxReduceNetworkLatency.TabIndex = 64;
            pictureBoxReduceNetworkLatency.TabStop = false;
            // 
            // controlReduceNetworkLatency
            // 
            controlReduceNetworkLatency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlReduceNetworkLatency.BorderColor = Color.LightGray;
            controlReduceNetworkLatency.ForeColor = Color.White;
            controlReduceNetworkLatency.IsOn = false;
            controlReduceNetworkLatency.Location = new Point(1048, 240);
            controlReduceNetworkLatency.Name = "controlReduceNetworkLatency";
            controlReduceNetworkLatency.OffColor = Color.DarkGray;
            controlReduceNetworkLatency.OffText = "OFF";
            controlReduceNetworkLatency.OnColor = Color.MediumSlateBlue;
            controlReduceNetworkLatency.OnText = "ON";
            controlReduceNetworkLatency.Size = new Size(34, 19);
            controlReduceNetworkLatency.TabIndex = 63;
            controlReduceNetworkLatency.TextEnabled = true;
            controlReduceNetworkLatency.Click += controlReduceNetworkLatency_Click;
            // 
            // controlReduceWindowsLatency
            // 
            controlReduceWindowsLatency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlReduceWindowsLatency.BorderColor = Color.LightGray;
            controlReduceWindowsLatency.ForeColor = Color.White;
            controlReduceWindowsLatency.IsOn = false;
            controlReduceWindowsLatency.Location = new Point(1048, 276);
            controlReduceWindowsLatency.Name = "controlReduceWindowsLatency";
            controlReduceWindowsLatency.OffColor = Color.DarkGray;
            controlReduceWindowsLatency.OffText = "OFF";
            controlReduceWindowsLatency.OnColor = Color.MediumSlateBlue;
            controlReduceWindowsLatency.OnText = "ON";
            controlReduceWindowsLatency.Size = new Size(34, 19);
            controlReduceWindowsLatency.TabIndex = 62;
            controlReduceWindowsLatency.TextEnabled = true;
            controlReduceWindowsLatency.Click += controlReduceWindowsLatency_Click;
            // 
            // pictureBoxReduceWindowsLatency
            // 
            pictureBoxReduceWindowsLatency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxReduceWindowsLatency.BackColor = Color.Transparent;
            pictureBoxReduceWindowsLatency.Image = Properties.Resources.Low_Latency;
            pictureBoxReduceWindowsLatency.Location = new Point(844, 270);
            pictureBoxReduceWindowsLatency.Name = "pictureBoxReduceWindowsLatency";
            pictureBoxReduceWindowsLatency.Size = new Size(33, 30);
            pictureBoxReduceWindowsLatency.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxReduceWindowsLatency.TabIndex = 61;
            pictureBoxReduceWindowsLatency.TabStop = false;
            // 
            // labelReduceWindowsLatency
            // 
            labelReduceWindowsLatency.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelReduceWindowsLatency.AutoSize = true;
            labelReduceWindowsLatency.ForeColor = SystemColors.ControlLightLight;
            labelReduceWindowsLatency.Location = new Point(886, 270);
            labelReduceWindowsLatency.Name = "labelReduceWindowsLatency";
            labelReduceWindowsLatency.Size = new Size(125, 30);
            labelReduceWindowsLatency.TabIndex = 60;
            labelReduceWindowsLatency.Text = "Reduce High DPC/ISR \r\nLatencies (set to ON):";
            // 
            // controlBCDMemoryUsage
            // 
            controlBCDMemoryUsage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlBCDMemoryUsage.BorderColor = Color.LightGray;
            controlBCDMemoryUsage.ForeColor = Color.White;
            controlBCDMemoryUsage.IsOn = false;
            controlBCDMemoryUsage.Location = new Point(1048, 492);
            controlBCDMemoryUsage.Name = "controlBCDMemoryUsage";
            controlBCDMemoryUsage.OffColor = Color.DarkGray;
            controlBCDMemoryUsage.OffText = "OFF";
            controlBCDMemoryUsage.OnColor = Color.MediumSlateBlue;
            controlBCDMemoryUsage.OnText = "ON";
            controlBCDMemoryUsage.Size = new Size(34, 19);
            controlBCDMemoryUsage.TabIndex = 59;
            controlBCDMemoryUsage.TextEnabled = true;
            controlBCDMemoryUsage.Click += controlBCDMemoryUsage_Click;
            // 
            // labelBCDMemoryUsage
            // 
            labelBCDMemoryUsage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelBCDMemoryUsage.AutoSize = true;
            labelBCDMemoryUsage.ForeColor = SystemColors.ControlLightLight;
            labelBCDMemoryUsage.Location = new Point(886, 486);
            labelBCDMemoryUsage.Name = "labelBCDMemoryUsage";
            labelBCDMemoryUsage.Size = new Size(110, 30);
            labelBCDMemoryUsage.TabIndex = 58;
            labelBCDMemoryUsage.Text = "Optimize Memory \r\nby BCD (set to ON):";
            // 
            // pictureBCDMemoryUsage
            // 
            pictureBCDMemoryUsage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBCDMemoryUsage.BackColor = Color.Transparent;
            pictureBCDMemoryUsage.Image = Properties.Resources.Bcd;
            pictureBCDMemoryUsage.Location = new Point(844, 486);
            pictureBCDMemoryUsage.Name = "pictureBCDMemoryUsage";
            pictureBCDMemoryUsage.Size = new Size(33, 30);
            pictureBCDMemoryUsage.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBCDMemoryUsage.TabIndex = 57;
            pictureBCDMemoryUsage.TabStop = false;
            // 
            // labelCoreIsolation_CFG
            // 
            labelCoreIsolation_CFG.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelCoreIsolation_CFG.AutoSize = true;
            labelCoreIsolation_CFG.ForeColor = SystemColors.ControlLightLight;
            labelCoreIsolation_CFG.Location = new Point(886, 198);
            labelCoreIsolation_CFG.Name = "labelCoreIsolation_CFG";
            labelCoreIsolation_CFG.Size = new Size(119, 30);
            labelCoreIsolation_CFG.TabIndex = 45;
            labelCoreIsolation_CFG.Text = "Core Isolation + CFG \r\n(set to OFF):";
            // 
            // pictureBoxCoreIsolation_CFG
            // 
            pictureBoxCoreIsolation_CFG.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxCoreIsolation_CFG.BackColor = Color.Transparent;
            pictureBoxCoreIsolation_CFG.Image = Properties.Resources.CoreIsolation_CFG;
            pictureBoxCoreIsolation_CFG.Location = new Point(844, 198);
            pictureBoxCoreIsolation_CFG.Name = "pictureBoxCoreIsolation_CFG";
            pictureBoxCoreIsolation_CFG.Size = new Size(33, 30);
            pictureBoxCoreIsolation_CFG.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxCoreIsolation_CFG.TabIndex = 44;
            pictureBoxCoreIsolation_CFG.TabStop = false;
            // 
            // controlCoreIsolation_Exploid
            // 
            controlCoreIsolation_Exploid.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlCoreIsolation_Exploid.BorderColor = Color.LightGray;
            controlCoreIsolation_Exploid.ForeColor = Color.White;
            controlCoreIsolation_Exploid.IsOn = false;
            controlCoreIsolation_Exploid.Location = new Point(1048, 204);
            controlCoreIsolation_Exploid.Name = "controlCoreIsolation_Exploid";
            controlCoreIsolation_Exploid.OffColor = Color.DarkGray;
            controlCoreIsolation_Exploid.OffText = "OFF";
            controlCoreIsolation_Exploid.OnColor = Color.MediumSlateBlue;
            controlCoreIsolation_Exploid.OnText = "ON";
            controlCoreIsolation_Exploid.Size = new Size(34, 19);
            controlCoreIsolation_Exploid.TabIndex = 43;
            controlCoreIsolation_Exploid.TextEnabled = true;
            controlCoreIsolation_Exploid.Click += controlCoreIsolation_Exploid_Click;
            // 
            // controlSystemDevices
            // 
            controlSystemDevices.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlSystemDevices.BorderColor = Color.LightGray;
            controlSystemDevices.ForeColor = Color.White;
            controlSystemDevices.IsOn = false;
            controlSystemDevices.Location = new Point(1048, 168);
            controlSystemDevices.Name = "controlSystemDevices";
            controlSystemDevices.OffColor = Color.DarkGray;
            controlSystemDevices.OffText = "OFF";
            controlSystemDevices.OnColor = Color.MediumSlateBlue;
            controlSystemDevices.OnText = "ON";
            controlSystemDevices.Size = new Size(34, 19);
            controlSystemDevices.TabIndex = 39;
            controlSystemDevices.TextEnabled = true;
            controlSystemDevices.Click += controlSystemDevices_Click;
            // 
            // controlMeltdownSpectreProtectionOnOff
            // 
            controlMeltdownSpectreProtectionOnOff.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlMeltdownSpectreProtectionOnOff.BorderColor = Color.LightGray;
            controlMeltdownSpectreProtectionOnOff.ForeColor = Color.White;
            controlMeltdownSpectreProtectionOnOff.IsOn = false;
            controlMeltdownSpectreProtectionOnOff.Location = new Point(1048, 132);
            controlMeltdownSpectreProtectionOnOff.Name = "controlMeltdownSpectreProtectionOnOff";
            controlMeltdownSpectreProtectionOnOff.OffColor = Color.DarkGray;
            controlMeltdownSpectreProtectionOnOff.OffText = "OFF";
            controlMeltdownSpectreProtectionOnOff.OnColor = Color.MediumSlateBlue;
            controlMeltdownSpectreProtectionOnOff.OnText = "ON";
            controlMeltdownSpectreProtectionOnOff.Size = new Size(34, 19);
            controlMeltdownSpectreProtectionOnOff.TabIndex = 39;
            controlMeltdownSpectreProtectionOnOff.TextEnabled = true;
            controlMeltdownSpectreProtectionOnOff.Click += controlMeltdownSpectreProtectionOnOff_Click;
            // 
            // labelSystemDevices
            // 
            labelSystemDevices.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelSystemDevices.AutoSize = true;
            labelSystemDevices.ForeColor = SystemColors.ControlLightLight;
            labelSystemDevices.Location = new Point(886, 162);
            labelSystemDevices.Name = "labelSystemDevices";
            labelSystemDevices.Size = new Size(151, 30);
            labelSystemDevices.TabIndex = 41;
            labelSystemDevices.Text = "System Devices and Virtual \r\nMachine (set to OFF):";
            // 
            // pictureBoxSystemDevices
            // 
            pictureBoxSystemDevices.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxSystemDevices.BackColor = Color.Transparent;
            pictureBoxSystemDevices.Image = Properties.Resources.System_Devices;
            pictureBoxSystemDevices.Location = new Point(844, 162);
            pictureBoxSystemDevices.Name = "pictureBoxSystemDevices";
            pictureBoxSystemDevices.Size = new Size(33, 30);
            pictureBoxSystemDevices.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxSystemDevices.TabIndex = 40;
            pictureBoxSystemDevices.TabStop = false;
            // 
            // labelMeltdown_Spectre
            // 
            labelMeltdown_Spectre.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelMeltdown_Spectre.AutoSize = true;
            labelMeltdown_Spectre.ForeColor = SystemColors.ControlLightLight;
            labelMeltdown_Spectre.Location = new Point(886, 126);
            labelMeltdown_Spectre.Name = "labelMeltdown_Spectre";
            labelMeltdown_Spectre.Size = new Size(129, 30);
            labelMeltdown_Spectre.TabIndex = 41;
            labelMeltdown_Spectre.Text = "Meltdown / Spectre \r\nProtection (set to OFF):";
            // 
            // pictureBoxMeltdown_Spectre
            // 
            pictureBoxMeltdown_Spectre.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxMeltdown_Spectre.BackColor = Color.Transparent;
            pictureBoxMeltdown_Spectre.Image = Properties.Resources.Meltdown_Spectre256;
            pictureBoxMeltdown_Spectre.Location = new Point(844, 126);
            pictureBoxMeltdown_Spectre.Name = "pictureBoxMeltdown_Spectre";
            pictureBoxMeltdown_Spectre.Size = new Size(33, 30);
            pictureBoxMeltdown_Spectre.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxMeltdown_Spectre.TabIndex = 40;
            pictureBoxMeltdown_Spectre.TabStop = false;
            // 
            // controlAMDnoShutter
            // 
            controlAMDnoShutter.BorderColor = Color.LightGray;
            controlAMDnoShutter.ForeColor = Color.White;
            controlAMDnoShutter.IsOn = false;
            controlAMDnoShutter.Location = new Point(213, 132);
            controlAMDnoShutter.Name = "controlAMDnoShutter";
            controlAMDnoShutter.OffColor = Color.DarkGray;
            controlAMDnoShutter.OffText = "OFF";
            controlAMDnoShutter.OnColor = Color.MediumSlateBlue;
            controlAMDnoShutter.OnText = "ON";
            controlAMDnoShutter.Size = new Size(34, 19);
            controlAMDnoShutter.TabIndex = 36;
            controlAMDnoShutter.TextEnabled = true;
            controlAMDnoShutter.Click += controlAMDnoShutter_Click;
            // 
            // labelAMDnoShutter
            // 
            labelAMDnoShutter.AutoSize = true;
            labelAMDnoShutter.ForeColor = SystemColors.ControlLightLight;
            labelAMDnoShutter.Location = new Point(56, 126);
            labelAMDnoShutter.Name = "labelAMDnoShutter";
            labelAMDnoShutter.Size = new Size(125, 30);
            labelAMDnoShutter.TabIndex = 37;
            labelAMDnoShutter.Text = "AMD GPU No-Shutter \r\n(set to ON):";
            // 
            // pictureBoxAMDnoShutter
            // 
            pictureBoxAMDnoShutter.BackColor = Color.Transparent;
            pictureBoxAMDnoShutter.Image = Properties.Resources.AMDgpu;
            pictureBoxAMDnoShutter.Location = new Point(10, 126);
            pictureBoxAMDnoShutter.Name = "pictureBoxAMDnoShutter";
            pictureBoxAMDnoShutter.Size = new Size(33, 30);
            pictureBoxAMDnoShutter.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxAMDnoShutter.TabIndex = 38;
            pictureBoxAMDnoShutter.TabStop = false;
            // 
            // FormTweakParadise
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(1105, 600);
            Controls.Add(label2);
            Controls.Add(labelAMD_INTEL);
            Controls.Add(pictureBoxMICROSOFTtp);
            Controls.Add(pictureBoxNVIDIAtp);
            Controls.Add(pictureBoxINTELtp);
            Controls.Add(pictureBoxAMDtp);
            Controls.Add(panelTitleFormTP1);
            Controls.Add(panelBase);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormTweakParadise";
            Opacity = 0.95D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tweak Paradise";
            TopMost = true;
            Load += FormTweakParadise_Load;
            panelTitleFormTP1.ResumeLayout(false);
            panelTitleFormTP1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAMDtp).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxINTELtp).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNVIDIAtp).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMICROSOFTtp).EndInit();
            panelBase.ResumeLayout(false);
            panelBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxINTELgpuSDI).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxNVIDIAgpuSDI).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAMDgpuSDI).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGameBarMessages).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUAC).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxReduceNetworkLatency).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxReduceWindowsLatency).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBCDMemoryUsage).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCoreIsolation_CFG).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSystemDevices).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMeltdown_Spectre).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAMDnoShutter).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonCloseFormTP;
        private Panel panelTitleFormTP1;
        private Label labelTitleFormTP;
        private PictureBox pictureBoxAMDtp;
        private PictureBox pictureBoxINTELtp;
        private PictureBox pictureBoxNVIDIAtp;
        private PictureBox pictureBoxMICROSOFTtp;
        private Label labelAMD_INTEL;
        private Label label2;
        private PictureBox pictureBox1;
        private Panel panelBase;
        private ToggleSwitchDev.ControlToggles controlAMDnoShutter;
        private Label labelAMDnoShutter;
        private PictureBox pictureBoxAMDnoShutter;
        private ToggleSwitchDev.ControlToggles controlMeltdownSpectreProtectionOnOff;
        private Label labelMeltdown_Spectre;
        private PictureBox pictureBoxMeltdown_Spectre;
        private ToggleSwitchDev.ControlToggles controlSystemDevices;
        private Label labelSystemDevices;
        private PictureBox pictureBoxSystemDevices;
        private Label labelCoreIsolation_CFG;
        private PictureBox pictureBoxCoreIsolation_CFG;
        private ToggleSwitchDev.ControlToggles controlCoreIsolation_Exploid;
        private ToggleSwitchDev.ControlToggles uacToggleSwitch;
        private Label labelUAC;
        private PictureBox pictureBoxUAC;
        private Label labelReduceNetworkLatency;
        private PictureBox pictureBoxReduceNetworkLatency;
        private ToggleSwitchDev.ControlToggles controlReduceNetworkLatency;
        private ToggleSwitchDev.ControlToggles controlReduceWindowsLatency;
        private PictureBox pictureBoxReduceWindowsLatency;
        private Label labelReduceWindowsLatency;
        private ToggleSwitchDev.ControlToggles controlBCDMemoryUsage;
        private Label labelBCDMemoryUsage;
        private PictureBox pictureBCDMemoryUsage;
        private ToggleSwitchDev.ControlToggles controlGameBarMessages;
        private Label labelGameBarMessages;
        private PictureBox pictureBoxGameBarMessages;
        private ToggleSwitchDev.ControlToggles controlAMDgpuSDI;
        private Label labelAMDgpuSDI;
        private PictureBox pictureBoxAMDgpuSDI;
        private ToggleSwitchDev.ControlToggles controlNVIDIAgpuSDI;
        private Label labelNVIDIAgpuSDI;
        private PictureBox pictureBoxNVIDIAgpuSDI;
        private Label label3;
        private ToggleSwitchDev.ControlToggles controliNTELgpuSDI;
        private Label labeliNTELgpuSDI;
        private PictureBox pictureBoxINTELgpuSDI;
    }
}