using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Serko.Exceptions;
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

        private readonly IEmailCleaner _emailCleaner;

        public EmailDataController(IEmailParser emailParser, ITotalsCalculator totalsCalculator, IEmailCleaner emailCleaner)
        {
            _emailParser = emailParser;
            _totalsCalculator = totalsCalculator;
            _emailCleaner = emailCleaner;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostEmailDataRequest input)
        {
            PostEmailDataResponse response;
            try
            {
                var cleanedText = _emailCleaner.Clean(input.Data);
                var extractedData = _emailParser.ExtractData(cleanedText);
                var totals = _totalsCalculator.Calculate(extractedData.Expense.Total ?? 0);
                response = new PostEmailDataResponse { ExtractedData = extractedData, Totals = totals };
            }
            catch (ExtractDataException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (MissingTotalException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(response);
        }
    }
}
