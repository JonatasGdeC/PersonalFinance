using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Security.Tokens;
using UserService.Domain.Services.LoggedUser;
using UserService.Infrastructure.DataAccess;

namespace UserService.Infrastructure.Services.LoggedUser;

internal class LoggedUser(UserServiceDbContext context, ITokenProvider tokenProvider) : ILoggedUser
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