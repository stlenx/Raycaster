using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Raycaster
{
    public class Ray
    {
        private Vector2 pos;
        private Vector2 dir;
        public Bitmap g;
        
        public Ray(float x, float y, Bitmap g)
        {
            this.g = g;
            pos = new Vector2(x,y);
            dir = new Vector2(1, 0);
        }

        public void LookAt(float x, float y)
        {
            dir.X = x - pos.X;
            dir.Y = y - pos.Y;
            dir = Vector2.Normalize(dir);
        }

        public void Draw(Bitmap B)
        {
            var gr = Graphics.FromImage(B);
            gr.DrawLine(Pens.Black, pos.X, pos.Y, pos.X + (dir.X * 10), pos.Y + (dir.Y * 10)); // HOLY SHIT PLEASE CHANGE THIS IN THE FUTURE
            g = B;
        }

        public object Cast(Boundary wall)
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
                return true;
            }
            else
            {
                return null;
            }
        }
    }
}