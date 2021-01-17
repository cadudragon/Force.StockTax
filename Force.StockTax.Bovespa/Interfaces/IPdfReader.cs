using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Force.StockTax.Bovespa.Interfaces
{
    public interface IPdfReader
    {
        List<string> GetTransactions(Stream source);


    }
}
