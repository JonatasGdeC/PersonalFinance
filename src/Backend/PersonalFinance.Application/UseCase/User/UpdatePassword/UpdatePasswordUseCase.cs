using FluentValidation.Results;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Domain.Repositories;
using PersonalFinance.Domain.Repositories.User;
using PersonalFinance.Domain.Security.Cryptography;
using PersonalFinance.Domain.Services.LoggedUser;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.User.UpdatePassword;
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