using Microsoft.Extensions.Configuration;

namespace PersonalFinance.Infrastructure.Extensions;

internal static class ConfigurationExtensions
{
    internal static string ConnectionString(this IConfiguration configuration) => configuration.GetConnectionString(name: "Connection")!;

    internal static bool IsUnitTestEnviroment(this IConfiguration configuration)
    {
        _ = bool.TryParse(value: configuration.GetSection(key: "InMemoryTests").Value, result: out bool inMemoryTests);

        return inMemoryTests;
    }
}