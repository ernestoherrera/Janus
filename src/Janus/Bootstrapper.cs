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

namespace Janus
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var assembly = Assembly.Load("JanusData");

            var dbConnection = new DbConnection();

            var databases = new List<string> { "Core", "History", "Reporting" };

            var dataMapper = new EntityMapper(dbConnection, typeof(IEntity), databases);

            foreach (var entity in EntityMapper.EntityMap.Keys)
            {
                var entityName = entity.ToString();
            }
        }
    }
}
