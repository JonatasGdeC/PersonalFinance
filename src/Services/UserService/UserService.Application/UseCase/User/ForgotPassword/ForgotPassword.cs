using MassTransit;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Contracts.Events;
using UserService.Domain.Repositories;
using UserService.Domain.Repositories.PasswordResetCode;
using UserService.Domain.Repositories.User;
using UserService.Domain.Security.CodeGenerator;
using UserService.Domain.Security.Cryptography;

namespace UserService.Application.UseCase.User.ForgotPassword;
using Domain.Entities;

public class ForgotPassword(
    IUserReadRepository userReadRepository,
    IPasswordResetCodeRepository passwordResetCodeRepository,
    ICodeGenerator codeGenerator,
    IEncrypter encrypter,
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint) : IForgotPassword
{
    public async Task Execute(ForgotPasswordRequest request)
    {
        User? user = await userReadRepository.GetByEmail(email: request.Email);
        if (user != null)
        {
            string code = codeGenerator.Generate();

            PasswordResetCode? resetCodeByUser = await passwordResetCodeRepository.GetByUserId(userId: user.Id);
            if (resetCodeByUser != null)
            {
                passwordResetCodeRepository.Remove(passwordResetCode: resetCodeByUser);
            }

            PasswordResetCode passwordResetCode = new()
            {
                UserId = user.Id,
                CodeHash = encrypter.Encrypt(value: code),
                Attempts = 0,
                ExpiresAt = DateTime.UtcNow.AddMinutes(value: 10),
            };

            await passwordResetCodeRepository.Add(passwordResetCode: passwordResetCode);
            await unitOfWork.Commit();
            
            await publishEndpoint.Publish(message: new PasswordResetCodeEvent
            {
                Email = user.Email,
                UserName = user.Name,
                Code = code
            });
        }
    }
}