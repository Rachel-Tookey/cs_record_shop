using Microsoft.AspNetCore.Mvc;
using RecordShop.Entities;
using RecordShop.Services;
using RecordShop.UserInputObjects;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            Console.WriteLine("Get artists controller called");
            var artists = _artistService.GetAllArtists();
            return Ok(artists);
        }


        [HttpPost(Name = "AddArtist")]
        public IActionResult AddArtist(ArtistDTO artist)
        {
            Console.WriteLine("Now posting Artist");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Artist newArtist = new Artist() { Name = artist.Name, ImageUrl = artist.ImageUrl, YearsActive = artist.YearsActive } ;
            _artistService.AddArtist(newArtist);
            return Created("/artists", "artist added");
        }


        [HttpGet("{id}")]
        public IActionResult GetArtistById(int id)
        {
            Console.WriteLine("id is :" + id);
            if (!_artistService.ExistsById(id))
            {
                return BadRequest("Id does not exist"); 
            }
            var artist = _artistService.GetArtistById(id);
            return Ok(artist);
        }

        [HttpPut]
        public IActionResult PutArtist(UpdateArtist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Console.WriteLine("Called the controller:" + artist.ImageUrl);

            var updatedArtist = _artistService.UpdateArtistByName(artist);

            return Ok(updatedArtist);
        
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArtist(int id)
        {
            if (!_artistService.ExistsById(id))
            {
                return BadRequest("Id does not exist");
            }
            _artistService.DeleteById(id);
            return NoContent();
        }


    }
}
