using Force.StockTax.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Force.StockTax.Model.Models
{
    public class Negotiation
    {
        public int Id { get; set; }
        public SinacorNote SinacorNote { get; set; }
        public Stock Stock { get; set; }
        public int  Amount { get; set; }
        public NegotiationType NegotiationType { get; set; }
        public decimal UnitaryPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
