using System;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ArtGalleryWebsite.Models.Entities;
using ArtGalleryWebsite.DAL;
using ArtGalleryWebsite.Models.DTO;
using System.Net.Mail;
using System.Net;

namespace ArtGalleryWebsite.Utils
{
    public static class Helper
    {
        private static SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

        // Helps to retrieve session data from the session object
        public static Dictionary<string, object> GetSessionDatas(HttpSessionState Session)
        {
            // Get the enumerator (similar to iterator in Java)
            System.Collections.IEnumerator ie = Session.GetEnumerator();
            Dictionary<string, object> values = new Dictionary<string, object>();
            string currentKey;

            // Iterate through using enumerator
            while (ie.MoveNext())
            {
                currentKey = (string)ie.Current;
                values.Add(currentKey, Session[currentKey]);
            }

            return values;
        }

        // Helps to register hidden state to client side
        public static void RegisterHiddenField(Page page, string id, object data)
        {
            page.ClientScript.RegisterHiddenField(id, SerializeObject(data));
        }

        // Helps to inject session state to client side
        public static void InjectSessionState(Page page, HttpSessionState session, string id = "session")
        {
            RegisterHiddenField(page, id, GetSessionDatas(session));
        }

        // Serialize object to JSON string (using CamelCase)
        public static string SerializeObject(object value, bool camelCase = true, bool formatIndent = false)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(value, new JsonSerializerSettings 
            { 
                ContractResolver = camelCase ? contractResolver : null,
                Formatting = formatIndent ? Formatting.Indented : Formatting.None,
            });
        }

        // This function helps write a json response by flushing and
        // immediately ending the current request which will cause a 
        // ThreadAbortException, use with caution
        public static void WriteJsonResponse(HttpContext context, string response, int statusCode = 200)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            context.Response.Clear();
            context.Response.Write(response);
            context.Response.Flush();
            context.Response.End();
        }
        public static void UpdateCartSessionStateIfNull(UnitOfWork unitOfWork, HttpSessionState session, int user_id)
        {
            if (session["cart"] == null)
            {
                // User's shopping cart
                List<ShoppingCart> found = (List<ShoppingCart>)unitOfWork.ShoppingCartRepository.Get(c => c.UserId == user_id);

                // If no cart, set the session as null
                if (found == null || found.Count == 0)
                {
                    session["cart"] = null;
                    return;
                }

                // Take away unwanted properties
                ShoppingCartDTO cart = new ShoppingCartDTO
                {
                    Id = found[0].Id,
                    Total = found[0].Total,
                    UserId = found[0].UserId
                };

                // Set the user's shopping cart as a session state
                session["cart"] = cart;
            }
        }

        public static void SendEmail(string recipientEmail, string subject, string body)
        {
            using (MailMessage mm = new MailMessage())
            {
                mm.From = new MailAddress(secObj.From); //--- Email address of the sender
                mm.To.Add(recipientEmail); //---- Email address of the recipient.
                mm.Subject = subject; //---- Subject of email.
                mm.Body = body; //---- Content of email.
             
                SmtpClient smtp = new SmtpClient();
                smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
                smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
                NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
                //---Your Email and password
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587; //---- SMTP Server port number. This varies from host to host. 
                smtp.Send(mm);
            }
        }
    }
}