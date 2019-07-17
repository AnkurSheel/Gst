using Microsoft.AspNetCore.Mvc;

using Serko.Models;
using Serko.Services;

namespace Serko.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailDataController : Controller
    {
        private readonly IXmlParser _xmlParser;

        public EmailDataController(IXmlParser xmlParser)
        {
            _xmlParser = xmlParser;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostEmailDataRequest input)
        {
            var extractedData = _xmlParser.ExtractData(input.Data);
            var response = new PostEmailDataResponse() { ExtractedData = extractedData };
            return Ok(response);
        }
    }
}
