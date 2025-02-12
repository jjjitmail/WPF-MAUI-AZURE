using NNN.Core.Presentation.MAUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Graphics
{
    public class TechniqueTriangleDown : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            PathF path = new PathF();
            path.MoveTo(0, 0);
            path.LineTo(16, 0);
            path.LineTo(8, 8);
            canvas.FillColor = Colors.Gray;
            canvas.FillPath(path);
        }
    }
}
