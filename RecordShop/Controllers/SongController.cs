using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetSongs(string artistName = "", string songName = "", string genre = "")
        {
            var songs = _songService.GetSongs(artistName, songName, genre);
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



    }
}
