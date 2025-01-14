using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordShop.Controllers;
using RecordShop.Entities;
using RecordShop.Services;

namespace RecordShop.Tests.Controllers
{

    public class AlbumControllerTests
    {

        private Mock<IAlbumService> _albumServiceMock; 

        private AlbumController _albumController;


        [SetUp]
        public void Setup()
        {
            _albumServiceMock = new Mock<IAlbumService>();
            _albumController = new AlbumController(_albumServiceMock.Object);   
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

            _albumServiceMock.Setup(album => album.GetAllAlbums()).Returns(albumList);

            var result = (OkObjectResult) _albumController.GetAlbums();

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(albumList);

        }

        [Test]
        public void PostAlbum_ReturnsCreated()
        {
            var albumToAdd = new DTO.AlbumDTO (){
                 Name = "Back to Black",
                 ArtistId = 1,
                 Description = "Album of the Decade",
                 ReleaseDate = new DateTime(2005, 04, 12)
                };


            var result = (CreatedResult)_albumController.AddAlbum(albumToAdd);

            result.StatusCode.Should().Be(201);

        }


        // fix this: 


        [Test]
        public void PostAlbum_CallsServiceMethodOnce()
        {
            var albumToAdd = new DTO.AlbumDTO()
            {
                Name = "Back to Black",
                ArtistId = 1,
                Description = "Album of the Decade",
                ReleaseDate = new DateTime(2005, 04, 12)
            };



            var result = (CreatedResult)_albumController.AddAlbum(albumToAdd);

            _albumServiceMock.Verify(a => a.AddAlbum(It.Is<Album>(a => a.Name == "Back to Black")), Times.Once());
        }


    }
}