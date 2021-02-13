using Force.StockTax.Bovespa.DTOs;
using Force.StockTax.Bovespa.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Force.StockTax.Bovespa.Services
{

    public class SinacorNoteWriter
    {
        private string DocumentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ForceTaxes";
        public void SinacorNotesToLocalSpreadsheet(List<SinacorNoteDto> notes)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var groups = notes.GroupBy(x => x.Date.Year.ToString());
            foreach (var group in groups)
            {
                using (var p = new ExcelPackage())
                {


                }


                CreateTaxesSpreadsheet(group.Key, group.ToList());
            }
        }

        private void CreateTaxesSpreadsheet(string year, List<SinacorNoteDto> sinacorNotes)
        {

            using (var p = new ExcelPackage())
            {

                var groups = sinacorNotes.GroupBy(x => x.Date.Month);
                foreach (var g in groups)
                {
                    var month = g.ToList();
                    var ws = p.Workbook.Worksheets.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key));


                    ws.Cells["A1"].Value = "TOTAL DE COMPRAS";
                    ws.Cells["B1"].Value = "TOTAL DE VENDAS";
                    for (int i = 0; i < month.Count; i++)
                    {
                        ws.Cells[$"A{i+2}"].Value = month[i].TotalBuys;
                        ws.Cells[$"B{i+2}"].Value = month[i].TotalSells;
                    }
                }

                p.SaveAs(new FileInfo($"{DocumentsFolderPath}\\bovespa-taxes" + year + ".xlsx"));
            }

        }

    }
}
