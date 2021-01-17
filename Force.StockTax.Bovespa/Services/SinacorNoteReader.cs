using Force.StockTax.Bovespa.Enums;
using Force.StockTax.Bovespa.Interfaces;
using Force.StockTax.Bovespa.Models;
using Force.StockTax.Bovespa.Utils;
using Force.StockTax.Bovespa.Utils.Extensions;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NotePosition = Force.StockTax.Bovespa.Enums.SinacorNotePositions;

namespace Force.StockTax.Bovespa.Services
{
    public class SinacorNoteReader : ISinacorNoteReader
    {
        private RectanglePositions BuyAndSellRec = NotePosition.BuyAndSellRectangle;
        private Stream PdfStream { get; set; }

        public SinacorNoteReader(Stream pdfStream)
        {
            PdfStream = pdfStream;
        }

        private List<Negotiation> GetNegotiations(SinacorNote sn)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(PdfStream));
            var addressRect = new Rectangle(BuyAndSellRec.AxisX, BuyAndSellRec.AxisY, BuyAndSellRec.Width, BuyAndSellRec.Height);

            try
            {
                var negotiations = new List<string>();
                int n = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    PdfPage page = pdfDoc.GetPage(i);

                    negotiations.AddRange(ReaderExtensions.ExtractText(page, addressRect).ToList());
                }

                //Parsing Sinacor Negotiations
               var ret = SinacorNoteParser.ParseNegotiations(negotiations, sn);

                return ret;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (!pdfDoc.IsClosed())
                {
                    pdfDoc.Close();
                }
            }
        }
        public SinacorNote GetSinacorNote()
        {
            SinacorNote sinacorNote = new SinacorNote();

            sinacorNote.Negotiations = GetNegotiations(sinacorNote);
            return sinacorNote;
        }
    }
}
