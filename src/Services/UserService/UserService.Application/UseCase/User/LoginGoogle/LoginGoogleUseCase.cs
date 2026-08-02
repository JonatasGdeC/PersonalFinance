using AutoMapper;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses.User;
using PersonalFinance.Exception.ExceptionBase;
using UserService.Domain.Repositories;
using UserService.Domain.Repositories.User;
using UserService.Domain.Security.External;
using UserService.Domain.Security.Tokens;

namespace UserService.Application.UseCase.User.LoginGoogle;
using Domain.Entities;

public class LoginGoogleUseCase(
    IGoogleAuthenticator googleAuthenticator,
    IUserReadRepository userReadRepository,
    IUserWriteRepository userWriteRepository,
    IUnitOfWork unitOfWork,
    IAccessTokenGenerator tokenGenerator,
    IMapper mapper) : ILoginGoogleUseCase
{
    public async Task<LoginResponse> Execute(LoginGoogleRequest request)
    {
        GoogleUserInfo googleUser = await googleAuthenticator.ValidateAndGetUserInfo(idToken: request.IdToken);
        if (!googleUser.EmailVerified)
        {
            throw new InvalidLoginException();
        }

        User? user = await userReadRepository.GetByEmail(email: googleUser.Email);

        if (user == null)
        {
            user = new User
            {
                Name = googleUser.Name,
                Email = googleUser.Email,
                GoogleId = googleUser.GoogleId
            };

            await userWriteRepository.Add(user: user);
        }
        else if (string.IsNullOrEmpty(value: user.GoogleId))
        {
            user.GoogleId = googleUser.GoogleId;
        }


        await unitOfWork.Commit();

        return new LoginResponse
        {
            User = mapper.Map<UserDto>(source: user),
            Token = tokenGenerator.Generate(user: user)
        };
    }
}