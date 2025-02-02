using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.Entities;

namespace RecordShop.Repository
{
    public interface ISongRepository
    {
        public List<Song> FetchSongs(); 
        public void AddSong(Song song);
        public bool ExistsById(int id);
        public void RemoveById(int id);
        public Song FetchSongById(int id);
    }

    public class SongRepository : ISongRepository
    {
        private readonly IDbContext _recordShopContext;


        public SongRepository(IDbContext rsContext)
        {
            _recordShopContext = rsContext;
        }


        public List<Song> FetchSongs()
        {
            return _recordShopContext.Songs.Include(s => s.Artist).Include(s => s.Artist.Genres).ToList();
        }

        public void AddSong(Song song)
        {
            _recordShopContext.Songs.Add(song);
            _recordShopContext.SaveChanges();
        }
        
        public bool ExistsById(int id)
        {
            return _recordShopContext.Songs.Where(s => s.Id == id).Any(); 
        }

        public Song FetchSongById(int id)
        {
            return _recordShopContext.Songs.Where(s => s.Id == id).FirstOrDefault();
        }
        
        public void RemoveById(int id)
        {
            var songRecord = FetchSongById(id);
            _recordShopContext.Songs.Remove(songRecord);
            _recordShopContext.SaveChanges();
        }
            

    }

}
