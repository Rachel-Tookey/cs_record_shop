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
        private readonly RecordShopContextSqlServer _recordShopContext;

        public ArtistRepository(RecordShopContextSqlServer recordShopContext)
        {
            _recordShopContext = recordShopContext;

        }

        public List<Artist> FetchAllArtists()
        {
            return _recordShopContext.Artists.ToList();
        }

        public void AddArtist(Artist artist)
        {
            _recordShopContext.Artists.Add(artist);
            _recordShopContext.SaveChanges();
        }

        public Artist FetchArtistById(int id)
        {
            return _recordShopContext.Artists.Where(a => a.Id == id).Include(a => a.Albums).First(); 

        }

        public bool ExistsById(int id)
        {
            return _recordShopContext.Artists.Where(a => a.Id == id).Any(); 
        }

        public Artist UpdateArtistByName(UpdateArtist artistUpdate)
        {
            var artistRecord = FetchArtistById(artistUpdate.Id);
            artistRecord.Name = artistUpdate.Name;
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


    public class ArtistRepositoryDev : IArtistRepository
    {
        private readonly RecordShopContextSqlite _recordShopContext;

        public ArtistRepositoryDev(RecordShopContextSqlite recordShopContext)
        {
            _recordShopContext = recordShopContext;

        }

        public List<Artist> FetchAllArtists()
        {
            return _recordShopContext.Artists.ToList();
        }

        public void AddArtist(Artist artist)
        {
            _recordShopContext.Artists.Add(artist);
            _recordShopContext.SaveChanges();
        }

        public Artist FetchArtistById(int id)
        {
            return _recordShopContext.Artists.Where(a => a.Id == id).Include(a => a.Albums).First();

        }

        public bool ExistsById(int id)
        {
            return _recordShopContext.Artists.Where(a => a.Id == id).Any();
        }

        public Artist UpdateArtistByName(UpdateArtist artistUpdate)
        {
            var artistRecord = FetchArtistById(artistUpdate.Id);
            artistRecord.Name = artistUpdate.Name;
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
