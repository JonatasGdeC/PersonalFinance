namespace Gateway.Api.Extensions;

internal static class CorsExtension
{
    internal static void AddCorsConfig(this IServiceCollection services, string corsPolicyName)
    {
        services.AddCors(setupAction: options =>
        {
            options.AddPolicy(name: corsPolicyName, configurePolicy: policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:5290"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}