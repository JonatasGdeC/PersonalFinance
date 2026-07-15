using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Security.Tokens;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Infrastructure.DataAccess;

namespace PersonalFinance.Infrastructure.Services.LoggedUser;

internal class LoggedUser(PersonalFinanceDbContext context, ITokenProvider tokenProvider) : ILoggedUser
{
    public async Task<User> Get()
    {
        string token = tokenProvider.TokenOnRequest();
        JwtSecurityTokenHandler tokenHandler = new();
        JwtSecurityToken? jwtSecurityToken = tokenHandler.ReadJwtToken(token: token);
        string userId = jwtSecurityToken.Claims.First(predicate: claim => claim.Type == ClaimTypes.Sid).Value;
        
        return await context.Users.AsNoTracking().FirstAsync(predicate: user => user.Id == Guid.Parse(userId));
    }
}