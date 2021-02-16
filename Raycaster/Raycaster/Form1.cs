using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Cursor;

namespace Raycaster
{
    public partial class Form1 : Form
    {

        private Ray ray;
        private Boundary wall;
        private Bitmap g;
        public Form1()
        {
            InitializeComponent();
            g = new Bitmap(806, 479);
            ray = new Ray(100,200, g);
            wall = new Boundary(300,100, 300,300, g);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = new Bitmap(806, 479);
            ray.Draw(g);
            wall.Draw(g);
            var pt = ray.Cast(wall);
            Console.WriteLine(pt);
            e.Graphics.DrawImage(ray.g,0,0);
            e.Graphics.DrawImage(wall.g,0,0);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            ray.LookAt(e.X, e.Y);
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeRedraw = true;
        }
    }
}