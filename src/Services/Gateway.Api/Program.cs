using Gateway.Api.Extensions;
using Shared.Infrastructure.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

const string corsPolicyName = "Frontend";

builder.Services.AddCorsConfig(corsPolicyName: corsPolicyName);
builder.Services.RateLimiting();
builder.Services.AddAuthenticationConfig(configuration: builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddOpenApi();

builder.Services.AddReverseProxyConfig(configuration: builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(policyName: corsPolicyName);
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapReverseProxy();

app.Run();
