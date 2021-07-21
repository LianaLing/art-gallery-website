using ArtGalleryWebsite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Utils
{
    public static class Helper
    {
        // Helps to retrieve session data from the session object
        public static List<SessionData> GetSessionDatas(System.Web.SessionState.HttpSessionState Session)
        {
            // Get the enumerator (similar to iterator in Java)
            System.Collections.IEnumerator ie = Session.GetEnumerator();
            List<SessionData> values = new List<SessionData>();
            string currentKey;

            // Iterate through using enumerator
            while (ie.MoveNext())
            {
                currentKey = (string)ie.Current;
                values.Add(new SessionData(currentKey, Session[currentKey].ToString()));
            }

            return values;
        }

        // Helps to inject session state to client side
        public static void InjectSessionState(System.Web.UI.Page Page, System.Web.SessionState.HttpSessionState Session, string id = "session")
        {
			Page.ClientScript.RegisterHiddenField(id, JsonConvert.SerializeObject(GetSessionDatas(Session)));
        }
    }
}