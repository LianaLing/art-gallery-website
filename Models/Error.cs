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
        public const string ErrorTypeAuth = "auth_error";
    }

    // This struct specifies the list possible error codes
    public struct ErrorCode
    {
        public const string ErrorCodeRequestBodyInvalid = "request_body_invalid";
        public const string ErrorCodeEmailOrPasswordInvalid = "email_or_password_invalid";
        public const string ErrorCodeEmailTaken = "email_taken";
        public const string ErrorCodeUserRoleInvalid = "user_role_invalid";
        public const string ErrorCodeDBTransactionFailed = "db_transaction_failed";
    }

    // This class defines the shape of an Error
    // make sure to use constants from ErrorType and ErrorCode
    public class Error
    {
        public string ErrorType { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public int HTTPStatusCode { get; set; }
    }
}