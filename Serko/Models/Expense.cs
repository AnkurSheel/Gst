using System.Xml.Serialization;

namespace Serko.Models
{
    public class Expense
    {
        [XmlElement("cost_centre")]
        public string CostCenter { get; set; }

        [XmlElement("total")]
        public decimal Total { get; set; }

        [XmlElement("payment_method")]
        public string PaymentMethod { get; set; }
    }
}
