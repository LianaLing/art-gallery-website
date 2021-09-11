using System;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ArtGalleryWebsite.Utils;
using ArtGalleryWebsite.Models.Queries;
using System.Collections.Generic;
using ArtGalleryWebsite.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace ArtGalleryWebsite
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get current session user
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

            // Get data for the page
            List<ArtQuery> data = selectAllArt();

            // Register hidden field to pass data from backend to frontend
            registerHiddenField("arts", data);
        }

        private void registerHiddenField(string id, object obj)
        {
            Page.ClientScript.RegisterHiddenField(id, JsonConvert.SerializeObject(obj));
        }

        // Fetch all [Art]s in the database
        private List<ArtQuery> selectAllArt()
        {
            ArtQuery.FetchAllArt();
            return Database.Select<ArtQuery>(ArtQuery.SqlQuery);
        }

    }
}