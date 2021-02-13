using Force.StockTax.Sinacor.Constants;
using Force.StockTax.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using NotePosition = Force.StockTax.Sinacor.Constants.SinacorNotePositions;

namespace Force.StockTax.Sinacor.SinacorNote
{
    /// <summary>
    /// Sinacor Note Negotiations Extractor
    /// </summary>
    internal class SinNoteNegotExtractor : NoteNegotExtractorBase
    {
        internal static NegotiationDto GetNegotiations()
        {
            string[,] negotiationNoteInfo = new string[3, 4];
            return null;
            
        }

        private static List<string> GetBuySellOperations() { 
            var operationsRectangle = RectanglePositions
                .GetRectangle(NotePosition.NegotiationsPosition.BuySellRoll);

            return null;
        }
    }
}
