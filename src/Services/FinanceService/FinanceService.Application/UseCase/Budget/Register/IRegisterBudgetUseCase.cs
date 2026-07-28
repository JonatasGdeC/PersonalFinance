using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Budget;

namespace PersonalFinance.Application.UseCase.Budget.Register;

public interface IRegisterBudgetUseCase
{
    Task<BudgetDto> Execute(RegisterBudgetRequest request);
}