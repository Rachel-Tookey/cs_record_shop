using Microsoft.EntityFrameworkCore;
using RecordShop.Entities;

namespace RecordShop.Data
{
    public interface IDbContext : IDisposable 
    {
        public DbSet<Album> Albums { get;  }

        public DbSet<Artist> Artists { get; }

        public DbSet<Genre> Genres { get; }

        public DbSet<AlbumGenre> AlbumGenres { get; }

        int SaveChanges();

    }
}
