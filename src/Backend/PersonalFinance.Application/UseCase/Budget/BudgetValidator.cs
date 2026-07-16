using FluentValidation;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Exception;

namespace PersonalFinance.Application.UseCase.Budget;

public class BudgetValidator : AbstractValidator<RegisterBudgetRequest>
{
    public BudgetValidator()
    {
        RuleFor(expression: request => request.Color)
            .NotEmpty().WithMessage(errorMessage: ResourceErrorMessages.COLOR_IS_REQUIRED);

        RuleFor(expression: request => request.MaximumSpend)
            .GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessages.MAXIMUM_SPEND_MUST_BE_GREATER_THAN_ZERO);
    }
}
