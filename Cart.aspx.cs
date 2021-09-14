using System;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ArtGalleryWebsite.Utils;
using ArtGalleryWebsite.Models.Queries;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.DAL;

namespace ArtGalleryWebsite
{
    public partial class Cart : System.Web.UI.Page
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();
        private static ArtGalleryDbContext dbContext = unitOfWork.GetContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get current session user
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

            // Get data for the page
            IEnumerable<ArtQuery> data = selectAllArt();

            // Register hidden field to pass data from backend to frontend
            //registerHiddenField("arts", data);

            data = data.Select(d =>
            {
                d.price = decimal.Round(d.price, 2, MidpointRounding.AwayFromZero);
                return d;
            });

            // Calculate
            decimal subtotal = data.Sum(d => d.price);
            decimal shipping = 20;
            decimal total = subtotal + shipping;

            // Set labels
            setLblSubtotal(subtotal);
            setLblShipping(shipping);
            setTotal(total);
        }

        // Fetch all [Art]s in the database
        private IEnumerable<ArtQuery> selectAllArt()
        {
            // Return the result
            return (from art in dbContext.Arts
                    join author in dbContext.Authors on art.AuthorID equals author.Id
                    join user in dbContext.Users on author.Id equals user.AuthorId
                    orderby art.Likes descending
                    select new ArtQuery
                    {
                        id = art.Id,
                        style = art.Style,
                        description = art.Description,
                        price = art.Price,
                        stock = art.Stock,
                        likes = art.Likes,
                        url = art.Url,
                        author = new ArtQuery.Author
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
                    }).AsEnumerable<ArtQuery>();
        }

        //private decimal calculateSubtotal()
        //{
        //    decimal subtotal = 0;
        //    foreach (ArtQuery art in Arts)
        //    {
        //        subtotal += art.price;
        //    }

        //    return subtotal;
        //}

        //private decimal calculateTotal(decimal subtotal, decimal shipping)
        //{
        //    return subtotal + shipping;
        //}

        private void setLblSubtotal(decimal subtotal)
        {
            lblSubtotal.Text = decimal.Round(subtotal, 2) + "";
        }

        private void setLblShipping(decimal shipping)
        {
            string str = (decimal.Round(shipping, 2) + "");
            if (!str.Contains("."))
            {
                str += ".00";
            }
            lblShipping.Text = str;
        }

        private void setTotal(decimal total)
        {
            lblTotal.Text = decimal.Round(total, 2) + "";
        }

        protected void btnShowItems_click(object sender, EventArgs e)
        {
            ItemsList.Visible = !ItemsList.Visible;
        }

        protected void btnShowShipBill_click(object sender, EventArgs e)
        {
            ShipBill.Visible = !ShipBill.Visible;
        }

        protected void btnPayWith_click(object sender, EventArgs e)
        {
            string alertContent = "Paid with ";
            if (rdbtnCard.Checked)
            {
                // Do something
                alertContent += "card";
            }
            else if (rdbtnTng.Checked)
            {
                alertContent += "Touch and Go";
            }
            else if (rdbtnGrab.Checked)
            {
                alertContent += "Grab Pay";
            }
            else if (rdbtnFpx.Checked)
            {
                alertContent += "FPX";
            }
            else
            {
                alertContent = "Please select a payment method";
            }

            System.Diagnostics.Trace.WriteLine(alertContent);
        }

        protected void btnSubmitShipBill_click(object sender, EventArgs e)
        {

        }
    }
}