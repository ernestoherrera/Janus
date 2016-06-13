using JanusModels.Models;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Authentication
{
    public class AuthenticatedUser : IUserIdentity
    {
        public int Id { get; }
        public string UserName { get; }

        public IEnumerable<string> Claims { get; }

        public AuthenticatedUser(User user)
        {
            if (user == null)
                throw new ArgumentException("user cannot be null");

            Id = user.Id;
            UserName = user.UserName;
        }
    }
}