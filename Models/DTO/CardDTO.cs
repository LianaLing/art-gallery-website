using System;

namespace ArtGalleryWebsite.Models.DTO
{
    public class CardDTO
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Name { get; set; }

        public string ExpMonth { get; set; }

        public string ExpYear { get; set; }

        public string Last4 { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}