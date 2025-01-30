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

                new Song (){
                 Name = "Back to Black",
                 ArtistId = 1,
                 Description = "Album of the Decade",
                 ReleaseDate = new DateTime(2005, 04, 12)
                }
            };

            _albumRepositoryMock.Setup(album => album.FetchSongs()).Returns(songList);

            var result = _songService.GetSongs();

            result.Should().BeEquivalentTo(songList);

        }


        [Test]
        public void PostSong_CallsServiceMethodOnce()
        {
            var songToAdd = new Song()
            {
                Name = "Back to Black",
                ArtistId = 1,
                Description = "Album of the Decade",
                ReleaseDate = new DateTime(2005, 04, 12)
            };


            _songService.AddSong(songToAdd);

            _albumRepositoryMock.Verify(a => a.AddSong(songToAdd), Times.Once());
        }

    }

    }
