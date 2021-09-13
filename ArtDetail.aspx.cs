using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using ArtGalleryWebsite.DAL;
using ArtGalleryWebsite.Utils;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.Queries;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite
{
    public partial class ArtDetail : System.Web.UI.Page
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();
        private static ArtGalleryDbContext dbContext = unitOfWork.GetContext();

        protected int artId;
        protected int favId;

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
            var data = selectCurrentArtDetail();
            var favs = selectAllFavourites(user.Id);
            var saved = checkIfArtIsInFav(user.Id);

            // Register hidden field to pass data from backend to frontend
            registerHiddenField("artState", data);
            registerHiddenField("favsState", favs);
            registerHiddenField("savedState", saved);
        }

        private void registerHiddenField(string id, object obj)
        {
            Page.ClientScript.RegisterHiddenField(id, Helper.SerializeObject(obj));
        }

        // Get the art which is clicked on
        private IQueryable selectCurrentArtDetail()
        {
            //ArtQuery.FetchCurrentArtDetail(getArtId());
            //return Database.Select<ArtQuery>(ArtQuery.SqlQuery);
            artId = getArtId();

            return dbContext.Arts
                .Where(art => art.Id == artId)
                .Join(
                    dbContext.Users,
                    art => art.AuthorID,
                    user => user.AuthorId,
                    (art, user) => new
                    {
                        id = art.Id,
                        style = art.Style,
                        description = art.Description,
                        price = art.Price,
                        stock = art.Stock,
                        likes = art.Likes,
                        url = art.Url,
                        author = new
                        {
                            id = art.Author.Id,
                            description = art.Author.Description,
                            verified = art.Author.Verified,
                            username = user.UserName,
                            name = user.Name,
                            ic = user.Ic,
                            dob = user.Dob,
                            contactNo = user.PhoneNumber,
                            email = user.Email,
                            avatarUrl = user.AvatarUrl
                        }
                    }
                );
        }

        // Get all [Favourite]s of the current session user regardles whether it is empty
        private IEnumerable<Favourite> selectAllFavourites(int id)
        {
            //FavQuery.FetchAllUserFavourites(id);
            //return Database.Select<FavQuery>(FavQuery.SqlQuery);
            return unitOfWork.FavouriteRepository.Get(filter: fav => fav.UserId == id, orderBy: fav => fav.OrderBy(f => f.UserId));
        }

        // Check if this art is saved in the user's [Favourite]s
        private IQueryable checkIfArtIsInFav(int id)
        {
            //FavQuery.FetchCurrentUser(id);
            //return Database.Select<FavQuery>(FavQuery.SqlQuery);

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
                artId = getArtId();
                favId = getFavId();
                System.Diagnostics.Trace.WriteLine($"fav_id: {favId}\nart_id: {artId}");
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
            //FavQuery.InsertFavArt();
            try
            {
                //return "Affected " + Database.Insert(FavQuery.SqlQuery) + " row(s)";
                FavArt favArt = new FavArt { ArtId = artId, FavId = favId };
                unitOfWork.FavArtRepository.Insert(favArt);
                unitOfWork.Save();

                return $"Successful insertion of FavArt {{ fav_id: {favArt.FavId}, art_id: {favArt.ArtId} }}";
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                return e + " [Artwork already saved in this collection.]";
            }
        }

        // Insert into database
        private string removeFromFavArt()
        {
            //FavQuery.RemoveFromFavArt();
            try
            {
                //return "Affected " + Database.Delete(FavQuery.SqlQuery) + " row(s)";
                FavArt deleted = unitOfWork.FavArtRepository.Delete(favId, artId);

                if (deleted == null) 
                    throw new Exception($"Unable to delete FavArt {{ fav_id: {favId}, art_id: {artId} }}");
                else
                    unitOfWork.Save();

                return $"Successful insertion of FavArt {{ fav_id: {favId}, art_id: {artId} }}";
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

        public void btnCartPage_click (object sender, EventArgs e)
        {

        }
    }
}