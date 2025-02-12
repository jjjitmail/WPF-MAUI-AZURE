using NNN.Core.Presentation.MAUI.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Converters
{
    public class FlyOutMenuItemBgColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Color.FromArgb(PSColor.DefaultBlueColor) : Color.FromArgb(PSColor.FlyOutMenuBgColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Color)value == Color.FromArgb(PSColor.DefaultBlueColor) ? true : false;
        }
    }
}
