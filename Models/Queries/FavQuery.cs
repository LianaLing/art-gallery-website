using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ArtGalleryWebsite.Models.Entities;
using ArtGalleryWebsite.Utils;

namespace ArtGalleryWebsite.Models.Queries
{
    public class FavQuery : Favourite
    {

        public static string SqlQuery;
        public static int art_id;
        public static int fav_id;

        public Art art;
        public Entities.User user;
        public Author author;

        public FavQuery()
        {
        }

        public FavQuery(int id, string name) : base (id, name)
        {

        }

        public FavQuery(int id, string name, Art art, Author author) : base(id, name)
        {
            this.art = art;
            this.author = author;
        }

        public FavQuery(int id, string name, int user_id, Art art, Author author) : base(id, name, user_id)
        {
            this.art = art;
            this.author = author;
        }

        public static void FetchCurrentUser(int id)
        {
            SqlQuery = $@"
                SELECT 
                       [Favourite].id, [Favourite].name,
                       [Art].id, [Art].style, [Art].description, [Art].price, [Art].stock,  
                       [Art].likes, [Art].url,
                       [Author].id, [Author].description, [Author].verified 
                FROM [Art], [Author], [User], [Favourite], [FavArt]
                WHERE [Favourite].user_id = [User].Id
                AND [FavArt].art_id = [Art].id
                AND [FavArt].fav_id = [Favourite].id
                AND [Art].author_id = [Author].id
                AND [User].Id = {id} 
                ORDER BY [Favourite].name ASC;";
        }

        public static void FetchAllUserFavourites(int id)
        {
            SqlQuery = $@"
                SELECT 
                        [Favourite].id, [Favourite].name
                FROM [Favourite]
                WHERE [Favourite].user_id = {id}
                ORDER BY [Favourite].name ASC;
            ";
        }

        public static void InsertFavArt()
        {
            SqlQuery = $@"INSERT INTO [FavArt] VALUES ('{fav_id}','{art_id}')";
        }

        public override ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            if(reader.FieldCount > 2)
            {
                return new FavQuery(
                reader.GetInt32(0),
                reader.GetStringOrNull(1),
                new Art(
                    reader.GetInt32(2),
                    reader.GetStringOrNull(3),
                    reader.GetStringOrNull(4),
                    reader.GetDecimal(5),
                    reader.GetInt32(6),
                    reader.GetInt32(7),
                    reader.GetStringOrNull(8)
                    ),
                new Author(
                    reader.GetInt32(9),
                    reader.GetStringOrNull(10),
                    reader.GetBoolean(11)
                    )
                );
            }
            return new FavQuery(
                reader.GetInt32(0),
                reader.GetStringOrNull(1)
                );
        }
    }
}