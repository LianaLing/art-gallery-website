using ArtGalleryWebsite.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace ArtGalleryWebsite.Utils
{
    public static class Helper
    {
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

        // Helps to inject session state to client side
        public static void InjectSessionState(System.Web.UI.Page page, HttpSessionState session, string id = "session")
        {
            page.ClientScript.RegisterHiddenField(id, SerializeObject(GetSessionDatas(session)));
        }

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
    }
}