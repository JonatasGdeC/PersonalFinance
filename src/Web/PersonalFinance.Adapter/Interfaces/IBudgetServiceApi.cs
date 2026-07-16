using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Communication.Responses.Budget;

namespace PersonalFinance.Adapter.Interfaces;

public interface IBudgetServiceApi
{
    Task<BudgetDto> Register(RegisterBudgetRequest request);
    Task Update(long budgetId, RegisterBudgetRequest request);
    Task Delete(long budgetId);
    Task<GetAllBudgetResponse?> GetAll();
}
