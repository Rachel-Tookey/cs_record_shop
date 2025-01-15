using System.ComponentModel.DataAnnotations;

namespace RecordShop.Wrappers
{
    public class UpdateArtistWrapper
    {
        [Required]
        public int Id { get; set; }

        [LengthAttribute(4, Int32.MaxValue)]
        public string Name { get; set; }

    }
}
