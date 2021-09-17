using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGalleryWebsite.Models.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.Entities;
using ArtGalleryWebsite.DAL;
using ArtGalleryWebsite.DAL.Extensions;

namespace ArtGalleryWebsite.User_Control
{
    public partial class CartItemNoQty : System.Web.UI.UserControl
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        protected ShoppingCart cart;

        protected void Page_Load(object sender, EventArgs e)
        {
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());
            cart = unitOfWork.GetShoppingCartByUserId(user.Id);
            if (cart == null) throw new Exception($"User {user.Name} has no items in cart.");
        }

        public CartItemDTO CartItem
        {
            get;
            set;
        }

        public Int32 Quantity
        {
            get;
            set;
        }

        protected void btnQtyDecr_click(object sender, EventArgs e)
        {
            // Decrease item quantity by 1 (remove one cart item from cart)
            System.Diagnostics.Trace.WriteLine("Clicked on decr");
        }

        protected void btnQtyIncr_click(object sender, EventArgs e)
        {
            // Increase item quantity by 1 (add one cart item from cart)
            // Add one art the the shopping cart
            cart = unitOfWork.AddArtToShoppingCart(cart.Id, CartItem.Art.Id, quantity: 1);

            //Update session state
            Session["cart"] = cart;

            // Refresh page
            Server.TransferRequest(Request.Url.AbsolutePath, false);
            System.Diagnostics.Trace.WriteLine("Clicked on incr");
        }
    }
}