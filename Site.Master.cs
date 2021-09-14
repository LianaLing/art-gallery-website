using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Utils;
using ArtGalleryWebsite.DAL;
using ArtGalleryWebsite.Models.Entities;
using System.Collections.Generic;

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

				// User's shopping cart
				List<ShoppingCart> found = unitOfWork.ShoppingCartRepository.Get(cart => cart.UserId == user.Id);

				// Set the user's shopping cart as a session state
				Session["cart"] = found.Count != 0 ? found[0] : null;
            }
			
			// Inject session state to client side
			Helper.InjectSessionState(Page, Session);
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