using System.ComponentModel.DataAnnotations;
using RecordShop.UserInputObjects;

namespace RecordShop.Entities
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string SpotifyUrl { get; set; }

        public Song() { }

        public Song(SongDTO songDto)
        {
            Name = songDto.Name;
            ArtistId = songDto.ArtistId;
            Description = songDto.Description;
            ReleaseDate = songDto.ReleaseDate;
            SpotifyUrl = songDto.SpotifyUrl;
        }

        public Song(string name, int artistId, string description, DateTime releaseDate, string spotifyUrl)
        {
            Name = name;
            ArtistId = artistId;
            Description = description;
            ReleaseDate = releaseDate;
            SpotifyUrl = spotifyUrl;
        }
        

    }
}
