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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            while (true)
            {
                var wall = new Boundary(300,100, 300,300, e);
                var ray = new Ray(100,200, e);
                var pt = ray.Cast(wall);
                Console.WriteLine(pt);
                wall.Draw();
                ray.Draw();
                var point = GetMousePositionWindowsForms();
                ray.lookAt(point.X, point.Y);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private static Point GetMousePositionWindowsForms()
        {
            var point = Control.MousePosition;
            return new Point(point.X, point.Y);
        }
    }
}