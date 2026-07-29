using PersonalFinance.Communication.Requests.User;

namespace UserService.Application.UseCase.User.UpdatePassword;

public interface IUpdatePasswordUseCase
{
    Task Execute(UpdatePasswordRequest request);
}