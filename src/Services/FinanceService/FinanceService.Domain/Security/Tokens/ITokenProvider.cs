namespace FinanceService.Domain.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}