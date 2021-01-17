using Force.StockTax.Bovespa.Interfaces;
using Force.StockTax.Bovespa.Utils;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Force.StockTax.Bovespa.Services
{
    public class SinacorNoteReader : IPdfReader
    {
        //
        public List<string> GetTransactions(Stream source)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(@"C:\Users\Desenv01\Downloads\NotaNegociacao_507256_20201229.pdf"));

            var x = 1;
            var y = 380;
            var w = 800;
            var h = 235;
            var addressRect = new Rectangle(x, y, w, h);
            try
            {
                var ret = new List<string>();
                int n = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    PdfPage page = pdfDoc.GetPage(i);

                    ret = ReaderExtensions.ExtractText(page, addressRect).ToList();
                }
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
    }
}
