using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Bill;
using FinanceService.Domain.Repositories.Budget;
using FinanceService.Domain.Repositories.Category;
using FinanceService.Domain.Repositories.Participant;
using FinanceService.Domain.Repositories.Pot;
using FinanceService.Domain.Repositories.Transaction;
using FinanceService.Domain.Security.Cryptography;
using FinanceService.Domain.Services.LoggedUser;
using FinanceService.Infrastructure.DataAccess;
using FinanceService.Infrastructure.DataAccess.Repositories;
using FinanceService.Infrastructure.Services.LoggedUser;
using Shared.Infrastructure.Extensions;

namespace FinanceService.Infrastructure;
using Security.Cryptography;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        AddDbContext(services: services, configuration: configurationManager);
        AddFluentMigrator(services: services, configuration: configurationManager);
        AddRepositories(services: services);
        
        services.AddScoped<IEncrypter, BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        services.AddDbContext<FinanceServiceDbContext>(optionsAction: options =>
            options.UseNpgsql(connectionString: connectionString));
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore()
            .ConfigureRunner(configure: config =>
            {
                Assembly infrastructure = Assembly.Load(assemblyString: "FinanceService.Infrastructure");
                IMigrationRunnerBuilder? migrationRunnerBuilder = config.AddPostgres();
                migrationRunnerBuilder.WithGlobalConnectionString(connectionStringOrName: connectionString).ScanIn(infrastructure).For.All();
            });
    }
    
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPotReadRepository, PotRepository>();
        services.AddScoped<IPotWriteRepository, PotRepository>();
        services.AddScoped<ITransactionReadRepository, TransactionRepository>();
        services.AddScoped<ITransactionWhiteRepository, TransactionRepository>();
        services.AddScoped<IParticipantReadRepository, ParticipantRepository>();
        services.AddScoped<IParticipantWriteRepository, ParticipantRepository>();
        services.AddScoped<ICategoryReadRepository, CategoryRepository>();
        services.AddScoped<ICategoryWriteRepository, CategoryRepository>();
        services.AddScoped<IBudgetReadRepository, BudgetRepository>();
        services.AddScoped<IBudgetWriteRepository, BudgetRepository>();
        services.AddScoped<IBillReadRepository, BillRepository>();
        services.AddScoped<IBillWriteRepository, BillRepository>();
    }
}