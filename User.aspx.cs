using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGalleryWebsite.DAL;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.Queries;
using ArtGalleryWebsite.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace ArtGalleryWebsite
{
    public class Icon
    {
        public string id;
        public string src;
        public string alt;
        public string href;
        public string creditRef;
        public string author;

        public Icon(string id, string src, string alt, string href, string creditRef, string author)
        {
            this.id = id;
            this.src = src;
            this.alt = alt;
            this.href = href;
            this.creditRef = creditRef;
            this.author = author;
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
        private static UnitOfWork unitOfWork = new UnitOfWork();
        private static ArtGalleryDbContext dbContext = unitOfWork.GetContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            Icon[] icons =
           {
                new Icon("icon_0001", "https://img.icons8.com/material/24/000000/pencil--v1.png", "Edit Icon", "", "https://icons8.com/icon/5824/pencil", "Icons8"),
                new Icon("icon_0002", "https://img.icons8.com/material-rounded/24/000000/share.png", "Share Icon", "", "https://icons8.com/icon/83213/share", "Icons8"),
                new Icon("icon_0003", "https://img.icons8.com/material-outlined/24/000000/settings--v1.png", "Settings Icon", "UserDetail.aspx", "https://icons8.com/icon/82535/settings", "Icons8"),
                new Icon("icon_0004", "https://img.icons8.com/material-rounded/24/000000/add.png", "Add Icon", "", "https://icons8.com/icon/85096/add", "Icons8"),
            };

            // Get session user
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

            // Get data for the page
            var data = selectNonEmptyFavourites(user.Id);
            var count = countArtInFavourites(user.Id);

            // Pass data into hidden field for frontend to parse
            registerHiddenField("iconsState", icons);
            registerHiddenField("state", data);
            registerHiddenField("countState", count);

            if (!IsPostBack)
            {
                CreateFav.Visible = false;
            }
        }

        private void registerHiddenField(string id, object obj)
        {
            Page.ClientScript.RegisterHiddenField(id, Helper.SerializeObject(obj));
        }

        private List<FavQuery> callFavQueryDatabase()
        {
            return Database.Select<FavQuery>(FavQuery.SqlQuery);
        }

        // Will only return favourites in [Favourite] that have >= 1 row of data
        // Empty favourites will not be returned
        private IQueryable selectNonEmptyFavourites(int id)
        {
            // FavQuery.FetchCurrentUser(id);
            // return callFavQueryDatabase();
            return (from art in dbContext.Arts
                    join author in dbContext.Authors on art.AuthorID equals author.Id
                    join favArt in dbContext.FavArts on art.Id equals favArt.ArtId
                    join fav in dbContext.Favourites on favArt.FavId equals fav.Id
                    join user in dbContext.Users on fav.UserId equals user.Id
                    where user.Id == id
                    orderby fav.Id ascending
                    select new
                    {
                        id = fav.Id,
                        name = fav.Name,
                        art = new
                        {
                            id = art.Id,
                            style = art.Style,
                            description = art.Description,
                            price = art.Price,
                            stock = art.Stock,
                            likes = art.Likes,
                            url = art.Url
                        },
                        author = new
                        {
                            id = author.Id,
                            description = author.Description,
                            verified = author.Verified
                        }
                    });
        }

        // Return the total number of rows in [FavArt] for each [Favourite]
        private IQueryable countArtInFavourites(int id)
        {
            //FavQuery.CountArtInFavourites(id);
            //return callFavQueryDatabase();

            return dbContext.Users
                .Where(user => user.Id == id)
                .Join(
                    dbContext.Favourites,
                    user => user.Id,
                    fav => fav.UserId,
                    (user, fav) => fav.Id
                )
                .Join(
                    dbContext.FavArts,
                    favId => favId,
                    favArt => favArt.FavId,
                    (favId, favArt) => new { art_id = favArt.ArtId, fav_id = favArt.FavId }
                )
                .GroupBy(res => res.fav_id)
                .Select(res => new { fav_id = res.Key, total_art = res.Count() });

            //return (from user in dbContext.Users
            //        join fav in dbContext.Favourites on user.Id equals fav.UserId
            //        join favArt in dbContext.FavArts on fav.Id equals favArt.FavId
            //        where user.Id == id
            //        group favArt by favArt.FavId into g
            //        select new { fav_id = g.Key, total_art = g.Count() }
            //    );
        }

        protected void btnSaveArtDetailPage_click(object sender, EventArgs e)
        {
            // Get button `value` field where frontend data is passed into backend
            string id = Request.Form[btnSaveArtDetailPage.UniqueID];

            // Pass frontend data into query string and redirect to the page
            Response.Redirect($"~/ArtDetail.aspx?id={id}");
        }

        protected void btnUserDetailPage_click(object sender, EventArgs e)
        {

        }

        protected void btnFavDetailPage_click(object sender, EventArgs e)
        {
            // Get button `value` field where frontend data is passed into backend
            string id = Request.Form[btnFavDetailPage.UniqueID];

            // Pass frontend data into query string and redirect to the page
            // Only display if user owns the fav
            // Query string might not be appropriate if users can change the id in the address bar
            //Response.Redirect($"~/FavDetail.aspx?id={id}");
            Response.Redirect($"~/FavDetail.aspx");
        }

        protected void btnShowCreateFav_click(object sender, EventArgs e)
        {
            CreateFav.Visible = true;
            validateCreateFav();
            System.Diagnostics.Trace.WriteLine("Clicked on show");
        }

        protected void btnCancelFav_click(object sender, EventArgs e)
        {
            CreateFav.Visible = false;
            System.Diagnostics.Trace.WriteLine("Clicked on cancel");
        }

        protected void btnCreateFav_click(object sender, EventArgs e)
        {
            validateCreateFav();
            System.Diagnostics.Trace.WriteLine("Clicked on create, Fav Name: " + txtFavName.Text);
            CreateFav.Visible = false;
            // Insert into database, will display one more new save
        }

        private void validateCreateFav()
        {
            if (CreateFav.Visible)
            {
                Validate("VGFav");
            }
        }
    }

}