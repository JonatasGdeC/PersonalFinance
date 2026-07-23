using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Budget;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Budget.Delete;
using Domain.Entities;

public class DeleteBudgetUseCase(
    IBudgetWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteBudgetUseCase
{
    public async Task Execute(Guid budgetId)
    {
        User user = await loggedUser.Get();

        Budget? budget = await writeRepository.GetById(budgetId: budgetId, userId: user.Id);
        if (budget == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.BUDGET_NOT_FOUND);
        }

        writeRepository.Delete(budget: budget);
        await unitOfWork.Commit();
    }
}
