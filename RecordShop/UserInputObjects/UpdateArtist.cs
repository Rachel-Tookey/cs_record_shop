using System.ComponentModel.DataAnnotations;

namespace RecordShop.UserInputObjects
{
    public class UpdateArtist
    {
        [Required]
        public int Id { get; set; }

        [Length(4, int.MaxValue)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int YearsActive { get; set; }

    }
}
