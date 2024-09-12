namespace HandleOS_Benchmark
{
    partial class FormBench
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
            buttonClose = new Button();
            customProgressBar1 = new CustomProgressBarResult();
            SuspendLayout();
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.ForeColor = Color.Red;
            buttonClose.Location = new Point(726, 0);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(75, 36);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "X";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.UseWaitCursor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // customProgressBar1
            // 
            customProgressBar1.BackColor = Color.Transparent;
            customProgressBar1.BackgroundColor = Color.FromArgb(40, 0, 0, 0);
            customProgressBar1.BarText = "";
            customProgressBar1.BorderThickness = 1F;
            customProgressBar1.EndColor = Color.FromArgb(104, 178, 234);
            customProgressBar1.FontColor = Color.White;
            customProgressBar1.FontSize = 7;
            customProgressBar1.GlowColor = Color.FromArgb(60, 255, 255, 255);
            customProgressBar1.GlowSpeed = 0.2F;
            customProgressBar1.HighlightColor = Color.WhiteSmoke;
            customProgressBar1.IsGlowEnabled = true;
            customProgressBar1.Location = new Point(305, 214);
            customProgressBar1.MaxValue = 100;
            customProgressBar1.MinValue = 0;
            customProgressBar1.Name = "customProgressBar1";
            customProgressBar1.RadiusBottomLeft = 8;
            customProgressBar1.RadiusBottomRight = 8;
            customProgressBar1.RadiusTopLeft = 8;
            customProgressBar1.RadiusTopRight = 8;
            customProgressBar1.ShadowColor = Color.FromArgb(10, 0, 0, 0);
            customProgressBar1.Size = new Size(200, 15);
            customProgressBar1.StartColor = Color.FromArgb(167, 136, 235);
            customProgressBar1.TabIndex = 2;
            customProgressBar1.Text = "customProgressBarResult1";
            customProgressBar1.UseWaitCursor = true;
            customProgressBar1.Value = 0;
            customProgressBar1.Click += customProgressBar1_Click;
            // 
            // FormBench
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            ClientSize = new Size(800, 450);
            Controls.Add(customProgressBar1);
            Controls.Add(buttonClose);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormBench";
            Opacity = 0.8D;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormBench";
            TopMost = true;
            UseWaitCursor = true;
            WindowState = FormWindowState.Maximized;
            Load += FormBench_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button buttonClose;
        private CustomProgressBarResult customProgressBar1;
    }
}