using System;

namespace ArtGalleryWebsite.Models.DTO
{
    public class PaymenMethodDTO
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int? UserId { get; set; }

        public int? CardId { get; set; }

        public int? BillingDetailsId { get; set; }

        public CardDTO Card { get; set; }

        public BillingDetailsDTO BillingDetails { get; set; }
    }
}