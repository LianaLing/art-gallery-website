using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.DTO;
using ArtGalleryWebsite.DAL;
using ArtGalleryWebsite.DAL.Extensions;
using ArtGalleryWebsite.Models.Entities;
using ArtGalleryWebsite.User_Control;
using ArtGalleryWebsite.Utils;

namespace ArtGalleryWebsite
{
    public partial class Cart : System.Web.UI.Page
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        private ApplicationUser user = null;

        private static bool cardDetailSubmitted = false;

        protected List<CartItemDTO> cartItems;

        protected static CartItemDTO ci;
        protected static int btnCiCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get current session user
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = manager.FindById(Page.User.Identity.GetUserId<int>());
            user = currentUser;

            // Fetch cart items
            cartItems = fetchCartItems(user.Id);

            // Render cart items
            renderCartItems(cartItems);

            // Calculate
            decimal subtotal = cartItems.Sum(ci => ci.Art.Price);
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
                btnPayWith.Enabled = true;
                btnCiCount = 0;
            }

            validateShipBill();

            // Compare credit card expiration date to ensure it is not in the past
            CompareExpDate.ValueToCompare = DateTime.Today.ToShortDateString();
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex is HttpUnhandledException)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }

        // Fetch cart items from database
        protected List<CartItemDTO> fetchCartItems(int user_id)
        {
            // Check whether user has a cart
            ShoppingCart cart = unitOfWork.GetShoppingCartByUserId(user_id);
            if (cart == null) throw new Exception($"User {user_id} has no items in cart.");

            // Fetch cart items
            List<CartItemDTO> cartItems = unitOfWork.GetCartItems(cart.Id);
            if (cartItems.Count == 0) throw new Exception($"User {user_id} has no items in cart.");

            return cartItems;
        }

        // Render cartItems to frontend using dynamic user control
        protected void renderCartItems(List<CartItemDTO> cartItems)
        {
            Dictionary<int, Pair<CartItemDTO, int>> grouped = new Dictionary<int, Pair<CartItemDTO, int>>();

            foreach (var ci in cartItems)
            {
                if (grouped.ContainsKey(ci.Art.Id))
                    grouped[ci.Art.Id].Second++;
                else
                    grouped[ci.Art.Id] = new Pair<CartItemDTO, int> { First = ci, Second = 1 };
            }

            foreach (var ci in grouped)
            {
                CartItemNoQty ciControl = (CartItemNoQty)LoadControl("~/User_Control/CartItemNoQty.ascx");
                ciControl.CartItem = ci.Value.First;
                ciControl.Quantity = ci.Value.Second;
                ItemsList.Controls.Add(ciControl);
            }
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

        private void setDefault(Models.Identity.User user)
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
            //Get card from database
            txtCardNo.Text = "4978095994255901";
            txtExpDate.Text = "01/10/2025";
        }

        private void validateShipBill()
        {
            if (ShipBill.Visible && allFieldsEnabled())
                Validate("VGShipBill");
        }

        private bool allFieldsEnabled()
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

        private void validateCardDetail()
        {
            if (CardDetail.Visible)
            {
                Validate("VGCardDetail");
            }
        }

        protected void cboxDefaultAddr_change(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("Checked");
            if (cboxDefaultAddr.Checked) // Using default address, disable input
            {
                // Change addr text to default address

                enableAddr(false);
            }
            else
            {
                enableAddr(true);
            }
        }

        protected void btnClearCart_click(object sender, EventArgs e)
        {
            // Remove all items from cart
            System.Diagnostics.Trace.WriteLine("Clicked on clear cart button");
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

        protected void btnSubmitCard_click(object sender, EventArgs e)
        {
            validateCardDetail();
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

            // Insert to database
            // CreateOrderFunc (create order -> create billigndetails -> create paymentmethod -> create payment -> update order)
            Order order = unitOfWork.CreateTransaction(user.Id, CreateTransactionParams());
            if (order == null) throw new Exception($"Unable to create order");
            System.Diagnostics.Trace.WriteLine(order);

            // Wrap up the transaction, send email
            // ConfirmPaymentFunc (update payment succeed -> update order succeed)
            unitOfWork.ConfirmPaymentAndOrder(order.Id);

            // Clear cart
            unitOfWork.ClearCart(user.Id);
            Session["cart"] = null;

            // SendEmailFunc

            // Refresh page (TODO: redirect to success page)
            Server.TransferRequest(Request.Url.AbsolutePath, false);
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

        protected PaymentExtension.TransactionParams CreateTransactionParams()
        {
            AddressDTO address = new AddressDTO
            {
                Country = txtAddrCountry.Text,
                State = txtAddrState.Text,
                City = txtAddrCity.Text,
                Line1 = txtAddrL1.Text,
                Line2 = txtAddrL2.Text,
                PostalCode = txtAddrPC.Text,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            PaymentExtension.TransactionParams param = new PaymentExtension.TransactionParams
            {
                Remark = "No Remark",
                Currency = "MYR",
                PaymentDescription = "No Description",
                TaxRate = 0.06M,
                ShippingAddress = address,
                PaymentMethod = new PaymenMethodDTO 
                { 
                    UserId = user.Id,
                    Type = "card", // TODO: Make this dynamic
                    Card = new CardDTO
                    {
                        Brand = ddlCardBrand.SelectedValue,
                        Name = txtFullName.Text,
                        Last4 = txtCardNo.Text.Substring(txtCardNo.Text.Length - 4),
                        ExpMonth = txtExpDate.Text.Split('/')[1],
                        ExpYear = txtExpDate.Text.Split('/')[2],
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    BillingDetails = new BillingDetailsDTO 
                    { 
                        Name = txtFullName.Text,
                        Email = txtEmail.Text,
                        Phone = txtPhone.Text,
                        Address = address,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                },
                CartItems = cartItems
            };

            return param;
        }

        protected void btnPayWith_click(object sender, EventArgs e)
        {
            validateShipBill();
            string alertContent = "";
            if (!CardDetail.Visible && !cardDetailSubmitted)
            {
                CardDetail.Visible = true;
                // Prefill with card if already have
                return;
            }

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
                    alertContent += "Full Name: " + txtFullName.Text;
                    alertContent += "\nEmail: " + txtEmail.Text;
                    if (txtAddrL2 != null || txtAddrL2.Text != "")
                    {
                        alertContent += $"\nAddress: {txtAddrL1.Text}, {txtAddrL2.Text}, {txtAddrCity.Text}, {txtAddrPC.Text}, {txtAddrState.Text}, {txtAddrCountry.Text}.";
                    }
                    else
                    {
                        alertContent += $"\nAddress: {txtAddrL1.Text}, {txtAddrCity.Text}, {txtAddrPC.Text}, {txtAddrState.Text}, {txtAddrCountry.Text}.";

                    }
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
                    btnPayWith.Enabled = false;
                }
                else
                {
                    lblPayConfirmHeader.Text = "Payment Failed";
                    lblPayConfirmBody.Text = "Please fill up the necessary details.";
                    System.Diagnostics.Trace.WriteLine("Please fill up shipping details.");
                }
            }

        }

        protected void btnClearCart_Click(object sender, EventArgs e)
        {
            unitOfWork.ClearCart(user.Id);
            Session["cart"] = null;
            Response.Redirect("~/Home.aspx");
        }
    }
}
