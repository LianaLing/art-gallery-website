using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[Cart] (
    //    [Id]     INT IDENTITY (1, 1) NOT NULL,
    //    [Total]  INT DEFAULT ((0)) NOT NULL,
    //    [UserId] INT NOT NULL,
    //    PRIMARY KEY CLUSTERED ([Id] ASC),
    //    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
    //);

    [Table("CartItem")]
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        [Required]
        public int ArtId { get; set; }

        public virtual Art Art { get; set; }

        [Required]
        public int CartId { get; set; }

        public virtual ShoppingCart Cart { get; set; }
    }
}
