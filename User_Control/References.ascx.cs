using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGalleryWebsite
{
    public partial class References : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string CreditRef
        {
            get { return lbtnRef.PostBackUrl; }
            set { lbtnRef.PostBackUrl = value; }
        }

        public string Alt
        {
            get { return lbtnRef.Text; }
            set { lbtnRef.Text = value; }
        }

        public string Author
        {
            get { return lblAuthor.Text; }
            set { lblAuthor.Text = value; }
        }

    }
}