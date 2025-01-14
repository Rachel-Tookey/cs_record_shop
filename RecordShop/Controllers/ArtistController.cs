using Microsoft.AspNetCore.Mvc;
using RecordShop.Entities;
using RecordShop.Services;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {

        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet(Name = "GetArtists")]
        public IActionResult GetArtists()
        {
            var artists = _artistService.GetAllArtists();
            return Ok(artists);
        }

        [HttpPost(Name = "AddArtist")]
        public IActionResult AddArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _artistService.AddArtist(artist);
            return Created("/artists", "artist added");
        }
    }
}
