using System.Text.Json.Serialization;

namespace RecordShop.Entities
{
    public class ArtistGenre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("artistid")]
        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        [JsonPropertyName("genreid")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
        
        public ArtistGenre() { }
    }
}
