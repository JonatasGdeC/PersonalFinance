using MassTransit;
using NotificationService.Api.Messaging.Consumers;
using NotificationService.Api.Services.MailKit;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);

builder.Services.AddOpenApi();

IConfigurationSection emailSettings =  builder.Configuration.GetSection(key: "EmailSettings");
builder.Services.Configure<EmailSettings>(config: emailSettings);
builder.Services.AddScoped<EmailService>();

builder.Services.AddMassTransit(configure: x =>
{
    x.AddConsumer<PasswordResetCodeEventConsumer>();
        
    x.UsingRabbitMq(configure: (context, cfg) =>
    {
        cfg.Host(host: "rabbitmq", port: 5672, virtualHost: "/", configure: h =>
        {
            h.Username(username: builder.Configuration[key: "RabbitMq:Username"] ?? "guest");
            h.Password(password: builder.Configuration[key: "RabbitMq:Password"] ?? "guest");
        });
            
        cfg.ConfigureEndpoints(registration: context);
    });
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();