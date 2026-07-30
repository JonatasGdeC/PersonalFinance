namespace PersonalFinance.Contracts.Events;

public record PasswordResetCodeEvent
{
    public required string Email { get; init; }
    public required string UserName { get; init; }
    public required string Code { get; init; }
}