using System;

using Serko.Models;

namespace Serko.Services
{
    public class TotalsCalculator : ITotalsCalculator
    {
        private const double GstRate = .15;

        public ExpenseTotals Calculate(decimal totalIncludingGst)
        {
            var totalIncludingGstAsDouble = (double)totalIncludingGst;

            var totalExcludingGst = totalIncludingGstAsDouble / (1 + GstRate);
            var gst = totalExcludingGst * GstRate;

            return new ExpenseTotals
                   {
                       TotalIncludingGst = totalIncludingGst,
                       Gst = decimal.Round((decimal)gst, 2, MidpointRounding.AwayFromZero),
                       TotalExcludingGst = decimal.Round((decimal)totalExcludingGst, 2, MidpointRounding.AwayFromZero)
                   };
        }
    }
}
