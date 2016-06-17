using Nancy;
using System.Collections.Generic;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System.Reflection;
using JanusData;
using FluentSql.Mappers;
using Dapper;
using System.Configuration;
using Nancy.Json;
using Janus.Extensions;
using Janus.Injection;
using System.Data;
using Nancy.Authentication.Stateless;
using Janus.Authentication;
using Janus.Contracts;
using Janus.Support;
using JanusCore.Injection;
using JanusCore.Support;
using JanusData.Factories;
using JanusModels.Contracts;
using System.Linq;
using System.Diagnostics;

namespace Janus
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var assembly = Assembly.Load("JanusData");

            var dbConnection = new DbConnection();
           
            var databases = new List<string> { "FluentSqlTest" };

            var dataMapper = new EntityMapper(dbConnection, typeof(IEntity), databases, null, false, OnPostMapping);

            dbConnection.Dispose();

            JsonSettings.MaxJsonLength = int.MaxValue;
            JsonSettings.PrimitiveConverters.Add(new NullableEnumConverter( new[] { assembly }));
        }
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var assemblies = new[]
            {
                Assembly.Load("JanusData"),
                Assembly.GetExecutingAssembly()
            };

            var injectionMapper = new InjectionMapper(assemblies);

            injectionMapper.RegisterByAttribute(new InjectionContainer(container));
        }

        private static void AllowAccessToConsumingSite(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, DELETE, PUT, OPTIONS");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Cache-Control, Pragma, Origin, Authorization, Content-Type, X-Requested-With");
            });

            //pipelines.OnError.AddItemToEndOfPipeline((ctx, exc) =>
            //{
            //    if (ctx.Response != null)
            //    {
            //        ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //        ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, DELETE, PUT, OPTIONS");
            //        ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Cache-Control, Pragma, Origin, Authorization, Content-Type, X-Requested-With");
            //    }

            //    return HttpStatusCode.InternalServerError;
            //});
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Register<IDbConnection, DbConnection>().AsSingleton();
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {

            var configuration = new StatelessAuthenticationConfiguration(nancyContext =>
            {
                var requestInfo = new RequestInfo(nancyContext.Request);

                if (!requestInfo.IsWellFormed)
                    return null;

                var authHelper = container.Resolve<AuthenticationHelper>();

                if (authHelper != null)
                    return authHelper.ValidateRequest(requestInfo);

                return null;
            });

            pipelines.BeforeRequest.AddItemToEndOfPipeline(ctx =>
            {
                container.Register<IContextProvider, ContextProvider>().AsSingleton();

                container.Resolve<IContextProvider>().Context = ctx;
                
                RegisterRequestServices(new InjectionContainer(container));

                return null;
            });

            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                var dbConnection = container.Resolve<IDbConnection>();

                if (dbConnection != null)
                    dbConnection.Dispose();
            });

            //pipelines.OnError += (ctx, ex) =>
            //{
            //    var genericFactory = container.Resolve<GenericFactory>();

            //    //var logger = new Logger(serviceFactory);

            //    //try
            //    //{
            //    //    logger.Error("An unhandled exception was caught in the bootstrapper.  ", ex);
            //    //}
            //    //catch { }

            //    return null;
            //};

            AllowAccessToConsumingSite(pipelines);

            StatelessAuthentication.Enable(pipelines, configuration);
        }

        private void OnPostMapping()
        {
            foreach (var table in EntityMapper.DatabaseTables.Where( t => !t.IsMapped))
            {
                Debug.WriteLine(table.Name);
            }
        }

        private void RegisterRequestServices(IInjectionContainer container)
        {
            container.Register(InjectionScopeEnum.Singleton, new GenericFactory(container));
        }
    }
}
