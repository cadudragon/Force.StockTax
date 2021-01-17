using System;
using System.Collections.Generic;
using System.Text;

namespace Force.StockTax.Bovespa.Models
{
    public class Broker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
    }
}
