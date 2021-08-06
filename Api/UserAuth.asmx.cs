using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using ArtGalleryWebsite.Models;

namespace ArtGalleryWebsite
{
    /// <summary>
    /// Summary description for UserAuth
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class UserAuth : System.Web.Services.WebService
    {
        const string AUTH_REDIRECT_BASE = "Auth";

        // Since C# enum doesn't support string values, we use struct instead
        struct AccountType
        {
            public const string Personal = "personal";
            public const string Artist = "artist";
        }

        // This endpoint is for logging in user
        [WebMethod]
        public string Login(string email, string password)
        {
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationSignInManager signInManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            // Create a login response object
            ApiResponse<LoginResponse> res = new ApiResponse<LoginResponse>(new LoginResponse());

            SignInStatus result = signInManager.PasswordSignIn(email, password, isPersistent: false, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    break;
                case SignInStatus.LockedOut:
                    break;
                case SignInStatus.RequiresVerification:
                    break;
                case SignInStatus.Failure:
                    break;
                default:
                    break;
            }

            // Check whether email and password given matches a valid user
            if (FormsAuthentication.Authenticate(email, password))
            {
                // if match, send a redirectUrl back to client
                res.data.redirectUrl = $"{AUTH_REDIRECT_BASE}/Login.aspx?email={email}&password={password}";
            }
            else
            {
                // else send an error to the client
                res.data = null;
                res.error = new Error(ErrorType.ErrorTypeRequest, ErrorCode.ErrorCodeEmailOrPasswordInvalid, "Invalid Email or Password", 404);
            }

            // finally send the serialized JSON string
            return JsonConvert.SerializeObject(res);
        }

        // NOTE: This endpoint is not completed yet
        [WebMethod]
        public string SignUp(string email, string password, string type)
        {
            // Check the account type
            switch (type)
            {
                case AccountType.Personal:
                    break;
                case AccountType.Artist:
                    break;
            }
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("email", email);
            map.Add("password", password);
            map.Add("type", type);

            return JsonConvert.SerializeObject(map);
        }
    }
}
