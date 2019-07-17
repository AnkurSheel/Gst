using System.Xml.Serialization;

namespace Serko.Models
{
    [XmlRoot("root")]
    public class EmailData
    {
        [XmlElement("expense")]
        public Expense Expense { get; set; }

        [XmlElement("vendor")]
        public string Vendor { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        //ToDo: Should this be a DateTime or something similar?
        [XmlElement("date")]
        public string Date { get; set; }
    }
}
