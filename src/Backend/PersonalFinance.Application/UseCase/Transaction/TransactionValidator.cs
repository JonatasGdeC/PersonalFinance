using FluentValidation;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Exception;

namespace PersonalFinance.Application.UseCase.Transaction;

public class TransactionValidator : AbstractValidator<RegisterTransactionRequest>
{
    public TransactionValidator()
    {
        RuleFor(expression: request => request.Amount)
            .GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expression: request => request.Date)
            .NotEqual(toCompare: default(DateTime)).WithMessage(errorMessage: ResourceErrorMessages.DATE_IS_REQUIRED);
    }
}
