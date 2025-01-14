using Microsoft.EntityFrameworkCore;
using RecordShop.Entities;

namespace RecordShop.Data
{
    public class RecordShopContext : DbContext 
    {
        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<AlbumGenre> AlbumGenres { get; set; }

        public RecordShopContext(DbContextOptions<RecordShopContext> options) : base(options) { } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Albums)
                .UsingEntity<AlbumGenre>();

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Albums)
                .WithMany(e => e.Genres)
                .UsingEntity<AlbumGenre>(); 
        }
    }
}
