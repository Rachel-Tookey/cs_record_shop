using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using RecordShop.UserInputObjects;


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

        [JsonPropertyName("yearsactive")]
        public int YearsActive { get; set; }

        
        public List<Song> Songs { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }

        public Artist() {} 

        public Artist(ArtistDTO artistDTO)
        {
            Name = artistDTO.Name;
            ImageUrl = artistDTO.ImageUrl;
            YearsActive = artistDTO.YearsActive;
        }

        public Artist(string name, string imageUrl, int yearsActive)
        {
            Name = name;
            ImageUrl = imageUrl;
            YearsActive = yearsActive;
        }
        
    }
}
