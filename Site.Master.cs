using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Utils;
using ArtGalleryWebsite.DAL;

namespace ArtGalleryWebsite
{
	public partial class SiteMaster : MasterPage
	{
        private static UnitOfWork unitOfWork = new UnitOfWork();

        protected void Page_Load(object sender, EventArgs e)
		{
			// If current user is logged in
            if (Page.User.Identity.IsAuthenticated)
            {
                ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

				// Get the current user
                ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

                // Set the user as a session state (filtered out 'PasswordHash')
                Session["user"] = IdentityHelper.FilterUser(user);

                // Update user's cart in session state if it is null
                Helper.UpdateCartSessionStateIfNull(unitOfWork, Session, user.Id);
            }
            else
            {
                Session["user"] = null;
                Session["cart"] = null;
            }

            // Inject session state to client side
            Helper.InjectSessionState(Page, Session);
        }

		protected void Page_Error(object sender, EventArgs e)
        {
			Exception ex = Server.GetLastError();

			System.Diagnostics.Trace.WriteLine($"{ex}");

			Server.ClearError();
        }

		// Control methods controlled by frontend
		public void btnUserPage_click(object sender, EventArgs e)
		{
			
		}

		public void btnHomePage_click(object sender, EventArgs e)
        {

        }
	}
}