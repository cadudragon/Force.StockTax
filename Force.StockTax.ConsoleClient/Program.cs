using Force.StockTax.Bovespa.Services;
using iText.Kernel.Pdf;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Force.StockTax.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", true);

            string sinacorNotePath = @"C:\Users\Desenv01\Downloads\magalu.pdf";

            MemoryStream sinacorNoteStream = new MemoryStream(File.ReadAllBytes(sinacorNotePath));

            SinacorNoteReader snr = new SinacorNoteReader(sinacorNoteStream);
            var ret = snr.GetSinacorNote();
        }
    }
}
