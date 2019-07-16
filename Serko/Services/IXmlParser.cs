using Serko.Models;

namespace Serko.Services
{
    public interface IXmlParser
    {
        EmailData ExtractXml(string text);
    }
}