using System;
using System.Collections.Generic;
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

        private static bool cardDetailSubmitted = false;

        protected IEnumerable<ArtQuery> Arts;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get current session user
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(Page.User.Identity.GetUserId<int>());

            // Get data for the page
            IEnumerable<ArtQuery> data = selectAllArt();

            // Register hidden field to pass data from backend to frontend
            //registerHiddenField("arts", data);

            Arts = data.Select(d =>
            {
                d.price = decimal.Round(d.price, 2, MidpointRounding.AwayFromZero);
                return d;
            });

            // Calculate
            decimal subtotal = Arts.Sum(d => d.price);
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
                CardDetail.Visible = false;
                cardDetailSubmitted = false;
                enableFields(true);
            }

            validateShipBill();

            // Compare credit card expiration date to ensure it is not in the past
            CompareExpDate.ValueToCompare = DateTime.Today.ToShortDateString();
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
            // Get user address from database
            txtAddrL1.Text = "Test addr line 1";
            txtAddrL2.Text = "Test addr line 2";
            txtAddrCity.Text = "Test addr city";
            txtAddrPC.Text = "40000";
            txtAddrState.Text = "Test addr state";
            txtAddrCountry.Text = "Test addr country";
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
            if (ShipBill.Visible && allFieldEnabled())
                Validate("VGShipBill");
        }

        private bool allFieldEnabled()
        {
            if (txtFullName.Enabled && txtEmail.Enabled && cboxDefaultAddr.Enabled && txtPhone.Enabled && addrEnabled())
            {
                return true;
            }
            return false;
        }

        private bool addrEnabled()
        {
            if (txtAddrL1.Enabled
            && txtAddrL2.Enabled
            && txtAddrCity.Enabled
            && txtAddrPC.Enabled
            && txtAddrState.Enabled
            && txtAddrCountry.Enabled)
            {
                return true;
            }
            return false;
        }

        private void enableFields(bool state)
        {
            txtFullName.Enabled = state;
            txtEmail.Enabled = state;
            cboxDefaultAddr.Enabled = state;
            enableAddr(state);
            txtPhone.Enabled = state;
        }

        private void enableAddr(bool state)
        {
            txtAddrL1.Enabled = state;
            txtAddrL2.Enabled = state;
            txtAddrCity.Enabled = state;
            txtAddrPC.Enabled = state;
            txtAddrState.Enabled = state;
            txtAddrCountry.Enabled = state;
        }

        protected void cboxDefaultAddr_change(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Checked");
            if (cboxDefaultAddr.Checked) // Using default address, disable input
            {
                enableAddr(false);
            }
            else
            {
                enableAddr(true);
            }
        }

        protected void btnSubmitCard_click(object sender, EventArgs e)
        {
            validateCardDetail();
            System.Diagnostics.Trace.WriteLine(RegexCardNo.IsValid);
            if (RegexCardNo.IsValid
                && ReqCardNo.IsValid
                && ReqExpDate.IsValid
                && CompareExpDate.IsValid
                && ReqCVV.IsValid
                && RegexCVV.IsValid)
            {
                CardDetail.Visible = false;
                cardDetailSubmitted = true;
                // Insert card to database
                btnPayWith.Text = "Confirm Payment With Card";
            }
            else
            {
                CardDetail.Visible = true;
                cardDetailSubmitted = false;
            }
        }

        private void validateCardDetail()
        {
            if (CardDetail.Visible)
            {
                Validate("VGCardDetail");
            }
        }

        protected void ddlCardBrand_change(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Selection index changed");
            if (ddlCardBrand.SelectedValue == "visa")
            {
                RegexCardNo.ValidationExpression = "^4[0-9]{12}(?:[0-9]{3})?$";
            }
            else if (ddlCardBrand.SelectedValue == "mastercard")
            {
                RegexCardNo.ValidationExpression = "^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$";
            }
            else if (ddlCardBrand.SelectedValue == "amex")
            {
                RegexCardNo.ValidationExpression = "^3[47][0-9]{13}$";
            }
            else if (ddlCardBrand.SelectedValue == "unionpay")
            {
                RegexCardNo.ValidationExpression = "^(62[0-9]{14,17})$";
            }
        }

        protected void btnPayWith_click(object sender, EventArgs e)
        {
            validateShipBill();
            string alertContent = "";
            if (!CardDetail.Visible && !cardDetailSubmitted)
                CardDetail.Visible = true;

            if (cardDetailSubmitted)
            {
                PaymentIndicator.Visible = true;

                if (txtFullName != null
                    && txtEmail != null
                    && txtAddrL1 != null
                    && txtAddrCity != null
                    && txtAddrPC != null
                    && txtAddrState != null
                    && txtAddrCountry != null
                    && txtPhone != null
                    && ddlCardBrand != null
                    && txtCardNo != null

                    && txtFullName.Text != ""
                    && txtEmail.Text != ""
                    && txtAddrL1.Text != ""
                    && txtAddrCity.Text != ""
                    && txtAddrPC.Text != ""
                    && txtAddrState.Text != ""
                    && txtAddrCountry.Text != ""
                    && txtPhone.Text != ""
                    && ddlCardBrand.Text != ""
                    && txtCardNo.Text != ""
                    )
                {
                    // Insert to database
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
                        alertContent += ddlCardBrand.Text.ToUpper() + " card: " + txtCardNo.Text;
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
                    enableFields(false);
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
}