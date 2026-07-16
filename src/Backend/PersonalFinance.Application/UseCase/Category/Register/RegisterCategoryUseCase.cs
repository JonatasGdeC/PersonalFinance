using AutoMapper;
using FluentValidation.Results;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.Category;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.Category.Register;
using Domain.Entities;

public class RegisterCategoryUseCase(
    ICategoryWriteRepository writeRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRegisterCategoryUseCase
{
    public async Task<CategoryDto> Execute(RegisterCategoryRequest request)
    {
        await Validate(request: request);

        User user = await loggedUser.Get();

        Category category = mapper.Map<Category>(source: request);
        category.UserId = user.Id;

        await writeRepository.Add(category: category);
        await unitOfWork.Commit();

        return mapper.Map<CategoryDto>(source: category);
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
