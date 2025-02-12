using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.Controls
{
    public class PSFrame : Frame
    {
        public static readonly BindableProperty PSHeightProperty = BindableProperty.Create(nameof(PSHeight), 
            typeof(double), typeof(PSFrame), 50.0, BindingMode.TwoWay, propertyChanged: PSHeightChanged);

        public double PSHeight
        {
            get => (double)GetValue(PSHeightProperty);
            set => SetValue(PSHeightProperty, value);
        }
        static void PSHeightChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            PSFrame _PSFrame = bindableObject as PSFrame;
            _PSFrame.HeightRequest = (double)newValue;
        }
    }
}
