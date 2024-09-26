namespace Console2Desk.FormMessageBox
{
    partial class FormMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMessageBox));
            panelTitleFormMessage = new Panel();
            buttonCloseWindowFormMessage = new Button();
            labelTitleFormMessage = new Label();
            listBoxFormMessage = new ListBox();
            checkBoxFormMessage = new CheckBox();
            special_Niewbie_ButtonStartRestore = new Special_Niewbie_Button();
            labelFormMessageInfo = new Label();
            labelListOfServices = new Label();
            special_Niewbie_ButtonFixWifi = new Special_Niewbie_Button();
            labelRestoreWiFi = new Label();
            panelBase = new Panel();
            panelTitleFormMessage.SuspendLayout();
            SuspendLayout();
            // 
            // panelTitleFormMessage
            // 
            panelTitleFormMessage.BackColor = Color.FromArgb(31, 31, 31);
            panelTitleFormMessage.Controls.Add(buttonCloseWindowFormMessage);
            panelTitleFormMessage.Controls.Add(labelTitleFormMessage);
            panelTitleFormMessage.Dock = DockStyle.Top;
            panelTitleFormMessage.ForeColor = SystemColors.ControlLightLight;
            panelTitleFormMessage.Location = new Point(0, 0);
            panelTitleFormMessage.Name = "panelTitleFormMessage";
            panelTitleFormMessage.Size = new Size(656, 56);
            panelTitleFormMessage.TabIndex = 0;
            panelTitleFormMessage.Paint += panelTitleFormMessage_Paint;
            // 
            // buttonCloseWindowFormMessage
            // 
            buttonCloseWindowFormMessage.BackColor = Color.Transparent;
            buttonCloseWindowFormMessage.FlatAppearance.BorderSize = 0;
            buttonCloseWindowFormMessage.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 0, 0);
            buttonCloseWindowFormMessage.FlatStyle = FlatStyle.Flat;
            buttonCloseWindowFormMessage.Font = new Font("Roboto Light", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCloseWindowFormMessage.ForeColor = Color.Red;
            buttonCloseWindowFormMessage.Location = new Point(581, 0);
            buttonCloseWindowFormMessage.Name = "buttonCloseWindowFormMessage";
            buttonCloseWindowFormMessage.Size = new Size(75, 56);
            buttonCloseWindowFormMessage.TabIndex = 1;
            buttonCloseWindowFormMessage.Text = "X";
            buttonCloseWindowFormMessage.UseVisualStyleBackColor = false;
            buttonCloseWindowFormMessage.Click += buttonCloseWindowFormMessage_Click;
            // 
            // labelTitleFormMessage
            // 
            labelTitleFormMessage.AutoSize = true;
            labelTitleFormMessage.Font = new Font("Roboto Medium", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitleFormMessage.Location = new Point(161, 12);
            labelTitleFormMessage.Name = "labelTitleFormMessage";
            labelTitleFormMessage.Size = new Size(334, 33);
            labelTitleFormMessage.TabIndex = 0;
            labelTitleFormMessage.Text = "HandleOS Restore Boost";
            labelTitleFormMessage.Click += labelTitleFormMessage_Click;
            // 
            // listBoxFormMessage
            // 
            listBoxFormMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listBoxFormMessage.BackColor = Color.FromArgb(60, 60, 60);
            listBoxFormMessage.BorderStyle = BorderStyle.FixedSingle;
            listBoxFormMessage.ForeColor = SystemColors.ControlLightLight;
            listBoxFormMessage.FormattingEnabled = true;
            listBoxFormMessage.ItemHeight = 15;
            listBoxFormMessage.Location = new Point(12, 132);
            listBoxFormMessage.Name = "listBoxFormMessage";
            listBoxFormMessage.Size = new Size(632, 182);
            listBoxFormMessage.TabIndex = 1;
            listBoxFormMessage.SelectedIndexChanged += listBoxFormMessage_SelectedIndexChanged;
            // 
            // checkBoxFormMessage
            // 
            checkBoxFormMessage.AutoSize = true;
            checkBoxFormMessage.Checked = true;
            checkBoxFormMessage.CheckState = CheckState.Checked;
            checkBoxFormMessage.Font = new Font("Roboto Light", 12F, FontStyle.Regular, GraphicsUnit.Point);
            checkBoxFormMessage.ForeColor = SystemColors.ControlLightLight;
            checkBoxFormMessage.Location = new Point(12, 320);
            checkBoxFormMessage.Name = "checkBoxFormMessage";
            checkBoxFormMessage.Size = new Size(260, 25);
            checkBoxFormMessage.TabIndex = 2;
            checkBoxFormMessage.Text = "Delete Microsoft Edge - OneDrive";
            checkBoxFormMessage.UseVisualStyleBackColor = true;
            checkBoxFormMessage.CheckedChanged += checkBoxFormMessage_CheckedChanged;
            // 
            // special_Niewbie_ButtonStartRestore
            // 
            special_Niewbie_ButtonStartRestore.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            special_Niewbie_ButtonStartRestore.BackColor = Color.FromArgb(50, 50, 50);
            special_Niewbie_ButtonStartRestore.BorderColor = Color.FromArgb(227, 227, 227);
            special_Niewbie_ButtonStartRestore.BorderRadius = 20;
            special_Niewbie_ButtonStartRestore.BorderSize = 1;
            special_Niewbie_ButtonStartRestore.FlatAppearance.BorderSize = 0;
            special_Niewbie_ButtonStartRestore.FlatStyle = FlatStyle.Flat;
            special_Niewbie_ButtonStartRestore.ForeColor = Color.White;
            special_Niewbie_ButtonStartRestore.HoverColor = Color.MediumSlateBlue;
            special_Niewbie_ButtonStartRestore.Location = new Point(546, 369);
            special_Niewbie_ButtonStartRestore.Name = "special_Niewbie_ButtonStartRestore";
            special_Niewbie_ButtonStartRestore.Size = new Size(98, 47);
            special_Niewbie_ButtonStartRestore.TabIndex = 3;
            special_Niewbie_ButtonStartRestore.Text = "Start\r\nRestore";
            special_Niewbie_ButtonStartRestore.TextColor = Color.White;
            special_Niewbie_ButtonStartRestore.UseVisualStyleBackColor = false;
            special_Niewbie_ButtonStartRestore.Click += special_Niewbie_ButtonStartRestore_Click;
            // 
            // labelFormMessageInfo
            // 
            labelFormMessageInfo.AutoSize = true;
            labelFormMessageInfo.Font = new Font("Roboto Light", 8F, FontStyle.Regular, GraphicsUnit.Point);
            labelFormMessageInfo.ForeColor = SystemColors.Control;
            labelFormMessageInfo.Location = new Point(12, 59);
            labelFormMessageInfo.Name = "labelFormMessageInfo";
            labelFormMessageInfo.Size = new Size(571, 45);
            labelFormMessageInfo.TabIndex = 4;
            labelFormMessageInfo.Text = resources.GetString("labelFormMessageInfo.Text");
            // 
            // labelListOfServices
            // 
            labelListOfServices.AutoSize = true;
            labelListOfServices.ForeColor = SystemColors.ButtonShadow;
            labelListOfServices.Location = new Point(12, 114);
            labelListOfServices.Name = "labelListOfServices";
            labelListOfServices.Size = new Size(176, 15);
            labelListOfServices.TabIndex = 5;
            labelListOfServices.Text = "List of Services to Restore Boost:";
            // 
            // special_Niewbie_ButtonFixWifi
            // 
            special_Niewbie_ButtonFixWifi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            special_Niewbie_ButtonFixWifi.BackColor = Color.FromArgb(50, 50, 50);
            special_Niewbie_ButtonFixWifi.BorderColor = Color.FromArgb(227, 227, 227);
            special_Niewbie_ButtonFixWifi.BorderRadius = 20;
            special_Niewbie_ButtonFixWifi.BorderSize = 1;
            special_Niewbie_ButtonFixWifi.FlatAppearance.BorderSize = 0;
            special_Niewbie_ButtonFixWifi.FlatStyle = FlatStyle.Flat;
            special_Niewbie_ButtonFixWifi.ForeColor = Color.White;
            special_Niewbie_ButtonFixWifi.HoverColor = Color.MediumSlateBlue;
            special_Niewbie_ButtonFixWifi.Location = new Point(12, 375);
            special_Niewbie_ButtonFixWifi.Name = "special_Niewbie_ButtonFixWifi";
            special_Niewbie_ButtonFixWifi.Size = new Size(87, 39);
            special_Niewbie_ButtonFixWifi.TabIndex = 6;
            special_Niewbie_ButtonFixWifi.Text = "Fix WIFI";
            special_Niewbie_ButtonFixWifi.TextColor = Color.White;
            special_Niewbie_ButtonFixWifi.UseVisualStyleBackColor = false;
            special_Niewbie_ButtonFixWifi.Click += special_Niewbie_ButtonFixWifi_Click;
            // 
            // labelRestoreWiFi
            // 
            labelRestoreWiFi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelRestoreWiFi.AutoSize = true;
            labelRestoreWiFi.ForeColor = SystemColors.Control;
            labelRestoreWiFi.Location = new Point(105, 379);
            labelRestoreWiFi.Name = "labelRestoreWiFi";
            labelRestoreWiFi.Size = new Size(240, 30);
            labelRestoreWiFi.TabIndex = 7;
            labelRestoreWiFi.Text = "After apply the Restore Boost If the WIFI will \r\nnot work or disappear press this button.";
            // 
            // panelBase
            // 
            panelBase.BorderStyle = BorderStyle.FixedSingle;
            panelBase.Dock = DockStyle.Fill;
            panelBase.Location = new Point(0, 0);
            panelBase.Name = "panelBase";
            panelBase.Size = new Size(656, 428);
            panelBase.TabIndex = 8;
            // 
            // FormMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(656, 428);
            Controls.Add(labelRestoreWiFi);
            Controls.Add(special_Niewbie_ButtonFixWifi);
            Controls.Add(labelListOfServices);
            Controls.Add(labelFormMessageInfo);
            Controls.Add(special_Niewbie_ButtonStartRestore);
            Controls.Add(checkBoxFormMessage);
            Controls.Add(listBoxFormMessage);
            Controls.Add(panelTitleFormMessage);
            Controls.Add(panelBase);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMessageBox";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormMessageBox";
            TopMost = true;
            Load += FormMessageBox_Load;
            panelTitleFormMessage.ResumeLayout(false);
            panelTitleFormMessage.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelTitleFormMessage;
        private Label labelTitleFormMessage;
        private Button buttonCloseWindowFormMessage;
        private ListBox listBoxFormMessage;
        private CheckBox checkBoxFormMessage;
        private Special_Niewbie_Button special_Niewbie_ButtonStartRestore;
        private Label labelFormMessageInfo;
        private Label labelListOfServices;
        private Special_Niewbie_Button special_Niewbie_ButtonFixWifi;
        private Label labelRestoreWiFi;
        private Panel panelBase;
    }
}