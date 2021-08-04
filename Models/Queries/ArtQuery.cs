using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Queries
{
    public class ArtQuery : ISqlParser
    {
        public static string SqlQuery = @"
            SELECT [Art].id, [Art].style, [Art].description, [Art].price, [Art].stock, [Art].likes, [Art].url,
                   [Author].id, [Author].description, [Author].verified, [User].username, [User].name, [User].ic, [User].dob, [User].contact_no, [User].email, [User].avatar_url
            FROM [Art], [Author], [User]
            WHERE [Art].author_id = [Author].id
            AND [Author].id = [User].author_id
            ORDER BY [Art].likes DESC;
        ";

        public int id;
        public string style;
        public string description;
        public decimal price;
        public int stock;
        public int likes;
        public string url;
        public Author author;

        public class Author
        {
            public int id;
            public string description;
            public bool verified;
            public string username;
            public string name;
            public string ic;
            public Nullable<DateTime> dob;
            public string contactNo;
            public string email;
            public string avatarUrl;

            public Author(int id, string description, bool verified)
            {                
                this.id = id;
                this.description = description;
                this.verified = verified;
            }


            public Author(int id, string description, bool verified, string username, string name, string ic, DateTime? dob, string contactNo, string email, string avatarUrl)
            {
                this.id = id;
                this.description = description;
                this.verified = verified;
                this.username = username;
                this.name = name;
                this.ic = ic;
                this.dob = dob;
                this.contactNo = contactNo;
                this.email = email;
                this.avatarUrl = avatarUrl;
            }
        }

        public ArtQuery() {
    }

        public ArtQuery(int id, string style, string description, decimal price, int stock, int likes, string url, Author author)
        {
            this.id = id;
            this.style = style;
            this.description = description;
            this.price = price;
            this.stock = stock;
            this.likes = likes;
            this.url = url;
            this.author = author;
        }
        
        public static void FetchAllArt()
        {
            SqlQuery = @"
            SELECT [Art].id, [Art].style, [Art].description, [Art].price, [Art].stock, [Art].likes, [Art].url,
                   [Author].id, [Author].description, [Author].verified, [User].username, [User].name, [User].ic, [User].dob, [User].contact_no, [User].email, [User].avatar_url
            FROM [Art], [Author], [User]
            WHERE [Art].author_id = [Author].id
            AND [Author].id = [User].author_id
            ORDER BY [Art].likes DESC;
        ";
        }

        public static void FetchCurrentArtDetail(int id)
        {
            SqlQuery = @"
            SELECT [Art].id, [Art].style, [Art].description, [Art].price, [Art].stock, [Art].likes, [Art].url,
                   [Author].id, [Author].description, [Author].verified, [User].username, [User].name, [User].ic, [User].dob, [User].contact_no, [User].email, [User].avatar_url
            FROM [Art], [Author], [User]
            WHERE [Art].author_id = [Author].id
            AND [Author].id = [User].author_id
            AND [Art].id = '" + id + "'";
        }

        public ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            return new ArtQuery(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetDecimal(3),
                reader.GetInt32(4),
                reader.GetInt32(5),
                reader.GetString(6),
                new Author(
                    reader.GetInt32(7),
                    reader.GetString(8),
                    reader.GetBoolean(9),
                    reader.GetString(10),
                    reader.GetString(11),
                    reader.GetString(12),
                    reader.GetDateTime(13),
                    reader.GetString(14),
                    reader.GetString(15),
                    reader.GetString(16)
                )
            );
        }
    }
}