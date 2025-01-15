using Microsoft.AspNetCore.Mvc;
using RecordShop.Entities;
using RecordShop.Services;
using RecordShop.Wrappers;

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


        [HttpGet("{id}")]
        public IActionResult GetArtistById(int id)
        {
            if (!_artistService.ExistsById(id))
            {
                return BadRequest("Id does not exist"); 
            }
            var artist = _artistService.GetArtistById(id);
            return Ok(artist);
        }

        [HttpPut]
        public IActionResult PutArtist(UpdateArtistWrapper artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

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
