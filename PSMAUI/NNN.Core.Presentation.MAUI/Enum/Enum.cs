using NNN.Core.Presentation.MAUI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI
{
    public enum ConnectionType
    {
        [Icon("\uF142")]
        Unknown = 0,
        [Icon("\uECF0")]
        USB = 1,
        [Icon("\uE702")]
        BLC = 2,
        [Icon("\uE702 LE")]
        BLE = 3
    }

    public enum DeviceType
    {
        [Text("Unknown")]
        Unknown = 0,
        [Text("EmStat3")]
        EmStat3 = 1
    }
}
