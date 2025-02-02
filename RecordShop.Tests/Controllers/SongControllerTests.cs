using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordShop.Controllers;
using RecordShop.Entities;
using RecordShop.Services;
using RecordShop.UserInputObjects;

namespace RecordShop.Tests.Controllers
{

    public class SongControllerTests
    {

        private Mock<ISongService> _songServiceMock; 

        private SongController _songController;


        [SetUp]
        public void Setup()
        {
            _songServiceMock = new Mock<ISongService>();
            _songController = new SongController(_songServiceMock.Object);   
        }


        [Test]
        public void GetSongs_ReturnsSongs()
        {
            List<Song> songList = new List<Song>() {

                new Song (){
                 Name = "Back to Black",
                 ArtistId = 1,
                 Description = "Song of the Decade",
                 ReleaseDate = new DateTime(2005, 04, 12)
                }
            };

            _songServiceMock.Setup(song => song.GetSongs("",  "", null)).Returns(songList);

            var result = (OkObjectResult) _songController.GetSongs("",  "", null);

            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(songList);

        }

        [Test]
        public void PostSong_ReturnsCreated()
        {
            var songToAdd = new SongDTO(){
                 Name = "Back to Black",
                 ArtistId = 1,
                 Description = "Song of the Decade",
                 ReleaseDate = new DateTime(2005, 04, 12)
                };

            var result = (CreatedResult)_songController.AddSong(songToAdd);

            result.StatusCode.Should().Be(201);

        }


        [Test]
        public void PostSong_CallsServiceMethodOnce()
        {
            var albumToAdd = new SongDTO(){
                Name = "Back to Black",
                ArtistId = 1,
                Description = "Song of the Decade",
                ReleaseDate = new DateTime(2005, 04, 12)
            };

            var result = (CreatedResult)_songController.AddSong(albumToAdd);

            _songServiceMock.Verify(a => a.AddSong(It.Is<Song>(a => a.Name == "Back to Black")), Times.Once());
        }


    }
}