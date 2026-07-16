using FluentValidation;
using PersonalFinance.Communication.Requests.Pot;
using PersonalFinance.Exception;

namespace PersonalFinance.Application.UseCase.Pot;

public class PotValidator : AbstractValidator<RegisterPotRequest>
{
    public PotValidator()
    {
        RuleFor(expression: request => request.Name)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessages.NAME_IS_REQUIRED)
            .MinimumLength(minimumLength: 3).WithMessage(errorMessage: ResourceErrorMessages.NAME_MINIMUM_LENGTH)
            .MaximumLength(maximumLength: 100).WithMessage(errorMessage: ResourceErrorMessages.NAME_MAXIMUM_LENGTH);

        RuleFor(expression: request => request.Color)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessages.COLOR_IS_REQUIRED);

        RuleFor(expression: request => request.Target)
            .GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessages.TARGET_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expression: request => request.CurrentAmount)
            .GreaterThanOrEqualTo(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessages.CURRENT_AMOUNT_MUST_BE_POSITIVE);
    }
}
