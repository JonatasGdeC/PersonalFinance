using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Application.UseCase.User.ValidateResetCode;

public interface IValidateResetCodeUseCase
{
    Task<ValidateResetCodeResponse> Execute(ValidateResetCodeRequest request);
}