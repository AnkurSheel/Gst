using Serko.Models;

namespace Serko.Services
{
    public interface IXmlParser
    {
        ExtractedEmailData ExtractData(string text);
    }
}