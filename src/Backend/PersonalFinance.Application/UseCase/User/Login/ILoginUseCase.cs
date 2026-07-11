using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Application.UseCase.User.Login;

public interface ILoginUseCase
{
    Task<LoginResponse> Execute(LoginRequest request);
}
