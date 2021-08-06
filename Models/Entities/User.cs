using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Entities
{
    public class User : ISqlParser
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string Ic { get; set; }

        public Nullable<DateTime> Dob { get; set; }

        public string AvatarUrl { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Nullable<int> AuthorId { get; set; }

        public string SecurityStamp { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public Nullable<DateTime> LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public User() { }

        public User(int Id, string Username, string PasswordHash, string Name, string Ic, DateTime? Dob, string AvatarUrl, string PhoneNumber, string Email, int? AuthorId, string SecurityStamp, bool PhoneNumberConfirmed, bool TwoFactorEnabled, DateTime? LockoutEndDateUtc, bool LockoutEnabled, int AccessFailedCount)
        {
            this.Id = Id;
            this.Username = Username;
            this.PasswordHash = PasswordHash;
            this.Name = Name;
            this.Ic = Ic;
            this.Dob = Dob;
            this.AvatarUrl = AvatarUrl;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.AuthorId = AuthorId;
            this.SecurityStamp = SecurityStamp;
            this.PhoneNumberConfirmed = PhoneNumberConfirmed;
            this.TwoFactorEnabled = TwoFactorEnabled;
            this.LockoutEndDateUtc = LockoutEndDateUtc;
            this.LockoutEnabled = LockoutEnabled;
            this.AccessFailedCount = AccessFailedCount;
        }

        public User(int Id, string Username, string Name, string Ic, DateTime? Dob, string PhoneNumber, string Email, string AvatarUrl, int? AuthorId)
        {
            this.Id = Id;
            this.Username = Username;
            this.Name = Name;
            this.Ic = Ic;
            this.Dob = Dob;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.AvatarUrl = AvatarUrl;
            this.AuthorId = AuthorId;
        }

        public virtual ISqlParser ParseFromSqlReader(SqlDataReader reader)
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