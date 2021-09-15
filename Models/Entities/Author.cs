using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    // CREATE TABLE [dbo].[Author] (
    //     [id]          INT  IDENTITY (1, 1) NOT NULL,
    //     [description] TEXT NULL,
    //     [verified]    BIT  DEFAULT ((0)) NOT NULL,
    //     PRIMARY KEY CLUSTERED ([id] ASC)
    // );

    [Table("Author")]
    public class Author
    {
        [Key, ScaffoldColumn(true)]
        public int Id { get; set; }

        [StringLength(10000), Display(Name = "Author Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public bool Verified { get; set; }

        public virtual ICollection<Art> Arts { get; set; }

        override
        public string ToString()
        {
            return $"id: {Id}\ndescription: {Description}\nverified: {Verified}";
        }
    }
}