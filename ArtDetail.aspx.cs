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
    public partial class ArtDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get current session user
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

            Icon[] icons =
          {
                new Icon("icon_0001", "https://img.icons8.com/ios-glyphs/30/000000/long-arrow-left.png", "Left Arrow Icon", "/Home.aspx", "https://icons8.com/icon/99996/left-arrow", "Icons8"),
                new Icon("icon_0002", "https://img.icons8.com/ios-glyphs/30/000000/star--v2.png", "Animated Star Icon", "", "https://icons8.com/icon/hmAU0m6F6i0C/star", "Icons8"),
                new Icon("icon_0003", "https://img.icons8.com/windows/32/000000/share-rounded.png", "Share Rounded Icon", "", "https://icons8.com/icon/FupVmEePjs1T/share-rounded", "Icons8"),
                new Icon("icon_0004", "https://img.icons8.com/windows/32/000000/facebook-like.png", "Facebook Like Icon", "", "https://icons8.com/icon/33481/facebook-like", "Icons8"),
                new Icon("icon_0005", "https://img.icons8.com/ios-filled/32/000000/facebook-like.png", "Facebook Like Icon Filled", "", "https://icons8.com/icon/33479/facebook-like", "Icons8"),
            };

            registerHiddenField("iconsState", icons);
  
            // Get data for the page
            List<ArtQuery> data = selectCurrentArtDetail();
            List <FavQuery> favs = selectAllFavourites(user.Id);
            List <FavQuery> saved = checkIfArtIsInFav(user.Id);

            // Register hidden field to pass data from backend to frontend
            registerHiddenField("artState", data);
            registerHiddenField("favsState", favs);
            registerHiddenField("savedState", saved);
        }

        private void registerHiddenField(string id, object obj)
        {
            Page.ClientScript.RegisterHiddenField(id, JsonConvert.SerializeObject(obj));
        }

        // Get the art which is clicked on
        private List<ArtQuery> selectCurrentArtDetail()
        {
            ArtQuery.FetchCurrentArtDetail(getArtId());
            return Database.Select<ArtQuery>(ArtQuery.SqlQuery);
        }

        // Get all [Favourite]s of the current session user regardles whether it is empty
        private List<FavQuery> selectAllFavourites(int id)
        {
            FavQuery.FetchAllUserFavourites(id);
            return Database.Select<FavQuery>(FavQuery.SqlQuery);
        }

        // Check if this art is saved in the user's [Favourite]s
        private List<FavQuery> checkIfArtIsInFav(int id)
        {
            FavQuery.FetchCurrentUser(id);
            return Database.Select<FavQuery>(FavQuery.SqlQuery);
        }

        // Get art_id from this page's query string, redirected from `Home.aspx.cs`
        private int getArtId()
        {
            return Convert.ToInt32(Request.QueryString.Get("id"));
        }

        // Get fav_id from the value of the button, which is passed to backend from frontend
        // Button value is in the form of "{art_id},{fav_id}"
        // e.g. value="1,2"
        private int getFavId()
        {
            string str = "";
            if (Request.Form[btnSaveStar.UniqueID] != null)
            {
                str = Request.Form[btnSaveStar.UniqueID];
            }
            else
            {
                str = Request.Form[btnRemoveStar.UniqueID];
            }

            string[] arr = str.Split(',');
            return Convert.ToInt32(arr[1]);
        }

        // Set the value of static variables in FavQuery class for query building
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

        // Insert into database
        private string insertIntoFavArt()
        {
            FavQuery.InsertFavArt();
            try
            {
               return "Affected " + Database.Insert(FavQuery.SqlQuery) + " row(s)";
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                return e + " [Artwork already saved in this collection.]";
            }
        }

        // Insert into database
        private string removeFromFavArt()
        {
            FavQuery.RemoveFromFavArt();
            try
            {
                return "Affected " + Database.Delete(FavQuery.SqlQuery) + " row(s)";
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                return e + " [Artwork already deleted from this collection.]";
            }
        }

        public void btnSaveStar_click(object sender, EventArgs e)
        {
            if (setFavQueryIds())
            {
                System.Diagnostics.Trace.WriteLine(insertIntoFavArt());
            }
        }

        public void btnRemoveStar_click(object sender, EventArgs e)
        {
            if (setFavQueryIds())
            {
                System.Diagnostics.Trace.WriteLine(removeFromFavArt());
            }
        }

    }
}