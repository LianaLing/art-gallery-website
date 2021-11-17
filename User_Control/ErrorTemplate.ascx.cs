using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGalleryWebsite.User_Control
{
    public partial class ErrorTemplate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Int32 HttpCode
        {
            get { return Int32.Parse(lblHttpCode.Text); }
            set { lblHttpCode.Text = value.ToString(); }
        }

        public string ErrBody
        {
            get { return lblErrBody.Text; }
            set { lblErrBody.Text = value; }
        }
    }
}