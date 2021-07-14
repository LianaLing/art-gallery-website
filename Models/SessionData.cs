using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models
{
    // This defines the structure of a session data 
    // which essentially is just a key-value pair
    public class SessionData
    {
        private string _key;
        private string _value;

        public SessionData(string key, string value)
        {
            this._key = key;
            this._value = value;
        }

        public string Key
        {
            get { return _key; }
        }

        public string Value
        {
            get { return _value; }
        }
    }
}