using Microsoft.AspNetCore.Mvc;
using RecordShop.UserInputObjects;
using RecordShop.Entities;
using RecordShop.Services;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {

        private readonly IAlbumService _albumService; 

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }


        [HttpGet(Name = "GetAlbums")]
        public IActionResult GetAlbums()
        {
            var albums = _albumService.GetAlbums();
            return Ok(albums);
        }



        [HttpPost(Name = "AddAlbums")]
        public IActionResult AddAlbum(AlbumDTO albumDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Album newAlbum = new Album();

            newAlbum.Name = albumDTO.Name;
            newAlbum.ArtistId = albumDTO.ArtistId;
            newAlbum.Description = albumDTO.Description;
            newAlbum.ReleaseDate = albumDTO.ReleaseDate;

            //if (newAlbum.Genres.Count > 0)
            //{
            //    foreach (var genre in newAlbum.Genres)
            //    {
            //        AlbumGenre newAB = new AlbumGenre() { Album = newAlbum, Genre = genre };
            //    }
            //}

            _albumService.AddAlbum(newAlbum);
            
            return Created("/Albums", "Album added");
        }



    }
}
