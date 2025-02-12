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
    public class CurrentStatusTextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return PSColor.DefaultLightBlueColor;
            if (value is CurrentStatusType)
            {
                switch ((CurrentStatusType)value)
                {
                    case CurrentStatusType.OK:
                    return PSColor.DefaultLightBlueColor;
                    case CurrentStatusType.Underload:
                    return PSColor.DefaultTextColor;
                    case CurrentStatusType.OverloadWarning:
                    return PSColor.DefaultWhiteColor;
                    case CurrentStatusType.Overload:
                    return PSColor.DefaultWhiteColor;
                    default:
                    return PSColor.DefaultLightBlueColor;
                }
            }
            return PSColor.DefaultLightBlueColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
