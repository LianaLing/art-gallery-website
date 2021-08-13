using System;
using System.Data.SqlClient;
using ArtGalleryWebsite.Utils;

namespace ArtGalleryWebsite.Models.Queries
{
    public class UserQuery : Entities.User, ISqlParser
    {
        public static string SqlQuery;

        public UserQuery()
        {
        }

        public UserQuery(int id, string username, string name, string ic, DateTime? dob, string phoneNumber, string email, string avatarUrl, int? authorId)
            : base (id, username, name, ic, dob, phoneNumber, email, avatarUrl, authorId)
        {
        }

        override
        public ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            return new UserQuery(
                reader.GetInt32(0),
                reader.GetStringOrNull(1),
                reader.GetStringOrNull(2),
                reader.GetStringOrNull(3),
                reader.GetDateTimeOrNull(4),
                reader.GetStringOrNull(5),
                reader.GetStringOrNull(6),
                reader.GetStringOrNull(7),
                reader.GetInt32(8)
            );
        }
    }
}