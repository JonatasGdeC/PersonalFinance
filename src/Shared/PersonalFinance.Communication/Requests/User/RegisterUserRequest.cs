namespace PersonalFinance.Communication.Requests.User;

public record RegisterUserRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}