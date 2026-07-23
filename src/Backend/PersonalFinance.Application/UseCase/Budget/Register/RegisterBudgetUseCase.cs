using AutoMapper;
using FluentValidation.Results;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Budget;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Budget.Register;
using Domain.Entities;

public class RegisterBudgetUseCase(
    IBudgetWriteRepository writeRepository,
    ICategoryWriteRepository categoryWriteRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRegisterBudgetUseCase
{
    public async Task<BudgetDto> Execute(RegisterBudgetRequest request)
    {
        await Validate(request: request);

        User user = await loggedUser.Get();

        Category category = await GetCategory(categoryId: request.CategoryId, userId: user.Id);

        Budget budget = mapper.Map<Budget>(source: request);
        budget.UserId = user.Id;
        budget.Category = category;

        await writeRepository.Add(budget: budget);
        await unitOfWork.Commit();

        return mapper.Map<BudgetDto>(source: budget);
    }

    private async Task<Category> GetCategory(Guid categoryId, Guid userId)
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
