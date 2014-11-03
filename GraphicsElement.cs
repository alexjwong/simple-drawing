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
        // Width and height of containing rectangle
        public int width, height;

        // Current pen and brush being used to draw graphics elements
        public Pen DrawPen;
        public Brush DrawBrush;

        public GraphicsElement()
        {

        }

        public virtual void Draw(Graphics g)
        {
            // Graphics object for the graphics element to be drawn on
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
        }
    }

    class Rectangle : GraphicsElement
    {
        // bools to determine if drawing fill, outline, or both
        private bool Fill, Outline;

        public Rectangle(Pen pen_in, Point loc_in, int width_in, int height_in, bool fill_in, bool outline_in, Brush fillcolor_in)
        {
            this.location = loc_in;
            this.width = width_in;
            this.height = height_in;
            this.DrawPen = pen_in;
            this.Fill = fill_in;
            this.Outline = outline_in;
            this.DrawBrush = fillcolor_in;
        }

        public override void Draw(Graphics g)
        {
            // Fill before outline
            if (Fill)
            {
                g.FillRectangle(DrawBrush, location.X, location.Y, width, height);
            }
            if (Outline)
            {
                g.DrawRectangle(DrawPen, location.X, location.Y, width, height);
            }
        }
    }

    class Ellipse : GraphicsElement
    {
        // bools to determine if drawing fill, outline, or both
        private bool Fill, Outline;

        public Ellipse(Pen pen_in, Point loc_in, int width_in, int height_in, bool fill_in, bool outline_in, Brush fillcolor_in)
        {
            this.location = loc_in;
            this.width = width_in;
            this.height = height_in;
            this.DrawPen = pen_in;
            this.Fill = fill_in;
            this.Outline = outline_in;
            this.DrawBrush = fillcolor_in;
        }

        public override void Draw(Graphics g)
        {
            // Fill before outline
            if (Fill)
            {
                g.FillEllipse(DrawBrush, location.X, location.Y, width, height);
            }
            if (Outline)
            {
                g.DrawEllipse(DrawPen, location.X, location.Y, width, height);
            }
        }
    }

    class Text : GraphicsElement
    {
        public string text;

        public Text(string s_in, Brush brush_in, Point location_in, int width_in, int height_in)
        {
            this.text = s_in;
            this.DrawBrush = brush_in;
            this.location = location_in;
            this.width = width_in;
            this.height = height_in;
        }

        public override void Draw(Graphics g)
        {
            // Set font (default)
            Font drawFont = new Font("Arial", 10, FontStyle.Regular);
            // Containing rectangle for the text string
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(location, new Size(width, height));
            // Draw this string with the containing rectangle
            g.DrawString(text, drawFont, DrawBrush, rect);
        }
    }
}
