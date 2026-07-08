
using PersonalFinance.Infrastructure;

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


app.Run();
