using RecordShop.Entities;
using RecordShop.Repository;
using FuzzySharp; 

namespace RecordShop.Services
{

    public interface ISongService
    {
        public List<Song> GetSongs(string artistName, string songName, string genre);

        public void AddSong(Song song);

    }



    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public List<Song> GetSongs(string artistName = "", string songName = "", string genre = "")
        {
            var songs = _songRepository.FetchSongs();
            if (songs == null) return new List<Song>();
            
            if (!string.IsNullOrEmpty(songName)) songs = songs.Where(s => Fuzz.PartialRatio(s.Name.ToLower(), songName.ToLower()) > 70).ToList();
            
            if (!string.IsNullOrEmpty(artistName)) songs = songs.Where(s => Fuzz.PartialRatio(s.Artist.Name.ToLower(), artistName.ToLower()) > 80).ToList();
            
            if (!string.IsNullOrEmpty(genre)) songs = songs.Where(s => s.Artist.Genres?.Any(g => g.Name.ToLower() == genre.ToLower()) ?? false).ToList();

            return songs.ToList();
        }

        public void AddSong(Song song)
        {
            _songRepository.AddSong(song); 
        }


    }
}
