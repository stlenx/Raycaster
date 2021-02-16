using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Raycaster
{
    public class Ray
    {
        public Vector2 pos;
        private Vector2 dir;
        public Bitmap g;
        
        public Ray(Vector2 pos, double angle)
        {
            this.pos = pos;
            dir = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));;
        }

        public void LookAt(float x, float y)
        {
            dir.X = x - pos.X;
            dir.Y = y - pos.Y;
            dir = Vector2.Normalize(dir);
        }

        public void Draw(Bitmap b)
        {
            var gr = Graphics.FromImage(b);
            gr.DrawLine(Pens.Black, pos.X, pos.Y, pos.X + (dir.X * 10), pos.Y + (dir.Y * 10)); 
            g = b;
        }

        public Vector2? Cast(Boundary wall)
        {
            var x1 = wall.a.X;
            var y1 = wall.a.Y;
            var x2 = wall.b.X;
            var y2 = wall.b.Y;
            
            var x3 = pos.X;
            var y3 = pos.Y;
            var x4 = pos.X + dir.X;
            var y4 = pos.Y + dir.Y;

            var den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (den == 0) {
                return null;
            }

            var t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            var u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;
            if (t > 0 && t < 1 && u > 0)
            {
                return new Vector2 {X = x1 + t * (x2 - x1), Y = y1 + t * (y2 - y1)};
            }
            return null;
        }
    }
}