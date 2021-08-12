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
        public int total_art = 0;

        public Art art;
        public Entities.User user;
        public Author author;

        public FavQuery()
        {
        }

        public FavQuery(int id, string name) : base (id, name)
        {

        }

        public FavQuery(int total_art, int id)
        {
            this.total_art = total_art;
            this.id = id;
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
                ORDER BY [Favourite].id ASC;";
        }

        public static void FetchAllUserFavourites(int id)
        {
            SqlQuery = $@"
                SELECT 
                        [Favourite].id, [Favourite].name
                FROM [Favourite]
                WHERE [Favourite].user_id = {id}
                ORDER BY [Favourite].id ASC;
            ";
        }

        public static void InsertFavArt()
        {
            SqlQuery = $@"INSERT INTO [FavArt] VALUES ('{fav_id}','{art_id}')";
        }

        public static void RemoveFromFavArt()
        {
            SqlQuery = $@"DELETE FROM [FavArt] WHERE fav_id = {fav_id} AND art_id = {art_id};";
        }

        public static void CountArtInFavourites(int id)
        {
            SqlQuery = $@"SELECT COUNT([FavArt].art_id) AS total_art, [FavArt].fav_id
                            FROM [FavArt], [Favourite], [User]
                            WHERE [FavArt].fav_id = [Favourite].id
                            AND [User].Id = [Favourite].user_id
                            AND [User].Id = {id}
                            GROUP BY [FavArt].fav_id
                            ORDER BY [FavArt].fav_id ASC;";
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
            try
            {
                return new FavQuery(
                reader.GetInt32(0),
                reader.GetInt32(1)
                );
            }
            catch
            {
                return new FavQuery(
                    reader.GetInt32(0),
                    reader.GetStringOrNull(1)
                    );
            }
        }
    }
}