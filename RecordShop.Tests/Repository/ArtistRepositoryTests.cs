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
            var result = _artistRepository.FetchAllArtists();

            result.Count.Should().Be(4);
            result[0].Name.Should().Be("Adele");

        }


        [Test]
        public void AddArtist_AddsArtist()
        {
            var artistToAdd = new Artist { Name = "Amy" };

            _artistRepository.AddArtist(artistToAdd);

            var result = _artistRepository.FetchAllArtists();

            result.Count.Should().Be(5);
            result[4].Name.Should().Be("Amy");

        }

        [Test]
        public void ExistsById_ReturnsTrue()
        {
            var result = _artistRepository.CheckArtistExistsById(1);

            result.Should().Be(true);

        }

        [Test]
        public void ExistsById_ReturnsFalse()
        {
            var result = _artistRepository.CheckArtistExistsById(10);

            result.Should().Be(false);

        }


        [Test]
        public void FetchArtistById_ReturnsArtist()
        {
            var result = _artistRepository.FetchArtistById(1);

            result.Name.Should().Be("Adele");

        }


    }
}
