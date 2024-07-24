/*
Console2Desk

Copyright (C) 2023 Special-Niewbie Softwares

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation version 3.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Console2Desk
{
    public class Special_Niewbie_Button : Button
    {
        // Fields
        private int borderSize = 1;
        private int borderRadius = 20;
        private Color originalBackColor;
        private Color borderColor = Color.FromArgb(227, 227, 227);
        private Color hoverColor = Color.MediumSlateBlue;
        private bool clicked = false;
        private bool mouseExitedWhileClicked = false; // New variable to track mouse output during clicking

        // Properties
        [Category("Niewbie Advance")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        [Category("Niewbie Advance")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        [Category("Niewbie Advance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("Niewbie Advance")]
        public Color HoverColor
        {
            get { return hoverColor; }
            set
            {
                hoverColor = value;
                this.Invalidate();
            }
        }

        [Category("Niewbie Advance")]
        public Color TextColor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }

        // Constructor
        public Special_Niewbie_Button()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(240, 390);
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
            originalBackColor = Color.FromArgb(50, 50, 50);
            this.BackColor = originalBackColor;
        }

        private void Button_Resize(object? sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }

        // Methods
        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
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

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;

            if (borderRadius > 2) //Rounded button
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //Button surface
                    this.Region = new Region(pathSurface);
                    //Draw surface border for HD result
                    if (Parent != null)
                    {
                        using (Pen penSurface = new Pen(Parent.BackColor, smoothSize))
                        {
                            pevent.Graphics.DrawPath(penSurface, pathSurface);
                        }
                    }

                    //Button border                    
                    if (borderSize >= 1)
                        //Draw control border
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else //Normal button
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                //Button surface
                this.Region = new Region(rectSurface);
                //Button border
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }


        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            clicked = true;
            mouseExitedWhileClicked = false; // Resets the variable when the button is clicked
            this.BackColor = hoverColor;
            System.Diagnostics.Debug.WriteLine("Clicked");
            System.Diagnostics.Debug.WriteLine("MouseExitedWhileClicked: " + mouseExitedWhileClicked);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            clicked = false;
            mouseExitedWhileClicked = false; // Resets the variable when the mouse is released
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position))) // Check if the mouse is over the button
            {
                this.BackColor = originalBackColor; // Resets the original background color
            }
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            mouseExitedWhileClicked = false; // Resets the variable when the mouse is pressed
        }
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            if (clicked && !ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                mouseExitedWhileClicked = true; // Sets the variable if the mouse leaves the button area while it is clicked
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            System.Diagnostics.Debug.WriteLine("MouseLeave");
            System.Diagnostics.Debug.WriteLine("Clicked: " + clicked);
            System.Diagnostics.Debug.WriteLine("BackColor: " + this.BackColor.Name);

            if (!clicked)
            {
                this.BackColor = originalBackColor; // Resets the original background color only if the button has not been clicked
            }
            else if (mouseExitedWhileClicked)
            {
                this.BackColor = hoverColor; // Reset the background color to hoverColor if the button was clicked and the mouse left the area during this state
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            System.Diagnostics.Debug.WriteLine("MouseEnter");
            System.Diagnostics.Debug.WriteLine("Focused: " + this.Focused);
            System.Diagnostics.Debug.WriteLine("Clicked: " + clicked);
            System.Diagnostics.Debug.WriteLine("BackColor: " + this.BackColor.Name);
            System.Diagnostics.Debug.WriteLine("MouseExitedWhileClicked: " + mouseExitedWhileClicked);

            if ((!clicked && !this.Focused) || (clicked && mouseExitedWhileClicked && !ClientRectangle.Contains(PointToClient(Cursor.Position)))) // Changing conditions to properly handle the mouse state
            {
                this.BackColor = hoverColor; // Change the hover color only if the button was not clicked or if it was clicked but the mouse left the button area
            }
            else if (clicked && mouseExitedWhileClicked) // Let's add a condition to restore the color if the mouse left the button area while it was clicked
            {
                this.BackColor = hoverColor; // If the button was clicked previously and the mouse moved in and out of the button area, reset the background color to hoverColor
                mouseExitedWhileClicked = false; // Reset the variable
            }
        }

        private void Container_BackColorChanged(object? sender, EventArgs e)
        {
            this.Invalidate();
        }

    }
}
