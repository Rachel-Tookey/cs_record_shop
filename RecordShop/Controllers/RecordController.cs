using Microsoft.AspNetCore.Mvc;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecordController : ControllerBase
    {

        private readonly ILogger<RecordController> _logger;

        public RecordController(ILogger<RecordController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetRecords")]
        public IActionResult GetRecords()
        {
            return Ok();
    }
    }
}
