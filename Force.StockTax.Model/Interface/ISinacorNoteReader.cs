using Force.StockTax.Model.DTO;
using Force.StockTax.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Force.StockTax.Model.Interfaces
{
    public interface ISinacorNoteReader
    {
         SinacorNoteDto GetSinacorNote();


    }
}
