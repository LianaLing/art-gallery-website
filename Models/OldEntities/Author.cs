using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryWebsite.Models.OldEntities
{
    // CREATE TABLE [dbo].[Author] (
    //     [id]          INT  IDENTITY (1, 1) NOT NULL,
    //     [description] TEXT NULL,
    //     [verified]    BIT  DEFAULT ((0)) NOT NULL,
    //     PRIMARY KEY CLUSTERED ([id] ASC)
    // );

    //public class Author
    //{
    //    [ScaffoldColumn(true)]
    //    public int AuthorID { get; set; }

    //    [StringLength(10000), Display(Name = "Author Description"), DataType(DataType.MultilineText)]
    //    public string Description { get; set; }

    //    public bool Verified { get; set; }

    //    public virtual ICollection<Art> Arts { get; set; }
    //}

    public class Author : ISqlParser
    {
        public int id
        {
            get;
            set;
        }

        public string description
        {
            get;
            set;
        }

        public bool verified
        {
            get;
            set;
        }

        public Author() { }

        public Author(int id, string description, bool verified)
        {
            this.id = id;
            this.description = description;
            this.verified = verified;
        }

        public ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            return new Author(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetBoolean(2)
            );
        }

        override
        public string ToString()
        {
            return $"id: {id}\ndescription: {description}\nverified: {verified}";
        }
    }
}