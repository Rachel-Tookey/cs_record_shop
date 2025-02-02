using Microsoft.EntityFrameworkCore;
using RecordShop.Entities;

namespace RecordShop.Data
{
    public interface IDbContext : IDisposable 
    {
        public DbSet<Song> Songs { get;  }

        public DbSet<Artist> Artists { get; }

        public DbSet<Genre> Genres { get; }

        // public DbSet<ArtistGenre> SongGenres { get; }

        int SaveChanges();

    }
}
