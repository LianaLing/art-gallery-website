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
    public class Icon
    {
        public string id;
        public string src;
        public string alt;
        public string href;

        public Icon(string id, string src, string alt, string href)
        {
            this.id = id;
            this.src = src;
            this.alt = alt;
            this.href = href;
        }
    }

    public class Save
    {
        public string id;
        public string title;
        public int pinNo;
        public int updatedAt;
        public string href;
        public SavedArt[] arts;

        public Save(string id, string title, int pinNo, int updatedAt, string href, SavedArt[] arts)
        {
            this.id = id;
            this.title = title;
            this.pinNo = pinNo;
            this.updatedAt = updatedAt;
            this.href = href;
            this.arts = arts;
        }
    }

    public class SavedArt
    {
        public string id;
        public string imageSrc;
        public string title;

        public SavedArt(string id, string imageSrc, string title)
        {
            this.id = id;
            this.imageSrc = imageSrc;
            this.title = title;
        }
    }

    public class Profile
    {
        public string name;
        public string username;
        public int following;
        public string avatarUrl;
        public string profileUrl;

        public Profile(string name, string username, int following, string avatarUrl, string profileUrl)
        {
            this.name = name;
            this.username = username;
            this.following = following;
            this.avatarUrl = avatarUrl;
            this.profileUrl = profileUrl;
        }
    }

    public partial class User : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Icon[] icons =
           {
                new Icon("icon_0001", "https://img.icons8.com/material/24/000000/pencil--v1.png", "Edit Icon", "/"),
                new Icon("icon_0002", "https://img.icons8.com/material-rounded/24/000000/share.png", "Share Icon", "/"),
                new Icon("icon_0003", "https://img.icons8.com/material-outlined/24/000000/settings--v1.png", "Settings Icon", "/"),
                new Icon("icon_0004", "https://img.icons8.com/material-rounded/24/000000/add.png", "Add Icon", "/"),
            };

            SavedArt[] savedArts1 =
            {
                new SavedArt("svart_0001", "https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg", "The Weasleys"),
                new SavedArt("svart_0002", "https://i.pinimg.com/564x/dc/c6/7e/dcc67ed0107da71b834615d48421efa4.jpg", "Still Life with Flowers in a Glass Vase"),
                new SavedArt("svart_0002", "https://i.pinimg.com/564x/2b/af/63/2baf63ece32d100cec72010f60eab476.jpg", "A Wheatfield, with Cypresses"),
            };
            
            SavedArt[] savedArts2 =
            {
                new SavedArt("svart_0001", "https://i.pinimg.com/originals/da/68/e7/da68e7f731bfd78e20dba0ead711ca99.jpg", "The Weasleys"),
            };

            Save[] saves =
            {
                new Save("save_0001", "My saves", savedArts1.Length, 9, "/", savedArts1),
                new Save("save_0002", "Harry Potter", savedArts2.Length, 0, "/", savedArts2),
            };

            Profile profile = new Profile("Liana Ling", "lianaling", 4444, "https://avatars.githubusercontent.com/u/68136684?v=4", "/");

            Page.ClientScript.RegisterHiddenField("iconsState", JsonConvert.SerializeObject(icons));
            Page.ClientScript.RegisterHiddenField("savesState", JsonConvert.SerializeObject(saves));
            Page.ClientScript.RegisterHiddenField("profileState", JsonConvert.SerializeObject(profile));
        }
    }
}