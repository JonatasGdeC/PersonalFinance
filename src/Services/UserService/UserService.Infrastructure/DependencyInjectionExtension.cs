using System.Reflection;
using FluentMigrator.Runner;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Extensions;
using UserService.Domain.Repositories;
using UserService.Domain.Repositories.User;
using UserService.Domain.Security.Cryptography;
using UserService.Domain.Security.Tokens;
using UserService.Domain.Services.LoggedUser;
using UserService.Infrastructure.DataAccess;
using UserService.Infrastructure.DataAccess.Repositories;
using UserService.Infrastructure.Security.Tokens;
using UserService.Infrastructure.Services.LoggedUser;

namespace UserService.Infrastructure;
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
        
        services.AddScoped<IEncrypter,BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();
        
        services.AddMassTransit(configure: x =>
        {
            x.UsingRabbitMq(configure: (context, cfg) =>
            {
                cfg.Host(host: "rabbitmq", port: 5672, virtualHost: "/", configure: h =>
                {
                    h.Username(username: configurationManager[key: "RabbitMq:Username"] ?? "guest");
                    h.Password(password: configurationManager[key: "RabbitMq:Password"] ?? "guest");
                });
            });
        });
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        services.AddDbContext<UserServiceDbContext>(optionsAction: options =>
            options.UseNpgsql(connectionString: connectionString));
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.ConnectionString();

        services.AddFluentMigratorCore()
            .ConfigureRunner(configure: config =>
            {
                Assembly infrastructure = Assembly.Load(assemblyString: "UserService.Infrastructure");
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
    }
}