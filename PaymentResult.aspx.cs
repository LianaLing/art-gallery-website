using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ArtGalleryWebsite.DAL;
using ArtGalleryWebsite.DAL.Extensions;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.DTO;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite
{
    public partial class PaymentResult : System.Web.UI.Page
    {
        protected ApplicationUser user = null;
        protected Order order = null;
        protected Payment payment = null;
        protected string error = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the order_id from query string
            string order_id_str = Request.QueryString["order_id"];
            if (String.IsNullOrEmpty(order_id_str)) return;

            int order_id = int.Parse(order_id_str);

            // Get the current user
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser currentUser = manager.FindById(Page.User.Identity.GetUserId<int>());
            user = currentUser;

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                // Fetch order detail
                order = unitOfWork.OrderRepository.GetById(order_id);
                if (order == null)
                {
                    error = $"Error fetching order with id {order_id}";
                    return;
                }

                // Fetch payment detail
                payment = unitOfWork.PaymentRepository.GetById(order.PaymentId);
                if (payment == null)
                { 
                    error = $"Error fetching payment with id {order.PaymentId}";
                    return;
                }
            }
        }
    }
}