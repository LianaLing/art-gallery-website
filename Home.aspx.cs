using System;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace ArtGalleryWebsite
{
    public class Art 
    {
        public string id;
        public string imgSrc;
        public string title;
        public string author;

        public Art(string id, string imgSrc, string title, string author)
        {
            this.id = id;
            this.imgSrc = imgSrc;
            this.title = title;
            this.author = author;
        }
    }

    public partial class Home : System.Web.UI.Page
    {
        private static readonly HttpClient client = new HttpClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            // HttpResponseMessage res = client.GetAsync("http://jsonplaceholder.typicode.com/photos?_start=0&_limit=9").Result;
            // ViewState["state"] = res.Content.ReadAsStringAsync().Result.ToString();
            // System.Diagnostics.Debug.WriteLine("asdasdasd");
            Art[] arts =
            {
                new Art("art_0001", "https://i.pinimg.com/564x/2b/af/63/2baf63ece32d100cec72010f60eab476.jpg", title: "Picture 1", author: "Da Vin Ci"),
                new Art("art_0002", "https://i.pinimg.com/564x/dc/c6/7e/dcc67ed0107da71b834615d48421efa4.jpg", title: "Picture 2", author: "Da Vin Ci"),
                new Art("art_0003", "https://i.pinimg.com/564x/07/c2/8d/07c28d60e58e30929ac337e25f4ce789.jpg", title: "Picture 3", author: "Da Vin Ci"),
                new Art("art_0004", "https://i.pinimg.com/564x/96/cf/e4/96cfe4e8603f47121e6adf9282b3f7c8.jpg", title: "Picture 4", author: "Da Vin Ci"),
                new Art("art_0005", "https://i.pinimg.com/564x/bb/37/b1/bb37b1b877e56726b137a387444ba33d.jpg", title: "Picture 5", author: "Da Vin Ci"),
                new Art("art_0006", "https://i.pinimg.com/564x/61/a9/8d/61a98d0f1c64a093a6707b82395afa58.jpg", title: "Picture 6", author: "Da Vin Ci"),
                new Art("art_0007", "https://i.pinimg.com/564x/1a/86/56/1a865603d9cb21e1f8f8b947c1df4e80.jpg", title: "Picture 7", author: "Da Vin Ci"),
                new Art("art_0008", "https://i.pinimg.com/564x/3d/dd/3f/3ddd3fa8fe1ae75133c877406abe280f.jpg", title: "Picture 8", author: "Da Vin Ci"),
                new Art("art_0009", "https://i.pinimg.com/564x/b9/92/49/b99249c860d9b507251991d063a245b4.jpg", title: "Picture 9", author: "Da Vin Ci"),
            };

            Page.ClientScript.RegisterHiddenField("state", JsonConvert.SerializeObject(arts));
        }
    }
}