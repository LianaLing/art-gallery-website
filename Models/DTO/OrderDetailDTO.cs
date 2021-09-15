using ArtGalleryWebsite.Models.Entities;
using System;
using System.Collections.Generic;

namespace ArtGalleryWebsite.Models.DTO
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<ArtDetailDTO> Arts { get; set; }

        public int? PaymentId { get; set; }

        public int? UserId { get; set; }

        public int? AddressId { get; set; }

        public OrderDetailDTOAddress Address { get; set; }

        public class OrderDetailDTOAddress
        {
            public int Id { get; set; }

            public string Country { get; set; }

            public string State { get; set; }

            public string City { get; set; }

            public string Line1 { get; set; }

            public string Line2 { get; set; }

            public string PostalCode { get; set; }

            public DateTime CreatedAt { get; set; }

            public DateTime UpdatedAt { get; set; }
        }
    }
}