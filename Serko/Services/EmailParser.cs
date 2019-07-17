using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

using Serko.Models;

namespace Serko.Services
{
    public class EmailParser : IEmailParser
    {
        public ExtractedExpenseData ExtractData(string text)
        {
            ExtractedExpenseData extractedExpenseData;
            text = CleanDataForXmlParsing(text);
            var xmlText = $"<root>{text}</root>";
            try
            {
                var serializer = new XmlSerializer(typeof(ExtractedExpenseData));
                using (TextReader reader = new StringReader(xmlText))
                {
                    extractedExpenseData = (ExtractedExpenseData)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return extractedExpenseData;
        }

        private string CleanDataForXmlParsing(string text)
        {
            return Regex.Replace(text, @"(From:.*)|(To.*)", "");
        }
    }
}
