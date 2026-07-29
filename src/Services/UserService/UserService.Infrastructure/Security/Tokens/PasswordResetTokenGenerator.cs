using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserService.Domain.Entities;
using UserService.Domain.Security.Tokens;

namespace UserService.Infrastructure.Security.Tokens;

internal class PasswordResetTokenGenerator(uint expirationTimeMinutes, string signingKey) : IPasswordResetTokenGenerator
{
    public string Generate(User user)
    {
        List<Claim> claims =
        [
            new(type: ClaimTypes.Sid, value: user.Id.ToString()),
            new(type: "purpose", value: "password-reset")
        ];

        SecurityTokenDescriptor tokenDescription = new()
        {
            Expires = DateTime.UtcNow.AddMinutes(value: expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(key: SecurityKey(), algorithm: SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims: claims)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor: tokenDescription);

        return tokenHandler.WriteToken(token: securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        byte[] key = Encoding.UTF8.GetBytes(s: signingKey);
        return new SymmetricSecurityKey(key: key);
    }
}