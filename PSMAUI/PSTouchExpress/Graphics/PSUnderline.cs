using PSTouchExpress.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.Graphics
{
    public class PSUnderline : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Color.FromArgb(PSColor.DefaultBlueColor);
            canvas.FillRectangle(0, 0, 190, 3);
        }
    }
}
