using Microsoft.EntityFrameworkCore;
using RecordShop.Entities;
using System.Text.Json; 

namespace RecordShop.Data
{
    public class RecordShopContext : DbContext 
    {

        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<AlbumGenre> AlbumGenres { get; set; }

        public RecordShopContext(DbContextOptions<RecordShopContext> options) : base(options) {
            Database.EnsureCreated();
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().Property(e => e.Id).ValueGeneratedOnAdd(); 
            
            modelBuilder.Entity<Artist>().HasData(JsonSerializer.Deserialize<List<Artist>>(File.ReadAllText("Resources/Artists.json")));

            modelBuilder.Entity<Album>().Property(e => e.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Album>().HasData(JsonSerializer.Deserialize<List<Album>>(File.ReadAllText("Resources/Album.json")));


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
