using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

using Serko.Models;

namespace Serko.Services
{
    public class XmlParser : IXmlParser
    {
        public ExtractedEmailData ExtractData(string text)
        {
            ExtractedEmailData extractedEmailData;
            text = CleanDataForXmlParsing(text);
            var xmlText = $"<root>{text}</root>";
            try
            {
                var serializer = new XmlSerializer(typeof(ExtractedEmailData));
                using (TextReader reader = new StringReader(xmlText))
                {
                    extractedEmailData = (ExtractedEmailData)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return extractedEmailData;
        }

        private string CleanDataForXmlParsing(string text)
        {
            return Regex.Replace(text, @"(From:.*)|(To.*)", "");
        }
    }
}
