//using PSTouchExpress;
using NNN.Core.Presentation.MAUI.Attributes;
using NNN.Core.Presentation.MAUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceType = NNN.Core.Presentation.MAUI.DeviceType;
using NNN.Core.Presentation.MAUI;

namespace NNN.Core.Presentation.MAUI.Models
{
    public class ConnectionItem
    {
        public bool Active { get; set; }

        public string Serial { get; set; }

        public DeviceType DeviceType { get; set; }
        public string DeviceTypeString => DeviceType.GetAttribute<TextAttribute>().Name;

        public ConnectionType ConnectionType { get; set; }
        public string ConnectionTypeString => ConnectionType.GetAttribute<IconAttribute>().Name;
    }    
}
