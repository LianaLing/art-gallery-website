using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Queries
{
    public class UserQuery : ArtGalleryWebsite.Models.Entities.User, ISqlParser
    {
        public static string SqlQuery;

        public UserQuery()
        {
        }

        public UserQuery(int id, string username, string name, string ic, DateTime? dob, string contactNo, string email, string avatarUrl) : base (id, username, name, ic, dob, contactNo, email, avatarUrl)
        {

        }

        public static void FetchCurrentUser(int id)
        {
            SqlQuery = @"
            SELECT [User].id, [User].username, [User].name, [User].ic, [User].dob, 
                   [User].contact_no, [User].email, [User].avatar_url
            FROM [User]
            WHERE [User].id = '" + id + "';";
        }

        public virtual ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            return new UserQuery(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetDateTime(4),
                reader.GetString(5),
                reader.GetString(6),
                reader.GetString(7)
            );
        }
    }
}