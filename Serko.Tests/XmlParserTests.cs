using Serko.Services;

using Xunit;

namespace Serko.Tests
{
    public class XmlParserTests
    {
        private readonly IXmlParser _xmlParser;

        public XmlParserTests()
        {
            _xmlParser = new XmlParser();
        }

        [Fact]
        public void ExtractXml_ValidFullEmailText_ValidEmailDataModel()
        {
            const string EmailText = @"
Hi Yvaine,
Please create an expense claim for the below. Relevant details are marked up as requested�

<expense><cost_centre>DEV002</cost_centre>
    <total>1024.01</total><payment_method>personal card</payment_method>
</expense>

From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test

Hi Antoine,

Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our 
<description>development team�s project end celebration dinner</description> on
<date>Tuesday 27 April 2017</date>. We expect to arrive around
7.15pm. Approximately 12 people but I�ll confirm exact numbers closer to the day.

Regards,
Ivan
";

            var emailData = _xmlParser.ExtractXml(EmailText);
            Assert.Equal("DEV002", emailData.Expense.CostCenter);
            Assert.Equal(1024.01M, emailData.Expense.Total);
            Assert.Equal("personal card", emailData.Expense.PaymentMethod);
            Assert.Equal("Viaduct Steakhouse", emailData.Vendor);
            Assert.Equal("development team�s project end celebration dinner", emailData.Description);
            Assert.Equal("Tuesday 27 April 2017", emailData.Date);
        }

        [Fact]
        public void ExtractXml_ValidExpenseText_ValidExpenseModel()
        {
            const string EmailText = @"
<expense><cost_centre>DEV002</cost_centre>
    <total>1024.01</total><payment_method>personal card</payment_method>
</expense>
";

            var emailData = _xmlParser.ExtractXml(EmailText);
            Assert.Equal("DEV002", emailData.Expense.CostCenter);
            Assert.Equal(1024.01M, emailData.Expense.Total, 2);
            Assert.Equal("personal card", emailData.Expense.PaymentMethod);
        }
    }
}