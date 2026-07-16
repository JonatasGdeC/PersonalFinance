using PersonalFinance.Communication.Requests.Budget;

namespace PersonalFinance.Application.UseCase.Budget.Update;

public interface IUpdateBudgetUseCase
{
    Task Execute(long budgetId, RegisterBudgetRequest request);
}