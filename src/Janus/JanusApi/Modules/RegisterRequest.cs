using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Modules
{
    public class RegisterRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
    }
}