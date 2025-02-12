using NNN.Core.Presentation.MAUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Triggers
{
    public class EntryTriggerAction : TriggerAction<VisualElement>
    {
        public Label TextLabel { get; set; }
        public VisualElement PSUnderline { get; set; }

        protected override void Invoke(VisualElement sender)
        {
            PSUnderline.IsVisible = true;
            TextLabel.TextColor = Color.FromArgb(PSColor.DefaultBlueColor);
            PSUnderline.TranslateTo(0, 5, 300, Easing.CubicInOut);            
        }
    }
}
