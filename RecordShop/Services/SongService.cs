using RecordShop.Data;
using RecordShop.Entities;
using RecordShop.Repository;

namespace RecordShop.Services
{

    public interface ISongService
    {
        public List<Song> GetSongs();

        public void AddSong(Song song);

    }



    public class SongService : ISongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;

        }

        public List<Song> GetSongs()
        {
            return _songRepository.FetchSongs();
        }

        public void AddSong(Song song)
        {
            _songRepository.AddSong(song); 
        }


    }
}
