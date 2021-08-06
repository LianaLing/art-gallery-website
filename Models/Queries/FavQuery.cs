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

        public Art art;
        public Entities.User user;
        public Author author;

        public FavQuery()
        {
        }

        public FavQuery(int id, string name, Art art, Entities.User user, Author author) : base(id, name)
        {
            this.art = art;
            this.user = user;
            this.author = author;
        }

        public FavQuery(int id, string name, int user_id, Art art, Entities.User user, Author author) : base(id, name, user_id)
        {
            this.art = art;
            this.user = user;
            this.author = author;
        }

        public static void FetchCurrentUser(int id)
        {
            SqlQuery = @"
                SELECT 
                       [Favourite].id, [Favourite].name,
                       [Art].id, [Art].style, [Art].description, [Art].price, [Art].stock,  
                       [Art].likes, [Art].url,
                       [User].id, [User].username, [User].name, [User].ic, [User].dob, 
                       [User].PhoneNumber, [User].Email, [User].AvatarUrl, [User].AuthorId,
                       [Author].id, [Author].description, [Author].verified 
                FROM [Art], [Author], [User], [Favourite], [FavArt]
                WHERE [Favourite].user_id = [User].id
                AND [FavArt].art_id = [Art].id
                AND [FavArt].fav_id = [Favourite].id
                AND [Art].author_id = [Author].id
                AND [User].id = '" + id + 
                "' ORDER BY [Favourite].name ASC;";
        }

        public override ISqlParser ParseFromSqlReader(SqlDataReader reader)
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
                new Entities.User (
                    reader.GetInt32(9),
                    reader.GetStringOrNull(10),
                    reader.GetStringOrNull(11),
                    reader.GetStringOrNull(12),
                    reader.GetDateTime(13),
                    reader.GetStringOrNull(14),
                    reader.GetStringOrNull(15),
                    reader.GetStringOrNull(16),
                    reader.GetInt32(17)
                    ),
                new Author(
                    reader.GetInt32(18),
                    reader.GetStringOrNull(19),
                    reader.GetBoolean(20)
                    )
                );
        }
    }
}