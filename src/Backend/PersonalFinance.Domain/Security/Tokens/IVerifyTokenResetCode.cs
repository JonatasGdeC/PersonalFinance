namespace PersonalFinance.Domain.Security.Tokens;

public interface IVerifyTokenResetCode
{
    Guid? GetUserId(string token);
}