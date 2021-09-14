using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGalleryWebsite
{
    public partial class UserDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // DOB should not be in the future
            CompareDOB.ValueToCompare = DateTime.Today.ToShortDateString();
            if (!IsPostBack)
            {
            }// Compare username with database
        }

        protected void calDOB_changed(Object sender, EventArgs e)
        {
            txtDOB.Text = calDOB.SelectedDate.ToShortDateString();
        }

        protected void calDOB_dayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date > DateTime.Today)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }
        }

        protected void btnCancel_click(Object sender, EventArgs e)
        {

        }

        protected void btnSubmit_click(Object sender, EventArgs e)
        {

        }
    }
}