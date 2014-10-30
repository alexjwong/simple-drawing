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
        private Pen CurrentPen;
        private Brush CurrentBrush;


        // Points to determine object location
        private Point point1, point2;

        // boolean to determine whether the first click has been entered 
        // (in drawing the rectangle that contains the object
        private bool FirstClick = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void LineButton_CheckedChanged(object sender, EventArgs e)
        {
            // All of the radio buttons in the "Draw" groupbox use this event handler
            if (LineButton.Checked) CurrentDraw = "Line";
            if (RectangleButton.Checked) CurrentDraw = "Rectangle";
            if (EllipseButton.Checked) CurrentDraw = "Ellipse";
            if (TextButton.Checked) CurrentDraw = "Text";

            this.Invalidate();
        }

        // All paint and clicks for the graphics objects happen in the lower panel
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            // Graphics object g
            Graphics g = e.Graphics;

            // Draw each graphics element
            foreach (GraphicsElement gobject in gobjects)
            {
                gobject.Draw(g);
            }

            Console.WriteLine(gobjects.Count);
        }

        private void DrawPanel_MouseClick(object sender, MouseEventArgs e)
        {
            // Both left and right clicks do the same thing
            if (FirstClick)
            {
                Point point2 = new Point(e.X, e.Y);
                FirstClick = false;

                // Add the object after a second point has been determined
                if (CurrentDraw == "Line")
                {
                    this.gobjects.Add(new Line(Pens.Black, point1, point2));
                }
                else if (CurrentDraw == "Rectangle")
                {
                    int recwidth = Math.Abs(point2.X - point1.X);
                    int recheight = Math.Abs(point2.Y - point1.X);
                    // logic for determining bounding rectange from any two opposite corner clicks
                    this.gobjects.Add(new Rectangle(CurrentPen, point1, recwidth, recheight));
                }
                else if (CurrentDraw == "Ellipse")
                {
                    int ellipsewidth = Math.Abs(point2.X - point1.X);
                    int ellipseheight = Math.Abs(point2.Y - point1.X);
                    this.gobjects.Add(new Ellipse(CurrentPen, point1, ellipsewidth, ellipseheight));
                }
                else if (CurrentDraw == "Text")
                {
                    this.gobjects.Add(new Text("test", CurrentBrush, point1));
                }
                else
                {
                    // If CurrentDraw is not any of those things, don't do anything
                    this.Invalidate();
                }
               
                this.Invalidate();
            }
            else
            {
                Point point1 = new Point(e.X, e.Y);
                FirstClick = true;
                this.Invalidate();
            }
            
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Remove last element added to the graphics object list
            this.gobjects.RemoveAt(gobjects.Count-1);
        }

        private void PenColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Key:
            // 0 = Black
            // 1 = Red
            // 2 = Blue
            // 3 = Green
            switch(PenColor.SelectedIndex)
            {
                case 0:
                    {
                        CurrentPen = Pens.Black;
                        CurrentBrush = Brushes.Black;
                        break;
                    }
                case 1:
                    {
                        CurrentPen = Pens.Red;
                        CurrentBrush = Brushes.Red;
                        break;
                    }
                case 2:
                    {
                        CurrentPen = Pens.Blue;
                        CurrentBrush = Brushes.Blue;
                        break;
                    }
                    
                case 3:
                    {
                        CurrentPen = Pens.Green;
                        CurrentBrush = Brushes.Green;
                        break;
                    }
                    
                default:
                    {
                        CurrentPen = Pens.Black;
                        CurrentBrush = Brushes.Black;
                        break;
                    }
            }
        }

        private void FillColor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PenWidth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FillCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void OutlineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        
        }
    }
}
