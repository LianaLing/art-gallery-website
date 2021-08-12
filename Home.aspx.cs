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

            List<ArtQuery> data = selectAllArt();
            List<FavQuery> favs = selectAllFavourites(user.Id);
            List<FavQuery> saved = checkIfArtIsInFav(user.Id);

            // Inject the data (serialized as a JSON string) as a hidden field at client side
            registerHiddenField("arts", data);
            registerHiddenField("favs", favs);
            registerHiddenField("saves", saved);
        }

        private void registerHiddenField(string id, object obj)
        {
            Page.ClientScript.RegisterHiddenField(id, JsonConvert.SerializeObject(obj));
        }

        private List<ArtQuery> selectAllArt()
        {
            ArtQuery.FetchAllArt();
            return Database.Select<ArtQuery>(ArtQuery.SqlQuery);
        }

        private List<FavQuery> selectAllFavourites(int id)
        {
            FavQuery.FetchAllUserFavourites(id);
            return Database.Select<FavQuery>(FavQuery.SqlQuery);
        }

        private List<FavQuery> checkIfArtIsInFav(int id)
        {
            FavQuery.FetchCurrentUser(id);
            return Database.Select<FavQuery>(FavQuery.SqlQuery);
        }

        protected void btnSaveArtChooseCollection_click(object sender, EventArgs e)
        {
            //string id = Request.Form[btnSaveArt.Attributes["value"]];
            //Button button = sender as Button;
            //string id = button.Attributes["value"];
            //System.Diagnostics.Trace.WriteLine(id);
        }

        private string[] splitId()
        {
            string str = Request.Form[btnSaveArt.UniqueID];
            string[] arr = str.Split(',');
            return arr;
        }

        private int getSomeId(int which)
        {
            string[] arr = splitId();
            return Convert.ToInt32(arr[which]);
        }

        private int getArtId()
        {
            return getSomeId(0);
        }
        
        private int getFavId()
        {
            return getSomeId(1);
        }

        private bool setFavQueryIds()
        {
            try
            {
                FavQuery.art_id = getArtId();
                FavQuery.fav_id = getFavId();
                System.Diagnostics.Trace.WriteLine($"fav_id: {FavQuery.fav_id}\nart_id: {FavQuery.art_id}");
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine("ERROR: " + e);
                return false;
            }
            return true;
        }

        private string insertIntoFavArt()
        {
            FavQuery.InsertFavArt(); //Query
            try
            {
                return "Affected " + Database.Insert(FavQuery.SqlQuery) + " row(s)";
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                return e + " [Artwork already saved in this collection.]";
            }
        }

        public void btnSaveArt_click(object sender, EventArgs e)
        {
            if (setFavQueryIds())
            {
                System.Diagnostics.Trace.WriteLine(insertIntoFavArt());
            }
        }

        public void btnArtDetailPage_click(object sender, EventArgs e)
        {
            string id = Request.Form[btnArtDetailPage.UniqueID];
            Response.Redirect($"~/ArtDetail.aspx?id={id}");
        }
    }
}