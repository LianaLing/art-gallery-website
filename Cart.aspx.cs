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

            setDefault(user);

            if (!IsPostBack) // First time loading page
            {
                ShipBill.Visible = false;
                btnContinue.Visible = true;
                btnPayWith.Visible = false;
                lblPayConfirmHeader.Text = "";
                lblPayConfirmBody.Text = "";
                PaymentIndicator.Visible = false;
            }

            validateShipBill();
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

        private void setDefault(ApplicationUser user)
        {
            txtFullName.Text = user.Name;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.PhoneNumber;
        }

        protected void btnShowItems_click(object sender, EventArgs e)
        {
            ItemsList.Visible = !ItemsList.Visible;
        }

        protected void btnShowShipBill_click(object sender, EventArgs e)
        {
            ShipBill.Visible = !ShipBill.Visible;
            btnPayWith.Visible = !btnPayWith.Visible;
            btnContinue.Visible = !btnContinue.Visible;
            validateShipBill();
        }

        protected void btnContinue_click(object sender, EventArgs e)
        {
            ShipBill.Visible = true;
            btnPayWith.Visible = true;
            btnContinue.Visible = false;
            validateShipBill();
        }

        private void validateShipBill()
        {
            if (ShipBill.Visible)
                Validate("VGShipBill");
        }

        protected void btnPayWith_click(object sender, EventArgs e)
        {
            validateShipBill();
            string alertContent = "";

            PaymentIndicator.Visible = true;

            //System.Diagnostics.Trace.WriteLine(txtFullName.Text);
            //System.Diagnostics.Trace.WriteLine(txtEmail.Text);
            //System.Diagnostics.Trace.WriteLine(txtAddrL1.Text);
            //System.Diagnostics.Trace.WriteLine(txtAddrL2.Text);
            //System.Diagnostics.Trace.WriteLine(txtAddrCity.Text);
            //System.Diagnostics.Trace.WriteLine(txtAddrPC.Text);
            //System.Diagnostics.Trace.WriteLine(txtAddrState.Text);
            //System.Diagnostics.Trace.WriteLine(txtAddrCountry.Text);

            if (txtFullName != null
                && txtEmail != null
                && txtAddrL1 != null
                && txtAddrCity != null
                && txtAddrPC != null
                && txtAddrState != null
                && txtAddrCountry != null
                && txtPhone != null

                && txtFullName.Text != ""
                && txtEmail.Text != ""
                && txtAddrL1.Text != ""
                && txtAddrCity.Text != ""
                && txtAddrPC.Text != ""
                && txtAddrState.Text != ""
                && txtAddrCountry.Text != ""
                && txtPhone.Text != "")
            {
                alertContent += "Full Name: " + txtFullName.Text;
                alertContent += "\nEmail: " + txtEmail.Text;
                if (txtAddrL2 != null)
                {
                    alertContent += $"\nAddress: {txtAddrL1.Text}, {txtAddrL2.Text}, {txtAddrCity.Text}, {txtAddrPC.Text}, {txtAddrState.Text}, {txtAddrCountry.Text}.";
                }
                alertContent += $"\nAddress: {txtAddrL1.Text}, {txtAddrCity.Text}, {txtAddrPC.Text}, {txtAddrState.Text}, {txtAddrCountry.Text}.";
                alertContent += "\nPhone: " + txtPhone.Text;
                alertContent += "\nPaid with ";

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
                    alertContent = "Please select a payment method.";
                }

                lblPayConfirmHeader.Text = "Payment Successful";
                lblPayConfirmBody.Text = alertContent;

                System.Diagnostics.Trace.WriteLine(alertContent);
            }
            else
            {
                lblPayConfirmHeader.Text = "Payment Failed";
                lblPayConfirmBody.Text = "Please fill up the necessary details.";
                System.Diagnostics.Trace.WriteLine("Please fill up shipping details.");
            }
        }
    }
}