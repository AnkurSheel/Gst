using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Newtonsoft.Json;

using Serko.Models;

using Xunit;

namespace Serko.Tests.Integration
{
    public class EmailDataControllerTests : IDisposable
    {
        private const string RequestUri = "api/EmailData";
        private readonly TestServer _server;

        private readonly HttpClient _client;

        public EmailDataControllerTests()
        {
            _server = new TestServer(new WebHostBuilder().UseKestrel().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _server?.Dispose();
            _client?.Dispose();
        }

        [Fact]
        public async Task Post_ValidFullEmailText_OkResponse()
        {
            const string EmailText = @"
Hi Yvaine,
Please create an expense claim for the below. Relevant details are marked up as requested…

<expense><cost_centre>DEV002</cost_centre>
    <total>1024.01</total><payment_method>personal card</payment_method>
</expense>

From: Ivan Castle
Sent: Friday, 16 February 2018 10:32 AM
To: Antoine Lloyd <Antoine.Lloyd@example.com>
Subject: test

Hi Antoine,

Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our 
<description>development team’s project end celebration dinner</description> on
<date>Tuesday 27 April 2017</date>. We expect to arrive around
7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.

Regards,
Ivan
";
            var input = new PostEmailDataRequest { Data = EmailText };
            var content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(RequestUri, content);

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonFromPostResponse = await response.Content.ReadAsStringAsync();

            var emailDataResponse = JsonConvert.DeserializeObject<PostEmailDataResponse>(jsonFromPostResponse);

            Assert.Equal("DEV002", emailDataResponse.ExtractedData.Expense.CostCenter);
            Assert.Equal(1024.01M, emailDataResponse.ExtractedData.Expense.Total);
            Assert.Equal("personal card", emailDataResponse.ExtractedData.Expense.PaymentMethod);
            Assert.Equal("Viaduct Steakhouse", emailDataResponse.ExtractedData.Vendor);
            Assert.Equal("development team’s project end celebration dinner", emailDataResponse.ExtractedData.Description);
            Assert.Equal("Tuesday 27 April 2017", emailDataResponse.ExtractedData.Date);

            // Data obtained from http://gstcal.co.nz/default.aspx
            Assert.Equal(1024.01M, emailDataResponse.Totals.TotalIncludingGst);
            Assert.Equal(133.57M, emailDataResponse.Totals.Gst);
            Assert.Equal(890.44M, emailDataResponse.Totals.TotalExcludingGst);

        }

        [Fact]
        public async Task Post_MissingClosingTag_InternalServerResponse()
        {
            const string EmailText = @"
<expense><cost_centre>DEV002</cost_centre>
    <total>1024.01</total><payment_method>personal card</payment_method>

Hi Antoine,
";
            var input = new PostEmailDataRequest { Data = EmailText };
            var content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(RequestUri, content);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal("Could not extract data", jsonFromPostResponse);
        }

        [Fact]
        public async Task Post_MissingTotal_BadRequestResponse()
        {
            const string EmailText = @"
<expense><cost_centre>DEV002</cost_centre>
    <payment_method>personal card</payment_method>
</expense>
";
            var input = new PostEmailDataRequest { Data = EmailText };
            var content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(RequestUri, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var jsonFromPostResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal("Total is missing", jsonFromPostResponse);
        }
    }
}
