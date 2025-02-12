using PSTouchExpress.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.Converters
{
    public class FlyOutMenuItemTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Color.FromArgb(PSColor.DefaultWhiteColor) : Color.FromArgb(PSColor.DefaultTextColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Color)value == Color.FromArgb(PSColor.DefaultWhiteColor) ? true : false;
        }
    }
}
