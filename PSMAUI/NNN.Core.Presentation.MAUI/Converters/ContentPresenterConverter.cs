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
    // not in used
    public class ContentPresenterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null && parameter is VisualElement)
            {
                VisualElement control = (VisualElement)parameter;
                //PSTasks.ActionTask(() => control.AnchorX = 255);
            }
            if (value != null && parameter is VisualElement)
            {
               VisualElement control = (VisualElement)parameter;
                try
                {
                    //control.TranslateTo(255, 0, 10, Easing.CubicInOut);
                    PSTasks.ActionTask(() =>
                    {                        
                        control.TranslateTo(0, 0, 350, Easing.CubicInOut);
                    });
                }
                catch (Exception err)
                {
                    //throw;
                }
               
                //control.TranslateTo(0, -55, 100, Easing.CubicInOut);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //throw new NotImplementedException();
        }
    }
}
