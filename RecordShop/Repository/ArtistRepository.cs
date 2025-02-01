using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.Entities;
using RecordShop.UserInputObjects;

namespace RecordShop.Repository
{
    public interface IArtistRepository
    {
        public List<Artist> FetchAllArtists();
        public void AddArtist(Artist artist);
        public Artist FetchArtistById(int id);
        public bool ExistsById(int id);
        public Artist UpdateArtistByName(UpdateArtist artistUpdate);
        public void RemoveById(int id);
    }



    public class ArtistRepository : IArtistRepository
    {
        private readonly IDbContext _recordShopContext;

        public ArtistRepository(IDbContext recordShopContext)
        {
            _recordShopContext = recordShopContext;

        }

        public List<Artist> FetchAllArtists()
        {
            return _recordShopContext.Artists.Include(a => a.Songs).ToList();
        }

        public void AddArtist(Artist artist)
        {
            _recordShopContext.Artists.Add(artist);
            _recordShopContext.SaveChanges();
        }

        public Artist FetchArtistById(int id)
        {
            return _recordShopContext.Artists.Where(a => a.Id == id).Include(a => a.Songs).Include(a => a.Genres).First(); 

        }

        public bool ExistsById(int id)
        {
            return _recordShopContext.Artists.Where(a => a.Id == id).Any(); 
        }

        public Artist UpdateArtistByName(UpdateArtist artistUpdate)
        {
            var artistRecord = FetchArtistById(artistUpdate.Id);
            artistRecord.Name = artistUpdate.Name;
            artistRecord.ImageUrl = artistUpdate.ImageUrl;
            artistRecord.YearsActive = artistUpdate.YearsActive;
            _recordShopContext.SaveChanges(); 
            return artistRecord;
        }

        public void RemoveById(int id)
        {
            var artistRecord = FetchArtistById(id);
            _recordShopContext.Artists.Remove(artistRecord);
            _recordShopContext.SaveChanges();
        }


    }

}
