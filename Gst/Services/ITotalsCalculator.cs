using Gst.Models;

namespace Gst.Services
{
    public interface ITotalsCalculator
    {
        ExpenseTotals Calculate(decimal totalIncludingGst);
    }
}
