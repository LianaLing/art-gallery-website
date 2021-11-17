using System;

namespace ArtGalleryWebsite.Models.DTO
{
    public class BillingDetailsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int AddressId { get; set; }

        public AddressDTO Address { get; set; }
    }
}