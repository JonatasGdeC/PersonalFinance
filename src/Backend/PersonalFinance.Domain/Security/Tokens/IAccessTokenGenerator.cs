using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}