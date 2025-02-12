using SMMDD.Interfaces;
using SMMDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMMDD.ViewModels
{
    public class MarketDepthSource : IMarketDepthSource
    {
        private IMarketDepth[] _bids;
        public IMarketDepth[] Bids => _bids;

        private IMarketDepth[] _asks;
        public IMarketDepth[] Asks => _asks;

        public event EventHandler OnDepthUpdated;

        public void Subscribe(string symbol)
        {
            OnDepthUpdated?.Invoke(this, EventArgs.Empty);
        }

        public List<MarketDepthGrid> MarketDepthGrids => new List<MarketDepthGrid>() 
        {
             new MarketDepthGrid()
             {
                 Price_Bid = "1.28",
                 Qty_Bid = 1000,
                 Source_Bid = "JPM",
                 Price_Ask = "1.281",
                 Qty_Ask = 5000,
                 Source_Ask = "CITI"
             },
             new MarketDepthGrid()
             {
                 Price_Bid = "1.2790",
                 Qty_Bid = 2000,
                 Source_Bid = "MS",
                 Price_Ask = "1.2785",
                 Qty_Ask = 1000,
                 Source_Ask = "JPM"
             },
             new MarketDepthGrid()
             {
                 Price_Bid = "1.2789",
                 Qty_Bid = 3000,
                 Source_Bid = "BARC",
                 Price_Ask = "1.289",
                 Qty_Ask = 2000,
                 Source_Ask = "RBS"
             }
        };
    }
}
