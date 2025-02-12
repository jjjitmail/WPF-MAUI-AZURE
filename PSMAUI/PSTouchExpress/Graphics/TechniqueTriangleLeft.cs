using PSTouchExpress.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.Graphics
{
    public class TechniqueTriangleLeft : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            PathF path = new PathF();
            path.MoveTo(12, 4);
            path.LineTo(12, 20);
            path.LineTo(4, 12);
            canvas.FillColor = Colors.Gray;
            canvas.FillPath(path);
        }
    }
}
