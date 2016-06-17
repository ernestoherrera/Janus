using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janus.Contracts
{
    public interface IContextProvider
    {
        NancyContext Context { get; set; }
        Request Request { get; }
    }
}
