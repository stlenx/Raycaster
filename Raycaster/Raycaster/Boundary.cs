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
        public Bitmap g;

        public Boundary(float x1, float y1, float x2, float y2, Bitmap g)
        {
            this.g = g;
            a = new Vector2(x1, y1);
            b = new Vector2(x2, y2);
        }

        public void Draw(Bitmap B)
        {
            var gr = Graphics.FromImage(B);
            gr.DrawLine(Pens.Black, a.X,a.Y,b.X,b.Y);
            g = B;
        }
    }
}