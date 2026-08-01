using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;
using UserService.Domain.Repositories;
using UserService.Domain.Repositories.PasswordResetCode;
using UserService.Domain.Repositories.User;
using UserService.Domain.Security.Cryptography;
using UserService.Domain.Security.Tokens;

namespace UserService.Application.UseCase.User.ValidateResetCode;
using Domain.Entities;

public class ValidateResetCodeUseCase(
    IUserReadRepository userReadRepository,
    IPasswordResetCodeRepository passwordResetCodeRepository,
    IEncrypter encrypter,
    IPasswordResetTokenGenerator passwordResetTokenGenerator,
    IUnitOfWork unitOfWork) : IValidateResetCodeUseCase
{
    public async Task<ValidateResetCodeResponse> Execute(ValidateResetCodeRequest request)
    {
        User user = await userReadRepository.GetByEmail(email: request.Email) ??
                    throw new BadRequestException(message: ResourceErrorMessages.CODE_INVALID);
        
        PasswordResetCode passwordResetCode = await passwordResetCodeRepository.GetByUserId(userId: user.Id) ??
                                              throw new BadRequestException(message: ResourceErrorMessages.CODE_INVALID);

        if (!passwordResetCode.IsValid)
        {
            throw new BadRequestException(message: ResourceErrorMessages.CODE_INVALID);
        }

        bool isValidCode = encrypter.Verify(value: request.Code, hash: passwordResetCode.CodeHash);
        if (!isValidCode)
        {
            passwordResetCode.Attempts++;
            passwordResetCodeRepository.Update(passwordResetCode: passwordResetCode);
            await unitOfWork.Commit();
            throw new BadRequestException(message: ResourceErrorMessages.CODE_INVALID);
        }

        return new ValidateResetCodeResponse
        {
            TokenResetPassword = passwordResetTokenGenerator.Generate(user: user)
        };
    }
}