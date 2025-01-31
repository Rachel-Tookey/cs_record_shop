using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.Repository;
using RecordShop.Entities;
using FluentAssertions;

namespace RecordShop.Tests.Repository
{
    public class SongRepositoryTests
    {

        private SongRepository _songRepository;


        [SetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<RecordShopContextSqlite>().UseSqlite(connection).Options;
            RecordShopContextSqlite context = new RecordShopContextSqlite(options);
            context.Artists.Add( new Artist("Amy Winehouse", "imageurl", 4)); 
            context.Songs.Add(new Song("Amy The Real Me",  1, "Good", new DateTime(2024, 01, 02), "www.song.com")); 
            context.SaveChanges();  
            _songRepository = new SongRepository(context);
        }


        [Test]
        public void GetAllSongs_ReturnsList()
        {
            var result = _songRepository.FetchSongs();
            result.Count.Should().Be(6);
            result[5].Name.Should().Be("Amy The Real Me");
        }


        [Test]
        public void AddSong_AddSongToDb()
        {
            var songToAdd = new Song("Amy The Real Me 2", 1, "Good", new DateTime(2024, 01, 02), "www.song.com");
            _songRepository.AddSong(songToAdd);

            var result = _songRepository.FetchSongs();

            result.Count.Should().Be(7);
            result[6].Name.Should().Be("Amy The Real Me 2");

        }

        [Test]
        public void AddSong_ForeignKeyError()
        {
            var songToAdd = new Song("Amy The Real Me", 10, "Good", new DateTime(2024, 01, 02), "www.song.com");
            Action addSongAction = () => _songRepository.AddSong(songToAdd);
            addSongAction.Should().Throw<DbUpdateException>();

        }



    }
}
