using RecordShop.Entities;
using RecordShop.Repository;

namespace RecordShop.Services
{
    public interface IArtistService
    {
        public List<Artist> GetAllArtists();

        public void AddArtist(Artist artist);

    }


    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;

        }

        public List<Artist> GetAllArtists()
        {
            return _artistRepository.GetAllArtists();
        }

        public void AddArtist(Artist artist)
        {
            _artistRepository.AddArtist(artist);
        }


    }
}
