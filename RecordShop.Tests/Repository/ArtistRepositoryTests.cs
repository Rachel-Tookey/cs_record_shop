using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordShop.Data;
using RecordShop.Entities;
using RecordShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var options = new DbContextOptionsBuilder<RecordShopContext>().UseSqlite(connection).Options;
            RecordShopContext context = new RecordShopContext(options);

            _artistRepository = new ArtistRepository(context);
        }


        [Test]
        public void GetAllArtists_ReturnsList()
        {
            var result = _artistRepository.GetAllArtists();

            result.Count.Should().Be(4);
            result[0].Name.Should().Be("Adele");

        }


        [Test]
        public void AddArtist_AddsArtistToDb()
        {
            var artistToAdd = new Artist { Name = "Amy" };

            _artistRepository.AddArtist(artistToAdd);

            var result = _artistRepository.GetAllArtists();

            result.Count.Should().Be(5);
            result[4].Name.Should().Be("Amy");

        }

    }
}
