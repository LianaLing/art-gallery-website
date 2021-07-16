using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGalleryWebsite.Models
{
    // This struct specifies the list possible error types
    public struct ErrorType
    {
        public const string ErrorTypePermission = "permission_error";
        public const string ErrorTypeRequest = "request_error";
    }

    // This struct specifies the list possible error codes
    public struct ErrorCode
    {
        public const string ErrorCodeRequestBodyInvalid = "request_body_invalid";
        public const string ErrorCodeEmailOrPasswordInvalid = "email_or_password_invalid";
    }

    // This class defines the shape of an Error
    // make sure to use constants from ErrorType and ErrorCode
    public class Error
    {
        public string errorType;
        public string errorCode;
        public string message;
        public int HTTPStatusCode;

        public Error(string errorType, string errorCode, string message, int HTTPStatusCode)
        {
            this.errorType = errorType;
            this.errorCode = errorCode;
            this.message = message;
            this.HTTPStatusCode = HTTPStatusCode;
        }
    }
}