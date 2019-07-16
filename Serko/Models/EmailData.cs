using System.Xml.Serialization;

namespace Serko.Models
{
    public class EmailData
    {
        public Expense Expense { get; set; }

        public string Vendor { get; set; }

        public string Description { get; set; }

        //ToDo: Should this be a DateTime or something similar?
        public string Date { get; set; }
    }
}
