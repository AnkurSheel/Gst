namespace Gst.Models
{
    public class ExpenseTotals
    {
        public decimal TotalIncludingGst { get; set; }

        public decimal Gst { get; set; }

        public decimal TotalExcludingGst { get; set; }
    }
}
