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

namespace RecordShop.Tests.Controllers
{
    public class ArtistControllerTests
    {
        private Mock<IArtistService> _artistServiceMock;

        private ArtistController _artistController;


        [SetUp]
        public void Setup()
        {
            _artistServiceMock = new Mock<IArtistService>();
            _artistController = new ArtistController(_artistServiceMock.Object);
        }


        [Test]
        public void GetArtists_ReturnsArtists()
        {
            List<Artist> artistList = new List<Artist>() {

                new Artist (){
                 Name = "Amy Winehouse"
                }
            };

            _artistServiceMock.Setup(artist => artist.GetAllArtists()).Returns(artistList);

            var result = (OkObjectResult)_artistController.GetArtists();

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(artistList);

        }

        [Test]
        public void PostArtist_ReturnsCreated()
        {
            var artistToAdd = new Artist()
            {
                Name = "Amy Winehouse"
            };


            var result = (CreatedResult)_artistController.AddArtist(artistToAdd);

            result.StatusCode.Should().Be(201);

        }


        [Test]
        public void PostInvalidArtist_ReturnsBadRequest()
        {
            var artistToAdd = new Artist()
            {
                Name = "Amy Winehouse" 
            };

            _artistController.ModelState.AddModelError("Test Error", "This is an error"); 

            var result = (BadRequestObjectResult)_artistController.AddArtist(artistToAdd);

            result.StatusCode.Should().Be(400);

        }


        [Test]
        public void PostArtist_CallsServiceMethodOnce()
        {
            var artistToAdd = new Artist()
            {
                Name = "Amy Winehouse"
            };


            var result = (CreatedResult)_artistController.AddArtist(artistToAdd);

            _artistServiceMock.Verify(a => a.AddArtist(artistToAdd), Times.Once());
        }


        [Test]
        public void GetArtistById_ReturnsOk()
        {
            var artistToReturn = new Artist()
            {
                Id = 1, 
                Name = "Amy Winehouse"
            };

            _artistServiceMock.Setup(a => a.ExistsById(1)).Returns(true);
            _artistServiceMock.Setup(a => a.GetArtistById(1)).Returns(artistToReturn);


            var result = (OkObjectResult)_artistController.GetArtistById(1);

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(artistToReturn);

        }


        [Test]
        public void GetArtistById_ReturnsBadRequest()
        {
            

            _artistServiceMock.Setup(a => a.ExistsById(1)).Returns(false);

            var result = (BadRequestObjectResult)_artistController.GetArtistById(1);

            result.StatusCode.Should().Be(400);

        }

    }
}
