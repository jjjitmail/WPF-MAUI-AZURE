using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTouchExpress.Attributes
{
    public class IconAttribute : Attribute
    {
        public string Name { get; private set; }

        public IconAttribute(string name)
        {
            this.Name = name;
        }
    }
    public class TextAttribute : Attribute
    {
        public string Icon { get; set; }
        public Point[] Points { get; set; }
        public string Name { get; set; }

        public TextAttribute(string name)
        {
            this.Name = name;
        }
        public TextAttribute(string name, string icon)
        {
            this.Name = name;
            this.Icon = icon;
        }
        public TextAttribute(string name, Point[] points)
        {
            this.Name = name;
            this.Points = points;
        }
        //Point[]
    }

    public class RangeAttribute : Attribute
    {
        public string Name { get; set; }
        public Color BackgroundColor { get; set; }

        public RangeAttribute(string name, string backGroundColor)
        {
            this.Name = name;
            this.BackgroundColor = Color.FromArgb(backGroundColor);
        }
    }
}
