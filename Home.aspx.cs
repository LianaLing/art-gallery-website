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
    public class Author
    {
        public string id;
        public string name;
        public string avatarUrl;

        public Author(string id, string name, string avatarUrl)
        {
            this.id = id;
            this.name = name;
            this.avatarUrl = avatarUrl;
        }
    }

    public class Art 
    {
        public string id;
        public string imgSrc;
        public string title;
        public double price;
        public Author author;

        public Art(string id, string imgSrc, double price, string title, Author author)
        {
            this.id = id;
            this.imgSrc = imgSrc;
            this.price = price;
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
            Author Liana = new Author("aut_0001", "Liana Ling", "https://avatars.githubusercontent.com/u/68136684?s=64&v=4");
            Author Marcus = new Author("aut_0002", "Marcus Lee", "https://avatars.githubusercontent.com/u/59773847?s=64&v=4");

            Art[] arts =
            {
                new Art("art_0001", "https://i.pinimg.com/564x/2b/af/63/2baf63ece32d100cec72010f60eab476.jpg", 19000.00, title: "Still Life with Flowers in a Glass Vase", author: Liana),
                new Art("art_0002", "https://i.pinimg.com/564x/dc/c6/7e/dcc67ed0107da71b834615d48421efa4.jpg", 11234.45, title: "A Wheatfield, with Cypresses A Wheatfield, with Cypresses", author: Marcus),
                new Art("art_0003", "https://i.pinimg.com/564x/07/c2/8d/07c28d60e58e30929ac337e25f4ce789.jpg", 11111.55, title: "A Wheatfield, with Cypresses", author: Liana),
                new Art("art_0004", "https://i.pinimg.com/564x/96/cf/e4/96cfe4e8603f47121e6adf9282b3f7c8.jpg", 11111.11, title: "A Wheatfield, with Cypresses", author: Marcus),
                new Art("art_0005", "https://i.pinimg.com/564x/bb/37/b1/bb37b1b877e56726b137a387444ba33d.jpg", 55555.55, title: "A Wheatfield, with Cypresses A Wheatfield, with Cypresses A Wheatfield, with Cypresses", author: Liana),
                new Art("art_0006", "https://i.pinimg.com/564x/61/a9/8d/61a98d0f1c64a093a6707b82395afa58.jpg", 88888.88, title: "A Wheatfield, with Cypresses", author: Marcus),
                new Art("art_0007", "https://i.pinimg.com/564x/1a/86/56/1a865603d9cb21e1f8f8b947c1df4e80.jpg", 90099.99, title: "A Wheatfield, with Cypresses", author: Liana),
                new Art("art_0008", "https://i.pinimg.com/564x/3d/dd/3f/3ddd3fa8fe1ae75133c877406abe280f.jpg", 22222.22, title: "A Wheatfield, with Cypresses", author: Marcus),
                new Art("art_0009", "https://i.pinimg.com/564x/b9/92/49/b99249c860d9b507251991d063a245b4.jpg", 33333.33, title: "A Wheatfield, with Cypresses", author: Liana),
            };

            Page.ClientScript.RegisterHiddenField("state", JsonConvert.SerializeObject(arts));
        }
    }
}