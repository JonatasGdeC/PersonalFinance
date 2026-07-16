using PersonalFinance.Communication.Responses.Budget;

namespace PersonalFinance.Application.UseCase.Budget.GetAll;

public interface IGetAllBudgetUseCase
{
    Task<GetAllBudgetResponse> Execute();
}