using JanusCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusCore.Injection
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class InjectableAttribute : Attribute
    {
        public InjectionScopeEnum Scope { get; private set; }

        public InjectableAttribute()
        {
            Scope = InjectionScopeEnum.Transient;
        }

        public InjectableAttribute(InjectionScopeEnum scope)
        {
            Scope = scope;
        }
    }
}
