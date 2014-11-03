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
        private bool Fill, Outline;
        private Brush FillColor;

        public Rectangle(Pen pen_in, Point loc_in, int width_in, int height_in, bool fill_in, bool outline_in, Brush fillcolor_in)
        {
            this.location = loc_in;
            this.width = width_in;
            this.height = height_in;
            this.DrawPen = pen_in;
            this.Fill = fill_in;
            this.Outline = outline_in;
            DrawBrush = fillcolor_in;

        }

        public override void Draw(Graphics g)
        {
            // Fill before outline
            // Default will fill the shape with white, making it look transparnet
            // g.FillRectangle(DrawBrush, location.X, location.Y, width, height);
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
        private int width, height;
        private bool Fill, Outline;
        private Brush FillColor;

        public Ellipse(Pen pen_in, Point loc_in, int width_in, int height_in, bool fill_in, bool outline_in, Brush fillcolor_in)
        {
            this.location = loc_in;
            this.width = width_in;
            this.height = height_in;
            this.DrawPen = pen_in;
            this.Fill = fill_in;
            this.Outline = outline_in;
            DrawBrush = fillcolor_in;
        }

        public override void Draw(Graphics g)
        {
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
