namespace HandleOS_Benchmark
{
    public static class ThemeSettings
    {
        public static Panel panelTitle { get; set; }
        public static Panel panelCentral { get; set; }
        public static PictureBox pictureBoxButton4FormSubScores { get; set; }
        public static PictureBox pictureBoxButton4FormSubScoresPress { get; set; }
        public static PictureBox pictureBoxCamera { get; set; }
        
        public static CustomButtonNiewbie customButtonNiewbieSTART { get; set; }
        public static CustomComboBoxNiewbie comboBoxTheme { get; set; }


        public static Label label1 { get; set; }
        public static Label label2 { get; set; }
        public static Label labelSSD { get; set; }
        public static Label labelDirect3D { get; set; }
        public static Label labelCPU { get; set; }
        public static Label labelRAM { get; set; }
        public static Label labelGPU { get; set; }
        public static Label labelSubScoreCPU { get; set; }
        public static Label labelSubScoreRAM { get; set; }
        public static Label labelSubScoreGPU { get; set; }
        public static Label labelSubScoreSSD { get; set; }
        public static Label labelSubScoreDirect3D { get; set; }
        public static Label labelNameTitle { get; set; }
        public static Label labelProductDeatils { get; set; }
        public static Label labelNameProduct { get; set; }
        public static Label labelDate { get; set; }

        public static RoundedPanel roundedPanelAverageScore { get; set; }
        public static RoundedPanel roundedPanel4LabelTitleScore { get; set; }
        public static RoundedPanel roundedPanelSubScores { get; set; }
        public static RoundedPanel roundedPanelTheme { get; set; }
        public static RoundedPanel roundedPanelComponents { get; set; }

        public static CustomProgressBar customProgressBarCPU { get; set; }
        public static CustomProgressBar customProgressBarRAM { get; set; }
        public static CustomProgressBar customProgressBarSSD { get; set; }
        public static CustomProgressBar customProgressBarGPU { get; set; }
        public static CustomProgressBar customProgressBarDirect3D { get; set; }
        public static CustomProgressBarResult customProgressBarResult { get; set; }

        // FormSubScores

        public static Label labelTitleSubscoreDetails { get; set; }
        public static Label labelCPU2 { get; set; }
        public static Label labelRam2 { get; set; } 
        public static Label labelGPU2 { get; set; }
        public static Label labelSSD2 { get; set; }
        public static Label labelCPULZWCompression { get; set; }
        public static Label labelCPUAES256Encryption { get; set; }
        public static Label labelCPUVistaCompression { get; set; }
        public static Label labelCPUSHA1Hash { get; set; }
        public static Label labelUniprocCPULZWCompression { get; set; }
        public static Label labelUniprocCPUAES256Encryption { get; set; }
        public static Label labelUniprocCPUVistaCompression { get; set; }
        public static Label labelUniprocCPUSHA1Hash { get; set; }
        public static Label labelDiskSequentialRead { get; set; }
        public static Label labelDiskRandomRead { get; set; }
        public static Label labelDirect3DBatchPerformance { get; set; }
        public static Label labelDirect3DAlphaBlendPerformance { get; set; }
        public static Label labelcustomProgressBarDirect3DALUPerformance { get; set; }
        public static Label labelDirect3DTextureLoadPerformance { get; set; }
        public static Label labelDirect3DGeometryPerformance { get; set; }
        public static Label labelDirect3DConstantBufferPerformance { get; set; }
        public static Label labelVideoMemoryThroughput { get; set; }
        public static Label labelDshowVideoEncodeTime { get; set; }
        public static Label labelDshowVideoDecodeTime { get; set; }
        public static Label labelMediaFoundationDecodeTime { get; set; }
        public static Label labelMemoryPerformance { get; set; }

        public static RoundedPanel roundedPanel1 { get; set; }
        public static RoundedPanel roundedPanel3 { get; set; }
        public static RoundedPanel roundedPanel2 { get; set; }
        public static RoundedPanel roundedPanel4 { get; set; }
        public static PictureBox pictureBoxCamera2 { get; set; }


        private static string currentTheme = "Light"; // Default theme
        public static event Action ThemeChanged;
        private static readonly string themeFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Console2Desk", "theme");

        public static string CurrentTheme
        {
            get { return currentTheme; }
            set
            {
                currentTheme = value;
                ApplyCurrentTheme();
                SaveThemeToFile();
                ThemeChanged?.Invoke(); // Invoke the event when the theme changes

            }
        }

        public static void ApplyCurrentTheme()
        {
            if (panelTitle != null)
            {
                Theme.ApplyTheme(currentTheme, panelTitle, panelCentral, pictureBoxButton4FormSubScoresPress, pictureBoxButton4FormSubScores,
                    pictureBoxCamera, label1, label2, labelSSD, labelDirect3D, labelCPU, labelRAM, labelGPU, 
                    labelSubScoreCPU, labelSubScoreRAM, labelSubScoreGPU, labelSubScoreSSD, labelSubScoreDirect3D,
                    labelNameTitle, labelProductDeatils, labelNameProduct, labelDate, roundedPanelAverageScore,
                    roundedPanel4LabelTitleScore, roundedPanelSubScores, roundedPanelTheme, roundedPanelComponents, customProgressBarCPU,
                    customProgressBarRAM, customProgressBarSSD, customProgressBarGPU, customProgressBarDirect3D,
                    customProgressBarResult, customButtonNiewbieSTART, comboBoxTheme, labelTitleSubscoreDetails, 
                    labelCPU2, labelRam2, labelGPU2, labelSSD2, labelCPULZWCompression, labelCPUAES256Encryption, labelCPUVistaCompression, labelCPUSHA1Hash,
                    labelUniprocCPULZWCompression, labelUniprocCPUAES256Encryption, labelUniprocCPUVistaCompression,
                    labelUniprocCPUSHA1Hash, labelDiskSequentialRead, labelDiskRandomRead, labelDirect3DBatchPerformance,
                    labelDirect3DAlphaBlendPerformance, labelcustomProgressBarDirect3DALUPerformance, labelDirect3DTextureLoadPerformance,
                    labelDirect3DGeometryPerformance, labelDirect3DConstantBufferPerformance, labelVideoMemoryThroughput,
                    labelDshowVideoEncodeTime, labelDshowVideoDecodeTime, labelMediaFoundationDecodeTime, labelMemoryPerformance, roundedPanel1, roundedPanel3,
                    roundedPanel2, roundedPanel4, pictureBoxCamera2);
            }
        }

        private static void SaveThemeToFile()
        {
            try
            {
                string content = currentTheme == "Dark" ? "1" : "0";
                File.WriteAllText(themeFilePath, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore nel salvataggio del tema: " + ex.Message);
            }
        }

        public static void LoadThemeFromFile()
        {
            try
            {
                if (File.Exists(themeFilePath))
                {
                    string content = File.ReadAllText(themeFilePath);
                    currentTheme = content == "1" ? "Dark" : "Light";
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(themeFilePath));
                    File.WriteAllText(themeFilePath, "0");
                    currentTheme = "Light";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore nel caricamento del tema: " + ex.Message);
                currentTheme = "Light"; // Default to Light theme in case of error
            }
        }
    }
}
