using Serko.Services;

using Xunit;

namespace Serko.Tests
{
    public class EmailCleanerTests
    {
        private readonly IEmailCleaner _emailCleaner;

        public EmailCleanerTests()
        {
            _emailCleaner = new EmailCleaner();
        }

        [Fact]
        public void Clean_EmailHeader_RemovesFromAndToData()
        {
            const string EmailText = @"
Some Data
From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test

Some more data
";

            const string ExpectedText = @"
Some Data

Sent: Friday, 16 February 2018 10:32 AM

Subject: test

Some more data
";

            var cleanedText = _emailCleaner.Clean(EmailText);
            Assert.Equal(ExpectedText, cleanedText, ignoreLineEndingDifferences:true);
        }
    }
}
