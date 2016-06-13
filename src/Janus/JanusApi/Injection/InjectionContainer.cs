using JanusCore.Injection;
using JanusCore.Support;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Injection
{
    public class InjectionContainer : IInjectionContainer
    {
        #region Private Members

        private TinyIoCContainer container;

        #endregion

        #region Constructors

        public InjectionContainer(TinyIoCContainer container)
        {
            this.container = container;
        }

        #endregion

        #region Register

        public void Register<TImplementation>(InjectionScopeEnum scope = InjectionScopeEnum.Transient, TImplementation instance = null)
            where TImplementation : class
        {
            Register(typeof(TImplementation), scope, instance);
        }

        public void Register(Type implementation, InjectionScopeEnum scope = InjectionScopeEnum.Transient, object instance = null)
        {
            TinyIoCContainer.RegisterOptions options;

            if (instance != null)
                options = container.Register(implementation, instance);
            else
                options = container.Register(implementation);

            if (instance == null)
                SetScope(options, scope);
        }

        public void Register<TService, TImplementation>(InjectionScopeEnum scope = InjectionScopeEnum.Transient, TImplementation instance = null)
            where TService : class
            where TImplementation : class, TService
        {
            Register(typeof(TService), typeof(TImplementation), scope, instance);
        }

        public void Register(Type service, Type implementation, InjectionScopeEnum scope = InjectionScopeEnum.Transient, object instance = null)
        {
            TinyIoCContainer.RegisterOptions options;

            if (instance != null)
                options = container.Register(service, implementation, instance);
            else
                options = container.Register(service, implementation);

            if (instance == null)
                SetScope(options, scope);
        }

        #endregion

        #region Get

        public T Get<T>()
            where T : class
        {
            try
            {
                return container.Resolve<T>();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region Helper Methods

        private TinyIoCContainer.RegisterOptions SetScope(TinyIoCContainer.RegisterOptions options, InjectionScopeEnum scope)
        {
            switch (scope)
            {
                case InjectionScopeEnum.Singleton:
                    return options.AsSingleton();

                default:
                    return options.AsMultiInstance();
            }
        }

        #endregion
    }
}