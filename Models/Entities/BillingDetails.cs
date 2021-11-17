using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[BillingDetails] (
    //    [id]         INT           IDENTITY (1, 1) NOT NULL,
    //    [name]       VARCHAR (255) NOT NULL,
    //    [email]      VARCHAR (255) NOT NULL,
    //    [phone]      VARCHAR (20)  NULL,
    //    [created_at] DATE          NOT NULL,
    //    [updated_at] DATE          NOT NULL,
    //    [address_id] INT           NULL,
    //    PRIMARY KEY CLUSTERED ([id] ASC),
    //    FOREIGN KEY ([address_id]) REFERENCES [dbo].[Address] ([id])
    //);

    [Table("BillingDetails")]
    public class BillingDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required, Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Required, Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("address_id")]
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
