using Microsoft.AspNetCore.Mvc;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {

        private readonly ILogger<AlbumController> _logger;

        public AlbumController(ILogger<AlbumController> logger)
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
