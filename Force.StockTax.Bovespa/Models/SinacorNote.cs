using System;
using System.Collections.Generic;
using System.Text;

namespace Force.StockTax.Bovespa.Models
{
    public class SinacorNote
    {
        public int Id { get; set; }
        public Broker Broker { get; set; }
        public List<Negotiation> Negotiations { get; set; }
    }
}
