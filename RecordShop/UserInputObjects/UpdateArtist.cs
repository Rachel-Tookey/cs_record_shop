﻿using System.ComponentModel.DataAnnotations;

namespace RecordShop.UserInputObjects
{
    public class UpdateArtist
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Length(4, int.MaxValue)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int YearsActive { get; set; }

    }
}
