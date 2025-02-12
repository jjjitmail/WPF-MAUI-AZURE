using NNN.Core.Presentation.MAUI.Attributes;
using NNN.Core.Presentation.MAUI;
using NNN.Core.Presentation.MAUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNN.Core.Instruments.Capabilities;
using NNN.Core.Instruments;
using NNN.Core.Instruments.Methods;
using NNN.Core.Common.Values;
using NNN.Core.Common.Parameters;
using Microsoft.Extensions.Localization;

namespace NNN.Core.Presentation.MAUI.Models
{
    public class Method : NotifyPropertyChangedBase
    {
        public int MethodId { get; set; }
        public PointCollection MethodIcon => PSPoints.GetPoints(MethodType.GetAttribute<MethodAttribute>().Icon, 2.8);

        public string MethodName => MethodType.GetAttribute<MethodAttribute>().Name;

        public MethodType MethodType { get; set; }

        

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged(); }
        }
        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get
            {
                if (_backgroundColor == null)
                    return Color.FromArgb(PSColor.DefaultWhiteColor);
                return _backgroundColor;
            }
            set { _backgroundColor = value; OnPropertyChanged(); }
        }

        private Color _textColor;
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; OnPropertyChanged(); }
        }

        //public Color TextColor => IsSelected ? Color.FromRgba(PSColor.DefaultWhiteColor) : Color.FromRgba(PSColor.DefaultTextColor);
        public Color IconColor => IsSelected ? Color.FromRgba(PSColor.DefaultWhiteColor) : Color.FromRgba(PSColor.DefaultBlueColor);
        //public Color BackgroundColor => IsSelected ? Color.FromRgba(PSColor.DefaultBlueColor) : Color.FromRgba(PSColor.DefaultWhiteColor);
    }
}
