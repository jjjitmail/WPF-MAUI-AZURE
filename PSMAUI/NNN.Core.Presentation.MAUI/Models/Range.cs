using NNN.Core.Presentation.MAUI.Attributes;
using NNN.Core.Presentation.MAUI.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NNN.Core.Presentation.MAUI.Models
{
    public class Range
    {
        public string Name => RangeType.GetAttribute<RangeAttribute>().Name;
        public object Value => RangeType.GetAttribute<RangeAttribute>().Value;
        public Color BackgroundColor => RangeType.GetAttribute<RangeAttribute>().BackgroundColor;
        public RangeType RangeType { get; set; }
        public bool IsSelected { get; set; }
        public string SelectionName { get; set; }
    }

    public enum RangeType
    {
        [Range("100 mA", "#2E4373", "100m")]
        mA100 = 1,
        [Range("10 mA", "#374F73", "10m")]
        mA10 = 2,
        [Range("1 mA", "#3D5A85", "1m")]
        mA1 = 3,
        [Range("100 μA", "#2F5D93", "100μ")]
        µA100 = 4,
        [Range("10 μA", "#2E6BA4", "10μ")]
        µA10 = 5,
        [Range("1 μA", "#3276B6", "1μ")]
        µA1 = 6,
        [Range("100 nA", "#2181B8", "100n")]
        nA100 = 7,
        [Range("10 nA", "#0090C9", "10n")]
        nA10 = 8,
        [Range("1 nA", "#59A9CC", "1n")]
        nA1 = 9,
        [Range("100 pA", "#8DC6DB", "100p")]
        pA100 = 10,
    }
}
