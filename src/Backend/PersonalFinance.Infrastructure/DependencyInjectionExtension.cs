using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Infrastructure.Extensions;

namespace PersonalFinance.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        AddFluentMigrator(services: services, configuration: configurationManager);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore()
            .ConfigureRunner(configure: config =>
            {
                Assembly infrastructure = Assembly.Load(assemblyString: "PlanShare.Infrastructure");
                IMigrationRunnerBuilder? migrationRunnerBuilder = config.AddPostgres();
                migrationRunnerBuilder.WithGlobalConnectionString(connectionStringOrName: connectionString)
                    .ScanIn(infrastructure).For.All();
            });
    }
}