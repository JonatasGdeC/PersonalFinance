using Google.Apis.Auth;
using PersonalFinance.Exception.ExceptionBase;
using UserService.Domain.Security.External;

namespace UserService.Infrastructure.Security.External;

internal sealed class GoogleAuthenticator : IGoogleAuthenticator
{
    private readonly string _clientId;

    public GoogleAuthenticator(string clientId) => _clientId = clientId;

    public async Task<GoogleUserInfo> ValidateAndGetUserInfo(string idToken)
    {
        GoogleJsonWebSignature.ValidationSettings settings = new()
        {
            Audience = [_clientId]
        };

        try
        {
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(jwt: idToken, validationSettings: settings);

            return new GoogleUserInfo(
                GoogleId: payload.Subject,
                Email: payload.Email,
                Name: payload.Name,
                EmailVerified: payload.EmailVerified);
        }
        catch (InvalidJwtException)
        {
            throw new InvalidLoginException();
        }
    }
}