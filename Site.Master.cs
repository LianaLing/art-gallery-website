using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Utils;

namespace ArtGalleryWebsite
{
	public partial class SiteMaster : MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Page.User.Identity.IsAuthenticated)
            {
                ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());
				Session["user"] = user;
            }
			
			// Inject session state to client side
			Helper.InjectSessionState(Page, Session);
		}

		// Control methods controlled by frontend
		public void btnUserPage_click(object sender, EventArgs e)
		{
			//ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('btnUserPage clicked')", true);
		}

		public void btnHomePage_click(object sender, EventArgs e)
        {

        }
	}
}