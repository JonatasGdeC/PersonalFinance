using FinanceService.Api.Extensions;
using FinanceService.Api.Filter;
using FinanceService.Api.Middleware;
using FinanceService.Api.Token;
using FinanceService.Domain.Security.Tokens;
using PersonalFinance.Application;
using FinanceService.Infrastructure;
using FinanceService.Infrastructure.Extensions;
using FinanceService.Infrastructure.Migrations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

const string corsPolicyName = "Frontend";

builder.Services.AddHttpContextAccessor();
builder.Services.AddCorsConfig(corsPolicyName: corsPolicyName);
builder.Services.AddSwaggerConfig();
builder.Services.RateLimiting();
builder.Services.AddAuthenticationConfig(configuration: builder.Configuration);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(configurationManager: builder.Configuration);

builder.Services.AddControllers(configure: options =>
{
    options.Filters.Add(filterType: typeof(ExceptionFilter));
});

builder.Services.AddOpenApi();

builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

WebApplication app = builder.Build();

app.UseMiddleware<CultureMiddleware>();

app.UseCors(policyName: corsPolicyName);
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
