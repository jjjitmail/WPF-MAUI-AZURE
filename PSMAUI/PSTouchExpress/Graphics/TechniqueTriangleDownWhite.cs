using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.Graphics
{
    public class TechniqueTriangleDownWhite : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            PathF path = new PathF();
            path.MoveTo(0, 0);
            path.LineTo(10, 0);
            path.LineTo(5, 5);
            canvas.FillColor = Colors.White;
            canvas.FillPath(path);
        }
    }
}
