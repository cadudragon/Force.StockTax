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

        private string GetBrokerName() {
            RectanglePositions brokerNameRec = NotePosition.BrokerNameRectangle;
            var addressRect = new Rectangle(brokerNameRec.AxisX, brokerNameRec.AxisY, brokerNameRec.Width, brokerNameRec.Height);

            try
            {
                var negotiations = new List<string>();
                int n = PdfDocument.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    PdfPage page = PdfDocument.GetPage(i);
                   return ReaderExtensions.ExtractText(page, addressRect).FirstOrDefault();
                }


                throw new Exception("BrokerName not found");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SetNoteHeader(SinacorNoteDto sn) {
            RectanglePositions noteIdRectangle = NotePosition.NoteIdRectangle;
            var addressRect = new Rectangle(noteIdRectangle.AxisX, noteIdRectangle.AxisY, noteIdRectangle.Width, noteIdRectangle.Height);

            try
            {
                var negotiations = new List<string>();
                int n = PdfDocument.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    PdfPage page = PdfDocument.GetPage(i);
                    var header = ReaderExtensions.ExtractText(page, addressRect).FirstOrDefault();
                    var headerItens = header.Split(' ');
                    sn.Id = headerItens[0];
                    sn.PageNumber = int.Parse(headerItens[1]);
                    sn.Date = headerItens[2];
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
            RectanglePositions NegotiationsRec = NotePosition.NegotiationsRectangle;
            var addressRect = new Rectangle(NegotiationsRec.AxisX, NegotiationsRec.AxisY, NegotiationsRec.Width, NegotiationsRec.Height);

            try
            {
                var negotiations = new List<string>();
                int n = PdfDocument.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    PdfPage page = PdfDocument.GetPage(i);
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
        }

        public SinacorNoteDto GetSinacorNote()
        {
            try
            {
                SinacorNoteDto sinacorNote = new SinacorNoteDto ();
                sinacorNote.Broker = GetBrokerName();
                SetNoteHeader(sinacorNote);
                sinacorNote.Negotiations = GetNegotiations(sinacorNote);
                return sinacorNote;
            }
            catch (Exception)
            {
                throw;
            }
            finally {
                ClosePdf();
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
