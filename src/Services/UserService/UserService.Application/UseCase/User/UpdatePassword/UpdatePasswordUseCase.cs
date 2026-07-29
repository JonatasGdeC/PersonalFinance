using FluentValidation.Results;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;
using UserService.Domain.Repositories;
using UserService.Domain.Repositories.User;
using UserService.Domain.Security.Cryptography;
using UserService.Domain.Services.LoggedUser;

namespace UserService.Application.UseCase.User.UpdatePassword;
using Domain.Entities;

public class UpdatePasswordUseCase(
    ILoggedUser loggedUser,
    IEncrypter encrypter,
    IUserWriteRepository writeRepository,
    IUnitOfWork unitOfWork) : IUpdatePasswordUseCase
{
    public async Task Execute(UpdatePasswordRequest request)
    {
        User user = await loggedUser.Get();
        
        Validate(request: request, user: user);
        
        user.Password = encrypter.Encrypt(value: request.NewPassword);
        writeRepository.Update(user: user);
        await unitOfWork.Commit();
    }
    
    private void Validate(UpdatePasswordRequest request, User user)
    {
        ValidationResult resultPassword = new PasswordValidator().Validate(instance: request.NewPassword);

        if (!resultPassword.IsValid)
        {
            List<string> errors = resultPassword.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }

        if (!string.IsNullOrEmpty(value: user.Password))
        {
            bool passwordMatch = encrypter.Verify(value: request.OldPassword, hash: user.Password);
            if (!passwordMatch)
            {
                throw new BadRequestException(message: ResourceErrorMessages.OLD_PASSWORD_INVALID);
            }
        }
    }
}