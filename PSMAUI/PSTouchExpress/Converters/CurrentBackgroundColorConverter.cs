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
    public class CurrentBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return PSColor.DefaultBlueColor;
            if (value is CurrentStatusType)
            {
                switch ((CurrentStatusType)value)
                {
                    case CurrentStatusType.OK:
                        return PSColor.DefaultBlueColor;
                    case CurrentStatusType.Underload:
                        return PSColor.Yellow;
                    case CurrentStatusType.OverloadWarning:
                        return PSColor.DarkOrange;
                    case CurrentStatusType.Overload:
                        return PSColor.Red;
                    default:
                        return PSColor.DefaultBlueColor;
                }
            }
            return PSColor.DefaultBlueColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
