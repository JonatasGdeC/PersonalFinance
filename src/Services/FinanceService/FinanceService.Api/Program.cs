using FinanceService.Api.Token;
using FinanceService.Domain.Security.Tokens;
using PersonalFinance.Application;
using FinanceService.Infrastructure;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Filter;
using Shared.Infrastructure.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerConfig();
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
