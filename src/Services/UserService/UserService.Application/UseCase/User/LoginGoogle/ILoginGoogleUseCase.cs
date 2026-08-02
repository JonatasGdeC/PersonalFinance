using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace UserService.Application.UseCase.User.LoginGoogle;

public interface ILoginGoogleUseCase
{
    public Task<LoginResponse> Execute(LoginGoogleRequest request);
}