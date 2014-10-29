using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_drawing
{
    class GraphicsElement
    {
        public GraphicsElement()
        {

        }

        public virtual void draw()
        {

        }
    }

    class Line : GraphicsElement
    {
        public Line()
        {
            
        }
    }

    class Rectangle : GraphicsElement
    {
        public Rectangle()
        {

        }
    }

    class Ellipse : GraphicsElement
    {
        public Ellipse()
        {

        }
    }

    class Text : GraphicsElement
    {
        public Text()
        {

        }
    }
}
