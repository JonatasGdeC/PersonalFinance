using PersonalFinance.Communication.Requests.User;

namespace PersonalFinance.Application.UseCase.User.UpdatePassword;

public interface IUpdatePasswordUseCase
{
    Task Execute(UpdatePasswordRequest request);
}