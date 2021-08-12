using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ArtGalleryWebsite
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HandleUserNotArtist();

            if (!IsPostBack)
                _BindData();
        }

        protected void HandleUserNotArtist()
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("/");

            bool userIsArtist = ((System.Security.Claims.ClaimsIdentity)User.Identity).HasClaim(claim => claim.Value.Contains(AuthRole.Artist));

            if (!userIsArtist)
                Response.Redirect("/");
        }

        private void _BindData()
        {
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(User.Identity.GetUserId<int>());

            // TODO: Handle this error by returning
            // some state to frontend
            if (user.AuthorId == null) return;

            try
            {
                List<Models.Entities.Art> arts = Database.Select<Models.Entities.Art>($"SELECT * FROM Art WHERE author_id = {user.AuthorId};");

                if (arts.Count > 0 && arts != null)
                {
                    ArtsGrid.DataSource = arts;
                    ArtsGrid.DataBind();
                } else
                {
                    DashboardLabel.Visible = true;
                    DashboardLabel.Text = "You haven't uploaded any artwork yet <img src='https://api.iconify.design/twemoji:lying-face.svg' class='w-8 h-8 ml-3' />";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ArtsGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                ArtsGrid.EditIndex = e.NewEditIndex;
                _BindData();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ArtsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((Label)ArtsGrid.Rows[e.RowIndex].FindControl("ArtId")).Text);
                string url = ((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ImageUrlEdit")).Text;
                string description = ((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ArtDescriptionEdit")).Text;
                decimal price = Convert.ToDecimal(((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ArtPriceEdit")).Text);
                int stock = Convert.ToInt32(((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ArtStockEdit")).Text);
                string style = ((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ArtStyleEdit")).Text;

                try
                {
                    Database.Update($@"UPDATE [Art]
                                       SET [url] = '{url}',
                                           [description] = '{description}',
                                           [price] = {price},
                                           [stock] = {stock},
                                           [style] = '{style}'
                                       WHERE [id] = {id}");
                }
                catch (Exception)
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = $"Error updating art with id {id}<img src='https://api.iconify.design/twemoji:crying-face.svg' class='w-8 h-8 ml-3' />, please try again.";
                }

                ArtsGrid.EditIndex = -1;
                _BindData();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ArtsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ArtsGrid.EditIndex = -1;
            _BindData();
        }
    }
}