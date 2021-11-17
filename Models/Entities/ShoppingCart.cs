using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[ShoppingCart] (
    //    [Id]     INT IDENTITY (1, 1) NOT NULL,
    //    [Total]  INT DEFAULT ((0)) NOT NULL,
    //    [UserId] INT NOT NULL,
    //    PRIMARY KEY CLUSTERED ([Id] ASC),
    //    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
    //);

    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public int Total { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual Identity.User User { get; set; }
    }
}
