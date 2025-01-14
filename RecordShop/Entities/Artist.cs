using System.Text.Json.Serialization;

namespace RecordShop.Entities
{
    public class Artist
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public List<Album> Albums { get; set; } 

    }
}
