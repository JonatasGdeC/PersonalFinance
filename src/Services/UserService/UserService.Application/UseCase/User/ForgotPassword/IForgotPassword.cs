using PersonalFinance.Communication.Requests.User;

namespace UserService.Application.UseCase.User.ForgotPassword;

public interface IForgotPassword
{
    Task Execute(ForgotPasswordRequest request);
}