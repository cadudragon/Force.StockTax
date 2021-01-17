using Force.StockTax.Bovespa.Enums;
using Force.StockTax.Bovespa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Force.StockTax.Bovespa.Utils
{
    public class SinacorNoteParser
    {
        public static List<Negotiation> ParseNegotiations(List<string> negotiations, SinacorNote sn)
        {

            List<Negotiation> ret = new List<Negotiation>();

            foreach (var n in negotiations)
            {
                if (Regex.IsMatch(negotiations.First(), @" D\n"))
                {
                    ret.AddRange(
                        Regex.Split(negotiations.First(), @" D\n")
                        .Select(bn => ParseNegotiationLine(bn, NegotiationType.BUY, sn))
                        .ToList()
                        );
                }
                else if (Regex.IsMatch(negotiations.First(), @" C\n"))
                {
                    ret.AddRange(
                        Regex.Split(negotiations.First(), @" C\n")
                        .Select(bn => ParseNegotiationLine(bn, NegotiationType.SELL, sn))
                        .ToList()
                        );
                }
            }
            return ret;
        }

        private static Negotiation ParseNegotiationLine(string negotiation, NegotiationType nt, SinacorNote sn)
        {

            var ret = new Negotiation();
            try
            {
                var negotiantionStrList = negotiation.Split(' ').ToList();
                negotiantionStrList.RemoveAll(n => string.IsNullOrWhiteSpace(n));

                //Removing last ocurrency string Buy(D) Sell (C) that wasnt removed on outside split
                var lastElement = negotiantionStrList.ElementAt(negotiantionStrList.Count - 1);
                if (lastElement.Last().Equals('D') || lastElement.Last().Equals('C')) {
                    negotiantionStrList.RemoveAt(negotiantionStrList.Count - 1);
                }


                //Removing column Prazo
                if (negotiantionStrList.Count() == 11)
                {
                    negotiantionStrList.RemoveAt(3);
                }

                //Removing column Obs. (*)
                if (negotiantionStrList.Count() == 10) {
                    negotiantionStrList.RemoveAt(6);
                }

                 ret = new Negotiation
                {
                    SinacorNote = sn,
                    Stock = new Stock { StockCode = negotiantionStrList.ElementAt(3) },
                    NegotiationType = nt,
                    Amount = int.Parse(negotiantionStrList.ElementAt(6)),
                    UnitaryPrice = decimal.Parse(negotiantionStrList.ElementAt(7)),
                    TotalPrice = decimal.Parse(negotiantionStrList.ElementAt(8))
                };
            }
            catch (Exception ex)
            {

             //TODO: Log Error   
            }
            return ret;
        }
    }
}
