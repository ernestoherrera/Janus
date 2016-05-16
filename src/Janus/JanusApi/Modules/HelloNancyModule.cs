using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Modules
{
    public class HelloNancyModule : NancyModule
    {
        public HelloNancyModule()
        {
            Get["/"] = parameters => "Hello Nancy";
        }
    }
}
