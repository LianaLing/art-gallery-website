using ArtGalleryWebsite.Utils;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Entities
{
    public class UserTable<TUser>
        where TUser : IdentityUser
    {
        public string GetUsername(string userId)
        {
            string sqlQuery = $"SELECT username FROM user WHERE id = {userId}";
            return Database.QueryValue<string>(sqlQuery);
        }

        public string GetUserId(string username)
        {
            string sqlQuery = $"SELECT id FROM user WHERE username = {username}";
            return Database.QueryValue<string>(sqlQuery);
        }
    }
}