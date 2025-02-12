using NNN.Core.Instruments;
using NNN.Core.Instruments.Capabilities;
using NNN.Core.Presentation.MAUI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI
{
    public enum MethodType
    {
        [Method("", "", "")]
        Unknown = 0,
        [Method(TechniqueId.MultiStepAmperometry, "Multistep Amperometry", "6.9,78.6 14.4,78.6 14.4,43.2 37.1,43.2 37.1,23.7 57.8,23.7 57.8,63.5 86.1,63.5 86.1,78.4 93.3,78.4")]
        MultistepAmperometry = 1,
        [Method(TechniqueId.LinearSweepVoltammetry, "Linear Sweep Voltammetry", "12.29,87.71 87.71,12.29")]
        LinearSweepVoltammetry = 2,
        [Method(TechniqueId.Chronoamperometry, "Chronoamperometry", "9.2,59.5 90.6,59.5")]
        Chronoamperometry = 3
    }
}
