using System;
using System.IO;
using System.Xml.Serialization;

using Gst.Exceptions;
using Gst.Models;

namespace Gst.Services
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
                //Todo: we should log this
                throw new ExtractDataException("Could not extract data", e);
            }

            if (extractedExpenseData.Expense.Total == null)
            {
                throw new MissingTotalException();
            }

            extractedExpenseData.Expense.CostCenter = extractedExpenseData.Expense.CostCenter ?? "UNKNOWN";

            return extractedExpenseData;
        }
    }
}
