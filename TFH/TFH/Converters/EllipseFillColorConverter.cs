using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace TFH.Converters
{
    public class EllipseFillColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool online = (bool)value;

            if (online)
                return Microsoft.Maui.Graphics.Color.FromArgb("#3fc380"); // green
            else
                return Microsoft.Maui.Graphics.Color.FromArgb("#ff4c30"); // red
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
