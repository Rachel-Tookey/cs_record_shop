using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace RecordShop.Entities
{
    public class Artist
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("imageurl")]
        public string ImageUrl { get; set; } 

        public List<Album> Albums { get; set; }

    }
}
