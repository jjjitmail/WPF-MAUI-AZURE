using SMMDD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMDD.Models
{
    public class Security
    {
        public SecurityType? Type { get; set; }
        public string ID { get; set; }
    }

    public enum SecurityType
    {
        FXSpot = 0,
        FXForward = 1
    }
}
