using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
    public class Art : ISqlParser
    {
        public int id;
        public string style;
        public string description;
        public decimal price;
        public int stock;
        public int likes;
        public string url;
        public int author_id;

        public Art() { }

        public Art(int id, string style, string description, decimal price, int stock, int likes, string url, int author_id)
        {
            this.id = id;
            this.style = style;
            this.description = description;
            this.price = price;
            this.stock = stock;
            this.likes = likes;
            this.url = url;
            this.author_id = author_id;
        }

        public ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            return new Art(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetDecimal(3),
                reader.GetInt32(4),
                reader.GetInt32(5),
                reader.GetString(6),
                reader.GetInt32(7)
            );
        }

        override
        public string ToString()
        {
            return $"id: {id}\nstyle: {style}\ndescription: {description}\nprice: {price}\nstock: {stock}\nlikes: {likes}\nurl: {url}\nauthor_id: {author_id}";
        }
    }
}