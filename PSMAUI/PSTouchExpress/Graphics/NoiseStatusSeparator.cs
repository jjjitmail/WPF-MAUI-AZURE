using PSTouchExpress.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.Graphics
{
    public class NoiseStatusSeparator : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Color.FromArgb(PSColor.DefaultBlueColor);
            canvas.FillRectangle(12, 0, 2, 20);
            canvas.FillRectangle(26, 0, 2, 20);
            canvas.FillRectangle(40, 0, 2, 20);
            canvas.FillRectangle(54, 0, 2, 20);
            canvas.FillRectangle(68, 0, 2, 20);
            canvas.FillRectangle(82, 0, 2, 20);
            canvas.FillRectangle(96, 0, 2, 20);
            canvas.FillRectangle(110, 0, 2, 20);
            canvas.FillRectangle(124, 0, 2, 20);
        }
    }
}
