using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Entities
{
    public class User : ISqlParser
    {
        public int id;
        public string username;
        public string name;
        public string ic;
        public Nullable<DateTime> dob;
        public string contactNo;
        public string email;
        public string avatarUrl;

        public User(int id, string username, string name, string ic, DateTime? dob, string contactNo, string email, string avatarUrl)
        {
            this.id = id;
            this.username = username;
            this.name = name;
            this.ic = ic;
            this.dob = dob;
            this.contactNo = contactNo;
            this.email = email;
            this.avatarUrl = avatarUrl;
        }

        public virtual ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            return new User(
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