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
            List<ArtQuery> data = Database.Select<ArtQuery>(ArtQuery.SqlQuery);

            // Inject the data (serialized as a JSON string) as a hidden field at client side
            Page.ClientScript.RegisterHiddenField(
                "arts",
                JsonConvert.SerializeObject(data)
            );
        }

        protected void btnSaveArt(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Hi from Vue button");
        }
    }
}