using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Authentication
{
    public class RequestInfo
    {
        public string AccessToken { get; set; }

        public bool IsWellFormed => !string.IsNullOrWhiteSpace(AccessToken);

        public RequestInfo(Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Headers.Authorization))
                return;

            var header = request.Headers.Authorization.Trim();

            var headerParts = header.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (headerParts.Count != 2)
                return;

            if (headerParts[0].Equals("bearer", StringComparison.CurrentCultureIgnoreCase))
                this.AccessToken = headerParts[1];

        }
    }
}