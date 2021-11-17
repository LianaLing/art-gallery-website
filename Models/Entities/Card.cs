using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[Card] (
    //    [id]         INT           IDENTITY (1, 1) NOT NULL,
    //    [brand]      VARCHAR (10)  NOT NULL,
    //    [name]       VARCHAR (255) NOT NULL,
    //    [exp_month]  VARCHAR (2)   NOT NULL,
    //    [exp_year]   CHAR (4)      NOT NULL,
    //    [last4]      CHAR (4)      NOT NULL,
    //    [created_at] DATE          NOT NULL,
    //    [updated_at] DATE          NOT NULL,
    //    PRIMARY KEY CLUSTERED ([id] ASC),
    //    CHECK ([brand]='unknown' OR [brand]='mastercard' OR [brand]='unionpay' OR [brand]='amex' OR [brand]='visa')
    //);


    [Table("Card")]
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Column("exp_month")]
        public string ExpMonth { get; set; }

        [Required, Column("exp_year")]
        public string ExpYear { get; set; }

        [Required]
        public string Last4 { get; set; }

        [Required, Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Required, Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
