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

        public DbSet<SongGenre> SongGenres { get; set; }

        public RecordShopContextSqlite(DbContextOptions<RecordShopContextSqlite> options) : base(options) {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().Property(e => e.Id).ValueGeneratedOnAdd(); 
            
            modelBuilder.Entity<Artist>().HasData(JsonSerializer.Deserialize<List<Artist>>(File.ReadAllText("Resources/Artists.json")));

            modelBuilder.Entity<Song>().Property(e => e.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Song>().HasData(JsonSerializer.Deserialize<List<Song>>(File.ReadAllText("Resources/Songsjson")));


            modelBuilder.Entity<Song>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Albums)
                .UsingEntity<SongGenre>();

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Albums)
                .WithMany(e => e.Genres)
                .UsingEntity<SongGenre>();
        }

 

    }
}
