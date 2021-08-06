using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models.Identity
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        // Extends columns from the default IdentityUser
        public string Name { get; set; }
        public string Ic { get; set; }
        public Nullable<DateTime> Dob { get; set; }
        public string AvatarUrl { get; set; }
        public Nullable<int> AuthorId { get; set; }
    }
}