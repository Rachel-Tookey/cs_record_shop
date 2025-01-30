using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using RecordShop.Controllers;
using RecordShop.Data;
using RecordShop.Repository;
using RecordShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordShop.Entities;
using FluentAssertions;

namespace RecordShop.Tests.Repository
{
    public class AlbumRepositoryTests
    {

        private AlbumRepository _albumRepository;


        [SetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<RecordShopContextSqlite>().UseSqlite(connection).Options;
            RecordShopContextSqlite context = new RecordShopContextSqlite(options);

            context.Artists.Add(new Artist { Name = "Amy", ImageUrl = "www.fake.com" });

            context.Albums.Add(new Album { Name = "Amy The Real Me", ArtistId = 1, Description = "Yo", ReleaseDate = new DateTime(2024, 01, 02) }); 

            context.SaveChanges();  

            _albumRepository = new AlbumRepository(context);
        }


        [Test]
        public void GetAllAlbums_ReturnsList()
        {
            var result = _albumRepository.FetchAlbums();

            result.Count.Should().Be(3);
            result[2].Name.Should().Be("Amy The Real Me");

        }


        [Test]
        public void AddAlbum_AddsAlbumToDb()
        {
            var albumToAdd = new Album { Name = "Amy The Real Me 2", ArtistId = 1, Description = "Yo Yo", ReleaseDate = new DateTime(2024, 02, 02) };

            _albumRepository.AddAlbum(albumToAdd);

            var result = _albumRepository.FetchAlbums();

            result.Count.Should().Be(4);
            result[3].Name.Should().Be("Amy The Real Me 2");

        }

        [Test]
        public void AddAlbum_ForeignKeyError()
        {
            var albumToAdd = new Album { Name = "Amy The Real Me 2", ArtistId = 20, Description = "Yo Yo", ReleaseDate = new DateTime(2024, 02, 02) };

            Action addAlbumAction = () => _albumRepository.AddAlbum(albumToAdd);
            addAlbumAction.Should().Throw<Microsoft.EntityFrameworkCore.DbUpdateException>();

        }



    }
}
