using PersonalFinance.Communication.Requests.User;

namespace UserService.Application.UseCase.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(UpdateUserRequest request);
}
