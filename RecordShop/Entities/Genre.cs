﻿namespace RecordShop.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Album> Albums { get; set; }
    }
}
