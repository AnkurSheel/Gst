using Microsoft.AspNetCore.Mvc;

using NSubstitute;

using Serko.Controllers;
using Serko.Models;
using Serko.Services;

using Xunit;

namespace Serko.Tests.Unit
{
    public class EmailDataControllerTests
    {
        private readonly EmailDataController _controller;

        private readonly IEmailParser _emailParser;

        private readonly ITotalsCalculator _totalsCalculator;

        private readonly IEmailCleaner _emailCleaner;

        public EmailDataControllerTests()
        {
            _emailCleaner = Substitute.For<IEmailCleaner>();
            _emailParser = Substitute.For<IEmailParser>();
            _totalsCalculator = Substitute.For<ITotalsCalculator>();
            _controller = new EmailDataController(_emailParser, _totalsCalculator, _emailCleaner);
        }

        [Fact]
        public void Post_NoExceptions_OkResponseWithExtractedData()
        {
            _emailCleaner.Clean("").ReturnsForAnyArgs("");
            var extractedExpenseData = new ExtractedExpenseData
                                       {
                                           Expense = new Expense { CostCenter = "DEV002", Total = 1024.01M, PaymentMethod = "personal card" },
                                           Vendor = "Viaduct Steakhouse",
                                           Description = "development team’s project end celebration dinner",
                                           Date = "Tuesday 27 April 2017"
                                       };
            _emailParser.ExtractData("").ReturnsForAnyArgs(extractedExpenseData);
            _totalsCalculator.Calculate(0M).ReturnsForAnyArgs(new ExpenseTotals());

            // Act
            var result = _controller.Post(new PostEmailDataRequest());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PostEmailDataResponse>(okResult.Value);
            Assert.Equal("DEV002", returnValue.ExtractedData.Expense.CostCenter);
            Assert.Equal(1024.01M, returnValue.ExtractedData.Expense.Total);
            Assert.Equal("personal card", returnValue.ExtractedData.Expense.PaymentMethod);
            Assert.Equal("Viaduct Steakhouse", returnValue.ExtractedData.Vendor);
            Assert.Equal("development team’s project end celebration dinner", returnValue.ExtractedData.Description);
            Assert.Equal("Tuesday 27 April 2017", returnValue.ExtractedData.Date);
        }

        [Fact]
        public void Post_NoExceptions_OkResponseWithCalculatedData()
        {
            _emailCleaner.Clean("").ReturnsForAnyArgs("");
            var extractedExpenseData = new ExtractedExpenseData { Expense = new Expense() };
            var totals = new ExpenseTotals { Gst = 1.1M, TotalExcludingGst = 2.7M, TotalIncludingGst = 3.6M };
            _emailParser.ExtractData("").ReturnsForAnyArgs(extractedExpenseData);
            _totalsCalculator.Calculate(0M).ReturnsForAnyArgs(totals);

            // Act
            var result = _controller.Post(new PostEmailDataRequest());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PostEmailDataResponse>(okResult.Value);
            Assert.Equal(1.1M, returnValue.Totals.Gst);
            Assert.Equal(2.7M, returnValue.Totals.TotalExcludingGst);
            Assert.Equal(3.6M, returnValue.Totals.TotalIncludingGst);
        }
    }
}
