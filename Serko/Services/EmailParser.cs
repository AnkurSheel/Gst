using System;
using System.IO;
using System.Xml.Serialization;

using Serko.Models;

namespace Serko.Services
{
    public class EmailParser : IEmailParser
    {
        public ExtractedExpenseData ExtractData(string text)
        {
            ExtractedExpenseData extractedExpenseData;
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
    }
}
