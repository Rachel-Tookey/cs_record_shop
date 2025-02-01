using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RecordShop.Entities;

namespace RecordShop.UserInputObjects
{
    public class ArtistDTO
    {

        [Required]
        [Length(4, int.MaxValue)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [Required]
        [JsonPropertyName("yearsActive")]
        public int YearsActive { get; set; }
        
        [JsonPropertyName("genres")]
        public List<GenreDTO> GenresDTO { get; set; }

    }
}
