using System;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace ArtGalleryWebsite.Auth
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //// TODO: Validate email and encrypt password (JWT or other hashing method)
            //string email = Request.QueryString.Get("email");
            //string password = Request.QueryString.Get("password");

            //// Check if email, password matches a valid user
            //if (FormsAuthentication.Authenticate(email, password))
            //{
            //    // if match then set the user's email as a session state 
            //    Session["email"] = email;

            //    // redirect the user to home page
            //    FormsAuthentication.RedirectFromLoginPage(email, false);
            //}
            //else
            //{
            //    // else redirect back to landing page
            //    Response.Redirect("/", true);
            //}
            LogIn();
        }

        //protected void CreateUser()
        //{
        //    UserStore<IdentityUser> userStore = new UserStore<IdentityUser>();
        //    UserManager<IdentityUser> manager = new UserManager<IdentityUser>(userStore);

        //    IdentityUser user = new IdentityUser() { UserName = "marcustut" };
        //    IdentityResult result = manager.Create(user, "test1234");

        //    if (result.Succeeded)
        //    {
        //        System.Diagnostics.Trace.WriteLine("Successfully created user");
        //    }
        //    else
        //    {
        //        System.Diagnostics.Trace.WriteLine(result.Errors.ToString());
        //    }
        //}

        protected void LogIn()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            var result = signinManager.PasswordSignIn("test", "test1234", false, false);

            System.Diagnostics.Trace.WriteLine(result.ToString());
        }
    }
}