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
    public class PotentialStatusTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return PSColor.DefaultWhiteColor;
            if (value is PotentialStatusType)
            {
                switch ((PotentialStatusType)value)
                {
                    case PotentialStatusType.OK:
                    return PSColor.DefaultLightBlueColor;
                    case PotentialStatusType.Underload:
                    return PSColor.DefaultTextColor;
                    case PotentialStatusType.OverloadWarning:
                    return PSColor.DefaultWhiteColor;
                    default:
                    return PSColor.DefaultWhiteColor;
                }
            }
            return PSColor.DefaultWhiteColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
