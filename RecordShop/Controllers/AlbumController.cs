using Microsoft.AspNetCore.Mvc;
using RecordShop.Entities;
using RecordShop.Services;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var albums = _albumService.GetAllAlbums();
            return Ok(albums);
        }

        [HttpPost(Name = "AddAlbums")]
        public IActionResult AddAlbum(Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _albumService.AddAlbum(album);
            return Created("/Albums", "Album added");
        }



    }
}
