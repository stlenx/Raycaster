using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Numerics;

namespace Raycaster
{
    public class Particle
    {
        private Vector2 pos;
        private int fov = 45;
        private double heading = 0;
        public List<float> scene = new List<float>();
        public readonly List<Ray> rays = new List<Ray>();

        public Particle()
        {
            pos = new Vector2(Form1.width / 2, Form1.height / 2);
            for (var i = -fov; i < fov; i += 1)
            {
                rays.Add(new Ray(pos, (Math.PI / 180) * i));
            }
        }

        public void Move(float amt)
        {
            var vel = new Vector2((float)Math.Cos(heading), (float)Math.Sin(heading));
            vel = Vector2.Normalize(vel) * amt;
            pos += vel;
        }

        public void Rotate(double angle)
        {
            heading += angle;
            var index = 0;
            for (var i = -fov; i < fov; i += 1)
            {
                rays[index].dir = new Vector2((float)Math.Cos((Math.PI / 180 * i) + heading), (float)Math.Sin((Math.PI / 180 * i) + heading));
                index++;
            }
        }

        public void Update(float x, float y) //0ms
        {
            pos = new Vector2(x, y);
            foreach (var ray in rays)
            {
                ray.pos = new Vector2(x, y);
            }
        }

        public void Cast(List<Boundary> walls)
        {
            var b = Form1.g;
            scene = new List<float>();
            foreach (var ray in rays)
            {
                var closest = new Vector2(0,0);
                var record = float.MaxValue;
                Console.WriteLine(record);
                foreach (var pt in walls.Select(wall => ray.Cast(wall)))
                {
                    if (pt == null) continue;
                    var d = Vector2.Distance(pos, pt.Value);
                    var a = ray.angle - heading;
                    a = Math.Cos(a);
                    if (a < 0)
                    {
                        a *= -1;
                    }
                    d *= (float) a;
                    Console.WriteLine(Math.Cos(a));
                    if (!(d < record)) continue;
                    record = d;
                    closest = pt.Value;
                }
                scene.Add(record);
                if (closest.X == 0 && closest.Y == 0) continue;
                var gr = Graphics.FromImage(b);
                gr.DrawLine(Pens.Black, pos.X, pos.Y, closest.X, closest.Y);
            }
        }

        public void Draw() //0ms
        {
            var b = new Bitmap(Form1.width, Form1.height);
            var gr = Graphics.FromImage(b);
            foreach (var pt in rays.Select(ray => ray.Draw()))
            {
                gr.DrawLine(Pens.Black, pos.X, pos.Y, pt.X,pt.Y);
            }
        }
    }
}