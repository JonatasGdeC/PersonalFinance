using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Application.UseCase.User.Register;

public interface IRegisterUserUseCase
{
    Task<RegisterUserResponse> Execute(RegisterUserRequest request);
}