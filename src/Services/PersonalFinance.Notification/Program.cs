using MassTransit;
using Microsoft.Extensions.Hosting;
using PersonalFinance.Notification.Consumers;
using PersonalFinance.Notification.Email;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args: args);

builder.Services.Configure<SmtpSettings>(config: builder.Configuration.GetSection(key: "Smtp"));
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();

builder.Services.AddMassTransit(configure: configurator =>
{
    configurator.AddConsumer<PasswordResetRequestedConsumer>();

    configurator.UsingRabbitMq(configure: (context, factoryConfigurator) =>
    {
        factoryConfigurator.Host(host: builder.Configuration[key: "RabbitMq:Host"], configure: hostConfigurator =>
        {
            hostConfigurator.Username(username: builder.Configuration[key: "RabbitMq:Username"]!);
            hostConfigurator.Password(password: builder.Configuration[key: "RabbitMq:Password"]!);
        });

        factoryConfigurator.ConfigureEndpoints(registration: context);
    });
});

IHost host = builder.Build();
host.Run();
