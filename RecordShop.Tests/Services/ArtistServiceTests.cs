using Moq;
using RecordShop.Entities;
using RecordShop.Repository;
using RecordShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions; 

namespace RecordShop.Tests.Services
{
    public class ArtistServiceTests
    {

        private Mock<IArtistRepository> _artistRepositoryMock;

        private ArtistService _artistService;


        [SetUp]
        public void Setup()
        {
            _artistRepositoryMock = new Mock<IArtistRepository>();
            _artistService = new ArtistService(_artistRepositoryMock.Object);
        }


        [Test]
        public void GetAlbums_ReturnsAlbums()
        {
            List<Artist> artistList = new List<Artist>() {

                new Artist (){
                 Name = "Amy Winehouse"
                }
            };

            _artistRepositoryMock.Setup(a => a.GetAllArtists()).Returns(artistList);

            var result = _artistService.GetAllArtists();

            result.Should().BeEquivalentTo(artistList);

        }


        [Test]
        public void PostAlbum_CallsServiceMethodOnce()
        {
            var artistToAdd = new Artist()
            {
                Name = "Amy Winehouse"
            };


            _artistService.AddArtist(artistToAdd);

            _artistRepositoryMock.Verify(a => a.AddArtist(artistToAdd), Times.Once());
        }

    }

}
