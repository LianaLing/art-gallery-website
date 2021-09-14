using System;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
    }
}