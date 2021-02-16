using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Cursor;

namespace Raycaster
{
    public partial class Form1 : Form
    {
        private Particle particle;
        private List<Boundary> walls = new List<Boundary>();
        private Bitmap g;
        public static readonly int width = 806;
        public static readonly int height = 479;
        public Form1()
        {
            InitializeComponent();
            particle = new Particle();
            g = new Bitmap(width, height);
            var rnd = new Random();
            for (var i = 0; i < 5; i++)
            {
                var x1 = rnd.Next(0, 600);
                var x2 = rnd.Next(0, 600);
                var y1 = rnd.Next(0, 600);
                var y2 = rnd.Next(0, 600);
                Console.WriteLine(x1 + " " + x2 + " " + y1 + " " + y2);
                walls.Add(new Boundary(x1,y1,x2,y2,g));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = new Bitmap(width, height);
            particle.Draw(g);
            particle.Cast(walls, g);

            foreach (var ray in particle.rays)
            {
                e.Graphics.DrawImage(ray.g,0,0);
            }
            foreach (var wall in walls)
            {
                wall.Draw(g);
                e.Graphics.DrawImage(wall.g,0,0);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            particle.Update(e.X, e.Y);
            this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeRedraw = true;
        }
    }
}