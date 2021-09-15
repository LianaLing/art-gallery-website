using ArtGalleryWebsite.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.DTO
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ArtId { get; set; }
        public ArtDetailDTO Art { get; set; }
        public int CartId { get; set; }
        public ShoppingCart Cart { get; set; }
    }
}
