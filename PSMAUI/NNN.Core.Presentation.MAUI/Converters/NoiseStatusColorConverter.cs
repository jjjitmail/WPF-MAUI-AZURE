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
    public class NoiseStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _color = PSColor.LightGray;
            if (value == null) return Color.FromArgb(_color);
            if (value is NoiseStatusType)
            {
                switch ((NoiseStatusType)value)
                {
                    case NoiseStatusType.Inactive:
                        _color = PSColor.LightGray;
                        break;
                    case NoiseStatusType.OK:
                        return Colors.Green;
                    case NoiseStatusType.Underload:
                        return Colors.Yellow;
                    case NoiseStatusType.OverloadWarning:
                        return Colors.Red;
                    default:
                        _color = PSColor.LightGray;
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
