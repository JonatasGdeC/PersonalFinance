using FluentValidation;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Exception;

namespace PersonalFinance.Application.UseCase.Bill;

public class BillValidator : AbstractValidator<RegisterBillRequest>
{
    public BillValidator()
    {
        RuleFor(expression: request => request.Amount)
            .GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expression: request => request.DueDate)
            .NotEqual(toCompare: default(DateTime)).WithMessage(errorMessage: ResourceErrorMessages.DATE_IS_REQUIRED);

        RuleFor(expression: request => request.InstallmentsTotal)
            .GreaterThan(valueToCompare: 0).WithMessage(errorMessage: ResourceErrorMessages.INSTALLMENTS_TOTAL_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expression: request => request.InstallmentsPaid)
            .Must(predicate: (request, installmentsPaid) => installmentsPaid >= 0 && installmentsPaid <= request.InstallmentsTotal)
            .WithMessage(errorMessage: ResourceErrorMessages.INSTALLMENTS_PAID_CANNOT_EXCEED_TOTAL);
    }
}
