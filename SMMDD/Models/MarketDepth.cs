using SMMDD.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMDD.Models
{
    public class MarketDepth : IMarketDepth
    {
        private decimal _price;
        public decimal Price 
        {
            get => _price;
            set => _price = value;
        }

        private string _source;
        public string Source
        {
            get => _source;
            set => _source = value;
        }
        private decimal _qty;
        public decimal Qty
        {
            get => _qty;
            set => _qty = value;
        }
    }
}
