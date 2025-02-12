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
    public class PotentialBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _color = PSColor.DefaultBlueColor;
            if (value == null) return Color.FromArgb(_color);
            if (value is PotentialStatusType)
            {
                switch ((PotentialStatusType)value)
                {
                    case PotentialStatusType.OK:
                        _color = PSColor.DefaultBlueColor;
                        break;
                    case PotentialStatusType.Underload:
                        return Colors.Yellow;
                    case PotentialStatusType.OverloadWarning:
                        return Colors.Red;
                    default:
                        _color = PSColor.DefaultBlueColor;
                        break;
                }
            }
            return Color.FromArgb(_color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
