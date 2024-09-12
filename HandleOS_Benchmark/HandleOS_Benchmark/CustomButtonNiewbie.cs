using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace HandleOS_Benchmark
{
    public class CustomButtonNiewbie : Button
    {
        // Fields
        private int borderSize = 1;
        private int borderRadius = 30;
        private Color borderColor = Color.FromArgb(101, 125, 156);
        private Color backgroundColor = Color.WhiteSmoke;
        private Color textColor = Color.FromArgb(101, 125, 156);
        private Color highlightColor = Color.WhiteSmoke;
        private Color glowColor = Color.FromArgb(60, 255, 255, 255);
        private Color shadowColor = Color.FromArgb(10, 0, 0, 0);
        private int fontSize = 12;
        private float mGlowPositionPrecise = -320f;
        private int mGlowPosition = -320;
        private float glowSpeed = 0.2f;
        private bool isGlowEnabled = false;
        private bool isMouseHover = false;
        private System.Windows.Forms.Timer glowTimer;
        private Color fontColor = Color.Black;
        private string barText = "Custom Button";


        // Properties
        [Category("Niewbie Advance")]
        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public Color BackgroundColor
        {
            get => backgroundColor;
            set { backgroundColor = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public Color TextColor
        {
            get => textColor;
            set { textColor = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public Color HighlightColor
        {
            get => highlightColor;
            set { highlightColor = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public Color GlowColor
        {
            get => glowColor;
            set { glowColor = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public Color ShadowColor
        {
            get => shadowColor;
            set { shadowColor = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public int FontSize
        {
            get => fontSize;
            set { fontSize = value; Invalidate(); }
        }

        [Category("Niewbie Advance")]
        public float GlowSpeed
        {
            get => glowSpeed;
            set
            {
                glowSpeed = Math.Max(0.1f, Math.Min(value, 20f));
                Invalidate();
            }
        }

        [Category("Niewbie Advance")]
        public bool IsGlowEnabled
        {
            get => isGlowEnabled;
            set
            {
                isGlowEnabled = value;
                glowTimer.Enabled = value;
                Invalidate();
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
        public string BarText
        {
            get { return barText; }
            set
            {
                barText = value;
                this.Invalidate(); // Richiedi il ridisegno del controllo
            }
        }

        // Constructor
        public CustomButtonNiewbie()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.Text = "";
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
            this.MouseEnter += CustomButtonNiewbie_MouseEnter;
            this.MouseLeave += CustomButtonNiewbie_MouseLeave;

            // Initialize and setup the glow timer
            glowTimer = new System.Windows.Forms.Timer
            {
                Interval = 15
            };
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

        // Methods
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            // Ensure no default focus or highlight
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.Transparent;
            this.FlatAppearance.MouseDownBackColor = Color.Transparent; // Assicura trasparenza quando il mouse è premuto
            this.FlatAppearance.MouseOverBackColor = Color.Transparent; // Assicura trasparenza quando il mouse è sopra

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int padding = 2; // Add a small padding
            Rectangle rect = new Rectangle(padding, padding, this.Width - 2 * padding, this.Height - 2 * padding);

            DrawBackground(g, rect);
            DrawBackgroundShadows(g, rect);
            if (isMouseHover)
            {
                DrawHighlight(g, rect);
            }
            // Draw text in the center of the bar with support for text wrapping
            using (var font = new Font("Roboto Medium", this.fontSize))
            using (var brush = new SolidBrush(this.FontColor))
            using (var stringFormat = new StringFormat())
            {
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.FormatFlags = StringFormatFlags.LineLimit;
                stringFormat.Trimming = StringTrimming.EllipsisWord; // Line breaks and ellipsis if the text is too long

                // Create a rectangle for the text
                RectangleF textRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);

                // Draws text, allowing line breaks
                g.DrawString(BarText, font, brush, textRect, stringFormat);
            }
            DrawGlow(g, rect);
            DrawText(g, rect);
            DrawBorder(g, rect);
        }

        private void DrawBackground(Graphics g, Rectangle rect)
        {
            using (GraphicsPath path = GetFigurePath(rect, borderRadius))
            using (SolidBrush brush = new SolidBrush(backgroundColor))
            {
                g.FillPath(brush, path);
            }
        }

        private void DrawBackgroundShadows(Graphics g, Rectangle rect)
        {
            // Left shadow
            using (LinearGradientBrush leftShadowBrush = new LinearGradientBrush(
                new Point(rect.Left, rect.Top),
                new Point(rect.Left + rect.Width / 6, rect.Top),
                Color.Transparent,
                shadowColor))
            {
                g.FillRectangle(leftShadowBrush, rect.Left, rect.Top, rect.Width / 6, rect.Height);
            }

            // Right shadow
            using (LinearGradientBrush rightShadowBrush = new LinearGradientBrush(
                new Point(rect.Right - rect.Width / 6, rect.Top),
                new Point(rect.Right, rect.Top),
                shadowColor,
                Color.Transparent))
            {
                g.FillRectangle(rightShadowBrush, rect.Right - rect.Width / 6, rect.Top, rect.Width / 6, rect.Height);
            }
        }

        private void DrawHighlight(Graphics g, Rectangle rect)
        {
            using (GraphicsPath path = GetFigurePath(rect, borderRadius))
            using (Region region = new Region(path))
            {
                // Clip graphics to the button shape
                g.SetClip(region, CombineMode.Intersect);

                // Top highlight
                using (LinearGradientBrush topHighlightBrush = new LinearGradientBrush(
                    new Point(rect.Left, rect.Top),
                    new Point(rect.Left, rect.Top + rect.Height / 4),
                    Color.FromArgb(100, Color.White),
                    Color.Transparent))
                {
                    g.FillRectangle(topHighlightBrush, rect.Left, rect.Top, rect.Width, rect.Height / 4);
                }

                // Bottom highlight
                using (LinearGradientBrush bottomHighlightBrush = new LinearGradientBrush(
                    new Point(rect.Left, rect.Bottom - rect.Height / 4),
                    new Point(rect.Left, rect.Bottom),
                    Color.Transparent,
                    Color.FromArgb(100, highlightColor)))
                {
                    g.FillRectangle(bottomHighlightBrush, rect.Left, rect.Bottom - rect.Height / 4, rect.Width, rect.Height / 4);
                }

                // Reset the clip
                g.ResetClip();
            }
        }

        private void DrawGlow(Graphics g, Rectangle rect)
        {
            if (!isGlowEnabled) return;

            Rectangle glowRect = new Rectangle(mGlowPosition, rect.Top, 60, rect.Height);
            using (LinearGradientBrush glowBrush = new LinearGradientBrush(
                glowRect, Color.Transparent, glowColor, LinearGradientMode.Horizontal))
            {
                ColorBlend colorBlend = new ColorBlend(4)
                {
                    Colors = new Color[] { Color.Transparent, glowColor, glowColor, Color.Transparent },
                    Positions = new float[] { 0.0f, 0.4f, 0.6f, 1.0f }
                };
                glowBrush.InterpolationColors = colorBlend;

                g.FillRectangle(glowBrush, glowRect);
            }
        }

        private void DrawText(Graphics g, Rectangle rect)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                using (Font font = new Font(this.Font.FontFamily, fontSize))
                using (SolidBrush brush = new SolidBrush(textColor))
                {
                    g.DrawString(this.Text, font, brush, rect, sf);
                }
            }
        }

        private void DrawBorder(Graphics g, Rectangle rect)
        {
            if (borderSize > 0)
            {
                using (GraphicsPath path = GetFigurePath(rect, borderRadius))
                using (Pen pen = new Pen(borderColor, borderSize))
                {
                    pen.Alignment = PenAlignment.Center;
                    g.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            // Ensure that the curve size is not larger than the rectangle's width or height
            curveSize = Math.Min(curveSize, Math.Min(rect.Width, rect.Height));

            // Ensure that all values are positive
            if (rect.Width > 0 && rect.Height > 0 && curveSize > 0)
            {
                path.StartFigure();
                path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
                path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
                path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
                path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
                path.CloseFigure();
            }
            else
            {
                // If we can't draw a proper rounded rectangle, fall back to a regular rectangle
                path.AddRectangle(rect);
            }

            return path;
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }

        private void CustomButtonNiewbie_MouseEnter(object sender, EventArgs e)
        {
            // Trigger highlight effect on mouse enter
            isMouseHover = true;
            this.Invalidate();
        }

        private void CustomButtonNiewbie_MouseLeave(object sender, EventArgs e)
        {
            // Reset highlight effect on mouse leave
            isMouseHover = false;
            this.Invalidate();
        }
    }
}
