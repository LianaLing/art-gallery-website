using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Utils;
using ArtGalleryWebsite.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ArtGalleryWebsite.DAL;

namespace ArtGalleryWebsite
{
  public partial class Dashboard : System.Web.UI.Page
  {
    private UnitOfWork unitOfWork = new UnitOfWork();

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
        ErrorLabel.Visible = true;
        ErrorLabel.Text = "Your user is not registered correctly, please contact support";
      }

      try
      {
        //List<Models.Entities.Art> arts = Database.Select<Models.Entities.Art>($"SELECT * FROM Art WHERE author_id = {user.AuthorId};");

        IEnumerable<Art> arts = unitOfWork.ArtRepository.Get(filter: art => art.AuthorID == user.AuthorId, orderBy: art => art.OrderBy(a => a.Likes));

        if (arts.Count() > 0 && arts != null)
        {
          ArtsGrid.DataSource = arts;
          ArtsGrid.DataBind();
        }
        else
        {
          DashboardLabel.Visible = true;
          DashboardLabel.Text = "You haven't uploaded any artwork yet <img src='https://api.iconify.design/twemoji:lying-face.svg' style='width: 2rem; margin-left: 0.5rem;' />";
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Trace.WriteLine(ex);
        ErrorLabel.Visible = true;
        ErrorLabel.Text = "Error finding arts, this is probably a system bug";
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

      try
      {
        // Update the art in database
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
        // Set the error message
        ErrorLabel.Visible = true;
        ErrorLabel.Text = $"Error updating art with id {id}<img src='https://api.iconify.design/twemoji:crying-face.svg' style='width: 2rem; margin-left: 0.5rem;' />, please try again.";
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

      try
      {
        // Delete the art in database
        Database.Delete($@"DELETE FROM [Art]
                                    WHERE [id] = {id}");
      }
      catch (Exception)
      {
        ErrorLabel.Visible = true;
        ErrorLabel.Text = $"Error deleting art with id {id}<img src='https://api.iconify.design/twemoji:crying-face.svg' style='width: 2rem; margin-left: 0.5rem;' />, please try again.";
      }

      _BindData();
    }

    protected void RefreshData_Click(Object sender, EventArgs e)
    {
      System.Diagnostics.Trace.WriteLine("I am Called");
      _BindData();
    }
  }
}