using System;
using System.Collections.Generic;
using System.Text;

namespace Force.StockTax.Bovespa.Enums
{
    /// <summary>
    /// Rectangle position on PDF for a given section
    /// </summary>
    internal static class SinacorNotePositions
    {
        //internal static RectanglePositions BuyAndSellRectangle =
        //   new RectanglePositions { AxisX = 1, AxisY = 385, Width = 800, Height = 205 };

        internal static RectanglePositions BuyAndSellRectangle =
           new RectanglePositions { AxisX = 1, AxisY = 384, Width = 800, Height = 205 };

        internal static RectanglePositions NoteNumber =
            new RectanglePositions { AxisX = 1, AxisY = 380, Width = 800, Height = 235 };

        internal static RectanglePositions NegoctiationDate =
            new RectanglePositions { AxisX = 1, AxisY = 380, Width = 800, Height = 235 };
    }

    internal class RectanglePositions
    {
        public float AxisX { get; set; }
        public float AxisY { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }


}
