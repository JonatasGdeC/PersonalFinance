using PersonalFinance.Infrastructure;
using PersonalFinance.Infrastructure.Extensions;
using PersonalFinance.Infrastructure.Migrations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddInfrastructure(configurationManager: builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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
