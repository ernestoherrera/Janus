using JanusCore.Extensions;
using JanusCore.Injection;
using JanusCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Janus.Injection
{
    public class InjectionMapper
    {
        private const string ROOT_NAMESPACE = "Janus";

        private readonly IDictionary<string, Type> interfaces;
        private readonly IDictionary<string, Type> implementations;

        #region Constructors

        protected InjectionMapper()
        {
            this.interfaces = new Dictionary<string, Type>();
            this.implementations = new Dictionary<string, Type>();
        }

        public InjectionMapper(IEnumerable<Assembly> assemblies)
            : this()
        {
            if (assemblies == null)
                return;

            var types = assemblies.SelectMany(a => a.GetLoadableTypes()).ToList();


            this.interfaces = types.Where(t => t.IsInterface && !String.IsNullOrEmpty(t.Namespace)).ToDictionary(t => t.FullName);
            this.implementations = types.Where(t => t.IsClass && !t.IsAbstract && !String.IsNullOrEmpty(t.Namespace)).ToDictionary(t => t.FullName);
        }

        #endregion        

        public void RegisterByAttribute(IInjectionContainer container)
        {
            foreach (var implementation in implementations.Values)
            {
                var service = GetInterface(implementation);
                var scope = GetScope(implementation);

                if (service == null && scope == null)
                    continue;

                RegisterService(container, service, implementation, scope ?? InjectionScopeEnum.Transient);
            }
        }

        private void RegisterService(IInjectionContainer container, Type service, Type implementation, InjectionScopeEnum scope)
        {
            if (implementation == null)
                return;

            if (service == null)
                container.Register(implementation, scope);
            else
                container.Register(service, implementation, scope);
        }

        private Type GetInterface(Type implementation)
        {
            var interfaceName = "I" + implementation.Name;
            var interfaceNamespace = implementation.Namespace ?? string.Empty;

            var qualifiedName = interfaceNamespace + "." + interfaceName;

            if (interfaces.ContainsKey(qualifiedName))
                return interfaces[qualifiedName];

            qualifiedName = interfaceNamespace + ".Contracts." + interfaceName;

            if (interfaces.ContainsKey(qualifiedName))
                return interfaces[qualifiedName];

            return null;
        }

        private InjectionScopeEnum? GetScope(Type type)
        {
            if (type == null || type.Namespace == null || !type.Namespace.StartsWith(ROOT_NAMESPACE))
                return null;

            var scopeAttribute = type.GetCustomAttribute<InjectableAttribute>();

            if (scopeAttribute != null)
                return scopeAttribute.Scope;

            var scope = type.GetDirectInterfaces().Select(GetScope).FirstOrDefault(s => s != null);

            return scope ?? GetScope(type.BaseType);
        }
    }
}