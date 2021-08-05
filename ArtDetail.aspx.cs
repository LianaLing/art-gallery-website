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

namespace ArtGalleryWebsite
{
    public partial class ArtDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Icon[] icons =
          {
                new Icon("icon_0001", "https://img.icons8.com/ios-glyphs/30/000000/long-arrow-left.png", "Left Arrow Icon", "/Home.aspx", "https://icons8.com/icon/99996/left-arrow", "Icons8"),
                new Icon("icon_0002", "https://img.icons8.com/ios-glyphs/30/000000/star--v2.png", "Animated Star Icon", "", "https://icons8.com/icon/hmAU0m6F6i0C/star", "Icons8"),
                new Icon("icon_0003", "https://img.icons8.com/windows/32/000000/share-rounded.png", "Share Rounded Icon", "", "https://icons8.com/icon/FupVmEePjs1T/share-rounded", "Icons8"),
                new Icon("icon_0004", "https://img.icons8.com/windows/32/000000/facebook-like.png", "Facebook Like Icon", "", "https://icons8.com/icon/33481/facebook-like", "Icons8"),
            };

            //Author Liana = new Author("aut_0001", "Liana Ling", "https://avatars.githubusercontent.com/u/68136684?s=64&v=4");
            //Art art = new Art("art_0099", "https://i.pinimg.com/564x/2b/31/44/2b31440e87f6abe17fe71bb3533bc62e.jpg", 1299.99, "Niffler", Liana);

            Page.ClientScript.RegisterHiddenField("iconsState", JsonConvert.SerializeObject(icons));
            //Page.ClientScript.RegisterHiddenField("artState", JsonConvert.SerializeObject(art));

            // Current art is set when button is clicked in Site.Master.cs
            int id = Convert.ToInt32(Request.QueryString.Get("id"));
            ArtQuery.FetchCurrentArtDetail(id);
            List<ArtQuery> data = Database.Select<ArtQuery>(ArtQuery.SqlQuery);

            Page.ClientScript.RegisterHiddenField("artState", JsonConvert.SerializeObject(data));

        }

    }
}