namespace UserService.Domain.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}