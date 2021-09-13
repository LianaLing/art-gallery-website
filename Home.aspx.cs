using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ArtGalleryWebsite.DAL;
using ArtGalleryWebsite.Utils;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.Entities;
using ArtGalleryWebsite.Models.Queries;

namespace ArtGalleryWebsite
{
    public partial class Home : System.Web.UI.Page
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();
        private static ArtGalleryDbContext dbContext = unitOfWork.GetContext();

        protected int artId;
        protected int favId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Below are sample code for fetching an external API, just ignore for now
            // HttpResponseMessage res = client.GetAsync("http://jsonplaceholder.typicode.com/photos?_start=0&_limit=9").Result;
            // ViewState["state"] = res.Content.ReadAsStringAsync().Result.ToString();
            // System.Diagnostics.Debug.WriteLine("asdasdasd");

            // Get current session user
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = manager.FindById(Page.User.Identity.GetUserId<int>());

            // Get data for the page
            var data = selectAllArt();
            var favs = selectAllFavourites(currentUser.Id);
            var saved = checkIfArtIsInFav(currentUser.Id);

            // Get data for the page
            // Inject the data (serialized as a JSON string) as a hidden field at client side
            registerHiddenField("arts", data);
            registerHiddenField("favs", favs);
            registerHiddenField("saves", saved);
        }

        private void registerHiddenField(string id, object obj)
        {
            Page.ClientScript.RegisterHiddenField(id, Helper.SerializeObject(obj));
        }

        // Fetch all [Art]s in the database
        private IQueryable selectAllArt()
        {
            // Return the result
            return (from art in dbContext.Arts
                    join author in dbContext.Authors on art.AuthorID equals author.Id
                    join user in dbContext.Users on author.Id equals user.AuthorId
                    orderby art.Likes descending
                    select new
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
                            id = author.Id,
                            description = author.Description,
                            verified = author.Verified,
                            username = user.UserName,
                            name = user.Name,
                            ic = user.Ic,
                            dob = user.Dob,
                            contactNo = user.PhoneNumber,
                            email = user.Email,
                            avatarUrl = user.AvatarUrl
                        }
                    });
        }

        // Fetch all [Favourite]s of the user regardless of whether it is empty
        private IEnumerable<Favourite> selectAllFavourites(int id)
        {
            // Fetch all [Favourite]s of the user of the current session, regardless of whether it has rows in [FavArt]
            // [Favourite]s with empty [FavArt] rows will be displayed
            // Useful for finding out all [Favourite]s that belong to a particular user
            return unitOfWork.FavouriteRepository.Get(filter: fav => fav.UserId == id, orderBy: fav => fav.OrderBy(f => f.UserId));
        }

        // Check if the current art is saved in the user's [Favourite]s
        private IQueryable checkIfArtIsInFav(int id)
        {
            // Fetch [Favourite] rows that belongs to the logged in user in the current session
            // [Favourite]s wil empty [FavArt] rows will not be fetched
            // Ordered by [Favourite].id because the COUNT query is ordered by [FavArt].id
            // Keeping the same order will ease frontend parsing so please do not change this
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

        // Parse the value of the button, which is passed to backend from frontend
        // Button value is in the form of "{art_id},{fav_id}"
        // e.g. value="1,2"
        private string[] splitId()
        {
            string str = "";
            if (Request.Form[btnSaveArt.UniqueID] != null)
            {
                str = Request.Form[btnSaveArt.UniqueID];
            }
            else
            {
                str = Request.Form[btnRemoveArt.UniqueID];
            }

            string[] arr = str.Split(',');
            return arr;
        }

        // Get either art_id or fav_id
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
            //FavQuery.InsertFavArt(); //Query
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

        // Delete from database
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

        public void btnSaveArt_click(object sender, EventArgs e)
        {
            if (setFavQueryIds())
            {
                System.Diagnostics.Trace.WriteLine(insertIntoFavArt());
            }
        }

        public void btnRemoveArt_click(object sender, EventArgs e)
        {
            if (setFavQueryIds())
            {
                System.Diagnostics.Trace.WriteLine(removeFromFavArt());
            }
        }

        public void btnArtDetailPage_click(object sender, EventArgs e)
        {
            // Get value of button which is passed from frontend to backend
            string id = Request.Form[btnArtDetailPage.UniqueID];
            Response.Redirect($"~/ArtDetail.aspx?id={id}");
        }
    }
}