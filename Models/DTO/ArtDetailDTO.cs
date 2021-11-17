using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.DTO
{
    public class ArtDetailDTO
    {
        public int Id { get; set; }
        public string Style { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Likes { get; set; }
        public string Url { get; set; }

        public ArtDetailDTOAuthor Author { get; set; }

        public class ArtDetailDTOAuthor
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool Verified { get; set; }
            public string Username { get; set; }
            public string Name { get; set; }
            public string Ic { get; set; }
            public DateTime? Dob { get; set; }
            public string ContactNo { get; set; }
            public string Email { get; set; }
            public string AvatarUrl { get; set; }
        }
    }
}