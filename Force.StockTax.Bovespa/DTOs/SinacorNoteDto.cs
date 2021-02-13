using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Force.StockTax.Bovespa.DTOs
{
    public class SinacorNoteDto
    {
        public string Id { get; set; }
        public string Broker { get; set; }
        public List<NegotiationDto> Negotiations { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalSells { get; set; }
        public decimal TotalBuys { get; set; }

        public bool OperationsAreValid()
        {
            var totalBuy = Negotiations
                 .Where(x => x.NegotiationType == Enums.NegotiationType.BUY)
                 .Select(x => x.TotalPrice).Sum();

            var totalSell = Negotiations
                 .Where(x => x.NegotiationType == Enums.NegotiationType.SELL)
                 .Select(x => x.TotalPrice).Sum();

            var totalBuyMatch = totalBuy == TotalBuys;
            var totalSellMatch = TotalSells == totalSell;

            return totalBuyMatch && totalSellMatch;

        }
    }
}
