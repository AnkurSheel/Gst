using Serko.Exceptions;
using Serko.Services;

using Xunit;

namespace Serko.Tests.Unit
{
    public class EmailParserTests
    {
        private readonly IEmailParser _emailParser;

        public EmailParserTests()
        {
            _emailParser = new EmailParser();
        }

        [Fact]
        public void ExtractData_CleanedFullEmailText_ValidExtractedDataModel()
        {
            const string EmailText = @"
Hi Yvaine,
Please create an expense claim for the below. Relevant details are marked up as requested…

<expense><cost_centre>DEV002</cost_centre>
    <total>1024.01</total><payment_method>personal card</payment_method>
</expense>

Sent: Friday, 16 February 2018 10:32 AM
Subject: test

Hi Antoine,

Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our 
<description>development team’s project end celebration dinner</description> on
<date>Tuesday 27 April 2017</date>. We expect to arrive around
7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.

Regards,
Ivan
";

            var emailData = _emailParser.ExtractData(EmailText);
            Assert.Equal("DEV002", emailData.Expense.CostCenter);
            Assert.Equal(1024.01M, emailData.Expense.Total);
            Assert.Equal("personal card", emailData.Expense.PaymentMethod);
            Assert.Equal("Viaduct Steakhouse", emailData.Vendor);
            Assert.Equal("development team’s project end celebration dinner", emailData.Description);
            Assert.Equal("Tuesday 27 April 2017", emailData.Date);
        }

        [Fact]
        public void ExtractData_ValidExpenseText_ValidExpenseModel()
        {
            const string EmailText = @"
<expense><cost_centre>DEV002</cost_centre>
    <total>1024.01</total><payment_method>personal card</payment_method>
</expense>
";

            var emailData = _emailParser.ExtractData(EmailText);
            Assert.Equal("DEV002", emailData.Expense.CostCenter);
            Assert.Equal(1024.01M, emailData.Expense.Total, 2);
            Assert.Equal("personal card", emailData.Expense.PaymentMethod);
        }

        [Fact]
        public void ExtractData_InvalidText_ThrowsException()
        {
            const string EmailText = @"
<expense><cost_centre>DEV002</cost_centre>
    <total>1024.01</total><payment_method>personal card</payment_method>
</expense>

From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test


Hi Antoine,
";

           var ex = Assert.Throws<ExtractExpenseException>(() => _emailParser.ExtractData(EmailText));
           Assert.Equal("Could not extract data", ex.Message);
           Assert.NotNull(ex.InnerException);
        }
    }
}
