namespace UserService.Domain.Security.Tokens;

public interface IVerifyTokenResetCode
{
    Guid? GetUserId(string token);
}