namespace PersonalFinance.Communication.Requests.User;

public record UpdateUserRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? ProfileImage { get; set; }
    public bool EmailNotificationsEnabled { get; set; } = true;
}