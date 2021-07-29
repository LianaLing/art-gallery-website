using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// This file is to store the response structure of each endpoint
namespace ArtGalleryWebsite.Models
{
    // This defines the structure for response from `/Api/UserAuth.asmx/Login`
    class LoginResponse
    {
        public string redirectUrl;
    }
}