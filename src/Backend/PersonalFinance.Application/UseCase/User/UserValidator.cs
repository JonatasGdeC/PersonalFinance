using FluentValidation;
using PersonalFinance.Application.UseCase.User.Update;
using PersonalFinance.Communication.Requests.User;

namespace PersonalFinance.Application.UseCase.User;

public class UserValidator : AbstractValidator<RegisterUserRequest>
{
    public UserValidator()
    {
        RuleFor(expression: request => request).SetValidator(validator: new UpdateUserValidator());
        RuleFor(expression: request => request.Password).SetValidator(validator: new PasswordValidator());
    }
}
