using SMMDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMDD.Interfaces
{
    public interface IViewModel
    {
        Task BtnAction(Func<Task> func);

        ActionResultModel ActionResultModel { get; set; }
    }
}
