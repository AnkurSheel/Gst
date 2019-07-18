using Gst.Models;

namespace Gst.Services
{
    public interface IEmailParser
    {
        ExtractedExpenseData ExtractData(string text);
    }
}
