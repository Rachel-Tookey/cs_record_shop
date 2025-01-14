using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordShop.Controllers;
using RecordShop.Entities;
using RecordShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using RecordShop.Repository;

namespace RecordShop.Tests.Services
{
    public class AlbumServiceTests
    {

        private Mock<IAlbumRepository> _albumRepositoryMock;

        private AlbumService _albumService;


        [SetUp]
        public void Setup()
        {
            _albumRepositoryMock = new Mock<IAlbumRepository>();
            _albumService = new AlbumService(_albumRepositoryMock.Object);
        }


        [Test]
        public void GetAlbums_ReturnsAlbums()
        {
            List<Album> albumList = new List<Album>() {

                new Album (){
                 Name = "Back to Black",
                 ArtistId = 1,
                 Description = "Album of the Decade",
                 ReleaseDate = new DateTime(2005, 04, 12)
                }
            };

            _albumRepositoryMock.Setup(album => album.GetAllAlbums()).Returns(albumList);

            var result = _albumService.GetAllAlbums();

            result.Should().BeEquivalentTo(albumList);

        }


        [Test]
        public void PostAlbum_CallsServiceMethodOnce()
        {
            var albumToAdd = new Album()
            {
                Name = "Back to Black",
                ArtistId = 1,
                Description = "Album of the Decade",
                ReleaseDate = new DateTime(2005, 04, 12)
            };


            _albumService.AddAlbum(albumToAdd);

            _albumRepositoryMock.Verify(a => a.AddAlbum(albumToAdd), Times.Once());
        }

    }

    }
