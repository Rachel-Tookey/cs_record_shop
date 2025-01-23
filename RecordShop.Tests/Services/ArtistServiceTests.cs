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
using RecordShop.DTO;
using Microsoft.AspNetCore.Mvc;
using RecordShop.Controllers;
using RecordShop.UserInputObjects;

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

            _artistRepositoryMock.Setup(a => a.FetchAllArtists()).Returns(artistList);

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


        [Test]
        public void GetArtistById_ReturnsArtist()
        {
            var artistToAdd = new Artist()
            {
                Id = 1, 
                Name = "Amy Winehouse"
            };

            _artistRepositoryMock.Setup(a => a.FetchArtistById(1)).Returns(artistToAdd);

            var result = _artistService.GetArtistById(1);

            result.Should().BeEquivalentTo(artistToAdd);

        }


        [Test]
        public void CheckArtistExistsById_ReturnsTrue()
        {
            
            _artistRepositoryMock.Setup(a => a.ExistsById(1)).Returns(true);

            var result = _artistService.ExistsById(1);

            result.Should().Be(true); 

        }

        [Test]
        public void CheckArtistExistsById_ReturnsFalse()
        {

            _artistRepositoryMock.Setup(a => a.ExistsById(1)).Returns(false);

            var result = _artistService.ExistsById(1);

            result.Should().Be(false);

        }

        [Test]
        public void DeleteById_CallsDeleteByIdOnce()
        {
            _artistService.DeleteById(1);

            _artistRepositoryMock.Verify(a => a.RemoveById(1), Times.Once);

        }

        [Test]
        public void UpdateArtist_CallsUpdateOnce()
        {
            var updateArtist = new UpdateArtist()
            {
                Id = 2,
                Name = "Amy Jade Winehouse"
            };


            _artistService.UpdateArtistByName(updateArtist);

            _artistRepositoryMock.Verify(a => a.UpdateArtistByName(updateArtist), Times.Once);

        }


        [Test]
        public void UpdateArtist_ReturnsUpdatedArtist()
        {
            var updateArtist = new UpdateArtist()
            {
                Id = 2,
                Name = "Amy Jade Winehouse"
            };

            var updatedArtist = new Artist()
            {
                Id = 2,
                Name = "Amy Jade Winehouse"
            };

            _artistRepositoryMock.Setup(a => a.UpdateArtistByName(updateArtist)).Returns(updatedArtist);

            var result = _artistService.UpdateArtistByName(updateArtist);


            result.Name.Should().Be("Amy Jade Winehouse"); 
        }



    }

}
