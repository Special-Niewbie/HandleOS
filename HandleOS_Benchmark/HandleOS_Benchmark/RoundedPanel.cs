using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace HandleOS_Benchmark
{
    public class RoundedPanel : Panel
    {
        private int topLeftRadius = 30;
        private int topRightRadius = 30;
        private int bottomLeftRadius = 30;
        private int bottomRightRadius = 30;
        private float gradientAngle = 90F;
        private Color gradientTopColor = Color.DodgerBlue;
        private Color gradientBottomColor = Color.CadetBlue;
        private float gradientOpacity = 1.0f;

        private int shadowSize = -1;
        private int shadowOffsetX = 1;
        private int shadowOffsetY = 1;
        private Color shadowColor = Color.FromArgb(20, 0, 0, 0);

        private int borderSize = 1;
        private Color borderColor = Color.LightGray;
        private string barText = "";
        private Color fontColor = Color.Black;
        private int fontSize = 7;

        private int internalMargin = 1; // Margine interno per ridurre le dimensioni del pannello

        // Constructor
        public RoundedPanel()
        {
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Black;
            this.Size = new Size(350, 200);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        // Properties
        [Category("Niewbie Advance")]
        public int TopLeftRadius { get => topLeftRadius; set { topLeftRadius = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public int TopRightRadius { get => topRightRadius; set { topRightRadius = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public int BottomLeftRadius { get => bottomLeftRadius; set { bottomLeftRadius = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public int BottomRightRadius { get => bottomRightRadius; set { bottomRightRadius = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public float GradientAngle { get => gradientAngle; set { gradientAngle = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public Color GradientTopColor { get => gradientTopColor; set { gradientTopColor = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public Color GradientBottomColor { get => gradientBottomColor; set { gradientBottomColor = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public float GradientOpacity { get => gradientOpacity; set { gradientOpacity = Math.Max(0, Math.Min(1, value)); this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public int ShadowSize { get => shadowSize; set { shadowSize = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public int ShadowOffsetX { get => shadowOffsetX; set { shadowOffsetX = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public int ShadowOffsetY { get => shadowOffsetY; set { shadowOffsetY = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public Color ShadowColor { get => shadowColor; set { shadowColor = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public int BorderSize { get => borderSize; set { borderSize = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public Color BorderColor { get => borderColor; set { borderColor = value; this.Invalidate(); } }
        [Category("Niewbie Advance")]
        public int InternalMargin
        {
            get => internalMargin;
            set { internalMargin = value; this.Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public string BarText
        {
            get { return barText; }
            set
            {
                barText = value;
                this.Invalidate(); // Richiedi il ridisegno del controllo
            }
        }

        [Category("Niewbie Advance")]
        public Color FontColor
        {
            get { return fontColor; }
            set
            {
                fontColor = value;
                this.Invalidate(); // Richiedi il ridisegno del controllo
            }
        }

        [Category("Niewbie Advance")]
        public int FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                this.Invalidate(); // Richiedi il ridisegno del controllo
            }
        }

        // Methods
        private GraphicsPath GetRoundedPanelPath(RectangleF rectangle)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.StartFigure();

            // Usa il margine interno per garantire che il pannello colorato non vada oltre il bordo
            graphicsPath.AddArc(rectangle.X, rectangle.Y, topLeftRadius * 2, topLeftRadius * 2, 180, 90);
            graphicsPath.AddArc(rectangle.Width - topRightRadius * 2, rectangle.Y, topRightRadius * 2, topRightRadius * 2, 270, 90);
            graphicsPath.AddArc(rectangle.Width - bottomRightRadius * 2, rectangle.Height - bottomRightRadius * 2, bottomRightRadius * 2, bottomRightRadius * 2, 0, 90);
            graphicsPath.AddArc(rectangle.X, rectangle.Height - bottomLeftRadius * 2, bottomLeftRadius * 2, bottomLeftRadius * 2, 90, 90);

            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // Migliora la qualità del disegno

            // 1. Disegna l'ombra per prima
            DrawShadow(e.Graphics);

            // 2. Gradient con opacità
            RectangleF panelRect = new RectangleF(
                internalMargin,
                internalMargin,
                this.Width - internalMargin * 2,
                this.Height - internalMargin * 2
            );

            using (GraphicsPath path = GetRoundedPanelPath(panelRect))
            using (Region region = new Region(path))
            {
                e.Graphics.SetClip(region, CombineMode.Replace);

                Color topColorWithOpacity = Color.FromArgb((int)(gradientOpacity * 255), gradientTopColor);
                Color bottomColorWithOpacity = Color.FromArgb((int)(gradientOpacity * 255), gradientBottomColor);
                using (LinearGradientBrush brushSpecialNiewbie = new LinearGradientBrush(panelRect, topColorWithOpacity, bottomColorWithOpacity, gradientAngle))
                {
                    e.Graphics.FillRectangle(brushSpecialNiewbie, panelRect);
                }
                e.Graphics.ResetClip();
            }

            // Disegna il testo al centro della barra
            using (var font = new Font("Roboto Medium", this.fontSize))
            using (var brush = new SolidBrush(this.FontColor))
            {
                var textSize = e.Graphics.MeasureString(BarText, font);
                var textX = (this.Width - textSize.Width) / 2;
                var textY = (this.Height + 3 - textSize.Height) / 2;
                e.Graphics.DrawString(BarText, font, brush, textX, textY);
            }

            // 3. Disegna il bordo arrotondato con qualità HD
            RectangleF borderRect = new RectangleF(
                internalMargin,
                internalMargin,
                this.Width - internalMargin * 2,
                this.Height - internalMargin * 2
            );
            using (GraphicsPath borderPath = GetRoundedPanelPath(borderRect))
            using (Pen borderPen = new Pen(borderColor, borderSize))
            {
                e.Graphics.DrawPath(borderPen, borderPath);
            }
        }

        private void DrawShadow(Graphics g)
        {
            // Creiamo una regione di disegno che include l'ombra oltre i confini del pannello
            RectangleF shadowRect = new RectangleF(
                shadowOffsetX - shadowSize,
                shadowOffsetY - shadowSize,
                this.Width + shadowSize * 2,
                this.Height + shadowSize * 2
            );
            using (GraphicsPath shadowPath = GetRoundedPanelPath(shadowRect))
            using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
            {
                shadowBrush.CenterColor = Color.Transparent;
                shadowBrush.SurroundColors = new Color[] { shadowColor };
                shadowBrush.Blend = new Blend
                {
                    Factors = new float[] { 0.0f, 0.3f, 1.0f },
                    Positions = new float[] { 0.0f, 0.5f, 1.0f }
                };

                // Riempie il percorso dell'ombra che si estende oltre i confini del pannello
                g.FillPath(shadowBrush, shadowPath);
            }
        }
    }
}
