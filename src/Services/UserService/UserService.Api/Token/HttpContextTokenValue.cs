using UserService.Domain.Security.Tokens;

namespace UserService.Api.Token;

public class HttpContextTokenValue(IHttpContextAccessor accessor) : ITokenProvider
{
    public string TokenOnRequest()
    {
        string authorization = accessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authorization.Replace(oldValue: "Bearer ", newValue: string.Empty);
    }
}