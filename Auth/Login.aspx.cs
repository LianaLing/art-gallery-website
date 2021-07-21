using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGalleryWebsite.Auth
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: Validate email and encrypt password (JWT or other hashing method)
            string email = Request.QueryString.Get("email");
            string password = Request.QueryString.Get("password");

            // Check if email, password matches a valid user
            if (FormsAuthentication.Authenticate(email, password))
            {
                // if match then set the user's email as a session state 
                Session["email"] = email;

                // redirect the user to home page
                FormsAuthentication.RedirectFromLoginPage(email, false);
            }
            else
            {
                // else redirect back to landing page
                Response.Redirect("/", true);
            }
        }
    }
}