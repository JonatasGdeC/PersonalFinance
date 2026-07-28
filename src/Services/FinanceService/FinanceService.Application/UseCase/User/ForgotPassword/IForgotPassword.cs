using PersonalFinance.Communication.Requests.User;

namespace PersonalFinance.Application.UseCase.User.ForgotPassword;

public interface IForgotPassword
{
    Task Execute(ForgotPasswordRequest request);
}