using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Entities
{
    public class User
    {
        public int id;
        public string username;
        public string name;
        public string ic;
        public Nullable<DateTime> dob;
        public string contactNo;
        public string email;
        public string avatarUrl;

        public User()
        {

        }

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
    }
}