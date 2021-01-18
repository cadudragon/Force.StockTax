using System;
using System.Collections.Generic;
using System.Text;

namespace Force.StockTax.Bovespa.DTOs
{
   public class SinacorNoteDto
    {
        public string Id { get; set; }
        public string Broker { get; set; }
        public List<NegotiationDto> Negotiations { get; set; }
        public string Date { get; set; }
        public int PageNumber { get; set; }
    }
}
