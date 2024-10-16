namespace Console2Desk.HiberSleep
{
    partial class FormHiberSleep
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHiberSleep));
            panelTitleBar = new Panel();
            labelTitleHiberSleep = new Label();
            buttonCloseFormHiberSleep = new Button();
            panelContainer = new Panel();
            radioButtonEnabled = new Special_NiewbieRadioButton();
            pictureBox1 = new PictureBox();
            radioButtonDisabled = new Special_NiewbieRadioButton();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            labelinfoHiberSleep = new Label();
            panelTitleBar.SuspendLayout();
            panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panelTitleBar
            // 
            panelTitleBar.Controls.Add(labelTitleHiberSleep);
            panelTitleBar.Controls.Add(buttonCloseFormHiberSleep);
            panelTitleBar.Location = new Point(0, 0);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(800, 65);
            panelTitleBar.TabIndex = 0;
            panelTitleBar.Paint += panelTitleBar_Paint;
            // 
            // labelTitleHiberSleep
            // 
            labelTitleHiberSleep.AutoSize = true;
            labelTitleHiberSleep.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitleHiberSleep.ForeColor = SystemColors.ControlLightLight;
            labelTitleHiberSleep.Location = new Point(252, 5);
            labelTitleHiberSleep.Name = "labelTitleHiberSleep";
            labelTitleHiberSleep.Size = new Size(297, 60);
            labelTitleHiberSleep.TabIndex = 2;
            labelTitleHiberSleep.Text = "HandleOS \r\nHibernation / Sleep / Standby\r\n";
            labelTitleHiberSleep.TextAlign = ContentAlignment.MiddleCenter;
            labelTitleHiberSleep.Click += labelTitleHiberSleep_Click;
            // 
            // buttonCloseFormHiberSleep
            // 
            buttonCloseFormHiberSleep.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCloseFormHiberSleep.BackColor = Color.FromArgb(40, 40, 40);
            buttonCloseFormHiberSleep.FlatAppearance.BorderSize = 0;
            buttonCloseFormHiberSleep.FlatStyle = FlatStyle.Flat;
            buttonCloseFormHiberSleep.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCloseFormHiberSleep.ForeColor = Color.Red;
            buttonCloseFormHiberSleep.Location = new Point(735, 1);
            buttonCloseFormHiberSleep.Name = "buttonCloseFormHiberSleep";
            buttonCloseFormHiberSleep.Size = new Size(65, 62);
            buttonCloseFormHiberSleep.TabIndex = 1;
            buttonCloseFormHiberSleep.Text = "X";
            buttonCloseFormHiberSleep.UseVisualStyleBackColor = false;
            buttonCloseFormHiberSleep.Click += buttonCloseFormHiberSleep_Click;
            // 
            // panelContainer
            // 
            panelContainer.BackColor = Color.FromArgb(40, 40, 40);
            panelContainer.Controls.Add(pictureBox1);
            panelContainer.Controls.Add(radioButtonEnabled);
            panelContainer.Controls.Add(radioButtonDisabled);
            panelContainer.Controls.Add(label3);
            panelContainer.Controls.Add(label2);
            panelContainer.Controls.Add(label1);
            panelContainer.Controls.Add(labelinfoHiberSleep);
            panelContainer.Controls.Add(panelTitleBar);
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Location = new Point(0, 0);
            panelContainer.Name = "panelContainer";
            panelContainer.Size = new Size(800, 600);
            panelContainer.TabIndex = 0;
            panelContainer.Paint += panelContainer_Paint;
            // 
            // radioButtonEnabled
            // 
            radioButtonEnabled.CheckAlign = ContentAlignment.MiddleRight;
            radioButtonEnabled.CheckedColor = Color.MediumSlateBlue;
            radioButtonEnabled.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            radioButtonEnabled.ForeColor = SystemColors.ControlLightLight;
            radioButtonEnabled.Location = new Point(605, 132);
            radioButtonEnabled.MinimumSize = new Size(0, 32);
            radioButtonEnabled.Name = "radioButtonEnabled";
            radioButtonEnabled.Size = new Size(165, 61);
            radioButtonEnabled.TabIndex = 10;
            radioButtonEnabled.TabStop = true;
            radioButtonEnabled.Text = "ENABLED";
            radioButtonEnabled.UnCheckedColor = Color.Gray;
            radioButtonEnabled.UseVisualStyleBackColor = true;
            radioButtonEnabled.CheckedChanged += radioButtonEnabled_CheckedChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.HiberSleepStand;
            pictureBox1.Location = new Point(271, 73);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(259, 179);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // radioButtonDisabled
            // 
            radioButtonDisabled.CheckAlign = ContentAlignment.MiddleRight;
            radioButtonDisabled.CheckedColor = Color.MediumSlateBlue;
            radioButtonDisabled.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            radioButtonDisabled.ForeColor = SystemColors.ControlLightLight;
            radioButtonDisabled.Location = new Point(78, 132);
            radioButtonDisabled.MinimumSize = new Size(0, 32);
            radioButtonDisabled.Name = "radioButtonDisabled";
            radioButtonDisabled.Size = new Size(171, 61);
            radioButtonDisabled.TabIndex = 8;
            radioButtonDisabled.TabStop = true;
            radioButtonDisabled.Text = "DISABLED";
            radioButtonDisabled.UnCheckedColor = Color.Gray;
            radioButtonDisabled.UseVisualStyleBackColor = true;
            radioButtonDisabled.CheckedChanged += radioButtonDisabled_CheckedChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(12, 448);
            label3.Name = "label3";
            label3.Size = new Size(776, 151);
            label3.TabIndex = 5;
            label3.Text = resources.GetString("label3.Text");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Khaki;
            label2.Location = new Point(12, 422);
            label2.Name = "label2";
            label2.Size = new Size(148, 21);
            label2.TabIndex = 4;
            label2.Text = "Cons to Re-enable";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.LimeGreen;
            label1.Location = new Point(12, 265);
            label1.Name = "label1";
            label1.Size = new Size(144, 21);
            label1.TabIndex = 2;
            label1.Text = "Pros to Re-enable";
            // 
            // labelinfoHiberSleep
            // 
            labelinfoHiberSleep.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelinfoHiberSleep.ForeColor = SystemColors.ControlLightLight;
            labelinfoHiberSleep.Location = new Point(12, 291);
            labelinfoHiberSleep.Name = "labelinfoHiberSleep";
            labelinfoHiberSleep.Size = new Size(776, 117);
            labelinfoHiberSleep.TabIndex = 1;
            labelinfoHiberSleep.Text = resources.GetString("labelinfoHiberSleep.Text");
            // 
            // FormHiberSleep
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(800, 600);
            Controls.Add(panelContainer);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormHiberSleep";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormHiberSleep";
            TopMost = true;
            Load += FormHiberSleep_Load;
            panelTitleBar.ResumeLayout(false);
            panelTitleBar.PerformLayout();
            panelContainer.ResumeLayout(false);
            panelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTitleBar;
        private Button buttonCloseFormHiberSleep;
        private Panel panelContainer;
        private Label labelinfoHiberSleep;
        private Label label1;
        private Label label2;
        private Label label3;
        private Special_NiewbieRadioButton radioButtonDisabled;
        private Label labelTitleHiberSleep;
        private Special_NiewbieRadioButton radioButtonEnabled;
        private PictureBox pictureBox1;
    }
}