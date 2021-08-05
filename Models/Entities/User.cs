using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Entities
{
    public class User : ISqlParser
    {
        public int id
        {
            get;
            set;
        }

        public string username
        {
            get;
            set;
        }

        public string passwordhash
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public string ic
        {
            get;
            set;
        }

        public Nullable<DateTime> dob
        {
            get;
            set;
        }

        public string avatarUrl
        {
            get;
            set;
        }

        public string phoneNumber
        {
            get;
            set;
        }

        public string email
        {
            get;
            set;
        }

        public int authorId
        {
            get;
            set;
        }

        public string securityStamp
        {
            get;
            set;
        }

        public bool phoneNumberConfirmed
        {
            get;
            set;
        }

        public bool twoFactorEnabled
        {
            get;
            set;
        }

        public Nullable<DateTime> lockoutEndDateUtc
        {
            get;
            set;
        }

        public bool lockoutEnabled
        {
            get;
            set;
        }

        public int accessFailedCount
        {
            get;
            set;
        }

        public User() { }

        public User(int id, string username, string passwordhash, string name, string ic, DateTime? dob, string avatarUrl, string phoneNumber, string email, int authorId, string securityStamp, bool phoneNumberConfirmed, bool twoFactorEnabled, DateTime? lockoutEndDateUtc, bool lockoutEnabled, int accessFailedCount)
        {
            this.id = id;
            this.username = username;
            this.passwordhash = passwordhash;
            this.name = name;
            this.ic = ic;
            this.dob = dob;
            this.avatarUrl = avatarUrl;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.authorId = authorId;
            this.securityStamp = securityStamp;
            this.phoneNumberConfirmed = phoneNumberConfirmed;
            this.twoFactorEnabled = twoFactorEnabled;
            this.lockoutEndDateUtc = lockoutEndDateUtc;
            this.lockoutEnabled = lockoutEnabled;
            this.accessFailedCount = accessFailedCount;
        }

        public ISqlParser ParseFromSqlReader(SqlDataReader reader)
        {
            return new User(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetDateTime(5),
                reader.GetString(6),
                reader.GetString(7),
                reader.GetString(8),
                reader.GetInt32(9),
                reader.GetString(10),
                reader.GetBoolean(11),
                reader.GetBoolean(12),
                reader.GetDateTime(13),
                reader.GetBoolean(14),
                reader.GetInt32(15)
            );
        }
    }
}