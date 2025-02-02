using System.Text.Json.Serialization;
using RecordShop.UserInputObjects;

namespace RecordShop.Entities
{
    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Artist> Artists { get; set; }
        
        public Genre() { }

        public Genre(GenreDTO genreDTO)
        {
            Id = genreDTO.Id;
            Name = genreDTO.Name;
        }

    }
}
