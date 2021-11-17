using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[PaymentMethod] (
    //    [id]                 INT          IDENTITY (1, 1) NOT NULL,
    //    [type]               VARCHAR (10) NOT NULL,
    //    [created_at]         DATE         NOT NULL,
    //    [updated_at]         DATE         NOT NULL,
    //    [user_id]            INT          NULL,
    //    [card_id]            INT          NULL,
    //    [billing_details_id] INT          NULL,
    //    PRIMARY KEY CLUSTERED ([id] ASC),
    //    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([Id]),
    //    FOREIGN KEY ([card_id]) REFERENCES [dbo].[Card] ([id]),
    //    FOREIGN KEY ([billing_details_id]) REFERENCES [dbo].[BillingDetails] ([id]),
    //    CHECK ([type]='tng' OR [type]='grabpay' OR [type]='card' OR [type]='fpx')
    //);

    [Table("PaymentMethod")]
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required, Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Required, Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        public virtual Identity.User user { get; set; }

        [Column("card_id")]
        public int? CardId { get; set; }

        public virtual Card Card { get; set; }

        [Column("billing_details_id")]
        public int? BillingDetailsId { get; set; }

        public virtual BillingDetails BillingDetails { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
