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

        public EmailDataController(IEmailParser emailParser)
        {
            _emailParser = emailParser;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostEmailDataRequest input)
        {
            var extractedData = _emailParser.ExtractData(input.Data);
            var response = new PostEmailDataResponse() { ExtractedData = extractedData };
            return Ok(response);
        }
    }
}
