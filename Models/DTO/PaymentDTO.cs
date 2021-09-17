using System;

namespace ArtGalleryWebsite.Models.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public decimal Tax { get; set; }

        public DateTime? CanceledAt { get; set; }

        public DateTime? SucceededAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int? PaymentMethodId { get; set; }

        public PaymentMethodDTO PaymentMethod { get; set; }
    }
}