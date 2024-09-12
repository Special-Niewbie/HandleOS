namespace HandleOS_Benchmark
{
    public static class Theme
    {
        // Dark Theme Colors
        private static readonly Color darkTitleBackColor = Color.FromArgb(31, 31, 31);
        private static readonly Color darkForeColor = Color.WhiteSmoke;
        private static readonly Color darkPanelsBackColor = Color.FromArgb(40, 40, 40);
        private static readonly Color darkPanelsFontColors = Color.FromArgb(172, 172, 172);
        private static readonly Color darkButtonsColors = Color.FromArgb(222, 222, 222);
        private static readonly Color customProgressBarResultFont = Color.White;
        private static readonly int darkButtonsBorderSize = 0;
       

        // Light Theme Colors
        private static readonly Color lightTitleBackColor = Color.FromArgb(248, 249, 253);
        private static readonly Color lightForeColor = Color.FromArgb(31, 31, 31);
        private static readonly Color lightPanelsBackColor = Color.FromArgb(251, 251, 251);
        private static readonly Color lightPanelsFontColors = Color.FromArgb(89, 89, 89);
        private static readonly Color lightButtonsColors = Color.FromArgb(251, 251, 251);
        private static readonly int lightButtonsBorderSize = 1;


        public static void ApplyTheme(string theme, Panel panelTitle, Panel panelCentral, PictureBox pictureBoxButton4FormSubScoresPress,
            PictureBox pictureBoxButton4FormSubScores, PictureBox pictureBoxCamera, Label label1, Label label2, Label labelSSD, Label labelDirect3D, Label labelCPU, Label labelRAM, Label labelGPU, Label labelSubScoreCPU, 
            Label labelSubScoreRAM, Label labelSubScoreGPU, Label labelSubScoreSSD, Label labelSubScoreDirect3D, 
            Label labelNameTitle, Label labelProductDeatils, Label labelNameProduct, Label labelDate,
            RoundedPanel roundedPanelAverageScore, RoundedPanel roundedPanel4LabelTitleScore, RoundedPanel roundedPanelSubScores, 
            RoundedPanel roundedPanelTheme, RoundedPanel roundedPanelComponents, CustomProgressBar customProgressBarCPU,
            CustomProgressBar customProgressBarRAM, CustomProgressBar customProgressBarSSD, CustomProgressBar customProgressBarGPU,
            CustomProgressBar customProgressBarDirect3D, CustomProgressBarResult customProgressBarResult, CustomButtonNiewbie customButtonNiewbieSTART, CustomComboBoxNiewbie comboBoxTheme,
            Label labelTitleSubscoreDetails, Label labelCPU2, Label labelRam2, Label labelGPU2, Label labelSSD2, Label labelCPULZWCompression, Label labelCPUAES256Encryption,
            Label labelCPUVistaCompression, Label labelCPUSHA1Hash, Label labelUniprocCPULZWCompression, Label labelUniprocCPUAES256Encryption, Label labelUniprocCPUVistaCompression,
            Label labelUniprocCPUSHA1Hash, Label labelDiskSequentialRead, Label labelDiskRandomRead, Label labelDirect3DBatchPerformance,
            Label labelDirect3DAlphaBlendPerformance, Label labelcustomProgressBarDirect3DALUPerformance, Label labelDirect3DTextureLoadPerformance,
            Label labelDirect3DGeometryPerformance, Label labelDirect3DConstantBufferPerformance, Label labelVideoMemoryThroughput,
            Label labelDshowVideoEncodeTime, Label labelDshowVideoDecodeTime, Label labelMediaFoundationDecodeTime, Label labelMemoryPerformance,
            RoundedPanel roundedPanel1, RoundedPanel roundedPanel3, RoundedPanel roundedPanel2, RoundedPanel roundedPanel4, PictureBox pictureBoxCamera2)
        {
            switch (theme)
            {
                case "Dark":
                    ApplyDarkTheme(panelTitle, panelCentral, pictureBoxButton4FormSubScoresPress, pictureBoxButton4FormSubScores,
                        pictureBoxCamera, label1, label2, labelSSD, labelDirect3D, labelCPU, labelRAM, labelGPU,
                        labelSubScoreCPU, labelSubScoreRAM, labelSubScoreGPU, labelSubScoreSSD, labelSubScoreDirect3D,
                        labelNameTitle, labelProductDeatils, labelNameProduct, labelDate, roundedPanelAverageScore,
                        roundedPanel4LabelTitleScore, roundedPanelSubScores, roundedPanelTheme, roundedPanelComponents, customProgressBarCPU,
                        customProgressBarRAM, customProgressBarSSD, customProgressBarGPU, customProgressBarDirect3D, customProgressBarResult, customButtonNiewbieSTART, comboBoxTheme,
                        labelTitleSubscoreDetails, labelCPU2, labelRam2, labelGPU2, labelSSD2, labelCPULZWCompression, labelCPUAES256Encryption, labelCPUVistaCompression, labelCPUSHA1Hash, 
                        labelUniprocCPULZWCompression, labelUniprocCPUAES256Encryption, labelUniprocCPUVistaCompression, 
                        labelUniprocCPUSHA1Hash, labelDiskSequentialRead, labelDiskRandomRead, labelDirect3DBatchPerformance, 
                        labelDirect3DAlphaBlendPerformance, labelcustomProgressBarDirect3DALUPerformance, labelDirect3DTextureLoadPerformance, 
                        labelDirect3DGeometryPerformance, labelDirect3DConstantBufferPerformance, labelVideoMemoryThroughput, 
                        labelDshowVideoEncodeTime, labelDshowVideoDecodeTime, labelMediaFoundationDecodeTime, labelMemoryPerformance, roundedPanel1, roundedPanel3, 
                        roundedPanel2, roundedPanel4, pictureBoxCamera2);
                    break;
                case "Light":
                default:
                    ApplyLightTheme(panelTitle, panelCentral, pictureBoxButton4FormSubScoresPress, pictureBoxButton4FormSubScores,
                        pictureBoxCamera, label1, label2, labelSSD, labelDirect3D, labelCPU, labelRAM, labelGPU,
                        labelSubScoreCPU, labelSubScoreRAM, labelSubScoreGPU, labelSubScoreSSD, labelSubScoreDirect3D,
                        labelNameTitle, labelProductDeatils, labelNameProduct, labelDate, roundedPanelAverageScore,
                        roundedPanel4LabelTitleScore, roundedPanelSubScores, roundedPanelTheme, roundedPanelComponents, customProgressBarCPU,
                        customProgressBarRAM, customProgressBarSSD, customProgressBarGPU, customProgressBarDirect3D, customProgressBarResult, customButtonNiewbieSTART, comboBoxTheme,
                        labelTitleSubscoreDetails, labelCPU2, labelRam2, labelGPU2, labelSSD2, labelCPULZWCompression, labelCPUAES256Encryption, labelCPUVistaCompression, labelCPUSHA1Hash,
                        labelUniprocCPULZWCompression, labelUniprocCPUAES256Encryption, labelUniprocCPUVistaCompression,
                        labelUniprocCPUSHA1Hash, labelDiskSequentialRead, labelDiskRandomRead, labelDirect3DBatchPerformance,
                        labelDirect3DAlphaBlendPerformance, labelcustomProgressBarDirect3DALUPerformance, labelDirect3DTextureLoadPerformance,
                        labelDirect3DGeometryPerformance, labelDirect3DConstantBufferPerformance, labelVideoMemoryThroughput,
                        labelDshowVideoEncodeTime, labelDshowVideoDecodeTime, labelMediaFoundationDecodeTime, labelMemoryPerformance, roundedPanel1, roundedPanel3,
                        roundedPanel2, roundedPanel4, pictureBoxCamera2);
                    break;
            }
        }

        private static void ApplyDarkTheme(Panel panelTitle, Panel panelCentral, PictureBox pictureBoxButton4FormSubScoresPress, PictureBox pictureBoxButton4FormSubScores,
            PictureBox pictureBoxCamera, Label label1, Label label2, Label labelSSD, Label labelDirect3D, Label labelCPU, Label labelRAM, Label labelGPU, Label labelSubScoreCPU,
            Label labelSubScoreRAM, Label labelSubScoreGPU, Label labelSubScoreSSD, Label labelSubScoreDirect3D,
            Label labelNameTitle, Label labelProductDeatils, Label labelNameProduct, Label labelDate,
            RoundedPanel roundedPanelAverageScore, RoundedPanel roundedPanel4LabelTitleScore, RoundedPanel roundedPanelSubScores,
            RoundedPanel roundedPanelTheme, RoundedPanel roundedPanelComponents, CustomProgressBar customProgressBarCPU,
            CustomProgressBar customProgressBarRAM, CustomProgressBar customProgressBarSSD, CustomProgressBar customProgressBarGPU,
            CustomProgressBar customProgressBarDirect3D, CustomProgressBarResult customProgressBarResult, CustomButtonNiewbie customButtonNiewbieSTART, CustomComboBoxNiewbie comboBoxTheme,
            Label labelTitleSubscoreDetails, Label labelCPU2, Label labelRam2, Label labelGPU2, Label labelSSD2, Label labelCPULZWCompression, Label labelCPUAES256Encryption,
            Label labelCPUVistaCompression, Label labelCPUSHA1Hash, Label labelUniprocCPULZWCompression, Label labelUniprocCPUAES256Encryption, Label labelUniprocCPUVistaCompression,
            Label labelUniprocCPUSHA1Hash, Label labelDiskSequentialRead, Label labelDiskRandomRead, Label labelDirect3DBatchPerformance,
            Label labelDirect3DAlphaBlendPerformance, Label labelcustomProgressBarDirect3DALUPerformance, Label labelDirect3DTextureLoadPerformance,
            Label labelDirect3DGeometryPerformance, Label labelDirect3DConstantBufferPerformance, Label labelVideoMemoryThroughput,
            Label labelDshowVideoEncodeTime, Label labelDshowVideoDecodeTime, Label labelMediaFoundationDecodeTime, Label labelMemoryPerformance,
            RoundedPanel roundedPanel1, RoundedPanel roundedPanel3, RoundedPanel roundedPanel2, RoundedPanel roundedPanel4, PictureBox pictureBoxCamera2)
        {
            panelTitle.BackColor = darkTitleBackColor;            
            panelCentral.BackColor = darkPanelsBackColor;
            pictureBoxButton4FormSubScoresPress.Image = Properties.Resources.Button4FormSubScoresDark128;
            pictureBoxButton4FormSubScores.Image = Properties.Resources.Button4FormSubScoresDark128;
            pictureBoxCamera.Image = Properties.Resources.ButtonPictureDark32;

            label1.ForeColor = darkForeColor;
            label2.ForeColor = darkForeColor;
            labelSSD.ForeColor = darkForeColor;
            labelDirect3D.ForeColor = darkForeColor;
            labelCPU.ForeColor = darkForeColor;
            labelRAM.ForeColor = darkForeColor;
            labelGPU.ForeColor = darkForeColor;
            labelSubScoreCPU.ForeColor = darkForeColor;
            labelSubScoreRAM.ForeColor = darkForeColor;
            labelSubScoreGPU.ForeColor = darkForeColor;
            labelSubScoreSSD.ForeColor = darkForeColor;
            labelSubScoreDirect3D.ForeColor = darkForeColor;
            labelNameTitle.ForeColor = darkForeColor;
            labelProductDeatils.ForeColor = darkForeColor;
            labelNameProduct.ForeColor = darkForeColor;
            labelDate.ForeColor = darkForeColor;

            roundedPanelAverageScore.GradientTopColor = darkPanelsBackColor;
            roundedPanelAverageScore.GradientBottomColor = darkPanelsBackColor;
            roundedPanelAverageScore.FontColor = darkPanelsFontColors;
            roundedPanel4LabelTitleScore.GradientTopColor = darkPanelsBackColor;
            roundedPanel4LabelTitleScore.GradientBottomColor = darkPanelsBackColor;
            roundedPanel4LabelTitleScore.FontColor = darkPanelsFontColors;
            roundedPanelSubScores.GradientTopColor = darkPanelsBackColor;
            roundedPanelSubScores.GradientBottomColor = darkPanelsBackColor;
            roundedPanelSubScores.FontColor = darkPanelsFontColors;
            roundedPanelTheme.GradientTopColor = darkPanelsBackColor;
            roundedPanelTheme.GradientBottomColor = darkPanelsBackColor;
            roundedPanelTheme.FontColor = darkPanelsFontColors;
            roundedPanelComponents.GradientTopColor = darkPanelsBackColor;
            roundedPanelComponents.GradientBottomColor = darkPanelsBackColor;
            roundedPanelComponents.FontColor = darkPanelsFontColors;

            customProgressBarCPU.BackgroundColor = darkPanelsBackColor;
            customProgressBarRAM.BackgroundColor = darkPanelsBackColor;
            customProgressBarSSD.BackgroundColor = darkPanelsBackColor;
            customProgressBarGPU.BackgroundColor = darkPanelsBackColor;
            customProgressBarDirect3D.BackgroundColor = darkPanelsBackColor;
            customProgressBarResult.FontColor = darkForeColor;

            customButtonNiewbieSTART.BackgroundColor = darkButtonsColors;
            customButtonNiewbieSTART.BorderSize = darkButtonsBorderSize;
            comboBoxTheme.BackColor = darkPanelsBackColor;
            comboBoxTheme.ForeColor = darkPanelsFontColors;

            // FormSubScores
            if (labelTitleSubscoreDetails != null && labelCPU2 != null && labelRam2 != null && labelGPU2 != null && labelSSD2 != null
                && labelCPULZWCompression != null && labelCPUAES256Encryption != null && labelCPUVistaCompression != null && labelCPUSHA1Hash != null && labelUniprocCPULZWCompression != null
                && labelUniprocCPUAES256Encryption != null && labelUniprocCPUVistaCompression != null && labelUniprocCPUSHA1Hash != null && labelDiskSequentialRead != null && labelDiskRandomRead != null
                && labelDirect3DBatchPerformance != null && labelDirect3DAlphaBlendPerformance != null && labelcustomProgressBarDirect3DALUPerformance != null && labelDirect3DTextureLoadPerformance != null && labelDirect3DGeometryPerformance != null
                && labelDirect3DConstantBufferPerformance != null && labelVideoMemoryThroughput != null && labelDshowVideoEncodeTime != null && labelDshowVideoDecodeTime != null && labelMediaFoundationDecodeTime != null
                && labelMemoryPerformance != null && roundedPanel1 != null && roundedPanel3 != null && roundedPanel2 != null
                && roundedPanel4 != null && pictureBoxCamera2 != null)
            {
                labelTitleSubscoreDetails.ForeColor = darkForeColor;
                labelCPU2.ForeColor = darkForeColor;
                labelRam2.ForeColor = darkForeColor;
                labelGPU2.ForeColor = darkForeColor;
                labelSSD2.ForeColor = darkForeColor;
                labelCPULZWCompression.ForeColor = darkForeColor;
                labelCPUAES256Encryption.ForeColor = darkForeColor;
                labelCPUVistaCompression.ForeColor = darkForeColor;
                labelCPUSHA1Hash.ForeColor = darkForeColor;
                labelUniprocCPULZWCompression.ForeColor = darkForeColor;
                labelUniprocCPUAES256Encryption.ForeColor = darkForeColor;
                labelUniprocCPUVistaCompression.ForeColor = darkForeColor;
                labelUniprocCPUSHA1Hash.ForeColor = darkForeColor;
                labelDiskSequentialRead.ForeColor = darkForeColor;
                labelDiskRandomRead.ForeColor = darkForeColor;
                labelDirect3DBatchPerformance.ForeColor = darkForeColor;
                labelDirect3DAlphaBlendPerformance.ForeColor = darkForeColor;
                labelcustomProgressBarDirect3DALUPerformance.ForeColor = darkForeColor;
                labelDirect3DTextureLoadPerformance.ForeColor = darkForeColor;
                labelDirect3DGeometryPerformance.ForeColor = darkForeColor;
                labelDirect3DConstantBufferPerformance.ForeColor = darkForeColor;
                labelVideoMemoryThroughput.ForeColor = darkForeColor;
                labelDshowVideoEncodeTime.ForeColor = darkForeColor;
                labelDshowVideoDecodeTime.ForeColor = darkForeColor;
                labelMediaFoundationDecodeTime.ForeColor = darkForeColor;
                labelMemoryPerformance.ForeColor = darkForeColor;

                roundedPanel1.GradientTopColor = darkPanelsBackColor;
                roundedPanel1.GradientBottomColor = darkPanelsBackColor;
                roundedPanel2.GradientTopColor = darkPanelsBackColor;
                roundedPanel2.GradientBottomColor = darkPanelsBackColor;
                roundedPanel3.GradientTopColor = darkPanelsBackColor;
                roundedPanel3.GradientBottomColor = darkPanelsBackColor;                
                roundedPanel4.GradientTopColor = darkPanelsBackColor;
                roundedPanel4.GradientBottomColor = darkPanelsBackColor;

                pictureBoxCamera2.Image = Properties.Resources.ButtonPictureDark32;
            }

        }

        private static void ApplyLightTheme(Panel panelTitle, Panel panelCentral, PictureBox pictureBoxButton4FormSubScoresPress, PictureBox pictureBoxButton4FormSubScores,
            PictureBox pictureBoxCamera, Label label1, Label label2, Label labelSSD, Label labelDirect3D, Label labelCPU, Label labelRAM, Label labelGPU, Label labelSubScoreCPU,
            Label labelSubScoreRAM, Label labelSubScoreGPU, Label labelSubScoreSSD, Label labelSubScoreDirect3D,
            Label labelNameTitle, Label labelProductDeatils, Label labelNameProduct, Label labelDate,
            RoundedPanel roundedPanelAverageScore, RoundedPanel roundedPanel4LabelTitleScore, RoundedPanel roundedPanelSubScores,
            RoundedPanel roundedPanelTheme, RoundedPanel roundedPanelComponents, CustomProgressBar customProgressBarCPU,
            CustomProgressBar customProgressBarRAM, CustomProgressBar customProgressBarSSD, CustomProgressBar customProgressBarGPU,
            CustomProgressBar customProgressBarDirect3D, CustomProgressBarResult customProgressBarResult, CustomButtonNiewbie customButtonNiewbieSTART, CustomComboBoxNiewbie comboBoxTheme,
            Label labelTitleSubscoreDetails, Label labelCPU2, Label labelRam2, Label labelGPU2, Label labelSSD2, Label labelCPULZWCompression, Label labelCPUAES256Encryption,
            Label labelCPUVistaCompression, Label labelCPUSHA1Hash, Label labelUniprocCPULZWCompression, Label labelUniprocCPUAES256Encryption, Label labelUniprocCPUVistaCompression,
            Label labelUniprocCPUSHA1Hash, Label labelDiskSequentialRead, Label labelDiskRandomRead, Label labelDirect3DBatchPerformance,
            Label labelDirect3DAlphaBlendPerformance, Label labelcustomProgressBarDirect3DALUPerformance, Label labelDirect3DTextureLoadPerformance,
            Label labelDirect3DGeometryPerformance, Label labelDirect3DConstantBufferPerformance, Label labelVideoMemoryThroughput,
            Label labelDshowVideoEncodeTime, Label labelDshowVideoDecodeTime, Label labelMediaFoundationDecodeTime, Label labelMemoryPerformance,
            RoundedPanel roundedPanel1, RoundedPanel roundedPanel3, RoundedPanel roundedPanel2, RoundedPanel roundedPanel4, PictureBox pictureBoxCamera2)
        {
            panelTitle.BackColor = lightTitleBackColor;          
            panelCentral.BackColor = lightPanelsBackColor;
            pictureBoxButton4FormSubScoresPress.Image = Properties.Resources.Button4FormSubScores128;
            pictureBoxButton4FormSubScores.Image = Properties.Resources.Button4FormSubScores128;
            pictureBoxCamera.Image = Properties.Resources.ButtonPictureLight32;

            label1.ForeColor = lightForeColor;
            label2.ForeColor = lightForeColor;
            labelSSD.ForeColor = lightForeColor;
            labelDirect3D.ForeColor = lightForeColor;
            labelCPU.ForeColor = lightForeColor;
            labelRAM.ForeColor = lightForeColor;
            labelGPU.ForeColor = lightForeColor;
            labelSubScoreCPU.ForeColor = lightForeColor;
            labelSubScoreRAM.ForeColor = lightForeColor;
            labelSubScoreGPU.ForeColor = lightForeColor;
            labelSubScoreSSD.ForeColor = lightForeColor;
            labelSubScoreDirect3D.ForeColor = lightForeColor;
            labelNameTitle.ForeColor = lightForeColor;
            labelProductDeatils.ForeColor = lightForeColor;
            labelNameProduct.ForeColor = lightForeColor;
            labelDate.ForeColor = lightForeColor;

            roundedPanelAverageScore.GradientTopColor = lightPanelsBackColor;
            roundedPanelAverageScore.GradientBottomColor = lightPanelsBackColor;
            roundedPanelAverageScore.FontColor = lightPanelsFontColors;
            roundedPanel4LabelTitleScore.GradientTopColor = lightPanelsBackColor;
            roundedPanel4LabelTitleScore.GradientBottomColor = lightPanelsBackColor;
            roundedPanel4LabelTitleScore.FontColor = lightPanelsFontColors;
            roundedPanelSubScores.GradientTopColor = lightPanelsBackColor;
            roundedPanelSubScores.GradientBottomColor = lightPanelsBackColor;
            roundedPanelSubScores.FontColor = lightPanelsFontColors;
            roundedPanelTheme.GradientTopColor = lightPanelsBackColor;
            roundedPanelTheme.GradientBottomColor = lightPanelsBackColor;
            roundedPanelTheme.FontColor = lightPanelsFontColors;
            roundedPanelComponents.GradientTopColor = lightPanelsBackColor;
            roundedPanelComponents.GradientBottomColor = lightPanelsBackColor;
            roundedPanelComponents.FontColor = lightPanelsFontColors;

            customProgressBarCPU.BackgroundColor = lightPanelsBackColor;
            customProgressBarRAM.BackgroundColor = lightPanelsBackColor;
            customProgressBarSSD.BackgroundColor = lightPanelsBackColor;
            customProgressBarGPU.BackgroundColor = lightPanelsBackColor;
            customProgressBarDirect3D.BackgroundColor = lightPanelsBackColor;
            customProgressBarResult.FontColor = customProgressBarResultFont;

            customButtonNiewbieSTART.BackgroundColor = lightButtonsColors;
            customButtonNiewbieSTART.BorderSize = lightButtonsBorderSize;
            comboBoxTheme.BackColor = darkForeColor;
            comboBoxTheme.ForeColor = lightPanelsFontColors;

            // FormSubScores
            if (labelTitleSubscoreDetails != null && labelCPU2 != null && labelRam2 != null && labelGPU2 != null && labelSSD2 != null
                && labelCPULZWCompression != null && labelCPUAES256Encryption != null && labelCPUVistaCompression != null && labelCPUSHA1Hash != null && labelUniprocCPULZWCompression != null
                && labelUniprocCPUAES256Encryption != null && labelUniprocCPUVistaCompression != null && labelUniprocCPUSHA1Hash != null && labelDiskSequentialRead != null && labelDiskRandomRead != null
                && labelDirect3DBatchPerformance != null && labelDirect3DAlphaBlendPerformance != null && labelcustomProgressBarDirect3DALUPerformance != null && labelDirect3DTextureLoadPerformance != null && labelDirect3DGeometryPerformance != null
                && labelDirect3DConstantBufferPerformance != null && labelVideoMemoryThroughput != null && labelDshowVideoEncodeTime != null && labelDshowVideoDecodeTime != null && labelMediaFoundationDecodeTime != null
                && labelMemoryPerformance != null && roundedPanel1 != null && roundedPanel3 != null && roundedPanel2 != null
                && roundedPanel4 != null && pictureBoxCamera2 != null)
            {
                labelTitleSubscoreDetails.ForeColor = lightForeColor;
                labelCPU2.ForeColor = lightForeColor;
                labelRam2.ForeColor = lightForeColor;
                labelGPU2.ForeColor = lightForeColor;
                labelSSD2.ForeColor = lightForeColor;
                labelCPULZWCompression.ForeColor = lightForeColor;
                labelCPUAES256Encryption.ForeColor = lightForeColor;
                labelCPUVistaCompression.ForeColor = lightForeColor;
                labelCPUSHA1Hash.ForeColor = lightForeColor;
                labelUniprocCPULZWCompression.ForeColor = lightForeColor;
                labelUniprocCPUAES256Encryption.ForeColor = lightForeColor;
                labelUniprocCPUVistaCompression.ForeColor = lightForeColor;
                labelUniprocCPUSHA1Hash.ForeColor = lightForeColor;
                labelDiskSequentialRead.ForeColor = lightForeColor;
                labelDiskRandomRead.ForeColor = lightForeColor;
                labelDirect3DBatchPerformance.ForeColor = lightForeColor;
                labelDirect3DAlphaBlendPerformance.ForeColor = lightForeColor;
                labelcustomProgressBarDirect3DALUPerformance.ForeColor = lightForeColor;
                labelDirect3DTextureLoadPerformance.ForeColor = lightForeColor;
                labelDirect3DGeometryPerformance.ForeColor = lightForeColor;
                labelDirect3DConstantBufferPerformance.ForeColor = lightForeColor;
                labelVideoMemoryThroughput.ForeColor = lightForeColor;
                labelDshowVideoEncodeTime.ForeColor = lightForeColor;
                labelDshowVideoDecodeTime.ForeColor = lightForeColor;
                labelMediaFoundationDecodeTime.ForeColor = lightForeColor;
                labelMemoryPerformance.ForeColor = lightForeColor;
                roundedPanel1.GradientTopColor = lightPanelsBackColor;
                roundedPanel1.GradientBottomColor = lightPanelsBackColor;                
                roundedPanel2.GradientTopColor = lightPanelsBackColor;
                roundedPanel2.GradientBottomColor = lightPanelsBackColor;
                roundedPanel3.GradientTopColor = lightPanelsBackColor;
                roundedPanel3.GradientBottomColor = lightPanelsBackColor;
                roundedPanel4.GradientTopColor = lightPanelsBackColor;
                roundedPanel4.GradientBottomColor = lightPanelsBackColor;

                pictureBoxCamera2.Image = Properties.Resources.ButtonPictureLight32;
            }
        }
    }
}
