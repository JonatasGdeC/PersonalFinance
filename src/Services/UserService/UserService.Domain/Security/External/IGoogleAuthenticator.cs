namespace UserService.Domain.Security.External;

public interface IGoogleAuthenticator
{
    Task<GoogleUserInfo> ValidateAndGetUserInfo(string idToken);
}

public record GoogleUserInfo(string GoogleId, string Email, string Name, bool EmailVerified);