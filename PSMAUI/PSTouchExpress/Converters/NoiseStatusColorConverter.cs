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
    public class NoiseStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return PSColor.LightGray;
            if (value is NoiseStatusType)
            {
                switch ((NoiseStatusType)value)
                {
                    case NoiseStatusType.Inactive:
                    return PSColor.LightGray;
                    case NoiseStatusType.OK:
                    return PSColor.Green;
                    case NoiseStatusType.Underload:
                    return PSColor.Yellow;
                    case NoiseStatusType.OverloadWarning:
                    return PSColor.Red;
                    default:
                    return PSColor.LightGray;
                }
            }
            return PSColor.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
