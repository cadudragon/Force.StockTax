using iText.Kernel.Geom;
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
        //internal static RectanglePositions NegotiationsRectangle =
        //   new RectanglePositions { AxisX = 1, AxisY = 384, Width = 800, Height = 205 };

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

        /// <summary>
        ///  Rectangle equivalent to total sells operation
        /// </summary>
        internal static RectanglePositions TotalSells =
            new RectanglePositions { AxisX = 1, AxisY = 360, Width = 300, Height = 10 };

        /// <summary>
        ///  Rectangle equivalent to total sells operation
        /// </summary>
        internal static RectanglePositions TotalBuys =
            new RectanglePositions { AxisX = 1, AxisY = 350, Width = 300, Height = 10 };

        internal static NegotiationsPosition NegotiationsPosition = new NegotiationsPosition
        {
            BuySellRoll = new RectanglePositions { AxisX = 1, AxisY = 350, Width = 300, Height = 10 };
    };
    }


    internal class NegotiationsPosition
    {
        /// <summary>
        ///  Rectangle equivalent to the Buy/Sell Roll
        /// </summary>
        internal  RectanglePositions BuySellRoll { get; set; }

        /// <summary>
        ///  Rectangle equivalent to the Stock name
        /// </summary>
        internal  RectanglePositions StockName { get; set; }

        /// <summary>
        ///  Rectangle equivalent to the Amount of stocks
        /// </summary>
        internal  RectanglePositions Amount { get; set; }

        /// <summary>
        ///  Rectangle equivalent to the StockPrice
        /// </summary>
        internal  RectanglePositions StockPrice { get; set; }
    }

    protected class RectanglePositions
    {
        public float AxisX { get; set; }
        public float AxisY { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public static Rectangle GetRectangle(RectanglePositions rectanglePositions)
        {
            var rec = new Rectangle(rectanglePositions.AxisX, rectanglePositions.AxisY, rectanglePositions.Width, rectanglePositions.Height);
            return rec;
        }
    }


}
