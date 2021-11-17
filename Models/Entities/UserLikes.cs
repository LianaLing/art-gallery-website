using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[UserLikes] (
    //    [user_id] INT NOT NULL,
    //    [art_id] INT NOT NULL,
    //    PRIMARY KEY CLUSTERED ([user_id] ASC, [art_id] ASC),
    //    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id]),
    //    FOREIGN KEY ([art_id]) REFERENCES [dbo].[Art] ([id])
    //);

    [Table("UserLikes")]
    public class UserLikes
    {
        [Key]
        [Column("user_id", Order = 1)]
        public int UserId { get; set; }

        [Key]
        [Column("art_id", Order = 2)]
        public int ArtId { get; set; }
    }
}
