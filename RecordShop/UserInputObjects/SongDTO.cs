using RecordShop.Entities;
using System.ComponentModel.DataAnnotations;

namespace RecordShop.UserInputObjects
{
    public class SongDTO
    {

        [Required]
        public string Name { get; set; }

        public int ArtistId { get; set; }

        public string Description { get; set; }

        public string SpotifyUrl { get; set; }
        
        [Required]
        public DateTime ReleaseDate { get; set; }

    }
}
