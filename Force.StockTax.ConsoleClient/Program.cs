using Force.StockTax.Bovespa.Services;
using iText.Kernel.Pdf;
using System;
using System.IO;

namespace Force.StockTax.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string sinacorNotePath = @"C:\Users\Desenv01\Downloads\NotaNegociacao_507256_20190410.pdf";
            MemoryStream sinacorNoteStream = new MemoryStream(File.ReadAllBytes(sinacorNotePath));

            SinacorNoteReader snr = new SinacorNoteReader(sinacorNoteStream);
            var ret = snr.GetSinacorNote();
        }
    }
}
