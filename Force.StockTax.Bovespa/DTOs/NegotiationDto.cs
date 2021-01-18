using Force.StockTax.Bovespa.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Force.StockTax.Bovespa.DTOs
{
    public class NegotiationDto
    {
        public string SinacorNoteId { get; set; }
        public string Stock { get; set; }
        public int Amount { get; set; }
        public NegotiationType NegotiationType { get; set; }
        public decimal UnitaryPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
