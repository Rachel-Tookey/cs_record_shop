using RecordShop.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RecordShop.UserInputObjects
{
    public class SongDTO
    {
        
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("artistid")]
        public int ArtistId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("spotifyurl")]
        public string SpotifyUrl { get; set; }
        
        [Required]
        [JsonPropertyName("releasedate")]
        public DateTime ReleaseDate { get; set; }

    }
}
