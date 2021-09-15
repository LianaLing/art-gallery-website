
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[OrderArt] (
    //    [art_id]   INT NOT NULL,
    //    [order_id] INT NOT NULL,
    //    PRIMARY KEY CLUSTERED ([art_id] ASC, [order_id] ASC),
    //    FOREIGN KEY ([order_id]) REFERENCES [dbo].[Order] ([id]),
    //    FOREIGN KEY ([art_id]) REFERENCES [dbo].[Art] ([id])
    //);

    [Table("OrderArt")]
    public class OrderArt
    {
        [Key]
        [Column("art_id", Order = 1)]
        public int ArtId { get; set; }

        [Key]
        [Column("order_id", Order = 2)]
        public int OrderId { get; set; }
    }
}
