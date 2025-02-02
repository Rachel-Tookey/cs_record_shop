using RecordShop.Entities;
using RecordShop.Repository;
using FuzzySharp; 

namespace RecordShop.Services
{

    public interface ISongService
    {
        public List<Song> GetSongs(string artistName, string songName, string genre);
        public void AddSong(Song song);
        public Song GetRandomSong();
        public List<Song> GetMatchingSongs(string search);
        public bool ExistsById(int id);
        public void DeleteById(int id);
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
        
        public List<Song> GetMatchingSongs(string search = "")
        {
            var songs = _songRepository.FetchSongs();
            if (songs == null) return new List<Song>();
            if (!string.IsNullOrEmpty(search)) songs = songs
                .Where(a => Fuzz.PartialRatio(a.Name.ToLower(), search.ToLower()) > 60 || Fuzz.PartialRatio(a.Artist.Name.ToLower(), search.ToLower()) > 90)
                .OrderByDescending(a => Fuzz.PartialRatio(a.Name.ToLower(), search.ToLower()))
                .Take(6).ToList();
            return songs; 
        }
        
        public Song GetRandomSong()
        {
            var songs = _songRepository.FetchSongs();
            Random rn = new Random();
            var randomSong = songs[rn.Next(0, songs.Count)]; 
            return randomSong; 
        }

        public void AddSong(Song song)
        {
            _songRepository.AddSong(song); 
        }

        public bool ExistsById(int id)
        {
            return _songRepository.ExistsById(id); 
        }

        public void DeleteById(int id)
        {
            _songRepository.RemoveById(id);
        }

    }
}
