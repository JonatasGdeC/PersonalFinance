using Microsoft.OpenApi;

namespace PersonalFinance.Api.Extensions;

public static class SwaggerExtension
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(setupAction: config =>
        {
            config.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = """
                              JWT Authorization header using the Bearer scheme.
                              Enter 'Bearer' [space] and then your token.
                              Example: 'Bearer 12345abcdef'
                              """,
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                Type = SecuritySchemeType.ApiKey
            });

            config.AddSecurityRequirement(securityRequirement: document => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference(referenceId: "Bearer", hostDocument: document),
                    []
                }
            });
        });
    }
}