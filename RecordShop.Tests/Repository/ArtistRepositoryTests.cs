using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.UserInputObjects;
using RecordShop.Entities;
using RecordShop.Repository;

namespace RecordShop.Tests.Repository
{
    public class ArtistRepositoryTests
    {
        private ArtistRepository _artistRepository;


        [SetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<RecordShopContextSqlite>().UseSqlite(connection).Options;
            RecordShopContextSqlite context = new RecordShopContextSqlite(options);

            _artistRepository = new ArtistRepository(context);
        }


        [Test]
        public void GetAllArtists_ReturnsList()
        {
            var result = _artistRepository.FetchAllArtists();

            result.Count.Should().Be(8);
            result[0].Name.Should().Be("Adele");

        }


        [Test]
        public void AddArtist_AddsArtist()
        {
            var artistToAdd = new Artist { Name = "Amy", ImageUrl = "www.fake.com" };

            _artistRepository.AddArtist(artistToAdd);

            var result = _artistRepository.FetchAllArtists();

            result.Count.Should().Be(9);
            result[8].Name.Should().Be("Amy");

        }

        [Test]
        public void ExistsById_ReturnsTrue()
        {
            var result = _artistRepository.ExistsById(1);

            result.Should().Be(true);

        }

        [Test]
        public void ExistsById_ReturnsFalse()
        {
            var result = _artistRepository.ExistsById(10);

            result.Should().Be(false);

        }


        [Test]
        public void FetchArtistById_ReturnsArtist()
        {
            var result = _artistRepository.FetchArtistById(1);

            result.Name.Should().Be("Adele");

        }


        [Test]
        public void DeleteArtistById_DeletesArtist()
        {

            var beforeResult = _artistRepository.ExistsById(1);

            beforeResult.Should().Be(true);

            _artistRepository.RemoveById(1);

            var afterResult = _artistRepository.ExistsById(1);

            afterResult.Should().Be(false);
        }


        [Test]
        public void UpdateArtistName_UpdatesArtist()
        {
            var beforeArtist = _artistRepository.FetchArtistById(2);

            beforeArtist.Name.Should().Be("Amy Winehouse");

            var updateArtist = new UpdateArtist()
            {
                Id = 2,
                Name = "Amy Jade Winehouse",
                ImageUrl = "www.fake.com"
            };

            var afterArtist = _artistRepository.UpdateArtistByName(updateArtist);

            afterArtist.Name.Should().Be("Amy Jade Winehouse");

        }




    }
}
