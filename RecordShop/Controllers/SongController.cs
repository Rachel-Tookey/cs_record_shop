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
        public IActionResult GetAlbums()
        {
            var albums = _songService.GetSongs();
            return Ok(albums);
        }



        [HttpPost(Name = "AddSongs")]
        public IActionResult AddSong(SongDTO songDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Song newSong = new (songDto);
            _songService.AddSong(newSong);

            //if (newAlbum.Genres.Count > 0)
            //{
            //    foreach (var genre in newAlbum.Genres)
            //    {
            //        AlbumGenre newAB = new AlbumGenre() { Album = newAlbum, Genre = genre };
            //    }
            //}

            
            return Created("/Albums", "Album added");
        }



    }
}
