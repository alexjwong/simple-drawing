using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;

namespace simple_drawing
{
    class GraphicsElement
    {
        // Origin of each graphics object
        public Point location = new Point();

        public Pen DrawPen;
        public Brush DrawBrush;
        public Color FillColor;

        public GraphicsElement()
        {

        }

        public virtual void Draw(Graphics g)
        {

        }
    }

    class Line : GraphicsElement
    {
        Point pt1, pt2;
        
        public Line(Pen pen_in, Point pt1_in, Point pt2_in)
        {
            this.pt1 = pt1_in;
            this.pt2 = pt2_in;
            this.DrawPen = pen_in;
        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(DrawPen, pt1, pt2);
            Console.WriteLine("Line draw called");
        }
    }

    class Rectangle : GraphicsElement
    {
        private int width, height;

        public Rectangle(Pen pen_in, Point loc_in, int width_in, int height_in)
        {
            this.location = loc_in;
            this.width = width_in;
            this.height = height_in;
            this.DrawPen = pen_in;
        }

        public override void Draw(Graphics g)
        {
            // Fill before outline
            // Default will fill the shape with white, making it look transparnet
            // g.FillRectangle(DrawBrush, location.X, location.Y, width, height);
            g.DrawRectangle(DrawPen, location.X, location.Y, width, height);
        }
    }

    class Ellipse : GraphicsElement
    {
        private int width, height;

        public Ellipse(Pen pen_in, Point loc_in, int width_in, int height_in)
        {
            this.location = loc_in;
            this.width = width_in;
            this.height = height_in;
            this.DrawPen = pen_in;
        }

        public override void Draw(Graphics g)
        {
            // g.FillEllipse(DrawBrush, location.X, location.Y, width, height);
            g.DrawEllipse(DrawPen, location.X, location.Y, width, height);
        }
    }

    class Text : GraphicsElement
    {
        public string text;

        public Text(string s_in, Brush brush_in, Point location_in)
        {
            this.text = s_in;
            this.DrawBrush = brush_in;
            this.location = location_in;
        }

        public override void Draw(Graphics g)
        {
            // Set font (default)
            Font drawFont = new Font("Arial", 12, FontStyle.Regular);

            g.DrawString(text, drawFont, DrawBrush, location);
        }
    }
}
