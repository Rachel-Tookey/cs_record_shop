using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.Entities;

namespace RecordShop.Repository
{
    public interface IAlbumRepository
    {
        public List<Album> FetchAlbums(); 

        public void AddAlbum(Album album);  

    }


    public class AlbumRepository : IAlbumRepository
    {
        private readonly RecordShopContextSqlServer _recordShopContext;

        public AlbumRepository(RecordShopContextSqlServer rsContext) { 
            _recordShopContext = rsContext;
        
        }

        public List<Album> FetchAlbums()
        {
            return _recordShopContext.Albums.ToList();
        }

        public void AddAlbum(Album album)
        {
            _recordShopContext.Albums.Add(album);
            _recordShopContext.SaveChanges();
        }


    }
}
