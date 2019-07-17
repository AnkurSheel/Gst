using Serko.Models;

namespace Serko.Services
{
    public interface IEmailParser
    {
        ExtractedExpenseData ExtractData(string text);
    }
}