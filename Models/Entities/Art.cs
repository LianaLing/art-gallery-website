using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryWebsite.Models.Entities
{
    // CREATE TABLE [dbo].[Art] (
    //     [id]          INT          IDENTITY (1, 1) NOT NULL,
    //     [style]       VARCHAR (50) NOT NULL,
    //     [description] TEXT         NULL,
    //     [price]       MONEY        NOT NULL,
    //     [stock]       INT          DEFAULT ((1)) NOT NULL,
    //     [likes]       INT          DEFAULT ((0)) NOT NULL,
    //     [url]         TEXT         NOT NULL,
    //     [author_id]   INT          NULL,
    //     PRIMARY KEY CLUSTERED ([id] ASC),
    //     FOREIGN KEY ([author_id]) REFERENCES [dbo].[Author] ([id])
    // );

    [Table("Art")]
    public class Art
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50), Display(Name = "Style")]
        public string Style { get; set; }

        [StringLength(10000), Display(Name = "Art Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required, Display(Name = "Price"), DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Display(Name = "Likes")]
        public int Likes { get; set; }

        [Required, DataType(DataType.ImageUrl)]
        public string Url { get; set; }

        [Column("author_id")]
        public int? AuthorID { get; set; }

        public virtual Author Author { get; set; }

        override
        public string ToString()
        {
            return $"id: {Id}\nstyle: {Style}\ndescription: {Description}\nprice: {Price}\nstock: {Stock}\nlikes: {Likes}\nurl: {Url}\nauthor_id: {AuthorID}\nauthor: {Author}";
        }
    }

    //public class Art : ISqlParser
    //{
    //    public int id
    //    {
    //        get;
    //        set;
    //    }

    //    public string style
    //    {
    //        get;
    //        set;
    //    }

    //    public string description
    //    {
    //        get;
    //        set;
    //    }

    //    public decimal price
    //    {
    //        get;
    //        set;
    //    }

    //    public int stock
    //    {
    //        get;
    //        set;
    //    }

    //    public int likes
    //    {
    //        get;
    //        set;
    //    }

    //    public string url
    //    {
    //        get;
    //        set;
    //    }

    //    public int author_id
    //    {
    //        get;
    //        set;
    //    }

    //    public Art() { }

    //    public Art(int id, string style, string description, decimal price, int stock, int likes, string url)
    //    {
    //        this.id = id;
    //        this.style = style;
    //        this.description = description;
    //        this.price = price;
    //        this.stock = stock;
    //        this.likes = likes;
    //        this.url = url;
    //    }

    //    public Art(int id, string style, string description, decimal price, int stock, int likes, string url, int author_id)
    //    {
    //        this.id = id;
    //        this.style = style;
    //        this.description = description;
    //        this.price = price;
    //        this.stock = stock;
    //        this.likes = likes;
    //        this.url = url;
    //        this.author_id = author_id;
    //    }

    //    public virtual ISqlParser ParseFromSqlReader(SqlDataReader reader)
    //    {
    //        return new Art(
    //            reader.GetInt32(0),
    //            reader.GetString(1),
    //            reader.GetString(2),
    //            reader.GetDecimal(3),
    //            reader.GetInt32(4),
    //            reader.GetInt32(5),
    //            reader.GetString(6),
    //            reader.GetInt32(7)
    //        );
    //    }

    //    override
    //    public string ToString()
    //    {
    //        return $"id: {id}\nstyle: {style}\ndescription: {description}\nprice: {price}\nstock: {stock}\nlikes: {likes}\nurl: {url}\nauthor_id: {author_id}";
    //    }
    //}
}