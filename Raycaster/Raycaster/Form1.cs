using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var wall = new Boundary(300,100, 300,300, e);
            ray = new Ray(100,200, e);
            var pt = ray.Cast(wall);
            Console.WriteLine(pt);
            wall.Draw();
            ray.Draw();
            ray.LookAt(Position.X,Position.Y);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine(Position.X);
            Console.WriteLine(Position.Y);
            ray.LookAt(Position.X,Position.Y);
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            int R = e.X % 255;
            int G = e.Y % 255;

            int B = (R + G) / 2;

            BackColor = Color.FromArgb(R, G, B);
        }
    }
}