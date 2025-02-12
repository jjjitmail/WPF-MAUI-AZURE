using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.Maui.Essentials;

namespace PSTouchExpress.Helpers
{
    public static class PSApplication
    {
        public static double AppDensity = DeviceDisplay.MainDisplayInfo.Density;
        public static double AppWidth = DeviceDisplay.MainDisplayInfo.Width / AppDensity;
        public static double AppHeight = DeviceDisplay.MainDisplayInfo.Height / AppDensity;
    }
}
