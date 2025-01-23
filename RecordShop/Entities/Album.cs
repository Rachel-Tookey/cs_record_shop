using System.ComponentModel.DataAnnotations; 

namespace RecordShop.Entities
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<Genre> Genres { get; set; }


    }
}
