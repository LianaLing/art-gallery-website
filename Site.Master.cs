using ArtGalleryWebsite.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ArtGalleryWebsite.Models.Queries;

namespace ArtGalleryWebsite
{
	public partial class SiteMaster : MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
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