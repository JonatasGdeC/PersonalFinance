using FluentValidation.Results;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;
using UserService.Domain.Repositories;
using UserService.Domain.Repositories.PasswordResetCode;
using UserService.Domain.Repositories.User;
using UserService.Domain.Security.Cryptography;
using UserService.Domain.Security.Tokens;

namespace UserService.Application.UseCase.User.ResetPassword;
using Domain.Entities;

public class ResetPassword(
    IVerifyTokenResetCode verifyTokenResetCode,
    IUserWriteRepository userWriteRepository, 
    IEncrypter encrypter,
    IUnitOfWork unitOfWork,
    IPasswordResetCodeRepository passwordResetCodeRepository) : IResetPassword
{
    public async Task Execute(ResetPasswordRequest request)
    {
        Validate(request: request);
        
        Guid userId = verifyTokenResetCode.GetUserId(token: request.TokenResetPassword) ?? throw new BadRequestException(message: ResourceErrorMessages.INVALID_TOKEN);
        User user = await userWriteRepository.GetById(id: userId) ?? throw new BadRequestException(message: ResourceErrorMessages.INVALID_TOKEN);
        PasswordResetCode resetCode = await passwordResetCodeRepository.GetByUserId(userId: userId) ?? throw new BadRequestException(message: ResourceErrorMessages.INVALID_TOKEN);

        user.Password = encrypter.Encrypt(value: request.NewPassword);

        userWriteRepository.Update(user: user);
        passwordResetCodeRepository.Remove(passwordResetCode: resetCode);

        await unitOfWork.Commit();
    }

    private void Validate(ResetPasswordRequest request)
    {
        ValidationResult resultPassword = new PasswordValidator().Validate(instance: request.NewPassword);

        if (!resultPassword.IsValid)
        {
            List<string> errors = resultPassword.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}