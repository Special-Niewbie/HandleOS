namespace Console2Desk
{
    partial class AboutBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            panel2 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            labelVersion = new Label();
            labelSoftware = new Label();
            labelCopyright = new Label();
            labelDev = new Label();
            labelSoftwareDescription = new Label();
            labelVersionDescription = new Label();
            labelCopyrightDescription = new Label();
            labelDevDescription = new Label();
            richTextBox1 = new RichTextBox();
            labelLicense = new Label();
            labelVisitMySite = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 22F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLight;
            label1.Location = new Point(198, 13);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(207, 41);
            label1.TabIndex = 1;
            label1.Text = "Console2Desk";
            label1.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Handle_Icon_trans;
            pictureBox1.Location = new Point(65, 4);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(105, 56);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(565, 63);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlDark;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Location = new Point(18, 62);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(531, 3);
            panel2.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 86F));
            tableLayoutPanel1.Controls.Add(labelVersion, 0, 1);
            tableLayoutPanel1.Controls.Add(labelSoftware, 0, 0);
            tableLayoutPanel1.Controls.Add(labelCopyright, 0, 2);
            tableLayoutPanel1.Controls.Add(labelDev, 0, 3);
            tableLayoutPanel1.Controls.Add(labelSoftwareDescription, 1, 0);
            tableLayoutPanel1.Controls.Add(labelVersionDescription, 1, 1);
            tableLayoutPanel1.Controls.Add(labelCopyrightDescription, 1, 2);
            tableLayoutPanel1.Controls.Add(labelDevDescription, 1, 3);
            tableLayoutPanel1.Location = new Point(0, 67);
            tableLayoutPanel1.Margin = new Padding(2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(568, 83);
            tableLayoutPanel1.TabIndex = 3;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Dock = DockStyle.Fill;
            labelVersion.ForeColor = SystemColors.ControlLight;
            labelVersion.Location = new Point(2, 20);
            labelVersion.Margin = new Padding(2, 0, 2, 0);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(75, 20);
            labelVersion.TabIndex = 1;
            labelVersion.Text = "Version:";
            labelVersion.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelSoftware
            // 
            labelSoftware.AutoSize = true;
            labelSoftware.Dock = DockStyle.Fill;
            labelSoftware.ForeColor = SystemColors.ControlLight;
            labelSoftware.Location = new Point(2, 0);
            labelSoftware.Margin = new Padding(2, 0, 2, 0);
            labelSoftware.Name = "labelSoftware";
            labelSoftware.Size = new Size(75, 20);
            labelSoftware.TabIndex = 2;
            labelSoftware.Text = "Software:";
            labelSoftware.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            labelCopyright.AutoSize = true;
            labelCopyright.Dock = DockStyle.Fill;
            labelCopyright.ForeColor = SystemColors.ControlLight;
            labelCopyright.Location = new Point(2, 40);
            labelCopyright.Margin = new Padding(2, 0, 2, 0);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(75, 20);
            labelCopyright.TabIndex = 3;
            labelCopyright.Text = "Copyright:";
            labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelDev
            // 
            labelDev.AutoSize = true;
            labelDev.Dock = DockStyle.Fill;
            labelDev.ForeColor = SystemColors.ControlLight;
            labelDev.Location = new Point(2, 60);
            labelDev.Margin = new Padding(2, 0, 2, 0);
            labelDev.Name = "labelDev";
            labelDev.Size = new Size(75, 23);
            labelDev.TabIndex = 4;
            labelDev.Text = "Developers:";
            labelDev.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelSoftwareDescription
            // 
            labelSoftwareDescription.AutoSize = true;
            labelSoftwareDescription.Dock = DockStyle.Fill;
            labelSoftwareDescription.ForeColor = SystemColors.ControlLight;
            labelSoftwareDescription.Location = new Point(81, 0);
            labelSoftwareDescription.Margin = new Padding(2, 0, 2, 0);
            labelSoftwareDescription.Name = "labelSoftwareDescription";
            labelSoftwareDescription.Size = new Size(485, 20);
            labelSoftwareDescription.TabIndex = 5;
            labelSoftwareDescription.Text = "Console2Desk - is a tool for HandleOS.";
            labelSoftwareDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelVersionDescription
            // 
            labelVersionDescription.AutoSize = true;
            labelVersionDescription.Dock = DockStyle.Fill;
            labelVersionDescription.ForeColor = SystemColors.ControlLight;
            labelVersionDescription.Location = new Point(81, 20);
            labelVersionDescription.Margin = new Padding(2, 0, 2, 0);
            labelVersionDescription.Name = "labelVersionDescription";
            labelVersionDescription.Size = new Size(485, 20);
            labelVersionDescription.TabIndex = 6;
            labelVersionDescription.TextAlign = ContentAlignment.MiddleLeft;
            labelVersionDescription.Click += labelVersionDescription_Click;
            // 
            // labelCopyrightDescription
            // 
            labelCopyrightDescription.AutoSize = true;
            labelCopyrightDescription.Dock = DockStyle.Fill;
            labelCopyrightDescription.ForeColor = SystemColors.ControlLight;
            labelCopyrightDescription.Location = new Point(81, 40);
            labelCopyrightDescription.Margin = new Padding(2, 0, 2, 0);
            labelCopyrightDescription.Name = "labelCopyrightDescription";
            labelCopyrightDescription.Size = new Size(485, 20);
            labelCopyrightDescription.TabIndex = 7;
            labelCopyrightDescription.Text = "Copyright (C) 2023 Special-Niewbie Softwares (Under GNU GENERAL LICENSE).";
            labelCopyrightDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelDevDescription
            // 
            labelDevDescription.AutoSize = true;
            labelDevDescription.Dock = DockStyle.Fill;
            labelDevDescription.ForeColor = SystemColors.ControlLight;
            labelDevDescription.Location = new Point(81, 60);
            labelDevDescription.Margin = new Padding(2, 0, 2, 0);
            labelDevDescription.Name = "labelDevDescription";
            labelDevDescription.Size = new Size(485, 23);
            labelDevDescription.TabIndex = 8;
            labelDevDescription.Text = "Special-Niewbie";
            labelDevDescription.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.BackColor = Color.FromArgb(50, 50, 50);
            richTextBox1.ForeColor = SystemColors.ControlLight;
            richTextBox1.Location = new Point(80, 186);
            richTextBox1.Margin = new Padding(2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(470, 111);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = resources.GetString("richTextBox1.Text");
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // labelLicense
            // 
            labelLicense.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelLicense.AutoSize = true;
            labelLicense.ForeColor = SystemColors.ControlLight;
            labelLicense.Location = new Point(76, 169);
            labelLicense.Margin = new Padding(2, 0, 2, 0);
            labelLicense.Name = "labelLicense";
            labelLicense.Size = new Size(49, 15);
            labelLicense.TabIndex = 5;
            labelLicense.Text = "License:";
            labelLicense.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // labelVisitMySite
            // 
            labelVisitMySite.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelVisitMySite.AutoSize = true;
            labelVisitMySite.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            labelVisitMySite.ForeColor = SystemColors.ControlLight;
            labelVisitMySite.Location = new Point(20, 186);
            labelVisitMySite.Margin = new Padding(2, 0, 2, 0);
            labelVisitMySite.Name = "labelVisitMySite";
            labelVisitMySite.Size = new Size(45, 26);
            labelVisitMySite.TabIndex = 6;
            labelVisitMySite.Text = "My site\r\n below:";
            labelVisitMySite.TextAlign = ContentAlignment.MiddleCenter;
            labelVisitMySite.Click += labelVisitMySite_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = Properties.Resources.logo;
            pictureBox2.Location = new Point(13, 220);
            pictureBox2.Margin = new Padding(2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(55, 42);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // AboutBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 31);
            ClientSize = new Size(565, 302);
            Controls.Add(pictureBox2);
            Controls.Add(labelVisitMySite);
            Controls.Add(labelLicense);
            Controls.Add(richTextBox1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "AboutBox";
            StartPosition = FormStartPosition.CenterParent;
            Text = "About Console2Desk ";
            TopMost = true;
            Load += AboutBox_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelVersion;
        private Label labelSoftware;
        private Label labelCopyright;
        private Label labelDev;
        private Label labelSoftwareDescription;
        private Label labelVersionDescription;
        private Label labelCopyrightDescription;
        private Label labelDevDescription;
        private RichTextBox richTextBox1;
        private Label labelLicense;
        private Label labelVisitMySite;
        private PictureBox pictureBox2;
    }
}