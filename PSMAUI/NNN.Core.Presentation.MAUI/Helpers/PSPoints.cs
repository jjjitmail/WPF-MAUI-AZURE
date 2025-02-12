using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Helpers
{
    public static class PSPoints
    {
        public static PointCollection GetPoints(string StringPoints, double size = 1)
        {
            var _points = new PointCollection();
            string[] _stringPoints = StringPoints.Split(' ');
            foreach (var point in _stringPoints)
            {
                string[] values = point.Split(',');
                _points.Add(new(Convert.ToDouble(values[0]) / size, Convert.ToDouble(values[1]) / size));
            }

            return _points;
        }
    }
}
