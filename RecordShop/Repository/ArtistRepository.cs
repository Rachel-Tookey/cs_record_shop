using RecordShop.Data;
using RecordShop.Entities;

namespace RecordShop.Repository
{
    public interface IArtistRepository
    {
        public List<Artist> GetAllArtists();

        public void AddArtist(Artist artist);

    }


    public class ArtistRepository : IArtistRepository
    {
        private readonly RecordShopContext _recordShopContext;

        public ArtistRepository(RecordShopContext recordShopContext)
        {
            _recordShopContext = recordShopContext;

        }

        public List<Artist> GetAllArtists()
        {
            return _recordShopContext.Artists.ToList();
        }

        public void AddArtist(Artist artist)
        {
            _recordShopContext.Artists.Add(artist);
            _recordShopContext.SaveChanges();
        }

    }
}
