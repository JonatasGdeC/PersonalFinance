using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;
using PersonalFinance.Domain.Repositories.User;
using PersonalFinance.Domain.Security.Cryptography;
using PersonalFinance.Domain.Security.Tokens;
using PersonalFinance.Exception.ExceptionBase;

namespace PersonalFinance.Application.UseCase.User.Login;
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