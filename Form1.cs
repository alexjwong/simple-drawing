using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simple_drawing
{
    public partial class Form1 : Form
    {
        // Array list of all graphics objects
        private List<GraphicsElement> gobjects = new List<GraphicsElement>();

        // Variable to track current state (ie what radio button of graphics objects is checked off)
        private string CurrentDraw;
        private Brush CurrentBrush = Brushes.Black;
        private Brush CurrentFill = Brushes.White;
        private Pen CurrentPen = Pens.Black;
        private int CurrentPenWidth = 1;
        private string CurrentText = "";
        private bool Fill, Outline = false;
        
        // Points to determine object location
        private Point point1, point2;
        private Point origin;

        // boolean to determine whether the first click has been entered 
        // (in drawing the rectangle that contains the object)
        private bool FirstClick = false;

        public Form1()
        {
            InitializeComponent();

            // Set default values for controls
            this.PenColor.SetSelected(0, true);
            this.FillColor.SetSelected(0, true);
            this.PenWidth.SetSelected(0, true);
            this.LineButton.Select();
        }

        // All paint and clicks for the graphics objects happen in the lower panel
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            // Graphics object g
            Graphics g = e.Graphics;

            // Draw each graphics element
            foreach (GraphicsElement gobject in this.gobjects)
            {
                gobject.Draw(g);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // use the Form paint event to continually refresh the DrawPanel
            // and force DrawPanel_Paint to be called
            DrawPanel.Refresh();
        }

        private void DrawPanel_MouseClick(object sender, MouseEventArgs e)
        {
            // Both left and right clicks do the same thing
            if (FirstClick) // if first click happened, process second click
            {
                point2 = new Point(e.X, e.Y);
                FirstClick = false;

                // Make the current pen
                CurrentPen = new Pen(CurrentBrush, CurrentPenWidth);

                // Add the object after a second point has been determined
                if (CurrentDraw == "Line")
                {
                    this.gobjects.Add(new Line(CurrentPen, point1, point2));
                }
                else if (CurrentDraw == "Rectangle")
                {
                    if (Fill || Outline)    // only add object if fill or outline is checked
                    {
                        // Given the two points, find the upper left point to use as origin
                        origin.X = Math.Min(point1.X, point2.X);
                        origin.Y = Math.Min(point1.Y, point2.Y);
                        int recwidth = Math.Abs(point2.X - point1.X);
                        int recheight = Math.Abs(point2.Y - point1.Y);
                        // Add rectangle using origin
                        this.gobjects.Add(new Rectangle(CurrentPen, origin, recwidth, recheight, Fill, Outline, CurrentFill));
                    }
                }
                else if (CurrentDraw == "Ellipse")
                {
                    if (Fill || Outline)    // only add object if fill or outline is checked
                    {
                        // Given the two points, find the upper left point to use as origin
                        origin.X = Math.Min(point1.X, point2.X);
                        origin.Y = Math.Min(point1.Y, point2.Y);
                        int ellipsewidth = Math.Abs(point2.X - point1.X);
                        int ellipseheight = Math.Abs(point2.Y - point1.Y);
                        // Add ellipse using origin
                        this.gobjects.Add(new Ellipse(CurrentPen, origin, ellipsewidth, ellipseheight, Fill, Outline, CurrentFill));
                    }
                }
                else if (CurrentDraw == "Text")
                {
                    if (CurrentText != "")  // only add if there is actually text
                    {
                        // Given the two points, find the upper left point to use as origin
                        origin.X = Math.Min(point1.X, point2.X);
                        origin.Y = Math.Min(point1.Y, point2.Y);
                        int textwidth = Math.Abs(point2.X - point1.X);
                        int textheight = Math.Abs(point2.Y - point1.Y);
                        this.gobjects.Add(new Text(CurrentText, CurrentBrush, origin, textwidth, textheight));
                    }
                }
                else
                {
                    // If CurrentDraw is not any of those things, don't do anything
                    this.Invalidate();
                }
               
                this.Invalidate();
            }
            
            else    // its the first click
            {
                point1 = new Point(e.X, e.Y);
                FirstClick = true;
                this.Invalidate();
            }
        }

        private void LineButton_CheckedChanged(object sender, EventArgs e)
        {
            // All of the radio buttons in the "Draw" groupbox use this event handler
            // This controls the 'state,' or what object we're currently equipped to add
            if (LineButton.Checked) CurrentDraw = "Line";
            if (RectangleButton.Checked) CurrentDraw = "Rectangle";
            if (EllipseButton.Checked) CurrentDraw = "Ellipse";
            if (TextButton.Checked) CurrentDraw = "Text";

            this.Invalidate();
        }


        private void PenColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This changes the brush color.
            // The brush color is used in the paint handler along with the width to create the Pen.
            switch(PenColor.SelectedIndex)  // use index of listbox to set corresponding color
            {
                case 0: CurrentBrush = Brushes.Black; break;
                case 1: CurrentBrush = Brushes.Red; break;
                case 2: CurrentBrush = Brushes.Blue; break;
                case 3: CurrentBrush = Brushes.Green; break;
                default: CurrentBrush = Brushes.Black; break;
            }
            this.Invalidate();
        }

        private void FillColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Fill color is just a brush color
            switch (FillColor.SelectedIndex)    // use index of listbox to set corresponding color
            {
                case 0: CurrentFill = Brushes.White; break;
                case 1: CurrentFill = Brushes.Black; break;
                case 2: CurrentFill = Brushes.Red; break;
                case 3: CurrentFill = Brushes.Blue; break;
                case 4: CurrentFill = Brushes.Green; break;
                default: CurrentFill = Brushes.Black; break;
            }
            this.Invalidate();
        }

        private void PenWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set corresponding PenWidth.  Add 1 b/c index starts at 0
            CurrentPenWidth = PenWidth.SelectedIndex + 1;
            this.Invalidate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CurrentText = textBox1.Text;
            this.Invalidate();
        }

        private void FillCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Fill = !Fill;
            this.Invalidate();
        }

        private void OutlineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Outline = !Outline;
            this.Invalidate();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gobjects.Clear();
            this.Invalidate();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Remove last element added to the graphics object list
            if (gobjects.Count != 0)    // making sure list is not empty
            {
                this.gobjects.RemoveAt(gobjects.Count - 1);
            }
            this.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
