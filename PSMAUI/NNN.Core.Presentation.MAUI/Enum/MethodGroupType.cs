using NNN.Core.Presentation.MAUI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI
{
    public enum MethodGroupType
    {
        [Method("1", "","")]
        Unkown = 0,
        [Method("2", "Voltammetric Techniques", "")]
        VoltammetricTechniques = 1,
        [Method("3", "Amperometric Techniques", "")]
        AmperometricTechniques = 2
    }
}
