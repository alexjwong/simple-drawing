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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Draw each graphics element
            foreach (GraphicsElement g in gobjects)
            {
                g.draw();
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Invalidate();
        }

    }
}
