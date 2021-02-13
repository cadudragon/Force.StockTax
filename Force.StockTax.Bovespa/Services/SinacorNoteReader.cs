using Force.StockTax.Bovespa.DTOs;
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
        private PdfDocument PdfDocument { get; set; }

        public SinacorNoteReader(Stream pdfStream)
        {
            PdfDocument = new PdfDocument(new PdfReader(pdfStream));
        }

        private string GetBrokerName()
        {
            var addressRect = RectanglePositions.GetRectangle(NotePosition.BrokerNameRectangle);
            PdfPage page = PdfDocument.GetPage(1);
            return ReaderExtensions.ExtractText(page, addressRect).FirstOrDefault();

            throw new Exception("BrokerName not found");
        }

        private void SetNoteHeader(SinacorNoteDto sn)
        {
            var noteHeaderRect = RectanglePositions.GetRectangle(NotePosition.NoteIdRectangle);

            try
            {
                int n = PdfDocument.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    PdfPage page = PdfDocument.GetPage(i);
                    var header = ReaderExtensions.ExtractText(page, noteHeaderRect).FirstOrDefault();
                    var headerItens = header.Split(' ');
                    sn.Id = headerItens[0];
                    //sn.PageNumber = int.Parse(headerItens[1]);
                    sn.Date = DateTime.Parse(headerItens[2]);
                    return;
                }


                throw new Exception("BrokerName not found");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<NegotiationDto> GetNegotiations(SinacorNoteDto sn)
        {
            var negotiationsRec = RectanglePositions.GetRectangle(NotePosition.NegotiationsRectangle);

            try
            {
                var negotiations = new List<string>();
                int n = PdfDocument.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    PdfPage page = PdfDocument.GetPage(i);
                    negotiations.AddRange(ReaderExtensions.ExtractText(page, negotiationsRec).ToList());
                }

                //Parsing Sinacor Negotiations
                var ret = SinacorNoteParser.ParseNegotiations(negotiations, sn);

                return ret;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SinacorNoteDto GetSinacorNote()
        {
            try
            {
                SinacorNoteDto sinacorNote = new SinacorNoteDto();
                sinacorNote.Broker = GetBrokerName();
                SetNoteHeader(sinacorNote);
                sinacorNote.Negotiations = GetNegotiations(sinacorNote);
                sinacorNote.TotalSells = GetTotalSells();
                sinacorNote.TotalBuys = GetTotalBuys();

                if (!sinacorNote.OperationsAreValid()) {
                    throw new Exception("Operations are not valid");
                }

                return sinacorNote;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ClosePdf();
            }
        }

        public decimal GetTotalSells()
        {
            var financialResume = RectanglePositions.GetRectangle(NotePosition.TotalSells);

            try
            {
                var negotiations = new List<string>();
                int n = PdfDocument.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    PdfPage page = PdfDocument.GetPage(i);
                    var text = ReaderExtensions.ExtractText(page, financialResume).FirstOrDefault();
                    var sells = text.Split(' ');

                    return decimal.Parse(sells[3]);
                }

                throw new Exception("Total Sells not found");
            }
            catch (Exception)
            {
                throw new Exception("Error while parsing sells");
            }
        }

        public decimal GetTotalBuys()
        {
            var financialResume = RectanglePositions.GetRectangle(NotePosition.TotalBuys);

            try
            {
                var negotiations = new List<string>();
                int n = PdfDocument.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    PdfPage page = PdfDocument.GetPage(i);
                    var text = ReaderExtensions.ExtractText(page, financialResume).FirstOrDefault();
                    var sells = text.Split(' ');

                    return decimal.Parse(sells[3]);
                }

                throw new Exception("TotalBuys not found");
            }
            catch (Exception)
            {
                throw new Exception("Error while parsing buys");
            }
        }

        private void ClosePdf()
        {
            if (!PdfDocument.IsClosed())
            {
                PdfDocument.Close();
            }
        }
    }
}
