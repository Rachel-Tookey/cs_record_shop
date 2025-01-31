using Microsoft.EntityFrameworkCore;
using RecordShop.Entities;
using System.Text.Json; 

namespace RecordShop.Data
{
    public class RecordShopContextSqlite : DbContext, IDbContext
    {
        
        public DbSet<Song> Songs { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<ArtistGenre> SongGenres { get; set; }

        public RecordShopContextSqlite(DbContextOptions<RecordShopContextSqlite> options) : base(options) {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().Property(e => e.Id).ValueGeneratedOnAdd(); 
            modelBuilder.Entity<Song>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Genre>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ArtistGenre>().Property(e => e.Id).ValueGeneratedOnAdd();

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
