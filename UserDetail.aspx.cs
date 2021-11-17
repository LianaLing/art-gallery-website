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
            // Compare credit card expiration date to ensure it is not in the past
            CompareExpDate.ValueToCompare = DateTime.Today.ToShortDateString();
            if (!IsPostBack)
            {
            }// Compare username with database
            CompareUsername.ValueToCompare = "";
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

        protected void btnCancel_click(Object sender, EventArgs e)
        {

        }

        protected void btnSubmit_click(Object sender, EventArgs e)
        {

        }
    }
}