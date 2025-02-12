using NNN.Core.Presentation.MAUI.Attributes;
using NNN.Core.Presentation.MAUI;
using NNN.Core.Presentation.MAUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Models
{
    public class MethodGroup : List<Method>
    {
        public MethodGroup(MethodGroupType techniqueType, List<Method> metryList) : base(metryList)
        {
            MethodType = techniqueType;
            //HeaderName = headerName;
            //HeaderName = MethodType.GetAttribute<MethodAttribute>().Name;
        }
        //public string HeaderName { get; set; }
        public string HeaderName => MethodType.GetAttribute<MethodAttribute>().Name;
        public MethodGroupType MethodType { get; set; }
    }
}
