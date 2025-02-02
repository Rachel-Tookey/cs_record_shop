using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RecordShop.UserInputObjects;
using RecordShop.Entities;
using RecordShop.Services;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {

        private readonly ISongService _songService; 

        public SongController(ISongService songService)
        {
            _songService = songService;
        }
        
        [HttpGet(Name = "GetSongs")]
        public IActionResult GetSongs(string artistName = "", string songName = "", [FromQuery] string[] genres = null)
        {
            var songs = _songService.GetSongs(artistName, songName, genres);
            return Ok(songs);
        }
        

        [HttpPost(Name = "AddSongs")]
        public IActionResult AddSong(SongDTO songDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Song newSong = new (songDto);
            _songService.AddSong(newSong);
            return Created("/Songs", "Song added");
        }

        [HttpGet("random")]
        public IActionResult GetRandomSong()
        {
            var randomSong = _songService.GetRandomSong(); 
            return Ok(randomSong);
        }

        [HttpGet("homepage")]
        public IActionResult GetMatchingSongArtist(string search = "")
        {
            var songs = _songService.GetMatchingSongs(search);
            return Ok(songs);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSong(int id)
        {
            if (!_songService.ExistsById(id)) return BadRequest("Id does not exist");
            _songService.DeleteById(id);
            return NoContent();
        }
        
        
    }
}
