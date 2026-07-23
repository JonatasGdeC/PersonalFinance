using FluentValidation.Results;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Category.Update;
using Domain.Entities;

public class UpdateCategoryUseCase(
    ICategoryWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IUpdateCategoryUseCase
{
    public async Task Execute(Guid categoryId, RegisterCategoryRequest request)
    {
        await Validate(request: request);

        User user = await loggedUser.Get();

        Category? category = await writeRepository.GetById(categoryId: categoryId, userId: user.Id);
        if (category == null)
        {
            throw new NotFoundException(message: ResourceErrorMessages.CATEGORY_NOT_FOUND);
        }

        category.Name = request.Name;

        writeRepository.Update(category: category);
        await unitOfWork.Commit();
    }

    private async Task Validate(RegisterCategoryRequest request)
    {
        CategoryValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
