using Janus.Contracts;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Support
{
    public class ContextProvider : IContextProvider
    {
        public NancyContext Context { get; set; }
        public IUserIdentity CurrentUser => Context.CurrentUser;
        public Request Request => Context?.Request;
    }
}