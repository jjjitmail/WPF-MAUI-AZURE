using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMDD.Models
{
    public class MarketDepthGrid
    {
        public string Source_Ask { get; set; }
        public int Qty_Ask { get; set; }
        public string Price_Ask { get; set; }
        
        
        public string Price_Bid { get; set; }        
        public int Qty_Bid { get; set; }
        public string Source_Bid { get; set; }

    }
}
