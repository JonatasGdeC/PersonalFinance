namespace PersonalFinance.Api.Extensions;

public static class CorsExtension
{
    public static void AddCorsConfig(this IServiceCollection services, string corsPolicyName)
    {
        services.AddCors(setupAction: options =>
        {
            options.AddPolicy(name: corsPolicyName, configurePolicy: policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:5072",
                        "https://kanban-jgc.vercel.app"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}