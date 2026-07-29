using AutoMapper;
using FluentValidation.Results;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;
using PersonalFinance.Communication.Validators;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;
using UserService.Domain.Repositories;
using UserService.Domain.Repositories.User;
using UserService.Domain.Security.Cryptography;
using UserService.Domain.Security.Tokens;

namespace UserService.Application.UseCase.User.Register;
using Domain.Entities;

public class RegisterUserUseCase(
    IUserWriteRepository writeRepository,
    IUserReadRepository readRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IEncrypter passwordEncrypter,
    IAccessTokenGenerator tokenGenerator) : IRegisterUserUseCase
{
    public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
    {
        await Validate(request: request);

        User user = mapper.Map<User>(source: request);
        user.Password = passwordEncrypter.Encrypt(value: request.Password);

        await writeRepository.Add(user: user);
        await unitOfWork.Commit();
        
        return new RegisterUserResponse
        {
            User = mapper.Map<UserDto>(source: user),
            Token = tokenGenerator.Generate(user: user)
        };
    }

    private async Task Validate(RegisterUserRequest request)
    {
        UserValidator validator = new();
        ValidationResult? result = await validator.ValidateAsync(instance: request);

        User? existingUser = await readRepository.GetByEmail(email: request.Email);
        if (existingUser != null)
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