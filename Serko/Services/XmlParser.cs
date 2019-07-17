using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

using Serko.Models;

namespace Serko.Services
{
    public class XmlParser : IXmlParser
    {
        public EmailData ExtractXml(string text)
        {
            EmailData emailData;
            text = CleanDataForXmlParsing(text);
            var xmlText = $"<root>{text}</root>";
            try
            {
                var serializer = new XmlSerializer(typeof(EmailData));
                using (TextReader reader = new StringReader(xmlText))
                {
                    emailData = (EmailData)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return emailData;
        }

        private string CleanDataForXmlParsing(string text)
        {
            return Regex.Replace(text, @"(From:.*)|(To.*)", "");
        }
    }
}
