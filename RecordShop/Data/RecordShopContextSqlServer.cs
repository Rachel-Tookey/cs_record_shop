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

        public DbSet<SongGenre> SongGenres { get; set; }

        public RecordShopContextSqlServer(DbContextOptions<RecordShopContextSqlServer> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Artist>().HasData(JsonSerializer.Deserialize<List<Artist>>(File.ReadAllText("Resources/Artists.json")));

            modelBuilder.Entity<Song>().HasData(JsonSerializer.Deserialize<List<Song>>(File.ReadAllText("Resources/Songs.json")));


            modelBuilder.Entity<Song>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Songs)
                .UsingEntity<SongGenre>();

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Songs)
                .WithMany(e => e.Genres)
                .UsingEntity<SongGenre>();
        }

    }
}
