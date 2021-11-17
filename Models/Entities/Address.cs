using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[Address] (
    //    [id]          INT           IDENTITY (1, 1) NOT NULL,
    //    [country]     VARCHAR (255) NOT NULL,
    //    [state]       VARCHAR (255) NOT NULL,
    //    [city]        VARCHAR (255) NOT NULL,
    //    [line1]       VARCHAR (255) NOT NULL,
    //    [line2]       VARCHAR (255) NULL,
    //    [postal_code] CHAR (5)      NOT NULL,
    //    [created_at]  DATE          NOT NULL,
    //    [updated_at]  DATE          NOT NULL,
    //    PRIMARY KEY CLUSTERED ([id] ASC)
    //);

    [Table("Address")]
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Line1 { get; set; }

        public string Line2 { get; set; }

        [Required, Column("postal_code")]
        public string PostalCode { get; set; }

        [Required, Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Required, Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
