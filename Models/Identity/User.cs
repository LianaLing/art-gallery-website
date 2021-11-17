using ArtGalleryWebsite.Models.Entities;
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
        public DateTime? Dob { get; set; }
        public string AvatarUrl { get; set; }
        public int? AuthorId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}