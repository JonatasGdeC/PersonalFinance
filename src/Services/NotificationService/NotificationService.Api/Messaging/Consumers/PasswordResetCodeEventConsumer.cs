using MassTransit;
using NotificationService.Api.Services.MailKit;
using PersonalFinance.Contracts.Events;

namespace NotificationService.Api.Messaging.Consumers;

public class PasswordResetCodeEventConsumer(EmailService emailService) : IConsumer<PasswordResetCodeEvent>
{
    public async Task Consume(ConsumeContext<PasswordResetCodeEvent> context)
    {
        await emailService.SendPasswordResetCode(to: context.Message.Email, userName: context.Message.UserName, code: context.Message.Code);
    }
}
