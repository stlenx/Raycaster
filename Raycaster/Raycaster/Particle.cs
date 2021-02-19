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
        public const int fov = 45;
        private double heading;
        public readonly List<Ray> rays = new List<Ray>();

        public Particle()
        {
            heading = 0;
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
            Update();
        }

        public void Rotate(double angle)
        {
            heading += angle;
            var index = 0;
            for (var i = -fov; i < fov; i += 1)
            {
                rays[index].dir = new Vector2((float)Math.Cos((Math.PI / 180 * i) + heading), (float)Math.Sin((Math.PI / 180 * i) + heading));
                //rays[index].angle = heading;
                index++;
            }
            var gr = Graphics.FromImage(Form1.g);
            gr.DrawLine(Pens.Red, pos.X, pos.Y, (float)Math.Cos((Math.PI / 180 * 0) + heading) * 20, (float)Math.Sin((Math.PI / 180 * 0) + heading) * 20);
        }

        public void Update() => rays.ForEach(ray=>ray.pos=this.pos);

        public List<double> Cast(List<Boundary> walls)
        {
            var scene = new List<double>();
            foreach (var ray in rays)
            {
                Vector2? closest = null;
                var record = 1000d;
                foreach (var pt in walls.Select(wall => ray.Cast(wall)))
                {
                    if (pt == null)
                    { }
                    else
                    {
                        var d = Vector2.Distance(pos, pt.Value);
                    
                        var a = ray.angle - heading;

                        var b = d * Math.Cos(a);
                        if (b < 0) {b *= -1;}
                        if (b < record)
                        {
                            record = b;
                            closest = pt.Value;
                        }
                    }
                }
                scene.Add(record);
                if (closest != null)
                {
                    var gr = Graphics.FromImage(Form1.g);
                    gr.DrawLine(Pens.Black, pos.X, pos.Y, closest.Value.X, closest.Value.Y);
                }
            }
            return scene;
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