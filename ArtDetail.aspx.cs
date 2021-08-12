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
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

            Icon[] icons =
          {
                new Icon("icon_0001", "https://img.icons8.com/ios-glyphs/30/000000/long-arrow-left.png", "Left Arrow Icon", "/Home.aspx", "https://icons8.com/icon/99996/left-arrow", "Icons8"),
                new Icon("icon_0002", "https://img.icons8.com/ios-glyphs/30/000000/star--v2.png", "Animated Star Icon", "", "https://icons8.com/icon/hmAU0m6F6i0C/star", "Icons8"),
                new Icon("icon_0003", "https://img.icons8.com/windows/32/000000/share-rounded.png", "Share Rounded Icon", "", "https://icons8.com/icon/FupVmEePjs1T/share-rounded", "Icons8"),
                new Icon("icon_0004", "https://img.icons8.com/windows/32/000000/facebook-like.png", "Facebook Like Icon", "", "https://icons8.com/icon/33481/facebook-like", "Icons8"),
            };

            registerHiddenField("iconsState", icons);
  
            // Current art is set when button is clicked in Home.cs
            List<ArtQuery> data = selectCurrentArtDetail();
            List <FavQuery> favs = selectAllFavourites(user.Id);
            registerHiddenField("artState", data);
            registerHiddenField("favsState", favs);
        }

        private void registerHiddenField(string id, object obj)
        {
            Page.ClientScript.RegisterHiddenField(id, JsonConvert.SerializeObject(obj));
        }

        private List<ArtQuery> selectCurrentArtDetail()
        {
            ArtQuery.FetchCurrentArtDetail(getArtId());
            return Database.Select<ArtQuery>(ArtQuery.SqlQuery);
        }

        private List<FavQuery> selectAllFavourites(int id)
        {
            FavQuery.FetchAllUserFavourites(id);
            return Database.Select<FavQuery>(FavQuery.SqlQuery);
        }

        private int getArtId()
        {
            return Convert.ToInt32(Request.QueryString.Get("id"));
        }

        private int getFavId()
        {
            string str = Request.Form[btnSaveStar.UniqueID];
            string[] arr = str.Split(',');
            return Convert.ToInt32(arr[1]);
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

        public void btnSaveStar_click(object sender, EventArgs e)
        {
            if (setFavQueryIds())
            {
                System.Diagnostics.Trace.WriteLine(insertIntoFavArt());
            }
        }

    }
}