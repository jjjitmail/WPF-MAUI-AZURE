using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMDD.Interfaces
{
    public interface IMarketDepthSource
    {
        void Subscribe(string symbol);
        event EventHandler OnDepthUpdated;
        IMarketDepth[] Bids { get; }
        IMarketDepth[] Asks { get; }
    }

    public interface IMarketDepth
    {
        decimal Price { get; set; }
        string Source { get; set; }
        decimal Qty { get; set; }
    }
}
