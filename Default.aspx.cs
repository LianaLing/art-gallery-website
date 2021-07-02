using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGalleryWebsite
{
    public partial class _Default : Page
    {
        public int counter
        {
            get
            {
                // This code is using the ViewState to preserve the counter state
                // see https://www.tutorialspoint.com/asp.net/asp.net_managing_state.htm
                if (ViewState["pcounter"] != null)
                {
                    return ((int)ViewState["pcounter"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["pcounter"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // This counter works because every button click will trigger
            // a new page load, and hence the counter increases
            counter++;
            lblCounter.Text = counter.ToString();
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            // Instead of Console.WriteLine(), we use the following to write
            // output to the Debug console
            System.Diagnostics.Debug.WriteLine("I am clicked from client side");
        }
    }
}