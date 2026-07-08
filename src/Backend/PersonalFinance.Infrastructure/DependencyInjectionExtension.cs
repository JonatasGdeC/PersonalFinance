using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Pot;
using PersonalFinance.Domain.Repositories.Transaction;
using PersonalFinance.Domain.Repositories.User;
using PersonalFinance.Infrastructure.DataAccess;
using PersonalFinance.Infrastructure.DataAccess.Repositories;
using PersonalFinance.Infrastructure.Extensions;

namespace PersonalFinance.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        AddRepositories(services: services);
        AddFluentMigrator(services: services, configuration: configurationManager);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore()
            .ConfigureRunner(configure: config =>
            {
                Assembly infrastructure = Assembly.Load(assemblyString: "PersonalFinance.Infrastructure");
                IMigrationRunnerBuilder? migrationRunnerBuilder = config.AddPostgres();
                migrationRunnerBuilder.WithGlobalConnectionString(connectionStringOrName: connectionString)
                    .ScanIn(infrastructure).For.All();
            });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserReadRepository, UserRepository>();
        services.AddScoped<IUserWriteRepository, UserRepository>();
        services.AddScoped<IPotReadRepository, PotRepository>();
        services.AddScoped<IPotWriteRepository, PotRepository>();
        services.AddScoped<ITransactionReadRepository, TransactionRepository>();
        services.AddScoped<ITransactionWhiteRepository, TransactionRepository>();
    }
}