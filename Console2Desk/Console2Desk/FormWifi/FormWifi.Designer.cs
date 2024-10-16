namespace Console2Desk.FormWifi
{
    partial class FormWifi
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
            labelTitleWiFi = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            buttonConnectWiFi = new Special_Niewbie_Button();
            pictureBoxBackArrow = new PictureBox();
            controlWiFi = new ToggleSwitchDev.ControlToggles();
            textBoxInfoConnection = new TextBox();
            textBoxPassWiFi = new TextBox();
            pictureBoxShowPassword = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBackArrow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxShowPassword).BeginInit();
            SuspendLayout();
            // 
            // labelTitleWiFi
            // 
            labelTitleWiFi.AutoSize = true;
            labelTitleWiFi.BackColor = Color.Transparent;
            labelTitleWiFi.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            labelTitleWiFi.ForeColor = SystemColors.ControlLight;
            labelTitleWiFi.Location = new Point(46, 9);
            labelTitleWiFi.Name = "labelTitleWiFi";
            labelTitleWiFi.Size = new Size(41, 21);
            labelTitleWiFi.TabIndex = 1;
            labelTitleWiFi.Text = "WiFi";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Cursor = Cursors.Hand;
            flowLayoutPanel1.Location = new Point(12, 47);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(371, 351);
            flowLayoutPanel1.TabIndex = 2;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // buttonConnectWiFi
            // 
            buttonConnectWiFi.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonConnectWiFi.BackColor = Color.FromArgb(50, 50, 50);
            buttonConnectWiFi.BorderColor = Color.FromArgb(227, 227, 227);
            buttonConnectWiFi.BorderRadius = 12;
            buttonConnectWiFi.BorderSize = 1;
            buttonConnectWiFi.FlatAppearance.BorderSize = 0;
            buttonConnectWiFi.FlatStyle = FlatStyle.Flat;
            buttonConnectWiFi.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            buttonConnectWiFi.ForeColor = Color.White;
            buttonConnectWiFi.HoverColor = Color.MediumSlateBlue;
            buttonConnectWiFi.Location = new Point(290, 442);
            buttonConnectWiFi.Name = "buttonConnectWiFi";
            buttonConnectWiFi.Size = new Size(91, 36);
            buttonConnectWiFi.TabIndex = 4;
            buttonConnectWiFi.Text = "Connect";
            buttonConnectWiFi.TextColor = Color.White;
            buttonConnectWiFi.UseVisualStyleBackColor = false;
            buttonConnectWiFi.Click += buttonConnectWiFi_Click;
            // 
            // pictureBoxBackArrow
            // 
            pictureBoxBackArrow.Cursor = Cursors.Hand;
            pictureBoxBackArrow.Image = Properties.Resources.BackArrowWiFi;
            pictureBoxBackArrow.Location = new Point(-2, -2);
            pictureBoxBackArrow.Name = "pictureBoxBackArrow";
            pictureBoxBackArrow.Size = new Size(50, 42);
            pictureBoxBackArrow.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxBackArrow.TabIndex = 7;
            pictureBoxBackArrow.TabStop = false;
            pictureBoxBackArrow.Click += pictureBoxBackArrow_Click;
            // 
            // controlWiFi
            // 
            controlWiFi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            controlWiFi.BorderColor = Color.LightGray;
            controlWiFi.ForeColor = Color.White;
            controlWiFi.IsOn = false;
            controlWiFi.Location = new Point(341, 9);
            controlWiFi.Name = "controlWiFi";
            controlWiFi.OffColor = Color.DarkGray;
            controlWiFi.OffText = "";
            controlWiFi.OnColor = Color.FromArgb(123, 104, 238);
            controlWiFi.OnText = "";
            controlWiFi.Size = new Size(36, 20);
            controlWiFi.TabIndex = 8;
            controlWiFi.Text = "controlToggles1";
            controlWiFi.TextEnabled = true;
            controlWiFi.Click += controlWiFi_Click;
            // 
            // textBoxInfoConnection
            // 
            textBoxInfoConnection.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxInfoConnection.BackColor = Color.FromArgb(31, 31, 31);
            textBoxInfoConnection.BorderStyle = BorderStyle.None;
            textBoxInfoConnection.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxInfoConnection.ForeColor = SystemColors.ControlLight;
            textBoxInfoConnection.Location = new Point(13, 404);
            textBoxInfoConnection.Name = "textBoxInfoConnection";
            textBoxInfoConnection.ReadOnly = true;
            textBoxInfoConnection.Size = new Size(368, 15);
            textBoxInfoConnection.TabIndex = 9;
            textBoxInfoConnection.TextChanged += textBoxInfoConnection_TextChanged;
            // 
            // textBoxPassWiFi
            // 
            textBoxPassWiFi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxPassWiFi.BackColor = Color.FromArgb(31, 31, 31);
            textBoxPassWiFi.BorderStyle = BorderStyle.FixedSingle;
            textBoxPassWiFi.Cursor = Cursors.Hand;
            textBoxPassWiFi.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPassWiFi.ForeColor = SystemColors.ControlLight;
            textBoxPassWiFi.Location = new Point(13, 449);
            textBoxPassWiFi.Name = "textBoxPassWiFi";
            textBoxPassWiFi.PlaceholderText = "  WiFi Password";
            textBoxPassWiFi.Size = new Size(200, 25);
            textBoxPassWiFi.TabIndex = 10;
            textBoxPassWiFi.UseSystemPasswordChar = true;
            textBoxPassWiFi.Visible = false;
            textBoxPassWiFi.TextChanged += textBoxPassWiFi_TextChanged;
            // 
            // pictureBoxShowPassword
            // 
            pictureBoxShowPassword.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            pictureBoxShowPassword.Cursor = Cursors.Hand;
            pictureBoxShowPassword.Image = Properties.Resources.show_password;
            pictureBoxShowPassword.Location = new Point(217, 447);
            pictureBoxShowPassword.Name = "pictureBoxShowPassword";
            pictureBoxShowPassword.Size = new Size(49, 26);
            pictureBoxShowPassword.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxShowPassword.TabIndex = 11;
            pictureBoxShowPassword.TabStop = false;
            pictureBoxShowPassword.Visible = false;
            pictureBoxShowPassword.Click += pictureBoxShowPassword_Click;
            pictureBoxShowPassword.MouseDown += pictureBoxShowPassword_MouseDown;
            pictureBoxShowPassword.MouseUp += pictureBoxShowPassword_MouseUp;
            // 
            // FormWifi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 31);
            ClientSize = new Size(395, 490);
            Controls.Add(pictureBoxShowPassword);
            Controls.Add(textBoxPassWiFi);
            Controls.Add(textBoxInfoConnection);
            Controls.Add(controlWiFi);
            Controls.Add(pictureBoxBackArrow);
            Controls.Add(buttonConnectWiFi);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(labelTitleWiFi);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormWifi";
            Opacity = 0.95D;
            Text = "FormWifi";
            TopMost = true;
            Load += FormWifi_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxBackArrow).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxShowPassword).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitleWiFi;
        private FlowLayoutPanel flowLayoutPanel1;
        private Special_Niewbie_Button buttonConnectWiFi;
        private PictureBox pictureBoxBackArrow;
        private ToggleSwitchDev.ControlToggles controlWiFi;
        private TextBox textBoxInfoConnection;
        private TextBox textBoxPassWiFi;
        private PictureBox pictureBoxShowPassword;
    }
}