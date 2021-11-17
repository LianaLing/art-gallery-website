using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[FavArt] (
    //    [fav_id] INT NOT NULL,
    //    [art_id] INT NOT NULL,
    //    PRIMARY KEY CLUSTERED ([fav_id] ASC, [art_id] ASC),
    //    FOREIGN KEY ([fav_id]) REFERENCES [dbo].[Favourite] ([id]),
    //    FOREIGN KEY ([art_id]) REFERENCES [dbo].[Art] ([id])
    //);

    [Table("FavArt")]
    public class FavArt
    {
        [Key]
        [Column("fav_id", Order = 1)]
        public int FavId { get; set; }

        [Key]
        [Column("art_id", Order = 2)]
        public int ArtId { get; set; }
    }
}
