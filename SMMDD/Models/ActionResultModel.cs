using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMDD.Models
{
    public class ActionResultModel
    {
        public bool Executed { get; set; }
        public bool IsValid { get; set; }
        public string ResultMessage { get; set; }
    }
}
