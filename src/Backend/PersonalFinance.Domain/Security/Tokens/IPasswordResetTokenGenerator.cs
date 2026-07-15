using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Domain.Security.Tokens;

public interface IPasswordResetTokenGenerator
{
    string Generate(User user);
}