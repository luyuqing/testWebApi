using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using testWebApi.Migrations;

namespace testWebApi.ServiceObjects
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, bool doSchemaExport = false)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2012Dialect>();
                c.ConnectionString = "Server=localhost,1433;Database=testWebApi;Uid=SA;Password=Password@1;";
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                // c.SchemaAction = SchemaAutoAction.Validate;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });

            configuration.SetProperty("hbm2ddl.keywords", "none");
            configuration.AddMapping(domainMapping);

            var sessionFactory = configuration.BuildSessionFactory();

            if (doSchemaExport)
            {
                var session = sessionFactory.OpenSession();
                // var export = new SchemaExport(configuration);
                // export.Execute(false, true, false, session.Connection, null);

                // if version exists some delete or update migrations:
                var migration = new Migration();
                migration.DoMigrate("1-1-1", session);  // get version from configuration

                // only adds new columns.
                new SchemaUpdate(configuration).Execute(false, true);

                services.AddSingleton(sessionFactory);
                services.AddScoped(factory => session);
            }
            else
            {
                services.AddSingleton(sessionFactory);
                services.AddScoped(factory => sessionFactory.OpenSession());
            }
            return services;
        }
    }
}