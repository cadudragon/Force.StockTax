using Force.StockTax.ConsoleClient.Configuration;
using Force.StockTax.Model.DTO;
using Force.StockTax.Sinacor.Services;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Force.StockTax.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupManager.Setup();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", true);

            //string sinacorNotePath = @"C:\Users\Desenv01\Downloads\magalu.pdf";

           DirectoryInfo d = new DirectoryInfo(SetupConstants.DocumentsFolderPath + "\\notas-para-processar");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.pdf"); //Getting Text files

            var notes = new List<SinacorNoteDto>();

            foreach (FileInfo file in Files)
            {
                MemoryStream sinacorNoteStream = new MemoryStream(File.ReadAllBytes(file.FullName));

                SinacorNoteReader snr = new SinacorNoteReader(sinacorNoteStream);
                var ret = snr.GetSinacorNote();

                notes.Add(ret);
            }

            var writer = new SinacorNoteWriter();
            writer.SinacorNotesToLocalSpreadsheet(notes);

        }
    }
}
