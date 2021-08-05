using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.Identity;

namespace ArtGalleryWebsite.Auth
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<Models.Identity.User, Role, int, UserLogin, UserRole, UserClaim>(new ApplicationDbContext());
            var manager = new UserManager<Models.Identity.User, int>(userStore);

            System.Diagnostics.Trace.WriteLine(userStore.Context.Database.Connection.ConnectionString);

            var user = new Models.Identity.User() 
            { 
                UserName = UserName.Text,
                Email = "test@test.com",
                AvatarUrl = "https://www.google.com"
            };
            IdentityResult result = manager.Create(user, Password.Text);

            if (result.Succeeded)
            {
                StatusMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
            }
            else
            {
                StatusMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}