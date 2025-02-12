using NNN.Core.Presentation.MAUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Graphics
{
    public class NoiseStatusLinearGradientBrush : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            Microsoft.Maui.Controls.GradientStop _Green = new Microsoft.Maui.Controls.GradientStop(Color.FromArgb("LimeGreen"), 0);
            Microsoft.Maui.Controls.GradientStop _Yellow = new Microsoft.Maui.Controls.GradientStop(Color.FromArgb("Yellow"), float.Parse("0.5"));
            Microsoft.Maui.Controls.GradientStop _Red = new Microsoft.Maui.Controls.GradientStop(Color.FromArgb("Red"), 1);

            GradientStopCollection collections = new GradientStopCollection();
            collections.Add(_Green);
            collections.Add(_Yellow);
            collections.Add(_Red);

            LinearGradientBrush linGrBrush = new LinearGradientBrush(collections);            
        }
    }
}
