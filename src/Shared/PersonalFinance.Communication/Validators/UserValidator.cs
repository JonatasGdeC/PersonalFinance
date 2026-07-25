using FluentValidation;
using PersonalFinance.Communication.Requests.User;

namespace PersonalFinance.Communication.Validators;

public class UserValidator : AbstractValidator<RegisterUserRequest>
{
    public UserValidator()
    {
        RuleFor(expression: request => request).SetValidator(validator: new UpdateUserValidator());
        RuleFor(expression: request => request.Password).SetValidator(validator: new PasswordValidator());
    }
}
