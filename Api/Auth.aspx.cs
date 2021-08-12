using ArtGalleryWebsite.Models;
using ArtGalleryWebsite.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Services;

namespace ArtGalleryWebsite.Api
{
    public partial class Auth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the user manager
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // Get the current user
            ApplicationUser user = manager.FindById(User.Identity.GetUserId<int>());

            // Filter out 'PasswordHash' field
            Dictionary<string, object> filteredUser = IdentityHelper.FilterUser(user);

            // Handle different types of auth action
            switch (Request.QueryString["action"])
            {
                case AuthAction.Signup:
                case AuthAction.Login:
                    Session["user"] = filteredUser;
                    break;
                case AuthAction.Delete:
                case AuthAction.Logout:
                    Session.Remove("user");
                    break;
                default:
                    System.Diagnostics.Trace.WriteLine("Invalid auth action");
                    break;
            }

            // Redirect to path from [Redirect] or '/' if [Redirect] is not provided
            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        }

        // This endpoint is for logging in user
        [WebMethod(true)]
        public static string Login(string email, string password, bool remember)
        {
            // First get the current http context
            HttpContext Context = new Auth().Context;

            // Define the response object
            ApiResponse<Dictionary<string, object>> res = new ApiResponse<Dictionary<string, object>>(new Dictionary<string, object>());

            // UserManager from Identity
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // SignInManager from Identity
            ApplicationSignInManager signInManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            // Sign in the user and get the result
            SignInStatus signInResult = signInManager.PasswordSignIn(email, password, isPersistent: remember, shouldLockout: false);

            // Handle different sign in results
            switch (signInResult)
            {
                case SignInStatus.Success:
                    // Set a redirectUrl in the response
                    res.Data["redirectUrl"] = IdentityHelper.GetAuthRedirectUrl(Context.Request.QueryString["ReturnUrl"], AuthAction.Login);
                    break;
                case SignInStatus.Failure:
                    // Set an error in the response
                    res.Data = null;
                    res.Error = new Error
                    {
                        ErrorType = ErrorType.ErrorTypeRequest,
                        ErrorCode = ErrorCode.ErrorCodeEmailOrPasswordInvalid,
                        Message = "Invalid email or password",
                        HTTPStatusCode = 404
                    };
                    Context.Response.StatusCode = 404;
                    break;
            }

            // Return serialized JSON in camelCase
            return Helper.SerializeObject(res);
        }

        [WebMethod(true)]
        public static string Signup(string email, string password, string role)
        {
            // Get current http context
            HttpContext Context = new Auth().Context;

            // Define the response object
            ApiResponse<Dictionary<string, object>> res = new ApiResponse<Dictionary<string, object>>(new Dictionary<string, object>());

            // Validate role given
            if (role != AuthRole.Artist && role != AuthRole.Personal)
            {
                res.Data = null;
                res.Error = new Error
                {
                    ErrorType = ErrorType.ErrorTypeRequest,
                    ErrorCode = ErrorCode.ErrorUserRoleInvalid,
                    Message = $"{role} is not one of '{AuthRole.Personal}' or '{AuthRole.Artist}'",
                    HTTPStatusCode = 404
                };
                Context.Response.StatusCode = 404;
                return Helper.SerializeObject(res);
            }

            // Get the UserManager and SignInManager from Identity
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationSignInManager signInManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            // Define the new user's properties
            ApplicationUser user = new ApplicationUser() { UserName = email, Email = email };

            // Create a user and get the result
            IdentityResult result = manager.Create(user, password);

            // If account is successfully created
            if (result.Succeeded)
            {
                // Sign in the user
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                // Add the claim (role) to the user
                manager.AddClaim(user.Id, new Claim(ClaimTypes.Role, role));
                // Set a redirectUrl in the response
                res.Data["redirectUrl"] = IdentityHelper.GetAuthRedirectUrl(Context.Request.QueryString["ReturnUrl"], AuthAction.Signup);
            }
            else
            {
                // Set an error in the response
                res.Data = null;
                res.Error = new Error
                {
                    ErrorType = ErrorType.ErrorTypeRequest,
                    ErrorCode = ErrorCode.ErrorCodeEmailTaken,
                    Message = result.Errors.FirstOrDefault(),
                    HTTPStatusCode = 404
                };
                Context.Response.StatusCode = 404;
            }

            // Return serialized JSON in camelCase
            return Helper.SerializeObject(res);
        }

        [WebMethod(true)]
        public static string Logout()
        {
            // Get current http context
            HttpContext Context = new Auth().Context;
            
            // Define the response object
            ApiResponse<Dictionary<string, object>> res = new ApiResponse<Dictionary<string, object>>(new Dictionary<string, object>());

            // Get the AuthManager from Identity
            IAuthenticationManager authManager = Context.GetOwinContext().Authentication;

            // Logout the current user
            authManager.SignOut();

            // Set a redirectUrl in the response
            res.Data["redirectUrl"] = IdentityHelper.GetAuthRedirectUrl(Context.Request.QueryString["ReturnUrl"], AuthAction.Logout);

            // Return serialized JSON in camelCase
            return Helper.SerializeObject(res);
        }

        [WebMethod(true)]
        public static string Delete(string username, string email)
        {
            HttpContext Context = new Auth().Context;

            ApiResponse<Dictionary<string, object>> res = new ApiResponse<Dictionary<string, object>>(new Dictionary<string, object>());

            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            ApplicationUser user = new ApplicationUser() { UserName = username, Email = email };
            IdentityResult result = manager.Delete(user);

            if (result.Succeeded)
            {
                res.Data["redirectUrl"] = IdentityHelper.GetAuthRedirectUrl(Context.Request.QueryString["ReturnUrl"], AuthAction.Logout);
            }
            else
            {
                res.Data = null;
                res.Error = new Error
                {
                    ErrorType = ErrorType.ErrorTypeRequest,
                    ErrorCode = ErrorCode.ErrorCodeEmailOrPasswordInvalid,
                    Message = result.Errors.FirstOrDefault(),
                    HTTPStatusCode = 404
                };
                Context.Response.StatusCode = 404;
            }

            return Helper.SerializeObject(res);
        }
    }
}