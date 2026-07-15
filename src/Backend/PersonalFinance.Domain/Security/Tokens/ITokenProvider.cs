namespace PersonalFinance.Domain.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}