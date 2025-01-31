using Microsoft.EntityFrameworkCore;
using RecordShop.Entities;
using System.Text.Json;

namespace RecordShop.Data
{
    public class RecordShopContextSqlServer : DbContext, IDbContext
    {
        public DbSet<Song> Songs { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<ArtistGenre> SongGenres { get; set; }

        public RecordShopContextSqlServer(DbContextOptions<RecordShopContextSqlServer> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Artist>().HasData(JsonSerializer.Deserialize<List<Artist>>(File.ReadAllText("Resources/Artists.json"))!);
            modelBuilder.Entity<Song>().HasData(JsonSerializer.Deserialize<List<Song>>(File.ReadAllText("Resources/Songs.json"))!);
            modelBuilder.Entity<Genre>().HasData(JsonSerializer.Deserialize<List<Genre>>(File.ReadAllText("Resources/Genres.json"))!);
            modelBuilder.Entity<ArtistGenre>().HasData(JsonSerializer.Deserialize<List<ArtistGenre>>(File.ReadAllText("Resources/ArtistGenres.json"))!);
            
            modelBuilder.Entity<Artist>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Artists)
                .UsingEntity<ArtistGenre>();

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Artists)
                .WithMany(e => e.Genres)
                .UsingEntity<ArtistGenre>();
        }

    }
}
