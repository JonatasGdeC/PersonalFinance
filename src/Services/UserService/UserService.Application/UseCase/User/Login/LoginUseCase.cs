using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;
using PersonalFinance.Exception.ExceptionBase;
using UserService.Domain.Repositories.User;
using UserService.Domain.Security.Cryptography;
using UserService.Domain.Security.Tokens;

namespace UserService.Application.UseCase.User.Login;
using Domain.Entities;

public class LoginUseCase(
    IUserReadRepository readRepository,
    IEncrypter passwordEncrypter,
    IAccessTokenGenerator tokenGenerator,
    IMapper mapper) : ILoginUseCase
{
    public async Task<LoginResponse> Execute(LoginRequest request)
    {
        User? user = await readRepository.GetByEmail(email: request.Email);

        if (user == null || string.IsNullOrEmpty(value: user.Password) || !passwordEncrypter.Verify(value: request.Password, hash: user.Password))
        {
            throw new InvalidLoginException();
        }

        return new LoginResponse
        {
            User = mapper.Map<UserDto>(source: user),
            Token = tokenGenerator.Generate(user: user)
        };
    }
}