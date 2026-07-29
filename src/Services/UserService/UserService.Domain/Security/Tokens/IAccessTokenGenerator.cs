using UserService.Domain.Entities;

namespace UserService.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}