using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Console2Desk.HiberSleep
{
    public partial class FormHiberSleep : Form
    {
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private int borderRadius = 16;
        private int borderSize = 3;
        private Color borderColor = Color.FromArgb(83, 83, 83);

        private bool isInitializing = true;

        public FormHiberSleep(Form form, System.Windows.Forms.Timer timer)
        {
            InitializeComponent();

            this.Padding = new Padding(borderSize);
            this.BackColor = borderColor;

            this.Paint += Form1_Paint;
            this.ResizeEnd += Form1_ResizeEnd;
            this.SizeChanged += Form1_SizeChanged;
            this.Activated += Form1_Activated;
            labelTitleHiberSleep.MouseDown += labelTitleHiberSleep_MouseDown;
            panelTitleBar.MouseDown += panelTitleBar_MouseDown;
            panelTitleBar.Paint += panelTitleBar_Paint;
            panelContainer.Paint += panelContainer_Paint;
            panelContainer.MouseDown += panelContainer_MouseDown;

            radioButtonEnabled.CheckedChanged += radioButtonEnabled_CheckedChanged;
            radioButtonDisabled.CheckedChanged += radioButtonDisabled_CheckedChanged;

        }

        private void FormHiberSleep_Load(object sender, EventArgs e)
        {
            InitializeRadioButtons();
        }

        private GraphicsPath GetTopRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            // Top left corner
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            // Top right corner
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            // Bottom right (no curve)
            path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
            // Close the path
            path.CloseFigure();
            return path;
        }

        private void ControlRegionAndBorderTopOnly(Control control, float radius, Graphics graph, Color borderColor)
        {
            using (GraphicsPath roundPath = GetTopRoundedPath(control.ClientRectangle, radius))
            using (Pen penBorder = new Pen(borderColor, 1))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                control.Region = new Region(roundPath);
                graph.DrawPath(penBorder, roundPath);
            }
        }

        private void panelTitleBar_Paint(object sender, PaintEventArgs e)
        {
            // Applica l'arrotondamento anche al panelTitleBar
            ControlRegionAndBorderTopOnly(panelTitleBar, borderRadius - (borderSize / 2), e.Graphics, borderColor);
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void labelTitleHiberSleep_Click(object sender, EventArgs e)
        {

        }
        private void labelTitleHiberSleep_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- Minimize borderless form from taskbar
                return cp;
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        private void FormRegionAndBorder(Form form, float radius, Graphics graph, Color borderColor, float borderSize)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                using (GraphicsPath roundPath = GetRoundedPath(form.ClientRectangle, radius))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                using (Matrix transform = new Matrix())
                {
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    form.Region = new Region(roundPath);
                    if (borderSize >= 1)
                    {
                        Rectangle rect = form.ClientRectangle;
                        float scaleX = 1.0F - ((borderSize + 1) / rect.Width);
                        float scaleY = 1.0F - ((borderSize + 1) / rect.Height);

                        transform.Scale(scaleX, scaleY);
                        transform.Translate(borderSize / 1.6F, borderSize / 1.6F);

                        graph.Transform = transform;
                        graph.DrawPath(penBorder, roundPath);
                    }
                }
            }
        }

        private void ControlRegionAndBorder(Control control, float radius, Graphics graph, Color borderColor)
        {
            using (GraphicsPath roundPath = GetRoundedPath(control.ClientRectangle, radius))
            using (Pen penBorder = new Pen(borderColor, 1))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                control.Region = new Region(roundPath);
                graph.DrawPath(penBorder, roundPath);
            }
        }

        private void DrawPath(Rectangle rect, Graphics graph, Color color)
        {
            using (GraphicsPath roundPath = GetRoundedPath(rect, borderRadius))
            using (Pen penBorder = new Pen(color, 3))
            {
                graph.DrawPath(penBorder, roundPath);
            }
        }

        private struct FormBoundsColors
        {
            public Color TopLeftColor;
            public Color TopRightColor;
            public Color BottomLeftColor;
            public Color BottomRightColor;
        }
        private FormBoundsColors GetFormBoundsColors()
        {
            var fbColor = new FormBoundsColors();
            using (var bmp = new Bitmap(1, 1))
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle rectBmp = new Rectangle(0, 0, 1, 1);

                //Top Left
                rectBmp.X = this.Bounds.X - 1;
                rectBmp.Y = this.Bounds.Y;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.TopLeftColor = bmp.GetPixel(0, 0);

                //Top Right
                rectBmp.X = this.Bounds.Right;
                rectBmp.Y = this.Bounds.Y;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.TopRightColor = bmp.GetPixel(0, 0);

                //Bottom Left
                rectBmp.X = this.Bounds.X;
                rectBmp.Y = this.Bounds.Bottom;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.BottomLeftColor = bmp.GetPixel(0, 0);

                //Bottom Right
                rectBmp.X = this.Bounds.Right;
                rectBmp.Y = this.Bounds.Bottom;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.BottomRightColor = bmp.GetPixel(0, 0);
            }
            return fbColor;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //-> SMOOTH OUTER BORDER
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectForm = this.ClientRectangle;
            int mWidht = rectForm.Width / 2;
            int mHeight = rectForm.Height / 2;
            var fbColors = GetFormBoundsColors();

            //Top Left
            DrawPath(rectForm, e.Graphics, fbColors.TopLeftColor);

            //Top Right
            Rectangle rectTopRight = new Rectangle(mWidht, rectForm.Y, mWidht, mHeight);
            DrawPath(rectTopRight, e.Graphics, fbColors.TopRightColor);

            //Bottom Left
            Rectangle rectBottomLeft = new Rectangle(rectForm.X, rectForm.X + mHeight, mWidht, mHeight);
            DrawPath(rectBottomLeft, e.Graphics, fbColors.BottomLeftColor);

            //Bottom Right
            Rectangle rectBottomRight = new Rectangle(mWidht, rectForm.Y + mHeight, mWidht, mHeight);
            DrawPath(rectBottomRight, e.Graphics, fbColors.BottomRightColor);

            //-> SET ROUNDED REGION AND BORDER
            FormRegionAndBorder(this, borderRadius, e.Graphics, borderColor, borderSize);
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            ControlRegionAndBorder(panelContainer, borderRadius - (borderSize / 2), e.Graphics, borderColor);
        }
        private void panelContainer_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void buttonCloseFormHiberSleep_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButtonDisabled_CheckedChanged(object sender, EventArgs e)
        {
            if (isInitializing || !radioButtonDisabled.Checked) return;

            try
            {
                var radioFunctions = new RadioButtonDisabledFunctions();
                bool removalSuccessful = radioFunctions.ProcessHibernationRemoval();

                if (!removalSuccessful)
                {
                    radioButtonDisabled.Checked = false;
                }
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage(
                    $"An error occurred:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK
                );
                radioButtonDisabled.Checked = false;
            }
        }

        private void radioButtonEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (isInitializing || !radioButtonEnabled.Checked) return;

            try
            {
                var radioFunctions = new RadioButtonEnabledFunctions();
                bool setupSuccessful = radioFunctions.ProcessHibernationSetup();

                if (!setupSuccessful)
                {
                    radioButtonEnabled.Checked = false;
                }
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage(
                    $"An error occurred:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK
                );
                radioButtonEnabled.Checked = false;
            }
        }
        private void InitializeRadioButtons()
        {
            try
            {
                isInitializing = true;
                bool hibernateEnabled = RadioButtonsCheckStatus.IsHibernateEnabled();

                // Imposta direttamente le proprietà Checked
                if (hibernateEnabled)
                {
                    radioButtonEnabled.Checked = true;
                    radioButtonDisabled.Checked = false;
                }
                else
                {
                    radioButtonEnabled.Checked = false;
                    radioButtonDisabled.Checked = true;
                }
            }
            catch (Exception ex)
            {
                DependencyContainer.MessagesBoxImplementation.ShowMessage(
                    $"Error checking hibernation state:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK
                );
            }
            finally
            {
                isInitializing = false;
            }
        }
    }
}
