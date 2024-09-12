using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace HandleOS_Benchmark
{
    public class CustomProgressBar : Control
    {
        private int value;
        private float borderThickness = 0.2f; // Spessore predefinito del bordo
        private int minValue = 0;
        private int radiusTopLeft = 8;
        private int radiusTopRight = 8;
        private int radiusBottomRight = 8;
        private int radiusBottomLeft = 8;
        private Color backgroundColor = Color.FromArgb(251, 251, 251);
        private Color highlightColor = Color.WhiteSmoke;
        private Color glowColor = Color.FromArgb(60, 255, 255, 255);
        private Color startColor = Color.FromArgb(167, 136, 235);
        private Color endColor = Color.FromArgb(104, 178, 234);
        private Color shadowColor = Color.FromArgb(10, 0, 0, 0); // Colore predefinito dell'ombra
        private string barText = "";
        private Color fontColor = Color.White;
        private int fontSize = 7;
        private int fontPositionX = 5;
        private float mGlowPositionPrecise = -320f;
        private int mGlowPosition = -320;
        private int maxValue = 100;
        private float glowSpeed = 0.2f; // Controls the speed of the glow effect
        private bool isGlowEnabled = true; // Toggles the glow effect on/off
        private System.Windows.Forms.Timer glowTimer; // Timer for animating the glow effect

        // Properties
        [Category("Niewbie Advance")]
        public int Value
        {

            get { return value; }
            set
            {
                this.value = Math.Max(Math.Min(value, MaxValue), MinValue);
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public int MinValue
        {

            get { return minValue; }
            set
            {
                minValue = value;
                this.Invalidate();
            }


        }
        [Category("Niewbie Advance")]
        public int RadiusTopLeft
        {

            get { return radiusTopLeft; }
            set
            {
                radiusTopLeft = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public int RadiusTopRight
        {

            get { return radiusTopRight; }
            set
            {
                radiusTopRight = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public int RadiusBottomRight
        {
            get { return radiusBottomRight; }
            set
            {
                radiusBottomRight = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public int RadiusBottomLeft
        {
            get { return radiusBottomLeft; }
            set
            {
                radiusBottomLeft = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public Color HighlightColor
        {

            get { return highlightColor; }
            set
            {
                highlightColor = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public Color GlowColor
        {
            get { return glowColor; }
            set
            {
                glowColor = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public Color StartColor
        {
            get { return startColor; }
            set
            {

                startColor = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public Color EndColor
        {
            get { return endColor; }
            set
            {
                endColor = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        public int MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                this.Invalidate();
            }

        }
        [Category("Niewbie Advance")]
        // Property to control glow speed
        public float GlowSpeed
        {
            get { return glowSpeed; }
            set
            {
                glowSpeed = Math.Max(0.1f, Math.Min(value, 20f)); // Limit speed between 0.1 and 20
                this.Invalidate();
            }
        }
        [Category("Niewbie Advance")]
        // Property to enable/disable glow effect
        public bool IsGlowEnabled
        {
            get { return isGlowEnabled; }
            set
            {
                isGlowEnabled = value;
                glowTimer.Enabled = value; // Start/stop the timer based on the enabled state
                this.Invalidate();
            }
        }
        [Category("Niewbie Advance")]
        public Color ShadowColor
        {
            get { return shadowColor; }
            set
            {
                shadowColor = value;
                this.Invalidate(); // Ridisegna il controllo quando il colore dell'ombra cambia
            }
        }
        [Category("Niewbie Advance")]
        public float BorderThickness
        {
            get { return borderThickness; }
            set
            {
                borderThickness = Math.Max(0, Math.Min(value, 5)); // Limitiamo lo spessore tra 0 e 5
                this.Invalidate(); // Ridisegna il controllo quando lo spessore del bordo cambia
            }
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

        [Category("Niewbie Advance")]
        public int FontPositionX
        {
            get { return fontPositionX; }
            set
            {
                fontPositionX = value;
                this.Invalidate(); // Richiedi il ridisegno del controllo
            }
        }

        // Constructor: Initializes the custom progress bar
        public CustomProgressBar()
        {
            // Set control styles for optimal rendering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.Size = new Size(200, 15);
            BackColor = Color.Transparent;

            // Initialize and setup the glow timer
            glowTimer = new System.Windows.Forms.Timer();
            glowTimer.Interval = 15;
            glowTimer.Tick += (s, e) =>
            {
                if (isGlowEnabled)
                {
                    mGlowPositionPrecise += glowSpeed;
                    if (mGlowPositionPrecise > this.Width)
                        mGlowPositionPrecise = -320f;
                    mGlowPosition = (int)mGlowPositionPrecise;
                    this.Invalidate();
                }
            };
            glowTimer.Start();
        }

        // Paint event handler
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            DrawBackground(e.Graphics);//ok
            DrawBackgroundShadows(e.Graphics);//ok
            DrawBar(e.Graphics);//ok
            DrawBarShadows(e.Graphics);//ok
            DrawHighlight(e.Graphics);
            DrawInnerStroke(e.Graphics);//ok
            DrawGlow(e.Graphics);//ok
            DrawOuterStroke(e.Graphics);//ok
        }

        // Draw background
        private void DrawBackground(Graphics g)
        {
            Rectangle r = this.ClientRectangle;
            r.Width--;
            r.Height--;
            GraphicsPath path = RoundRect(r, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft);
            g.FillPath(new SolidBrush(this.BackgroundColor), path);
        }

        // Draw background shadows
        private void DrawBackgroundShadows(Graphics g)
        {
            // Ombra sinistra
            Rectangle lr = new Rectangle(2, 2, (radiusTopLeft + radiusBottomLeft) / 2 + 10, this.Height - 5);
            LinearGradientBrush leftShadowBrush = new LinearGradientBrush(lr, shadowColor, Color.Transparent, LinearGradientMode.Horizontal);
            GraphicsPath leftShadowPath = RoundRect(lr, radiusTopLeft, 0, 0, radiusBottomLeft);
            g.FillPath(leftShadowBrush, leftShadowPath);

            // Ombra destra
            Rectangle rr = new Rectangle(this.Width - (radiusTopRight + radiusBottomRight) / 2 - 12, 2, (radiusTopRight + radiusBottomRight) / 2 + 10, this.Height - 5);
            LinearGradientBrush rightShadowBrush = new LinearGradientBrush(rr, Color.Transparent, shadowColor, LinearGradientMode.Horizontal);
            GraphicsPath rightShadowPath = RoundRect(rr, 0, radiusTopRight, radiusBottomRight, 0);
            g.FillPath(rightShadowBrush, rightShadowPath);
        }

        //// Draw progress bar

        private void DrawBar(Graphics g)
        {
            Rectangle barRect = new Rectangle(1, 0, this.Width - 2, this.Height);
            int filledWidth = (int)(Value * 1.0F / (MaxValue - MinValue) * (this.Width - 2));
            barRect.Width = filledWidth;

            using (GraphicsPath barPath = RoundRect(barRect, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft))
            using (GraphicsPath clientPath = RoundRect(this.ClientRectangle, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft))
            {
                Region regionBar = new Region(barPath);
                Region regionClient = new Region(clientPath);
                regionBar.Intersect(regionClient);

                g.SetClip(regionBar, CombineMode.Replace);

                // Create a LinearGradientBrush for the entire width of the control
                using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(this.Width, 0),
                    startColor,
                    endColor))
                {
                    // Fill the entire bar area with the gradient
                    g.FillRectangle(gradientBrush, 0, 0, this.Width, this.Height);
                }

                // Disegna il testo al centro della barra
                using (var font = new Font("Roboto", this.fontSize))
                using (var brush = new SolidBrush(this.FontColor))
                {
                    var textSize = g.MeasureString(BarText, font);
                    var textX = this.fontPositionX;
                    var textY = (this.Height + 3 - textSize.Height) / 2;
                    g.DrawString(BarText, font, brush, textX, textY);
                }

                g.ResetClip();
            }
        }

        // Draw shadows on the bar
        private void DrawBarShadows(Graphics g)
        {
            // Left shadow
            Rectangle leftShadowRect = new Rectangle(1, 2, this.Width / 6, this.Height - 3);
            LinearGradientBrush leftShadowBrush = new LinearGradientBrush(leftShadowRect, Color.White, Color.White, LinearGradientMode.Horizontal);
            ColorBlend leftShadowBlend = new ColorBlend(3)
            {
                Colors = new Color[] { Color.Transparent, Color.FromArgb(255, 0, 0, 0), Color.Transparent },
                Positions = new float[] { 0.0F, 0.2F, 1.0F }
            };
            leftShadowBrush.InterpolationColors = leftShadowBlend;
            GraphicsPath leftShadowPath = new GraphicsPath();
            leftShadowPath.AddRectangle(leftShadowRect);
            Region leftShadowRegion = new Region(leftShadowPath);
            Region barRegion = new Region(RoundRect(this.ClientRectangle, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft));
            leftShadowRegion.Intersect(barRegion);

            using (LinearGradientBrush brushLR = new LinearGradientBrush(leftShadowRect, Color.White, Color.White, LinearGradientMode.Horizontal))
            {
                ColorBlend blendLR = new ColorBlend(3)
                {
                    Colors = new Color[] { Color.Transparent, shadowColor, Color.Transparent },
                    Positions = new float[] { 0.0F, 0.2F, 1.0F }
                };
                brushLR.InterpolationColors = blendLR;
                g.FillRegion(brushLR, leftShadowRegion);
            }

            // Right shadow
            Rectangle rightShadowRect = new Rectangle(this.Width - 3, 2, this.Width / 6, this.Height - 3);
            rightShadowRect.X = (int)(Value * 1.0F / (MaxValue - MinValue) * this.Width) - this.Width / 6;
            LinearGradientBrush rightShadowBrush = new LinearGradientBrush(rightShadowRect, Color.Black, Color.Black, LinearGradientMode.Horizontal);
            ColorBlend rightShadowBlend = new ColorBlend(3)
            {
                Colors = new Color[] { Color.Transparent, Color.FromArgb(50, 0, 0, 0), Color.Transparent },
                Positions = new float[] { 0.0F, 0.8F, 1.0F }
            };
            rightShadowBrush.InterpolationColors = rightShadowBlend;
            GraphicsPath rightShadowPath = RoundRect(rightShadowRect, 0, radiusTopRight, radiusBottomRight, 0);
            Region rightShadowRegion = new Region(rightShadowPath);
            rightShadowRegion.Intersect(barRegion);

            using (LinearGradientBrush brushRR = new LinearGradientBrush(rightShadowRect, Color.Black, Color.Black, LinearGradientMode.Horizontal))
            {
                ColorBlend blendRR = new ColorBlend(3)
                {
                    Colors = new Color[] { Color.Transparent, shadowColor, Color.Transparent },
                    Positions = new float[] { 0.0F, 0.8F, 1.0F }
                };
                brushRR.InterpolationColors = blendRR;
                g.FillRegion(brushRR, rightShadowRegion);
            }
        }

        private void DrawHighlight(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
                
                Rectangle tr = new Rectangle(0, 0, this.Width, this.Height / 4);
            GraphicsPath tp = RoundRect(tr, radiusTopLeft, radiusTopRight, 0, 0);

            //Rectangle br = new Rectangle(1, (int) (this.Height - (float)this.Height/5), this.Width, (int)((float) this.Height/5));
            //GraphicsPath bp = RoundRect(br, 0, 0, radiusBottomRight, radiusBottomLeft);

            Rectangle br = new Rectangle(1, this.Height - 8, this.Width, 7);
            GraphicsPath bp = RoundRect(br, 0, 0, radiusBottomRight, radiusBottomLeft);

            Region regionTP = new Region(tp);
            Region regionBP = new Region(bp);

            Rectangle ri = this.ClientRectangle;
            ri.X++; ri.Y++; ri.Width -= 1; ri.Height -= 1;
            GraphicsPath rri = RoundRect(ri, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft);
            Region regionRRI = new Region(rri);

            regionTP.Intersect(regionRRI);

            regionBP.Intersect(regionRRI);

            using (LinearGradientBrush tg = new LinearGradientBrush(tr, Color.White, Color.FromArgb(100, Color.White), LinearGradientMode.Vertical))
            {
                g.FillRegion(tg, regionTP);
            }

            using (LinearGradientBrush bg = new LinearGradientBrush(br, Color.Transparent, Color.FromArgb(100, this.HighlightColor), LinearGradientMode.Vertical))
            {
                g.FillRegion(bg, regionBP);
            }

            g.ResetClip();
        }

        // Draw inner stroke
        private void DrawInnerStroke(Graphics g)
        {
            if (borderThickness <= 0) return; // Non disegnare se lo spessore è 0 o negativo

            Rectangle strokeRect = new Rectangle(
                (int)(borderThickness / 2),
                (int)(borderThickness / 2),
                this.Width - (int)borderThickness,
                this.Height - (int)borderThickness);

            using (Pen pen = new Pen(Color.FromArgb(178, 178, 178), borderThickness))
            {
                GraphicsPath path = RoundRect(strokeRect, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft);
                g.DrawPath(pen, path);
            }
        }

        // Modifica il metodo DrawOuterStroke per usare il nuovo spessore del bordo
        private void DrawOuterStroke(Graphics g)
        {
            if (borderThickness <= 0) return; // Non disegnare se lo spessore è 0 o negativo

            Rectangle outerStrokeRect = new Rectangle(
                (int)(borderThickness / 2),
                (int)(borderThickness / 2),
                this.Width - (int)borderThickness - 1,
                this.Height - (int)borderThickness - 1);

            using (Pen pen = new Pen(Color.FromArgb(120, 120, 120), borderThickness))
            {
                GraphicsPath path = RoundRect(outerStrokeRect, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft);
                g.DrawPath(pen, path);
            }
        }

        // Draw glow effect
        private void DrawGlow(Graphics g)
        {
            if (!isGlowEnabled) return; // Skip drawing glow if it's disabled
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle r = new Rectangle((int)mGlowPosition, 0, 60, this.Height);
            LinearGradientBrush lgb = new LinearGradientBrush(r, Color.White, Color.White, LinearGradientMode.Horizontal);
            ColorBlend cb = new ColorBlend(4);
            cb.Colors = new Color[] { Color.Transparent, this.GlowColor, this.GlowColor, Color.Transparent };
            cb.Positions = new float[] { 0.0F, 0.5F, 0.6F, 1.0F };
            lgb.InterpolationColors = cb;

            Rectangle clip = new Rectangle(1, 2, this.Width - 3, this.Height - 3);
            clip.Width = (int)(Value * 1.0F / (MaxValue - MinValue) * this.Width);

            Rectangle rPath = this.ClientRectangle;
            rPath.X++; rPath.Y++; rPath.Width -= 3; rPath.Height -= 3;
            GraphicsPath rr = RoundRect(rPath, radiusTopLeft, radiusTopRight, radiusBottomRight, radiusBottomLeft);

            GraphicsPath glowPath = new GraphicsPath();
            glowPath.AddRectangle(r);
            Region glowRegion = new Region(glowPath);
            Region pathRegion = new Region(rr);

            glowRegion.Intersect(pathRegion);

            using (LinearGradientBrush brush = new LinearGradientBrush(r, Color.White, Color.White, LinearGradientMode.Horizontal))
            {
                brush.InterpolationColors = cb;
                g.FillRegion(brush, glowRegion);
            }
            g.ResetClip();
        }
        //Create rounded rectangle path
        private GraphicsPath RoundRect(Rectangle bounds, int radiusTopLeft, int radiusTopRight, int radiusBottomRight, int radiusBottomLeft)
        {
            int diameterTopLeft = Math.Max(radiusTopLeft * 2, 0);
            int diameterTopRight = Math.Max(radiusTopRight * 2, 0);
            int diameterBottomRight = Math.Max(radiusBottomRight * 2, 0);
            int diameterBottomLeft = Math.Max(radiusBottomLeft * 2, 0);
            Size sizeTopLeft = new Size(diameterTopLeft, diameterTopLeft);
            Size sizeTopRight = new Size(diameterTopRight, diameterTopRight);
            Size sizeBottomRight = new Size(diameterBottomRight, diameterBottomRight);
            Size sizeBottomLeft = new Size(diameterBottomLeft, diameterBottomLeft);
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            // Top left arc
            if (diameterTopLeft > 0)
            {
                path.AddArc(new Rectangle(bounds.Location, sizeTopLeft), 180, 90);
            }
            else
            {
                path.AddLine(bounds.Left, bounds.Top, bounds.Left, bounds.Top);
            }

            // Top edge
            path.AddLine(bounds.Left + radiusTopLeft, bounds.Top, bounds.Right - radiusTopRight, bounds.Top);

            // Top right arc
            if (diameterTopRight > 0)
            {
                path.AddArc(new Rectangle(new Point(bounds.Right - diameterTopRight, bounds.Top), sizeTopRight), 270, 90);
            }
            else
            {
                path.AddLine(bounds.Right, bounds.Top, bounds.Right, bounds.Top);
            }

            // Right edge
            path.AddLine(bounds.Right, bounds.Top + radiusTopRight, bounds.Right, bounds.Bottom - radiusBottomRight);
            
            // Bottom right arc
            if (diameterBottomRight > 0)
            {
                path.AddArc(new Rectangle(new Point(bounds.Right - diameterBottomRight, bounds.Bottom - diameterBottomRight), sizeBottomRight), 0, 90);
            }
            else
            {
                path.AddLine(bounds.Right, bounds.Bottom, bounds.Right, bounds.Bottom);
            }
                
            // Bottom edge
            path.AddLine(bounds.Right - radiusBottomRight, bounds.Bottom, bounds.Left + radiusBottomLeft, bounds.Bottom);
            
            // Bottom left arc
            if (diameterBottomLeft > 0)
            {
                path.AddArc(new Rectangle(new Point(bounds.Left, bounds.Bottom - diameterBottomLeft), sizeBottomLeft), 90, 90);
            }
            else
            {
                path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Bottom);
            }
            // Left edge
            path.AddLine(bounds.Left, bounds.Bottom - radiusBottomLeft, bounds.Left, bounds.Top + radiusTopLeft);
            path.CloseFigure();
            return path;
        }
        // Get intermediate color between start and end colors
        private Color GetIntermediateColor()
        {
            float proportion = (Value - MinValue) / (float)(MaxValue - MinValue);
            return InterpolateColor(StartColor, EndColor, proportion);
        }
        // Interpolate color
        private Color InterpolateColor(Color start, Color end, float proportion)
        {
            int r = (int)(start.R + (end.R - start.R) * proportion);
            int g = (int)(start.G + (end.G - start.G) * proportion);
            int b = (int)(start.B + (end.B - start.B) * proportion);
            return Color.FromArgb(r, g, b);
        }

    }
}
