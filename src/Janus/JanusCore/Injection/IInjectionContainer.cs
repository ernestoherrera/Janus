using JanusCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JanusCore.Injection
{
    public interface IInjectionContainer
    {
        void Register<TImplementation>(InjectionScopeEnum scope = InjectionScopeEnum.Transient, TImplementation instance = null)
            where TImplementation : class;

        void Register(Type implementation, InjectionScopeEnum scope = InjectionScopeEnum.Transient, object instance = null);

        void Register<TService, TImplementation>(InjectionScopeEnum scope = InjectionScopeEnum.Transient, TImplementation instance = null)
            where TService : class
            where TImplementation : class, TService;

        void Register(Type service, Type implementation, InjectionScopeEnum scope = InjectionScopeEnum.Transient, object instance = null);

        T Get<T>()
            where T : class;
    }
}
