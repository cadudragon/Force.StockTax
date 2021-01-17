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
            string sinacorNote = @"C:\Users\Desenv01\Downloads\NotaNegociacao_507256_20190410.pdf";

            FileStream fileStream = new FileStream(sinacorNote, FileMode.Open);

            SinacorNoteReader snr = new SinacorNoteReader();
            var ret = snr.GetTransactions(fileStream);
            //using (StreamReader reader = new StreamReader(fileStream))
            //{
            //    string line = reader.ReadLine();
            //}
        }
    }
}
