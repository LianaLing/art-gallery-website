using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[Order] (
    //    [id]         INT          IDENTITY (1, 1) NOT NULL,
    //    [status]     VARCHAR (10) NOT NULL,
    //    [remark]     TEXT         NULL,
    //    [payment_id] INT          NULL,
    //    [user_id]    INT          NULL,
    //    [address_id] INT          NULL,
    //    [created_at] DATETIME     NOT NULL, 
    //    [updated_at] DATETIME     NOT NULL, 
    //    PRIMARY KEY CLUSTERED ([id] ASC),
    //    FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([Id]),
    //    FOREIGN KEY ([payment_id]) REFERENCES [dbo].[Payment] ([id]),
    //    FOREIGN KEY ([address_id]) REFERENCES [dbo].[Address] ([id]),
    //    CHECK ([status] = 'succeeded'
    //           OR [status] = 'processing'
    //           OR [status] = 'canceled')
    //);



    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }

        public string Remark { get; set; }

        [Required, Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Required, Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("payment_id")]
        public int? PaymentId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        public virtual Identity.User User { get; set; }

        [Column("address_id")]
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }
    }
}
