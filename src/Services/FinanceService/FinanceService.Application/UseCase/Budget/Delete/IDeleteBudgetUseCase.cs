namespace PersonalFinance.Application.UseCase.Budget.Delete;

public interface IDeleteBudgetUseCase
{
    Task Execute(Guid budgetId);
}