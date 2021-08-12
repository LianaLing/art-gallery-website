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
    public partial class Home : System.Web.UI.Page
    {
        // private static readonly HttpClient client = new HttpClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Below are sample code for fetching an external API, just ignore for now
            // HttpResponseMessage res = client.GetAsync("http://jsonplaceholder.typicode.com/photos?_start=0&_limit=9").Result;
            // ViewState["state"] = res.Content.ReadAsStringAsync().Result.ToString();
            // System.Diagnostics.Debug.WriteLine("asdasdasd");

            // Fetch art responses from the database
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

            ArtQuery.FetchAllArt();
            List<ArtQuery> data = Database.Select<ArtQuery>(ArtQuery.SqlQuery);
            FavQuery.FetchCurrentUser(user.Id); //Hardcoded
            List<FavQuery> favs = Database.Select<FavQuery>(FavQuery.SqlQuery);

            // Inject the data (serialized as a JSON string) as a hidden field at client side
            Page.ClientScript.RegisterHiddenField(
                "arts",
                JsonConvert.SerializeObject(data)
            );
            Page.ClientScript.RegisterHiddenField(
                "favs",
                JsonConvert.SerializeObject(favs)
            );
        }

        protected void btnSaveArtChooseCollection_click(object sender, EventArgs e)
        {
            //string id = Request.Form[btnSaveArt.Attributes["value"]];
            //Button button = sender as Button;
            //string id = button.Attributes["value"];
            //System.Diagnostics.Trace.WriteLine(id);
        }

        public void btnSaveArt_click(object sender, EventArgs e)
        {
            string str = Request.Form[btnSaveArt.UniqueID];
            string[] strArr = str.Split(',');
            int art_id = Convert.ToInt32(strArr[0]);

            FavQuery.art_id = 0;
            FavQuery.art_id = art_id;
            System.Diagnostics.Trace.WriteLine("art_id: " + FavQuery.art_id);

            int fav_id = Convert.ToInt32(strArr[1]);
            FavQuery.fav_id = 0;
            FavQuery.fav_id = fav_id;
            System.Diagnostics.Trace.WriteLine("fav_id: " + FavQuery.fav_id);

            FavQuery.InsertFavArt();

            try
            {
                System.Diagnostics.Trace.WriteLine("Affected " + Database.Insert(FavQuery.SqlQuery) + " row(s)");
            }
            catch (System.Data.SqlClient.SqlException err)
            {
                System.Diagnostics.Trace.WriteLine(err + " [Artwork already saved in this collection.]");
            }
        }

        public void btnArtDetailPage_click(object sender, EventArgs e)
        {
            string id = Request.Form[btnArtDetailPage.UniqueID];
            Response.Redirect($"~/ArtDetail.aspx?id={id}");
        }
    }
}