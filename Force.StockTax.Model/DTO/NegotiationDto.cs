using Force.StockTax.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Force.StockTax.Model.DTO
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
