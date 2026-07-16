namespace PersonalFinance.Application.UseCase.Budget.Delete;

public interface IDeleteBudgetUseCase
{
    Task Execute(long budgetId);
}