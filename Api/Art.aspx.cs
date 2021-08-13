using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Utils;

namespace ArtGalleryWebsite.Api
{
    public partial class Art : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string Create(string description, string style, string url, decimal price, int stock, int authorId)
        {
            // First get the current http context
            HttpContext Context = new Art().Context;

            // Define the response object
            ApiResponse<Dictionary<string, object>> res = new ApiResponse<Dictionary<string, object>>(new Dictionary<string, object>());

            try
            {
                // Insert a new data into the DB
                int rowsAffected = Database.Insert($@"
                    INSERT INTO [Art] ([description], [style], [url], [stock], [price], author_id)
                    VALUES ('{description}', '{style}', '{url}', {stock}, {price}, {authorId});
                ");
                if (rowsAffected < 1) throw new Exception("No rows inserted");
                res.Data["success"] = true;
                res.Error = null;
            }
            catch (Exception)
            {
                res.Error = new Error
                {
                    ErrorType = ErrorType.ErrorTypeRequest,
                    ErrorCode = ErrorCode.ErrorCodeDBTransactionFailed,
                    Message = new Exception().Message,
                    HTTPStatusCode = 404
                };
                Context.Response.StatusCode = 404;
            }

            return Helper.SerializeObject(res);
        }
    }
}