using NNN.Core.Presentation.MAUI.Helpers;
using NNN.Core.Presentation.MAUI.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Converters
{
    public class TabHeaderTextColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is Boolean)
            {
                bool isSelected = (bool)value;
                if (isSelected)
                    return Color.FromArgb(PSColor.DefaultBlueColor);
            }

            return Color.FromArgb(PSColor.DefaultTextColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
