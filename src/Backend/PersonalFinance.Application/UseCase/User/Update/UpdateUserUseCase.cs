using FluentValidation.Results;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.User;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.User.Update;
using Domain.Entities;

public class UpdateUserUseCase(
    IUserWriteRepository writeRepository,
    IUserReadRepository readRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IUpdateUserUseCase
{
    public async Task Execute(UpdateUserRequest request)
    {
        User user = await loggedUser.Get();
        await Validate(request: request, currentUserId: user.Id);

        user.Name = request.Name;
        user.Email = request.Email;

        writeRepository.Update(user: user);
        await unitOfWork.Commit();
    }

    private async Task Validate(UpdateUserRequest request, Guid currentUserId)
    {
        UpdateUserValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        User? existingUser = await readRepository.GetByEmail(email: request.Email);
        if (existingUser != null && existingUser.Id != currentUserId)
        {
            result.Errors.Add(item: new ValidationFailure(propertyName: string.Empty, errorMessage: ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}
