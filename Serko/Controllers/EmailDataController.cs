using Microsoft.AspNetCore.Mvc;

using Serko.Models;
using Serko.Services;

namespace Serko.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailDataController : Controller
    {
        private readonly IEmailParser _emailParser;

        private readonly ITotalsCalculator _totalsCalculator;

        public EmailDataController(IEmailParser emailParser, ITotalsCalculator totalsCalculator)
        {
            _emailParser = emailParser;
            _totalsCalculator = totalsCalculator;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostEmailDataRequest input)
        {
            var extractedData = _emailParser.ExtractData(input.Data);
            var totals = _totalsCalculator.Calculate(extractedData.Expense.Total);
            var response = new PostEmailDataResponse() { ExtractedData = extractedData, Totals = totals};
            return Ok(response);
        }
    }
}
