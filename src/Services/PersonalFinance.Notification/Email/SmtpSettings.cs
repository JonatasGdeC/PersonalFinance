namespace PersonalFinance.Notification.Email;

public class SmtpSettings
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string From { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}
