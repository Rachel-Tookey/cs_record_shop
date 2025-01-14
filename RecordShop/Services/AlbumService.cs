using RecordShop.Data;
using RecordShop.Entities;
using RecordShop.Repository;

namespace RecordShop.Services
{

    public interface IAlbumService
    {
        public List<Album> GetAllAlbums();

        public void AddAlbum(Album album);

    }



    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;

        }

        public List<Album> GetAllAlbums()
        {
            return _albumRepository.GetAllAlbums();
        }

        public void AddAlbum(Album album)
        {
            _albumRepository.AddAlbum(album); 
        }


    }
}
