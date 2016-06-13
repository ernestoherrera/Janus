using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Janus
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var assembly = Assembly.Load("JanusData");

            var dbConnection = new DbConnection(GetConnectionString());
           
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

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["FluentSql"].ToString();
        }

        private void OnPostMapping()
        {
            foreach (var entity in EntityMapper.EntityMap.Keys)
            {
                var typeName = entity.Name;
            }
        }
    }
}
