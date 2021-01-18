using Force.StockTax.Bovespa.DTOs;
using Force.StockTax.Bovespa.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Force.StockTax.Bovespa.Interfaces
{
    public interface ISinacorNoteReader
    {
         SinacorNoteDto GetSinacorNote();


    }
}
