using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[Favourite] (
    //    [id]      INT          IDENTITY (1, 1) NOT NULL,
    //    [name]    VARCHAR (50) NULL,
    //    [user_id] INT          NOT NULL,
    //    PRIMARY KEY CLUSTERED ([id] ASC),
    //    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([Id])
    //);

    [Table("Favourite")]
    public class Favourite
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
    }
}
