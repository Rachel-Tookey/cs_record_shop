using System.Text.Json.Serialization;

namespace RecordShop.Entities
{
    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("artists")]
        public List<Artist> Artists { get; set; }
        
        public Genre() { }
    }
}
