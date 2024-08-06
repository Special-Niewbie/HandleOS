using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk.ToggleSwitchDev
{
    public class ControlToggles : Control
    {
        private float diameter;
        private RectangleShape rect;
        private RectangleF circle;
        private bool isON;
        private float artis;
        private Color borderColor;
        private bool textEnabled;
        private string OnTex = "";
        private string OffTex = "";
        public Color OnCol;
        public Color OffCol;
        private System.Windows.Forms.Timer painTicker = new System.Windows.Forms.Timer();
        public event SliderChangedEventHandler SliderValueChanged;

        public ControlToggles()
        {

            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
            this.artis = 4f;
            this.diameter = 30f;
            this.textEnabled = true;
            this.rect = new RectangleShape(2f * this.diameter, this.diameter + 2f, this.diameter / 2f, 1f, 1f);
            this.circle = new RectangleF(1f, 1f, this.diameter, this.diameter);
            this.isON = false;
            this.borderColor = Color.LightGray;
            this.painTicker.Tick += new EventHandler(this.paintTicker_Tick);
            this.painTicker.Interval = 1;
            this.OnCol = Color.FromArgb(123, 104, 238);
            this.OffCol = Color.DarkGray;
            this.ForeColor = Color.White;
            this.OnTex = "ON";
            this.OffTex = "OFF";
        }
        protected override void OnEnabledChanged(EventArgs e)
        {

            base.Invalidate();
            base.OnEnabledChanged(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                this.isON = !this.isON;
                this.IsOn = this.isON;
                base.OnMouseClick(e);

            }

        }
        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = (SmoothingMode)SmoothingMode.HighQuality;
            if (base.Enabled)
            {
                Pen pen;
                using (SolidBrush brush = new SolidBrush(this.isON ? this.OnCol : this.OffCol))
                {
                    e.Graphics.FillPath((Brush)brush, this.rect.Path);
                }
                using (pen = new Pen(this.borderColor, 2f))
                {
                    e.Graphics.DrawPath(pen, this.rect.Path);
                }
                if (this.textEnabled)
                {
                    using (Font font = new Font("Segoe UI", (8.2f * this.diameter) / 46f, (FontStyle)FontStyle.Bold))
                    {
                        SolidBrush b = new SolidBrush(this.ForeColor);
                        int height = TextRenderer.MeasureText(this.OnTex, font).Height;
                        float num2 = (this.diameter - height) / 2f;
                        e.Graphics.DrawString(this.OnTex, font, b, 5f, num2 + 1f);
                        height = TextRenderer.MeasureText(this.OffTex, font).Height;
                        num2 = (this.diameter - height) / 2f;
                        e.Graphics.DrawString(this.OffTex, font, b, this.diameter + 2f, num2 + 1f);
                    }
                    using (SolidBrush brush2 = new SolidBrush("#E3E3E3".FromHex()))
                    {
                        e.Graphics.FillEllipse((Brush)brush2, this.circle);
                    }
                    using (pen = new Pen(Color.LightGray, 1.2f))
                    {
                        e.Graphics.DrawEllipse(pen, this.circle);
                    }
                }
                else
                {
                    using (SolidBrush brush3 = new SolidBrush("#E3E3E3".FromHex()))
                    {
                        using (SolidBrush brush4 = new SolidBrush("#E3E3E3".FromHex()))
                        {
                            e.Graphics.FillPath((Brush)brush3, this.rect.Path);
                            e.Graphics.FillEllipse((Brush)brush4, this.circle);
                            e.Graphics.DrawEllipse(Pens.DarkGray, this.circle);
                        }
                    }
                }
            }
            base.OnPaint(e);
        }
        protected override void OnResize(EventArgs e)
        {
            base.Width = (base.Height - 2) * 2;
            this.diameter = base.Width / 2;
            this.artis = (4f * this.diameter) * 30f;
            this.rect = new RectangleShape(2f * this.diameter, this.diameter + 2f, this.diameter / 2f, 1f, 1f);
            this.circle = new RectangleF(!this.isON ? 1f : ((base.Width - this.diameter) - 1f), 1f, this.diameter, this.diameter);
            base.OnResize(e);
        }

        private void paintTicker_Tick(object sender, EventArgs e)
        {
            float x = this.circle.X;
            if (this.isON)
            {
                if ((x + this.artis) <= ((base.Width - this.diameter) - 1f))
                {
                    x += this.artis;
                    this.circle = new RectangleF(x, 1f, this.diameter, this.diameter);
                    base.Invalidate();
                }
                else
                {
                    x = (base.Width - this.diameter) - 1f;
                    this.circle = new RectangleF(x, 1f, this.diameter, this.diameter);
                    base.Invalidate();
                    this.painTicker.Stop();
                }
            }
            else if ((x - this.artis) >= 1f)
            {
                x -= this.artis;
                this.circle = new RectangleF(x, 1f, this.diameter, this.diameter);
            }
            else
            {
                x = 1f;
                this.circle = new RectangleF(x, 1f, this.diameter, this.diameter);
                base.Invalidate();
                this.painTicker.Stop();
            }
        }
        public Color OnColor
        {
            get => this.OnCol;
            set
            {
                this.OnCol = value;
                base.Invalidate();
            }
        }

        public Color OffColor
        {
            get => this.OffCol;
            set
            {
                this.OffCol = value;
                base.Invalidate();
            }
        }
        public bool TextEnabled
        {
            get =>
                this.textEnabled;
            set
            {
                this.textEnabled = value;
                base.Invalidate();
            }
        }
        public bool IsOn
        {
            get =>
                this.isON;
            set
            {
                this.painTicker.Stop();
                this.isON = value;
                this.painTicker.Start();
                if (this.SliderValueChanged != null)
                {
                    this.SliderValueChanged(this, EventArgs.Empty);
                }
            }
        }
        public Color BorderColor
        {
            get =>
            this.borderColor;
            set
            {

                this.borderColor = value;
                base.Invalidate();

            }
        }
        protected override Size DefaultSize
            => new Size(60, 35);
        public delegate void SliderChangedEventHandler(object sender, EventArgs e);
        public string OnText
        {
            get =>
                this.OnTex;
            set
            {
                this.OnTex = value;
                base.Invalidate();
            }
        }
        public string OffText
        {
            get =>
                this.OffTex;
            set
            {
                this.OffTex = value;
                base.Invalidate();
            }
        }
    }
}
