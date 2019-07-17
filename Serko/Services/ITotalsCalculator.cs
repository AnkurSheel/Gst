using Serko.Models;

namespace Serko.Services
{
    public interface ITotalsCalculator
    {
        ExpenseTotals Calculate(decimal totalIncludingGst);
    }
}
