using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

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
        // Since C# enum doesn't support string values, we use struct instead
        struct AccountType
        {
            public const string Personal = "personal";
            public const string Artist = "artist";
        }

        [WebMethod]
        public string Login(string email, string password)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("email", email);
            map.Add("password", password);

            return JsonConvert.SerializeObject(map);
        }

        [WebMethod]
        public string SignUp(string email, string password, string type)
        {
            // Check the account type
            switch(type)
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
