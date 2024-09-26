namespace Console2Desk.info
{
    partial class FormInfoControllers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInfoControllers));
            buttonCloseWindowInfoControllers = new Button();
            labelTitleInfoControllers = new Label();
            panelTitleInfoControllers = new Panel();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            labelLSRSTitle = new Label();
            labelLSRS = new Label();
            labelRSPress = new Label();
            labelRSPressTitle = new Label();
            labelRBRT = new Label();
            labelRBRTTilte = new Label();
            labelLBLT = new Label();
            labelBLTTitle = new Label();
            labelAButton = new Label();
            labelAButtontitle = new Label();
            labelBACKButton = new Label();
            labelBackButtonTitle = new Label();
            labelYButton = new Label();
            labelYButtonTitle = new Label();
            labelDPad = new Label();
            labelDPadTitle = new Label();
            labelSeparatorFormInfo = new Label();
            panel1 = new Panel();
            panelTitleInfoControllers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonCloseWindowInfoControllers
            // 
            buttonCloseWindowInfoControllers.BackColor = Color.Transparent;
            buttonCloseWindowInfoControllers.FlatAppearance.BorderSize = 0;
            buttonCloseWindowInfoControllers.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 0, 0);
            buttonCloseWindowInfoControllers.FlatStyle = FlatStyle.Flat;
            buttonCloseWindowInfoControllers.Font = new Font("Roboto Light", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCloseWindowInfoControllers.ForeColor = Color.Red;
            buttonCloseWindowInfoControllers.Location = new Point(725, 0);
            buttonCloseWindowInfoControllers.Name = "buttonCloseWindowInfoControllers";
            buttonCloseWindowInfoControllers.Size = new Size(75, 56);
            buttonCloseWindowInfoControllers.TabIndex = 1;
            buttonCloseWindowInfoControllers.Text = "X";
            buttonCloseWindowInfoControllers.UseVisualStyleBackColor = false;
            buttonCloseWindowInfoControllers.Click += buttonCloseWindowInfoControllers_Click;
            // 
            // labelTitleInfoControllers
            // 
            labelTitleInfoControllers.AutoSize = true;
            labelTitleInfoControllers.Font = new Font("Roboto Medium", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitleInfoControllers.Location = new Point(258, 12);
            labelTitleInfoControllers.Name = "labelTitleInfoControllers";
            labelTitleInfoControllers.Size = new Size(285, 33);
            labelTitleInfoControllers.TabIndex = 0;
            labelTitleInfoControllers.Text = "C2D Commands Info";
            labelTitleInfoControllers.Click += labelTitleInfoControllers_Click;
            // 
            // panelTitleInfoControllers
            // 
            panelTitleInfoControllers.BackColor = Color.FromArgb(31, 31, 31);
            panelTitleInfoControllers.Controls.Add(buttonCloseWindowInfoControllers);
            panelTitleInfoControllers.Controls.Add(labelTitleInfoControllers);
            panelTitleInfoControllers.Dock = DockStyle.Top;
            panelTitleInfoControllers.ForeColor = SystemColors.ControlLightLight;
            panelTitleInfoControllers.Location = new Point(0, 0);
            panelTitleInfoControllers.Name = "panelTitleInfoControllers";
            panelTitleInfoControllers.Size = new Size(800, 56);
            panelTitleInfoControllers.TabIndex = 1;
            panelTitleInfoControllers.Paint += panelTitleInfoControllers_Paint;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.WASD_Icon;
            pictureBox3.Location = new Point(477, 59);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(60, 60);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.Controller_List;
            pictureBox1.Location = new Point(238, 120);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(552, 387);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // labelLSRSTitle
            // 
            labelLSRSTitle.AutoSize = true;
            labelLSRSTitle.Font = new Font("Roboto Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelLSRSTitle.ForeColor = SystemColors.ControlLightLight;
            labelLSRSTitle.Location = new Point(12, 68);
            labelLSRSTitle.Name = "labelLSRSTitle";
            labelLSRSTitle.Size = new Size(203, 23);
            labelLSRSTitle.TabIndex = 3;
            labelLSRSTitle.Text = "LS or RS (X/Y axes):";
            // 
            // labelLSRS
            // 
            labelLSRS.AutoSize = true;
            labelLSRS.Font = new Font("Roboto Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelLSRS.ForeColor = SystemColors.ControlLight;
            labelLSRS.Location = new Point(12, 91);
            labelLSRS.Name = "labelLSRS";
            labelLSRS.Size = new Size(182, 36);
            labelLSRS.TabIndex = 4;
            labelLSRS.Text = "Move Mouse (RS better to use \r\non Windows Menu Settings)";
            // 
            // labelRSPress
            // 
            labelRSPress.AutoSize = true;
            labelRSPress.Font = new Font("Roboto Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelRSPress.ForeColor = SystemColors.ControlLight;
            labelRSPress.Location = new Point(12, 156);
            labelRSPress.Name = "labelRSPress";
            labelRSPress.Size = new Size(128, 18);
            labelRSPress.TabIndex = 6;
            labelRSPress.Text = "Middle Mouse Button";
            // 
            // labelRSPressTitle
            // 
            labelRSPressTitle.AutoSize = true;
            labelRSPressTitle.Font = new Font("Roboto Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelRSPressTitle.ForeColor = SystemColors.ControlLightLight;
            labelRSPressTitle.Location = new Point(12, 133);
            labelRSPressTitle.Name = "labelRSPressTitle";
            labelRSPressTitle.Size = new Size(101, 23);
            labelRSPressTitle.TabIndex = 5;
            labelRSPressTitle.Text = "RS Press:";
            // 
            // labelRBRT
            // 
            labelRBRT.AutoSize = true;
            labelRBRT.Font = new Font("Roboto Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelRBRT.ForeColor = SystemColors.ControlLight;
            labelRBRT.Location = new Point(12, 205);
            labelRBRT.Name = "labelRBRT";
            labelRBRT.Size = new Size(179, 36);
            labelRBRT.TabIndex = 8;
            labelRBRT.Text = "RB = Mouse Left Click Button\r\nRT = Mouse Right Click Button";
            // 
            // labelRBRTTilte
            // 
            labelRBRTTilte.AutoSize = true;
            labelRBRTTilte.Font = new Font("Roboto Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelRBRTTilte.ForeColor = SystemColors.ControlLightLight;
            labelRBRTTilte.Location = new Point(12, 182);
            labelRBRTTilte.Name = "labelRBRTTilte";
            labelRBRTTilte.Size = new Size(100, 23);
            labelRBRTTilte.TabIndex = 7;
            labelRBRTTilte.Text = "RB or RT:";
            // 
            // labelLBLT
            // 
            labelLBLT.AutoSize = true;
            labelLBLT.Font = new Font("Roboto Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelLBLT.ForeColor = SystemColors.ControlLight;
            labelLBLT.Location = new Point(12, 271);
            labelLBLT.Name = "labelLBLT";
            labelLBLT.Size = new Size(174, 36);
            labelLBLT.TabIndex = 10;
            labelLBLT.Text = "LB = TAB (move forward)\r\nLT = TAB+SHIFT (move back)";
            // 
            // labelBLTTitle
            // 
            labelBLTTitle.AutoSize = true;
            labelBLTTitle.Font = new Font("Roboto Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelBLTTitle.ForeColor = SystemColors.ControlLightLight;
            labelBLTTitle.Location = new Point(12, 248);
            labelBLTTitle.Name = "labelBLTTitle";
            labelBLTTitle.Size = new Size(94, 23);
            labelBLTTitle.TabIndex = 9;
            labelBLTTitle.Text = "LB or LT:";
            // 
            // labelAButton
            // 
            labelAButton.AutoSize = true;
            labelAButton.Font = new Font("Roboto Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelAButton.ForeColor = SystemColors.ControlLight;
            labelAButton.Location = new Point(12, 338);
            labelAButton.Name = "labelAButton";
            labelAButton.Size = new Size(101, 18);
            labelAButton.TabIndex = 12;
            labelAButton.Text = "Enter (Keyboard)";
            // 
            // labelAButtontitle
            // 
            labelAButtontitle.AutoSize = true;
            labelAButtontitle.Font = new Font("Roboto Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelAButtontitle.ForeColor = SystemColors.ControlLightLight;
            labelAButtontitle.Location = new Point(12, 315);
            labelAButtontitle.Name = "labelAButtontitle";
            labelAButtontitle.Size = new Size(99, 23);
            labelAButtontitle.TabIndex = 11;
            labelAButtontitle.Text = "A Button:";
            // 
            // labelBACKButton
            // 
            labelBACKButton.AutoSize = true;
            labelBACKButton.Font = new Font("Roboto Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelBACKButton.ForeColor = SystemColors.ControlLight;
            labelBACKButton.Location = new Point(12, 462);
            labelBACKButton.Name = "labelBACKButton";
            labelBACKButton.Size = new Size(93, 18);
            labelBACKButton.TabIndex = 14;
            labelBACKButton.Text = "Esc (Keyboard)";
            // 
            // labelBackButtonTitle
            // 
            labelBackButtonTitle.AutoSize = true;
            labelBackButtonTitle.Font = new Font("Roboto Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelBackButtonTitle.ForeColor = SystemColors.ControlLightLight;
            labelBackButtonTitle.Location = new Point(12, 439);
            labelBackButtonTitle.Name = "labelBackButtonTitle";
            labelBackButtonTitle.Size = new Size(138, 23);
            labelBackButtonTitle.TabIndex = 13;
            labelBackButtonTitle.Text = "BACK Button:";
            // 
            // labelYButton
            // 
            labelYButton.AutoSize = true;
            labelYButton.Font = new Font("Roboto Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelYButton.ForeColor = SystemColors.ControlLight;
            labelYButton.Location = new Point(12, 390);
            labelYButton.Name = "labelYButton";
            labelYButton.Size = new Size(160, 36);
            labelYButton.TabIndex = 16;
            labelYButton.Text = "Old (X) - New (Y) \r\nVirtual Keyboard on Screen";
            // 
            // labelYButtonTitle
            // 
            labelYButtonTitle.AutoSize = true;
            labelYButtonTitle.Font = new Font("Roboto Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelYButtonTitle.ForeColor = SystemColors.ControlLightLight;
            labelYButtonTitle.Location = new Point(12, 367);
            labelYButtonTitle.Name = "labelYButtonTitle";
            labelYButtonTitle.Size = new Size(143, 23);
            labelYButtonTitle.TabIndex = 15;
            labelYButtonTitle.Text = "Y or X Button:";
            labelYButtonTitle.Click += labelYButtonTitle_Click;
            // 
            // labelDPad
            // 
            labelDPad.AutoSize = true;
            labelDPad.Font = new Font("Roboto Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            labelDPad.ForeColor = SystemColors.ControlLight;
            labelDPad.Location = new Point(12, 514);
            labelDPad.Name = "labelDPad";
            labelDPad.Size = new Size(124, 36);
            labelDPad.TabIndex = 18;
            labelDPad.Text = "Arrows Keyboard \r\nUp, Down, Left, Right";
            // 
            // labelDPadTitle
            // 
            labelDPadTitle.AutoSize = true;
            labelDPadTitle.Font = new Font("Roboto Medium", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelDPadTitle.ForeColor = SystemColors.ControlLightLight;
            labelDPadTitle.Location = new Point(12, 491);
            labelDPadTitle.Name = "labelDPadTitle";
            labelDPadTitle.Size = new Size(72, 23);
            labelDPadTitle.TabIndex = 17;
            labelDPadTitle.Text = "D-Pad:";
            // 
            // labelSeparatorFormInfo
            // 
            labelSeparatorFormInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            labelSeparatorFormInfo.BorderStyle = BorderStyle.Fixed3D;
            labelSeparatorFormInfo.Location = new Point(226, 70);
            labelSeparatorFormInfo.Name = "labelSeparatorFormInfo";
            labelSeparatorFormInfo.Size = new Size(2, 483);
            labelSeparatorFormInfo.TabIndex = 19;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pictureBox3);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 569);
            panel1.TabIndex = 20;
            // 
            // FormInfoControllers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(800, 569);
            Controls.Add(labelSeparatorFormInfo);
            Controls.Add(labelDPad);
            Controls.Add(labelDPadTitle);
            Controls.Add(labelYButton);
            Controls.Add(labelYButtonTitle);
            Controls.Add(labelBACKButton);
            Controls.Add(labelBackButtonTitle);
            Controls.Add(labelAButton);
            Controls.Add(labelAButtontitle);
            Controls.Add(labelLBLT);
            Controls.Add(labelBLTTitle);
            Controls.Add(labelRBRT);
            Controls.Add(labelRBRTTilte);
            Controls.Add(labelRSPress);
            Controls.Add(labelRSPressTitle);
            Controls.Add(labelLSRS);
            Controls.Add(labelLSRSTitle);
            Controls.Add(panelTitleInfoControllers);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormInfoControllers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormInfoControllers";
            TopMost = true;
            Load += FormInfoControllers_Load;
            panelTitleInfoControllers.ResumeLayout(false);
            panelTitleInfoControllers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonCloseWindowInfoControllers;
        private Label labelTitleInfoControllers;
        private Panel panelTitleInfoControllers;
        private PictureBox pictureBox1;
        private Label labelLSRSTitle;
        private Label labelLSRS;
        private Label labelRSPress;
        private Label labelRSPressTitle;
        private Label labelRBRT;
        private Label labelRBRTTilte;
        private Label labelLBLT;
        private Label labelBLTTitle;
        private Label labelAButton;
        private Label labelAButtontitle;
        private Label labelBACKButton;
        private Label labelBackButtonTitle;
        private Label labelYButton;
        private Label labelYButtonTitle;
        private Label labelDPad;
        private Label labelDPadTitle;
        private Label labelSeparatorFormInfo;
        private Panel panel1;
        private PictureBox pictureBox3;
    }
}