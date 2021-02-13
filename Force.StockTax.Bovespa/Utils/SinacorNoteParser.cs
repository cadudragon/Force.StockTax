using Force.StockTax.Bovespa.DTOs;
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
        public static List<NegotiationDto> ParseNegotiations(List<string> negotiations, SinacorNoteDto sn)
        {

            List<NegotiationDto> ret = new List<NegotiationDto>();

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

        private static NegotiationDto ParseNegotiationLine(string negotiation, NegotiationType nt, SinacorNoteDto sn)
        {
            ;

            var ret = new NegotiationDto();
            try
            {
                var negotiantionStrList = JoinCompanyName(negotiation).Split(' ').ToList();
                negotiantionStrList.RemoveAll(n => string.IsNullOrWhiteSpace(n));

                //Removing last ocurrency string Buy(D) Sell (C) that wasnt removed on outside split
                var lastElement = negotiantionStrList.ElementAt(negotiantionStrList.Count - 1);
                if (lastElement.Last().Equals('D') || lastElement.Last().Equals('C'))
                {
                    negotiantionStrList.RemoveAt(negotiantionStrList.Count - 1);
                }


                //Removing column Prazo
                if (negotiantionStrList.Count() == 11)
                {
                    negotiantionStrList.RemoveAt(3);
                }

                //Removing column Obs. (*)
                if (negotiantionStrList.Count() == 10)
                {
                    negotiantionStrList.RemoveAt(6);
                }

                ret = new NegotiationDto();
                ret.SinacorNoteId = sn.Id;
                ret.Stock = negotiantionStrList.ElementAt(3);
                ret.NegotiationType = nt;
                ret.Amount = int.Parse(negotiantionStrList.ElementAt(4));
                ret.UnitaryPrice = decimal.Parse(negotiantionStrList.ElementAt(5));
                ret.TotalPrice = decimal.Parse(negotiantionStrList.ElementAt(6));
            }
            catch (Exception ex)
            {

                throw;
            }
            return ret;
        }

        private static string JoinCompanyName(string negotiationLine)
        {
            negotiationLine = negotiationLine.Replace("ON NM", "");
            negotiationLine = negotiationLine.Replace("PN N1", "");
            negotiationLine = negotiationLine.Replace("ON", "");

            var ret = "";
            var companyNameSimplified = false;
            foreach (var companyName in SinacorCompanyNegotiationName.CompanyNegotiationNames)
            {
                if (negotiationLine.Contains(companyName.Key))
                {
                    ret = negotiationLine.Replace(companyName.Key, companyName.Value);
                    companyNameSimplified = true;
                }
            }

            if (!companyNameSimplified)
            {
                throw new Exception("JoinCompanyName not implemented for the given negotiationLine" + negotiationLine);
            }

            return ret;
        }
    }
}
