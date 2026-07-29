using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Filter;
using Shared.Infrastructure.Middleware;
using UserService.Api.Token;
using UserService.Application;
using UserService.Domain.Security.Tokens;
using UserService.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerConfig();
builder.Services.AddOpenApi();
builder.Services.AddAuthenticationConfig(configuration: builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configurationManager: builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

builder.Services.AddControllers(configure: options =>
{
    options.Filters.Add(filterType: typeof(ExceptionFilter));
});

WebApplication app = builder.Build();

app.UseMiddleware<CultureMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

if (!builder.Configuration.IsUnitTestEnvironment())
{
    await MigrateDatabase();
}

app.Run();

async Task MigrateDatabase()
{
    await using AsyncServiceScope scope = app.Services.CreateAsyncScope();
    string stringConnection = builder.Configuration.ConnectionString();
    DataBaseMigration.Migrate(connectionString: stringConnection, serviceProvider: scope.ServiceProvider);
}