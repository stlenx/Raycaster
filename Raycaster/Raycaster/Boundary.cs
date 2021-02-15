using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using System.Reflection.Emit;
using System.Security.Cryptography.Xml;

namespace Raycaster
{
    public class Boundary
    {
        public readonly Vector2 a;
        public readonly Vector2 b;
        private readonly PaintEventArgs e;

        public Boundary(float x1, float y1, float x2, float y2, PaintEventArgs e)
        {
            this.e = e;
            a = new Vector2(x1, y1);
            b = new Vector2(x2, y2);
        }

        public void Draw()
        {
            var g = e.Graphics;
            g.DrawLine(Pens.Black, a.X,a.Y,b.X,b.Y);
        }
    }
}