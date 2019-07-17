using Serko.Services;

using Xunit;

namespace Serko.Tests
{
    public class TotalsCalculatorTests
    {
        private readonly ITotalsCalculator _totalsCalculator;

        public TotalsCalculatorTests()
        {
            _totalsCalculator = new TotalsCalculator();
        }

        [Fact]
        public void ExtractData_ValidFullEmailText_ValidEmailDataModel()
        {
            // Data obtained from http://gstcal.co.nz/default.aspx
            var calculatedExpenseData = _totalsCalculator.Calculate(1024.01M);
            Assert.Equal(1024.01M, calculatedExpenseData.TotalIncludingGst);
            Assert.Equal(133.57M, calculatedExpenseData.Gst);
            Assert.Equal(890.44M, calculatedExpenseData.TotalExcludingGst);
        }
    }
}
