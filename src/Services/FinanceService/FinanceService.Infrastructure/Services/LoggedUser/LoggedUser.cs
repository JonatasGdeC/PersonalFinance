using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinanceService.Domain.Security.Tokens;
using FinanceService.Domain.Services.LoggedUser;

namespace FinanceService.Infrastructure.Services.LoggedUser;

internal class LoggedUser(ITokenProvider tokenProvider) : ILoggedUser
{
    private JwtSecurityToken GetJwtSecurityToken()
    {
        string token = tokenProvider.TokenOnRequest();
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.ReadJwtToken(token: token);
    }
    
    public Guid GetUserId()
    {
        string userId = GetJwtSecurityToken().Claims.First(predicate: claim => claim.Type == ClaimTypes.Sid).Value;
        return Guid.Parse(input: userId);
    }

    public string GetUserEmail()
    {
        return GetJwtSecurityToken().Claims.First(predicate: claim => claim.Type == ClaimTypes.Email).Value;
    }

    public string GetUserName()
    {
        return GetJwtSecurityToken().Claims.First(predicate: claim => claim.Type == ClaimTypes.Name).Value;
    }
}