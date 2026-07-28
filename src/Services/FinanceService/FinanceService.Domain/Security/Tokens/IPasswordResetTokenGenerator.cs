using FinanceService.Domain.Entities;

namespace FinanceService.Domain.Security.Tokens;

public interface IPasswordResetTokenGenerator
{
    string Generate(User user);
}