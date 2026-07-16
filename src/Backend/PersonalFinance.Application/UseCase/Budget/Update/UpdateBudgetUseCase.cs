using FluentValidation.Results;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Budget;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Budget.Update;
using Domain.Entities;

public class UpdateBudgetUseCase(
    IBudgetWriteRepository writeRepository,
    ICategoryWriteRepository categoryWriteRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IUpdateBudgetUseCase
{
    public async Task Execute(long budgetId, RegisterBudgetRequest request)
    {
        await Validate(request: request);

        User user = await loggedUser.Get();

        Budget? budget = await writeRepository.GetById(budgetId: budgetId, userId: user.Id);
        if (budget == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.BUDGET_NOT_FOUND);
        }

        Category category = await GetCategory(categoryId: request.CategoryId, userId: user.Id);

        budget.MaximumSpend = request.MaximumSpend;
        budget.Color = request.Color;
        budget.CategoryId = category.Id;

        writeRepository.Update(budget: budget);
        await unitOfWork.Commit();
    }

    private async Task<Category> GetCategory(long categoryId, Guid userId)
    {
        Category? category = await categoryWriteRepository.GetById(categoryId: categoryId, userId: userId);
        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.CATEGORY_NOT_FOUND);
        }

        return category;
    }

    private async Task Validate(RegisterBudgetRequest request)
    {
        BudgetValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
