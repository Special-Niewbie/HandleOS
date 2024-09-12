using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Console2Desk.info
{
    public partial class FormInfoControllers : Form
    {
        private bool _isDragging = false;
        private Point _dragStartPoint;

        public FormInfoControllers(Form form, System.Windows.Forms.Timer timer)
        {
            InitializeComponent();

            panelTitleInfoControllers.MouseDown += panelTitleInfoControllers_MouseDown;
            panelTitleInfoControllers.MouseMove += panelTitleInfoControllers_MouseMove;
            panelTitleInfoControllers.MouseUp += panelTitleInfoControllers_MouseUp;
            labelTitleInfoControllers.MouseDown += labelTitleInfoControllers_MouseDown;
            labelTitleInfoControllers.MouseMove += labelTitleInfoControllers_MouseMove;
            labelTitleInfoControllers.MouseUp += labelTitleInfoControllers_MouseUp;
        }

        
               

        private void buttonCloseWindowInfoControllers_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormInfoControllers_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           

        }

        private void panelTitleInfoControllers_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelTitleInfoControllers_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging Window
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void panelTitleInfoControllers_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Calculate the new position of the window
                var deltaX = e.X - _dragStartPoint.X;
                var deltaY = e.Y - _dragStartPoint.Y;
                var newLocation = new Point(this.Left + deltaX, this.Top + deltaY);
                this.Location = newLocation;
            }
        }

        private void panelTitleInfoControllers_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging Window
            _isDragging = false;
        }

        private void labelTitleInfoControllers_Click(object sender, EventArgs e)
        {

        }
        private void labelTitleInfoControllers_MouseDown(object sender, MouseEventArgs e)
        {
            // Start dragging Window
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void labelTitleInfoControllers_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Calculate the new position of the window
                var deltaX = e.X - _dragStartPoint.X;
                var deltaY = e.Y - _dragStartPoint.Y;
                var newLocation = new Point(this.Left + deltaX, this.Top + deltaY);
                this.Location = newLocation;
            }
        }

        private void labelTitleInfoControllers_MouseUp(object sender, MouseEventArgs e)
        {
            // Stop dragging Window
            _isDragging = false;
        }
    }
}
