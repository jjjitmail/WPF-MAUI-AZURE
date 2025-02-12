using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Models
{
    public class PlotItem
    {
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public double Pb { get; set; }
        public string CloseIconBtn { get; set; } = "\uE10A";
    }
}
