using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Authentication
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string deviceId { get; set; }

    }
}