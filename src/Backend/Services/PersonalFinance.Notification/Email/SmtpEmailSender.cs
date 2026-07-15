using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace PersonalFinance.Notification.Email;

public class SmtpEmailSender(IOptions<SmtpSettings> settings, ILogger<SmtpEmailSender> logger) : IEmailSender
{
    public async Task SendAsync(string toEmail, string toName, string subject, string body, CancellationToken cancellationToken)
    {
        SmtpSettings smtpSettings = settings.Value;

        MimeMessage message = new();
        message.From.Add(address: MailboxAddress.Parse(text: smtpSettings.From));
        message.To.Add(address: new MailboxAddress(name: toName, address: toEmail));
        message.Subject = subject;
        message.Body = new TextPart(subtype: "plain") { Text = body };

        using SmtpClient client = new();
        await client.ConnectAsync(host: smtpSettings.Host, port: smtpSettings.Port, options: MailKit.Security.SecureSocketOptions.Auto, cancellationToken: cancellationToken);

        if (!string.IsNullOrWhiteSpace(value: smtpSettings.Username))
        {
            await client.AuthenticateAsync(userName: smtpSettings.Username, password: smtpSettings.Password ?? string.Empty, cancellationToken: cancellationToken);
        }

        await client.SendAsync(message: message, cancellationToken: cancellationToken);
        await client.DisconnectAsync(quit: true, cancellationToken: cancellationToken);

        logger.LogInformation(message: "Email de {Subject} enviado para {Email}", subject, toEmail);
    }
}
