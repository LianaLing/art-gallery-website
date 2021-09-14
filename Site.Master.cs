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