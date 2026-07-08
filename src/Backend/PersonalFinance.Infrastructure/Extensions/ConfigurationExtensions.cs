using Microsoft.Extensions.Configuration;

namespace PersonalFinance.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string ConnectionString(this IConfiguration configuration) => configuration.GetConnectionString(name: "Connection")!;

    public static bool IsUnitTestEnvironment(this IConfiguration configuration)
    {
        _ = bool.TryParse(value: configuration.GetSection(key: "InMemoryTests").Value, result: out bool inMemoryTests);

        return inMemoryTests;
    }
}