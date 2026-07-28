using FinanceService.Domain.Entities;

namespace FinanceService.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}