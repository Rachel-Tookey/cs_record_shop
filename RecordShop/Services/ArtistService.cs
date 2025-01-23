using RecordShop.Entities;
using RecordShop.Repository;
using RecordShop.UserInputObjects;
using System.Reflection.Metadata.Ecma335;

namespace RecordShop.Services
{
    public interface IArtistService
    {
        public List<Artist> GetAllArtists();

        public void AddArtist(Artist artist);
        public Artist GetArtistById(int id);
        public bool ExistsById(int id);
        public Artist UpdateArtistByName(UpdateArtist artistUpdate);
        public void DeleteById(int id);
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
            return _artistRepository.FetchAllArtists();
        }

        public void AddArtist(Artist artist)
        {
            _artistRepository.AddArtist(artist);
        }

        public Artist GetArtistById(int id)
        {
            var artist = _artistRepository.FetchArtistById(id);
            return artist; 
        }

        public bool ExistsById(int id)
        {
            return _artistRepository.ExistsById(id);
        }

        public Artist UpdateArtistByName(UpdateArtist artistUpdate)
        {
            return _artistRepository.UpdateArtistByName(artistUpdate);
        }

        public void DeleteById(int id)
        {
            _artistRepository.RemoveById(id);
        }



    }
}
