using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.Entities;

namespace RecordShop.Repository
{
    public interface ISongRepository
    {
        public List<Song> FetchSongs(); 
        public void AddSong(Song song);  

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
    }

}
