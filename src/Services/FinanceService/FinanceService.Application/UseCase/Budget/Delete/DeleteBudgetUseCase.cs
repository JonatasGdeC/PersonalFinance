using FinanceService.Domain.Repositories;
using FinanceService.Domain.Repositories.Budget;
using FinanceService.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Budget.Delete;
using FinanceService.Domain.Entities;

public class DeleteBudgetUseCase(
    IBudgetWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteBudgetUseCase
{
    public async Task Execute(Guid budgetId)
    {
        Guid userId = loggedUser.GetUserId();

        Budget? budget = await writeRepository.GetById(budgetId: budgetId, userId: userId);
        if (budget == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.BUDGET_NOT_FOUND);
        }

        writeRepository.Delete(budget: budget);
        await unitOfWork.Commit();
    }
}
