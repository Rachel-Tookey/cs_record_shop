using Microsoft.AspNetCore.Mvc;
using RecordShop.DTO;
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
        public IActionResult AddAlbum(AlbumDTO albumDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Album newAlbum = new Album();

            // map these objects using mapper 
            newAlbum.Name = albumDTO.Name;
            newAlbum.ArtistId = albumDTO.ArtistId;
            newAlbum.Description = albumDTO.Description;
            newAlbum.ReleaseDate = albumDTO.ReleaseDate;



            _albumService.AddAlbum(newAlbum);
            return Created("/Albums", "Album added");
        }



    }
}
