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

            context.Artists.Add(new Artist { Name = "Amy", ImageUrl = "www.fake.com" });

            context.Songs.Add(new Song { Name = "Amy The Real Me", ArtistId = 1, Description = "Yo", SpotifyUrl = "www.song.com", ReleaseDate = new DateTime(2024, 01, 02) }); 

            context.SaveChanges();  

            _songRepository = new SongRepository(context);
        }


        [Test]
        public void GetAllSongs_ReturnsList()
        {
            var result = _songRepository.FetchSongs();

            result.Count.Should().Be(3);
            result[2].Name.Should().Be("Amy The Real Me");

        }


        [Test]
        public void AddSong_AddSongToDb()
        {
            var songToAdd = new Song { Name = "Amy The Real Me 2", ArtistId = 1, Description = "Yo Yo", ReleaseDate = new DateTime(2024, 02, 02) };

            _songRepository.AddSong(songToAdd);

            var result = _songRepository.FetchSongs();

            result.Count.Should().Be(4);
            result[3].Name.Should().Be("Amy The Real Me 2");

        }

        [Test]
        public void AddSong_ForeignKeyError()
        {
            var songToAdd = new Song { Name = "Amy The Real Me 2", ArtistId = 20, Description = "Yo Yo", ReleaseDate = new DateTime(2024, 02, 02) };

            Action addSongAction = () => _songRepository.AddSong(songToAdd);
            addSongAction.Should().Throw<DbUpdateException>();

        }



    }
}
