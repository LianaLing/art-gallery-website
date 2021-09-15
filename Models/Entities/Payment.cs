using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    //CREATE TABLE [dbo].[Payment] (
    //    [id]                INT          IDENTITY (1, 1) NOT NULL,
    //    [amount]            MONEY        NOT NULL,
    //    [currency]          CHAR (3)     NOT NULL,
    //    [description]       TEXT         NULL,
    //    [status]            VARCHAR (10) NOT NULL,
    //    [tax]               SMALLMONEY   NOT NULL,
    //    [canceled_at]       DATE         NULL,
    //    [succeeded_at]      DATE         NULL,
    //    [created_at]        DATE         NOT NULL,
    //    [updated_at]        DATE         NOT NULL,
    //    [payment_method_id] INT          NULL,
    //    PRIMARY KEY CLUSTERED ([id] ASC),
    //    FOREIGN KEY ([payment_method_id]) REFERENCES [dbo].[PaymentMethod] ([id]),
    //    CHECK ([status]='succeeded' OR [status]='processing' OR [status]='canceled')
    //);

    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Column("canceled_at")]
        public DateTime? CanceledAt { get; set; }

        [Column("succeeded_at")]
        public DateTime? SucceededAt { get; set; }

        [Required, Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Required, Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("payment_method_id")]
        public int? PaymentMethodId { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
