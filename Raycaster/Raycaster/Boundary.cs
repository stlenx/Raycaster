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

        public Boundary(float x1, float y1, float x2, float y2)
        {
            a = new Vector2(x1, y1);
            b = new Vector2(x2, y2);
        }

        public Vector4 Draw()
        {
            return new Vector4( a.X, a.Y,b.X,b.Y);
            //gr.DrawLine(Pens.Black, a.X,a.Y,b.X,b.Y);
            
        }
    }
}