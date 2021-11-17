using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGalleryWebsite.User_Control
{
    public partial class ErrorLabel : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Text
        {
            get { return lblErrInner.Text; }
            set { lblErrInner.Text = value; }
        }

    }
}