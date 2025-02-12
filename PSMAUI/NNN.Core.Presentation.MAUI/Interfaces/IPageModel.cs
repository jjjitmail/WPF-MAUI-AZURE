using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Interfaces
{
    public interface IPageModel 
    {
        IView FlyOutMenuViewPage { get; set; }
        Task BtnPressAction(Func<Task> func);
    }
}
