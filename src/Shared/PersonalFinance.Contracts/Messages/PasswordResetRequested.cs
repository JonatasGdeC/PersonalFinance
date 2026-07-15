namespace PersonalFinance.Contracts.Messages;

public record PasswordResetRequested(Guid UserId, string Name, string Email, string ResetCode);
