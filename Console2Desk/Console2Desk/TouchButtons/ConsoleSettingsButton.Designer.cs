namespace Console2Desk.TouchButtons
{
    partial class ConsoleSettingsButton
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
            radioButtonPlaynite = new RadioButton();
            radioButtonSteam = new RadioButton();
            radioButtonCustomLauncher = new RadioButton();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            buttonSave = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // radioButtonPlaynite
            // 
            radioButtonPlaynite.AutoSize = true;
            radioButtonPlaynite.Cursor = Cursors.Hand;
            radioButtonPlaynite.FlatAppearance.BorderColor = Color.White;
            radioButtonPlaynite.FlatAppearance.BorderSize = 2;
            radioButtonPlaynite.FlatAppearance.CheckedBackColor = Color.MediumSlateBlue;
            radioButtonPlaynite.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonPlaynite.ForeColor = SystemColors.ControlLight;
            radioButtonPlaynite.ImageAlign = ContentAlignment.MiddleRight;
            radioButtonPlaynite.Location = new Point(70, 31);
            radioButtonPlaynite.Name = "radioButtonPlaynite";
            radioButtonPlaynite.Padding = new Padding(0, 0, 14, 0);
            radioButtonPlaynite.Size = new Size(132, 34);
            radioButtonPlaynite.TabIndex = 0;
            radioButtonPlaynite.Text = " Playnite ";
            radioButtonPlaynite.TextAlign = ContentAlignment.MiddleCenter;
            radioButtonPlaynite.TextImageRelation = TextImageRelation.TextBeforeImage;
            radioButtonPlaynite.UseVisualStyleBackColor = true;
            radioButtonPlaynite.CheckedChanged += radioButtonPlaynite_CheckedChanged;
            // 
            // radioButtonSteam
            // 
            radioButtonSteam.AutoSize = true;
            radioButtonSteam.Cursor = Cursors.Hand;
            radioButtonSteam.FlatAppearance.BorderColor = Color.White;
            radioButtonSteam.FlatAppearance.BorderSize = 2;
            radioButtonSteam.FlatAppearance.CheckedBackColor = Color.MediumSlateBlue;
            radioButtonSteam.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonSteam.ForeColor = SystemColors.ControlLight;
            radioButtonSteam.ImageAlign = ContentAlignment.MiddleRight;
            radioButtonSteam.Location = new Point(70, 115);
            radioButtonSteam.Name = "radioButtonSteam";
            radioButtonSteam.Size = new Size(109, 34);
            radioButtonSteam.TabIndex = 1;
            radioButtonSteam.Text = " Steam  ";
            radioButtonSteam.TextImageRelation = TextImageRelation.TextBeforeImage;
            radioButtonSteam.UseVisualStyleBackColor = true;
            radioButtonSteam.CheckedChanged += radioButtonSteam_CheckedChanged;
            // 
            // radioButtonCustomLauncher
            // 
            radioButtonCustomLauncher.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            radioButtonCustomLauncher.AutoSize = true;
            radioButtonCustomLauncher.Cursor = Cursors.Hand;
            radioButtonCustomLauncher.FlatAppearance.BorderColor = Color.White;
            radioButtonCustomLauncher.FlatAppearance.BorderSize = 2;
            radioButtonCustomLauncher.FlatAppearance.CheckedBackColor = Color.MediumSlateBlue;
            radioButtonCustomLauncher.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonCustomLauncher.ForeColor = SystemColors.ControlLight;
            radioButtonCustomLauncher.Location = new Point(70, 205);
            radioButtonCustomLauncher.Name = "radioButtonCustomLauncher";
            radioButtonCustomLauncher.Size = new Size(111, 34);
            radioButtonCustomLauncher.TabIndex = 2;
            radioButtonCustomLauncher.Text = " Custom";
            radioButtonCustomLauncher.TextAlign = ContentAlignment.MiddleRight;
            radioButtonCustomLauncher.UseVisualStyleBackColor = true;
            radioButtonCustomLauncher.CheckedChanged += radioButtonCustomLauncher_CheckedChanged;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = SystemColors.ButtonShadow;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(70, 252);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(410, 29);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.steam48;
            pictureBox1.Location = new Point(14, 106);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(48, 48);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.Playnite48;
            pictureBox2.Location = new Point(14, 22);
            pictureBox2.Margin = new Padding(2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(48, 48);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox3.Image = Properties.Resources.FileExplorer_48x48;
            pictureBox3.Location = new Point(14, 198);
            pictureBox3.Margin = new Padding(2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(48, 40);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // buttonSave
            // 
            buttonSave.Anchor = AnchorStyles.Bottom;
            buttonSave.BackColor = Color.FromArgb(40, 40, 40);
            buttonSave.FlatAppearance.MouseOverBackColor = Color.MediumSlateBlue;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSave.ForeColor = SystemColors.ControlLight;
            buttonSave.Location = new Point(194, 309);
            buttonSave.Margin = new Padding(2);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(129, 39);
            buttonSave.TabIndex = 8;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = false;
            buttonSave.Click += buttonSave_Click;
            // 
            // ConsoleSettingsButton
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(517, 362);
            Controls.Add(buttonSave);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(radioButtonCustomLauncher);
            Controls.Add(radioButtonSteam);
            Controls.Add(radioButtonPlaynite);
            MaximizeBox = false;
            Name = "ConsoleSettingsButton";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Choose default Launcher";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton radioButtonPlaynite;
        private RadioButton radioButtonSteam;
        private RadioButton radioButtonCustomLauncher;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Button buttonSave;
    }
}