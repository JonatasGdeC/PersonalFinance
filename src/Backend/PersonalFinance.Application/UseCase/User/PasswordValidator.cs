using FluentValidation;
using PersonalFinance.Exception;

namespace PersonalFinance.Application.UseCase.User;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(expression: password => password)
            .NotEmpty()
            .WithMessage(errorMessage: ResourceErrorMessages.PASSWORD_IS_REQUIRED)
            .MinimumLength(minimumLength: 8)
            .WithMessage(errorMessage: ResourceErrorMessages.PASSWORD_MINIMUM_LENGTH)
            .Matches(expression: @"[a-z]")
            .WithMessage(errorMessage: ResourceErrorMessages.PASSWORD_MUST_CONTAIN_LOWERCASE_LETTER)
            .Matches(expression: @"[A-Z]")
            .WithMessage(errorMessage: ResourceErrorMessages.PASSWORD_MUST_CONTAIN_UPPERCASE_LETTER)
            .Matches(expression: @"[0-9]")
            .WithMessage(errorMessage: ResourceErrorMessages.PASSWORD_MUST_CONTAIN_NUMBER)
            .Matches(expression: @"[\W_]")
            .WithMessage(errorMessage: ResourceErrorMessages.PASSWORD_MUST_CONTAIN_SPECIAL_CHARACTER);
    }
}