using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGalleryWebsite.Models.DTO;

namespace ArtGalleryWebsite.User_Control
{
    public partial class CartItemNoQty : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        //public CartItemDTO CartItem
        //{
        //    get { return CartItem; }
        //    set { CartItem = value; }
        //}

        //public bool SameAsPrevious
        //{
        //    get { return SameAsPrevious; }
        //    set { SameAsPrevious = value; }
        //}

        //public void setValues()
        //{
        //    //foreach (CartItemDTO CartItem in CartItems)
        //    //{
        //    //    ciImg.ImageUrl = CartItem.Art.Url;
        //    //    ciImg.AlternateText = CartItem.Art.Description;
        //    //    ciDesc.Text = CartItem.Art.Description;
        //    //    ciAuthor.Text = CartItem.Art.Author.Name;
        //    //    ciPrice.Text = "RM " + CartItem.Art.Price;
        //    //}

        //    ciImg.ImageUrl = CartItem.Art.Url;
        //    ciImg.AlternateText = CartItem.Art.Description;
        //    ciDesc.Text = CartItem.Art.Description;
        //    ciAuthor.Text = CartItem.Art.Author.Name;
        //    ciPrice.Text = "RM " + CartItem.Art.Price;
        //    System.Diagnostics.Trace.WriteLine(CartItem);
        //}
    }
}