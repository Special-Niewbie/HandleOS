namespace HandleOS_Benchmark
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            buttonCloseWindow = new Button();
            panelTitle = new Panel();
            label1 = new Label();
            comboBoxTheme = new CustomComboBoxNiewbie();
            panelCentral = new Panel();
            roundedPanelAverageScore = new RoundedPanel();
            pictureBoxConsole = new PictureBox();
            customProgressBarResult = new CustomProgressBarResult();
            customProgressBarDirect3D = new CustomProgressBar();
            customProgressBarGPU = new CustomProgressBar();
            customProgressBarSSD = new CustomProgressBar();
            customProgressBarRAM = new CustomProgressBar();
            customProgressBarCPU = new CustomProgressBar();
            labelProductDeatils = new Label();
            labelNameProduct = new Label();
            labelNameTitle = new Label();
            labelSubScoreRAM = new Label();
            labelSubScoreGPU = new Label();
            labelSubScoreSSD = new Label();
            labelSubScoreDirect3D = new Label();
            labelSubScoreCPU = new Label();
            labelRAM = new Label();
            labelGPU = new Label();
            labelSSD = new Label();
            labelDirect3D = new Label();
            labelCPU = new Label();
            customButtonNiewbieClearData = new CustomButtonNiewbie();
            pictureBoxCamera = new PictureBox();
            labelSlogan = new Label();
            label2 = new Label();
            labelAverageScore = new Label();
            labelPanelSubScore = new Label();
            pictureBoxButton4FormSubScores = new PictureBox();
            pictureBoxButton4FormSubScoresPress = new PictureBox();
            roundedPanel4LabelTitleScore = new RoundedPanel();
            roundedPanelSubScores = new RoundedPanel();
            roundedPanelTheme = new RoundedPanel();
            roundedPanelComponents = new RoundedPanel();
            customButtonNiewbieSTART = new CustomButtonNiewbie();
            labelDate = new Label();
            panelTitle.SuspendLayout();
            panelCentral.SuspendLayout();
            roundedPanelAverageScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxConsole).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCamera).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxButton4FormSubScores).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxButton4FormSubScoresPress).BeginInit();
            SuspendLayout();
            // 
            // buttonCloseWindow
            // 
            buttonCloseWindow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCloseWindow.BackColor = Color.Transparent;
            buttonCloseWindow.FlatAppearance.BorderSize = 0;
            buttonCloseWindow.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 0, 0);
            buttonCloseWindow.FlatStyle = FlatStyle.Flat;
            buttonCloseWindow.Font = new Font("Roboto Light", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            buttonCloseWindow.ForeColor = Color.Red;
            buttonCloseWindow.Location = new Point(737, 0);
            buttonCloseWindow.Name = "buttonCloseWindow";
            buttonCloseWindow.Size = new Size(75, 46);
            buttonCloseWindow.TabIndex = 0;
            buttonCloseWindow.Text = "X";
            buttonCloseWindow.UseVisualStyleBackColor = false;
            buttonCloseWindow.Click += buttonCloseWindow_Click;
            // 
            // panelTitle
            // 
            panelTitle.BackColor = Color.FromArgb(248, 249, 253);
            panelTitle.Controls.Add(label1);
            panelTitle.Controls.Add(buttonCloseWindow);
            panelTitle.Dock = DockStyle.Top;
            panelTitle.Location = new Point(0, 0);
            panelTitle.Name = "panelTitle";
            panelTitle.Size = new Size(812, 46);
            panelTitle.TabIndex = 1;
            panelTitle.Paint += panelTitle_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Roboto Medium", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.FromArgb(51, 51, 52);
            label1.Location = new Point(257, 7);
            label1.Name = "label1";
            label1.Size = new Size(298, 33);
            label1.TabIndex = 1;
            label1.Text = "HandleOS Benchmark";
            label1.Click += label1_Click;
            // 
            // comboBoxTheme
            // 
            comboBoxTheme.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            comboBoxTheme.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxTheme.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxTheme.BackColor = Color.WhiteSmoke;
            comboBoxTheme.BorderColor = Color.FromArgb(101, 125, 156);
            comboBoxTheme.BorderRadius = 15;
            comboBoxTheme.BorderSize = 0;
            comboBoxTheme.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxTheme.Font = new Font("Roboto Medium", 12F, FontStyle.Bold, GraphicsUnit.Point);
            comboBoxTheme.ForeColor = Color.FromArgb(89, 89, 89);
            comboBoxTheme.IconColor = Color.FromArgb(101, 125, 156);
            comboBoxTheme.ListBackColor = Color.FromArgb(230, 228, 245);
            comboBoxTheme.ListTextColor = Color.DimGray;
            comboBoxTheme.Location = new Point(12, 531);
            comboBoxTheme.MinimumSize = new Size(120, 30);
            comboBoxTheme.Name = "comboBoxTheme";
            comboBoxTheme.Size = new Size(120, 30);
            comboBoxTheme.TabIndex = 2;
            comboBoxTheme.Texts = "";
            comboBoxTheme.OnSelectedIndexChanged += comboBoxTheme_OnSelectedIndexChanged;
            // 
            // panelCentral
            // 
            panelCentral.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelCentral.BackColor = Color.FromArgb(251, 251, 251);
            panelCentral.Controls.Add(roundedPanelAverageScore);
            panelCentral.Controls.Add(customProgressBarDirect3D);
            panelCentral.Controls.Add(customProgressBarGPU);
            panelCentral.Controls.Add(customProgressBarSSD);
            panelCentral.Controls.Add(customProgressBarRAM);
            panelCentral.Controls.Add(customProgressBarCPU);
            panelCentral.Controls.Add(labelProductDeatils);
            panelCentral.Controls.Add(labelNameProduct);
            panelCentral.Controls.Add(labelNameTitle);
            panelCentral.Controls.Add(labelSubScoreRAM);
            panelCentral.Controls.Add(labelSubScoreGPU);
            panelCentral.Controls.Add(labelSubScoreSSD);
            panelCentral.Controls.Add(labelSubScoreDirect3D);
            panelCentral.Controls.Add(labelSubScoreCPU);
            panelCentral.Controls.Add(labelRAM);
            panelCentral.Controls.Add(labelGPU);
            panelCentral.Controls.Add(labelSSD);
            panelCentral.Controls.Add(labelDirect3D);
            panelCentral.Controls.Add(labelCPU);
            panelCentral.Font = new Font("Roboto Medium", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            panelCentral.Location = new Point(13, 133);
            panelCentral.Name = "panelCentral";
            panelCentral.Size = new Size(787, 374);
            panelCentral.TabIndex = 2;
            panelCentral.Paint += panelCentral_Paint;
            // 
            // roundedPanelAverageScore
            // 
            roundedPanelAverageScore.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            roundedPanelAverageScore.BackColor = Color.Transparent;
            roundedPanelAverageScore.BarText = "";
            roundedPanelAverageScore.BorderColor = Color.LightGray;
            roundedPanelAverageScore.BorderSize = 1;
            roundedPanelAverageScore.BottomLeftRadius = 30;
            roundedPanelAverageScore.BottomRightRadius = 30;
            roundedPanelAverageScore.Controls.Add(pictureBoxConsole);
            roundedPanelAverageScore.Controls.Add(customProgressBarResult);
            roundedPanelAverageScore.FontColor = Color.Black;
            roundedPanelAverageScore.FontSize = 7;
            roundedPanelAverageScore.ForeColor = Color.Black;
            roundedPanelAverageScore.GradientAngle = 90F;
            roundedPanelAverageScore.GradientBottomColor = Color.FromArgb(251, 251, 251);
            roundedPanelAverageScore.GradientOpacity = 1F;
            roundedPanelAverageScore.GradientTopColor = Color.FromArgb(251, 251, 251);
            roundedPanelAverageScore.InternalMargin = 1;
            roundedPanelAverageScore.Location = new Point(450, 37);
            roundedPanelAverageScore.Name = "roundedPanelAverageScore";
            roundedPanelAverageScore.ShadowColor = Color.FromArgb(10, 0, 0, 0);
            roundedPanelAverageScore.ShadowOffsetX = 2;
            roundedPanelAverageScore.ShadowOffsetY = 1;
            roundedPanelAverageScore.ShadowSize = 1;
            roundedPanelAverageScore.Size = new Size(334, 318);
            roundedPanelAverageScore.TabIndex = 26;
            roundedPanelAverageScore.TopLeftRadius = 30;
            roundedPanelAverageScore.TopRightRadius = 30;
            roundedPanelAverageScore.Paint += roundedPanelAverageScore_Paint;
            // 
            // pictureBoxConsole
            // 
            pictureBoxConsole.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxConsole.Location = new Point(110, 3);
            pictureBoxConsole.Name = "pictureBoxConsole";
            pictureBoxConsole.Size = new Size(120, 80);
            pictureBoxConsole.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxConsole.TabIndex = 1;
            pictureBoxConsole.TabStop = false;
            pictureBoxConsole.Click += pictureBoxConsole_Click;
            // 
            // customProgressBarResult
            // 
            customProgressBarResult.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            customProgressBarResult.BackColor = Color.Transparent;
            customProgressBarResult.BackgroundColor = Color.FromArgb(251, 251, 251);
            customProgressBarResult.BarText = "";
            customProgressBarResult.BorderThickness = 0.2F;
            customProgressBarResult.EndColor = Color.FromArgb(104, 178, 234);
            customProgressBarResult.FontColor = Color.White;
            customProgressBarResult.FontSize = 50;
            customProgressBarResult.GlowColor = Color.FromArgb(60, 255, 255, 255);
            customProgressBarResult.GlowSpeed = 0.2F;
            customProgressBarResult.HighlightColor = Color.WhiteSmoke;
            customProgressBarResult.IsGlowEnabled = true;
            customProgressBarResult.Location = new Point(29, 102);
            customProgressBarResult.MaxValue = 100;
            customProgressBarResult.MinValue = 0;
            customProgressBarResult.Name = "customProgressBarResult";
            customProgressBarResult.RadiusBottomLeft = 8;
            customProgressBarResult.RadiusBottomRight = 8;
            customProgressBarResult.RadiusTopLeft = 8;
            customProgressBarResult.RadiusTopRight = 8;
            customProgressBarResult.ShadowColor = Color.FromArgb(10, 0, 0, 0);
            customProgressBarResult.Size = new Size(280, 194);
            customProgressBarResult.StartColor = Color.FromArgb(167, 136, 235);
            customProgressBarResult.TabIndex = 0;
            customProgressBarResult.Text = "customProgressBarResult";
            customProgressBarResult.Value = 100;
            customProgressBarResult.Click += customProgressBarResult_Click;
            // 
            // customProgressBarDirect3D
            // 
            customProgressBarDirect3D.BackColor = Color.Transparent;
            customProgressBarDirect3D.BackgroundColor = Color.FromArgb(251, 251, 251);
            customProgressBarDirect3D.BarText = "";
            customProgressBarDirect3D.BorderThickness = 0.2F;
            customProgressBarDirect3D.EndColor = Color.FromArgb(104, 178, 234);
            customProgressBarDirect3D.FontColor = Color.White;
            customProgressBarDirect3D.FontPositionX = 5;
            customProgressBarDirect3D.FontSize = 7;
            customProgressBarDirect3D.GlowColor = Color.FromArgb(60, 255, 255, 255);
            customProgressBarDirect3D.GlowSpeed = 0.2F;
            customProgressBarDirect3D.HighlightColor = Color.WhiteSmoke;
            customProgressBarDirect3D.IsGlowEnabled = true;
            customProgressBarDirect3D.Location = new Point(5, 336);
            customProgressBarDirect3D.MaxValue = 100;
            customProgressBarDirect3D.MinValue = 0;
            customProgressBarDirect3D.Name = "customProgressBarDirect3D";
            customProgressBarDirect3D.RadiusBottomLeft = 8;
            customProgressBarDirect3D.RadiusBottomRight = 8;
            customProgressBarDirect3D.RadiusTopLeft = 8;
            customProgressBarDirect3D.RadiusTopRight = 8;
            customProgressBarDirect3D.ShadowColor = Color.FromArgb(10, 0, 0, 0);
            customProgressBarDirect3D.Size = new Size(381, 15);
            customProgressBarDirect3D.StartColor = Color.FromArgb(167, 136, 235);
            customProgressBarDirect3D.TabIndex = 25;
            customProgressBarDirect3D.Text = "customProgressBar1";
            customProgressBarDirect3D.Value = 0;
            customProgressBarDirect3D.Click += customProgressBarDirect3D_Click;
            // 
            // customProgressBarGPU
            // 
            customProgressBarGPU.BackColor = Color.Transparent;
            customProgressBarGPU.BackgroundColor = Color.FromArgb(251, 251, 251);
            customProgressBarGPU.BarText = "";
            customProgressBarGPU.BorderThickness = 0.2F;
            customProgressBarGPU.EndColor = Color.FromArgb(104, 178, 234);
            customProgressBarGPU.FontColor = Color.White;
            customProgressBarGPU.FontPositionX = 5;
            customProgressBarGPU.FontSize = 7;
            customProgressBarGPU.GlowColor = Color.FromArgb(60, 255, 255, 255);
            customProgressBarGPU.GlowSpeed = 0.2F;
            customProgressBarGPU.HighlightColor = Color.WhiteSmoke;
            customProgressBarGPU.IsGlowEnabled = true;
            customProgressBarGPU.Location = new Point(5, 292);
            customProgressBarGPU.MaxValue = 100;
            customProgressBarGPU.MinValue = 0;
            customProgressBarGPU.Name = "customProgressBarGPU";
            customProgressBarGPU.RadiusBottomLeft = 8;
            customProgressBarGPU.RadiusBottomRight = 8;
            customProgressBarGPU.RadiusTopLeft = 8;
            customProgressBarGPU.RadiusTopRight = 8;
            customProgressBarGPU.ShadowColor = Color.FromArgb(10, 0, 0, 0);
            customProgressBarGPU.Size = new Size(381, 15);
            customProgressBarGPU.StartColor = Color.FromArgb(167, 136, 235);
            customProgressBarGPU.TabIndex = 24;
            customProgressBarGPU.Text = "customProgressBar1";
            customProgressBarGPU.Value = 0;
            customProgressBarGPU.Click += customProgressBarGPU_Click;
            // 
            // customProgressBarSSD
            // 
            customProgressBarSSD.BackColor = Color.Transparent;
            customProgressBarSSD.BackgroundColor = Color.FromArgb(251, 251, 251);
            customProgressBarSSD.BarText = "";
            customProgressBarSSD.BorderThickness = 0.2F;
            customProgressBarSSD.EndColor = Color.FromArgb(104, 178, 234);
            customProgressBarSSD.FontColor = Color.White;
            customProgressBarSSD.FontPositionX = 5;
            customProgressBarSSD.FontSize = 7;
            customProgressBarSSD.GlowColor = Color.FromArgb(60, 255, 255, 255);
            customProgressBarSSD.GlowSpeed = 0.2F;
            customProgressBarSSD.HighlightColor = Color.WhiteSmoke;
            customProgressBarSSD.IsGlowEnabled = true;
            customProgressBarSSD.Location = new Point(5, 248);
            customProgressBarSSD.MaxValue = 100;
            customProgressBarSSD.MinValue = 0;
            customProgressBarSSD.Name = "customProgressBarSSD";
            customProgressBarSSD.RadiusBottomLeft = 8;
            customProgressBarSSD.RadiusBottomRight = 8;
            customProgressBarSSD.RadiusTopLeft = 8;
            customProgressBarSSD.RadiusTopRight = 8;
            customProgressBarSSD.ShadowColor = Color.FromArgb(10, 0, 0, 0);
            customProgressBarSSD.Size = new Size(381, 15);
            customProgressBarSSD.StartColor = Color.FromArgb(167, 136, 235);
            customProgressBarSSD.TabIndex = 23;
            customProgressBarSSD.Text = "customProgressBar1";
            customProgressBarSSD.Value = 0;
            customProgressBarSSD.Click += customProgressBarSSD_Click;
            // 
            // customProgressBarRAM
            // 
            customProgressBarRAM.BackColor = Color.Transparent;
            customProgressBarRAM.BackgroundColor = Color.FromArgb(251, 251, 251);
            customProgressBarRAM.BarText = "";
            customProgressBarRAM.BorderThickness = 0.2F;
            customProgressBarRAM.EndColor = Color.FromArgb(104, 178, 234);
            customProgressBarRAM.FontColor = Color.White;
            customProgressBarRAM.FontPositionX = 5;
            customProgressBarRAM.FontSize = 7;
            customProgressBarRAM.GlowColor = Color.FromArgb(60, 255, 255, 255);
            customProgressBarRAM.GlowSpeed = 0.2F;
            customProgressBarRAM.HighlightColor = Color.WhiteSmoke;
            customProgressBarRAM.IsGlowEnabled = true;
            customProgressBarRAM.Location = new Point(5, 204);
            customProgressBarRAM.MaxValue = 100;
            customProgressBarRAM.MinValue = 0;
            customProgressBarRAM.Name = "customProgressBarRAM";
            customProgressBarRAM.RadiusBottomLeft = 8;
            customProgressBarRAM.RadiusBottomRight = 8;
            customProgressBarRAM.RadiusTopLeft = 8;
            customProgressBarRAM.RadiusTopRight = 8;
            customProgressBarRAM.ShadowColor = Color.FromArgb(10, 0, 0, 0);
            customProgressBarRAM.Size = new Size(381, 15);
            customProgressBarRAM.StartColor = Color.FromArgb(167, 136, 235);
            customProgressBarRAM.TabIndex = 22;
            customProgressBarRAM.Text = "customProgressBar1";
            customProgressBarRAM.Value = 0;
            customProgressBarRAM.Click += customProgressBarRAM_Click;
            // 
            // customProgressBarCPU
            // 
            customProgressBarCPU.BackColor = Color.Transparent;
            customProgressBarCPU.BackgroundColor = Color.FromArgb(251, 251, 251);
            customProgressBarCPU.BarText = "";
            customProgressBarCPU.BorderThickness = 0.2F;
            customProgressBarCPU.EndColor = Color.FromArgb(104, 178, 234);
            customProgressBarCPU.FontColor = Color.White;
            customProgressBarCPU.FontPositionX = 5;
            customProgressBarCPU.FontSize = 7;
            customProgressBarCPU.GlowColor = Color.FromArgb(60, 255, 255, 255);
            customProgressBarCPU.GlowSpeed = 0.2F;
            customProgressBarCPU.HighlightColor = Color.WhiteSmoke;
            customProgressBarCPU.IsGlowEnabled = true;
            customProgressBarCPU.Location = new Point(5, 160);
            customProgressBarCPU.MaxValue = 100;
            customProgressBarCPU.MinValue = 0;
            customProgressBarCPU.Name = "customProgressBarCPU";
            customProgressBarCPU.RadiusBottomLeft = 8;
            customProgressBarCPU.RadiusBottomRight = 8;
            customProgressBarCPU.RadiusTopLeft = 8;
            customProgressBarCPU.RadiusTopRight = 8;
            customProgressBarCPU.ShadowColor = Color.FromArgb(10, 0, 0, 0);
            customProgressBarCPU.Size = new Size(381, 15);
            customProgressBarCPU.StartColor = Color.FromArgb(167, 136, 235);
            customProgressBarCPU.TabIndex = 21;
            customProgressBarCPU.Text = "customProgressBar1";
            customProgressBarCPU.Value = 0;
            customProgressBarCPU.Click += customProgressBarCPU_Click;
            // 
            // labelProductDeatils
            // 
            labelProductDeatils.AutoSize = true;
            labelProductDeatils.Font = new Font("Roboto Light", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelProductDeatils.Location = new Point(5, 63);
            labelProductDeatils.Name = "labelProductDeatils";
            labelProductDeatils.Size = new Size(29, 16);
            labelProductDeatils.TabIndex = 20;
            labelProductDeatils.Text = "Null";
            labelProductDeatils.Click += labelProductDeatils_Click;
            // 
            // labelNameProduct
            // 
            labelNameProduct.AutoSize = true;
            labelNameProduct.Font = new Font("Roboto Light", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelNameProduct.Location = new Point(73, 36);
            labelNameProduct.Name = "labelNameProduct";
            labelNameProduct.Size = new Size(47, 26);
            labelNameProduct.TabIndex = 18;
            labelNameProduct.Text = "Null";
            labelNameProduct.Click += labelNameProduct_Click;
            // 
            // labelNameTitle
            // 
            labelNameTitle.AutoSize = true;
            labelNameTitle.Font = new Font("Roboto", 15F, FontStyle.Regular, GraphicsUnit.Point);
            labelNameTitle.Location = new Point(3, 37);
            labelNameTitle.Name = "labelNameTitle";
            labelNameTitle.Size = new Size(69, 24);
            labelNameTitle.TabIndex = 17;
            labelNameTitle.Text = "Name:";
            // 
            // labelSubScoreRAM
            // 
            labelSubScoreRAM.AutoSize = true;
            labelSubScoreRAM.Font = new Font("Roboto Light", 13F, FontStyle.Regular, GraphicsUnit.Point);
            labelSubScoreRAM.ForeColor = Color.FromArgb(51, 51, 52);
            labelSubScoreRAM.Location = new Point(392, 199);
            labelSubScoreRAM.Name = "labelSubScoreRAM";
            labelSubScoreRAM.Size = new Size(20, 24);
            labelSubScoreRAM.TabIndex = 16;
            labelSubScoreRAM.Text = "1";
            labelSubScoreRAM.TextAlign = ContentAlignment.MiddleCenter;
            labelSubScoreRAM.Click += labelSubScoreRAM_Click;
            // 
            // labelSubScoreGPU
            // 
            labelSubScoreGPU.AutoSize = true;
            labelSubScoreGPU.Font = new Font("Roboto Light", 13F, FontStyle.Regular, GraphicsUnit.Point);
            labelSubScoreGPU.ForeColor = Color.FromArgb(51, 51, 52);
            labelSubScoreGPU.Location = new Point(392, 287);
            labelSubScoreGPU.Name = "labelSubScoreGPU";
            labelSubScoreGPU.Size = new Size(20, 24);
            labelSubScoreGPU.TabIndex = 15;
            labelSubScoreGPU.Text = "1";
            labelSubScoreGPU.TextAlign = ContentAlignment.MiddleCenter;
            labelSubScoreGPU.Click += labelSubScoreGPU_Click;
            // 
            // labelSubScoreSSD
            // 
            labelSubScoreSSD.AutoSize = true;
            labelSubScoreSSD.Font = new Font("Roboto Light", 13F, FontStyle.Regular, GraphicsUnit.Point);
            labelSubScoreSSD.ForeColor = Color.FromArgb(51, 51, 52);
            labelSubScoreSSD.Location = new Point(392, 243);
            labelSubScoreSSD.Name = "labelSubScoreSSD";
            labelSubScoreSSD.Size = new Size(20, 24);
            labelSubScoreSSD.TabIndex = 14;
            labelSubScoreSSD.Text = "1";
            labelSubScoreSSD.TextAlign = ContentAlignment.MiddleCenter;
            labelSubScoreSSD.Click += labelSubScoreSSD_Click;
            // 
            // labelSubScoreDirect3D
            // 
            labelSubScoreDirect3D.AutoSize = true;
            labelSubScoreDirect3D.Font = new Font("Roboto Light", 13F, FontStyle.Regular, GraphicsUnit.Point);
            labelSubScoreDirect3D.ForeColor = Color.FromArgb(51, 51, 52);
            labelSubScoreDirect3D.Location = new Point(392, 331);
            labelSubScoreDirect3D.Name = "labelSubScoreDirect3D";
            labelSubScoreDirect3D.Size = new Size(20, 24);
            labelSubScoreDirect3D.TabIndex = 13;
            labelSubScoreDirect3D.Text = "1";
            labelSubScoreDirect3D.TextAlign = ContentAlignment.MiddleCenter;
            labelSubScoreDirect3D.Click += labelSubScoreDirect3D_Click;
            // 
            // labelSubScoreCPU
            // 
            labelSubScoreCPU.AutoSize = true;
            labelSubScoreCPU.Font = new Font("Roboto Light", 13F, FontStyle.Regular, GraphicsUnit.Point);
            labelSubScoreCPU.ForeColor = Color.FromArgb(51, 51, 52);
            labelSubScoreCPU.Location = new Point(392, 156);
            labelSubScoreCPU.Name = "labelSubScoreCPU";
            labelSubScoreCPU.Size = new Size(20, 24);
            labelSubScoreCPU.TabIndex = 12;
            labelSubScoreCPU.Text = "1";
            labelSubScoreCPU.TextAlign = ContentAlignment.MiddleCenter;
            labelSubScoreCPU.Click += labelSubScoreCPU_Click;
            // 
            // labelRAM
            // 
            labelRAM.AutoSize = true;
            labelRAM.Font = new Font("Roboto Medium", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelRAM.Location = new Point(5, 183);
            labelRAM.Name = "labelRAM";
            labelRAM.Size = new Size(43, 18);
            labelRAM.TabIndex = 11;
            labelRAM.Text = "RAM";
            // 
            // labelGPU
            // 
            labelGPU.AutoSize = true;
            labelGPU.Font = new Font("Roboto Medium", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelGPU.Location = new Point(5, 271);
            labelGPU.Name = "labelGPU";
            labelGPU.Size = new Size(41, 18);
            labelGPU.TabIndex = 10;
            labelGPU.Text = "GPU";
            // 
            // labelSSD
            // 
            labelSSD.AutoSize = true;
            labelSSD.Font = new Font("Roboto Medium", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelSSD.Location = new Point(5, 227);
            labelSSD.Name = "labelSSD";
            labelSSD.Size = new Size(133, 18);
            labelSSD.TabIndex = 9;
            labelSSD.Text = "HDD/SSD/NVMe";
            // 
            // labelDirect3D
            // 
            labelDirect3D.AutoSize = true;
            labelDirect3D.Font = new Font("Roboto Medium", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelDirect3D.Location = new Point(5, 315);
            labelDirect3D.Name = "labelDirect3D";
            labelDirect3D.Size = new Size(143, 18);
            labelDirect3D.TabIndex = 8;
            labelDirect3D.Text = "Desktop Graphics";
            // 
            // labelCPU
            // 
            labelCPU.AutoSize = true;
            labelCPU.Font = new Font("Roboto Medium", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            labelCPU.Location = new Point(5, 139);
            labelCPU.Name = "labelCPU";
            labelCPU.Size = new Size(41, 18);
            labelCPU.TabIndex = 7;
            labelCPU.Text = "CPU";
            // 
            // customButtonNiewbieClearData
            // 
            customButtonNiewbieClearData.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            customButtonNiewbieClearData.BackColor = Color.Transparent;
            customButtonNiewbieClearData.BackgroundColor = Color.Transparent;
            customButtonNiewbieClearData.BarText = "Clear  Scores";
            customButtonNiewbieClearData.BorderColor = Color.FromArgb(101, 125, 156);
            customButtonNiewbieClearData.BorderRadius = 34;
            customButtonNiewbieClearData.BorderSize = 2;
            customButtonNiewbieClearData.Cursor = Cursors.Hand;
            customButtonNiewbieClearData.FlatAppearance.BorderSize = 0;
            customButtonNiewbieClearData.FlatAppearance.MouseDownBackColor = Color.Transparent;
            customButtonNiewbieClearData.FlatAppearance.MouseOverBackColor = Color.Transparent;
            customButtonNiewbieClearData.FlatStyle = FlatStyle.Flat;
            customButtonNiewbieClearData.Font = new Font("Roboto Light", 7F, FontStyle.Bold, GraphicsUnit.Point);
            customButtonNiewbieClearData.FontColor = Color.FromArgb(101, 125, 156);
            customButtonNiewbieClearData.FontSize = 6;
            customButtonNiewbieClearData.ForeColor = Color.White;
            customButtonNiewbieClearData.GlowColor = Color.FromArgb(60, 255, 255, 255);
            customButtonNiewbieClearData.GlowSpeed = 0.2F;
            customButtonNiewbieClearData.HighlightColor = Color.WhiteSmoke;
            customButtonNiewbieClearData.IsGlowEnabled = false;
            customButtonNiewbieClearData.Location = new Point(205, 530);
            customButtonNiewbieClearData.Name = "customButtonNiewbieClearData";
            customButtonNiewbieClearData.ShadowColor = Color.FromArgb(0, 0, 0, 0);
            customButtonNiewbieClearData.Size = new Size(39, 34);
            customButtonNiewbieClearData.TabIndex = 31;
            customButtonNiewbieClearData.TextAlign = ContentAlignment.BottomCenter;
            customButtonNiewbieClearData.TextColor = Color.FromArgb(101, 125, 156);
            customButtonNiewbieClearData.UseVisualStyleBackColor = false;
            customButtonNiewbieClearData.Click += customButtonNiewbieClearData_Click;
            // 
            // pictureBoxCamera
            // 
            pictureBoxCamera.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBoxCamera.BackColor = Color.Transparent;
            pictureBoxCamera.Cursor = Cursors.Hand;
            pictureBoxCamera.Image = Properties.Resources.ButtonPictureLight32;
            pictureBoxCamera.Location = new Point(157, 533);
            pictureBoxCamera.Name = "pictureBoxCamera";
            pictureBoxCamera.Size = new Size(28, 28);
            pictureBoxCamera.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxCamera.TabIndex = 30;
            pictureBoxCamera.TabStop = false;
            pictureBoxCamera.Click += pictureBoxCamera_Click;
            // 
            // labelSlogan
            // 
            labelSlogan.AutoSize = true;
            labelSlogan.BackColor = Color.Transparent;
            labelSlogan.Font = new Font("Roboto Medium", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelSlogan.ForeColor = Color.FromArgb(33, 129, 213);
            labelSlogan.Location = new Point(12, 60);
            labelSlogan.Name = "labelSlogan";
            labelSlogan.Size = new Size(468, 17);
            labelSlogan.TabIndex = 3;
            labelSlogan.Text = "Level up your Handheld's performance and dominate the game!";
            labelSlogan.Click += labelSlogan_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Roboto Light", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.FromArgb(31, 31, 31);
            label2.Location = new Point(12, 77);
            label2.Name = "label2";
            label2.Size = new Size(570, 16);
            label2.TabIndex = 4;
            label2.Text = "The HandleOS Benchmark Index rates core system components, based on a precision scale from 1 to 100.";
            label2.Click += label2_Click;
            // 
            // labelAverageScore
            // 
            labelAverageScore.Anchor = AnchorStyles.Top;
            labelAverageScore.AutoSize = true;
            labelAverageScore.Font = new Font("Roboto Light", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelAverageScore.Location = new Point(114, 11);
            labelAverageScore.Name = "labelAverageScore";
            labelAverageScore.Size = new Size(104, 20);
            labelAverageScore.TabIndex = 2;
            labelAverageScore.Text = "Average Score";
            // 
            // labelPanelSubScore
            // 
            labelPanelSubScore.Anchor = AnchorStyles.Top;
            labelPanelSubScore.AutoSize = true;
            labelPanelSubScore.Font = new Font("Roboto Light", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            labelPanelSubScore.Location = new Point(3, 11);
            labelPanelSubScore.Name = "labelPanelSubScore";
            labelPanelSubScore.Size = new Size(71, 20);
            labelPanelSubScore.TabIndex = 1;
            labelPanelSubScore.Text = "Subscore";
            // 
            // pictureBoxButton4FormSubScores
            // 
            pictureBoxButton4FormSubScores.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxButton4FormSubScores.BackColor = Color.Transparent;
            pictureBoxButton4FormSubScores.Cursor = Cursors.Hand;
            pictureBoxButton4FormSubScores.Image = Properties.Resources.Button4FormSubScores128;
            pictureBoxButton4FormSubScores.Location = new Point(753, 56);
            pictureBoxButton4FormSubScores.Name = "pictureBoxButton4FormSubScores";
            pictureBoxButton4FormSubScores.Size = new Size(48, 48);
            pictureBoxButton4FormSubScores.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxButton4FormSubScores.TabIndex = 9;
            pictureBoxButton4FormSubScores.TabStop = false;
            pictureBoxButton4FormSubScores.Click += pictureBoxButton4FormSubScores_Click;
            pictureBoxButton4FormSubScores.MouseDown += pictureBoxButton4FormSubScores_MouseDown;
            pictureBoxButton4FormSubScores.MouseUp += pictureBoxButton4FormSubScores_MouseUp;
            // 
            // pictureBoxButton4FormSubScoresPress
            // 
            pictureBoxButton4FormSubScoresPress.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxButton4FormSubScoresPress.BackColor = Color.Transparent;
            pictureBoxButton4FormSubScoresPress.Cursor = Cursors.Hand;
            pictureBoxButton4FormSubScoresPress.Image = Properties.Resources.Button4FormSubScores128;
            pictureBoxButton4FormSubScoresPress.Location = new Point(757, 60);
            pictureBoxButton4FormSubScoresPress.Name = "pictureBoxButton4FormSubScoresPress";
            pictureBoxButton4FormSubScoresPress.Size = new Size(40, 40);
            pictureBoxButton4FormSubScoresPress.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxButton4FormSubScoresPress.TabIndex = 10;
            pictureBoxButton4FormSubScoresPress.TabStop = false;
            pictureBoxButton4FormSubScoresPress.Visible = false;
            pictureBoxButton4FormSubScoresPress.Click += pictureBoxButton4FormSubScoresPress_Click;
            pictureBoxButton4FormSubScoresPress.MouseDown += pictureBoxButton4FormSubScoresPress_MouseDown;
            pictureBoxButton4FormSubScoresPress.MouseUp += pictureBoxButton4FormSubScoresPress_MouseUp;
            // 
            // roundedPanel4LabelTitleScore
            // 
            roundedPanel4LabelTitleScore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            roundedPanel4LabelTitleScore.BackColor = Color.Transparent;
            roundedPanel4LabelTitleScore.BarText = "Average Score";
            roundedPanel4LabelTitleScore.BorderColor = Color.LightGray;
            roundedPanel4LabelTitleScore.BorderSize = 1;
            roundedPanel4LabelTitleScore.BottomLeftRadius = 5;
            roundedPanel4LabelTitleScore.BottomRightRadius = 5;
            roundedPanel4LabelTitleScore.Font = new Font("Roboto Light", 9F, FontStyle.Regular, GraphicsUnit.Point);
            roundedPanel4LabelTitleScore.FontColor = Color.FromArgb(89, 89, 89);
            roundedPanel4LabelTitleScore.FontSize = 11;
            roundedPanel4LabelTitleScore.ForeColor = Color.Black;
            roundedPanel4LabelTitleScore.GradientAngle = 90F;
            roundedPanel4LabelTitleScore.GradientBottomColor = Color.FromArgb(251, 251, 251);
            roundedPanel4LabelTitleScore.GradientOpacity = 1F;
            roundedPanel4LabelTitleScore.GradientTopColor = Color.FromArgb(251, 251, 251);
            roundedPanel4LabelTitleScore.InternalMargin = 1;
            roundedPanel4LabelTitleScore.Location = new Point(466, 116);
            roundedPanel4LabelTitleScore.Name = "roundedPanel4LabelTitleScore";
            roundedPanel4LabelTitleScore.ShadowColor = Color.FromArgb(20, 0, 0, 0);
            roundedPanel4LabelTitleScore.ShadowOffsetX = 1;
            roundedPanel4LabelTitleScore.ShadowOffsetY = 1;
            roundedPanel4LabelTitleScore.ShadowSize = -1;
            roundedPanel4LabelTitleScore.Size = new Size(335, 48);
            roundedPanel4LabelTitleScore.TabIndex = 12;
            roundedPanel4LabelTitleScore.TopLeftRadius = 15;
            roundedPanel4LabelTitleScore.TopRightRadius = 15;
            roundedPanel4LabelTitleScore.Paint += roundedPanel4LabelTitleScore_Paint;
            // 
            // roundedPanelSubScores
            // 
            roundedPanelSubScores.Anchor = AnchorStyles.Top;
            roundedPanelSubScores.BackColor = Color.Transparent;
            roundedPanelSubScores.BarText = "Subscore";
            roundedPanelSubScores.BorderColor = Color.LightGray;
            roundedPanelSubScores.BorderSize = 1;
            roundedPanelSubScores.BottomLeftRadius = 5;
            roundedPanelSubScores.BottomRightRadius = 5;
            roundedPanelSubScores.Font = new Font("Roboto Light", 9F, FontStyle.Regular, GraphicsUnit.Point);
            roundedPanelSubScores.FontColor = Color.FromArgb(89, 89, 89);
            roundedPanelSubScores.FontSize = 11;
            roundedPanelSubScores.ForeColor = Color.Black;
            roundedPanelSubScores.GradientAngle = 90F;
            roundedPanelSubScores.GradientBottomColor = Color.FromArgb(251, 251, 251);
            roundedPanelSubScores.GradientOpacity = 1F;
            roundedPanelSubScores.GradientTopColor = Color.FromArgb(251, 251, 251);
            roundedPanelSubScores.InternalMargin = 1;
            roundedPanelSubScores.Location = new Point(368, 116);
            roundedPanelSubScores.Name = "roundedPanelSubScores";
            roundedPanelSubScores.ShadowColor = Color.FromArgb(20, 0, 0, 0);
            roundedPanelSubScores.ShadowOffsetX = 1;
            roundedPanelSubScores.ShadowOffsetY = 1;
            roundedPanelSubScores.ShadowSize = -1;
            roundedPanelSubScores.Size = new Size(92, 48);
            roundedPanelSubScores.TabIndex = 13;
            roundedPanelSubScores.TopLeftRadius = 15;
            roundedPanelSubScores.TopRightRadius = 15;
            roundedPanelSubScores.Paint += roundedPanelSubScores_Paint;
            // 
            // roundedPanelTheme
            // 
            roundedPanelTheme.Anchor = AnchorStyles.Top;
            roundedPanelTheme.BackColor = Color.Transparent;
            roundedPanelTheme.BarText = "Key Metrics";
            roundedPanelTheme.BorderColor = Color.LightGray;
            roundedPanelTheme.BorderSize = 1;
            roundedPanelTheme.BottomLeftRadius = 5;
            roundedPanelTheme.BottomRightRadius = 5;
            roundedPanelTheme.Font = new Font("Roboto Light", 9F, FontStyle.Regular, GraphicsUnit.Point);
            roundedPanelTheme.FontColor = Color.FromArgb(89, 89, 89);
            roundedPanelTheme.FontSize = 11;
            roundedPanelTheme.ForeColor = Color.Black;
            roundedPanelTheme.GradientAngle = 90F;
            roundedPanelTheme.GradientBottomColor = Color.FromArgb(251, 251, 251);
            roundedPanelTheme.GradientOpacity = 1F;
            roundedPanelTheme.GradientTopColor = Color.FromArgb(251, 251, 251);
            roundedPanelTheme.InternalMargin = 1;
            roundedPanelTheme.Location = new Point(157, 116);
            roundedPanelTheme.Name = "roundedPanelTheme";
            roundedPanelTheme.ShadowColor = Color.FromArgb(20, 0, 0, 0);
            roundedPanelTheme.ShadowOffsetX = 1;
            roundedPanelTheme.ShadowOffsetY = 1;
            roundedPanelTheme.ShadowSize = -1;
            roundedPanelTheme.Size = new Size(205, 48);
            roundedPanelTheme.TabIndex = 14;
            roundedPanelTheme.TopLeftRadius = 15;
            roundedPanelTheme.TopRightRadius = 15;
            roundedPanelTheme.Paint += roundedPanelTheme_Paint;
            // 
            // roundedPanelComponents
            // 
            roundedPanelComponents.BackColor = Color.Transparent;
            roundedPanelComponents.BarText = "Component";
            roundedPanelComponents.BorderColor = Color.LightGray;
            roundedPanelComponents.BorderSize = 1;
            roundedPanelComponents.BottomLeftRadius = 5;
            roundedPanelComponents.BottomRightRadius = 5;
            roundedPanelComponents.Font = new Font("Roboto Light", 9F, FontStyle.Regular, GraphicsUnit.Point);
            roundedPanelComponents.FontColor = Color.FromArgb(89, 89, 89);
            roundedPanelComponents.FontSize = 11;
            roundedPanelComponents.ForeColor = Color.Black;
            roundedPanelComponents.GradientAngle = 90F;
            roundedPanelComponents.GradientBottomColor = Color.FromArgb(251, 251, 251);
            roundedPanelComponents.GradientOpacity = 1F;
            roundedPanelComponents.GradientTopColor = Color.FromArgb(251, 251, 251);
            roundedPanelComponents.InternalMargin = 1;
            roundedPanelComponents.Location = new Point(13, 116);
            roundedPanelComponents.Name = "roundedPanelComponents";
            roundedPanelComponents.ShadowColor = Color.FromArgb(20, 0, 0, 0);
            roundedPanelComponents.ShadowOffsetX = 1;
            roundedPanelComponents.ShadowOffsetY = 1;
            roundedPanelComponents.ShadowSize = -1;
            roundedPanelComponents.Size = new Size(138, 48);
            roundedPanelComponents.TabIndex = 27;
            roundedPanelComponents.TopLeftRadius = 15;
            roundedPanelComponents.TopRightRadius = 15;
            roundedPanelComponents.Paint += roundedPanelComponents_Paint;
            // 
            // customButtonNiewbieSTART
            // 
            customButtonNiewbieSTART.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            customButtonNiewbieSTART.BackColor = Color.Transparent;
            customButtonNiewbieSTART.BackgroundColor = Color.WhiteSmoke;
            customButtonNiewbieSTART.BarText = "START";
            customButtonNiewbieSTART.BorderColor = Color.FromArgb(101, 125, 156);
            customButtonNiewbieSTART.BorderRadius = 30;
            customButtonNiewbieSTART.BorderSize = 1;
            customButtonNiewbieSTART.Cursor = Cursors.Hand;
            customButtonNiewbieSTART.FlatAppearance.BorderSize = 0;
            customButtonNiewbieSTART.FlatAppearance.MouseDownBackColor = Color.Transparent;
            customButtonNiewbieSTART.FlatAppearance.MouseOverBackColor = Color.Transparent;
            customButtonNiewbieSTART.FlatStyle = FlatStyle.Flat;
            customButtonNiewbieSTART.Font = new Font("Roboto Medium", 9F, FontStyle.Bold, GraphicsUnit.Point);
            customButtonNiewbieSTART.FontColor = Color.FromArgb(101, 125, 156);
            customButtonNiewbieSTART.FontSize = 11;
            customButtonNiewbieSTART.ForeColor = Color.FromArgb(101, 125, 156);
            customButtonNiewbieSTART.GlowColor = Color.FromArgb(60, 255, 255, 255);
            customButtonNiewbieSTART.GlowSpeed = 0.2F;
            customButtonNiewbieSTART.HighlightColor = Color.WhiteSmoke;
            customButtonNiewbieSTART.IsGlowEnabled = false;
            customButtonNiewbieSTART.Location = new Point(690, 521);
            customButtonNiewbieSTART.Name = "customButtonNiewbieSTART";
            customButtonNiewbieSTART.ShadowColor = Color.FromArgb(0, 0, 0, 0);
            customButtonNiewbieSTART.Size = new Size(110, 40);
            customButtonNiewbieSTART.TabIndex = 28;
            customButtonNiewbieSTART.TextColor = Color.FromArgb(101, 125, 156);
            customButtonNiewbieSTART.UseVisualStyleBackColor = false;
            customButtonNiewbieSTART.Click += customButtonNiewbieSTART_Click;
            // 
            // labelDate
            // 
            labelDate.AutoSize = true;
            labelDate.BackColor = Color.Transparent;
            labelDate.Location = new Point(13, 510);
            labelDate.Name = "labelDate";
            labelDate.Size = new Size(29, 15);
            labelDate.TabIndex = 29;
            labelDate.Text = "Null";
            labelDate.Click += labelDate_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(812, 573);
            Controls.Add(customButtonNiewbieClearData);
            Controls.Add(pictureBoxCamera);
            Controls.Add(labelDate);
            Controls.Add(comboBoxTheme);
            Controls.Add(customButtonNiewbieSTART);
            Controls.Add(roundedPanelComponents);
            Controls.Add(roundedPanelTheme);
            Controls.Add(roundedPanelSubScores);
            Controls.Add(roundedPanel4LabelTitleScore);
            Controls.Add(pictureBoxButton4FormSubScoresPress);
            Controls.Add(pictureBoxButton4FormSubScores);
            Controls.Add(labelSlogan);
            Controls.Add(label2);
            Controls.Add(panelCentral);
            Controls.Add(panelTitle);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            panelTitle.ResumeLayout(false);
            panelTitle.PerformLayout();
            panelCentral.ResumeLayout(false);
            panelCentral.PerformLayout();
            roundedPanelAverageScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxConsole).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCamera).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxButton4FormSubScores).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxButton4FormSubScoresPress).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonCloseWindow;
        private Panel panelTitle;
        private Label label1;
        private Panel panelCentral;
        private Label labelSlogan;
        private Label label2;
        private RoundedPanel roundedPanelComponent;
        private RoundedPanel roundedPanelWhatIsRated;
        private RoundedPanel roundedPanelSubScore;
        private Label labelPanelSubScore;
        private Label labelAverageScore;
        private Label labelSSD;
        private Label labelDirect3D;
        private Label labelCPU;
        private Label labelRAM;
        private Label labelGPU;
        private Label labelSubScoreCPU;
        private Label labelSubScoreRAM;
        private Label labelSubScoreGPU;
        private Label labelSubScoreSSD;
        private Label labelSubScoreDirect3D;
        private Label labelNameTitle;
        private Label labelNameProduct;
        private Label labelProductDeatils;
        private PictureBox pictureBoxButton4FormSubScores;
        private PictureBox pictureBoxButton4FormSubScoresPress;       
        private CustomProgressBar customProgressBarCPU;
        private CustomProgressBar customProgressBarRAM;
        private CustomProgressBar customProgressBarSSD;
        private CustomProgressBar customProgressBarGPU;
        private CustomProgressBar customProgressBarDirect3D;
        private RoundedPanel roundedPanelAverageScore;
        private CustomProgressBarResult customProgressBarResult;
        private PictureBox pictureBoxConsole;
        private RoundedPanel roundedPanel4LabelTitleScore;
        private RoundedPanel roundedPanelSubScores;
        private RoundedPanel roundedPanelTheme;
        private RoundedPanel roundedPanelComponents;
        private CustomButtonNiewbie customButtonNiewbieSTART;
        private CustomComboBoxNiewbie comboBoxTheme;
        private Label labelDate;
        private PictureBox pictureBoxCamera;
        private CustomButtonNiewbie customButtonNiewbieClearData;
    }
}
