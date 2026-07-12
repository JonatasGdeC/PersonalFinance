using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Application.UseCase.User.Login;

public class LoginUseCase : ILoginUseCase
{
    public Task<LoginResponse> Execute(LoginRequest request) => throw new NotImplementedException();
}