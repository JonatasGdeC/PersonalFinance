using MassTransit;
using PersonalFinance.Contracts.Messages;
using PersonalFinance.Notification.Email;

namespace PersonalFinance.Notification.Consumers;

public class PasswordResetRequestedConsumer(IEmailSender emailSender, ILogger<PasswordResetRequestedConsumer> logger) : IConsumer<PasswordResetRequested>
{
    public async Task Consume(ConsumeContext<PasswordResetRequested> context)
    {
        PasswordResetRequested message = context.Message;

        logger.LogInformation(message: "Processando pedido de reset de senha para {Email}", message.Email);

        await emailSender.SendAsync(
            toEmail: message.Email,
            toName: message.Name,
            subject: "Recuperação de senha - Personal Finance",
            body: $"Olá {message.Name}, seu código de recuperação de senha é: {message.ResetCode}. Ele expira em alguns minutos.",
            cancellationToken: context.CancellationToken);
    }
}
