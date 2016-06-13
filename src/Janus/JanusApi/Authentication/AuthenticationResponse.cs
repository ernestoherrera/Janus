using JanusModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Authentication
{
    public class AuthenticationResponse
    {
        public string Username { get; }
        public string FullName { get; }
        public string Token { get; }
        public bool IsTemporary { get; }

        public bool Success { get; }
        public string Message { get; }

        public AuthenticationResponse(User user, string token, bool isTemporary)
        {
            Success = user != null;

            Token = token;
            IsTemporary = isTemporary;

            if (user != null)
            {
                Username = user.UserName;
                FullName = string.Format("{0} {1}", user.UserName, user.UserName).Trim();
            }
        }

        public AuthenticationResponse(bool success, string message)
        {
            Success = success;
            Message = message;

            IsTemporary = true;
        }
    }
}