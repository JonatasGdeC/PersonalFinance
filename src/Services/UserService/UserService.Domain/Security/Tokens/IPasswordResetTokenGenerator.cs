using UserService.Domain.Entities;

namespace UserService.Domain.Security.Tokens;

public interface IPasswordResetTokenGenerator
{
    string Generate(User user);
}