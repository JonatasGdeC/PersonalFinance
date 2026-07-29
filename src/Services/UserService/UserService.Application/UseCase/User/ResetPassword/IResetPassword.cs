using PersonalFinance.Communication.Requests.User;

namespace UserService.Application.UseCase.User.ResetPassword;

public interface IResetPassword
{
    Task Execute(ResetPasswordRequest request);
}