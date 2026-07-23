using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Communication.Responses.Budget;

namespace PersonalFinance.Adapter.Interfaces;

public interface IBudgetServiceApi
{
    Task<BudgetDto> Register(RegisterBudgetRequest request);
    Task Update(Guid budgetId, RegisterBudgetRequest request);
    Task Delete(Guid budgetId);
    Task<GetAllBudgetResponse?> GetAll();
}
