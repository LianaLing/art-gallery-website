using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ArtGalleryWebsite.DAL;

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

            if (user.AuthorId == null)
            {
                lblErr.Visible = true;
                lblErr.Text = "Your user is not registered correctly, please contact support";
            }

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    IEnumerable<Art> arts = unitOfWork.ArtRepository.Get(filter: art => art.AuthorId == user.AuthorId, orderBy: art => art.OrderBy(a => a.Likes));

                    if (arts.Count() > 0 && arts != null)
                    {
                        ArtsGrid.DataSource = arts;
                        DashboardLabel.Visible = false;
                    }
                    else
                    {
                        DashboardLabel.Visible = true;
                        DashboardLabel.Text = "You haven't uploaded any artwork yet <img src='https://api.iconify.design/twemoji:lying-face.svg' style='width: 2rem; margin-left: 0.5rem;' />";
                    }

                    // Update the grid
                    ArtsGrid.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(ex);
                    lblErr.Visible = true;
                    lblErr.Text = "Error finding arts, this is probably a system bug";
                }
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
            // Get the data from controls
            int id = Convert.ToInt32(((Label)ArtsGrid.Rows[e.RowIndex].FindControl("ArtId")).Text);
            string url = ((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ImageUrlEdit")).Text;
            string description = ((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ArtDescriptionEdit")).Text;
            decimal price = Convert.ToDecimal(((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ArtPriceEdit")).Text);
            int stock = Convert.ToInt32(((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ArtStockEdit")).Text);
            string style = ((TextBox)ArtsGrid.Rows[e.RowIndex].FindControl("ArtStyleEdit")).Text;

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    // Fetch the original art from db
                    Art art = unitOfWork.ArtRepository.GetById(id);
                    if (art == null) throw new Exception($"Art with id {id} does not exist");

                    // Replace with updated param
                    art.Url = url;
                    art.Description = description;
                    art.Price = price;
                    art.Stock = stock;
                    art.Style = style;

                    // Execute the update
                    unitOfWork.ArtRepository.Update(art);
                    unitOfWork.Save();
                }
                catch (Exception)
                {
                    // Set the error message
                    lblErr.Visible = true;
                    lblErr.Text = $"Error updating art with id {id}<img src='https://api.iconify.design/twemoji:crying-face.svg' style='width: 2rem; margin-left: 0.5rem;' />, please try again.";
                }
            }

            ArtsGrid.EditIndex = -1;
            _BindData();
        }

        protected void ArtsGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ArtsGrid.EditIndex = -1;
            _BindData();
        }

        protected void ArtsGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(((Label)ArtsGrid.Rows[e.RowIndex].FindControl("ArtId")).Text);

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                try
                {
                    if (unitOfWork.ArtRepository.Delete(id) == null) throw new Exception($"Error deleting art with id {id}");
                    unitOfWork.Save();
                }
                catch (Exception)
                {
                    lblErr.Visible = true;
                    lblErr.Text = $"Error deleting art with id {id}<img src='https://api.iconify.design/twemoji:crying-face.svg' style='width: 2rem; margin-left: 0.5rem;' />, please try again.";
                }
            }

            _BindData();
        }

        protected void RefreshData_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("I am Called");
            _BindData();
        }
    }
}