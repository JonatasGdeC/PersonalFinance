using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using FinanceService.Domain.Entities;
using FinanceService.Domain.Security.Tokens;
using FinanceService.Domain.Services.LoggedUser;
using FinanceService.Infrastructure.DataAccess;

namespace FinanceService.Infrastructure.Services.LoggedUser;

internal class LoggedUser(PersonalFinanceDbContext context, ITokenProvider tokenProvider) : ILoggedUser
{
    private JwtSecurityToken GetJwtSecurityToken()
    {
        string token = tokenProvider.TokenOnRequest();
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.ReadJwtToken(token: token);
    }
    
    public async Task<User> Get()
    {
        string token = tokenProvider.TokenOnRequest();
        JwtSecurityTokenHandler tokenHandler = new();
        JwtSecurityToken? jwtSecurityToken = tokenHandler.ReadJwtToken(token: token);
        string userId = jwtSecurityToken.Claims.First(predicate: claim => claim.Type == ClaimTypes.Sid).Value;
        
        return await context.Users.AsNoTracking().FirstAsync(predicate: user => user.Id == Guid.Parse(userId));
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