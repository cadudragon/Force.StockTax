using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Force.StockTax.Sinacor.Utils.Extensions
{

    //https://stackoverflow.com/questions/48597948/text-extraction-from-a-pdf-using-itext7-how-to-improve-its-performance
    internal static class ReaderExtensions
    {
        public static string[] ExtractText(this PdfPage page, params Rectangle[] rects)
        {
            var textEventListener = new LocationTextExtractionStrategy();
            PdfTextExtractor.GetTextFromPage(page, textEventListener);
            string[] result = new string[rects.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = textEventListener.GetResultantText(rects[i]);
            }
            return result;
        }

        public static String GetResultantText(this LocationTextExtractionStrategy strategy, Rectangle rect)
        {
            IList<TextChunk> locationalResult = (IList<TextChunk>)locationalResultField.GetValue(strategy);
            List<TextChunk> nonMatching = new List<TextChunk>();
            foreach (TextChunk chunk in locationalResult)
            {
                ITextChunkLocation location = chunk.GetLocation();
                Vector start = location.GetStartLocation();
                Vector end = location.GetEndLocation();
                if (!rect.IntersectsLine(start.Get(Vector.I1), start.Get(Vector.I2), end.Get(Vector.I1), end.Get(Vector.I2)))
                {
                    nonMatching.Add(chunk);
                }
            }
            nonMatching.ForEach(c => locationalResult.Remove(c));
            try
            {
                return strategy.GetResultantText();
            }
            finally
            {
                nonMatching.ForEach(c => locationalResult.Add(c));
            }
        }

        private static FieldInfo locationalResultField = typeof(LocationTextExtractionStrategy).GetField("locationalResult", BindingFlags.NonPublic | BindingFlags.Instance);
    }

}
