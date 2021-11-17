using ArtGalleryWebsite.Models;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using ArtGalleryWebsite.Models.Entities;

namespace ArtGalleryWebsite.Utils
{
    public struct AuthAction
    {
        public const string Login = "login";
        public const string Signup = "signup";
        public const string Logout = "logout";
        public const string ChangeRole = "changerole";
        public const string Delete = "delete";
    }

    public static class IdentityHelper
    {
        // Used for XSRF when linking external logins
        public const string XsrfKey = "XsrfId";

        public const string ProviderNameKey = "providerName";
        public static string GetProviderNameFromRequest(HttpRequest request)
        {
            return request.QueryString[ProviderNameKey];
        }

        public const string CodeKey = "code";
        public static string GetCodeFromRequest(HttpRequest request)
        {
            return request.QueryString[CodeKey];
        }

        public const string UserIdKey = "userId";
        public static string GetUserIdFromRequest(HttpRequest request)
        {
            return HttpUtility.UrlDecode(request.QueryString[UserIdKey]);
        }

        // TODO: This has to change to our own Url
        public static string GetResetPasswordRedirectUrl(string code, HttpRequest request)
        {
            var absoluteUri = "/Account/ResetPassword?" + CodeKey + "=" + HttpUtility.UrlEncode(code);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        // TODO: This has to change to our own url
        public static string GetUserConfirmationRedirectUrl(string code, string userId, HttpRequest request)
        {
            var absoluteUri = "/Account/Confirm?" + CodeKey + "=" + HttpUtility.UrlEncode(code) + "&" + UserIdKey + "=" + HttpUtility.UrlEncode(userId);
            return new Uri(request.Url, absoluteUri).AbsoluteUri.ToString();
        }

        private static bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response)
        {
            if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
            {
                response.Redirect(returnUrl);
            }
            else
            {
                response.Redirect("/Home.aspx");
            }
        }

        public static string GetRedirectUrl(string returnUrl)
        {
            if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }
            else
            {
                return "/Home.aspx";
            }
        }

        public static string GetAuthRedirectUrl(string returnUrl, string authAction, Dictionary<string, string> extraParams = null)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString.Add("action", authAction);

            if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
                queryString.Add("returnUrl", returnUrl);

            if (extraParams != null)
                foreach (KeyValuePair<string, string> param in extraParams)
                    queryString.Add(param.Key, param.Value);

            return $"Api/Auth.aspx?{queryString}";
        }

        public static Dictionary<string, object> FilterUser(ApplicationUser user)
        {
            // If user is null
            if (user == null)
                return null;

            // Extract all the user's orders out
            var orders = user.Orders.Select(o => new
            {
                Id = o.Id,
                Status = o.Status,
                Remark = o.Remark,
                CreatedAt = o.CreatedAt,
                UpdatedAt = o.UpdatedAt,
                PaymentId = o.PaymentId,
                UserId = o.UserId,
                AddressId = o.AddressId,
                Address = o.Address,
            }).ToList();

            // Extract all the user's paymentmethods out
            var paymentMethods = user.PaymentMethods.Select(pm => new
            {
                Id = pm.Id,
                Type = pm.Type,
                CreatedAt = pm.CreatedAt,
                UpdatedAt = pm.UpdatedAt,
                UserId = pm.UserId,
                CardId = pm.CardId,
                Card = pm.Card,
                BillingDetailsId = pm.BillingDetailsId,
                BillingDetails = pm.BillingDetails == null ? null : new
                {
                    Id = pm.BillingDetails.Id,
                    Name = pm.BillingDetails.Name,
                    Email = pm.BillingDetails.Email,
                    Phone = pm.BillingDetails.Phone,
                    CreatedAt = pm.BillingDetails.CreatedAt,
                    UpdatedAt = pm.BillingDetails.UpdatedAt,
                    AddressId = pm.BillingDetails.AddressId,
                    Address = pm.BillingDetails.Address
                }
            }).ToList();

            var intermediate = new
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                LockoutEnabled = user.LockoutEnabled,
                Claims = user.Claims,
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Ic = user.Ic,
                Dob = user.Dob,
                AvatarUrl = user.AvatarUrl,
                AuthorId = user.AuthorId,
                Orders = orders,
                PaymentMethods = paymentMethods,
            };

            // First we serialize the user as JSON
            string json = Helper.SerializeObject(intermediate);

            // Then deserialize it back to a dictionary
            Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            return dictionary;
        }
    }
}
