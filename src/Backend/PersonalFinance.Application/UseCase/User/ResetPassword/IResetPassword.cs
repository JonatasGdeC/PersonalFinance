using PersonalFinance.Communication.Requests.User;

namespace PersonalFinance.Application.UseCase.User.ResetPassword;

public interface IResetPassword
{
    Task Execute(ResetPasswordRequest request);
}