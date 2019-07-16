using System.Xml.Serialization;

namespace Serko.Models
{
    public class Expense
    {
        public string CostCenter { get; set; }

        public decimal Total { get; set; }

        public string PaymentMethod { get; set; }
    }
}
