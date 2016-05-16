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

namespace Janus
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var assembly = Assembly.Load("JanusData");

            var dbConnection = new DbConnection(GetConnectionString());

            //var databases = new List<string> { "Core", "History", "Reporting" };
            var databases = new List<string> { "FluentSqlTest" };

            var dataMapper = new EntityMapper(dbConnection, typeof(IEntity), databases, null, false);

            foreach (var entity in EntityMapper.EntityMap.Keys)
            {
                var entityName = entity.ToString();
            }
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["FluentSql"].ToString();
        }
    }
}
