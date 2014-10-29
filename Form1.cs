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
        private string CurrentDraw = "";

        // Points to determine object location
        private Point point1, point2;

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
        }

        private void DrawPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point1 = new Point(e.X, e.Y);

                this.Invalidate();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Remove last element added to the graphics object list
            gobjects.RemoveAt(gobjects.Count);
        }

    }
}
