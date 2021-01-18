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
        /// <summary>
        ///  Rectangle equivalent to buy and sell operations on Sinacor Pdf Note
        /// </summary>
        internal static RectanglePositions NegotiationsRectangle =
           new RectanglePositions { AxisX = 1, AxisY = 384, Width = 800, Height = 205 };
        /// <summary>
        ///  Rectangle equivalent to broker name
        /// </summary>
        internal static RectanglePositions BrokerNameRectangle =
           new RectanglePositions { AxisX = 1, AxisY = 750, Width = 800, Height = 20 };

        /// <summary>
        ///  Rectangle equivalent to broker name
        /// </summary>
        internal static RectanglePositions NoteIdRectangle =
           new RectanglePositions { AxisX = 1, AxisY = 760, Width = 800, Height = 20 };
    }

    internal class RectanglePositions
    {
        public float AxisX { get; set; }
        public float AxisY { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }


}
