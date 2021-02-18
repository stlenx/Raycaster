﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public static readonly int width = 806;
        public static readonly int height = 479;
        private Graphics gr;
        public static Bitmap g;
        private static float distProjPlane = 0;
        public Form1()
        {
            InitializeComponent();
            g = new Bitmap(width, height);
            particle = new Particle();
            var rnd = new Random();
            for (var i = 0; i < 5; i++)
            {
                var x1 = rnd.Next(0, 600);
                var x2 = rnd.Next(0, 600);
                var y1 = rnd.Next(0, 600);
                var y2 = rnd.Next(0, 600);
                walls.Add(new Boundary(x1,y1,x2,y2));
            }
            distProjPlane = (float) (width / 2.0 / Math.Tan(Particle.fov / 2.0));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = new Bitmap(width, height);
            gr = Graphics.FromImage(g);
            //~1ms

            var scene = particle.Cast(walls);
            if (scene.Count != 0)
            {
                var w = width / scene.Count;
                for (var i = 0; i < scene.Count; i++)
                {
                    var b = Map(0, float.MaxValue, 255, 0, scene[i] * scene[i]);
                    var h = (width / scene[i]) * distProjPlane;
                    //gr.FillRectangle(new SolidBrush(Color.FromArgb((int) b,(int) b,(int) b)), 
                    //    new Rectangle(i * w + w / 2, height / 2, w + 1, (int)h));
                }
            }

            foreach (var pt in walls.Select(wall => wall.Draw())) //~10ms
            {
                gr.DrawLine(Pens.Black, pt.X, pt.Y, pt.Z, pt.W);
            }
            e.Graphics.DrawImage(g, 0,0);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //particle.Update(e.X, e.Y);
            //Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeRedraw = true;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'd':
                    particle.Rotate(0.1);
                    break;
                case 'a':
                    particle.Rotate(-0.1);
                    break;
                case 'w':
                    particle.Move(2);
                    break;
                case 's':
                    particle.Move(-2);
                    break;
            }
            Refresh();
        }
        
        static double Map(double a1, double a2, double b1, double b2, double s) => b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}