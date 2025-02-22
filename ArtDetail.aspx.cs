﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ArtGalleryWebsite.DAL;
using ArtGalleryWebsite.DAL.Extensions;
using ArtGalleryWebsite.Utils;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.DTO;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite
{
    public partial class ArtDetail : System.Web.UI.Page
    {
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

            Helper.RegisterHiddenField(Page, "iconsState", icons);

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                // Get data for the page
                ArtDetailDTO data = unitOfWork.GetArtDetailById(getArtId());
                IEnumerable<Favourite> favs = unitOfWork.FavouriteRepository.Get(filter: fav => fav.UserId == user.Id, orderBy: fav => fav.OrderBy(f => f.UserId));
                IEnumerable<FavDTO> saved = unitOfWork.GetUserFavourites(user.Id);
                bool liked = unitOfWork.isArtLiked(user.Id, getArtId());

                // Register hidden field to pass data from backend to frontend
                Helper.RegisterHiddenField(Page, "artState", data);
                Helper.RegisterHiddenField(Page, "favsState", favs);
                Helper.RegisterHiddenField(Page, "savedState", saved);
                Helper.RegisterHiddenField(Page, "likedState", liked);
            }
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            // Handle specific exception.
            if (exc is HttpUnhandledException)
            {
                lblErr.Text = "An error occurred on this page. Please verify your " +
                "information to resolve the issue.";
            }
            // Clear the error from the server.
            Server.ClearError();
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
            if (Request.Form[btnSaveArt.UniqueID] != null)
            {
                str = Request.Form[btnSaveArt.UniqueID];
            }
            else
            {
                str = Request.Form[btnRemoveArt.UniqueID];
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
            catch (Exception ex)
            {
                lblErr.Visible = true;
                lblErr.Text = $"{ex}";
                System.Diagnostics.Trace.WriteLine("ERROR: " + ex);
                return false;
            }
            return true;
        }

        // Insert into database
        private string insertIntoFavArt()
        {
            //FavQuery.InsertFavArt();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    //return "Affected " + Database.Insert(FavQuery.SqlQuery) + " row(s)";
                    FavArt favArt = new FavArt { ArtId = artId, FavId = favId };
                    unitOfWork.FavArtRepository.Insert(favArt);
                    unitOfWork.Save();

                    return $"Successful insertion of FavArt {{ fav_id: {favArt.FavId}, art_id: {favArt.ArtId} }}";
                }
                catch (Exception ex)
                {
                    lblErr.Visible = true;
                    lblErr.Text = $"{ex}";
                    return ex + " [Artwork already saved in this collection.]";
                }
            }
        }

        // Remove FavArt from database
        private string removeFromFavArt()
        {
            //FavQuery.RemoveFromFavArt();
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
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
                catch (Exception ex)
                {
                    lblErr.Visible = true;
                    lblErr.Text = $"{ex}";
                    return ex + " [Artwork already deleted from this collection.]";
                }
            }
        }

        protected void btnSaveArt_click(object sender, EventArgs e)
        {
            if (setFavQueryIds())
            {
                System.Diagnostics.Trace.WriteLine(insertIntoFavArt());
                Server.TransferRequest(Request.Url.AbsolutePath, false);
            }
        }

        protected void btnRemoveArt_click(object sender, EventArgs e)
        {
            if (setFavQueryIds())
            {
                System.Diagnostics.Trace.WriteLine(removeFromFavArt());
                Server.TransferRequest(Request.Url.AbsolutePath, false);
            }
        }

        protected void btnLikeHandler_click(object sender, EventArgs e)
        {
            // Check if art id exists in query string
            if (getArtId() == 0) throw new Exception("Invalid Art Id");

            // Get current session user
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    int art_id = getArtId();

                    // Check if is art not liked
                    if (!unitOfWork.isArtLiked(user.Id, art_id))
                    {
                        unitOfWork.addLikeToArt(user.Id, art_id);
                    }
                    else
                    {
                        unitOfWork.removeLikeFromArt(user.Id, art_id);
                    }

                    // Refresh the page
                    Server.TransferRequest(Request.Url.AbsolutePath, false);
                }
                catch (Exception ex)
                {
                    lblErr.Visible = true;
                    lblErr.Text = $"{ex}";
                }
            }
        }

        protected void btnAddToCart_click(object sender, EventArgs e)
        {
            // Check if art id exists in query string
            if (getArtId() == 0) throw new Exception("Invalid Art Id");

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    // Get current session user
                    ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

                    ShoppingCart cart = unitOfWork.GetShoppingCartByUserId(user.Id);

                    // If the user does not have a shopping cart
                    if (cart == null)
                        cart = unitOfWork.CreateShoppingCart(user.Id);

                    // Add one art the the shopping cart
                    cart = unitOfWork.AddArtToShoppingCart(cart.Id, getArtId(), quantity: 1);

                    // Update session state
                    Session["cart"] = new ShoppingCartDTO { Id = cart.Id, Total = cart.Total, UserId = cart.UserId };

                    // Refresh page
                    Server.TransferRequest(Request.Url.AbsolutePath, false);
                }
                catch (Exception ex)
                {
                    lblErr.Visible = true;
                    lblErr.Text = $"{ex}";
                }
            }
        }

        protected void btnPurchaseNow_click(object sender, EventArgs e)
        {
            // Check if art id exists in query string
            if (getArtId() == 0) throw new Exception("Invalid Art Id");

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    // Get current session user
                    ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

                    ShoppingCart cart = unitOfWork.GetShoppingCartByUserId(user.Id);

                    // If the user does not have a shopping cart
                    if (cart == null)
                        cart = unitOfWork.CreateShoppingCart(user.Id);

                    // Add one art the the shopping cart
                    cart = unitOfWork.AddArtToShoppingCart(cart.Id, getArtId(), quantity: 1);

                    // Update session state
                    Session["cart"] = new ShoppingCartDTO { Id = cart.Id, Total = cart.Total, UserId = cart.UserId };

                    // Refresh page
                    Response.Redirect("~/Cart.aspx");
                }
                catch (Exception ex)
                {
                    lblErr.Visible = true;
                    lblErr.Text = $"{ex}";
                }
            }
        }
    }
}

        //// Get all [Favourite]s of the current session user regardles whether it is empty
        //private IEnumerable<Favourite> selectAllFavourites(int id)
        //{
        //    //FavQuery.FetchAllUserFavourites(id);
        //    //return Database.Select<FavQuery>(FavQuery.SqlQuery);
        //    return unitOfWork.FavouriteRepository.Get(filter: fav => fav.UserId == id, orderBy: fav => fav.OrderBy(f => f.UserId));
        //}

        //// Check if this art is saved in the user's [Favourite]s
        //private IQueryable<FavQuery> checkIfArtIsInFav(int id)
        //{
        //    //FavQuery.FetchCurrentUser(id);
        //    //return Database.Select<FavQuery>(FavQuery.SqlQuery);
        //}
