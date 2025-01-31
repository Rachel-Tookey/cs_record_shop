using Moq;
using RecordShop.Entities;
using RecordShop.Services;
using FluentAssertions;
using RecordShop.Repository;

namespace RecordShop.Tests.Services
{
    public class SongServiceTests
    {

        private Mock<ISongRepository> _albumRepositoryMock;

        private SongService _songService;


        [SetUp]
        public void Setup()
        {
            _albumRepositoryMock = new Mock<ISongRepository>();
            _songService = new SongService(_albumRepositoryMock.Object);
        }


        [Test]
        public void GetSongs_ReturnsSongs()
        {
            List<Song> songList = new List<Song>() {
                        new Song("Back to Black", 
                        1, "Song of the Decade", 
                        new DateTime(2005, 04, 12), 
                        "www.spotifylink.com")
            };

            _albumRepositoryMock.Setup(album => album.FetchSongs()).Returns(songList);
            var result = _songService.GetSongs();
            result.Should().BeEquivalentTo(songList);

        }


        [Test]
        public void PostSong_CallsServiceMethodOnce()
        {
            var songToAdd = new Song("Back to Black", 1, "Song of the Decade", new DateTime(2005, 04, 12), "www.spotifylink.com");
            _songService.AddSong(songToAdd);
            _albumRepositoryMock.Verify(a => a.AddSong(songToAdd), Times.Once());
        }

    }

    }
