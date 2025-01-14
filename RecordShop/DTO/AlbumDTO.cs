using RecordShop.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecordShop.DTO
{
    public class AlbumDTO
    {

        [Required]
        public string Name { get; set; }

        public int ArtistId { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

    }
}
