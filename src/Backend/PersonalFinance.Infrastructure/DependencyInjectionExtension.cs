using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Bill;
using PersonalFinance.Domain.Repositories.Budget;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Repositories.Participant;
using PersonalFinance.Domain.Repositories.Pot;
using PersonalFinance.Domain.Repositories.Transaction;
using PersonalFinance.Domain.Repositories.User;
using PersonalFinance.Domain.Security.Cryptography;
using PersonalFinance.Domain.Security.Tokens;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Infrastructure.DataAccess;
using PersonalFinance.Infrastructure.DataAccess.Repositories;
using PersonalFinance.Infrastructure.Extensions;
using PersonalFinance.Infrastructure.Security.Tokens;
using PersonalFinance.Infrastructure.Services.LoggedUser;

namespace PersonalFinance.Infrastructure;
using Security.Cryptography;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        AddDbContext(services: services, configuration: configurationManager);
        AddFluentMigrator(services: services, configuration: configurationManager);
        AddUserToken(services: services, configuration: configurationManager);
        AddPasswordResetToken(services: services, configuration: configurationManager);
        AddRepositories(services: services);
        
        services.AddScoped<IEncrypter, BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        services.AddDbContext<PersonalFinanceDbContext>(optionsAction: options =>
            options.UseNpgsql(connectionString: connectionString));
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore()
            .ConfigureRunner(configure: config =>
            {
                Assembly infrastructure = Assembly.Load(assemblyString: "PersonalFinance.Infrastructure");
                IMigrationRunnerBuilder? migrationRunnerBuilder = config.AddPostgres();
                migrationRunnerBuilder.WithGlobalConnectionString(connectionStringOrName: connectionString).ScanIn(infrastructure).For.All();
            });
    }
    
    private static void AddUserToken(IServiceCollection services, IConfigurationManager configuration)
    {
        IConfigurationSection expirationTimeMinutes = configuration.GetSection(key: "Settings:Jwt:ExpiresMinutes");
        IConfigurationSection signingKey = configuration.GetSection(key: "Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(implementationFactory: _ => 
            new JwtTokenGenerator(expirationTimeMinutes: uint.Parse(s: expirationTimeMinutes.Value!) , signingKey: signingKey.Value!));
    }
    
    private static void AddPasswordResetToken(IServiceCollection services, IConfigurationManager configuration)
    {
        IConfigurationSection expirationTimeMinutes = configuration.GetSection(key: "Settings:PasswordResetToken:ExpiresMinutes");
        IConfigurationSection signingKey = configuration.GetSection(key: "Settings:Jwt:SigningKey");
        
        services.AddScoped<IPasswordResetTokenGenerator>(implementationFactory: _ =>
            new PasswordResetTokenGenerator(expirationTimeMinutes: uint.Parse(s: expirationTimeMinutes.Value!), signingKey: signingKey.Value!));
        
        services.AddScoped<IVerifyTokenResetCode>(implementationFactory: _ => new VerifyTokenResetCode(signingKey: signingKey.Value!));
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