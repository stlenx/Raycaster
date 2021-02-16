using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Raycaster
{
    public class Particle
    {
        private Vector2 pos;
        public readonly List<Ray> rays = new List<Ray>();

        public Particle()
        {
            pos = new Vector2(Form1.width / 2, Form1.height / 2);
            for (var i = 0; i < 360; i += 10)
            {
                rays.Add(new Ray(pos, (Math.PI / 180) * i));
            }
        }

        public void Update(float x, float y)
        {
            pos = new Vector2(x, y);
            foreach (var ray in rays)
            {
                ray.pos = new Vector2(x, y);
            }
        }

        public void Cast(List<Boundary> walls, Bitmap b)
        {
            foreach (var ray in rays)
            {
                var closest = new Vector2(0,0);
                var record = float.MaxValue;
                foreach (var wall in walls)
                {
                    var pt = ray.Cast(wall);
                    if (pt == null) continue;
                    var d = Vector2.Distance(pos, pt.Value);

                    if (!(d < record)) continue;
                    record = d;
                    closest = pt.Value;
                }
                if (closest.X == 0 || closest.Y == 0) continue;
                var gr = Graphics.FromImage(b);
                gr.DrawLine(Pens.Black, pos.X, pos.Y, closest.X, closest.Y);
                ray.g = b;
            }
        }

        public void Draw(Bitmap b)
        {
            foreach (var ray in rays)
            {
                ray.Draw(b);
            }
        }
    }
}