using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace PersonalFinance.Api.Extensions;

public static class AuthenticationExtension
{
    public static void AddAuthenticationConfig(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddAuthentication(configureOptions: options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions: options =>
            {
                string signingKey = configuration.GetValue<string>(key: "Settings:Jwt:SigningKey")!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(s: signingKey))
                };
            });
    }
}