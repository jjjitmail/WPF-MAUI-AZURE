using PSTouchExpress.Helpers;
using PSTouchExpress.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.Converters
{
    public class TabSelectedLineColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Boolean)
            {
                bool isSelected = (bool)value;
                if (isSelected)
                    return PSColor.DefaultBlueColor;
            }

            return PSColor.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
