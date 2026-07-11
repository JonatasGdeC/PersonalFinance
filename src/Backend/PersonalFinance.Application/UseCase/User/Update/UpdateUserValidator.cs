using FluentValidation;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Exception;

namespace PersonalFinance.Application.UseCase.User.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(expression: request => request.Name)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessages.NAME_IS_REQUIRED)
            .MinimumLength(minimumLength: 3).WithMessage(errorMessage: ResourceErrorMessages.NAME_MINIMUM_LENGTH)
            .MaximumLength(maximumLength: 200).WithMessage(errorMessage: ResourceErrorMessages.NAME_MAXIMUM_LENGTH);

        RuleFor(expression: request => request.Email)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessages.EMAIL_IS_REQUIRED)
            .EmailAddress().WithMessage(errorMessage: ResourceErrorMessages.EMAIL_INVALID);
    }
}
