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
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = manager.FindById(User.Identity.GetUserId<int>());

            switch (Request.QueryString["action"])
            {
                case AuthAction.Signup:
                case AuthAction.Login:
                    Session["user"] = user;
                    break;
                case AuthAction.Delete:
                case AuthAction.Logout:
                    Session.Remove("user");
                    IdentityHelper.RedirectToReturnUrl("/", Response);
                    break;
                default:
                    System.Diagnostics.Trace.WriteLine("Invalid auth action");
                    break;
            }

            IdentityHelper.RedirectToReturnUrl(Request.QueryString["Redirect"], Response);
        }

        // This endpoint is for logging in user
        [WebMethod(true)]
        public static string Login(string email, string password, bool remember)
        {
            HttpContext Context = new Auth().Context;

            ApiResponse<Dictionary<string, object>> res = new ApiResponse<Dictionary<string, object>>(new Dictionary<string, object>());

            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationSignInManager signInManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            SignInStatus signInResult = signInManager.PasswordSignIn(email, password, isPersistent: remember, shouldLockout: false);
            switch (signInResult)
            {
                case SignInStatus.Success:
                    res.Data["redirectUrl"] = IdentityHelper.GetAuthRedirectUrl(Context.Request.QueryString["Redirect"], AuthAction.Login);
                    break;
                case SignInStatus.Failure:
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

            return Helper.SerializeObject(res);
        }

        [WebMethod(true)]
        public static string Signup(string email, string password, string role)
        {
            HttpContext Context = new Auth().Context;

            ApiResponse<Dictionary<string, object>> res = new ApiResponse<Dictionary<string, object>>(new Dictionary<string, object>());

            // Validate role
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

            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationSignInManager signInManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            ApplicationUser user = new ApplicationUser() { UserName = email, Email = email };
            IdentityResult result = manager.Create(user, password);

            if (result.Succeeded)
            {
                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                manager.AddClaim(user.Id, new Claim(ClaimTypes.Role, role));
                res.Data["redirectUrl"] = IdentityHelper.GetAuthRedirectUrl(Context.Request.QueryString["Redirect"], AuthAction.Signup);
            }
            else
            {
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

            return Helper.SerializeObject(res);
        }

        [WebMethod(true)]
        public static string Logout()
        {
            HttpContext Context = new Auth().Context;
            
            ApiResponse<Dictionary<string, object>> res = new ApiResponse<Dictionary<string, object>>(new Dictionary<string, object>());

            IAuthenticationManager authManager = Context.GetOwinContext().Authentication;
            authManager.SignOut();

            res.Data["redirectUrl"] = IdentityHelper.GetAuthRedirectUrl(Context.Request.QueryString["Redirect"], AuthAction.Logout);

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
                res.Data["redirectUrl"] = IdentityHelper.GetAuthRedirectUrl(Context.Request.QueryString["Redirect"], AuthAction.Logout);
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